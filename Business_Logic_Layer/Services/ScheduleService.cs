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

            var sections = await context.Sections
                .AsNoTracking()
                .OrderBy(section => section.StudyYearID)
                .ThenBy(section => section.BranchID)
                .ThenBy(section => section.SectionName)
                .ToListAsync();

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
                    int lessonCount = CalculateLessonCount(subject);
                    var matchingSections = sections
                        .Where(section =>
                            section.StudyYearID == subject.StudyYearID &&
                            (!subject.BranchID.HasValue || section.BranchID == subject.BranchID))
                        .ToList();

                    if (matchingSections.Count == 0)
                    {
                        skippedCount++;
                        missingSectionCount++;
                    }

                    return matchingSections.SelectMany(section =>
                        Enumerable.Range(1, lessonCount)
                            .Select(lessonNumber => new ScheduleGenerationRequest(assignment, section, lessonNumber, lessonCount)));
                })
                .OrderByDescending(request => request.Assignment.Subject.StudyYearID)
                .ThenBy(request => request.Assignment.Subject.BranchID ?? request.Section.BranchID ?? 0)
                .ThenBy(request => request.Section.SectionName)
                .ThenBy(request => request.Assignment.Subject.SubjectName)
                .ThenBy(request => request.LessonNumber)
                .ToList();

            foreach (var request in scheduleRequests)
            {
                var assignment = request.Assignment;
                var subject = assignment.Subject;
                var section = request.Section;

                var targetClassrooms = classrooms
                    .Where(classroom => classroom.Capacity >= section.StudentCount)
                    .DefaultIfEmpty(classrooms.FirstOrDefault())
                    .Where(classroom => classroom is not null)
                    .Cast<Classroom>()
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

            if (!await context.Subjects.AnyAsync(s => s.SubjectID == schedule.SubjectID))
            {
                throw new ArgumentException("Subject does not exist.");
            }

            if (!await context.FacultyMembers.AnyAsync(f => f.FacultyMemberID == schedule.FacultyMemberID))
            {
                throw new ArgumentException("Faculty member does not exist.");
            }

            if (!await context.Classrooms.AnyAsync(c => c.ClassroomID == schedule.ClassroomID))
            {
                throw new ArgumentException("Classroom does not exist.");
            }

            if (!await context.TimeSlots.AnyAsync(t => t.TimeSlotID == schedule.TimeSlotID))
            {
                throw new ArgumentException("Time slot does not exist.");
            }

            if (schedule.StudyYearID.HasValue &&
                !await context.StudyYears.AnyAsync(sy => sy.StudyYearID == schedule.StudyYearID.Value))
            {
                throw new ArgumentException("Study year does not exist.");
            }

            if (schedule.BranchID.HasValue &&
                !await context.Branches.AnyAsync(b => b.BranchID == schedule.BranchID.Value))
            {
                throw new ArgumentException("Branch does not exist.");
            }

            if (schedule.SectionID.HasValue &&
                !await context.Sections.AnyAsync(s => s.SectionID == schedule.SectionID.Value))
            {
                throw new ArgumentException("Section does not exist.");
            }

            await EnsureNoConflictsAsync(context, schedule, isUpdate);
        }

        private static async Task EnsureNoConflictsAsync(AppDbContext context, Schedule schedule, bool isUpdate)
        {
            var classroomConflict = await context.Schedules.AnyAsync(s =>
                s.ClassroomID == schedule.ClassroomID &&
                s.TimeSlotID == schedule.TimeSlotID &&
                s.DayOfWeek == schedule.DayOfWeek &&
                (!isUpdate || s.ScheduleID != schedule.ScheduleID));

            if (classroomConflict)
            {
                throw new ArgumentException("This classroom is already booked for the selected day and time slot.");
            }

            var facultyConflict = await context.Schedules.AnyAsync(s =>
                s.FacultyMemberID == schedule.FacultyMemberID &&
                s.TimeSlotID == schedule.TimeSlotID &&
                s.DayOfWeek == schedule.DayOfWeek &&
                (!isUpdate || s.ScheduleID != schedule.ScheduleID));

            if (facultyConflict)
            {
                throw new ArgumentException("This faculty member is already booked for the selected day and time slot.");
            }

            var cohortConflict = await context.Schedules.AnyAsync(s =>
                s.StudyYearID == schedule.StudyYearID &&
                s.BranchID == schedule.BranchID &&
                s.SectionID == schedule.SectionID &&
                s.TimeSlotID == schedule.TimeSlotID &&
                s.DayOfWeek == schedule.DayOfWeek &&
                (!isUpdate || s.ScheduleID != schedule.ScheduleID));

            if (cohortConflict)
            {
                throw new ArgumentException("This study year, branch, or section already has a schedule in the selected slot.");
            }
        }

        private static bool HasScheduleConflict(IEnumerable<Schedule> schedules, Schedule candidate)
        {
            return schedules.Any(schedule =>
                schedule.TimeSlotID == candidate.TimeSlotID &&
                schedule.DayOfWeek == candidate.DayOfWeek &&
                (schedule.ClassroomID == candidate.ClassroomID ||
                 schedule.FacultyMemberID == candidate.FacultyMemberID ||
                 (schedule.StudyYearID == candidate.StudyYearID &&
                  schedule.BranchID == candidate.BranchID &&
                  schedule.SectionID == candidate.SectionID)));
        }

        private static bool HasSameSubjectSectionOnDay(IEnumerable<Schedule> schedules, Schedule candidate)
        {
            return schedules.Any(schedule =>
                schedule.SubjectID == candidate.SubjectID &&
                schedule.SectionID == candidate.SectionID &&
                schedule.DayOfWeek == candidate.DayOfWeek);
        }

        private static int CalculateLessonCount(Subject subject)
        {
            var hours = subject.TheoreticalHours + subject.PracticalHours;

            if (hours <= 0)
            {
                hours = subject.CreditUnits;
            }

            return Math.Max(1, (int)Math.Ceiling(hours));
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
        int RequiredLessons);
}
