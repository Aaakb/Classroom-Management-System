using Guna.UI2.WinForms;

namespace University_Timetable_and_Classroom_Management_System
{
    internal static class PageDesignerHelper
    {
        public static Guna2HtmlLabel CreateLabel(string text, int x, int y, float size = 9.5F, bool bold = true)
        {
            return new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI" + (bold ? " Semibold" : string.Empty), size, bold ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = bold ? Color.FromArgb(51, 65, 85) : Color.FromArgb(100, 116, 139),
                Location = new Point(x, y),
                Text = text
            };
        }

        public static Guna2TextBox CreateTextBox(string name, string placeholder, int x, int y, bool readOnly = false)
        {
            return new Guna2TextBox
            {
                BorderColor = readOnly ? Color.FromArgb(226, 232, 240) : Color.FromArgb(203, 213, 225),
                BorderRadius = 8,
                Enabled = !readOnly,
                FillColor = readOnly ? Color.FromArgb(248, 250, 252) : Color.White,
                FocusedState = { BorderColor = Color.FromArgb(37, 99, 235) },
                Font = new Font("Segoe UI", 10F),
                ForeColor = readOnly ? Color.FromArgb(100, 116, 139) : Color.FromArgb(15, 23, 42),
                HoverState = { BorderColor = Color.FromArgb(59, 130, 246) },
                Location = new Point(x, y),
                Name = name,
                PlaceholderForeColor = Color.FromArgb(148, 163, 184),
                PlaceholderText = placeholder,
                Size = new Size(180, 42)
            };
        }

        public static Guna2ComboBox CreateComboBox(string name, int x, int y)
        {
            return new Guna2ComboBox
            {
                BackColor = Color.Transparent,
                BorderColor = Color.FromArgb(203, 213, 225),
                BorderRadius = 8,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FocusedColor = Color.FromArgb(37, 99, 235),
                FocusedState = { BorderColor = Color.FromArgb(37, 99, 235) },
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(15, 23, 42),
                HoverState = { BorderColor = Color.FromArgb(59, 130, 246) },
                ItemHeight = 36,
                Location = new Point(x, y),
                Name = name,
                Size = new Size(180, 42)
            };
        }

        public static Guna2DateTimePicker CreateTimePicker(string name, int x, int y)
        {
            return new Guna2DateTimePicker
            {
                BorderRadius = 8,
                Checked = true,
                CustomFormat = "HH:mm",
                FillColor = Color.White,
                Font = new Font("Segoe UI", 10F),
                Format = DateTimePickerFormat.Custom,
                Location = new Point(x, y),
                Name = name,
                ShowUpDown = true,
                Size = new Size(180, 42)
            };
        }

        public static Guna2ToggleSwitch CreateToggle(string name, int x, int y)
        {
            return new Guna2ToggleSwitch
            {
                CheckedState = { FillColor = Color.FromArgb(37, 99, 235) },
                Location = new Point(x, y + 7),
                Name = name,
                Size = new Size(52, 28),
                UncheckedState = { FillColor = Color.FromArgb(203, 213, 225) }
            };
        }

        public static Guna2Button CreateButton(string name, string text, int x, int y, Color fillColor, Color foreColor)
        {
            return new Guna2Button
            {
                BorderRadius = 8,
                Cursor = Cursors.Hand,
                FillColor = fillColor,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                ForeColor = foreColor,
                Location = new Point(x, y),
                Name = name,
                Size = new Size(108, 38),
                Text = text
            };
        }

        public static Guna2DataGridView CreateGrid(string name)
        {
            var grid = new Guna2DataGridView
            {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                ColumnHeadersHeight = 44,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                EnableHeadersVisualStyles = false,
                GridColor = Color.FromArgb(226, 232, 240),
                Location = new Point(24, 64),
                MultiSelect = false,
                Name = name,
                ReadOnly = true,
                RowHeadersVisible = false,
                RowTemplate = { Height = 42 },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Size = new Size(852, 290)
            };

            grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = Color.FromArgb(30, 41, 59),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(30, 64, 175)
            };

            grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                BackColor = Color.FromArgb(15, 23, 42),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(15, 23, 42),
                SelectionForeColor = Color.White,
                WrapMode = DataGridViewTriState.True
            };

            grid.DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(30, 41, 59),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(30, 64, 175)
            };

            return grid;
        }

        public static DataGridViewTextBoxColumn CreateColumn(string headerText, float fillWeight)
        {
            return new DataGridViewTextBoxColumn
            {
                HeaderText = headerText,
                ReadOnly = true,
                FillWeight = fillWeight
            };
        }
    }
}
