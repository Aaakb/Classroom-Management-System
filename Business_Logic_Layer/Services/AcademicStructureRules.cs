namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    public static class AcademicStructureRules
    {
        public static bool UsesGeneralSections(int studyYearId)
        {
            return studyYearId is 1 or 2;
        }

        public static bool UsesBranches(int studyYearId)
        {
            return studyYearId >= 3;
        }

        public static IReadOnlyList<string> GetAllowedSectionNames(int studyYearId)
        {
            return studyYearId switch
            {
                1 => ["A1", "A2", "B1", "B2"],
                2 => ["A", "B"],
                _ => []
            };
        }

        public static string FormatAllowedSectionNames(int studyYearId)
        {
            var names = GetAllowedSectionNames(studyYearId);
            return names.Count == 0 ? string.Empty : string.Join(", ", names);
        }

        public static bool SectionMatchesSubject(
            int studyYearId,
            int? subjectBranchId,
            int? sectionBranchId,
            string sectionName)
        {
            if (UsesGeneralSections(studyYearId))
            {
                var allowedNames = GetAllowedSectionNames(studyYearId);

                return !subjectBranchId.HasValue &&
                    !sectionBranchId.HasValue &&
                    allowedNames.Contains(sectionName.Trim(), StringComparer.OrdinalIgnoreCase);
            }

            if (UsesBranches(studyYearId))
            {
                return subjectBranchId.HasValue && sectionBranchId == subjectBranchId;
            }

            return sectionBranchId == subjectBranchId;
        }
    }
}
