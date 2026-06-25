using System.Globalization;
using System.Text;

namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    public sealed class SchedulePdfExportService
    {
        private const int PageWidth = 842;
        private const int PageHeight = 595;
        private const int TableLeft = 27;
        private const int TableTop = 502;
        private const int HeaderHeight = 34;
        private const int LectureRowHeight = 78;
        private const int BreakRowHeight = 34;
        private const int TimeColumnWidth = 92;
        private const int DayColumnWidth = 174;
        private const int TableWidth = TimeColumnWidth + DayColumnWidth * 4;
        private const string HeaderColor = "0.10 0.18 0.31";
        private const string HeaderTextColor = "1 1 1";
        private const string BorderColor = "0.78 0.84 0.90";
        private const string RowLineColor = "0.86 0.90 0.95";
        private const string BreakColor = "0.75 0.90 0.58";
        private const string AlternatingRowColor = "0.97 0.98 1";
        private const string TextColor = "0.05 0.09 0.18";
        private const string MutedTextColor = "0.30 0.38 0.50";

        public async Task ExportAsync(string filePath, IReadOnlyList<SchedulePdfRow> rows)
        {
            byte[] pdfBytes = BuildPdf(rows);
            await File.WriteAllBytesAsync(filePath, pdfBytes);
        }

        private static byte[] BuildPdf(IReadOnlyList<SchedulePdfRow> rows)
        {
            var objects = new List<string>();
            int catalogObjectId = AddObject(objects, string.Empty);
            int pagesObjectId = AddObject(objects, string.Empty);
            int fontObjectId = AddObject(objects, "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>");

            var pageRows = BuildPages(rows);
            var pageObjectIds = new List<int>();

            foreach (var page in pageRows)
            {
                int contentObjectId = objects.Count + 1;
                AddObject(objects, BuildPageContent(page));

                int pageObjectId = objects.Count + 1;
                pageObjectIds.Add(pageObjectId);
                AddObject(objects,
                    $"<< /Type /Page /Parent {pagesObjectId} 0 R /MediaBox [0 0 {PageWidth} {PageHeight}] /Resources << /Font << /F1 {fontObjectId} 0 R >> >> /Contents {contentObjectId} 0 R >>");
            }

            objects[catalogObjectId - 1] = $"<< /Type /Catalog /Pages {pagesObjectId} 0 R >>";
            objects[pagesObjectId - 1] = $"<< /Type /Pages /Kids [{string.Join(" ", pageObjectIds.Select(id => $"{id} 0 R"))}] /Count {pageObjectIds.Count} >>";

            return WritePdf(objects);
        }

        private static List<SchedulePdfPage> BuildPages(IReadOnlyList<SchedulePdfRow> rows)
        {
            if (rows.Count == 0)
            {
                return
                [
                    new SchedulePdfPage(
                        new SchedulePdfPageKey(0, "No Records", "-", "-", "-"),
                        ScheduleDayRules.AllDays.Take(4).ToList(),
                        [])
                ];
            }

            return rows
                .GroupBy(row => new SchedulePdfPageKey(
                    row.SemesterNumber,
                    row.StudyYear,
                    NormalizeBranch(row.Branch),
                    row.Section,
                    "-"))
                .OrderBy(group => StudyYearOrder(group.Key.StudyYear))
                .ThenBy(group => group.Key.Branch)
                .ThenBy(group => group.Key.Section)
                .ThenBy(group => group.Key.SemesterNumber)
                .Select(group => new SchedulePdfPage(
                    group.Key,
                    ScheduleDayRules.GetSchedulingDays(group.Key.StudyYear),
                    group.OrderBy(row => DayOrder(row.DayOfWeek))
                        .ThenBy(row => row.StartTime)
                        .ThenBy(row => row.Subject)
                        .ToList()))
                .ToList();
        }

        private static string BuildPageContent(SchedulePdfPage page)
        {
            var builder = new StringBuilder();

            WritePageTitle(builder, page.Key);
            DrawFilledRect(builder, TableLeft, TableTop - HeaderHeight, TableWidth, HeaderHeight, HeaderColor);
            DrawTableHeader(builder, page.Days);
            DrawTimelineRows(builder, page.Rows, page.Days);

            string content = builder.ToString();
            return $"<< /Length {Encoding.ASCII.GetByteCount(content)} >>\nstream\n{content}endstream";
        }

        private static void WritePageTitle(StringBuilder builder, SchedulePdfPageKey key)
        {
            string title = key.SemesterNumber == 0
                ? "Schedule"
                : $"Semester {key.SemesterNumber} - {key.Branch} - Section {CleanSection(key.Section)}";

            WriteText(builder, TableLeft, 552, title, 14, TextColor);
            WriteText(builder, TableLeft, 532, "Weekly timetable by day, lecture period, classroom, and instructor.", 8, MutedTextColor);
        }

        private static void DrawTableHeader(StringBuilder builder, IReadOnlyList<string> days)
        {
            WriteText(builder, TableLeft + 28, TableTop - 21, "Time", 9, HeaderTextColor);

            for (int index = 0; index < days.Count; index++)
            {
                int x = TableLeft + TimeColumnWidth + index * DayColumnWidth;
                WriteText(builder, x + 60, TableTop - 21, days[index], 9, HeaderTextColor);
            }
        }

        private static void DrawTimelineRows(
            StringBuilder builder,
            IReadOnlyList<SchedulePdfRow> rows,
            IReadOnlyList<string> days)
        {
            int rowTop = TableTop - HeaderHeight;

            DrawVerticalLines(builder, rowTop - TimelineHeight(), TableTop, days.Count);
            DrawLine(builder, TableLeft, rowTop, TableLeft + TableWidth, rowTop, BorderColor);

            foreach (var timelineRow in BuildTimeline())
            {
                int rowHeight = timelineRow.IsBreak ? BreakRowHeight : LectureRowHeight;
                int rowBottom = rowTop - rowHeight;

                if (timelineRow.IsBreak)
                {
                    DrawFilledRect(builder, TableLeft, rowBottom, TableWidth, rowHeight, BreakColor);
                }
                else if ((TableTop - HeaderHeight - rowTop) / LectureRowHeight % 2 == 1)
                {
                    DrawFilledRect(builder, TableLeft, rowBottom, TableWidth, rowHeight, AlternatingRowColor);
                }

                WriteText(builder, TableLeft + 13, rowBottom + rowHeight / 2 - 3, timelineRow.Label, 8, TextColor);

                if (timelineRow.IsBreak)
                {
                    DrawBreakCells(builder, rowBottom, rowHeight, days);
                }
                else
                {
                    DrawLectureCells(builder, rows, timelineRow, rowBottom, rowHeight, days);
                }

                DrawLine(builder, TableLeft, rowBottom, TableLeft + TableWidth, rowBottom, RowLineColor);
                rowTop = rowBottom;
            }

            DrawRect(builder, TableLeft, TableTop - HeaderHeight - TimelineHeight(), TableWidth, HeaderHeight + TimelineHeight(), BorderColor);
        }

        private static void DrawBreakCells(
            StringBuilder builder,
            int rowBottom,
            int rowHeight,
            IReadOnlyList<string> days)
        {
            for (int index = 0; index < days.Count; index++)
            {
                int x = TableLeft + TimeColumnWidth + index * DayColumnWidth;
                WriteText(builder, x + 72, rowBottom + rowHeight / 2 - 3, "Break", 8, TextColor);
            }
        }

        private static void DrawLectureCells(
            StringBuilder builder,
            IReadOnlyList<SchedulePdfRow> rows,
            TimelineRow timelineRow,
            int rowBottom,
            int rowHeight,
            IReadOnlyList<string> days)
        {
            for (int index = 0; index < days.Count; index++)
            {
                string day = days[index];
                int x = TableLeft + TimeColumnWidth + index * DayColumnWidth;
                var entries = rows
                    .Where(row =>
                        string.Equals(row.DayOfWeek, day, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(row.StartTime, FormatTime(timelineRow.Start), StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(row.EndTime, FormatTime(timelineRow.End), StringComparison.OrdinalIgnoreCase))
                    .OrderBy(row => row.GroupName)
                    .ThenBy(row => row.Subject)
                    .ToList();

                if (entries.Count == 0)
                {
                    continue;
                }

                WriteCellEntries(builder, x + 8, rowBottom + rowHeight - 11, entries);
            }
        }

        private static void WriteCellEntries(StringBuilder builder, int x, int yTop, IReadOnlyList<SchedulePdfRow> entries)
        {
            var lines = new List<string>();

            foreach (var entry in entries.Take(2))
            {
                if (lines.Count > 0)
                {
                    lines.Add(string.Empty);
                }

                lines.Add(Shorten(entry.Subject, 25));
                lines.Add(Shorten(entry.FacultyMember, 24));
                lines.Add(Shorten($"{entry.Classroom} | {BuildTypeLabel(entry)}", 25));
            }

            if (entries.Count > 2)
            {
                lines.Add($"+{entries.Count - 2} more");
            }

            int y = yTop;

            foreach (string line in lines.Take(8))
            {
                WriteText(builder, x, y, line, string.IsNullOrEmpty(line) ? 4 : 6, TextColor);
                y -= 8;
            }
        }

        private static IEnumerable<TimelineRow> BuildTimeline()
        {
            foreach (var slot in ScheduleTimingRules.OfficialLectureSlots.Where(slot => slot.Start < ScheduleTimingRules.BreakStart))
            {
                yield return TimelineRow.Lecture(slot.Start, slot.End);
            }

            yield return TimelineRow.Break(ScheduleTimingRules.BreakStart, ScheduleTimingRules.BreakEnd);

            foreach (var slot in ScheduleTimingRules.OfficialLectureSlots.Where(slot => slot.Start >= ScheduleTimingRules.BreakEnd))
            {
                yield return TimelineRow.Lecture(slot.Start, slot.End);
            }
        }

        private static int TimelineHeight()
        {
            return ScheduleTimingRules.OfficialLectureSlots.Count * LectureRowHeight + BreakRowHeight;
        }

        private static string BuildTypeLabel(SchedulePdfRow row)
        {
            if (!string.Equals(row.LectureType, "Practical", StringComparison.OrdinalIgnoreCase))
            {
                return "Theory";
            }

            return string.IsNullOrWhiteSpace(row.GroupName) || row.GroupName == "-" || row.GroupName == "All"
                ? "Practical"
                : $"Practical {row.GroupName}";
        }

        private static string NormalizeBranch(string branch)
        {
            return string.IsNullOrWhiteSpace(branch) || branch == "-"
                ? "General"
                : branch.Trim();
        }

        private static string CleanSection(string section)
        {
            return section.Split(" - ", StringSplitOptions.TrimEntries)[0];
        }

        private static string FormatTime(TimeSpan time)
        {
            return DateTime.Today.Add(time).ToString("hh:mm tt", CultureInfo.InvariantCulture);
        }

        private static int StudyYearOrder(string yearName)
        {
            return yearName.Trim().ToLowerInvariant() switch
            {
                "first year" => 1,
                "second year" => 2,
                "third year" => 3,
                "fourth year" => 4,
                _ => 99
            };
        }

        private static int DayOrder(string day)
        {
            return day.Trim().ToLowerInvariant() switch
            {
                "sunday" => 1,
                "monday" => 2,
                "tuesday" => 3,
                "wednesday" => 4,
                "thursday" => 5,
                _ => 99
            };
        }

        private static void DrawVerticalLines(
            StringBuilder builder,
            int tableBottom,
            int tableTop,
            int dayCount)
        {
            int x = TableLeft;

            for (int index = 0; index <= dayCount + 1; index++)
            {
                DrawLine(builder, x, tableBottom, x, tableTop, BorderColor);
                x += index == 0 ? TimeColumnWidth : DayColumnWidth;
            }
        }

        private static void WriteText(StringBuilder builder, int x, int y, string text, int fontSize, string color)
        {
            builder.AppendLine("BT");
            builder.AppendLine($"/F1 {fontSize} Tf");
            builder.AppendLine($"{color} rg");
            builder.AppendLine($"1 0 0 1 {x} {y} Tm ({Escape(text)}) Tj");
            builder.AppendLine("ET");
        }

        private static void DrawFilledRect(StringBuilder builder, int x, int y, int width, int height, string color)
        {
            builder.AppendLine("q");
            builder.AppendLine($"{color} rg");
            builder.AppendLine($"{x} {y} {width} {height} re f");
            builder.AppendLine("Q");
        }

        private static void DrawRect(StringBuilder builder, int x, int y, int width, int height, string color)
        {
            builder.AppendLine("q");
            builder.AppendLine($"{color} RG");
            builder.AppendLine("0.8 w");
            builder.AppendLine($"{x} {y} {width} {height} re S");
            builder.AppendLine("Q");
        }

        private static void DrawLine(StringBuilder builder, int x1, int y1, int x2, int y2, string color)
        {
            builder.AppendLine("q");
            builder.AppendLine($"{color} RG");
            builder.AppendLine("0.6 w");
            builder.AppendLine($"{x1} {y1} m {x2} {y2} l S");
            builder.AppendLine("Q");
        }

        private static string Shorten(string? value, int maxLength)
        {
            string text = string.IsNullOrWhiteSpace(value) ? "-" : value.Trim();
            return text.Length <= maxLength ? text : text[..Math.Max(0, maxLength - 3)] + "...";
        }

        private static string Escape(string value)
        {
            var safe = new string(value.Select(ch => ch is >= ' ' and <= '~' ? ch : '?').ToArray());
            return safe.Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)");
        }

        private static int AddObject(List<string> objects, string content)
        {
            objects.Add(content);
            return objects.Count;
        }

        private static byte[] WritePdf(IReadOnlyList<string> objects)
        {
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream, Encoding.ASCII, leaveOpen: true);
            var offsets = new List<long> { 0 };

            writer.WriteLine("%PDF-1.4");
            writer.Flush();

            for (int index = 0; index < objects.Count; index++)
            {
                offsets.Add(stream.Position);
                writer.WriteLine($"{index + 1} 0 obj");
                writer.WriteLine(objects[index]);
                writer.WriteLine("endobj");
                writer.Flush();
            }

            long xrefOffset = stream.Position;
            writer.WriteLine("xref");
            writer.WriteLine($"0 {objects.Count + 1}");
            writer.WriteLine("0000000000 65535 f ");

            foreach (long offset in offsets.Skip(1))
            {
                writer.WriteLine($"{offset:0000000000} 00000 n ");
            }

            writer.WriteLine("trailer");
            writer.WriteLine($"<< /Size {objects.Count + 1} /Root 1 0 R >>");
            writer.WriteLine("startxref");
            writer.WriteLine(xrefOffset);
            writer.WriteLine("%%EOF");
            writer.Flush();

            return stream.ToArray();
        }
    }

    public sealed record SchedulePdfRow(
        int SemesterNumber,
        string StudyYear,
        string Branch,
        string Section,
        string GroupName,
        string LectureType,
        string Subject,
        string FacultyMember,
        string Classroom,
        string DayOfWeek,
        string StartTime,
        string EndTime);

    internal sealed record SchedulePdfPage(
        SchedulePdfPageKey Key,
        IReadOnlyList<string> Days,
        IReadOnlyList<SchedulePdfRow> Rows);

    internal sealed record SchedulePdfPageKey(
        int SemesterNumber,
        string StudyYear,
        string Branch,
        string Section,
        string GroupName);

    internal sealed record TimelineRow(
        TimeSpan Start,
        TimeSpan End,
        bool IsBreak)
    {
        public string Label => $"{SchedulePdfExportServiceTime.Format(Start)} - {SchedulePdfExportServiceTime.Format(End)}";

        public static TimelineRow Lecture(TimeSpan start, TimeSpan end)
        {
            return new TimelineRow(start, end, false);
        }

        public static TimelineRow Break(TimeSpan start, TimeSpan end)
        {
            return new TimelineRow(start, end, true);
        }
    }

    internal static class SchedulePdfExportServiceTime
    {
        public static string Format(TimeSpan time)
        {
            return DateTime.Today.Add(time).ToString("hh:mm tt", CultureInfo.InvariantCulture);
        }
    }
}
