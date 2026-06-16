namespace University_Timetable_and_Classroom_Management_System
{
    public partial class DashboardForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlSidebar = new Guna.UI2.WinForms.Guna2Panel();
            lblSidebarFooter = new Guna.UI2.WinForms.Guna2HtmlLabel();
            separatorSidebar = new Guna.UI2.WinForms.Guna2Separator();
            lblSidebarSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblApplicationName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            pnlWorkspace = new Guna.UI2.WinForms.Guna2Panel();
            pnlQuickStart = new Guna.UI2.WinForms.Guna2Panel();
            lblQuickStartBody = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblQuickStartTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlDatabaseStatus = new Guna.UI2.WinForms.Guna2Panel();
            lblDatabaseStatusValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblDatabaseStatusTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlSchedulesMetric = new Guna.UI2.WinForms.Guna2Panel();
            lblSchedulesMetricValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSchedulesMetricTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlClassroomsMetric = new Guna.UI2.WinForms.Guna2Panel();
            lblClassroomsMetricValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblClassroomsMetricTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlFacultyMetric = new Guna.UI2.WinForms.Guna2Panel();
            lblFacultyMetricValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblFacultyMetricTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlSubjectsMetric = new Guna.UI2.WinForms.Guna2Panel();
            lblSubjectsMetricValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSubjectsMetricTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            lblPageSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblPageTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlSidebar.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlWorkspace.SuspendLayout();
            pnlQuickStart.SuspendLayout();
            pnlDatabaseStatus.SuspendLayout();
            pnlSchedulesMetric.SuspendLayout();
            pnlClassroomsMetric.SuspendLayout();
            pnlFacultyMetric.SuspendLayout();
            pnlSubjectsMetric.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = System.Drawing.Color.Transparent;
            pnlSidebar.Controls.Add(lblSidebarFooter);
            pnlSidebar.Controls.Add(separatorSidebar);
            pnlSidebar.Controls.Add(lblSidebarSubtitle);
            pnlSidebar.Controls.Add(lblApplicationName);
            pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            pnlSidebar.FillColor = System.Drawing.Color.FromArgb(24, 38, 62);
            pnlSidebar.Location = new System.Drawing.Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new System.Drawing.Size(240, 720);
            pnlSidebar.TabIndex = 0;
            // 
            // lblSidebarFooter
            // 
            lblSidebarFooter.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblSidebarFooter.BackColor = System.Drawing.Color.Transparent;
            lblSidebarFooter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblSidebarFooter.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblSidebarFooter.Location = new System.Drawing.Point(24, 671);
            lblSidebarFooter.Name = "lblSidebarFooter";
            lblSidebarFooter.Size = new System.Drawing.Size(146, 17);
            lblSidebarFooter.TabIndex = 3;
            lblSidebarFooter.Text = "Academic Scheduling Suite";
            // 
            // separatorSidebar
            // 
            separatorSidebar.FillColor = System.Drawing.Color.FromArgb(51, 65, 85);
            separatorSidebar.Location = new System.Drawing.Point(24, 78);
            separatorSidebar.Name = "separatorSidebar";
            separatorSidebar.Size = new System.Drawing.Size(192, 10);
            separatorSidebar.TabIndex = 2;
            // 
            // lblSidebarSubtitle
            // 
            lblSidebarSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblSidebarSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblSidebarSubtitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblSidebarSubtitle.Location = new System.Drawing.Point(26, 52);
            lblSidebarSubtitle.Name = "lblSidebarSubtitle";
            lblSidebarSubtitle.Size = new System.Drawing.Size(130, 17);
            lblSidebarSubtitle.TabIndex = 1;
            lblSidebarSubtitle.Text = "Classroom Management";
            // 
            // lblApplicationName
            // 
            lblApplicationName.BackColor = System.Drawing.Color.Transparent;
            lblApplicationName.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblApplicationName.ForeColor = System.Drawing.Color.White;
            lblApplicationName.Location = new System.Drawing.Point(24, 20);
            lblApplicationName.Name = "lblApplicationName";
            lblApplicationName.Size = new System.Drawing.Size(206, 33);
            lblApplicationName.TabIndex = 0;
            lblApplicationName.Text = "University Timetable";
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(pnlWorkspace);
            pnlMain.Controls.Add(pnlHeader);
            pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlMain.FillColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlMain.Location = new System.Drawing.Point(240, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new System.Drawing.Size(940, 720);
            pnlMain.TabIndex = 1;
            // 
            // pnlWorkspace
            // 
            pnlWorkspace.Controls.Add(pnlQuickStart);
            pnlWorkspace.Controls.Add(pnlDatabaseStatus);
            pnlWorkspace.Controls.Add(pnlSchedulesMetric);
            pnlWorkspace.Controls.Add(pnlClassroomsMetric);
            pnlWorkspace.Controls.Add(pnlFacultyMetric);
            pnlWorkspace.Controls.Add(pnlSubjectsMetric);
            pnlWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlWorkspace.FillColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlWorkspace.Location = new System.Drawing.Point(0, 88);
            pnlWorkspace.Name = "pnlWorkspace";
            pnlWorkspace.Padding = new System.Windows.Forms.Padding(28, 24, 28, 28);
            pnlWorkspace.Size = new System.Drawing.Size(940, 632);
            pnlWorkspace.TabIndex = 1;
            // 
            // pnlQuickStart
            // 
            pnlQuickStart.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlQuickStart.BackColor = System.Drawing.Color.Transparent;
            pnlQuickStart.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlQuickStart.BorderRadius = 8;
            pnlQuickStart.BorderThickness = 1;
            pnlQuickStart.Controls.Add(lblQuickStartBody);
            pnlQuickStart.Controls.Add(lblQuickStartTitle);
            pnlQuickStart.FillColor = System.Drawing.Color.White;
            pnlQuickStart.Location = new System.Drawing.Point(28, 180);
            pnlQuickStart.Name = "pnlQuickStart";
            pnlQuickStart.Size = new System.Drawing.Size(544, 424);
            pnlQuickStart.TabIndex = 4;
            // 
            // lblQuickStartBody
            // 
            lblQuickStartBody.BackColor = System.Drawing.Color.Transparent;
            lblQuickStartBody.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblQuickStartBody.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblQuickStartBody.Location = new System.Drawing.Point(24, 58);
            lblQuickStartBody.Name = "lblQuickStartBody";
            lblQuickStartBody.Size = new System.Drawing.Size(421, 116);
            lblQuickStartBody.TabIndex = 1;
            lblQuickStartBody.Text = "Use the sidebar to manage academic branches, study years, sections, subjects, classrooms, time slots, faculty assignments, and final schedules. Database summaries will be connected in the next stage.";
            // 
            // lblQuickStartTitle
            // 
            lblQuickStartTitle.BackColor = System.Drawing.Color.Transparent;
            lblQuickStartTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblQuickStartTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblQuickStartTitle.Location = new System.Drawing.Point(24, 24);
            lblQuickStartTitle.Name = "lblQuickStartTitle";
            lblQuickStartTitle.Size = new System.Drawing.Size(129, 25);
            lblQuickStartTitle.TabIndex = 0;
            lblQuickStartTitle.Text = "System Guide";
            // 
            // pnlDatabaseStatus
            // 
            pnlDatabaseStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            pnlDatabaseStatus.BackColor = System.Drawing.Color.Transparent;
            pnlDatabaseStatus.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlDatabaseStatus.BorderRadius = 8;
            pnlDatabaseStatus.BorderThickness = 1;
            pnlDatabaseStatus.Controls.Add(lblDatabaseStatusValue);
            pnlDatabaseStatus.Controls.Add(lblDatabaseStatusTitle);
            pnlDatabaseStatus.FillColor = System.Drawing.Color.White;
            pnlDatabaseStatus.Location = new System.Drawing.Point(596, 180);
            pnlDatabaseStatus.Name = "pnlDatabaseStatus";
            pnlDatabaseStatus.Size = new System.Drawing.Size(316, 424);
            pnlDatabaseStatus.TabIndex = 5;
            // 
            // lblDatabaseStatusValue
            // 
            lblDatabaseStatusValue.BackColor = System.Drawing.Color.Transparent;
            lblDatabaseStatusValue.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblDatabaseStatusValue.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblDatabaseStatusValue.Location = new System.Drawing.Point(24, 62);
            lblDatabaseStatusValue.Name = "lblDatabaseStatusValue";
            lblDatabaseStatusValue.Size = new System.Drawing.Size(168, 32);
            lblDatabaseStatusValue.TabIndex = 1;
            lblDatabaseStatusValue.Text = "Not checked yet";
            // 
            // lblDatabaseStatusTitle
            // 
            lblDatabaseStatusTitle.BackColor = System.Drawing.Color.Transparent;
            lblDatabaseStatusTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblDatabaseStatusTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblDatabaseStatusTitle.Location = new System.Drawing.Point(24, 24);
            lblDatabaseStatusTitle.Name = "lblDatabaseStatusTitle";
            lblDatabaseStatusTitle.Size = new System.Drawing.Size(152, 25);
            lblDatabaseStatusTitle.TabIndex = 0;
            lblDatabaseStatusTitle.Text = "Database Status";
            // 
            // pnlSchedulesMetric
            // 
            pnlSchedulesMetric.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlSchedulesMetric.BackColor = System.Drawing.Color.Transparent;
            pnlSchedulesMetric.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlSchedulesMetric.BorderRadius = 8;
            pnlSchedulesMetric.BorderThickness = 1;
            pnlSchedulesMetric.Controls.Add(lblSchedulesMetricValue);
            pnlSchedulesMetric.Controls.Add(lblSchedulesMetricTitle);
            pnlSchedulesMetric.FillColor = System.Drawing.Color.White;
            pnlSchedulesMetric.Location = new System.Drawing.Point(697, 24);
            pnlSchedulesMetric.Name = "pnlSchedulesMetric";
            pnlSchedulesMetric.Size = new System.Drawing.Size(215, 132);
            pnlSchedulesMetric.TabIndex = 3;
            // 
            // lblSchedulesMetricValue
            // 
            lblSchedulesMetricValue.BackColor = System.Drawing.Color.Transparent;
            lblSchedulesMetricValue.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblSchedulesMetricValue.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblSchedulesMetricValue.Location = new System.Drawing.Point(22, 56);
            lblSchedulesMetricValue.Name = "lblSchedulesMetricValue";
            lblSchedulesMetricValue.Size = new System.Drawing.Size(31, 43);
            lblSchedulesMetricValue.TabIndex = 1;
            lblSchedulesMetricValue.Text = "--";
            // 
            // lblSchedulesMetricTitle
            // 
            lblSchedulesMetricTitle.BackColor = System.Drawing.Color.Transparent;
            lblSchedulesMetricTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblSchedulesMetricTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblSchedulesMetricTitle.Location = new System.Drawing.Point(24, 26);
            lblSchedulesMetricTitle.Name = "lblSchedulesMetricTitle";
            lblSchedulesMetricTitle.Size = new System.Drawing.Size(64, 19);
            lblSchedulesMetricTitle.TabIndex = 0;
            lblSchedulesMetricTitle.Text = "Schedules";
            // 
            // pnlClassroomsMetric
            // 
            pnlClassroomsMetric.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlClassroomsMetric.BackColor = System.Drawing.Color.Transparent;
            pnlClassroomsMetric.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlClassroomsMetric.BorderRadius = 8;
            pnlClassroomsMetric.BorderThickness = 1;
            pnlClassroomsMetric.Controls.Add(lblClassroomsMetricValue);
            pnlClassroomsMetric.Controls.Add(lblClassroomsMetricTitle);
            pnlClassroomsMetric.FillColor = System.Drawing.Color.White;
            pnlClassroomsMetric.Location = new System.Drawing.Point(474, 24);
            pnlClassroomsMetric.Name = "pnlClassroomsMetric";
            pnlClassroomsMetric.Size = new System.Drawing.Size(199, 132);
            pnlClassroomsMetric.TabIndex = 2;
            // 
            // lblClassroomsMetricValue
            // 
            lblClassroomsMetricValue.BackColor = System.Drawing.Color.Transparent;
            lblClassroomsMetricValue.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblClassroomsMetricValue.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblClassroomsMetricValue.Location = new System.Drawing.Point(22, 56);
            lblClassroomsMetricValue.Name = "lblClassroomsMetricValue";
            lblClassroomsMetricValue.Size = new System.Drawing.Size(31, 43);
            lblClassroomsMetricValue.TabIndex = 1;
            lblClassroomsMetricValue.Text = "--";
            // 
            // lblClassroomsMetricTitle
            // 
            lblClassroomsMetricTitle.BackColor = System.Drawing.Color.Transparent;
            lblClassroomsMetricTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblClassroomsMetricTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblClassroomsMetricTitle.Location = new System.Drawing.Point(24, 26);
            lblClassroomsMetricTitle.Name = "lblClassroomsMetricTitle";
            lblClassroomsMetricTitle.Size = new System.Drawing.Size(74, 19);
            lblClassroomsMetricTitle.TabIndex = 0;
            lblClassroomsMetricTitle.Text = "Classrooms";
            // 
            // pnlFacultyMetric
            // 
            pnlFacultyMetric.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlFacultyMetric.BackColor = System.Drawing.Color.Transparent;
            pnlFacultyMetric.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlFacultyMetric.BorderRadius = 8;
            pnlFacultyMetric.BorderThickness = 1;
            pnlFacultyMetric.Controls.Add(lblFacultyMetricValue);
            pnlFacultyMetric.Controls.Add(lblFacultyMetricTitle);
            pnlFacultyMetric.FillColor = System.Drawing.Color.White;
            pnlFacultyMetric.Location = new System.Drawing.Point(251, 24);
            pnlFacultyMetric.Name = "pnlFacultyMetric";
            pnlFacultyMetric.Size = new System.Drawing.Size(199, 132);
            pnlFacultyMetric.TabIndex = 1;
            // 
            // lblFacultyMetricValue
            // 
            lblFacultyMetricValue.BackColor = System.Drawing.Color.Transparent;
            lblFacultyMetricValue.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblFacultyMetricValue.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblFacultyMetricValue.Location = new System.Drawing.Point(22, 56);
            lblFacultyMetricValue.Name = "lblFacultyMetricValue";
            lblFacultyMetricValue.Size = new System.Drawing.Size(31, 43);
            lblFacultyMetricValue.TabIndex = 1;
            lblFacultyMetricValue.Text = "--";
            // 
            // lblFacultyMetricTitle
            // 
            lblFacultyMetricTitle.BackColor = System.Drawing.Color.Transparent;
            lblFacultyMetricTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblFacultyMetricTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblFacultyMetricTitle.Location = new System.Drawing.Point(24, 26);
            lblFacultyMetricTitle.Name = "lblFacultyMetricTitle";
            lblFacultyMetricTitle.Size = new System.Drawing.Size(48, 19);
            lblFacultyMetricTitle.TabIndex = 0;
            lblFacultyMetricTitle.Text = "Faculty";
            // 
            // pnlSubjectsMetric
            // 
            pnlSubjectsMetric.BackColor = System.Drawing.Color.Transparent;
            pnlSubjectsMetric.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlSubjectsMetric.BorderRadius = 8;
            pnlSubjectsMetric.BorderThickness = 1;
            pnlSubjectsMetric.Controls.Add(lblSubjectsMetricValue);
            pnlSubjectsMetric.Controls.Add(lblSubjectsMetricTitle);
            pnlSubjectsMetric.FillColor = System.Drawing.Color.White;
            pnlSubjectsMetric.Location = new System.Drawing.Point(28, 24);
            pnlSubjectsMetric.Name = "pnlSubjectsMetric";
            pnlSubjectsMetric.Size = new System.Drawing.Size(199, 132);
            pnlSubjectsMetric.TabIndex = 0;
            // 
            // lblSubjectsMetricValue
            // 
            lblSubjectsMetricValue.BackColor = System.Drawing.Color.Transparent;
            lblSubjectsMetricValue.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblSubjectsMetricValue.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblSubjectsMetricValue.Location = new System.Drawing.Point(22, 56);
            lblSubjectsMetricValue.Name = "lblSubjectsMetricValue";
            lblSubjectsMetricValue.Size = new System.Drawing.Size(31, 43);
            lblSubjectsMetricValue.TabIndex = 1;
            lblSubjectsMetricValue.Text = "--";
            // 
            // lblSubjectsMetricTitle
            // 
            lblSubjectsMetricTitle.BackColor = System.Drawing.Color.Transparent;
            lblSubjectsMetricTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblSubjectsMetricTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblSubjectsMetricTitle.Location = new System.Drawing.Point(24, 26);
            lblSubjectsMetricTitle.Name = "lblSubjectsMetricTitle";
            lblSubjectsMetricTitle.Size = new System.Drawing.Size(55, 19);
            lblSubjectsMetricTitle.TabIndex = 0;
            lblSubjectsMetricTitle.Text = "Subjects";
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblPageSubtitle);
            pnlHeader.Controls.Add(lblPageTitle);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.FillColor = System.Drawing.Color.White;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(940, 88);
            pnlHeader.TabIndex = 0;
            // 
            // lblPageSubtitle
            // 
            lblPageSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblPageSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblPageSubtitle.Location = new System.Drawing.Point(32, 50);
            lblPageSubtitle.Name = "lblPageSubtitle";
            lblPageSubtitle.Size = new System.Drawing.Size(399, 19);
            lblPageSubtitle.TabIndex = 1;
            lblPageSubtitle.Text = "Overview for timetable and classroom management workflows.";
            // 
            // lblPageTitle
            // 
            lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            lblPageTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblPageTitle.Location = new System.Drawing.Point(32, 16);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new System.Drawing.Size(128, 34);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Dashboard";
            // 
            // DashboardForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            ClientSize = new System.Drawing.Size(1180, 720);
            Controls.Add(pnlMain);
            Controls.Add(pnlSidebar);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            MinimumSize = new System.Drawing.Size(980, 600);
            Name = "DashboardForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Dashboard";
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlWorkspace.ResumeLayout(false);
            pnlQuickStart.ResumeLayout(false);
            pnlQuickStart.PerformLayout();
            pnlDatabaseStatus.ResumeLayout(false);
            pnlDatabaseStatus.PerformLayout();
            pnlSchedulesMetric.ResumeLayout(false);
            pnlSchedulesMetric.PerformLayout();
            pnlClassroomsMetric.ResumeLayout(false);
            pnlClassroomsMetric.PerformLayout();
            pnlFacultyMetric.ResumeLayout(false);
            pnlFacultyMetric.PerformLayout();
            pnlSubjectsMetric.ResumeLayout(false);
            pnlSubjectsMetric.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlSidebar;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblApplicationName;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSidebarSubtitle;
        private Guna.UI2.WinForms.Guna2Separator separatorSidebar;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSidebarFooter;
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageSubtitle;
        private Guna.UI2.WinForms.Guna2Panel pnlWorkspace;
        private Guna.UI2.WinForms.Guna2Panel pnlSubjectsMetric;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubjectsMetricTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubjectsMetricValue;
        private Guna.UI2.WinForms.Guna2Panel pnlFacultyMetric;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblFacultyMetricTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblFacultyMetricValue;
        private Guna.UI2.WinForms.Guna2Panel pnlClassroomsMetric;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblClassroomsMetricTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblClassroomsMetricValue;
        private Guna.UI2.WinForms.Guna2Panel pnlSchedulesMetric;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSchedulesMetricTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSchedulesMetricValue;
        private Guna.UI2.WinForms.Guna2Panel pnlQuickStart;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblQuickStartTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblQuickStartBody;
        private Guna.UI2.WinForms.Guna2Panel pnlDatabaseStatus;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDatabaseStatusTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDatabaseStatusValue;
    }
}
