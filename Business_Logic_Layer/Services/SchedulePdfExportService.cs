using System.Globalization;
using System.Text;

namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    public sealed class SchedulePdfExportService
    {
        private const int RowsPerPage = 18;
        private const int PageWidth = 842;
        private const int PageHeight = 595;
        private const int Margin = 30;
        private const int TableTop = 500;
        private const int HeaderHeight = 28;
        private const int RowHeight = 24;
        private const int TableWidth = 782;

        private const string HeaderColor = "0.10 0.18 0.31";
        private const string HeaderTextColor = "1 1 1";
        private const string BorderColor = "0.78 0.84 0.90";
        private const string RowLineColor = "0.88 0.92 0.96";
        private const string AlternatingRowColor = "0.97 0.98 1";
        private const string TextColor = "0.05 0.09 0.18";
        private const string MutedTextColor = "0.30 0.38 0.50";

        private static readonly PdfColumn[] Columns =
        [
            new("Day", 70, 12, row => row.DayOfWeek),
            new("Time", 120, 21, row => $"{row.StartTime} - {row.EndTime}"),
            new("Subject", 210, 34, row => row.Subject),
            new("Faculty", 140, 24, row => row.FacultyMember),
            new("Room", 120, 22, row => row.Classroom),
            new("Type", 82, 10, row => BuildTypeLabel(row)),
            new("Group", 40, 5, row => BuildGroupLabel(row))
        ];

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
                return [new SchedulePdfPage(new SchedulePdfPageKey(0, "No Records", "-", "-"), 1, 1, [])];
            }

            var pages = new List<SchedulePdfPage>();

            var groups = rows
                .GroupBy(row => new SchedulePdfPageKey(
                    row.SemesterNumber,
                    row.StudyYear,
                    NormalizeBranch(row.Branch),
                    CleanSection(row.Section)))
                .OrderBy(group => StudyYearOrder(group.Key.StudyYear))
                .ThenBy(group => group.Key.Branch)
                .ThenBy(group => group.Key.Section)
                .ThenBy(group => group.Key.SemesterNumber);

            foreach (var group in groups)
            {
                var orderedRows = group
                    .OrderBy(row => DayOrder(row.DayOfWeek))
                    .ThenBy(row => ParseDisplayTime(row.StartTime))
                    .ThenBy(row => row.Subject)
                    .ToList();
                var chunks = orderedRows.Chunk(RowsPerPage).ToList();

                for (int index = 0; index < chunks.Count; index++)
                {
                    pages.Add(new SchedulePdfPage(
                        group.Key,
                        index + 1,
                        chunks.Count,
                        chunks[index].ToList()));
                }
            }

            return pages;
        }

        private static string BuildPageContent(SchedulePdfPage page)
        {
            var builder = new StringBuilder();

            WritePageTitle(builder, page);
            DrawTable(builder, page.Rows);

            string content = builder.ToString();
            return $"<< /Length {Encoding.ASCII.GetByteCount(content)} >>\nstream\n{content}endstream";
        }

        private static void WritePageTitle(StringBuilder builder, SchedulePdfPage page)
        {
            string title = page.Key.SemesterNumber == 0
                ? "Schedule"
                : $"Semester {page.Key.SemesterNumber} - {page.Key.Branch} - Section {page.Key.Section}";
            string subtitle = page.TotalPages > 1
                ? $"Timetable sessions - Page {page.PageNumber} of {page.TotalPages}"
                : "Timetable sessions";

            WriteText(builder, Margin, 552, title, 14, TextColor);
            WriteText(builder, Margin, 532, subtitle, 8, MutedTextColor);
        }

        private static void DrawTable(StringBuilder builder, IReadOnlyList<SchedulePdfRow> rows)
        {
            DrawFilledRect(builder, Margin, TableTop - HeaderHeight, TableWidth, HeaderHeight, HeaderColor);
            DrawTableFrame(builder, rows.Count);
            WriteHeader(builder);

            int rowTop = TableTop - HeaderHeight;

            if (rows.Count == 0)
            {
                int rowBottom = rowTop - RowHeight;
                WriteText(builder, Margin + 8, rowBottom + 8, "No schedule records to export.", 8, TextColor);
                return;
            }

            for (int index = 0; index < rows.Count; index++)
            {
                int rowBottom = rowTop - RowHeight;

                if (index % 2 == 1)
                {
                    DrawFilledRect(builder, Margin, rowBottom, TableWidth, RowHeight, AlternatingRowColor);
                }

                WriteRow(builder, rows[index], rowBottom + 8);
                rowTop = rowBottom;
            }
        }

        private static void WriteHeader(StringBuilder builder)
        {
            int x = Margin;

            foreach (var column in Columns)
            {
                WriteText(builder, x + 6, TableTop - 18, column.Header, 8, HeaderTextColor);
                x += column.Width;
            }
        }

        private static void WriteRow(StringBuilder builder, SchedulePdfRow row, int y)
        {
            int x = Margin;

            foreach (var column in Columns)
            {
                WriteText(builder, x + 6, y, Shorten(column.GetValue(row), column.MaxLength), 7, TextColor);
                x += column.Width;
            }
        }

        private static void DrawTableFrame(StringBuilder builder, int rowCount)
        {
            int bodyRows = Math.Max(rowCount, 1);
            int tableBottom = TableTop - HeaderHeight - bodyRows * RowHeight;

            DrawRect(builder, Margin, tableBottom, TableWidth, TableTop - tableBottom, BorderColor);
            DrawLine(builder, Margin, TableTop - HeaderHeight, Margin + TableWidth, TableTop - HeaderHeight, BorderColor);

            for (int row = 1; row <= bodyRows; row++)
            {
                int y = TableTop - HeaderHeight - row * RowHeight;
                DrawLine(builder, Margin, y, Margin + TableWidth, y, RowLineColor);
            }

            int x = Margin;

            foreach (var column in Columns)
            {
                DrawLine(builder, x, tableBottom, x, TableTop, BorderColor);
                x += column.Width;
            }

            DrawLine(builder, Margin + TableWidth, tableBottom, Margin + TableWidth, TableTop, BorderColor);
        }

        private static string BuildTypeLabel(SchedulePdfRow row)
        {
            return string.Equals(row.LectureType, "Practical", StringComparison.OrdinalIgnoreCase)
                ? "Practical"
                : "Theory";
        }

        private static string BuildGroupLabel(SchedulePdfRow row)
        {
            return string.IsNullOrWhiteSpace(row.GroupName) ||
                row.GroupName == "-" ||
                row.GroupName == "All"
                ? "-"
                : row.GroupName;
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

        private static TimeSpan ParseDisplayTime(string value)
        {
            return DateTime.TryParseExact(
                value,
                "hh:mm tt",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var dateTime)
                ? dateTime.TimeOfDay
                : TimeSpan.Zero;
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
            builder.AppendLine("0.6 w");
            builder.AppendLine($"{x} {y} {width} {height} re S");
            builder.AppendLine("Q");
        }

        private static void DrawLine(StringBuilder builder, int x1, int y1, int x2, int y2, string color)
        {
            builder.AppendLine("q");
            builder.AppendLine($"{color} RG");
            builder.AppendLine("0.5 w");
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
        int PageNumber,
        int TotalPages,
        IReadOnlyList<SchedulePdfRow> Rows);

    internal sealed record SchedulePdfPageKey(
        int SemesterNumber,
        string StudyYear,
        string Branch,
        string Section);

    internal sealed record PdfColumn(
        string Header,
        int Width,
        int MaxLength,
        Func<SchedulePdfRow, string> GetValue);
}
