namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    internal static class ScheduleDayRules
    {
        public static IReadOnlyList<string> AllDays { get; } =
        [
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday"
        ];

        public static IReadOnlyList<string> GetSchedulingDays(int studyYearId)
        {
            return studyYearId switch
            {
                1 => ["Sunday", "Monday", "Tuesday", "Wednesday"],
                2 => ["Sunday", "Monday", "Tuesday", "Thursday"],
                3 => ["Sunday", "Monday", "Wednesday", "Thursday"],
                4 => ["Sunday", "Tuesday", "Wednesday", "Thursday"],
                _ => AllDays.Take(4).ToList()
            };
        }

        public static IReadOnlyList<string> GetSchedulingDays(string studyYearName)
        {
            return studyYearName.Trim().ToLowerInvariant() switch
            {
                "first year" => GetSchedulingDays(1),
                "second year" => GetSchedulingDays(2),
                "third year" => GetSchedulingDays(3),
                "fourth year" => GetSchedulingDays(4),
                _ => AllDays.Take(4).ToList()
            };
        }
    }
}
