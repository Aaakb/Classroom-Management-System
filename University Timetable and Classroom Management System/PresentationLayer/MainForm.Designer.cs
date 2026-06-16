namespace University_Timetable_and_Classroom_Management_System
{
    public partial class MainForm : Form
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlSidebar = new Guna.UI2.WinForms.Guna2Panel();
            btnSchedules = new Guna.UI2.WinForms.Guna2Button();
            btnTimeSlots = new Guna.UI2.WinForms.Guna2Button();
            btnClassrooms = new Guna.UI2.WinForms.Guna2Button();
            btnFacultyAssignments = new Guna.UI2.WinForms.Guna2Button();
            btnFacultyMembers = new Guna.UI2.WinForms.Guna2Button();
            btnSubjects = new Guna.UI2.WinForms.Guna2Button();
            btnSections = new Guna.UI2.WinForms.Guna2Button();
            btnStudyYears = new Guna.UI2.WinForms.Guna2Button();
            btnBranches = new Guna.UI2.WinForms.Guna2Button();
            btnDashboard = new Guna.UI2.WinForms.Guna2Button();
            separatorSidebar = new Guna.UI2.WinForms.Guna2Separator();
            lblSidebarSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblApplicationName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlContent = new Guna.UI2.WinForms.Guna2Panel();
            pnlSidebar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.Transparent;
            pnlSidebar.Controls.Add(btnSchedules);
            pnlSidebar.Controls.Add(btnTimeSlots);
            pnlSidebar.Controls.Add(btnClassrooms);
            pnlSidebar.Controls.Add(btnFacultyAssignments);
            pnlSidebar.Controls.Add(btnFacultyMembers);
            pnlSidebar.Controls.Add(btnSubjects);
            pnlSidebar.Controls.Add(btnSections);
            pnlSidebar.Controls.Add(btnStudyYears);
            pnlSidebar.Controls.Add(btnBranches);
            pnlSidebar.Controls.Add(btnDashboard);
            pnlSidebar.Controls.Add(separatorSidebar);
            pnlSidebar.Controls.Add(lblSidebarSubtitle);
            pnlSidebar.Controls.Add(lblApplicationName);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.FillColor = Color.FromArgb(24, 38, 62);
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(252, 760);
            pnlSidebar.TabIndex = 0;
            // 
            // btnSchedules
            // 
            btnSchedules.BorderRadius = 8;
            btnSchedules.Cursor = Cursors.Hand;
            btnSchedules.FillColor = Color.FromArgb(24, 38, 62);
            btnSchedules.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnSchedules.ForeColor = Color.FromArgb(226, 232, 240);
            btnSchedules.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnSchedules.Location = new Point(24, 568);
            btnSchedules.Name = "btnSchedules";
            btnSchedules.Size = new Size(204, 42);
            btnSchedules.TabIndex = 12;
            btnSchedules.Text = "Schedules";
            btnSchedules.TextAlign = HorizontalAlignment.Left;
            btnSchedules.TextOffset = new Point(14, 0);
            // 
            // btnTimeSlots
            // 
            btnTimeSlots.BorderRadius = 8;
            btnTimeSlots.Cursor = Cursors.Hand;
            btnTimeSlots.FillColor = Color.FromArgb(24, 38, 62);
            btnTimeSlots.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnTimeSlots.ForeColor = Color.FromArgb(226, 232, 240);
            btnTimeSlots.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnTimeSlots.Location = new Point(24, 520);
            btnTimeSlots.Name = "btnTimeSlots";
            btnTimeSlots.Size = new Size(204, 42);
            btnTimeSlots.TabIndex = 11;
            btnTimeSlots.Text = "Time Slots";
            btnTimeSlots.TextAlign = HorizontalAlignment.Left;
            btnTimeSlots.TextOffset = new Point(14, 0);
            // 
            // btnClassrooms
            // 
            btnClassrooms.BorderRadius = 8;
            btnClassrooms.Cursor = Cursors.Hand;
            btnClassrooms.FillColor = Color.FromArgb(24, 38, 62);
            btnClassrooms.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnClassrooms.ForeColor = Color.FromArgb(226, 232, 240);
            btnClassrooms.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnClassrooms.Location = new Point(24, 472);
            btnClassrooms.Name = "btnClassrooms";
            btnClassrooms.Size = new Size(204, 42);
            btnClassrooms.TabIndex = 10;
            btnClassrooms.Text = "Classrooms";
            btnClassrooms.TextAlign = HorizontalAlignment.Left;
            btnClassrooms.TextOffset = new Point(14, 0);
            // 
            // btnFacultyAssignments
            // 
            btnFacultyAssignments.BorderRadius = 8;
            btnFacultyAssignments.Cursor = Cursors.Hand;
            btnFacultyAssignments.FillColor = Color.FromArgb(24, 38, 62);
            btnFacultyAssignments.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnFacultyAssignments.ForeColor = Color.FromArgb(226, 232, 240);
            btnFacultyAssignments.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnFacultyAssignments.Location = new Point(24, 424);
            btnFacultyAssignments.Name = "btnFacultyAssignments";
            btnFacultyAssignments.Size = new Size(204, 42);
            btnFacultyAssignments.TabIndex = 9;
            btnFacultyAssignments.Text = "Faculty Assignments";
            btnFacultyAssignments.TextAlign = HorizontalAlignment.Left;
            btnFacultyAssignments.TextOffset = new Point(14, 0);
            // 
            // btnFacultyMembers
            // 
            btnFacultyMembers.BorderRadius = 8;
            btnFacultyMembers.Cursor = Cursors.Hand;
            btnFacultyMembers.FillColor = Color.FromArgb(24, 38, 62);
            btnFacultyMembers.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnFacultyMembers.ForeColor = Color.FromArgb(226, 232, 240);
            btnFacultyMembers.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnFacultyMembers.Location = new Point(24, 376);
            btnFacultyMembers.Name = "btnFacultyMembers";
            btnFacultyMembers.Size = new Size(204, 42);
            btnFacultyMembers.TabIndex = 8;
            btnFacultyMembers.Text = "Faculty Members";
            btnFacultyMembers.TextAlign = HorizontalAlignment.Left;
            btnFacultyMembers.TextOffset = new Point(14, 0);
            // 
            // btnSubjects
            // 
            btnSubjects.BorderRadius = 8;
            btnSubjects.Cursor = Cursors.Hand;
            btnSubjects.FillColor = Color.FromArgb(24, 38, 62);
            btnSubjects.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnSubjects.ForeColor = Color.FromArgb(226, 232, 240);
            btnSubjects.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnSubjects.Location = new Point(24, 328);
            btnSubjects.Name = "btnSubjects";
            btnSubjects.Size = new Size(204, 42);
            btnSubjects.TabIndex = 7;
            btnSubjects.Text = "Subjects";
            btnSubjects.TextAlign = HorizontalAlignment.Left;
            btnSubjects.TextOffset = new Point(14, 0);
            // 
            // btnSections
            // 
            btnSections.BorderRadius = 8;
            btnSections.Cursor = Cursors.Hand;
            btnSections.FillColor = Color.FromArgb(24, 38, 62);
            btnSections.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnSections.ForeColor = Color.FromArgb(226, 232, 240);
            btnSections.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnSections.Location = new Point(24, 280);
            btnSections.Name = "btnSections";
            btnSections.Size = new Size(204, 42);
            btnSections.TabIndex = 6;
            btnSections.Text = "Sections";
            btnSections.TextAlign = HorizontalAlignment.Left;
            btnSections.TextOffset = new Point(14, 0);
            // 
            // btnStudyYears
            // 
            btnStudyYears.BorderRadius = 8;
            btnStudyYears.Cursor = Cursors.Hand;
            btnStudyYears.FillColor = Color.FromArgb(24, 38, 62);
            btnStudyYears.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnStudyYears.ForeColor = Color.FromArgb(226, 232, 240);
            btnStudyYears.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnStudyYears.Location = new Point(24, 232);
            btnStudyYears.Name = "btnStudyYears";
            btnStudyYears.Size = new Size(204, 42);
            btnStudyYears.TabIndex = 5;
            btnStudyYears.Text = "Study Years";
            btnStudyYears.TextAlign = HorizontalAlignment.Left;
            btnStudyYears.TextOffset = new Point(14, 0);
            // 
            // btnBranches
            // 
            btnBranches.BorderRadius = 8;
            btnBranches.Cursor = Cursors.Hand;
            btnBranches.FillColor = Color.FromArgb(24, 38, 62);
            btnBranches.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnBranches.ForeColor = Color.FromArgb(226, 232, 240);
            btnBranches.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnBranches.Location = new Point(24, 184);
            btnBranches.Name = "btnBranches";
            btnBranches.Size = new Size(204, 42);
            btnBranches.TabIndex = 4;
            btnBranches.Text = "Branches";
            btnBranches.TextAlign = HorizontalAlignment.Left;
            btnBranches.TextOffset = new Point(14, 0);
            // 
            // btnDashboard
            // 
            btnDashboard.BorderRadius = 8;
            btnDashboard.Cursor = Cursors.Hand;
            btnDashboard.FillColor = Color.FromArgb(37, 99, 235);
            btnDashboard.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnDashboard.ForeColor = Color.White;
            btnDashboard.HoverState.FillColor = Color.FromArgb(29, 78, 216);
            btnDashboard.Location = new Point(24, 136);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(204, 42);
            btnDashboard.TabIndex = 3;
            btnDashboard.Text = "Dashboard";
            btnDashboard.TextAlign = HorizontalAlignment.Left;
            btnDashboard.TextOffset = new Point(14, 0);
            // 
            // separatorSidebar
            // 
            separatorSidebar.FillColor = Color.FromArgb(51, 65, 85);
            separatorSidebar.Location = new Point(24, 104);
            separatorSidebar.Name = "separatorSidebar";
            separatorSidebar.Size = new Size(204, 10);
            separatorSidebar.TabIndex = 2;
            // 
            // lblSidebarSubtitle
            // 
            lblSidebarSubtitle.BackColor = Color.Transparent;
            lblSidebarSubtitle.Font = new Font("Segoe UI", 9F);
            lblSidebarSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSidebarSubtitle.Location = new Point(26, 62);
            lblSidebarSubtitle.Name = "lblSidebarSubtitle";
            lblSidebarSubtitle.Size = new Size(133, 17);
            lblSidebarSubtitle.TabIndex = 1;
            lblSidebarSubtitle.Text = "Classroom Management";
            // 
            // lblApplicationName
            // 
            lblApplicationName.BackColor = Color.Transparent;
            lblApplicationName.Font = new Font("Segoe UI Semibold", 17F, FontStyle.Bold);
            lblApplicationName.ForeColor = Color.White;
            lblApplicationName.Location = new Point(24, 26);
            lblApplicationName.Name = "lblApplicationName";
            lblApplicationName.Size = new Size(216, 33);
            lblApplicationName.TabIndex = 0;
            lblApplicationName.Text = "University Timetable";
            // 
            // pnlContent
            // 
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.FillColor = Color.FromArgb(245, 247, 250);
            pnlContent.Location = new Point(252, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(988, 760);
            pnlContent.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1240, 760);
            Controls.Add(pnlContent);
            Controls.Add(pnlSidebar);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1100, 680);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Automated University Timetable and Classroom Management System";
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlSidebar;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblApplicationName;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSidebarSubtitle;
        private Guna.UI2.WinForms.Guna2Separator separatorSidebar;
        private Guna.UI2.WinForms.Guna2Button btnDashboard;
        private Guna.UI2.WinForms.Guna2Button btnBranches;
        private Guna.UI2.WinForms.Guna2Button btnStudyYears;
        private Guna.UI2.WinForms.Guna2Button btnSections;
        private Guna.UI2.WinForms.Guna2Button btnSubjects;
        private Guna.UI2.WinForms.Guna2Button btnFacultyMembers;
        private Guna.UI2.WinForms.Guna2Button btnFacultyAssignments;
        private Guna.UI2.WinForms.Guna2Button btnClassrooms;
        private Guna.UI2.WinForms.Guna2Button btnTimeSlots;
        private Guna.UI2.WinForms.Guna2Button btnSchedules;
        private Guna.UI2.WinForms.Guna2Panel pnlContent;
    }
}
