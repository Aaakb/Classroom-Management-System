using University_Timetable_and_Classroom_Management_System.BusinessLayer;

namespace University_Timetable_and_Classroom_Management_System
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
            bool updateDatabaseOnly = args.Contains("--update-db", StringComparer.OrdinalIgnoreCase);

            var databaseUpdated = ApplyDatabaseFixes(updateDatabaseOnly);

            if (updateDatabaseOnly && !databaseUpdated)
            {
                Environment.ExitCode = 1;
                return;
            }

            if (updateDatabaseOnly)
            {
                return;
            }

            Application.Run(new MainShellForm());
        }

        private static bool ApplyDatabaseFixes(bool silent)
        {
            try
            {
                new DatabaseMaintenanceService()
                    .ApplyPendingFixesAsync()
                    .GetAwaiter()
                    .GetResult();

                return true;
            }
            catch (Exception ex)
            {
                if (silent)
                {
                    Console.Error.WriteLine(ex);
                }
                else
                {
                    MessageBox.Show(
                        $"Database update could not be completed.\n\n{ex.Message}",
                        "Database Update",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                return false;
            }
        }
    }
}
