namespace University_Timetable_and_Classroom_Management_System
{
    public partial class FacultyMemberSubjectsForm : System.Windows.Forms.Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            components = new System.ComponentModel.Container();
            pnlSidebar = new Guna.UI2.WinForms.Guna2Panel();
            lblSidebarFooter = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnNavigationSchedules = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationFacultyMembers = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationFacultyAssignments = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationTimeSlots = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationClassrooms = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationSubjects = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationSections = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationStudyYears = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationBranches = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationDashboard = new Guna.UI2.WinForms.Guna2Button();
            separatorSidebar = new Guna.UI2.WinForms.Guna2Separator();
            lblSidebarSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblApplicationName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            pnlWorkspace = new Guna.UI2.WinForms.Guna2Panel();
            pnlAssignmentsTable = new Guna.UI2.WinForms.Guna2Panel();
            dgvFacultyMemberSubjects = new Guna.UI2.WinForms.Guna2DataGridView();
            colFacultyMemberId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colFacultyMember = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSubjectId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSubject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblTableSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTableTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlAssignmentEditor = new Guna.UI2.WinForms.Guna2Panel();
            btnClearAssignmentForm = new Guna.UI2.WinForms.Guna2Button();
            btnDeleteAssignment = new Guna.UI2.WinForms.Guna2Button();
            btnUpdateAssignment = new Guna.UI2.WinForms.Guna2Button();
            btnAddAssignment = new Guna.UI2.WinForms.Guna2Button();
            cmbSubject = new Guna.UI2.WinForms.Guna2ComboBox();
            lblSubject = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbFacultyMember = new Guna.UI2.WinForms.Guna2ComboBox();
            lblFacultyMember = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEditorSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEditorTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            lblPageSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblPageTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlSidebar.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlWorkspace.SuspendLayout();
            pnlAssignmentsTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFacultyMemberSubjects).BeginInit();
            pnlAssignmentEditor.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = System.Drawing.Color.Transparent;
            pnlSidebar.Controls.Add(lblSidebarFooter);
            pnlSidebar.Controls.Add(btnNavigationSchedules);
            pnlSidebar.Controls.Add(btnNavigationFacultyMembers);
            pnlSidebar.Controls.Add(btnNavigationFacultyAssignments);
            pnlSidebar.Controls.Add(btnNavigationTimeSlots);
            pnlSidebar.Controls.Add(btnNavigationClassrooms);
            pnlSidebar.Controls.Add(btnNavigationSubjects);
            pnlSidebar.Controls.Add(btnNavigationSections);
            pnlSidebar.Controls.Add(btnNavigationStudyYears);
            pnlSidebar.Controls.Add(btnNavigationBranches);
            pnlSidebar.Controls.Add(btnNavigationDashboard);
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
            lblSidebarFooter.TabIndex = 13;
            lblSidebarFooter.Text = "Academic Scheduling Suite";
            // 
            // btnNavigationSchedules
            // 
            btnNavigationSchedules.BorderRadius = 8;
            btnNavigationSchedules.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationSchedules.FillColor = System.Drawing.Color.FromArgb(24, 38, 62);
            btnNavigationSchedules.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationSchedules.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnNavigationSchedules.HoverState.FillColor = System.Drawing.Color.FromArgb(36, 55, 86);
            btnNavigationSchedules.Location = new System.Drawing.Point(24, 602);
            btnNavigationSchedules.Name = "btnNavigationSchedules";
            btnNavigationSchedules.Size = new System.Drawing.Size(192, 44);
            btnNavigationSchedules.TabIndex = 12;
            btnNavigationSchedules.Text = "Schedules";
            btnNavigationSchedules.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            btnNavigationSchedules.TextOffset = new System.Drawing.Point(14, 0);
            // 
            // btnNavigationFacultyMembers
            // 
            btnNavigationFacultyMembers.BorderRadius = 8;
            btnNavigationFacultyMembers.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationFacultyMembers.FillColor = System.Drawing.Color.FromArgb(24, 38, 62);
            btnNavigationFacultyMembers.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationFacultyMembers.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnNavigationFacultyMembers.HoverState.FillColor = System.Drawing.Color.FromArgb(36, 55, 86);
            btnNavigationFacultyMembers.Location = new System.Drawing.Point(24, 546);
            btnNavigationFacultyMembers.Name = "btnNavigationFacultyMembers";
            btnNavigationFacultyMembers.Size = new System.Drawing.Size(192, 44);
            btnNavigationFacultyMembers.TabIndex = 11;
            btnNavigationFacultyMembers.Text = "Faculty Members";
            btnNavigationFacultyMembers.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            btnNavigationFacultyMembers.TextOffset = new System.Drawing.Point(14, 0);
            // 
            // btnNavigationFacultyAssignments
            // 
            btnNavigationFacultyAssignments.BorderRadius = 8;
            btnNavigationFacultyAssignments.Checked = true;
            btnNavigationFacultyAssignments.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationFacultyAssignments.Enabled = false;
            btnNavigationFacultyAssignments.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnNavigationFacultyAssignments.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationFacultyAssignments.ForeColor = System.Drawing.Color.White;
            btnNavigationFacultyAssignments.HoverState.FillColor = System.Drawing.Color.FromArgb(29, 78, 216);
            btnNavigationFacultyAssignments.Location = new System.Drawing.Point(24, 490);
            btnNavigationFacultyAssignments.Name = "btnNavigationFacultyAssignments";
            btnNavigationFacultyAssignments.Size = new System.Drawing.Size(192, 44);
            btnNavigationFacultyAssignments.TabIndex = 10;
            btnNavigationFacultyAssignments.Text = "Faculty Assignments";
            btnNavigationFacultyAssignments.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            btnNavigationFacultyAssignments.TextOffset = new System.Drawing.Point(14, 0);
            // 
            // btnNavigationTimeSlots
            // 
            btnNavigationTimeSlots.BorderRadius = 8;
            btnNavigationTimeSlots.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationTimeSlots.FillColor = System.Drawing.Color.FromArgb(24, 38, 62);
            btnNavigationTimeSlots.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationTimeSlots.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnNavigationTimeSlots.HoverState.FillColor = System.Drawing.Color.FromArgb(36, 55, 86);
            btnNavigationTimeSlots.Location = new System.Drawing.Point(24, 434);
            btnNavigationTimeSlots.Name = "btnNavigationTimeSlots";
            btnNavigationTimeSlots.Size = new System.Drawing.Size(192, 44);
            btnNavigationTimeSlots.TabIndex = 9;
            btnNavigationTimeSlots.Text = "Time Slots";
            btnNavigationTimeSlots.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            btnNavigationTimeSlots.TextOffset = new System.Drawing.Point(14, 0);
            // 
            // btnNavigationClassrooms
            // 
            btnNavigationClassrooms.BorderRadius = 8;
            btnNavigationClassrooms.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationClassrooms.FillColor = System.Drawing.Color.FromArgb(24, 38, 62);
            btnNavigationClassrooms.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationClassrooms.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnNavigationClassrooms.HoverState.FillColor = System.Drawing.Color.FromArgb(36, 55, 86);
            btnNavigationClassrooms.Location = new System.Drawing.Point(24, 378);
            btnNavigationClassrooms.Name = "btnNavigationClassrooms";
            btnNavigationClassrooms.Size = new System.Drawing.Size(192, 44);
            btnNavigationClassrooms.TabIndex = 8;
            btnNavigationClassrooms.Text = "Classrooms";
            btnNavigationClassrooms.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            btnNavigationClassrooms.TextOffset = new System.Drawing.Point(14, 0);
            // 
            // btnNavigationSubjects
            // 
            btnNavigationSubjects.BorderRadius = 8;
            btnNavigationSubjects.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationSubjects.FillColor = System.Drawing.Color.FromArgb(24, 38, 62);
            btnNavigationSubjects.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationSubjects.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnNavigationSubjects.HoverState.FillColor = System.Drawing.Color.FromArgb(36, 55, 86);
            btnNavigationSubjects.Location = new System.Drawing.Point(24, 322);
            btnNavigationSubjects.Name = "btnNavigationSubjects";
            btnNavigationSubjects.Size = new System.Drawing.Size(192, 44);
            btnNavigationSubjects.TabIndex = 7;
            btnNavigationSubjects.Text = "Subjects";
            btnNavigationSubjects.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            btnNavigationSubjects.TextOffset = new System.Drawing.Point(14, 0);
            // 
            // btnNavigationSections
            // 
            btnNavigationSections.BorderRadius = 8;
            btnNavigationSections.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationSections.FillColor = System.Drawing.Color.FromArgb(24, 38, 62);
            btnNavigationSections.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationSections.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnNavigationSections.HoverState.FillColor = System.Drawing.Color.FromArgb(36, 55, 86);
            btnNavigationSections.Location = new System.Drawing.Point(24, 266);
            btnNavigationSections.Name = "btnNavigationSections";
            btnNavigationSections.Size = new System.Drawing.Size(192, 44);
            btnNavigationSections.TabIndex = 6;
            btnNavigationSections.Text = "Sections";
            btnNavigationSections.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            btnNavigationSections.TextOffset = new System.Drawing.Point(14, 0);
            // 
            // btnNavigationStudyYears
            // 
            btnNavigationStudyYears.BorderRadius = 8;
            btnNavigationStudyYears.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationStudyYears.FillColor = System.Drawing.Color.FromArgb(24, 38, 62);
            btnNavigationStudyYears.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationStudyYears.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnNavigationStudyYears.HoverState.FillColor = System.Drawing.Color.FromArgb(36, 55, 86);
            btnNavigationStudyYears.Location = new System.Drawing.Point(24, 210);
            btnNavigationStudyYears.Name = "btnNavigationStudyYears";
            btnNavigationStudyYears.Size = new System.Drawing.Size(192, 44);
            btnNavigationStudyYears.TabIndex = 5;
            btnNavigationStudyYears.Text = "Study Years";
            btnNavigationStudyYears.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            btnNavigationStudyYears.TextOffset = new System.Drawing.Point(14, 0);
            // 
            // btnNavigationBranches
            // 
            btnNavigationBranches.BorderRadius = 8;
            btnNavigationBranches.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationBranches.FillColor = System.Drawing.Color.FromArgb(24, 38, 62);
            btnNavigationBranches.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationBranches.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnNavigationBranches.HoverState.FillColor = System.Drawing.Color.FromArgb(36, 55, 86);
            btnNavigationBranches.Location = new System.Drawing.Point(24, 154);
            btnNavigationBranches.Name = "btnNavigationBranches";
            btnNavigationBranches.Size = new System.Drawing.Size(192, 44);
            btnNavigationBranches.TabIndex = 4;
            btnNavigationBranches.Text = "Branches";
            btnNavigationBranches.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            btnNavigationBranches.TextOffset = new System.Drawing.Point(14, 0);
            // 
            // btnNavigationDashboard
            // 
            btnNavigationDashboard.BorderRadius = 8;
            btnNavigationDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationDashboard.FillColor = System.Drawing.Color.FromArgb(24, 38, 62);
            btnNavigationDashboard.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationDashboard.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnNavigationDashboard.HoverState.FillColor = System.Drawing.Color.FromArgb(36, 55, 86);
            btnNavigationDashboard.Location = new System.Drawing.Point(24, 98);
            btnNavigationDashboard.Name = "btnNavigationDashboard";
            btnNavigationDashboard.Size = new System.Drawing.Size(192, 44);
            btnNavigationDashboard.TabIndex = 3;
            btnNavigationDashboard.Text = "Dashboard";
            btnNavigationDashboard.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            btnNavigationDashboard.TextOffset = new System.Drawing.Point(14, 0);
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
            pnlWorkspace.Controls.Add(pnlAssignmentsTable);
            pnlWorkspace.Controls.Add(pnlAssignmentEditor);
            pnlWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlWorkspace.FillColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlWorkspace.Location = new System.Drawing.Point(0, 88);
            pnlWorkspace.Name = "pnlWorkspace";
            pnlWorkspace.Padding = new System.Windows.Forms.Padding(28, 24, 28, 28);
            pnlWorkspace.Size = new System.Drawing.Size(940, 632);
            pnlWorkspace.TabIndex = 1;
            // 
            // pnlAssignmentsTable
            // 
            pnlAssignmentsTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlAssignmentsTable.BackColor = System.Drawing.Color.Transparent;
            pnlAssignmentsTable.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlAssignmentsTable.BorderRadius = 8;
            pnlAssignmentsTable.BorderThickness = 1;
            pnlAssignmentsTable.Controls.Add(dgvFacultyMemberSubjects);
            pnlAssignmentsTable.Controls.Add(lblTableSubtitle);
            pnlAssignmentsTable.Controls.Add(lblTableTitle);
            pnlAssignmentsTable.FillColor = System.Drawing.Color.White;
            pnlAssignmentsTable.Location = new System.Drawing.Point(28, 248);
            pnlAssignmentsTable.Name = "pnlAssignmentsTable";
            pnlAssignmentsTable.Size = new System.Drawing.Size(884, 356);
            pnlAssignmentsTable.TabIndex = 1;
            // 
            // dgvFacultyMemberSubjects
            // 
            dgvFacultyMemberSubjects.AllowUserToAddRows = false;
            dgvFacultyMemberSubjects.AllowUserToDeleteRows = false;
            dgvFacultyMemberSubjects.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            dgvFacultyMemberSubjects.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvFacultyMemberSubjects.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvFacultyMemberSubjects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvFacultyMemberSubjects.BackgroundColor = System.Drawing.Color.White;
            dgvFacultyMemberSubjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvFacultyMemberSubjects.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvFacultyMemberSubjects.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvFacultyMemberSubjects.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvFacultyMemberSubjects.ColumnHeadersHeight = 44;
            dgvFacultyMemberSubjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvFacultyMemberSubjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colFacultyMemberId, colFacultyMember, colSubjectId, colSubject });
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvFacultyMemberSubjects.DefaultCellStyle = dataGridViewCellStyle3;
            dgvFacultyMemberSubjects.EnableHeadersVisualStyles = false;
            dgvFacultyMemberSubjects.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgvFacultyMemberSubjects.Location = new System.Drawing.Point(24, 78);
            dgvFacultyMemberSubjects.MultiSelect = false;
            dgvFacultyMemberSubjects.Name = "dgvFacultyMemberSubjects";
            dgvFacultyMemberSubjects.ReadOnly = true;
            dgvFacultyMemberSubjects.RowHeadersVisible = false;
            dgvFacultyMemberSubjects.RowTemplate.Height = 42;
            dgvFacultyMemberSubjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvFacultyMemberSubjects.Size = new System.Drawing.Size(836, 254);
            dgvFacultyMemberSubjects.TabIndex = 2;
            // 
            // colFacultyMemberId
            // 
            colFacultyMemberId.DataPropertyName = "FacultyMemberID";
            colFacultyMemberId.FillWeight = 45F;
            colFacultyMemberId.HeaderText = "Faculty ID";
            colFacultyMemberId.Name = "colFacultyMemberId";
            colFacultyMemberId.ReadOnly = true;
            // 
            // colFacultyMember
            // 
            colFacultyMember.FillWeight = 125F;
            colFacultyMember.HeaderText = "Faculty Member";
            colFacultyMember.Name = "colFacultyMember";
            colFacultyMember.ReadOnly = true;
            // 
            // colSubjectId
            // 
            colSubjectId.DataPropertyName = "SubjectID";
            colSubjectId.FillWeight = 45F;
            colSubjectId.HeaderText = "Subject ID";
            colSubjectId.Name = "colSubjectId";
            colSubjectId.ReadOnly = true;
            // 
            // colSubject
            // 
            colSubject.FillWeight = 125F;
            colSubject.HeaderText = "Subject";
            colSubject.Name = "colSubject";
            colSubject.ReadOnly = true;
            // 
            // lblTableSubtitle
            // 
            lblTableSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblTableSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblTableSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTableSubtitle.Location = new System.Drawing.Point(24, 43);
            lblTableSubtitle.Name = "lblTableSubtitle";
            lblTableSubtitle.Size = new System.Drawing.Size(310, 17);
            lblTableSubtitle.TabIndex = 1;
            lblTableSubtitle.Text = "Review assigned teaching responsibilities by subject.";
            // 
            // lblTableTitle
            // 
            lblTableTitle.BackColor = System.Drawing.Color.Transparent;
            lblTableTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTableTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblTableTitle.Location = new System.Drawing.Point(24, 18);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new System.Drawing.Size(205, 25);
            lblTableTitle.TabIndex = 0;
            lblTableTitle.Text = "Faculty Assignments List";
            // 
            // pnlAssignmentEditor
            // 
            pnlAssignmentEditor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlAssignmentEditor.BackColor = System.Drawing.Color.Transparent;
            pnlAssignmentEditor.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlAssignmentEditor.BorderRadius = 8;
            pnlAssignmentEditor.BorderThickness = 1;
            pnlAssignmentEditor.Controls.Add(btnClearAssignmentForm);
            pnlAssignmentEditor.Controls.Add(btnDeleteAssignment);
            pnlAssignmentEditor.Controls.Add(btnUpdateAssignment);
            pnlAssignmentEditor.Controls.Add(btnAddAssignment);
            pnlAssignmentEditor.Controls.Add(cmbSubject);
            pnlAssignmentEditor.Controls.Add(lblSubject);
            pnlAssignmentEditor.Controls.Add(cmbFacultyMember);
            pnlAssignmentEditor.Controls.Add(lblFacultyMember);
            pnlAssignmentEditor.Controls.Add(lblEditorSubtitle);
            pnlAssignmentEditor.Controls.Add(lblEditorTitle);
            pnlAssignmentEditor.FillColor = System.Drawing.Color.White;
            pnlAssignmentEditor.Location = new System.Drawing.Point(28, 24);
            pnlAssignmentEditor.Name = "pnlAssignmentEditor";
            pnlAssignmentEditor.Size = new System.Drawing.Size(884, 202);
            pnlAssignmentEditor.TabIndex = 0;
            // 
            // btnClearAssignmentForm
            // 
            btnClearAssignmentForm.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClearAssignmentForm.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnClearAssignmentForm.BorderRadius = 8;
            btnClearAssignmentForm.BorderThickness = 1;
            btnClearAssignmentForm.Cursor = System.Windows.Forms.Cursors.Hand;
            btnClearAssignmentForm.FillColor = System.Drawing.Color.White;
            btnClearAssignmentForm.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnClearAssignmentForm.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            btnClearAssignmentForm.HoverState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            btnClearAssignmentForm.Location = new System.Drawing.Point(752, 142);
            btnClearAssignmentForm.Name = "btnClearAssignmentForm";
            btnClearAssignmentForm.Size = new System.Drawing.Size(108, 38);
            btnClearAssignmentForm.TabIndex = 9;
            btnClearAssignmentForm.Text = "Clear";
            // 
            // btnDeleteAssignment
            // 
            btnDeleteAssignment.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnDeleteAssignment.BorderRadius = 8;
            btnDeleteAssignment.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDeleteAssignment.FillColor = System.Drawing.Color.FromArgb(220, 38, 38);
            btnDeleteAssignment.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnDeleteAssignment.ForeColor = System.Drawing.Color.White;
            btnDeleteAssignment.HoverState.FillColor = System.Drawing.Color.FromArgb(185, 28, 28);
            btnDeleteAssignment.Location = new System.Drawing.Point(632, 142);
            btnDeleteAssignment.Name = "btnDeleteAssignment";
            btnDeleteAssignment.Size = new System.Drawing.Size(108, 38);
            btnDeleteAssignment.TabIndex = 8;
            btnDeleteAssignment.Text = "Delete";
            // 
            // btnUpdateAssignment
            // 
            btnUpdateAssignment.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnUpdateAssignment.BorderRadius = 8;
            btnUpdateAssignment.Cursor = System.Windows.Forms.Cursors.Hand;
            btnUpdateAssignment.FillColor = System.Drawing.Color.FromArgb(14, 116, 144);
            btnUpdateAssignment.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnUpdateAssignment.ForeColor = System.Drawing.Color.White;
            btnUpdateAssignment.HoverState.FillColor = System.Drawing.Color.FromArgb(21, 94, 117);
            btnUpdateAssignment.Location = new System.Drawing.Point(752, 98);
            btnUpdateAssignment.Name = "btnUpdateAssignment";
            btnUpdateAssignment.Size = new System.Drawing.Size(108, 38);
            btnUpdateAssignment.TabIndex = 7;
            btnUpdateAssignment.Text = "Update";
            // 
            // btnAddAssignment
            // 
            btnAddAssignment.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAddAssignment.BorderRadius = 8;
            btnAddAssignment.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAddAssignment.FillColor = System.Drawing.Color.FromArgb(22, 163, 74);
            btnAddAssignment.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnAddAssignment.ForeColor = System.Drawing.Color.White;
            btnAddAssignment.HoverState.FillColor = System.Drawing.Color.FromArgb(21, 128, 61);
            btnAddAssignment.Location = new System.Drawing.Point(632, 98);
            btnAddAssignment.Name = "btnAddAssignment";
            btnAddAssignment.Size = new System.Drawing.Size(108, 38);
            btnAddAssignment.TabIndex = 6;
            btnAddAssignment.Text = "Add";
            // 
            // cmbSubject
            // 
            cmbSubject.BackColor = System.Drawing.Color.Transparent;
            cmbSubject.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            cmbSubject.BorderRadius = 8;
            cmbSubject.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbSubject.FocusedColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbSubject.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbSubject.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cmbSubject.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            cmbSubject.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            cmbSubject.ItemHeight = 36;
            cmbSubject.Location = new System.Drawing.Point(316, 112);
            cmbSubject.Name = "cmbSubject";
            cmbSubject.Size = new System.Drawing.Size(276, 42);
            cmbSubject.TabIndex = 5;
            // 
            // lblSubject
            // 
            lblSubject.BackColor = System.Drawing.Color.Transparent;
            lblSubject.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblSubject.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblSubject.Location = new System.Drawing.Point(316, 87);
            lblSubject.Name = "lblSubject";
            lblSubject.Size = new System.Drawing.Size(47, 19);
            lblSubject.TabIndex = 4;
            lblSubject.Text = "Subject";
            // 
            // cmbFacultyMember
            // 
            cmbFacultyMember.BackColor = System.Drawing.Color.Transparent;
            cmbFacultyMember.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            cmbFacultyMember.BorderRadius = 8;
            cmbFacultyMember.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbFacultyMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbFacultyMember.FocusedColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbFacultyMember.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbFacultyMember.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cmbFacultyMember.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            cmbFacultyMember.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            cmbFacultyMember.ItemHeight = 36;
            cmbFacultyMember.Location = new System.Drawing.Point(24, 112);
            cmbFacultyMember.Name = "cmbFacultyMember";
            cmbFacultyMember.Size = new System.Drawing.Size(264, 42);
            cmbFacultyMember.TabIndex = 3;
            // 
            // lblFacultyMember
            // 
            lblFacultyMember.BackColor = System.Drawing.Color.Transparent;
            lblFacultyMember.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblFacultyMember.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblFacultyMember.Location = new System.Drawing.Point(24, 87);
            lblFacultyMember.Name = "lblFacultyMember";
            lblFacultyMember.Size = new System.Drawing.Size(101, 19);
            lblFacultyMember.TabIndex = 2;
            lblFacultyMember.Text = "Faculty Member";
            // 
            // lblEditorSubtitle
            // 
            lblEditorSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblEditorSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblEditorSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblEditorSubtitle.Location = new System.Drawing.Point(24, 44);
            lblEditorSubtitle.Name = "lblEditorSubtitle";
            lblEditorSubtitle.Size = new System.Drawing.Size(361, 17);
            lblEditorSubtitle.TabIndex = 1;
            lblEditorSubtitle.Text = "Assign teaching responsibilities before generating schedules.";
            // 
            // lblEditorTitle
            // 
            lblEditorTitle.BackColor = System.Drawing.Color.Transparent;
            lblEditorTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblEditorTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblEditorTitle.Location = new System.Drawing.Point(24, 18);
            lblEditorTitle.Name = "lblEditorTitle";
            lblEditorTitle.Size = new System.Drawing.Size(224, 25);
            lblEditorTitle.TabIndex = 0;
            lblEditorTitle.Text = "Faculty Assignment Details";
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
            lblPageSubtitle.Size = new System.Drawing.Size(419, 19);
            lblPageSubtitle.TabIndex = 1;
            lblPageSubtitle.Text = "Manage faculty-to-subject assignments used by the timetable.";
            // 
            // lblPageTitle
            // 
            lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            lblPageTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblPageTitle.Location = new System.Drawing.Point(32, 16);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new System.Drawing.Size(403, 34);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Faculty Assignments Management";
            // 
            // FacultyMemberSubjectsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            ClientSize = new System.Drawing.Size(1180, 720);
            Controls.Add(pnlMain);
            Controls.Add(pnlSidebar);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            MinimumSize = new System.Drawing.Size(980, 600);
            Name = "FacultyMemberSubjectsForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Faculty Assignments Management";
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlWorkspace.ResumeLayout(false);
            pnlAssignmentsTable.ResumeLayout(false);
            pnlAssignmentsTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFacultyMemberSubjects).EndInit();
            pnlAssignmentEditor.ResumeLayout(false);
            pnlAssignmentEditor.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlSidebar;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblApplicationName;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSidebarSubtitle;
        private Guna.UI2.WinForms.Guna2Separator separatorSidebar;
        private Guna.UI2.WinForms.Guna2Button btnNavigationDashboard;
        private Guna.UI2.WinForms.Guna2Button btnNavigationBranches;
        private Guna.UI2.WinForms.Guna2Button btnNavigationStudyYears;
        private Guna.UI2.WinForms.Guna2Button btnNavigationSections;
        private Guna.UI2.WinForms.Guna2Button btnNavigationSubjects;
        private Guna.UI2.WinForms.Guna2Button btnNavigationClassrooms;
        private Guna.UI2.WinForms.Guna2Button btnNavigationTimeSlots;
        private Guna.UI2.WinForms.Guna2Button btnNavigationFacultyAssignments;
        private Guna.UI2.WinForms.Guna2Button btnNavigationFacultyMembers;
        private Guna.UI2.WinForms.Guna2Button btnNavigationSchedules;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSidebarFooter;
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageSubtitle;
        private Guna.UI2.WinForms.Guna2Panel pnlWorkspace;
        private Guna.UI2.WinForms.Guna2Panel pnlAssignmentEditor;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEditorTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEditorSubtitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblFacultyMember;
        private Guna.UI2.WinForms.Guna2ComboBox cmbFacultyMember;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubject;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSubject;
        private Guna.UI2.WinForms.Guna2Button btnAddAssignment;
        private Guna.UI2.WinForms.Guna2Button btnUpdateAssignment;
        private Guna.UI2.WinForms.Guna2Button btnDeleteAssignment;
        private Guna.UI2.WinForms.Guna2Button btnClearAssignmentForm;
        private Guna.UI2.WinForms.Guna2Panel pnlAssignmentsTable;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableSubtitle;
        private Guna.UI2.WinForms.Guna2DataGridView dgvFacultyMemberSubjects;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFacultyMemberId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFacultyMember;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubjectId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubject;
    }
}
