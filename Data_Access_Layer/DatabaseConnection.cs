namespace Data_Access_Layer
{
    internal static class DatabaseConnection
    {
        private const string EnvironmentVariableName = "UNIVERSITY_TIMETABLE_CONNECTION_STRING";

        private const string DefaultConnectionString =
            @"Server=.\SQLEXPRESS;Database=UniversityTimetableDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static string ConnectionString =>
            Environment.GetEnvironmentVariable(EnvironmentVariableName) ?? DefaultConnectionString;
    }
}
