namespace University_Timetable_and_Classroom_Management_System
{
    public partial class ClassroomsForm : System.Windows.Forms.Form
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
            pnlClassroomsTable = new Guna.UI2.WinForms.Guna2Panel();
            dgvClassrooms = new Guna.UI2.WinForms.Guna2DataGridView();
            colClassroomId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colClassroomNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colCapacity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colRoomType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblTableSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTableTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlClassroomEditor = new Guna.UI2.WinForms.Guna2Panel();
            btnClearClassroomForm = new Guna.UI2.WinForms.Guna2Button();
            btnDeleteClassroom = new Guna.UI2.WinForms.Guna2Button();
            btnUpdateClassroom = new Guna.UI2.WinForms.Guna2Button();
            btnAddClassroom = new Guna.UI2.WinForms.Guna2Button();
            cmbRoomType = new Guna.UI2.WinForms.Guna2ComboBox();
            lblRoomType = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtCapacity = new Guna.UI2.WinForms.Guna2TextBox();
            lblCapacity = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtClassroomNumber = new Guna.UI2.WinForms.Guna2TextBox();
            lblClassroomNumber = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtClassroomId = new Guna.UI2.WinForms.Guna2TextBox();
            lblClassroomId = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEditorSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEditorTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            lblPageSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblPageTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlSidebar.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlWorkspace.SuspendLayout();
            pnlClassroomsTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClassrooms).BeginInit();
            pnlClassroomEditor.SuspendLayout();
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
            btnNavigationSchedules.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationSchedules.FillColor = System.Drawing.Color.FromArgb(24, 38, 62);
            btnNavigationSchedules.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationSchedules.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnNavigationSchedules.HoverState.FillColor = System.Drawing.Color.FromArgb(36, 55, 86);
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
            btnNavigationClassrooms.Checked = true;
            btnNavigationClassrooms.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationClassrooms.Enabled = false;
            btnNavigationClassrooms.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnNavigationClassrooms.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationClassrooms.ForeColor = System.Drawing.Color.White;
            btnNavigationClassrooms.HoverState.FillColor = System.Drawing.Color.FromArgb(29, 78, 216);
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
            pnlWorkspace.Controls.Add(pnlClassroomsTable);
            pnlWorkspace.Controls.Add(pnlClassroomEditor);
            pnlWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlWorkspace.FillColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlWorkspace.Location = new System.Drawing.Point(0, 88);
            pnlWorkspace.Name = "pnlWorkspace";
            pnlWorkspace.Padding = new System.Windows.Forms.Padding(28, 24, 28, 28);
            pnlWorkspace.Size = new System.Drawing.Size(940, 632);
            pnlWorkspace.TabIndex = 1;
            // 
            // pnlClassroomsTable
            // 
            pnlClassroomsTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlClassroomsTable.BackColor = System.Drawing.Color.Transparent;
            pnlClassroomsTable.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlClassroomsTable.BorderRadius = 8;
            pnlClassroomsTable.BorderThickness = 1;
            pnlClassroomsTable.Controls.Add(dgvClassrooms);
            pnlClassroomsTable.Controls.Add(lblTableSubtitle);
            pnlClassroomsTable.Controls.Add(lblTableTitle);
            pnlClassroomsTable.FillColor = System.Drawing.Color.White;
            pnlClassroomsTable.Location = new System.Drawing.Point(28, 248);
            pnlClassroomsTable.Name = "pnlClassroomsTable";
            pnlClassroomsTable.Size = new System.Drawing.Size(884, 356);
            pnlClassroomsTable.TabIndex = 1;
            // 
            // dgvClassrooms
            // 
            dgvClassrooms.AllowUserToAddRows = false;
            dgvClassrooms.AllowUserToDeleteRows = false;
            dgvClassrooms.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            dgvClassrooms.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvClassrooms.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvClassrooms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvClassrooms.BackgroundColor = System.Drawing.Color.White;
            dgvClassrooms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvClassrooms.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvClassrooms.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvClassrooms.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvClassrooms.ColumnHeadersHeight = 44;
            dgvClassrooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvClassrooms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colClassroomId, colClassroomNumber, colCapacity, colRoomType });
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvClassrooms.DefaultCellStyle = dataGridViewCellStyle3;
            dgvClassrooms.EnableHeadersVisualStyles = false;
            dgvClassrooms.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgvClassrooms.Location = new System.Drawing.Point(24, 78);
            dgvClassrooms.MultiSelect = false;
            dgvClassrooms.Name = "dgvClassrooms";
            dgvClassrooms.ReadOnly = true;
            dgvClassrooms.RowHeadersVisible = false;
            dgvClassrooms.RowTemplate.Height = 42;
            dgvClassrooms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvClassrooms.Size = new System.Drawing.Size(836, 254);
            dgvClassrooms.TabIndex = 2;
            // 
            // colClassroomId
            // 
            colClassroomId.DataPropertyName = "ClassroomID";
            colClassroomId.FillWeight = 45F;
            colClassroomId.HeaderText = "Classroom ID";
            colClassroomId.Name = "colClassroomId";
            colClassroomId.ReadOnly = true;
            // 
            // colClassroomNumber
            // 
            colClassroomNumber.DataPropertyName = "ClassroomNumber";
            colClassroomNumber.FillWeight = 120F;
            colClassroomNumber.HeaderText = "Classroom Number";
            colClassroomNumber.Name = "colClassroomNumber";
            colClassroomNumber.ReadOnly = true;
            // 
            // colCapacity
            // 
            colCapacity.DataPropertyName = "Capacity";
            colCapacity.FillWeight = 65F;
            colCapacity.HeaderText = "Capacity";
            colCapacity.Name = "colCapacity";
            colCapacity.ReadOnly = true;
            // 
            // colRoomType
            // 
            colRoomType.DataPropertyName = "RoomType";
            colRoomType.FillWeight = 95F;
            colRoomType.HeaderText = "Room Type";
            colRoomType.Name = "colRoomType";
            colRoomType.ReadOnly = true;
            // 
            // lblTableSubtitle
            // 
            lblTableSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblTableSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblTableSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTableSubtitle.Location = new System.Drawing.Point(24, 43);
            lblTableSubtitle.Name = "lblTableSubtitle";
            lblTableSubtitle.Size = new System.Drawing.Size(272, 17);
            lblTableSubtitle.TabIndex = 1;
            lblTableSubtitle.Text = "Review and select classroom capacity records.";
            // 
            // lblTableTitle
            // 
            lblTableTitle.BackColor = System.Drawing.Color.Transparent;
            lblTableTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTableTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblTableTitle.Location = new System.Drawing.Point(24, 18);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new System.Drawing.Size(123, 25);
            lblTableTitle.TabIndex = 0;
            lblTableTitle.Text = "Classrooms List";
            // 
            // pnlClassroomEditor
            // 
            pnlClassroomEditor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlClassroomEditor.BackColor = System.Drawing.Color.Transparent;
            pnlClassroomEditor.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlClassroomEditor.BorderRadius = 8;
            pnlClassroomEditor.BorderThickness = 1;
            pnlClassroomEditor.Controls.Add(btnClearClassroomForm);
            pnlClassroomEditor.Controls.Add(btnDeleteClassroom);
            pnlClassroomEditor.Controls.Add(btnUpdateClassroom);
            pnlClassroomEditor.Controls.Add(btnAddClassroom);
            pnlClassroomEditor.Controls.Add(cmbRoomType);
            pnlClassroomEditor.Controls.Add(lblRoomType);
            pnlClassroomEditor.Controls.Add(txtCapacity);
            pnlClassroomEditor.Controls.Add(lblCapacity);
            pnlClassroomEditor.Controls.Add(txtClassroomNumber);
            pnlClassroomEditor.Controls.Add(lblClassroomNumber);
            pnlClassroomEditor.Controls.Add(txtClassroomId);
            pnlClassroomEditor.Controls.Add(lblClassroomId);
            pnlClassroomEditor.Controls.Add(lblEditorSubtitle);
            pnlClassroomEditor.Controls.Add(lblEditorTitle);
            pnlClassroomEditor.FillColor = System.Drawing.Color.White;
            pnlClassroomEditor.Location = new System.Drawing.Point(28, 24);
            pnlClassroomEditor.Name = "pnlClassroomEditor";
            pnlClassroomEditor.Size = new System.Drawing.Size(884, 202);
            pnlClassroomEditor.TabIndex = 0;
            // 
            // btnClearClassroomForm
            // 
            btnClearClassroomForm.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClearClassroomForm.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnClearClassroomForm.BorderRadius = 8;
            btnClearClassroomForm.BorderThickness = 1;
            btnClearClassroomForm.Cursor = System.Windows.Forms.Cursors.Hand;
            btnClearClassroomForm.FillColor = System.Drawing.Color.White;
            btnClearClassroomForm.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnClearClassroomForm.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            btnClearClassroomForm.HoverState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            btnClearClassroomForm.Location = new System.Drawing.Point(752, 142);
            btnClearClassroomForm.Name = "btnClearClassroomForm";
            btnClearClassroomForm.Size = new System.Drawing.Size(108, 38);
            btnClearClassroomForm.TabIndex = 13;
            btnClearClassroomForm.Text = "Clear";
            btnClearClassroomForm.Visible = false;
            // 
            // btnDeleteClassroom
            // 
            btnDeleteClassroom.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnDeleteClassroom.BorderRadius = 8;
            btnDeleteClassroom.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDeleteClassroom.FillColor = System.Drawing.Color.FromArgb(220, 38, 38);
            btnDeleteClassroom.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnDeleteClassroom.ForeColor = System.Drawing.Color.White;
            btnDeleteClassroom.HoverState.FillColor = System.Drawing.Color.FromArgb(185, 28, 28);
            btnDeleteClassroom.Location = new System.Drawing.Point(632, 142);
            btnDeleteClassroom.Name = "btnDeleteClassroom";
            btnDeleteClassroom.Size = new System.Drawing.Size(108, 38);
            btnDeleteClassroom.TabIndex = 12;
            btnDeleteClassroom.Text = "Delete";
            // 
            // btnUpdateClassroom
            // 
            btnUpdateClassroom.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnUpdateClassroom.BorderRadius = 8;
            btnUpdateClassroom.Cursor = System.Windows.Forms.Cursors.Hand;
            btnUpdateClassroom.FillColor = System.Drawing.Color.FromArgb(14, 116, 144);
            btnUpdateClassroom.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnUpdateClassroom.ForeColor = System.Drawing.Color.White;
            btnUpdateClassroom.HoverState.FillColor = System.Drawing.Color.FromArgb(21, 94, 117);
            btnUpdateClassroom.Location = new System.Drawing.Point(752, 98);
            btnUpdateClassroom.Name = "btnUpdateClassroom";
            btnUpdateClassroom.Size = new System.Drawing.Size(108, 38);
            btnUpdateClassroom.TabIndex = 11;
            btnUpdateClassroom.Text = "Update";
            // 
            // btnAddClassroom
            // 
            btnAddClassroom.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAddClassroom.BorderRadius = 8;
            btnAddClassroom.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAddClassroom.FillColor = System.Drawing.Color.FromArgb(22, 163, 74);
            btnAddClassroom.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnAddClassroom.ForeColor = System.Drawing.Color.White;
            btnAddClassroom.HoverState.FillColor = System.Drawing.Color.FromArgb(21, 128, 61);
            btnAddClassroom.Location = new System.Drawing.Point(632, 98);
            btnAddClassroom.Name = "btnAddClassroom";
            btnAddClassroom.Size = new System.Drawing.Size(108, 38);
            btnAddClassroom.TabIndex = 10;
            btnAddClassroom.Text = "Add";
            // 
            // cmbRoomType
            // 
            cmbRoomType.BackColor = System.Drawing.Color.Transparent;
            cmbRoomType.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            cmbRoomType.BorderRadius = 8;
            cmbRoomType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbRoomType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbRoomType.FocusedColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbRoomType.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbRoomType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cmbRoomType.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            cmbRoomType.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            cmbRoomType.ItemHeight = 36;
            cmbRoomType.Items.AddRange(new object[] { "Lecture Hall", "Laboratory", "Computer Lab", "Seminar Room" });
            cmbRoomType.Location = new System.Drawing.Point(402, 112);
            cmbRoomType.Name = "cmbRoomType";
            cmbRoomType.Size = new System.Drawing.Size(190, 42);
            cmbRoomType.TabIndex = 9;
            // 
            // lblRoomType
            // 
            lblRoomType.BackColor = System.Drawing.Color.Transparent;
            lblRoomType.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblRoomType.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblRoomType.Location = new System.Drawing.Point(402, 87);
            lblRoomType.Name = "lblRoomType";
            lblRoomType.Size = new System.Drawing.Size(69, 19);
            lblRoomType.TabIndex = 8;
            lblRoomType.Text = "Room Type";
            // 
            // txtCapacity
            // 
            txtCapacity.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            txtCapacity.BorderRadius = 8;
            txtCapacity.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtCapacity.DefaultText = "";
            txtCapacity.DisabledState.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtCapacity.DisabledState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtCapacity.DisabledState.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtCapacity.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            txtCapacity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtCapacity.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            txtCapacity.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            txtCapacity.Location = new System.Drawing.Point(270, 112);
            txtCapacity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtCapacity.Name = "txtCapacity";
            txtCapacity.PasswordChar = '\0';
            txtCapacity.PlaceholderForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            txtCapacity.PlaceholderText = "Capacity";
            txtCapacity.SelectedText = "";
            txtCapacity.Size = new System.Drawing.Size(104, 42);
            txtCapacity.TabIndex = 7;
            // 
            // lblCapacity
            // 
            lblCapacity.BackColor = System.Drawing.Color.Transparent;
            lblCapacity.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblCapacity.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblCapacity.Location = new System.Drawing.Point(270, 87);
            lblCapacity.Name = "lblCapacity";
            lblCapacity.Size = new System.Drawing.Size(55, 19);
            lblCapacity.TabIndex = 6;
            lblCapacity.Text = "Capacity";
            // 
            // txtClassroomNumber
            // 
            txtClassroomNumber.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            txtClassroomNumber.BorderRadius = 8;
            txtClassroomNumber.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtClassroomNumber.DefaultText = "";
            txtClassroomNumber.DisabledState.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtClassroomNumber.DisabledState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtClassroomNumber.DisabledState.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtClassroomNumber.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            txtClassroomNumber.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtClassroomNumber.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            txtClassroomNumber.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            txtClassroomNumber.Location = new System.Drawing.Point(184, 112);
            txtClassroomNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtClassroomNumber.Name = "txtClassroomNumber";
            txtClassroomNumber.PasswordChar = '\0';
            txtClassroomNumber.PlaceholderForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            txtClassroomNumber.PlaceholderText = "Room No.";
            txtClassroomNumber.SelectedText = "";
            txtClassroomNumber.Size = new System.Drawing.Size(60, 42);
            txtClassroomNumber.TabIndex = 5;
            // 
            // lblClassroomNumber
            // 
            lblClassroomNumber.BackColor = System.Drawing.Color.Transparent;
            lblClassroomNumber.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblClassroomNumber.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblClassroomNumber.Location = new System.Drawing.Point(184, 87);
            lblClassroomNumber.Name = "lblClassroomNumber";
            lblClassroomNumber.Size = new System.Drawing.Size(51, 19);
            lblClassroomNumber.TabIndex = 4;
            lblClassroomNumber.Text = "Room #";
            // 
            // txtClassroomId
            // 
            txtClassroomId.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtClassroomId.BorderRadius = 8;
            txtClassroomId.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtClassroomId.DefaultText = "";
            txtClassroomId.DisabledState.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtClassroomId.DisabledState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtClassroomId.DisabledState.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtClassroomId.Enabled = false;
            txtClassroomId.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtClassroomId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtClassroomId.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtClassroomId.Location = new System.Drawing.Point(24, 112);
            txtClassroomId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtClassroomId.Name = "txtClassroomId";
            txtClassroomId.PasswordChar = '\0';
            txtClassroomId.PlaceholderForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            txtClassroomId.PlaceholderText = "Auto";
            txtClassroomId.SelectedText = "";
            txtClassroomId.Size = new System.Drawing.Size(132, 42);
            txtClassroomId.TabIndex = 3;
            // 
            // lblClassroomId
            // 
            lblClassroomId.BackColor = System.Drawing.Color.Transparent;
            lblClassroomId.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblClassroomId.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblClassroomId.Location = new System.Drawing.Point(24, 87);
            lblClassroomId.Name = "lblClassroomId";
            lblClassroomId.Size = new System.Drawing.Size(85, 19);
            lblClassroomId.TabIndex = 2;
            lblClassroomId.Text = "Classroom ID";
            // 
            // lblEditorSubtitle
            // 
            lblEditorSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblEditorSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblEditorSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblEditorSubtitle.Location = new System.Drawing.Point(24, 44);
            lblEditorSubtitle.Name = "lblEditorSubtitle";
            lblEditorSubtitle.Size = new System.Drawing.Size(298, 17);
            lblEditorSubtitle.TabIndex = 1;
            lblEditorSubtitle.Text = "Prepare classroom details before applying an action.";
            // 
            // lblEditorTitle
            // 
            lblEditorTitle.BackColor = System.Drawing.Color.Transparent;
            lblEditorTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblEditorTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblEditorTitle.Location = new System.Drawing.Point(24, 18);
            lblEditorTitle.Name = "lblEditorTitle";
            lblEditorTitle.Size = new System.Drawing.Size(155, 25);
            lblEditorTitle.TabIndex = 0;
            lblEditorTitle.Text = "Classroom Details";
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
            lblPageSubtitle.Size = new System.Drawing.Size(341, 19);
            lblPageSubtitle.TabIndex = 1;
            lblPageSubtitle.Text = "Manage rooms, capacities, and classroom type information.";
            // 
            // lblPageTitle
            // 
            lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            lblPageTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblPageTitle.Location = new System.Drawing.Point(32, 16);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new System.Drawing.Size(312, 34);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Classrooms Management";
            // 
            // ClassroomsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            ClientSize = new System.Drawing.Size(1180, 720);
            Controls.Add(pnlMain);
            Controls.Add(pnlSidebar);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            MinimumSize = new System.Drawing.Size(980, 600);
            Name = "ClassroomsForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Classrooms Management";
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlWorkspace.ResumeLayout(false);
            pnlClassroomsTable.ResumeLayout(false);
            pnlClassroomsTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClassrooms).EndInit();
            pnlClassroomEditor.ResumeLayout(false);
            pnlClassroomEditor.PerformLayout();
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
        private Guna.UI2.WinForms.Guna2Panel pnlClassroomEditor;
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
        private Guna.UI2.WinForms.Guna2Button btnAddClassroom;
        private Guna.UI2.WinForms.Guna2Button btnUpdateClassroom;
        private Guna.UI2.WinForms.Guna2Button btnDeleteClassroom;
        private Guna.UI2.WinForms.Guna2Button btnClearClassroomForm;
        private Guna.UI2.WinForms.Guna2Panel pnlClassroomsTable;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableSubtitle;
        private Guna.UI2.WinForms.Guna2DataGridView dgvClassrooms;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClassroomId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClassroomNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCapacity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoomType;
    }
}
