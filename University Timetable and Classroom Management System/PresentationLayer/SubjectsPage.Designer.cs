namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SubjectsPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            rootPanel = new Guna.UI2.WinForms.Guna2Panel();
            lblPageTitle = PageDesignerHelper.CreateLabel("Subjects", 28, 22, 18F);
            lblPageSubtitle = PageDesignerHelper.CreateLabel("Manage subjects, credit units, semester, and academic ownership.", 30, 58, 10F, false);
            pnlEditor = CreatePanel(28, 94, 900, 340);
            lblEditorTitle = PageDesignerHelper.CreateLabel("Subject Details", 24, 18, 13F);
            lblEditorSubtitle = PageDesignerHelper.CreateLabel("Create and maintain subject records.", 24, 45, 9F, false);
            lblSubjectId = PageDesignerHelper.CreateLabel("Subject ID", 24, 88);
            txtSubjectId = PageDesignerHelper.CreateTextBox("txtSubjectId", "Auto", 24, 113, true);
            lblSubjectName = PageDesignerHelper.CreateLabel("Subject Name", 228, 88);
            txtSubjectName = PageDesignerHelper.CreateTextBox("txtSubjectName", "Enter subject name", 228, 113);
            lblStudyYear = PageDesignerHelper.CreateLabel("Study Year", 432, 88);
            cmbStudyYear = PageDesignerHelper.CreateComboBox("cmbStudyYear", 432, 113);
            lblBranch = PageDesignerHelper.CreateLabel("Branch", 24, 162);
            cmbBranch = PageDesignerHelper.CreateComboBox("cmbBranch", 24, 187);
            lblSemester = PageDesignerHelper.CreateLabel("Semester", 228, 162);
            cmbSemester = PageDesignerHelper.CreateComboBox("cmbSemester", 228, 187);
            lblTheoreticalHours = PageDesignerHelper.CreateLabel("Theoretical Hours", 432, 162);
            txtTheoreticalHours = PageDesignerHelper.CreateTextBox("txtTheoreticalHours", "0", 432, 187);
            lblPracticalHours = PageDesignerHelper.CreateLabel("Practical Hours", 24, 236);
            txtPracticalHours = PageDesignerHelper.CreateTextBox("txtPracticalHours", "0", 24, 261);
            lblCreditUnits = PageDesignerHelper.CreateLabel("Credit Units", 228, 236);
            txtCreditUnits = PageDesignerHelper.CreateTextBox("txtCreditUnits", "0", 228, 261);
            lblRequirementType = PageDesignerHelper.CreateLabel("Requirement Type", 432, 236);
            cmbRequirementType = PageDesignerHelper.CreateComboBox("cmbRequirementType", 432, 261);
            btnAdd = PageDesignerHelper.CreateButton("btnAdd", "Add", 648, 88, Color.FromArgb(22, 163, 74), Color.White);
            btnUpdate = PageDesignerHelper.CreateButton("btnUpdate", "Update", 768, 88, Color.FromArgb(14, 116, 144), Color.White);
            btnDelete = PageDesignerHelper.CreateButton("btnDelete", "Delete", 648, 132, Color.FromArgb(220, 38, 38), Color.White);
            btnClear = PageDesignerHelper.CreateButton("btnClear", "Clear", 768, 132, Color.White, Color.FromArgb(51, 65, 85));
            btnRefresh = PageDesignerHelper.CreateButton("btnRefresh", "Refresh", 648, 176, Color.White, Color.FromArgb(51, 65, 85));
            pnlTable = CreatePanel(28, 456, 900, 330);
            lblTableTitle = PageDesignerHelper.CreateLabel("Subjects List", 24, 18, 13F);
            dgvRecords = PageDesignerHelper.CreateGrid("dgvRecords");
            colSubjectId = PageDesignerHelper.CreateColumn("ID", 28F);
            colSubjectName = PageDesignerHelper.CreateColumn("Subject", 115F);
            colStudyYear = PageDesignerHelper.CreateColumn("Study Year", 70F);
            colSemester = PageDesignerHelper.CreateColumn("Semester", 45F);
            colBranch = PageDesignerHelper.CreateColumn("Branch", 70F);
            colCreditUnits = PageDesignerHelper.CreateColumn("Units", 45F);
            colRequirementType = PageDesignerHelper.CreateColumn("Requirement", 80F);
            rootPanel.SuspendLayout();
            pnlEditor.SuspendLayout();
            pnlTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecords).BeginInit();
            SuspendLayout();
            cmbRequirementType.Items.AddRange(new object[] { "Core", "Elective", "University Requirement", "College Requirement" });
            rootPanel.AutoScroll = true;
            rootPanel.Controls.Add(lblPageTitle);
            rootPanel.Controls.Add(lblPageSubtitle);
            rootPanel.Controls.Add(pnlEditor);
            rootPanel.Controls.Add(pnlTable);
            rootPanel.Dock = DockStyle.Fill;
            rootPanel.FillColor = Color.FromArgb(245, 247, 250);
            rootPanel.Size = new Size(960, 720);
            pnlEditor.Controls.Add(lblEditorTitle);
            pnlEditor.Controls.Add(lblEditorSubtitle);
            pnlEditor.Controls.Add(lblSubjectId);
            pnlEditor.Controls.Add(txtSubjectId);
            pnlEditor.Controls.Add(lblSubjectName);
            pnlEditor.Controls.Add(txtSubjectName);
            pnlEditor.Controls.Add(lblStudyYear);
            pnlEditor.Controls.Add(cmbStudyYear);
            pnlEditor.Controls.Add(lblBranch);
            pnlEditor.Controls.Add(cmbBranch);
            pnlEditor.Controls.Add(lblSemester);
            pnlEditor.Controls.Add(cmbSemester);
            pnlEditor.Controls.Add(lblTheoreticalHours);
            pnlEditor.Controls.Add(txtTheoreticalHours);
            pnlEditor.Controls.Add(lblPracticalHours);
            pnlEditor.Controls.Add(txtPracticalHours);
            pnlEditor.Controls.Add(lblCreditUnits);
            pnlEditor.Controls.Add(txtCreditUnits);
            pnlEditor.Controls.Add(lblRequirementType);
            pnlEditor.Controls.Add(cmbRequirementType);
            pnlEditor.Controls.Add(btnAdd);
            pnlEditor.Controls.Add(btnUpdate);
            pnlEditor.Controls.Add(btnDelete);
            pnlEditor.Controls.Add(btnClear);
            pnlEditor.Controls.Add(btnRefresh);
            pnlTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlTable.Controls.Add(lblTableTitle);
            pnlTable.Controls.Add(dgvRecords);
            dgvRecords.Columns.AddRange(new DataGridViewColumn[] { colSubjectId, colSubjectName, colStudyYear, colSemester, colBranch, colCreditUnits, colRequirementType });
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(rootPanel);
            Name = "SubjectsPage";
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
            return new Guna.UI2.WinForms.Guna2Panel { Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, BackColor = Color.Transparent, BorderColor = Color.FromArgb(226, 232, 240), BorderRadius = 8, BorderThickness = 1, FillColor = Color.White, Location = new Point(x, y), Size = new Size(width, height) };
        }

        private Guna.UI2.WinForms.Guna2Panel rootPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageSubtitle;
        private Guna.UI2.WinForms.Guna2Panel pnlEditor;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEditorTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEditorSubtitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubjectId;
        private Guna.UI2.WinForms.Guna2TextBox txtSubjectId;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubjectName;
        private Guna.UI2.WinForms.Guna2TextBox txtSubjectName;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblStudyYear;
        private Guna.UI2.WinForms.Guna2ComboBox cmbStudyYear;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblBranch;
        private Guna.UI2.WinForms.Guna2ComboBox cmbBranch;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSemester;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSemester;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTheoreticalHours;
        private Guna.UI2.WinForms.Guna2TextBox txtTheoreticalHours;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPracticalHours;
        private Guna.UI2.WinForms.Guna2TextBox txtPracticalHours;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCreditUnits;
        private Guna.UI2.WinForms.Guna2TextBox txtCreditUnits;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblRequirementType;
        private Guna.UI2.WinForms.Guna2ComboBox cmbRequirementType;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2Button btnUpdate;
        private Guna.UI2.WinForms.Guna2Button btnDelete;
        private Guna.UI2.WinForms.Guna2Button btnClear;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Guna.UI2.WinForms.Guna2Panel pnlTable;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableTitle;
        private Guna.UI2.WinForms.Guna2DataGridView dgvRecords;
        private DataGridViewTextBoxColumn colSubjectId;
        private DataGridViewTextBoxColumn colSubjectName;
        private DataGridViewTextBoxColumn colStudyYear;
        private DataGridViewTextBoxColumn colSemester;
        private DataGridViewTextBoxColumn colBranch;
        private DataGridViewTextBoxColumn colCreditUnits;
        private DataGridViewTextBoxColumn colRequirementType;
    }
}
