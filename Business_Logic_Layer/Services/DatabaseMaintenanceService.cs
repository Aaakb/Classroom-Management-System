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
                    IF COL_LENGTH(N'dbo.Schedules', N'SemesterNumber') IS NULL
                    BEGIN
                        ALTER TABLE [Schedules] ADD [SemesterNumber] int NULL;

                        UPDATE schedule
                        SET [SemesterNumber] = subject.[SemesterNumber]
                        FROM [Schedules] schedule
                        INNER JOIN [Subjects] subject ON subject.[SubjectID] = schedule.[SubjectID];

                        UPDATE [Schedules]
                        SET [SemesterNumber] = 1
                        WHERE [SemesterNumber] IS NULL;

                        ALTER TABLE [Schedules] ALTER COLUMN [SemesterNumber] int NOT NULL;
                    END

                    IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Schedules_ClassroomID_TimeSlotID' AND object_id = OBJECT_ID(N'[Schedules]'))
                        DROP INDEX [IX_Schedules_ClassroomID_TimeSlotID] ON [Schedules];

                    IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Schedules_FacultyMemberID_TimeSlotID' AND object_id = OBJECT_ID(N'[Schedules]'))
                        DROP INDEX [IX_Schedules_FacultyMemberID_TimeSlotID] ON [Schedules];

                    IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Schedules_StudyYearID_BranchID_TimeSlotID' AND object_id = OBJECT_ID(N'[Schedules]'))
                        DROP INDEX [IX_Schedules_StudyYearID_BranchID_TimeSlotID] ON [Schedules];

                    IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Schedules_ClassroomID_TimeSlotID_DayOfWeek' AND object_id = OBJECT_ID(N'[Schedules]'))
                        DROP INDEX [IX_Schedules_ClassroomID_TimeSlotID_DayOfWeek] ON [Schedules];

                    IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Schedules_FacultyMemberID_TimeSlotID_DayOfWeek' AND object_id = OBJECT_ID(N'[Schedules]'))
                        DROP INDEX [IX_Schedules_FacultyMemberID_TimeSlotID_DayOfWeek] ON [Schedules];

                    IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Schedules_StudyYearID_BranchID_SectionID_TimeSlotID_DayOfWeek' AND object_id = OBJECT_ID(N'[Schedules]'))
                        DROP INDEX [IX_Schedules_StudyYearID_BranchID_SectionID_TimeSlotID_DayOfWeek] ON [Schedules];

                    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UQ_Classroom_Semester_Time' AND object_id = OBJECT_ID(N'[Schedules]'))
                        AND NOT EXISTS (
                            SELECT 1
                            FROM [Schedules]
                            GROUP BY [ClassroomID], [SemesterNumber], [DayOfWeek], [TimeSlotID]
                            HAVING COUNT(*) > 1)
                        CREATE UNIQUE INDEX [UQ_Classroom_Semester_Time]
                        ON [Schedules] ([ClassroomID], [SemesterNumber], [DayOfWeek], [TimeSlotID]);

                    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UQ_Faculty_Semester_Time' AND object_id = OBJECT_ID(N'[Schedules]'))
                        AND NOT EXISTS (
                            SELECT 1
                            FROM [Schedules]
                            GROUP BY [FacultyMemberID], [SemesterNumber], [DayOfWeek], [TimeSlotID]
                            HAVING COUNT(*) > 1)
                        CREATE UNIQUE INDEX [UQ_Faculty_Semester_Time]
                        ON [Schedules] ([FacultyMemberID], [SemesterNumber], [DayOfWeek], [TimeSlotID]);

                    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UQ_Section_Semester_Time' AND object_id = OBJECT_ID(N'[Schedules]'))
                        AND NOT EXISTS (
                            SELECT 1
                            FROM [Schedules]
                            GROUP BY [SectionID], [SemesterNumber], [DayOfWeek], [TimeSlotID]
                            HAVING COUNT(*) > 1)
                        CREATE UNIQUE INDEX [UQ_Section_Semester_Time]
                        ON [Schedules] ([SectionID], [SemesterNumber], [DayOfWeek], [TimeSlotID]);

                    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Schedules_Year_Branch_Section_Semester_Time' AND object_id = OBJECT_ID(N'[Schedules]'))
                        AND NOT EXISTS (
                            SELECT 1
                            FROM [Schedules]
                            GROUP BY [StudyYearID], [BranchID], [SectionID], [SemesterNumber], [DayOfWeek], [TimeSlotID]
                            HAVING COUNT(*) > 1)
                        CREATE UNIQUE INDEX [IX_Schedules_Year_Branch_Section_Semester_Time]
                        ON [Schedules] ([StudyYearID], [BranchID], [SectionID], [SemesterNumber], [DayOfWeek], [TimeSlotID]);
                END
                """);
        }
    }
}
