using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    internal static class ScheduleTimingRules
    {
        public static readonly TimeSpan WorkdayStart = new(9, 0, 0);
        public static readonly TimeSpan WorkdayEnd = new(16, 30, 0);
        public static readonly TimeSpan LectureDuration = TimeSpan.FromMinutes(90);
        public static readonly TimeSpan LectureGap = TimeSpan.Zero;
        public static readonly TimeSpan BreakStart = new(12, 30, 0);
        public static readonly TimeSpan BreakEnd = new(13, 30, 0);

        public static IReadOnlyList<(TimeSpan Start, TimeSpan End)> OfficialLectureSlots { get; } =
        [
            (new TimeSpan(9, 0, 0), new TimeSpan(10, 30, 0)),
            (new TimeSpan(10, 30, 0), new TimeSpan(12, 0, 0)),
            (new TimeSpan(13, 30, 0), new TimeSpan(15, 0, 0)),
            (new TimeSpan(15, 0, 0), new TimeSpan(16, 30, 0))
        ];

        public static bool IsOfficialLectureSlot(TimeSlot timeSlot)
        {
            return !timeSlot.IsBreak && OfficialLectureSlots.Any(slot =>
                slot.Start == timeSlot.StartTime &&
                slot.End == timeSlot.EndTime);
        }

        public static bool IsValidLectureRange(TimeSpan startTime, TimeSpan endTime)
        {
            return endTime - startTime == LectureDuration &&
                OfficialLectureSlots.Any(slot => slot.Start == startTime && slot.End == endTime);
        }
    }
}
