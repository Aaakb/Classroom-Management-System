namespace University_Timetable_and_Classroom_Management_System
{
    public partial class BranchesPage
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
            lblPageTitle = PageDesignerHelper.CreateLabel("Branches", 28, 22, 18F);
            lblPageSubtitle = PageDesignerHelper.CreateLabel("Manage academic branches used by subjects, sections, and schedules.", 30, 58, 10F, false);
            pnlEditor = CreatePanel(28, 94, 900, 210);
            lblEditorTitle = PageDesignerHelper.CreateLabel("Branch Details", 24, 18, 13F);
            lblEditorSubtitle = PageDesignerHelper.CreateLabel("Create and maintain branch records.", 24, 45, 9F, false);
            lblBranchId = PageDesignerHelper.CreateLabel("Branch ID", 24, 88);
            txtBranchId = PageDesignerHelper.CreateTextBox("txtBranchId", "Auto", 24, 113, true);
            lblBranchName = PageDesignerHelper.CreateLabel("Branch Name", 228, 88);
            txtBranchName = PageDesignerHelper.CreateTextBox("txtBranchName", "Enter branch name", 228, 113);
            btnAdd = PageDesignerHelper.CreateButton("btnAdd", "Add", 648, 88, Color.FromArgb(22, 163, 74), Color.White);
            btnUpdate = PageDesignerHelper.CreateButton("btnUpdate", "Update", 768, 88, Color.FromArgb(14, 116, 144), Color.White);
            btnDelete = PageDesignerHelper.CreateButton("btnDelete", "Delete", 648, 132, Color.FromArgb(220, 38, 38), Color.White);
            btnClear = PageDesignerHelper.CreateButton("btnClear", "Clear", 768, 132, Color.White, Color.FromArgb(51, 65, 85));
            btnRefresh = PageDesignerHelper.CreateButton("btnRefresh", "Refresh", 648, 176, Color.White, Color.FromArgb(51, 65, 85));
            pnlTable = CreatePanel(28, 326, 900, 380);
            lblTableTitle = PageDesignerHelper.CreateLabel("Branches List", 24, 18, 13F);
            dgvRecords = PageDesignerHelper.CreateGrid("dgvRecords");
            colBranchId = PageDesignerHelper.CreateColumn("ID", 30F);
            colBranchName = PageDesignerHelper.CreateColumn("Branch Name", 170F);
            rootPanel.SuspendLayout();
            pnlEditor.SuspendLayout();
            pnlTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecords).BeginInit();
            SuspendLayout();
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
            pnlEditor.Controls.Add(lblBranchId);
            pnlEditor.Controls.Add(txtBranchId);
            pnlEditor.Controls.Add(lblBranchName);
            pnlEditor.Controls.Add(txtBranchName);
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
            dgvRecords.Columns.AddRange(new DataGridViewColumn[] { colBranchId, colBranchName });
            // 
            // BranchesPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(rootPanel);
            Name = "BranchesPage";
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
        private Guna.UI2.WinForms.Guna2HtmlLabel lblBranchId;
        private Guna.UI2.WinForms.Guna2TextBox txtBranchId;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblBranchName;
        private Guna.UI2.WinForms.Guna2TextBox txtBranchName;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2Button btnUpdate;
        private Guna.UI2.WinForms.Guna2Button btnDelete;
        private Guna.UI2.WinForms.Guna2Button btnClear;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Guna.UI2.WinForms.Guna2Panel pnlTable;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableTitle;
        private Guna.UI2.WinForms.Guna2DataGridView dgvRecords;
        private DataGridViewTextBoxColumn colBranchId;
        private DataGridViewTextBoxColumn colBranchName;
    }
}
