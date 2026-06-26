using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SchedulesForm
    {
        private static string FormatTimeSlot(TimeSlot timeSlot)
        {
            return TimeDisplay.FormatRange(timeSlot.StartTime, timeSlot.EndTime);
        }

        private static string FormatSection(Section section)
        {
            string year = section.StudyYear?.YearName ?? "Year";

            return section.Branch is null
                ? $"{section.SectionName} - {year}"
                : $"{section.SectionName} - {year} - {section.Branch.BranchName}";
        }

        private static int DayOrder(string day)
        {
            return day switch
            {
                "Sunday" => 1,
                "Monday" => 2,
                "Tuesday" => 3,
                "Wednesday" => 4,
                "Thursday" => 5,
                _ => 99
            };
        }

        private static int StudyYearOrder(string studyYearName)
        {
            return studyYearName switch
            {
                "First Year" => 1,
                "Second Year" => 2,
                "Third Year" => 3,
                "Fourth Year" => 4,
                _ => 99
            };
        }
    }
}
