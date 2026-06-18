using System.Text;

namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    public sealed class SchedulePdfExportService
    {
        private const int RowsPerPage = 24;

        public async Task ExportAsync(string filePath, IReadOnlyList<SchedulePdfRow> rows)
        {
            byte[] pdfBytes = BuildPdf(rows);
            await File.WriteAllBytesAsync(filePath, pdfBytes);
        }

        private static byte[] BuildPdf(IReadOnlyList<SchedulePdfRow> rows)
        {
            var objects = new List<string>();
            AddObject(objects, string.Empty); // Catalog placeholder.
            AddObject(objects, string.Empty); // Pages placeholder.
            AddObject(objects, "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>");

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
                    $"<< /Type /Page /Parent 2 0 R /MediaBox [0 0 842 595] /Resources << /Font << /F1 3 0 R >> >> /Contents {contentObjectId} 0 R >>");
            }

            objects[0] = "<< /Type /Catalog /Pages 2 0 R >>";
            objects[1] = $"<< /Type /Pages /Kids [{string.Join(" ", pageObjectIds.Select(id => $"{id} 0 R"))}] /Count {pageObjectIds.Count} >>";

            return WritePdf(objects);
        }

        private static string BuildPageContent(IReadOnlyList<SchedulePdfRow> rows)
        {
            var builder = new StringBuilder();
            builder.AppendLine("BT");
            builder.AppendLine("/F1 16 Tf");
            builder.AppendLine("1 0 0 1 36 558 Tm");
            builder.AppendLine($"({Escape("University Timetable Schedule")}) Tj");
            builder.AppendLine("/F1 8 Tf");

            int y = 532;
            WriteText(builder, 36, y, "Day");
            WriteText(builder, 100, y, "Time");
            WriteText(builder, 190, y, "Subject");
            WriteText(builder, 350, y, "Faculty");
            WriteText(builder, 500, y, "Room");
            WriteText(builder, 570, y, "Study Year");
            WriteText(builder, 665, y, "Branch");
            WriteText(builder, 745, y, "Section");

            y -= 18;

            foreach (var row in rows)
            {
                WriteText(builder, 36, y, Shorten(row.DayOfWeek, 10));
                WriteText(builder, 100, y, Shorten(row.TimeSlot, 16));
                WriteText(builder, 190, y, Shorten(row.Subject, 28));
                WriteText(builder, 350, y, Shorten(row.FacultyMember, 26));
                WriteText(builder, 500, y, Shorten(row.Classroom, 12));
                WriteText(builder, 570, y, Shorten(row.StudyYear, 16));
                WriteText(builder, 665, y, Shorten(row.Branch, 14));
                WriteText(builder, 745, y, Shorten(row.Section, 14));
                y -= 18;
            }

            if (rows.Count == 0)
            {
                WriteText(builder, 36, y, "No schedule records to export.");
            }

            builder.AppendLine("ET");

            string content = builder.ToString();
            return $"<< /Length {Encoding.ASCII.GetByteCount(content)} >>\nstream\n{content}endstream";
        }

        private static void WriteText(StringBuilder builder, int x, int y, string text)
        {
            builder.AppendLine($"1 0 0 1 {x} {y} Tm ({Escape(text)}) Tj");
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
        string DayOfWeek,
        string TimeSlot,
        string Subject,
        string FacultyMember,
        string Classroom,
        string StudyYear,
        string Branch,
        string Section);
}
