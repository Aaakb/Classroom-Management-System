namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SectionsPage
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
            lblPageTitle = PageDesignerHelper.CreateLabel("Sections", 28, 22, 18F);
            lblPageSubtitle = PageDesignerHelper.CreateLabel("Manage student sections by study year and branch.", 30, 58, 10F, false);
            pnlEditor = CreatePanel(28, 94, 900, 260);
            lblEditorTitle = PageDesignerHelper.CreateLabel("Section Details", 24, 18, 13F);
            lblEditorSubtitle = PageDesignerHelper.CreateLabel("Create and maintain section records.", 24, 45, 9F, false);
            lblSectionId = PageDesignerHelper.CreateLabel("Section ID", 24, 88);
            txtSectionId = PageDesignerHelper.CreateTextBox("txtSectionId", "Auto", 24, 113, true);
            lblSectionName = PageDesignerHelper.CreateLabel("Section Name", 228, 88);
            txtSectionName = PageDesignerHelper.CreateTextBox("txtSectionName", "Enter section name", 228, 113);
            lblStudentCount = PageDesignerHelper.CreateLabel("Student Count", 432, 88);
            txtStudentCount = PageDesignerHelper.CreateTextBox("txtStudentCount", "Enter count", 432, 113);
            lblStudyYear = PageDesignerHelper.CreateLabel("Study Year", 24, 162);
            cmbStudyYear = PageDesignerHelper.CreateComboBox("cmbStudyYear", 24, 187);
            lblBranch = PageDesignerHelper.CreateLabel("Branch", 228, 162);
            cmbBranch = PageDesignerHelper.CreateComboBox("cmbBranch", 228, 187);
            btnAdd = PageDesignerHelper.CreateButton("btnAdd", "Add", 648, 88, Color.FromArgb(22, 163, 74), Color.White);
            btnUpdate = PageDesignerHelper.CreateButton("btnUpdate", "Update", 768, 88, Color.FromArgb(14, 116, 144), Color.White);
            btnDelete = PageDesignerHelper.CreateButton("btnDelete", "Delete", 648, 132, Color.FromArgb(220, 38, 38), Color.White);
            btnClear = PageDesignerHelper.CreateButton("btnClear", "Clear", 768, 132, Color.White, Color.FromArgb(51, 65, 85));
            btnRefresh = PageDesignerHelper.CreateButton("btnRefresh", "Refresh", 648, 176, Color.White, Color.FromArgb(51, 65, 85));
            pnlTable = CreatePanel(28, 376, 900, 330);
            lblTableTitle = PageDesignerHelper.CreateLabel("Sections List", 24, 18, 13F);
            dgvRecords = PageDesignerHelper.CreateGrid("dgvRecords");
            colSectionId = PageDesignerHelper.CreateColumn("ID", 30F);
            colSectionName = PageDesignerHelper.CreateColumn("Section", 80F);
            colStudentCount = PageDesignerHelper.CreateColumn("Students", 60F);
            colStudyYear = PageDesignerHelper.CreateColumn("Study Year", 90F);
            colBranch = PageDesignerHelper.CreateColumn("Branch", 90F);
            rootPanel.SuspendLayout();
            pnlEditor.SuspendLayout();
            pnlTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecords).BeginInit();
            SuspendLayout();
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
            pnlEditor.Controls.Add(lblSectionId);
            pnlEditor.Controls.Add(txtSectionId);
            pnlEditor.Controls.Add(lblSectionName);
            pnlEditor.Controls.Add(txtSectionName);
            pnlEditor.Controls.Add(lblStudentCount);
            pnlEditor.Controls.Add(txtStudentCount);
            pnlEditor.Controls.Add(lblStudyYear);
            pnlEditor.Controls.Add(cmbStudyYear);
            pnlEditor.Controls.Add(lblBranch);
            pnlEditor.Controls.Add(cmbBranch);
            pnlEditor.Controls.Add(btnAdd);
            pnlEditor.Controls.Add(btnUpdate);
            pnlEditor.Controls.Add(btnDelete);
            pnlEditor.Controls.Add(btnClear);
            pnlEditor.Controls.Add(btnRefresh);
            pnlTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlTable.Controls.Add(lblTableTitle);
            pnlTable.Controls.Add(dgvRecords);
            dgvRecords.Columns.AddRange(new DataGridViewColumn[] { colSectionId, colSectionName, colStudentCount, colStudyYear, colBranch });
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(rootPanel);
            Name = "SectionsPage";
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
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSectionId;
        private Guna.UI2.WinForms.Guna2TextBox txtSectionId;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSectionName;
        private Guna.UI2.WinForms.Guna2TextBox txtSectionName;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblStudentCount;
        private Guna.UI2.WinForms.Guna2TextBox txtStudentCount;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblStudyYear;
        private Guna.UI2.WinForms.Guna2ComboBox cmbStudyYear;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblBranch;
        private Guna.UI2.WinForms.Guna2ComboBox cmbBranch;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2Button btnUpdate;
        private Guna.UI2.WinForms.Guna2Button btnDelete;
        private Guna.UI2.WinForms.Guna2Button btnClear;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Guna.UI2.WinForms.Guna2Panel pnlTable;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableTitle;
        private Guna.UI2.WinForms.Guna2DataGridView dgvRecords;
        private DataGridViewTextBoxColumn colSectionId;
        private DataGridViewTextBoxColumn colSectionName;
        private DataGridViewTextBoxColumn colStudentCount;
        private DataGridViewTextBoxColumn colStudyYear;
        private DataGridViewTextBoxColumn colBranch;
    }
}
