using Microsoft.EntityFrameworkCore;
using Data_Access_Layer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    public class TimeSlotService
    {
        private static readonly TimeSpan LectureGap = TimeSpan.FromMinutes(10);

        public async Task<List<TimeSlot>> GetAllAsync()
        {
            await using var context = new AppDbContext();
            return await context.TimeSlots
                .AsNoTracking()
                .Where(t => !t.IsBreak)
                .OrderBy(t => t.StartTime)
                .ToListAsync();
        }

        public async Task<TimeSlot?> GetByIdAsync(int id)
        {
            await using var context = new AppDbContext();
            return await context.TimeSlots
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TimeSlotID == id && !t.IsBreak);
        }

        public async Task<TimeSlot> AddAsync(TimeSlot timeSlot)
        {
            await using var context = new AppDbContext();
            timeSlot.IsBreak = false;
            timeSlot.TimeSlotID = await AutoKeyGenerator.NextAsync(context.TimeSlots.Select(t => t.TimeSlotID));
            await ValidateAsync(context, timeSlot, false);
            await context.TimeSlots.AddAsync(timeSlot);
            await ManualKeySaveHelper.SaveWithManualKeyAsync(context, "[TimeSlots]");
            return timeSlot;
        }

        public async Task<TimeSlot> UpdateAsync(TimeSlot timeSlot)
        {
            await using var context = new AppDbContext();
            timeSlot.IsBreak = false;
            await ValidateAsync(context, timeSlot, true);
            context.TimeSlots.Update(timeSlot);
            await context.SaveChangesAsync();
            return timeSlot;
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = new AppDbContext();
            var timeSlot = await context.TimeSlots.FindAsync(id)
                ?? throw new KeyNotFoundException("Time slot not found.");

            context.TimeSlots.Remove(timeSlot);
            await context.SaveChangesAsync();
        }

        private static async Task ValidateAsync(AppDbContext context, TimeSlot timeSlot, bool isUpdate)
        {
            if (timeSlot.TimeSlotID <= 0)
            {
                throw new ArgumentException("Time slot ID is required.");
            }

            var idExists = await context.TimeSlots.AnyAsync(t => t.TimeSlotID == timeSlot.TimeSlotID);

            if (!isUpdate && idExists)
            {
                throw new ArgumentException("Time slot ID already exists.");
            }

            if (isUpdate && !idExists)
            {
                throw new KeyNotFoundException("Time slot not found.");
            }

            if (timeSlot.EndTime <= timeSlot.StartTime)
            {
                throw new ArgumentException("End time must be after start time.");
            }

            var exists = await context.TimeSlots.AnyAsync(t =>
                t.StartTime == timeSlot.StartTime &&
                t.EndTime == timeSlot.EndTime &&
                t.IsBreak == timeSlot.IsBreak &&
                (!isUpdate || t.TimeSlotID != timeSlot.TimeSlotID));

            if (exists)
            {
                throw new ArgumentException("Time slot already exists.");
            }

            if (timeSlot.IsBreak)
            {
                return;
            }

            var lectureSlots = await context.TimeSlots
                .AsNoTracking()
                .Where(t => !t.IsBreak && (!isUpdate || t.TimeSlotID != timeSlot.TimeSlotID))
                .Select(t => new { t.StartTime, t.EndTime })
                .ToListAsync();

            bool tooCloseToAnotherLecture = lectureSlots.Any(t =>
                timeSlot.StartTime < t.EndTime + LectureGap &&
                timeSlot.EndTime + LectureGap > t.StartTime);

            if (tooCloseToAnotherLecture)
            {
                throw new ArgumentException("Keep at least 10 minutes between lecture time slots.");
            }
        }
    }
}
