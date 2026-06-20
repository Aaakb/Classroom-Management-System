namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    public enum StudyYearLevel
    {
        Unknown = 0,
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4
    }

    public static class StudyYearRules
    {
        public static StudyYearLevel ResolveLevel(string? yearName, int fallbackId = 0)
        {
            string normalized = Normalize(yearName);

            if (Matches(normalized, "1", "1st", "1styear", "first", "firstyear", "firststage", "year1", "stage1", "level1", "الاولى", "الاول", "الاولي", "اولي", "مرحلةاولى", "مرحلةالاولى"))
            {
                return StudyYearLevel.First;
            }

            if (Matches(normalized, "2", "2nd", "2ndyear", "second", "secondyear", "secondstage", "year2", "stage2", "level2", "الثانية", "الثاني", "الثانيه", "ثانيه", "مرحلةثانية", "مرحلةالثانية"))
            {
                return StudyYearLevel.Second;
            }

            if (Matches(normalized, "3", "3rd", "3rdyear", "third", "thirdyear", "thirdstage", "year3", "stage3", "level3", "الثالثة", "الثالث", "الثالثه", "ثالثه", "مرحلةثالثة", "مرحلةالثالثة"))
            {
                return StudyYearLevel.Third;
            }

            if (Matches(normalized, "4", "4th", "4thyear", "fourth", "fourthyear", "fourthstage", "year4", "stage4", "level4", "الرابعة", "الرابع", "الرابعه", "رابعه", "مرحلةرابعة", "مرحلةالرابعة"))
            {
                return StudyYearLevel.Fourth;
            }

            return fallbackId is >= 1 and <= 4
                ? (StudyYearLevel)fallbackId
                : StudyYearLevel.Unknown;
        }

        public static string GetDisplayName(StudyYearLevel level)
        {
            return level switch
            {
                StudyYearLevel.First => "First Year",
                StudyYearLevel.Second => "Second Year",
                StudyYearLevel.Third => "Third Year",
                StudyYearLevel.Fourth => "Fourth Year",
                _ => string.Empty
            };
        }

        public static int GetSortOrder(StudyYearLevel level)
        {
            return level == StudyYearLevel.Unknown ? 99 : (int)level;
        }

        public static IReadOnlyList<string> GetAllowedSectionNames(StudyYearLevel level)
        {
            return level switch
            {
                StudyYearLevel.First => ["A1", "A2", "B1", "B2"],
                StudyYearLevel.Second => ["A", "B"],
                _ => []
            };
        }

        public static bool UsesGeneralSections(StudyYearLevel level)
        {
            return level is StudyYearLevel.First or StudyYearLevel.Second;
        }

        public static bool UsesBranches(StudyYearLevel level)
        {
            return level is StudyYearLevel.Third or StudyYearLevel.Fourth;
        }

        public static bool SectionMatchesSubject(
            StudyYearLevel level,
            int? subjectBranchId,
            int? sectionBranchId,
            string sectionName)
        {
            if (UsesGeneralSections(level))
            {
                return !subjectBranchId.HasValue &&
                    !sectionBranchId.HasValue &&
                    GetAllowedSectionNames(level).Contains(sectionName.Trim(), StringComparer.OrdinalIgnoreCase);
            }

            if (UsesBranches(level))
            {
                return subjectBranchId.HasValue && sectionBranchId == subjectBranchId;
            }

            return false;
        }

        public static string FormatAllowedSectionNames(StudyYearLevel level)
        {
            var names = GetAllowedSectionNames(level);
            return names.Count == 0 ? string.Empty : string.Join(", ", names);
        }

        private static bool Matches(string normalized, params string[] candidates)
        {
            return candidates.Any(candidate => normalized == Normalize(candidate));
        }

        private static string Normalize(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            string arabicNormalized = value
                .Trim()
                .ToLowerInvariant()
                .Replace('أ', 'ا')
                .Replace('إ', 'ا')
                .Replace('آ', 'ا')
                .Replace('ى', 'ي')
                .Replace('ة', 'ه');

            return new string(arabicNormalized
                .Where(char.IsLetterOrDigit)
                .ToArray());
        }
    }
}
