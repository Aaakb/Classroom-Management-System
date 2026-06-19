using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;

namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    public class DatabaseMaintenanceService
    {
        public async Task ApplyPendingFixesAsync()
        {
            await using var context = new AppDbContext();
            await context.Database.EnsureCreatedAsync();

            if (!context.Database.IsSqlServer())
            {
                return;
            }

            await context.Database.ExecuteSqlRawAsync("""
                IF OBJECT_ID(N'[Schedules]', N'U') IS NOT NULL
                BEGIN
                    IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Schedules_ClassroomID_TimeSlotID' AND object_id = OBJECT_ID(N'[Schedules]'))
                        DROP INDEX [IX_Schedules_ClassroomID_TimeSlotID] ON [Schedules];

                    IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Schedules_FacultyMemberID_TimeSlotID' AND object_id = OBJECT_ID(N'[Schedules]'))
                        DROP INDEX [IX_Schedules_FacultyMemberID_TimeSlotID] ON [Schedules];

                    IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Schedules_StudyYearID_BranchID_TimeSlotID' AND object_id = OBJECT_ID(N'[Schedules]'))
                        DROP INDEX [IX_Schedules_StudyYearID_BranchID_TimeSlotID] ON [Schedules];

                    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Schedules_ClassroomID_TimeSlotID_DayOfWeek' AND object_id = OBJECT_ID(N'[Schedules]'))
                        CREATE UNIQUE INDEX [IX_Schedules_ClassroomID_TimeSlotID_DayOfWeek]
                        ON [Schedules] ([ClassroomID], [TimeSlotID], [DayOfWeek]);

                    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Schedules_FacultyMemberID_TimeSlotID_DayOfWeek' AND object_id = OBJECT_ID(N'[Schedules]'))
                        CREATE UNIQUE INDEX [IX_Schedules_FacultyMemberID_TimeSlotID_DayOfWeek]
                        ON [Schedules] ([FacultyMemberID], [TimeSlotID], [DayOfWeek]);

                    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Schedules_StudyYearID_BranchID_SectionID_TimeSlotID_DayOfWeek' AND object_id = OBJECT_ID(N'[Schedules]'))
                        CREATE UNIQUE INDEX [IX_Schedules_StudyYearID_BranchID_SectionID_TimeSlotID_DayOfWeek]
                        ON [Schedules] ([StudyYearID], [BranchID], [SectionID], [TimeSlotID], [DayOfWeek]);
                END
                """);
        }
    }
}
