namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SchedulesForm
    {
        private static HashSet<int> GetConflictingScheduleIds(IReadOnlyCollection<ScheduleRow> rows)
        {
            var conflicts = new HashSet<int>();
            var rowList = rows.ToList();

            for (int i = 0; i < rowList.Count; i++)
            {
                for (int j = i + 1; j < rowList.Count; j++)
                {
                    if (!RowsOverlap(rowList[i], rowList[j]))
                    {
                        continue;
                    }

                    conflicts.Add(rowList[i].ScheduleID);
                    conflicts.Add(rowList[j].ScheduleID);
                }
            }

            return conflicts;
        }

        private static bool RowsOverlap(ScheduleRow first, ScheduleRow second)
        {
            if (first.SemesterNumber != second.SemesterNumber ||
                first.TimeSlotID != second.TimeSlotID ||
                !string.Equals(first.DayOfWeek, second.DayOfWeek, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return first.ClassroomID == second.ClassroomID ||
                first.FacultyMemberID == second.FacultyMemberID ||
                HasSectionOrGroupOverlap(first, second);
        }

        private static bool HasSectionOrGroupOverlap(ScheduleRow first, ScheduleRow second)
        {
            if (!first.SectionID.HasValue ||
                !second.SectionID.HasValue ||
                first.SectionID.Value != second.SectionID.Value)
            {
                return false;
            }

            if (IsWholeSection(first) || IsWholeSection(second))
            {
                return true;
            }

            return string.Equals(first.GroupName, second.GroupName, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsWholeSection(ScheduleRow row)
        {
            return string.Equals(row.LectureType, "Theory", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(row.GroupName, "All", StringComparison.OrdinalIgnoreCase) ||
                string.IsNullOrWhiteSpace(row.GroupName);
        }
    }
}
