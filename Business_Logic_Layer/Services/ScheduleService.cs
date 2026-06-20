using Microsoft.EntityFrameworkCore;
using Data_Access_Layer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    public class ScheduleService
    {
        public async Task<List<Schedule>> GetAllAsync()
        {
            await using var context = new AppDbContext();
            return await context.Schedules
                .Include(s => s.Subject)
                .Include(s => s.FacultyMember)
                .Include(s => s.Classroom)
                .Include(s => s.TimeSlot)
                .Include(s => s.StudyYear)
                .Include(s => s.Branch)
                .Include(s => s.Section)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<ScheduleDetailsView>> GetScheduleDetailsAsync()
        {
            return await GetScheduleDetailsByFiltersAsync(null, null, null, null, null);
        }

        public async Task<List<ScheduleDetailsView>> GetScheduleDetailsBySemesterAsync(int semesterNumber)
        {
            return await GetScheduleDetailsByFiltersAsync(semesterNumber, null, null, null, null);
        }

        public async Task<List<ScheduleDetailsView>> GetScheduleDetailsByFiltersAsync(
            int? semesterNumber,
            int? studyYearId,
            int? branchId,
            int? sectionId,
            string? lectureType = null)
        {
            await using var context = new AppDbContext();
            var query = context.ScheduleDetails.AsNoTracking().AsQueryable();

            if (semesterNumber.HasValue)
            {
                query = query.Where(schedule => schedule.SemesterNumber == semesterNumber.Value);
            }

            if (!string.IsNullOrWhiteSpace(lectureType) &&
                !string.Equals(lectureType, "All", StringComparison.OrdinalIgnoreCase))
            {
                string normalizedLectureType = NormalizeLectureType(lectureType);
                query = query.Where(schedule => schedule.LectureType == normalizedLectureType);
            }

            if (studyYearId.HasValue || branchId.HasValue || sectionId.HasValue)
            {
                var scheduleIds = context.Schedules.AsNoTracking().AsQueryable();

                if (studyYearId.HasValue)
                {
                    scheduleIds = scheduleIds.Where(schedule => schedule.StudyYearID == studyYearId.Value);
                }

                if (branchId.HasValue)
                {
                    scheduleIds = scheduleIds.Where(schedule => schedule.BranchID == branchId.Value);
                }

                if (sectionId.HasValue)
                {
                    scheduleIds = scheduleIds.Where(schedule => schedule.SectionID == sectionId.Value);
                }

                query = query.Where(schedule => scheduleIds
                    .Select(item => item.ScheduleID)
                    .Contains(schedule.ScheduleID));
            }

            var rows = await query.ToListAsync();

            return rows
                .OrderBy(row => StudyYearOrder(row.YearName))
                .ThenBy(row => row.BranchName)
                .ThenBy(row => row.SectionName)
                .ThenBy(row => row.SemesterNumber)
                .ThenBy(row => DayOrder(row.DayOfWeek))
                .ThenBy(row => row.StartTime)
                .ToList();
        }

        public async Task<Schedule?> GetByIdAsync(int id)
        {
            await using var context = new AppDbContext();
            return await context.Schedules
                .Include(s => s.Subject)
                .Include(s => s.FacultyMember)
                .Include(s => s.Classroom)
                .Include(s => s.TimeSlot)
                .Include(s => s.StudyYear)
                .Include(s => s.Branch)
                .Include(s => s.Section)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.ScheduleID == id);
        }

        public async Task<Schedule> AddAsync(Schedule schedule)
        {
            await using var context = new AppDbContext();
            await ValidateAsync(context, schedule, false);
            await context.Schedules.AddAsync(schedule);
            await context.SaveChangesAsync();
            return schedule;
        }

        public async Task<Schedule> UpdateAsync(Schedule schedule)
        {
            await using var context = new AppDbContext();
            await ValidateAsync(context, schedule, true);
            context.Schedules.Update(schedule);
            await context.SaveChangesAsync();
            return schedule;
        }

        public bool IsClassroomCapacityEnough(int classroomId, int sectionId)
        {
            using var context = new AppDbContext();

            var classroom = context.Classrooms
                .AsNoTracking()
                .FirstOrDefault(item => item.ClassroomID == classroomId);
            var section = context.Sections
                .AsNoTracking()
                .FirstOrDefault(item => item.SectionID == sectionId);

            return classroom is not null &&
                section is not null &&
                IsClassroomCapacityEnough(classroom, section);
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = new AppDbContext();
            var schedule = await context.Schedules.FindAsync(id)
                ?? throw new KeyNotFoundException("Schedule not found.");

            context.Schedules.Remove(schedule);
            await context.SaveChangesAsync();
        }

        public async Task<ScheduleGenerationResult> GenerateAsync()
        {
            await using var context = new AppDbContext();

            var subjects = await context.Subjects
                .AsNoTracking()
                .ToListAsync();

            var assignments = await context.FacultyMemberSubjects
                .Include(fms => fms.Subject)
                .OrderBy(fms => fms.Subject.StudyYearID)
                .ThenBy(fms => fms.Subject.SubjectName)
                .AsNoTracking()
                .ToListAsync();

            var sections = (await context.Sections
                .AsNoTracking()
                .OrderBy(section => section.StudyYearID)
                .ThenBy(section => section.BranchID)
                .ThenBy(section => section.SectionName)
                .ToListAsync())
                .Where(IsSchedulableSection)
                .ToList();

            var classrooms = await context.Classrooms
                .AsNoTracking()
                .OrderBy(classroom => classroom.Capacity)
                .ThenBy(classroom => classroom.ClassroomNumber)
                .ToListAsync();

            var timeSlots = await context.TimeSlots
                .AsNoTracking()
                .Where(slot => !slot.IsBreak)
                .OrderBy(slot => slot.StartTime)
                .ToListAsync();

            var existingSchedules = await context.Schedules.ToListAsync();
            var generatedSchedules = new List<Schedule>();
            string[] days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday"];
            int skippedCount = 0;
            int missingSectionCount = 0;
            int noClassroomCount = 0;
            int conflictCount = 0;
            int duplicateAssignmentCount = assignments.Count - assignments
                .Select(assignment => assignment.SubjectID)
                .Distinct()
                .Count();

            var assignedSubjectIds = assignments
                .Select(assignment => assignment.SubjectID)
                .ToHashSet();
            int unassignedSubjectsCount = subjects.Count(subject => !assignedSubjectIds.Contains(subject.SubjectID));

            var primaryAssignments = assignments
                .GroupBy(assignment => assignment.SubjectID)
                .Select(group => group.First())
                .ToList();

            var scheduleRequests = primaryAssignments
                .SelectMany(assignment =>
                {
                    var subject = assignment.Subject;
                    var matchingSections = sections
                        .Where(section =>
                            section.StudyYearID == subject.StudyYearID &&
                            AcademicStructureRules.SectionMatchesSubject(
                                subject.StudyYearID,
                                subject.BranchID,
                                section.BranchID,
                                section.SectionName))
                        .ToList();

                    if (matchingSections.Count == 0)
                    {
                        skippedCount++;
                        missingSectionCount++;
                    }

                    return matchingSections.SelectMany(section => CreateScheduleRequests(assignment, section));
                })
                .OrderByDescending(request => request.Assignment.Subject.StudyYearID)
                .ThenBy(request => request.Assignment.Subject.BranchID ?? request.Section.BranchID ?? 0)
                .ThenBy(request => request.Assignment.Subject.SemesterNumber)
                .ThenBy(request => request.Assignment.Subject.SubjectName)
                .ThenBy(request => request.LessonNumber)
                .ThenBy(request => request.Section.SectionName)
                .ToList();

            foreach (var request in scheduleRequests)
            {
                var assignment = request.Assignment;
                var subject = assignment.Subject;
                var section = request.Section;

                int requiredCapacity = GetRequiredCapacity(section, request.GroupName);
                var targetClassrooms = classrooms
                    .Where(classroom => classroom.Capacity >= requiredCapacity)
                    .ToList();

                if (targetClassrooms.Count == 0)
                {
                    skippedCount++;
                    noClassroomCount++;
                    continue;
                }

                bool created = false;

                foreach (string day in days)
                {
                    foreach (var timeSlot in timeSlots)
                    {
                        foreach (var classroom in targetClassrooms)
                        {
                            var candidate = new Schedule
                            {
                                SubjectID = subject.SubjectID,
                                FacultyMemberID = assignment.FacultyMemberID,
                                ClassroomID = classroom.ClassroomID,
                                TimeSlotID = timeSlot.TimeSlotID,
                                SemesterNumber = subject.SemesterNumber,
                                LectureType = request.LectureType,
                                GroupName = request.GroupName,
                                DayOfWeek = day,
                                StudyYearID = subject.StudyYearID,
                                BranchID = subject.BranchID ?? section.BranchID,
                                SectionID = section.SectionID
                            };

                            if (HasScheduleConflict(generatedSchedules, candidate))
                            {
                                continue;
                            }

                            if (request.RequiredLessons > 1 &&
                                HasSameSubjectSectionOnDay(generatedSchedules, candidate))
                            {
                                continue;
                            }

                            generatedSchedules.Add(candidate);
                            created = true;
                            break;
                        }

                        if (created)
                        {
                            break;
                        }
                    }
                }

                if (!created && request.RequiredLessons > 1)
                {
                    foreach (string day in days)
                    {
                        foreach (var timeSlot in timeSlots)
                        {
                            foreach (var classroom in targetClassrooms)
                            {
                                var candidate = new Schedule
                                {
                                    SubjectID = subject.SubjectID,
                                    FacultyMemberID = assignment.FacultyMemberID,
                                    ClassroomID = classroom.ClassroomID,
                                    TimeSlotID = timeSlot.TimeSlotID,
                                    SemesterNumber = subject.SemesterNumber,
                                    LectureType = request.LectureType,
                                    GroupName = request.GroupName,
                                    DayOfWeek = day,
                                    StudyYearID = subject.StudyYearID,
                                    BranchID = subject.BranchID ?? section.BranchID,
                                    SectionID = section.SectionID
                                };

                                if (HasScheduleConflict(generatedSchedules, candidate))
                                {
                                    continue;
                                }

                                generatedSchedules.Add(candidate);
                                created = true;
                                break;
                            }

                            if (created)
                            {
                                break;
                            }
                        }

                        if (created)
                        {
                            break;
                        }
                    }
                }

                if (!created)
                {
                    skippedCount++;
                    conflictCount++;
                }
            }

            await using var transaction = await context.Database.BeginTransactionAsync();

            context.Schedules.RemoveRange(existingSchedules);
            await context.SaveChangesAsync();

            await context.Schedules.AddRangeAsync(generatedSchedules);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new ScheduleGenerationResult(
                generatedSchedules.Count,
                skippedCount,
                scheduleRequests.Count,
                unassignedSubjectsCount,
                missingSectionCount,
                noClassroomCount,
                conflictCount,
                duplicateAssignmentCount,
                timeSlots.Count,
                classrooms.Count,
                sections.Count);
        }

        private static async Task ValidateAsync(AppDbContext context, Schedule schedule, bool isUpdate)
        {
            if (string.IsNullOrWhiteSpace(schedule.DayOfWeek))
            {
                throw new ArgumentException("Day of week is required.");
            }

            schedule.DayOfWeek = schedule.DayOfWeek.Trim();

            var subject = await context.Subjects
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SubjectID == schedule.SubjectID);

            if (subject is null)
            {
                throw new ArgumentException("Subject does not exist.");
            }

            if (!await context.FacultyMembers.AnyAsync(f => f.FacultyMemberID == schedule.FacultyMemberID))
            {
                throw new ArgumentException("Faculty member does not exist.");
            }

            if (!await CanFacultyTeachSubjectAsync(context, schedule.FacultyMemberID, schedule.SubjectID))
            {
                throw new ArgumentException("The selected faculty member is not assigned to teach this subject.");
            }

            var classroom = await context.Classrooms
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClassroomID == schedule.ClassroomID);

            if (classroom is null)
            {
                throw new ArgumentException("Classroom does not exist.");
            }

            if (!await context.TimeSlots.AnyAsync(t => t.TimeSlotID == schedule.TimeSlotID))
            {
                throw new ArgumentException("Time slot does not exist.");
            }

            if (!schedule.SectionID.HasValue)
            {
                throw new ArgumentException("Section is required.");
            }

            var section = await context.Sections
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SectionID == schedule.SectionID.Value);

            if (section is null)
            {
                throw new ArgumentException("Section does not exist.");
            }

            EnsureBaseSectionIsUsed(section);
            NormalizeLectureTypeAndGroup(schedule, section);

            if (!IsClassroomCapacityEnough(classroom, section, schedule.GroupName))
            {
                throw new ArgumentException("The selected classroom capacity is not enough for this section.");
            }

            if (section.StudyYearID != subject.StudyYearID)
            {
                throw new ArgumentException("The selected section does not belong to the subject study year.");
            }

            if (!AcademicStructureRules.SectionMatchesSubject(
                subject.StudyYearID,
                subject.BranchID,
                section.BranchID,
                section.SectionName))
            {
                throw new ArgumentException("The selected section is not valid for this subject.");
            }

            schedule.StudyYearID = subject.StudyYearID;
            schedule.BranchID = subject.BranchID ?? section.BranchID;
            schedule.SemesterNumber = subject.SemesterNumber;

            await EnsureNoConflictsAsync(context, schedule, isUpdate);
        }

        private static async Task EnsureNoConflictsAsync(AppDbContext context, Schedule schedule, bool isUpdate)
        {
            if (await HasClassroomConflictAsync(context, schedule, isUpdate))
            {
                throw new ArgumentException("This classroom is already booked for the selected day and time slot.");
            }

            if (await HasFacultyConflictAsync(context, schedule, isUpdate))
            {
                throw new ArgumentException("This faculty member is already booked for the selected day and time slot.");
            }

            if (await HasSectionOrGroupConflictAsync(context, schedule, isUpdate))
            {
                throw new ArgumentException("This study year, branch, or section already has a schedule in the selected slot.");
            }
        }

        private static async Task<bool> HasClassroomConflictAsync(AppDbContext context, Schedule schedule, bool isUpdate)
        {
            return await context.Schedules.AnyAsync(s =>
                s.ClassroomID == schedule.ClassroomID &&
                s.SemesterNumber == schedule.SemesterNumber &&
                s.TimeSlotID == schedule.TimeSlotID &&
                s.DayOfWeek == schedule.DayOfWeek &&
                (!isUpdate || s.ScheduleID != schedule.ScheduleID));
        }

        private static async Task<bool> HasFacultyConflictAsync(AppDbContext context, Schedule schedule, bool isUpdate)
        {
            return await context.Schedules.AnyAsync(s =>
                s.FacultyMemberID == schedule.FacultyMemberID &&
                s.SemesterNumber == schedule.SemesterNumber &&
                s.TimeSlotID == schedule.TimeSlotID &&
                s.DayOfWeek == schedule.DayOfWeek &&
                (!isUpdate || s.ScheduleID != schedule.ScheduleID));
        }

        private static async Task<bool> HasSectionOrGroupConflictAsync(AppDbContext context, Schedule schedule, bool isUpdate)
        {
            return await context.Schedules.AnyAsync(s =>
                s.StudyYearID == schedule.StudyYearID &&
                s.BranchID == schedule.BranchID &&
                s.SectionID == schedule.SectionID &&
                s.SemesterNumber == schedule.SemesterNumber &&
                s.TimeSlotID == schedule.TimeSlotID &&
                s.DayOfWeek == schedule.DayOfWeek &&
                (schedule.GroupName == null || s.GroupName == null || s.GroupName == schedule.GroupName) &&
                (!isUpdate || s.ScheduleID != schedule.ScheduleID));
        }

        private static async Task<bool> CanFacultyTeachSubjectAsync(
            AppDbContext context,
            int facultyMemberId,
            int subjectId)
        {
            return await context.FacultyMemberSubjects.AnyAsync(assignment =>
                assignment.FacultyMemberID == facultyMemberId &&
                assignment.SubjectID == subjectId);
        }

        private static void EnsureBaseSectionIsUsed(Section section)
        {
            if (!AcademicStructureRules.UsesGeneralSections(section.StudyYearID))
            {
                return;
            }

            var allowedSections = AcademicStructureRules.GetAllowedSectionNames(section.StudyYearID);

            if (!allowedSections.Contains(section.SectionName.Trim(), StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException("A1, A2, B1, and B2 must be stored as practical groups, not as independent sections.");
            }
        }

        private static bool IsSchedulableSection(Section section)
        {
            if (AcademicStructureRules.UsesGeneralSections(section.StudyYearID))
            {
                return !section.BranchID.HasValue &&
                    AcademicStructureRules.GetAllowedSectionNames(section.StudyYearID)
                        .Contains(section.SectionName.Trim(), StringComparer.OrdinalIgnoreCase);
            }

            return true;
        }

        private static bool IsClassroomCapacityEnough(Classroom classroom, Section section)
        {
            return IsClassroomCapacityEnough(classroom, section, null);
        }

        private static bool IsClassroomCapacityEnough(Classroom classroom, Section section, string? groupName)
        {
            return classroom.Capacity >= GetRequiredCapacity(section, groupName);
        }

        private static int GetRequiredCapacity(Section section, string? groupName)
        {
            if (!string.IsNullOrWhiteSpace(groupName) &&
                AcademicStructureRules.UsesGeneralSections(section.StudyYearID))
            {
                return Math.Max(1, (int)Math.Ceiling(section.StudentCount / 2.0));
            }

            return section.StudentCount;
        }

        private static void NormalizeLectureTypeAndGroup(Schedule schedule, Section section)
        {
            schedule.LectureType = NormalizeLectureType(schedule.LectureType);

            if (schedule.LectureType == "Theory")
            {
                schedule.GroupName = null;
                return;
            }

            string? normalizedGroupName = string.IsNullOrWhiteSpace(schedule.GroupName)
                ? null
                : schedule.GroupName.Trim().ToUpperInvariant();

            if (AcademicStructureRules.UsesGeneralSections(section.StudyYearID))
            {
                if (!IsValidGroupForSection(section, normalizedGroupName))
                {
                    throw new ArgumentException("Practical sessions for first and second year must use A1/A2 for section A or B1/B2 for section B.");
                }

                schedule.GroupName = normalizedGroupName;
                return;
            }

            if (!string.IsNullOrWhiteSpace(normalizedGroupName))
            {
                throw new ArgumentException("Practical group names A1, A2, B1, and B2 are only valid for first and second year sections.");
            }

            schedule.GroupName = null;
        }

        private static bool IsValidGroupForSection(Section section, string? groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                return false;
            }

            string normalizedGroupName = groupName.Trim().ToUpperInvariant();

            if (!AcademicStructureRules.GetAllowedPracticalGroupNames(section.StudyYearID)
                .Contains(normalizedGroupName, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }

            string baseSectionName = AcademicStructureRules.GetBaseSectionName(normalizedGroupName);
            return string.Equals(baseSectionName, section.SectionName.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        private static string NormalizeLectureType(string? lectureType)
        {
            if (string.Equals(lectureType, "Practical", StringComparison.OrdinalIgnoreCase))
            {
                return "Practical";
            }

            if (string.IsNullOrWhiteSpace(lectureType) ||
                string.Equals(lectureType, "Theory", StringComparison.OrdinalIgnoreCase))
            {
                return "Theory";
            }

            throw new ArgumentException("Lecture type must be Theory or Practical.");
        }

        private static bool HasScheduleConflict(IEnumerable<Schedule> schedules, Schedule candidate)
        {
            return schedules.Any(schedule =>
                schedule.SemesterNumber == candidate.SemesterNumber &&
                schedule.TimeSlotID == candidate.TimeSlotID &&
                schedule.DayOfWeek == candidate.DayOfWeek &&
                (schedule.ClassroomID == candidate.ClassroomID ||
                 schedule.FacultyMemberID == candidate.FacultyMemberID ||
                 (schedule.StudyYearID == candidate.StudyYearID &&
                  schedule.BranchID == candidate.BranchID &&
                  schedule.SectionID == candidate.SectionID &&
                  (candidate.GroupName == null ||
                   schedule.GroupName == null ||
                   schedule.GroupName == candidate.GroupName))));
        }

        private static bool HasSameSubjectSectionOnDay(IEnumerable<Schedule> schedules, Schedule candidate)
        {
            return schedules.Any(schedule =>
                schedule.SubjectID == candidate.SubjectID &&
                schedule.SectionID == candidate.SectionID &&
                schedule.SemesterNumber == candidate.SemesterNumber &&
                (schedule.GroupName == null ||
                 candidate.GroupName == null ||
                 schedule.GroupName == candidate.GroupName) &&
                schedule.DayOfWeek == candidate.DayOfWeek);
        }

        private static IEnumerable<ScheduleGenerationRequest> CreateScheduleRequests(
            FacultyMemberSubject assignment,
            Section section)
        {
            var subject = assignment.Subject;
            int theoryLessons = CalculateTheoryLessonCount(subject);

            foreach (int lessonNumber in Enumerable.Range(1, theoryLessons))
            {
                yield return new ScheduleGenerationRequest(
                    assignment,
                    section,
                    lessonNumber,
                    theoryLessons,
                    "Theory",
                    null);
            }

            int practicalLessons = CalculatePracticalLessonCount(subject);

            if (practicalLessons <= 0)
            {
                yield break;
            }

            var practicalGroups = GetPracticalGroupsForSection(section);

            if (practicalGroups.Count == 0)
            {
                foreach (int lessonNumber in Enumerable.Range(1, practicalLessons))
                {
                    yield return new ScheduleGenerationRequest(
                        assignment,
                        section,
                        lessonNumber,
                        practicalLessons,
                        "Practical",
                        null);
                }

                yield break;
            }

            foreach (string groupName in practicalGroups)
            {
                foreach (int lessonNumber in Enumerable.Range(1, practicalLessons))
                {
                    yield return new ScheduleGenerationRequest(
                        assignment,
                        section,
                        lessonNumber,
                        practicalLessons,
                        "Practical",
                        groupName);
                }
            }
        }

        private static int CalculateTheoryLessonCount(Subject subject)
        {
            if (subject.TheoreticalHours > 0)
            {
                return (int)Math.Ceiling(subject.TheoreticalHours);
            }

            if (subject.PracticalHours > 0)
            {
                return 0;
            }

            var fallbackHours = subject.CreditUnits > 0 ? subject.CreditUnits : 1;
            return Math.Max(1, (int)Math.Ceiling(fallbackHours));
        }

        private static int CalculatePracticalLessonCount(Subject subject)
        {
            return subject.PracticalHours > 0
                ? Math.Max(1, (int)Math.Ceiling(subject.PracticalHours))
                : 0;
        }

        private static IReadOnlyList<string> GetPracticalGroupsForSection(Section section)
        {
            if (!AcademicStructureRules.UsesGeneralSections(section.StudyYearID))
            {
                return [];
            }

            return AcademicStructureRules.GetBaseSectionName(section.SectionName).ToUpperInvariant() switch
            {
                "A" => ["A1", "A2"],
                "B" => ["B1", "B2"],
                _ => []
            };
        }

        private static int StudyYearOrder(string yearName)
        {
            return yearName.Trim().ToLowerInvariant() switch
            {
                "first year" => 1,
                "second year" => 2,
                "third year" => 3,
                "fourth year" => 4,
                _ => 99
            };
        }

        private static int DayOrder(string day)
        {
            return day.Trim().ToLowerInvariant() switch
            {
                "sunday" => 1,
                "monday" => 2,
                "tuesday" => 3,
                "wednesday" => 4,
                "thursday" => 5,
                _ => 99
            };
        }
    }

    public sealed record ScheduleGenerationResult(
        int CreatedCount,
        int SkippedCount,
        int RequiredCount,
        int UnassignedSubjectsCount,
        int MissingSectionCount,
        int NoClassroomCount,
        int ConflictCount,
        int DuplicateAssignmentCount,
        int TimeSlotCount,
        int ClassroomCount,
        int SectionCount);

    internal sealed record ScheduleGenerationRequest(
        FacultyMemberSubject Assignment,
        Section Section,
        int LessonNumber,
        int RequiredLessons,
        string LectureType,
        string? GroupName);
}
