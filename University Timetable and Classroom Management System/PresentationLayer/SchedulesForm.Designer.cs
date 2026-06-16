namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SchedulesForm : System.Windows.Forms.Form
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
            btnNavigationFaculty = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationTimeSlots = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationClassrooms = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationSubjects = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationStudyYears = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationBranches = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationDashboard = new Guna.UI2.WinForms.Guna2Button();
            separatorSidebar = new Guna.UI2.WinForms.Guna2Separator();
            lblSidebarSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblApplicationName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            pnlWorkspace = new Guna.UI2.WinForms.Guna2Panel();
            pnlSchedulesTable = new Guna.UI2.WinForms.Guna2Panel();
            dgvSchedules = new Guna.UI2.WinForms.Guna2DataGridView();
            colScheduleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colDayOfWeek = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSubject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colFacultyMember = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colClassroom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTimeSlot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colStudyYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblTableSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTableTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlScheduleEditor = new Guna.UI2.WinForms.Guna2Panel();
            btnClearScheduleForm = new Guna.UI2.WinForms.Guna2Button();
            btnDeleteSchedule = new Guna.UI2.WinForms.Guna2Button();
            btnUpdateSchedule = new Guna.UI2.WinForms.Guna2Button();
            btnAddSchedule = new Guna.UI2.WinForms.Guna2Button();
            cmbSection = new Guna.UI2.WinForms.Guna2ComboBox();
            lblSection = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbBranch = new Guna.UI2.WinForms.Guna2ComboBox();
            lblBranch = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbStudyYear = new Guna.UI2.WinForms.Guna2ComboBox();
            lblStudyYear = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbTimeSlot = new Guna.UI2.WinForms.Guna2ComboBox();
            lblTimeSlot = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbClassroom = new Guna.UI2.WinForms.Guna2ComboBox();
            lblClassroom = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbFacultyMember = new Guna.UI2.WinForms.Guna2ComboBox();
            lblFacultyMember = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbSubject = new Guna.UI2.WinForms.Guna2ComboBox();
            lblSubject = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbDayOfWeek = new Guna.UI2.WinForms.Guna2ComboBox();
            lblDayOfWeek = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtScheduleId = new Guna.UI2.WinForms.Guna2TextBox();
            lblScheduleId = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEditorSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEditorTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            lblPageSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblPageTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlSidebar.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlWorkspace.SuspendLayout();
            pnlSchedulesTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSchedules).BeginInit();
            pnlScheduleEditor.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = System.Drawing.Color.Transparent;
            pnlSidebar.Controls.Add(lblSidebarFooter);
            pnlSidebar.Controls.Add(btnNavigationSchedules);
            pnlSidebar.Controls.Add(btnNavigationFaculty);
            pnlSidebar.Controls.Add(btnNavigationTimeSlots);
            pnlSidebar.Controls.Add(btnNavigationClassrooms);
            pnlSidebar.Controls.Add(btnNavigationSubjects);
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
            lblSidebarFooter.TabIndex = 11;
            lblSidebarFooter.Text = "Academic Scheduling Suite";
            // 
            // btnNavigationSchedules
            // 
            btnNavigationSchedules.BorderRadius = 8;
            btnNavigationSchedules.Checked = true;
            btnNavigationSchedules.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationSchedules.Enabled = false;
            btnNavigationSchedules.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnNavigationSchedules.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationSchedules.ForeColor = System.Drawing.Color.White;
            btnNavigationSchedules.HoverState.FillColor = System.Drawing.Color.FromArgb(29, 78, 216);
            btnNavigationSchedules.Location = new System.Drawing.Point(24, 490);
            btnNavigationSchedules.Name = "btnNavigationSchedules";
            btnNavigationSchedules.Size = new System.Drawing.Size(192, 44);
            btnNavigationSchedules.TabIndex = 10;
            btnNavigationSchedules.Text = "Schedules";
            btnNavigationSchedules.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            btnNavigationSchedules.TextOffset = new System.Drawing.Point(14, 0);
            // 
            // btnNavigationFaculty
            // 
            btnNavigationFaculty.BorderRadius = 8;
            btnNavigationFaculty.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationFaculty.FillColor = System.Drawing.Color.FromArgb(24, 38, 62);
            btnNavigationFaculty.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationFaculty.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnNavigationFaculty.HoverState.FillColor = System.Drawing.Color.FromArgb(36, 55, 86);
            btnNavigationFaculty.Location = new System.Drawing.Point(24, 434);
            btnNavigationFaculty.Name = "btnNavigationFaculty";
            btnNavigationFaculty.Size = new System.Drawing.Size(192, 44);
            btnNavigationFaculty.TabIndex = 9;
            btnNavigationFaculty.Text = "Faculty Members";
            btnNavigationFaculty.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            btnNavigationFaculty.TextOffset = new System.Drawing.Point(14, 0);
            // 
            // btnNavigationTimeSlots
            // 
            btnNavigationTimeSlots.BorderRadius = 8;
            btnNavigationTimeSlots.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationTimeSlots.FillColor = System.Drawing.Color.FromArgb(24, 38, 62);
            btnNavigationTimeSlots.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationTimeSlots.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnNavigationTimeSlots.HoverState.FillColor = System.Drawing.Color.FromArgb(36, 55, 86);
            btnNavigationTimeSlots.Location = new System.Drawing.Point(24, 378);
            btnNavigationTimeSlots.Name = "btnNavigationTimeSlots";
            btnNavigationTimeSlots.Size = new System.Drawing.Size(192, 44);
            btnNavigationTimeSlots.TabIndex = 8;
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
            btnNavigationClassrooms.Location = new System.Drawing.Point(24, 322);
            btnNavigationClassrooms.Name = "btnNavigationClassrooms";
            btnNavigationClassrooms.Size = new System.Drawing.Size(192, 44);
            btnNavigationClassrooms.TabIndex = 7;
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
            btnNavigationSubjects.Location = new System.Drawing.Point(24, 266);
            btnNavigationSubjects.Name = "btnNavigationSubjects";
            btnNavigationSubjects.Size = new System.Drawing.Size(192, 44);
            btnNavigationSubjects.TabIndex = 6;
            btnNavigationSubjects.Text = "Subjects";
            btnNavigationSubjects.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            btnNavigationSubjects.TextOffset = new System.Drawing.Point(14, 0);
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
            pnlWorkspace.Controls.Add(pnlSchedulesTable);
            pnlWorkspace.Controls.Add(pnlScheduleEditor);
            pnlWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlWorkspace.FillColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlWorkspace.Location = new System.Drawing.Point(0, 88);
            pnlWorkspace.Name = "pnlWorkspace";
            pnlWorkspace.Padding = new System.Windows.Forms.Padding(28, 24, 28, 28);
            pnlWorkspace.Size = new System.Drawing.Size(940, 632);
            pnlWorkspace.TabIndex = 1;
            // 
            // pnlSchedulesTable
            // 
            pnlSchedulesTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlSchedulesTable.BackColor = System.Drawing.Color.Transparent;
            pnlSchedulesTable.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlSchedulesTable.BorderRadius = 8;
            pnlSchedulesTable.BorderThickness = 1;
            pnlSchedulesTable.Controls.Add(dgvSchedules);
            pnlSchedulesTable.Controls.Add(lblTableSubtitle);
            pnlSchedulesTable.Controls.Add(lblTableTitle);
            pnlSchedulesTable.FillColor = System.Drawing.Color.White;
            pnlSchedulesTable.Location = new System.Drawing.Point(28, 336);
            pnlSchedulesTable.Name = "pnlSchedulesTable";
            pnlSchedulesTable.Size = new System.Drawing.Size(884, 268);
            pnlSchedulesTable.TabIndex = 1;
            // 
            // dgvSchedules
            // 
            dgvSchedules.AllowUserToAddRows = false;
            dgvSchedules.AllowUserToDeleteRows = false;
            dgvSchedules.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            dgvSchedules.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvSchedules.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvSchedules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvSchedules.BackgroundColor = System.Drawing.Color.White;
            dgvSchedules.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvSchedules.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSchedules.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvSchedules.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvSchedules.ColumnHeadersHeight = 44;
            dgvSchedules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvSchedules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colScheduleId, colDayOfWeek, colSubject, colFacultyMember, colClassroom, colTimeSlot, colStudyYear, colBranch, colSection });
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvSchedules.DefaultCellStyle = dataGridViewCellStyle3;
            dgvSchedules.EnableHeadersVisualStyles = false;
            dgvSchedules.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgvSchedules.Location = new System.Drawing.Point(24, 78);
            dgvSchedules.MultiSelect = false;
            dgvSchedules.Name = "dgvSchedules";
            dgvSchedules.ReadOnly = true;
            dgvSchedules.RowHeadersVisible = false;
            dgvSchedules.RowTemplate.Height = 42;
            dgvSchedules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvSchedules.Size = new System.Drawing.Size(836, 166);
            dgvSchedules.TabIndex = 2;
            // 
            // colScheduleId
            // 
            colScheduleId.DataPropertyName = "ScheduleID";
            colScheduleId.FillWeight = 38F;
            colScheduleId.HeaderText = "ID";
            colScheduleId.Name = "colScheduleId";
            colScheduleId.ReadOnly = true;
            // 
            // colDayOfWeek
            // 
            colDayOfWeek.DataPropertyName = "DayOfWeek";
            colDayOfWeek.FillWeight = 65F;
            colDayOfWeek.HeaderText = "Day";
            colDayOfWeek.Name = "colDayOfWeek";
            colDayOfWeek.ReadOnly = true;
            // 
            // colSubject
            // 
            colSubject.DataPropertyName = "SubjectID";
            colSubject.FillWeight = 100F;
            colSubject.HeaderText = "Subject";
            colSubject.Name = "colSubject";
            colSubject.ReadOnly = true;
            // 
            // colFacultyMember
            // 
            colFacultyMember.DataPropertyName = "FacultyMemberID";
            colFacultyMember.FillWeight = 100F;
            colFacultyMember.HeaderText = "Faculty";
            colFacultyMember.Name = "colFacultyMember";
            colFacultyMember.ReadOnly = true;
            // 
            // colClassroom
            // 
            colClassroom.DataPropertyName = "ClassroomID";
            colClassroom.FillWeight = 68F;
            colClassroom.HeaderText = "Room";
            colClassroom.Name = "colClassroom";
            colClassroom.ReadOnly = true;
            // 
            // colTimeSlot
            // 
            colTimeSlot.DataPropertyName = "TimeSlotID";
            colTimeSlot.FillWeight = 75F;
            colTimeSlot.HeaderText = "Time";
            colTimeSlot.Name = "colTimeSlot";
            colTimeSlot.ReadOnly = true;
            // 
            // colStudyYear
            // 
            colStudyYear.DataPropertyName = "StudyYearID";
            colStudyYear.FillWeight = 72F;
            colStudyYear.HeaderText = "Year";
            colStudyYear.Name = "colStudyYear";
            colStudyYear.ReadOnly = true;
            // 
            // colBranch
            // 
            colBranch.DataPropertyName = "BranchID";
            colBranch.FillWeight = 72F;
            colBranch.HeaderText = "Branch";
            colBranch.Name = "colBranch";
            colBranch.ReadOnly = true;
            // 
            // colSection
            // 
            colSection.DataPropertyName = "SectionID";
            colSection.FillWeight = 72F;
            colSection.HeaderText = "Section";
            colSection.Name = "colSection";
            colSection.ReadOnly = true;
            // 
            // lblTableSubtitle
            // 
            lblTableSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblTableSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblTableSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTableSubtitle.Location = new System.Drawing.Point(24, 43);
            lblTableSubtitle.Name = "lblTableSubtitle";
            lblTableSubtitle.Size = new System.Drawing.Size(286, 17);
            lblTableSubtitle.TabIndex = 1;
            lblTableSubtitle.Text = "Review and select timetable session records.";
            // 
            // lblTableTitle
            // 
            lblTableTitle.BackColor = System.Drawing.Color.Transparent;
            lblTableTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTableTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblTableTitle.Location = new System.Drawing.Point(24, 18);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new System.Drawing.Size(113, 25);
            lblTableTitle.TabIndex = 0;
            lblTableTitle.Text = "Schedules List";
            // 
            // pnlScheduleEditor
            // 
            pnlScheduleEditor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlScheduleEditor.BackColor = System.Drawing.Color.Transparent;
            pnlScheduleEditor.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlScheduleEditor.BorderRadius = 8;
            pnlScheduleEditor.BorderThickness = 1;
            pnlScheduleEditor.Controls.Add(btnClearScheduleForm);
            pnlScheduleEditor.Controls.Add(btnDeleteSchedule);
            pnlScheduleEditor.Controls.Add(btnUpdateSchedule);
            pnlScheduleEditor.Controls.Add(btnAddSchedule);
            pnlScheduleEditor.Controls.Add(cmbSection);
            pnlScheduleEditor.Controls.Add(lblSection);
            pnlScheduleEditor.Controls.Add(cmbBranch);
            pnlScheduleEditor.Controls.Add(lblBranch);
            pnlScheduleEditor.Controls.Add(cmbStudyYear);
            pnlScheduleEditor.Controls.Add(lblStudyYear);
            pnlScheduleEditor.Controls.Add(cmbTimeSlot);
            pnlScheduleEditor.Controls.Add(lblTimeSlot);
            pnlScheduleEditor.Controls.Add(cmbClassroom);
            pnlScheduleEditor.Controls.Add(lblClassroom);
            pnlScheduleEditor.Controls.Add(cmbFacultyMember);
            pnlScheduleEditor.Controls.Add(lblFacultyMember);
            pnlScheduleEditor.Controls.Add(cmbSubject);
            pnlScheduleEditor.Controls.Add(lblSubject);
            pnlScheduleEditor.Controls.Add(cmbDayOfWeek);
            pnlScheduleEditor.Controls.Add(lblDayOfWeek);
            pnlScheduleEditor.Controls.Add(txtScheduleId);
            pnlScheduleEditor.Controls.Add(lblScheduleId);
            pnlScheduleEditor.Controls.Add(lblEditorSubtitle);
            pnlScheduleEditor.Controls.Add(lblEditorTitle);
            pnlScheduleEditor.FillColor = System.Drawing.Color.White;
            pnlScheduleEditor.Location = new System.Drawing.Point(28, 24);
            pnlScheduleEditor.Name = "pnlScheduleEditor";
            pnlScheduleEditor.Size = new System.Drawing.Size(884, 290);
            pnlScheduleEditor.TabIndex = 0;
            // 
            // btnClearScheduleForm
            // 
            btnClearScheduleForm.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClearScheduleForm.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnClearScheduleForm.BorderRadius = 8;
            btnClearScheduleForm.BorderThickness = 1;
            btnClearScheduleForm.Cursor = System.Windows.Forms.Cursors.Hand;
            btnClearScheduleForm.FillColor = System.Drawing.Color.White;
            btnClearScheduleForm.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnClearScheduleForm.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            btnClearScheduleForm.HoverState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            btnClearScheduleForm.Location = new System.Drawing.Point(752, 142);
            btnClearScheduleForm.Name = "btnClearScheduleForm";
            btnClearScheduleForm.Size = new System.Drawing.Size(108, 38);
            btnClearScheduleForm.TabIndex = 23;
            btnClearScheduleForm.Text = "Clear";
            // 
            // btnDeleteSchedule
            // 
            btnDeleteSchedule.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnDeleteSchedule.BorderRadius = 8;
            btnDeleteSchedule.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDeleteSchedule.FillColor = System.Drawing.Color.FromArgb(220, 38, 38);
            btnDeleteSchedule.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnDeleteSchedule.ForeColor = System.Drawing.Color.White;
            btnDeleteSchedule.HoverState.FillColor = System.Drawing.Color.FromArgb(185, 28, 28);
            btnDeleteSchedule.Location = new System.Drawing.Point(632, 142);
            btnDeleteSchedule.Name = "btnDeleteSchedule";
            btnDeleteSchedule.Size = new System.Drawing.Size(108, 38);
            btnDeleteSchedule.TabIndex = 22;
            btnDeleteSchedule.Text = "Delete";
            // 
            // btnUpdateSchedule
            // 
            btnUpdateSchedule.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnUpdateSchedule.BorderRadius = 8;
            btnUpdateSchedule.Cursor = System.Windows.Forms.Cursors.Hand;
            btnUpdateSchedule.FillColor = System.Drawing.Color.FromArgb(14, 116, 144);
            btnUpdateSchedule.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnUpdateSchedule.ForeColor = System.Drawing.Color.White;
            btnUpdateSchedule.HoverState.FillColor = System.Drawing.Color.FromArgb(21, 94, 117);
            btnUpdateSchedule.Location = new System.Drawing.Point(752, 98);
            btnUpdateSchedule.Name = "btnUpdateSchedule";
            btnUpdateSchedule.Size = new System.Drawing.Size(108, 38);
            btnUpdateSchedule.TabIndex = 21;
            btnUpdateSchedule.Text = "Update";
            // 
            // btnAddSchedule
            // 
            btnAddSchedule.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAddSchedule.BorderRadius = 8;
            btnAddSchedule.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAddSchedule.FillColor = System.Drawing.Color.FromArgb(22, 163, 74);
            btnAddSchedule.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnAddSchedule.ForeColor = System.Drawing.Color.White;
            btnAddSchedule.HoverState.FillColor = System.Drawing.Color.FromArgb(21, 128, 61);
            btnAddSchedule.Location = new System.Drawing.Point(632, 98);
            btnAddSchedule.Name = "btnAddSchedule";
            btnAddSchedule.Size = new System.Drawing.Size(108, 38);
            btnAddSchedule.TabIndex = 20;
            btnAddSchedule.Text = "Add";
            // 
            // cmbSection
            // 
            cmbSection.BackColor = System.Drawing.Color.Transparent;
            cmbSection.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            cmbSection.BorderRadius = 8;
            cmbSection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbSection.FocusedColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbSection.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbSection.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cmbSection.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            cmbSection.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            cmbSection.ItemHeight = 36;
            cmbSection.Location = new System.Drawing.Point(402, 224);
            cmbSection.Name = "cmbSection";
            cmbSection.Size = new System.Drawing.Size(190, 42);
            cmbSection.TabIndex = 19;
            // 
            // lblSection
            // 
            lblSection.BackColor = System.Drawing.Color.Transparent;
            lblSection.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblSection.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblSection.Location = new System.Drawing.Point(402, 199);
            lblSection.Name = "lblSection";
            lblSection.Size = new System.Drawing.Size(46, 19);
            lblSection.TabIndex = 18;
            lblSection.Text = "Section";
            // 
            // cmbBranch
            // 
            cmbBranch.BackColor = System.Drawing.Color.Transparent;
            cmbBranch.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            cmbBranch.BorderRadius = 8;
            cmbBranch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbBranch.FocusedColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbBranch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbBranch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cmbBranch.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            cmbBranch.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            cmbBranch.ItemHeight = 36;
            cmbBranch.Location = new System.Drawing.Point(214, 224);
            cmbBranch.Name = "cmbBranch";
            cmbBranch.Size = new System.Drawing.Size(160, 42);
            cmbBranch.TabIndex = 17;
            // 
            // lblBranch
            // 
            lblBranch.BackColor = System.Drawing.Color.Transparent;
            lblBranch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblBranch.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblBranch.Location = new System.Drawing.Point(214, 199);
            lblBranch.Name = "lblBranch";
            lblBranch.Size = new System.Drawing.Size(45, 19);
            lblBranch.TabIndex = 16;
            lblBranch.Text = "Branch";
            // 
            // cmbStudyYear
            // 
            cmbStudyYear.BackColor = System.Drawing.Color.Transparent;
            cmbStudyYear.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            cmbStudyYear.BorderRadius = 8;
            cmbStudyYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbStudyYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbStudyYear.FocusedColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbStudyYear.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbStudyYear.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cmbStudyYear.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            cmbStudyYear.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            cmbStudyYear.ItemHeight = 36;
            cmbStudyYear.Location = new System.Drawing.Point(24, 224);
            cmbStudyYear.Name = "cmbStudyYear";
            cmbStudyYear.Size = new System.Drawing.Size(160, 42);
            cmbStudyYear.TabIndex = 15;
            // 
            // lblStudyYear
            // 
            lblStudyYear.BackColor = System.Drawing.Color.Transparent;
            lblStudyYear.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblStudyYear.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblStudyYear.Location = new System.Drawing.Point(24, 199);
            lblStudyYear.Name = "lblStudyYear";
            lblStudyYear.Size = new System.Drawing.Size(69, 19);
            lblStudyYear.TabIndex = 14;
            lblStudyYear.Text = "Study Year";
            // 
            // cmbTimeSlot
            // 
            cmbTimeSlot.BackColor = System.Drawing.Color.Transparent;
            cmbTimeSlot.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            cmbTimeSlot.BorderRadius = 8;
            cmbTimeSlot.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbTimeSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbTimeSlot.FocusedColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbTimeSlot.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbTimeSlot.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cmbTimeSlot.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            cmbTimeSlot.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            cmbTimeSlot.ItemHeight = 36;
            cmbTimeSlot.Location = new System.Drawing.Point(402, 154);
            cmbTimeSlot.Name = "cmbTimeSlot";
            cmbTimeSlot.Size = new System.Drawing.Size(190, 42);
            cmbTimeSlot.TabIndex = 13;
            // 
            // lblTimeSlot
            // 
            lblTimeSlot.BackColor = System.Drawing.Color.Transparent;
            lblTimeSlot.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTimeSlot.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblTimeSlot.Location = new System.Drawing.Point(402, 129);
            lblTimeSlot.Name = "lblTimeSlot";
            lblTimeSlot.Size = new System.Drawing.Size(57, 19);
            lblTimeSlot.TabIndex = 12;
            lblTimeSlot.Text = "Time Slot";
            // 
            // cmbClassroom
            // 
            cmbClassroom.BackColor = System.Drawing.Color.Transparent;
            cmbClassroom.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            cmbClassroom.BorderRadius = 8;
            cmbClassroom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbClassroom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbClassroom.FocusedColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbClassroom.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbClassroom.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cmbClassroom.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            cmbClassroom.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            cmbClassroom.ItemHeight = 36;
            cmbClassroom.Location = new System.Drawing.Point(214, 154);
            cmbClassroom.Name = "cmbClassroom";
            cmbClassroom.Size = new System.Drawing.Size(160, 42);
            cmbClassroom.TabIndex = 11;
            // 
            // lblClassroom
            // 
            lblClassroom.BackColor = System.Drawing.Color.Transparent;
            lblClassroom.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblClassroom.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblClassroom.Location = new System.Drawing.Point(214, 129);
            lblClassroom.Name = "lblClassroom";
            lblClassroom.Size = new System.Drawing.Size(67, 19);
            lblClassroom.TabIndex = 10;
            lblClassroom.Text = "Classroom";
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
            cmbFacultyMember.Location = new System.Drawing.Point(24, 154);
            cmbFacultyMember.Name = "cmbFacultyMember";
            cmbFacultyMember.Size = new System.Drawing.Size(160, 42);
            cmbFacultyMember.TabIndex = 9;
            // 
            // lblFacultyMember
            // 
            lblFacultyMember.BackColor = System.Drawing.Color.Transparent;
            lblFacultyMember.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblFacultyMember.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblFacultyMember.Location = new System.Drawing.Point(24, 129);
            lblFacultyMember.Name = "lblFacultyMember";
            lblFacultyMember.Size = new System.Drawing.Size(101, 19);
            lblFacultyMember.TabIndex = 8;
            lblFacultyMember.Text = "Faculty Member";
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
            cmbSubject.Location = new System.Drawing.Point(402, 84);
            cmbSubject.Name = "cmbSubject";
            cmbSubject.Size = new System.Drawing.Size(190, 42);
            cmbSubject.TabIndex = 7;
            // 
            // lblSubject
            // 
            lblSubject.BackColor = System.Drawing.Color.Transparent;
            lblSubject.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblSubject.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblSubject.Location = new System.Drawing.Point(402, 59);
            lblSubject.Name = "lblSubject";
            lblSubject.Size = new System.Drawing.Size(47, 19);
            lblSubject.TabIndex = 6;
            lblSubject.Text = "Subject";
            // 
            // cmbDayOfWeek
            // 
            cmbDayOfWeek.BackColor = System.Drawing.Color.Transparent;
            cmbDayOfWeek.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            cmbDayOfWeek.BorderRadius = 8;
            cmbDayOfWeek.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbDayOfWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbDayOfWeek.FocusedColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbDayOfWeek.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbDayOfWeek.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cmbDayOfWeek.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            cmbDayOfWeek.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            cmbDayOfWeek.ItemHeight = 36;
            cmbDayOfWeek.Items.AddRange(new object[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday" });
            cmbDayOfWeek.Location = new System.Drawing.Point(184, 84);
            cmbDayOfWeek.Name = "cmbDayOfWeek";
            cmbDayOfWeek.Size = new System.Drawing.Size(190, 42);
            cmbDayOfWeek.TabIndex = 5;
            // 
            // lblDayOfWeek
            // 
            lblDayOfWeek.BackColor = System.Drawing.Color.Transparent;
            lblDayOfWeek.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblDayOfWeek.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblDayOfWeek.Location = new System.Drawing.Point(184, 59);
            lblDayOfWeek.Name = "lblDayOfWeek";
            lblDayOfWeek.Size = new System.Drawing.Size(78, 19);
            lblDayOfWeek.TabIndex = 4;
            lblDayOfWeek.Text = "Day Of Week";
            // 
            // txtScheduleId
            // 
            txtScheduleId.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtScheduleId.BorderRadius = 8;
            txtScheduleId.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtScheduleId.DefaultText = "";
            txtScheduleId.DisabledState.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtScheduleId.DisabledState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtScheduleId.DisabledState.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtScheduleId.Enabled = false;
            txtScheduleId.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtScheduleId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtScheduleId.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtScheduleId.Location = new System.Drawing.Point(24, 84);
            txtScheduleId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtScheduleId.Name = "txtScheduleId";
            txtScheduleId.PasswordChar = '\0';
            txtScheduleId.PlaceholderForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            txtScheduleId.PlaceholderText = "Auto";
            txtScheduleId.SelectedText = "";
            txtScheduleId.Size = new System.Drawing.Size(132, 42);
            txtScheduleId.TabIndex = 3;
            // 
            // lblScheduleId
            // 
            lblScheduleId.BackColor = System.Drawing.Color.Transparent;
            lblScheduleId.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblScheduleId.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblScheduleId.Location = new System.Drawing.Point(24, 59);
            lblScheduleId.Name = "lblScheduleId";
            lblScheduleId.Size = new System.Drawing.Size(74, 19);
            lblScheduleId.TabIndex = 2;
            lblScheduleId.Text = "Schedule ID";
            // 
            // lblEditorSubtitle
            // 
            lblEditorSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblEditorSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblEditorSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblEditorSubtitle.Location = new System.Drawing.Point(24, 34);
            lblEditorSubtitle.Name = "lblEditorSubtitle";
            lblEditorSubtitle.Size = new System.Drawing.Size(316, 17);
            lblEditorSubtitle.TabIndex = 1;
            lblEditorSubtitle.Text = "Prepare schedule session details before applying an action.";
            // 
            // lblEditorTitle
            // 
            lblEditorTitle.BackColor = System.Drawing.Color.Transparent;
            lblEditorTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblEditorTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblEditorTitle.Location = new System.Drawing.Point(24, 9);
            lblEditorTitle.Name = "lblEditorTitle";
            lblEditorTitle.Size = new System.Drawing.Size(135, 25);
            lblEditorTitle.TabIndex = 0;
            lblEditorTitle.Text = "Schedule Details";
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
            lblPageSubtitle.Size = new System.Drawing.Size(386, 19);
            lblPageSubtitle.TabIndex = 1;
            lblPageSubtitle.Text = "Manage final timetable sessions by day, room, instructor, and time.";
            // 
            // lblPageTitle
            // 
            lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            lblPageTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblPageTitle.Location = new System.Drawing.Point(32, 16);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new System.Drawing.Size(282, 34);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Schedules Management";
            // 
            // SchedulesForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            ClientSize = new System.Drawing.Size(1180, 720);
            Controls.Add(pnlMain);
            Controls.Add(pnlSidebar);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            MinimumSize = new System.Drawing.Size(980, 600);
            Name = "SchedulesForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Schedules Management";
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlWorkspace.ResumeLayout(false);
            pnlSchedulesTable.ResumeLayout(false);
            pnlSchedulesTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSchedules).EndInit();
            pnlScheduleEditor.ResumeLayout(false);
            pnlScheduleEditor.PerformLayout();
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
        private Guna.UI2.WinForms.Guna2Button btnNavigationSubjects;
        private Guna.UI2.WinForms.Guna2Button btnNavigationClassrooms;
        private Guna.UI2.WinForms.Guna2Button btnNavigationTimeSlots;
        private Guna.UI2.WinForms.Guna2Button btnNavigationFaculty;
        private Guna.UI2.WinForms.Guna2Button btnNavigationSchedules;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSidebarFooter;
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageSubtitle;
        private Guna.UI2.WinForms.Guna2Panel pnlWorkspace;
        private Guna.UI2.WinForms.Guna2Panel pnlScheduleEditor;
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
        private Guna.UI2.WinForms.Guna2Button btnAddSchedule;
        private Guna.UI2.WinForms.Guna2Button btnUpdateSchedule;
        private Guna.UI2.WinForms.Guna2Button btnDeleteSchedule;
        private Guna.UI2.WinForms.Guna2Button btnClearScheduleForm;
        private Guna.UI2.WinForms.Guna2Panel pnlSchedulesTable;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableSubtitle;
        private Guna.UI2.WinForms.Guna2DataGridView dgvSchedules;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScheduleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDayOfWeek;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubject;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFacultyMember;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClassroom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeSlot;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudyYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSection;
    }
}
