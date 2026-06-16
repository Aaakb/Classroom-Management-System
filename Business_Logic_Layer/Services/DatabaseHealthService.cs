using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;

namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    public sealed class DatabaseHealthService
    {
        public async Task<DatabaseHealthResult> CheckConnectionAsync()
        {
            try
            {
                await using var context = new AppDbContext();
                await context.Database.OpenConnectionAsync();

                string dataSource = context.Database.GetDbConnection().DataSource;
                string database = context.Database.GetDbConnection().Database;

                return new DatabaseHealthResult(
                    true,
                    $"Connected to {dataSource} / {database}");
            }
            catch (Exception ex)
            {
                return new DatabaseHealthResult(false, ex.Message);
            }
        }

        public async Task<IReadOnlyDictionary<string, int>> GetEntityCountsAsync()
        {
            await using var context = new AppDbContext();

            return new Dictionary<string, int>
            {
                ["Subjects"] = await context.Subjects.CountAsync(),
                ["Faculty"] = await context.FacultyMembers.CountAsync(),
                ["Classrooms"] = await context.Classrooms.CountAsync(),
                ["Schedules"] = await context.Schedules.CountAsync()
            };
        }
    }

    public sealed record DatabaseHealthResult(bool CanConnect, string Message);
}
