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
            var resourceResult = await SchedulingResourceMaintenance.EnsureOfficialResourcesAsync(context);

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

            var timeSlots = (await context.TimeSlots
                .AsNoTracking()
                .Where(slot => !slot.IsBreak)
                .OrderBy(slot => slot.StartTime)
                .ToListAsync())
                .Where(ScheduleTimingRules.IsOfficialLectureSlot)
                .ToList();

            var existingSchedules = await context.Schedules.ToListAsync();
            var generatedSchedules = new List<Schedule>();
            int skippedCount = 0;
            int missingSectionCount = 0;
            int noClassroomCount = 0;
            int conflictCount = 0;
            int duplicateAssignmentCount = assignments
                .GroupBy(assignment => new { assignment.FacultyMemberID, assignment.SubjectID })
                .Sum(group => Math.Max(0, group.Count() - 1));

            var assignedSubjectIds = assignments
                .Select(assignment => assignment.SubjectID)
                .ToHashSet();
            int unassignedSubjectsCount = subjects.Count(subject => !assignedSubjectIds.Contains(subject.SubjectID));

            var subjectTeachingAssignments = assignments
                .GroupBy(assignment => assignment.SubjectID)
                .Select(group => new SubjectTeachingAssignment(
                    group.First().Subject,
                    group.Select(assignment => assignment.FacultyMemberID)
                        .Distinct()
                        .OrderBy(facultyMemberId => facultyMemberId)
                        .ToList()))
                .ToList();

            var scheduleRequests = subjectTeachingAssignments
                .SelectMany(teachingAssignment =>
                {
                    var subject = teachingAssignment.Subject;
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

                    return matchingSections.SelectMany(section => CreateScheduleRequests(teachingAssignment, section));
                })
                .OrderByDescending(request => request.Subject.StudyYearID)
                .ThenBy(request => request.Subject.BranchID ?? request.Section.BranchID ?? 0)
                .ThenBy(request => request.Subject.SemesterNumber)
                .ThenBy(request => request.Subject.SubjectName)
                .ThenBy(request => request.LessonNumber)
                .ThenBy(request => request.Section.SectionName)
                .ToList();

            var planningResult = BuildSchedulePlanFast(
                scheduleRequests,
                classrooms,
                timeSlots);

            generatedSchedules.AddRange(planningResult.Schedules);
            skippedCount += planningResult.NoClassroomCount + planningResult.ConflictCount;
            noClassroomCount += planningResult.NoClassroomCount;
            conflictCount += planningResult.ConflictCount;

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
                sections.Count,
                resourceResult.AddedTimeSlots,
                resourceResult.AddedClassrooms,
                resourceResult.AddedFacultyMembers,
                resourceResult.AddedFacultySubjectAssignments);
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
                throw new ArgumentException("This classroom is already booked in the selected semester, day, and time slot.");
            }

            if (await HasFacultyConflictAsync(context, schedule, isUpdate))
            {
                throw new ArgumentException("This faculty member is already booked in the selected semester, day, and time slot.");
            }

            if (await HasSectionOrGroupConflictAsync(context, schedule, isUpdate))
            {
                throw new ArgumentException("This section or group already has a schedule in the selected semester, day, and time slot.");
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
            if (!string.IsNullOrWhiteSpace(groupName))
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

            if (!IsValidGroupForSection(section, normalizedGroupName))
            {
                throw new ArgumentException("Practical sessions must use A1/A2 for section A or B1/B2 for section B.");
            }

            schedule.GroupName = normalizedGroupName;
        }

        private static bool IsValidGroupForSection(Section section, string? groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                return false;
            }

            string normalizedGroupName = groupName.Trim().ToUpperInvariant();

            if (!AcademicStructureRules.GetAllowedPracticalGroupNames(section.SectionName)
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

        private static SchedulePlanningResult BuildSchedulePlanFast(
            IReadOnlyList<ScheduleGenerationRequest> requests,
            IReadOnlyCollection<Classroom> classrooms,
            IReadOnlyCollection<TimeSlot> timeSlots)
        {
            var requestPlans = requests
                .Select(request => new ScheduleRequestPlan(
                    request,
                    CreatePlacementCandidates(request, classrooms, timeSlots).ToList(),
                    CalculateRequestDifficulty(request)))
                .ToList();

            int noClassroomCount = requestPlans.Count(plan => plan.Candidates.Count == 0);
            var pendingPlans = requestPlans
                .Where(plan => plan.Candidates.Count > 0)
                .ToList();

            var scheduleState = new ScheduleGenerationState();
            int conflictCount = 0;

            foreach (var plan in pendingPlans
                .OrderBy(plan => plan.Candidates.Count)
                .ThenByDescending(plan => plan.DifficultyScore)
                .ThenBy(plan => plan.Request.Subject.SemesterNumber)
                .ThenByDescending(plan => plan.Request.Subject.StudyYearID)
                .ThenBy(plan => plan.Request.Subject.SubjectName)
                .ThenBy(plan => plan.Request.Section.SectionName))
            {
                if (TryPlaceFast(plan, scheduleState, avoidSameSubjectDay: true) ||
                    TryPlaceFast(plan, scheduleState, avoidSameSubjectDay: false))
                {
                    continue;
                }

                conflictCount++;
            }

            return new SchedulePlanningResult(
                scheduleState.Schedules,
                noClassroomCount,
                conflictCount);
        }

        private static bool TryPlaceFast(
            ScheduleRequestPlan plan,
            ScheduleGenerationState scheduleState,
            bool avoidSameSubjectDay)
        {
            Schedule? bestSchedule = null;
            int bestScore = int.MaxValue;

            foreach (var candidate in plan.Candidates)
            {
                foreach (int facultyMemberId in scheduleState.GetOrderedFacultyOptions(
                    plan.Request.FacultyMemberIds,
                    plan.Request.Subject.SemesterNumber,
                    candidate.DayOfWeek))
                {
                    var schedule = BuildSchedule(plan.Request, candidate, facultyMemberId);

                    if (!scheduleState.CanPlace(schedule))
                    {
                        continue;
                    }

                    if (avoidSameSubjectDay &&
                        scheduleState.HasSameSubjectSectionOnDay(schedule))
                    {
                        continue;
                    }

                    int score = scheduleState.ScorePlacement(schedule, candidate);

                    if (score >= bestScore)
                    {
                        continue;
                    }

                    bestSchedule = schedule;
                    bestScore = score;
                }
            }

            if (bestSchedule is null)
            {
                return false;
            }

            scheduleState.Add(bestSchedule);
            return true;
        }

        private static IEnumerable<SchedulePlacementCandidate> CreatePlacementCandidates(
            ScheduleGenerationRequest request,
            IReadOnlyCollection<Classroom> classrooms,
            IReadOnlyCollection<TimeSlot> timeSlots)
        {
            int requiredCapacity = GetRequiredCapacity(request.Section, request.GroupName);
            var targetClassrooms = GetTargetClassrooms(
                classrooms,
                requiredCapacity,
                request.LectureType);
            var days = ScheduleDayRules.GetSchedulingDays(request.Subject.StudyYearID);

            foreach (string day in days)
            {
                foreach (var timeSlot in timeSlots)
                {
                    foreach (var classroom in targetClassrooms)
                    {
                        yield return new SchedulePlacementCandidate(
                            day,
                            timeSlot,
                            classroom,
                            requiredCapacity);
                    }
                }
            }
        }

        private static Schedule BuildSchedule(
            ScheduleGenerationRequest request,
            SchedulePlacementCandidate candidate,
            int facultyMemberId)
        {
            var subject = request.Subject;
            var section = request.Section;

            return new Schedule
            {
                SubjectID = subject.SubjectID,
                FacultyMemberID = facultyMemberId,
                ClassroomID = candidate.Classroom.ClassroomID,
                TimeSlotID = candidate.TimeSlot.TimeSlotID,
                SemesterNumber = subject.SemesterNumber,
                LectureType = request.LectureType,
                GroupName = request.GroupName,
                DayOfWeek = candidate.DayOfWeek,
                StudyYearID = subject.StudyYearID,
                BranchID = subject.BranchID ?? section.BranchID,
                SectionID = section.SectionID
            };
        }

        private static int CalculateRequestDifficulty(ScheduleGenerationRequest request)
        {
            int difficulty = GetRequiredCapacity(request.Section, request.GroupName);

            if (string.Equals(request.LectureType, "Practical", StringComparison.OrdinalIgnoreCase))
            {
                difficulty += 40;
            }

            if (!string.IsNullOrWhiteSpace(request.GroupName))
            {
                difficulty += 20;
            }

            difficulty += request.Subject.StudyYearID * 10;
            difficulty += Math.Max(0, 6 - request.FacultyMemberIds.Count) * 8;

            return difficulty;
        }

        private static IEnumerable<ScheduleGenerationRequest> CreateScheduleRequests(
            SubjectTeachingAssignment teachingAssignment,
            Section section)
        {
            var subject = teachingAssignment.Subject;
            int theoryLessons = CalculateTheoryLessonCount(subject);

            foreach (int lessonNumber in Enumerable.Range(1, theoryLessons))
            {
                yield return new ScheduleGenerationRequest(
                    subject,
                    teachingAssignment.FacultyMemberIds,
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
                        subject,
                        teachingAssignment.FacultyMemberIds,
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
                        subject,
                        teachingAssignment.FacultyMemberIds,
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
                return 1;
            }

            if (subject.PracticalHours > 0)
            {
                return 0;
            }

            var fallbackHours = subject.CreditUnits > 0 ? subject.CreditUnits : 1;
            return 1;
        }

        private static int CalculatePracticalLessonCount(Subject subject)
        {
            return subject.PracticalHours > 0 ? 1 : 0;
        }

        private static IReadOnlyList<string> GetPracticalGroupsForSection(Section section)
        {
            return AcademicStructureRules.GetAllowedPracticalGroupNames(section.SectionName);
        }

        private static List<Classroom> GetTargetClassrooms(
            IReadOnlyCollection<Classroom> classrooms,
            int requiredCapacity,
            string lectureType)
        {
            var capacityMatches = classrooms
                .Where(classroom => classroom.Capacity >= requiredCapacity)
                .OrderBy(classroom => classroom.Capacity)
                .ThenBy(classroom => classroom.ClassroomNumber)
                .ToList();

            var preferredRooms = capacityMatches
                .Where(classroom => RoomMatchesLectureType(classroom, lectureType))
                .ToList();

            return preferredRooms.Count > 0
                ? preferredRooms
                : capacityMatches;
        }

        private static bool RoomMatchesLectureType(Classroom classroom, string lectureType)
        {
            bool isLab = string.Equals(classroom.RoomType, "Lab", StringComparison.OrdinalIgnoreCase) ||
                classroom.ClassroomNumber.Contains("Lab", StringComparison.OrdinalIgnoreCase);

            return string.Equals(lectureType, "Practical", StringComparison.OrdinalIgnoreCase)
                ? isLab
                : !isLab;
        }

        private sealed class ScheduleGenerationState
        {
            private readonly HashSet<(int ClassroomID, int SemesterNumber, string DayOfWeek, int TimeSlotID)> classroomBusy = [];
            private readonly HashSet<(int FacultyMemberID, int SemesterNumber, string DayOfWeek, int TimeSlotID)> facultyBusy = [];
            private readonly HashSet<(int? StudyYearID, int? BranchID, int? SectionID, int SemesterNumber, string DayOfWeek, int TimeSlotID)> sectionAnyBusy = [];
            private readonly HashSet<(int? StudyYearID, int? BranchID, int? SectionID, int SemesterNumber, string DayOfWeek, int TimeSlotID)> sectionWholeBusy = [];
            private readonly HashSet<(int? StudyYearID, int? BranchID, int? SectionID, int SemesterNumber, string DayOfWeek, int TimeSlotID, string GroupName)> sectionGroupBusy = [];
            private readonly HashSet<(int SubjectID, int? SectionID, int SemesterNumber, string DayOfWeek)> subjectDayAny = [];
            private readonly HashSet<(int SubjectID, int? SectionID, int SemesterNumber, string DayOfWeek)> subjectDayWhole = [];
            private readonly HashSet<(int SubjectID, int? SectionID, int SemesterNumber, string DayOfWeek, string GroupName)> subjectDayGroup = [];

            private readonly Dictionary<int, int> facultyTotalLoads = [];
            private readonly Dictionary<(int FacultyMemberID, int SemesterNumber, string DayOfWeek), int> facultyDayLoads = [];
            private readonly Dictionary<(int? StudyYearID, int? BranchID, int? SectionID, int SemesterNumber, string DayOfWeek), int> sectionDayLoads = [];
            private readonly Dictionary<(int SemesterNumber, string DayOfWeek, int TimeSlotID), int> slotLoads = [];

            public List<Schedule> Schedules { get; } = [];

            public IEnumerable<int> GetOrderedFacultyOptions(
                IReadOnlyList<int> facultyMemberIds,
                int semesterNumber,
                string dayOfWeek)
            {
                return facultyMemberIds
                    .OrderBy(facultyMemberId => Count(facultyDayLoads, (facultyMemberId, semesterNumber, dayOfWeek)))
                    .ThenBy(facultyMemberId => Count(facultyTotalLoads, facultyMemberId))
                    .ThenBy(facultyMemberId => facultyMemberId);
            }

            public bool CanPlace(Schedule schedule)
            {
                if (classroomBusy.Contains(ClassroomTimeKey(schedule)) ||
                    facultyBusy.Contains(FacultyTimeKey(schedule)))
                {
                    return false;
                }

                var sectionKey = SectionTimeKey(schedule);

                if (string.IsNullOrWhiteSpace(schedule.GroupName))
                {
                    return !sectionAnyBusy.Contains(sectionKey);
                }

                return !sectionWholeBusy.Contains(sectionKey) &&
                    !sectionGroupBusy.Contains(SectionGroupTimeKey(schedule));
            }

            public bool HasSameSubjectSectionOnDay(Schedule schedule)
            {
                var subjectKey = SubjectDayKey(schedule);

                if (string.IsNullOrWhiteSpace(schedule.GroupName))
                {
                    return subjectDayAny.Contains(subjectKey);
                }

                return subjectDayWhole.Contains(subjectKey) ||
                    subjectDayGroup.Contains(SubjectGroupDayKey(schedule));
            }

            public int ScorePlacement(Schedule schedule, SchedulePlacementCandidate candidate)
            {
                int facultyDayLoad = Count(
                    facultyDayLoads,
                    (schedule.FacultyMemberID, schedule.SemesterNumber, schedule.DayOfWeek));
                int facultyTotalLoad = Count(facultyTotalLoads, schedule.FacultyMemberID);
                int sectionDayLoad = Count(sectionDayLoads, SectionDayKey(schedule));
                int slotLoad = Count(slotLoads, (schedule.SemesterNumber, schedule.DayOfWeek, schedule.TimeSlotID));
                int roomWaste = Math.Max(0, candidate.Classroom.Capacity - candidate.RequiredCapacity);
                int sameSubjectDayPenalty = HasSameSubjectSectionOnDay(schedule) ? 180 : 0;
                int practicalPairingScore = GetPracticalPairingScore(schedule);

                return
                    facultyDayLoad * 45 +
                    sectionDayLoad * 42 +
                    facultyTotalLoad * 6 +
                    slotLoad * 3 +
                    roomWaste +
                    sameSubjectDayPenalty +
                    practicalPairingScore +
                    DayOrder(schedule.DayOfWeek);
            }

            public void Add(Schedule schedule)
            {
                Schedules.Add(schedule);

                classroomBusy.Add(ClassroomTimeKey(schedule));
                facultyBusy.Add(FacultyTimeKey(schedule));

                var sectionTimeKey = SectionTimeKey(schedule);
                sectionAnyBusy.Add(sectionTimeKey);

                if (string.IsNullOrWhiteSpace(schedule.GroupName))
                {
                    sectionWholeBusy.Add(sectionTimeKey);
                    subjectDayWhole.Add(SubjectDayKey(schedule));
                }
                else
                {
                    sectionGroupBusy.Add(SectionGroupTimeKey(schedule));
                    subjectDayGroup.Add(SubjectGroupDayKey(schedule));
                }

                subjectDayAny.Add(SubjectDayKey(schedule));

                Increment(facultyTotalLoads, schedule.FacultyMemberID);
                Increment(facultyDayLoads, (schedule.FacultyMemberID, schedule.SemesterNumber, schedule.DayOfWeek));
                Increment(sectionDayLoads, SectionDayKey(schedule));
                Increment(slotLoads, (schedule.SemesterNumber, schedule.DayOfWeek, schedule.TimeSlotID));
            }

            private static (int ClassroomID, int SemesterNumber, string DayOfWeek, int TimeSlotID) ClassroomTimeKey(Schedule schedule)
            {
                return (schedule.ClassroomID, schedule.SemesterNumber, schedule.DayOfWeek, schedule.TimeSlotID);
            }

            private static (int FacultyMemberID, int SemesterNumber, string DayOfWeek, int TimeSlotID) FacultyTimeKey(Schedule schedule)
            {
                return (schedule.FacultyMemberID, schedule.SemesterNumber, schedule.DayOfWeek, schedule.TimeSlotID);
            }

            private static (int? StudyYearID, int? BranchID, int? SectionID, int SemesterNumber, string DayOfWeek, int TimeSlotID) SectionTimeKey(Schedule schedule)
            {
                return (schedule.StudyYearID, schedule.BranchID, schedule.SectionID, schedule.SemesterNumber, schedule.DayOfWeek, schedule.TimeSlotID);
            }

            private static (int? StudyYearID, int? BranchID, int? SectionID, int SemesterNumber, string DayOfWeek, int TimeSlotID, string GroupName) SectionGroupTimeKey(Schedule schedule)
            {
                return (schedule.StudyYearID, schedule.BranchID, schedule.SectionID, schedule.SemesterNumber, schedule.DayOfWeek, schedule.TimeSlotID, schedule.GroupName ?? string.Empty);
            }

            private static (int? StudyYearID, int? BranchID, int? SectionID, int SemesterNumber, string DayOfWeek) SectionDayKey(Schedule schedule)
            {
                return (schedule.StudyYearID, schedule.BranchID, schedule.SectionID, schedule.SemesterNumber, schedule.DayOfWeek);
            }

            private static (int SubjectID, int? SectionID, int SemesterNumber, string DayOfWeek) SubjectDayKey(Schedule schedule)
            {
                return (schedule.SubjectID, schedule.SectionID, schedule.SemesterNumber, schedule.DayOfWeek);
            }

            private static (int SubjectID, int? SectionID, int SemesterNumber, string DayOfWeek, string GroupName) SubjectGroupDayKey(Schedule schedule)
            {
                return (schedule.SubjectID, schedule.SectionID, schedule.SemesterNumber, schedule.DayOfWeek, schedule.GroupName ?? string.Empty);
            }

            private int GetPracticalPairingScore(Schedule schedule)
            {
                if (string.IsNullOrWhiteSpace(schedule.GroupName) ||
                    !string.Equals(schedule.LectureType, "Practical", StringComparison.OrdinalIgnoreCase))
                {
                    return 0;
                }

                var pairedPractical = Schedules
                    .Where(existing =>
                        existing.SemesterNumber == schedule.SemesterNumber &&
                        existing.StudyYearID == schedule.StudyYearID &&
                        existing.BranchID == schedule.BranchID &&
                        existing.SectionID == schedule.SectionID &&
                        existing.DayOfWeek == schedule.DayOfWeek &&
                        existing.TimeSlotID == schedule.TimeSlotID &&
                        string.Equals(existing.LectureType, "Practical", StringComparison.OrdinalIgnoreCase) &&
                        !string.IsNullOrWhiteSpace(existing.GroupName) &&
                        !string.Equals(existing.GroupName, schedule.GroupName, StringComparison.OrdinalIgnoreCase));

                if (!pairedPractical.Any())
                {
                    return 18;
                }

                return pairedPractical.Any(existing => existing.SubjectID != schedule.SubjectID)
                    ? -220
                    : 140;
            }

            private static int Count<TKey>(Dictionary<TKey, int> values, TKey key)
                where TKey : notnull
            {
                return values.TryGetValue(key, out int count) ? count : 0;
            }

            private static void Increment<TKey>(Dictionary<TKey, int> values, TKey key)
                where TKey : notnull
            {
                values[key] = Count(values, key) + 1;
            }
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
        int SectionCount,
        int AddedTimeSlotCount,
        int AddedClassroomCount,
        int AddedFacultyMemberCount,
        int AddedFacultySubjectAssignmentCount);

    internal sealed record SubjectTeachingAssignment(
        Subject Subject,
        IReadOnlyList<int> FacultyMemberIds);

    internal sealed record ScheduleGenerationRequest(
        Subject Subject,
        IReadOnlyList<int> FacultyMemberIds,
        Section Section,
        int LessonNumber,
        int RequiredLessons,
        string LectureType,
        string? GroupName);

    internal sealed record SchedulePlanningResult(
        List<Schedule> Schedules,
        int NoClassroomCount,
        int ConflictCount);

    internal sealed record ScheduleRequestPlan(
        ScheduleGenerationRequest Request,
        List<SchedulePlacementCandidate> Candidates,
        int DifficultyScore);

    internal sealed record SchedulePlacementCandidate(
        string DayOfWeek,
        TimeSlot TimeSlot,
        Classroom Classroom,
        int RequiredCapacity);
}
