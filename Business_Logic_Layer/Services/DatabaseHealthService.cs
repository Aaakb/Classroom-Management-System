using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;

namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    public class DatabaseHealthService
    {
        public async Task<DatabaseHealthResult> CheckConnectionAsync()
        {
            try
            {
                await using var context = new AppDbContext();
                await context.Database.EnsureCreatedAsync();
                await context.Database.OpenConnectionAsync();
                string dataSource = context.Database.GetDbConnection().DataSource;
                await context.Database.CloseConnectionAsync();

                return new DatabaseHealthResult(
                    true,
                    $"Connected to UniversityTimetableDB on {dataSource}. Database schema is ready.");
            }
            catch (Exception ex)
            {
                return new DatabaseHealthResult(false, GetDetailedMessage(ex));
            }
        }

        public async Task<Dictionary<string, int>> GetEntityCountsAsync()
        {
            await using var context = new AppDbContext();

            return new Dictionary<string, int>
            {
                ["Branches"] = await context.Branches.CountAsync(),
                ["Study Years"] = await context.StudyYears.CountAsync(),
                ["Sections"] = await context.Sections.CountAsync(),
                ["Subjects"] = await context.Subjects.CountAsync(),
                ["Faculty Members"] = await context.FacultyMembers.CountAsync(),
                ["Classrooms"] = await context.Classrooms.CountAsync(),
                ["Time Slots"] = await context.TimeSlots.CountAsync(),
                ["Schedules"] = await context.Schedules.CountAsync()
            };
        }

        private static string GetDetailedMessage(Exception exception)
        {
            var current = exception;

            while (current.InnerException is not null)
            {
                current = current.InnerException;
            }

            return current.Message;
        }
    }

    public sealed record DatabaseHealthResult(bool CanConnect, string Message);
}
