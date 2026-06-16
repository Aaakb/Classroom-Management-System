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
    }
}
