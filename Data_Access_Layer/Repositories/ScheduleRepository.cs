using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;
using University_Timetable_and_Classroom_Management_System.Models;

namespace Data_Access_Layer.Repositories
{
    public class ScheduleRepository
    {
        private readonly AppDbContext _context;

        public ScheduleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Schedule>> GetAllAsync()
        {
            return await _context.Schedules
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
            return await _context.Schedules
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

        public async Task<int> AddAsync(Schedule entity)
        {
            await _context.Schedules.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Schedule entity)
        {
            _context.Schedules.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Schedules.FindAsync(id);

            if (entity is null)
            {
                return false;
            }

            _context.Schedules.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Schedules.AnyAsync(s => s.ScheduleID == id);
        }

        public async Task<bool> HasClassroomConflictAsync(
            int classroomId,
            string dayOfWeek,
            int timeSlotId,
            int? excludeScheduleId = null)
        {
            var query = _context.Schedules.AsNoTracking().Where(s =>
                s.ClassroomID == classroomId &&
                s.DayOfWeek == dayOfWeek &&
                s.TimeSlotID == timeSlotId);

            if (excludeScheduleId is not null)
            {
                query = query.Where(s => s.ScheduleID != excludeScheduleId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<bool> HasFacultyMemberConflictAsync(
            int facultyMemberId,
            string dayOfWeek,
            int timeSlotId,
            int? excludeScheduleId = null)
        {
            var query = _context.Schedules.AsNoTracking().Where(s =>
                s.FacultyMemberID == facultyMemberId &&
                s.DayOfWeek == dayOfWeek &&
                s.TimeSlotID == timeSlotId);

            if (excludeScheduleId is not null)
            {
                query = query.Where(s => s.ScheduleID != excludeScheduleId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<bool> HasSectionConflictAsync(
            int sectionId,
            string dayOfWeek,
            int timeSlotId,
            int? excludeScheduleId = null)
        {
            var query = _context.Schedules.AsNoTracking().Where(s =>
                s.SectionID == sectionId &&
                s.DayOfWeek == dayOfWeek &&
                s.TimeSlotID == timeSlotId);

            if (excludeScheduleId is not null)
            {
                query = query.Where(s => s.ScheduleID != excludeScheduleId.Value);
            }

            return await query.AnyAsync();
        }
    }
}
