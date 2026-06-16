namespace University_Timetable_and_Classroom_Management_System
{
    public partial class ClassroomsPage
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
            lblPageTitle = PageDesignerHelper.CreateLabel("Classrooms", 28, 22, 18F);
            lblPageSubtitle = PageDesignerHelper.CreateLabel("Manage room capacity and classroom type information.", 30, 58, 10F, false);
            pnlEditor = CreatePanel(28, 94, 900, 240);
            lblEditorTitle = PageDesignerHelper.CreateLabel("Classroom Details", 24, 18, 13F);
            lblEditorSubtitle = PageDesignerHelper.CreateLabel("Create and maintain classroom records.", 24, 45, 9F, false);
            lblClassroomId = PageDesignerHelper.CreateLabel("Classroom ID", 24, 88);
            txtClassroomId = PageDesignerHelper.CreateTextBox("txtClassroomId", "Auto", 24, 113, true);
            lblClassroomNumber = PageDesignerHelper.CreateLabel("Classroom Number", 228, 88);
            txtClassroomNumber = PageDesignerHelper.CreateTextBox("txtClassroomNumber", "Enter room number", 228, 113);
            lblCapacity = PageDesignerHelper.CreateLabel("Capacity", 432, 88);
            txtCapacity = PageDesignerHelper.CreateTextBox("txtCapacity", "Enter capacity", 432, 113);
            lblRoomType = PageDesignerHelper.CreateLabel("Room Type", 24, 162);
            cmbRoomType = PageDesignerHelper.CreateComboBox("cmbRoomType", 24, 187);
            btnAdd = PageDesignerHelper.CreateButton("btnAdd", "Add", 648, 88, Color.FromArgb(22, 163, 74), Color.White);
            btnUpdate = PageDesignerHelper.CreateButton("btnUpdate", "Update", 768, 88, Color.FromArgb(14, 116, 144), Color.White);
            btnDelete = PageDesignerHelper.CreateButton("btnDelete", "Delete", 648, 132, Color.FromArgb(220, 38, 38), Color.White);
            btnClear = PageDesignerHelper.CreateButton("btnClear", "Clear", 768, 132, Color.White, Color.FromArgb(51, 65, 85));
            btnRefresh = PageDesignerHelper.CreateButton("btnRefresh", "Refresh", 648, 176, Color.White, Color.FromArgb(51, 65, 85));
            pnlTable = CreatePanel(28, 356, 900, 350);
            lblTableTitle = PageDesignerHelper.CreateLabel("Classrooms List", 24, 18, 13F);
            dgvRecords = PageDesignerHelper.CreateGrid("dgvRecords");
            colClassroomId = PageDesignerHelper.CreateColumn("ID", 30F);
            colClassroomNumber = PageDesignerHelper.CreateColumn("Room Number", 90F);
            colCapacity = PageDesignerHelper.CreateColumn("Capacity", 60F);
            colRoomType = PageDesignerHelper.CreateColumn("Room Type", 80F);
            rootPanel.SuspendLayout();
            pnlEditor.SuspendLayout();
            pnlTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecords).BeginInit();
            SuspendLayout();
            cmbRoomType.Items.AddRange(new object[] { "Lecture Hall", "Laboratory", "Seminar Room", "Computer Lab" });
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
            pnlEditor.Controls.Add(lblClassroomId);
            pnlEditor.Controls.Add(txtClassroomId);
            pnlEditor.Controls.Add(lblClassroomNumber);
            pnlEditor.Controls.Add(txtClassroomNumber);
            pnlEditor.Controls.Add(lblCapacity);
            pnlEditor.Controls.Add(txtCapacity);
            pnlEditor.Controls.Add(lblRoomType);
            pnlEditor.Controls.Add(cmbRoomType);
            pnlEditor.Controls.Add(btnAdd);
            pnlEditor.Controls.Add(btnUpdate);
            pnlEditor.Controls.Add(btnDelete);
            pnlEditor.Controls.Add(btnClear);
            pnlEditor.Controls.Add(btnRefresh);
            pnlTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlTable.Controls.Add(lblTableTitle);
            pnlTable.Controls.Add(dgvRecords);
            dgvRecords.Columns.AddRange(new DataGridViewColumn[] { colClassroomId, colClassroomNumber, colCapacity, colRoomType });
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(rootPanel);
            Name = "ClassroomsPage";
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
        private Guna.UI2.WinForms.Guna2HtmlLabel lblClassroomId;
        private Guna.UI2.WinForms.Guna2TextBox txtClassroomId;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblClassroomNumber;
        private Guna.UI2.WinForms.Guna2TextBox txtClassroomNumber;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCapacity;
        private Guna.UI2.WinForms.Guna2TextBox txtCapacity;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblRoomType;
        private Guna.UI2.WinForms.Guna2ComboBox cmbRoomType;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2Button btnUpdate;
        private Guna.UI2.WinForms.Guna2Button btnDelete;
        private Guna.UI2.WinForms.Guna2Button btnClear;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Guna.UI2.WinForms.Guna2Panel pnlTable;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableTitle;
        private Guna.UI2.WinForms.Guna2DataGridView dgvRecords;
        private DataGridViewTextBoxColumn colClassroomId;
        private DataGridViewTextBoxColumn colClassroomNumber;
        private DataGridViewTextBoxColumn colCapacity;
        private DataGridViewTextBoxColumn colRoomType;
    }
}
