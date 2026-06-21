using System.Text;

namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    public sealed class SchedulePdfExportService
    {
        private const int RowsPerPage = 23;
        private const int PageWidth = 842;
        private const int PageHeight = 595;
        private const int TableLeft = 20;
        private const int TableTop = 558;
        private const int HeaderHeight = 26;
        private const int RowHeight = 20;
        private const int TableWidth = 802;

        private static readonly PdfColumn[] Columns =
        [
            new("Day", 62, 10, row => row.DayOfWeek),
            new("Time", 124, 22, row => BuildTimeLabel(row)),
            new("Class", 156, 27, row => BuildClassLabel(row)),
            new("Type", 82, 14, row => BuildTypeLabel(row)),
            new("Subject", 180, 32, row => row.Subject),
            new("Faculty", 132, 23, row => row.FacultyMember),
            new("Room", 66, 12, row => row.Classroom)
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

            var pageObjectIds = new List<int>();
            var rowPages = rows.Count == 0
                ? new List<IReadOnlyList<SchedulePdfRow>> { Array.Empty<SchedulePdfRow>() }
                : rows.Chunk(RowsPerPage).Select(chunk => (IReadOnlyList<SchedulePdfRow>)chunk).ToList();

            foreach (var pageRows in rowPages)
            {
                int contentObjectId = objects.Count + 1;
                AddObject(objects, BuildPageContent(pageRows));

                int pageObjectId = objects.Count + 1;
                pageObjectIds.Add(pageObjectId);
                AddObject(objects,
                    $"<< /Type /Page /Parent {pagesObjectId} 0 R /MediaBox [0 0 {PageWidth} {PageHeight}] /Resources << /Font << /F1 {fontObjectId} 0 R >> >> /Contents {contentObjectId} 0 R >>");
            }

            objects[catalogObjectId - 1] = $"<< /Type /Catalog /Pages {pagesObjectId} 0 R >>";
            objects[pagesObjectId - 1] = $"<< /Type /Pages /Kids [{string.Join(" ", pageObjectIds.Select(id => $"{id} 0 R"))}] /Count {pageObjectIds.Count} >>";

            return WritePdf(objects);
        }

        private static string BuildPageContent(IReadOnlyList<SchedulePdfRow> rows)
        {
            var builder = new StringBuilder();
            DrawFilledRect(builder, TableLeft, TableTop - HeaderHeight, TableWidth, HeaderHeight, "0.06 0.09 0.16");

            int rowTop = TableTop - HeaderHeight;

            foreach (var row in rows)
            {
                int rowBottom = rowTop - RowHeight;

                if ((TableTop - HeaderHeight - rowTop) / RowHeight % 2 == 1)
                {
                    DrawFilledRect(builder, TableLeft, rowBottom, TableWidth, RowHeight, "0.96 0.98 1");
                }

                rowTop = rowBottom;
            }

            DrawTableFrame(builder, TableTop, rows.Count);
            WriteHeader(builder);

            rowTop = TableTop - HeaderHeight;

            foreach (var row in rows)
            {
                int rowBottom = rowTop - RowHeight;
                WriteRow(builder, row, rowBottom + 6);
                rowTop = rowBottom;
            }

            if (rows.Count == 0)
            {
                int rowBottom = rowTop - RowHeight;
                WriteText(builder, TableLeft + 8, rowBottom + 6, "No schedule records to export.", 8, "0.10 0.12 0.18");
            }

            string content = builder.ToString();
            return $"<< /Length {Encoding.ASCII.GetByteCount(content)} >>\nstream\n{content}endstream";
        }

        private static void WriteHeader(StringBuilder builder)
        {
            int x = TableLeft;

            foreach (var column in Columns)
            {
                WriteText(builder, x + 6, TableTop - 16, column.Header, 8, "1 1 1");
                x += column.Width;
            }
        }

        private static void WriteRow(StringBuilder builder, SchedulePdfRow row, int y)
        {
            int x = TableLeft;

            foreach (var column in Columns)
            {
                WriteText(builder, x + 6, y, Shorten(column.GetValue(row), column.MaxLength), 7, "0.05 0.09 0.18");
                x += column.Width;
            }
        }

        private static string BuildTimeLabel(SchedulePdfRow row)
        {
            return $"{row.StartTime} - {row.EndTime}";
        }

        private static string BuildClassLabel(SchedulePdfRow row)
        {
            string section = row.Section.Split(" - ", StringSplitOptions.TrimEntries)[0];
            string branch = string.IsNullOrWhiteSpace(row.Branch) ||
                            row.Branch == "-" ||
                            string.Equals(row.Branch, "General", StringComparison.OrdinalIgnoreCase)
                ? string.Empty
                : $" / {row.Branch}";

            return $"{row.StudyYear}{branch} / {section}";
        }

        private static string BuildTypeLabel(SchedulePdfRow row)
        {
            if (!string.Equals(row.LectureType, "Practical", StringComparison.OrdinalIgnoreCase))
            {
                return "Theory";
            }

            return string.IsNullOrWhiteSpace(row.GroupName) || row.GroupName == "-"
                ? "Practical"
                : $"Practical {row.GroupName}";
        }

        private static void DrawTableFrame(StringBuilder builder, int tableTop, int rowCount)
        {
            int bodyRows = Math.Max(rowCount, 1);
            int tableBottom = tableTop - HeaderHeight - bodyRows * RowHeight;
            int x = TableLeft;

            DrawRect(builder, TableLeft, tableBottom, TableWidth, tableTop - tableBottom, "0.78 0.84 0.90");
            DrawLine(builder, TableLeft, tableTop - HeaderHeight, TableLeft + TableWidth, tableTop - HeaderHeight, "0.78 0.84 0.90");

            for (int row = 1; row <= bodyRows; row++)
            {
                int y = tableTop - HeaderHeight - row * RowHeight;
                DrawLine(builder, TableLeft, y, TableLeft + TableWidth, y, "0.86 0.90 0.95");
            }

            foreach (var column in Columns)
            {
                DrawLine(builder, x, tableBottom, x, tableTop, "0.78 0.84 0.90");
                x += column.Width;
            }

            DrawLine(builder, TableLeft + TableWidth, tableBottom, TableLeft + TableWidth, tableTop, "0.78 0.84 0.90");
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

    internal sealed record PdfColumn(
        string Header,
        int Width,
        int MaxLength,
        Func<SchedulePdfRow, string> GetValue);
}
