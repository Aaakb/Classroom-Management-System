namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SchedulesPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            rootPanel = new Guna.UI2.WinForms.Guna2Panel();
            lblPageTitle = PageDesignerHelper.CreateLabel("Schedules", 28, 22, 18F);
            lblPageSubtitle = PageDesignerHelper.CreateLabel("Manage the final timetable by day, subject, faculty member, room, and time slot.", 30, 58, 10F, false);
            pnlEditor = CreatePanel(28, 94, 900, 340);
            lblEditorTitle = PageDesignerHelper.CreateLabel("Schedule Details", 24, 18, 13F);
            lblEditorSubtitle = PageDesignerHelper.CreateLabel("Create and maintain timetable sessions.", 24, 45, 9F, false);
            lblScheduleId = PageDesignerHelper.CreateLabel("Schedule ID", 24, 88);
            txtScheduleId = PageDesignerHelper.CreateTextBox("txtScheduleId", "Auto", 24, 113, true);
            lblDayOfWeek = PageDesignerHelper.CreateLabel("Day Of Week", 228, 88);
            cmbDayOfWeek = PageDesignerHelper.CreateComboBox("cmbDayOfWeek", 228, 113);
            lblSubject = PageDesignerHelper.CreateLabel("Subject", 432, 88);
            cmbSubject = PageDesignerHelper.CreateComboBox("cmbSubject", 432, 113);
            lblFacultyMember = PageDesignerHelper.CreateLabel("Faculty Member", 24, 162);
            cmbFacultyMember = PageDesignerHelper.CreateComboBox("cmbFacultyMember", 24, 187);
            lblClassroom = PageDesignerHelper.CreateLabel("Classroom", 228, 162);
            cmbClassroom = PageDesignerHelper.CreateComboBox("cmbClassroom", 228, 187);
            lblTimeSlot = PageDesignerHelper.CreateLabel("Time Slot", 432, 162);
            cmbTimeSlot = PageDesignerHelper.CreateComboBox("cmbTimeSlot", 432, 187);
            lblStudyYear = PageDesignerHelper.CreateLabel("Study Year", 24, 236);
            cmbStudyYear = PageDesignerHelper.CreateComboBox("cmbStudyYear", 24, 261);
            lblBranch = PageDesignerHelper.CreateLabel("Branch", 228, 236);
            cmbBranch = PageDesignerHelper.CreateComboBox("cmbBranch", 228, 261);
            lblSection = PageDesignerHelper.CreateLabel("Section", 432, 236);
            cmbSection = PageDesignerHelper.CreateComboBox("cmbSection", 432, 261);
            btnAdd = PageDesignerHelper.CreateButton("btnAdd", "Add", 648, 88, Color.FromArgb(22, 163, 74), Color.White);
            btnUpdate = PageDesignerHelper.CreateButton("btnUpdate", "Update", 768, 88, Color.FromArgb(14, 116, 144), Color.White);
            btnDelete = PageDesignerHelper.CreateButton("btnDelete", "Delete", 648, 132, Color.FromArgb(220, 38, 38), Color.White);
            btnClear = PageDesignerHelper.CreateButton("btnClear", "Clear", 768, 132, Color.White, Color.FromArgb(51, 65, 85));
            btnRefresh = PageDesignerHelper.CreateButton("btnRefresh", "Refresh", 648, 176, Color.White, Color.FromArgb(51, 65, 85));
            pnlTable = CreatePanel(28, 456, 900, 330);
            lblTableTitle = PageDesignerHelper.CreateLabel("Schedules List", 24, 18, 13F);
            dgvRecords = PageDesignerHelper.CreateGrid("dgvRecords");
            colScheduleId = PageDesignerHelper.CreateColumn("ID", 28F);
            colDayOfWeek = PageDesignerHelper.CreateColumn("Day", 55F);
            colSubject = PageDesignerHelper.CreateColumn("Subject", 95F);
            colFacultyMember = PageDesignerHelper.CreateColumn("Faculty", 95F);
            colClassroom = PageDesignerHelper.CreateColumn("Room", 55F);
            colTimeSlot = PageDesignerHelper.CreateColumn("Time", 75F);
            colStudyYear = PageDesignerHelper.CreateColumn("Year", 55F);
            colBranch = PageDesignerHelper.CreateColumn("Branch", 65F);
            colSection = PageDesignerHelper.CreateColumn("Section", 65F);
            rootPanel.SuspendLayout();
            pnlEditor.SuspendLayout();
            pnlTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecords).BeginInit();
            SuspendLayout();
            // 
            // cmbDayOfWeek
            // 
            cmbDayOfWeek.Items.AddRange(new object[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday" });
            // 
            // rootPanel
            // 
            rootPanel.AutoScroll = true;
            rootPanel.Controls.Add(lblPageTitle);
            rootPanel.Controls.Add(lblPageSubtitle);
            rootPanel.Controls.Add(pnlEditor);
            rootPanel.Controls.Add(pnlTable);
            rootPanel.Dock = DockStyle.Fill;
            rootPanel.FillColor = Color.FromArgb(245, 247, 250);
            rootPanel.Name = "rootPanel";
            rootPanel.Size = new Size(960, 720);
            // 
            // pnlEditor
            // 
            pnlEditor.Controls.Add(lblEditorTitle);
            pnlEditor.Controls.Add(lblEditorSubtitle);
            pnlEditor.Controls.Add(lblScheduleId);
            pnlEditor.Controls.Add(txtScheduleId);
            pnlEditor.Controls.Add(lblDayOfWeek);
            pnlEditor.Controls.Add(cmbDayOfWeek);
            pnlEditor.Controls.Add(lblSubject);
            pnlEditor.Controls.Add(cmbSubject);
            pnlEditor.Controls.Add(lblFacultyMember);
            pnlEditor.Controls.Add(cmbFacultyMember);
            pnlEditor.Controls.Add(lblClassroom);
            pnlEditor.Controls.Add(cmbClassroom);
            pnlEditor.Controls.Add(lblTimeSlot);
            pnlEditor.Controls.Add(cmbTimeSlot);
            pnlEditor.Controls.Add(lblStudyYear);
            pnlEditor.Controls.Add(cmbStudyYear);
            pnlEditor.Controls.Add(lblBranch);
            pnlEditor.Controls.Add(cmbBranch);
            pnlEditor.Controls.Add(lblSection);
            pnlEditor.Controls.Add(cmbSection);
            pnlEditor.Controls.Add(btnAdd);
            pnlEditor.Controls.Add(btnUpdate);
            pnlEditor.Controls.Add(btnDelete);
            pnlEditor.Controls.Add(btnClear);
            pnlEditor.Controls.Add(btnRefresh);
            // 
            // pnlTable
            // 
            pnlTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlTable.Controls.Add(lblTableTitle);
            pnlTable.Controls.Add(dgvRecords);
            // 
            // dgvRecords
            // 
            dgvRecords.Columns.AddRange(new DataGridViewColumn[] { colScheduleId, colDayOfWeek, colSubject, colFacultyMember, colClassroom, colTimeSlot, colStudyYear, colBranch, colSection });
            // 
            // SchedulesPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(rootPanel);
            Name = "SchedulesPage";
            Size = new Size(960, 720);
            rootPanel.ResumeLayout(false);
            rootPanel.PerformLayout();
            pnlEditor.ResumeLayout(false);
            pnlEditor.PerformLayout();
            pnlTable.ResumeLayout(false);
            pnlTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecords).EndInit();
            ResumeLayout(false);
        }

        private static Guna.UI2.WinForms.Guna2Panel CreatePanel(int x, int y, int width, int height)
        {
            return new Guna.UI2.WinForms.Guna2Panel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent,
                BorderColor = Color.FromArgb(226, 232, 240),
                BorderRadius = 8,
                BorderThickness = 1,
                FillColor = Color.White,
                Location = new Point(x, y),
                Size = new Size(width, height)
            };
        }

        private Guna.UI2.WinForms.Guna2Panel rootPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageSubtitle;
        private Guna.UI2.WinForms.Guna2Panel pnlEditor;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEditorTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEditorSubtitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblScheduleId;
        private Guna.UI2.WinForms.Guna2TextBox txtScheduleId;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDayOfWeek;
        private Guna.UI2.WinForms.Guna2ComboBox cmbDayOfWeek;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubject;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSubject;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblFacultyMember;
        private Guna.UI2.WinForms.Guna2ComboBox cmbFacultyMember;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblClassroom;
        private Guna.UI2.WinForms.Guna2ComboBox cmbClassroom;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTimeSlot;
        private Guna.UI2.WinForms.Guna2ComboBox cmbTimeSlot;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblStudyYear;
        private Guna.UI2.WinForms.Guna2ComboBox cmbStudyYear;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblBranch;
        private Guna.UI2.WinForms.Guna2ComboBox cmbBranch;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSection;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSection;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2Button btnUpdate;
        private Guna.UI2.WinForms.Guna2Button btnDelete;
        private Guna.UI2.WinForms.Guna2Button btnClear;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Guna.UI2.WinForms.Guna2Panel pnlTable;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableTitle;
        private Guna.UI2.WinForms.Guna2DataGridView dgvRecords;
        private DataGridViewTextBoxColumn colScheduleId;
        private DataGridViewTextBoxColumn colDayOfWeek;
        private DataGridViewTextBoxColumn colSubject;
        private DataGridViewTextBoxColumn colFacultyMember;
        private DataGridViewTextBoxColumn colClassroom;
        private DataGridViewTextBoxColumn colTimeSlot;
        private DataGridViewTextBoxColumn colStudyYear;
        private DataGridViewTextBoxColumn colBranch;
        private DataGridViewTextBoxColumn colSection;
    }
}
