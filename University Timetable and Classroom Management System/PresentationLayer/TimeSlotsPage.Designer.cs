namespace University_Timetable_and_Classroom_Management_System
{
    public partial class TimeSlotsPage
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
            lblPageTitle = PageDesignerHelper.CreateLabel("Time Slots", 28, 22, 18F);
            lblPageSubtitle = PageDesignerHelper.CreateLabel("Manage lecture and break time ranges used by schedules.", 30, 58, 10F, false);
            pnlEditor = CreatePanel(28, 94, 900, 210);
            lblEditorTitle = PageDesignerHelper.CreateLabel("Time Slot Details", 24, 18, 13F);
            lblEditorSubtitle = PageDesignerHelper.CreateLabel("Create and maintain time slot records.", 24, 45, 9F, false);
            lblTimeSlotId = PageDesignerHelper.CreateLabel("Time Slot ID", 24, 88);
            txtTimeSlotId = PageDesignerHelper.CreateTextBox("txtTimeSlotId", "Auto", 24, 113, true);
            lblStartTime = PageDesignerHelper.CreateLabel("Start Time", 228, 88);
            dtpStartTime = PageDesignerHelper.CreateTimePicker("dtpStartTime", 228, 113);
            lblEndTime = PageDesignerHelper.CreateLabel("End Time", 432, 88);
            dtpEndTime = PageDesignerHelper.CreateTimePicker("dtpEndTime", 432, 113);
            lblIsBreak = PageDesignerHelper.CreateLabel("Is Break", 24, 162);
            tglIsBreak = PageDesignerHelper.CreateToggle("tglIsBreak", 24, 187);
            btnAdd = PageDesignerHelper.CreateButton("btnAdd", "Add", 648, 88, Color.FromArgb(22, 163, 74), Color.White);
            btnUpdate = PageDesignerHelper.CreateButton("btnUpdate", "Update", 768, 88, Color.FromArgb(14, 116, 144), Color.White);
            btnDelete = PageDesignerHelper.CreateButton("btnDelete", "Delete", 648, 132, Color.FromArgb(220, 38, 38), Color.White);
            btnClear = PageDesignerHelper.CreateButton("btnClear", "Clear", 768, 132, Color.White, Color.FromArgb(51, 65, 85));
            btnRefresh = PageDesignerHelper.CreateButton("btnRefresh", "Refresh", 648, 176, Color.White, Color.FromArgb(51, 65, 85));
            pnlTable = CreatePanel(28, 326, 900, 380);
            lblTableTitle = PageDesignerHelper.CreateLabel("Time Slots List", 24, 18, 13F);
            dgvRecords = PageDesignerHelper.CreateGrid("dgvRecords");
            colTimeSlotId = PageDesignerHelper.CreateColumn("ID", 30F);
            colStartTime = PageDesignerHelper.CreateColumn("Start", 70F);
            colEndTime = PageDesignerHelper.CreateColumn("End", 70F);
            colSlotType = PageDesignerHelper.CreateColumn("Type", 70F);
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
            pnlEditor.Controls.Add(lblTimeSlotId);
            pnlEditor.Controls.Add(txtTimeSlotId);
            pnlEditor.Controls.Add(lblStartTime);
            pnlEditor.Controls.Add(dtpStartTime);
            pnlEditor.Controls.Add(lblEndTime);
            pnlEditor.Controls.Add(dtpEndTime);
            pnlEditor.Controls.Add(lblIsBreak);
            pnlEditor.Controls.Add(tglIsBreak);
            pnlEditor.Controls.Add(btnAdd);
            pnlEditor.Controls.Add(btnUpdate);
            pnlEditor.Controls.Add(btnDelete);
            pnlEditor.Controls.Add(btnClear);
            pnlEditor.Controls.Add(btnRefresh);
            pnlTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlTable.Controls.Add(lblTableTitle);
            pnlTable.Controls.Add(dgvRecords);
            dgvRecords.Columns.AddRange(new DataGridViewColumn[] { colTimeSlotId, colStartTime, colEndTime, colSlotType });
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(rootPanel);
            Name = "TimeSlotsPage";
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
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTimeSlotId;
        private Guna.UI2.WinForms.Guna2TextBox txtTimeSlotId;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblStartTime;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpStartTime;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEndTime;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpEndTime;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblIsBreak;
        private Guna.UI2.WinForms.Guna2ToggleSwitch tglIsBreak;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2Button btnUpdate;
        private Guna.UI2.WinForms.Guna2Button btnDelete;
        private Guna.UI2.WinForms.Guna2Button btnClear;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Guna.UI2.WinForms.Guna2Panel pnlTable;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableTitle;
        private Guna.UI2.WinForms.Guna2DataGridView dgvRecords;
        private DataGridViewTextBoxColumn colTimeSlotId;
        private DataGridViewTextBoxColumn colStartTime;
        private DataGridViewTextBoxColumn colEndTime;
        private DataGridViewTextBoxColumn colSlotType;
    }
}
