namespace University_Timetable_and_Classroom_Management_System.Models
{
    public class ScheduleDetailsView
    {
        public int ScheduleID { get; set; }

        public int SemesterNumber { get; set; }

        public string YearName { get; set; } = string.Empty;

        public string? BranchName { get; set; }

        public string SectionName { get; set; } = string.Empty;

        public string SubjectName { get; set; } = string.Empty;

        public string FacultyMemberName { get; set; } = string.Empty;

        public string ClassroomNumber { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string DayOfWeek { get; set; } = string.Empty;
    }
}
