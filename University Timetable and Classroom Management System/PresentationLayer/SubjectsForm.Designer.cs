namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SubjectsForm : System.Windows.Forms.Form
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
            pnlSubjectsTable = new Guna.UI2.WinForms.Guna2Panel();
            dgvSubjects = new Guna.UI2.WinForms.Guna2DataGridView();
            colSubjectId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSubjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colStudyYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSemester = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTheoreticalHours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPracticalHours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colCreditUnits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colRequirementType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblTableSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTableTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlSubjectEditor = new Guna.UI2.WinForms.Guna2Panel();
            btnClearSubjectForm = new Guna.UI2.WinForms.Guna2Button();
            btnDeleteSubject = new Guna.UI2.WinForms.Guna2Button();
            btnUpdateSubject = new Guna.UI2.WinForms.Guna2Button();
            btnAddSubject = new Guna.UI2.WinForms.Guna2Button();
            txtCreditUnits = new Guna.UI2.WinForms.Guna2TextBox();
            lblCreditUnits = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtPracticalHours = new Guna.UI2.WinForms.Guna2TextBox();
            lblPracticalHours = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtTheoreticalHours = new Guna.UI2.WinForms.Guna2TextBox();
            lblTheoreticalHours = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbRequirementType = new Guna.UI2.WinForms.Guna2ComboBox();
            lblRequirementType = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbSemester = new Guna.UI2.WinForms.Guna2ComboBox();
            lblSemester = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbBranch = new Guna.UI2.WinForms.Guna2ComboBox();
            lblBranch = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbStudyYear = new Guna.UI2.WinForms.Guna2ComboBox();
            lblStudyYear = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtSubjectName = new Guna.UI2.WinForms.Guna2TextBox();
            lblSubjectName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtSubjectId = new Guna.UI2.WinForms.Guna2TextBox();
            lblSubjectId = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEditorSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEditorTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            lblPageSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblPageTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlSidebar.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlWorkspace.SuspendLayout();
            pnlSubjectsTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSubjects).BeginInit();
            pnlSubjectEditor.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = System.Drawing.Color.Transparent;
            pnlSidebar.Controls.Add(lblSidebarFooter);
            pnlSidebar.Controls.Add(btnNavigationSchedules);
            pnlSidebar.Controls.Add(btnNavigationFaculty);
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
            lblSidebarFooter.TabIndex = 10;
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
            btnNavigationSchedules.Location = new System.Drawing.Point(24, 434);
            btnNavigationSchedules.Name = "btnNavigationSchedules";
            btnNavigationSchedules.Size = new System.Drawing.Size(192, 44);
            btnNavigationSchedules.TabIndex = 9;
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
            btnNavigationFaculty.Location = new System.Drawing.Point(24, 378);
            btnNavigationFaculty.Name = "btnNavigationFaculty";
            btnNavigationFaculty.Size = new System.Drawing.Size(192, 44);
            btnNavigationFaculty.TabIndex = 8;
            btnNavigationFaculty.Text = "Faculty Members";
            btnNavigationFaculty.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            btnNavigationFaculty.TextOffset = new System.Drawing.Point(14, 0);
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
            btnNavigationSubjects.Checked = true;
            btnNavigationSubjects.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationSubjects.Enabled = false;
            btnNavigationSubjects.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnNavigationSubjects.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationSubjects.ForeColor = System.Drawing.Color.White;
            btnNavigationSubjects.HoverState.FillColor = System.Drawing.Color.FromArgb(29, 78, 216);
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
            pnlWorkspace.Controls.Add(pnlSubjectsTable);
            pnlWorkspace.Controls.Add(pnlSubjectEditor);
            pnlWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlWorkspace.FillColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlWorkspace.Location = new System.Drawing.Point(0, 88);
            pnlWorkspace.Name = "pnlWorkspace";
            pnlWorkspace.Padding = new System.Windows.Forms.Padding(28, 24, 28, 28);
            pnlWorkspace.Size = new System.Drawing.Size(940, 632);
            pnlWorkspace.TabIndex = 1;
            // 
            // pnlSubjectsTable
            // 
            pnlSubjectsTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlSubjectsTable.BackColor = System.Drawing.Color.Transparent;
            pnlSubjectsTable.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlSubjectsTable.BorderRadius = 8;
            pnlSubjectsTable.BorderThickness = 1;
            pnlSubjectsTable.Controls.Add(dgvSubjects);
            pnlSubjectsTable.Controls.Add(lblTableSubtitle);
            pnlSubjectsTable.Controls.Add(lblTableTitle);
            pnlSubjectsTable.FillColor = System.Drawing.Color.White;
            pnlSubjectsTable.Location = new System.Drawing.Point(28, 336);
            pnlSubjectsTable.Name = "pnlSubjectsTable";
            pnlSubjectsTable.Size = new System.Drawing.Size(884, 268);
            pnlSubjectsTable.TabIndex = 1;
            // 
            // dgvSubjects
            // 
            dgvSubjects.AllowUserToAddRows = false;
            dgvSubjects.AllowUserToDeleteRows = false;
            dgvSubjects.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            dgvSubjects.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvSubjects.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvSubjects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvSubjects.BackgroundColor = System.Drawing.Color.White;
            dgvSubjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvSubjects.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSubjects.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvSubjects.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvSubjects.ColumnHeadersHeight = 44;
            dgvSubjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvSubjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colSubjectId, colSubjectName, colStudyYear, colBranch, colSemester, colTheoreticalHours, colPracticalHours, colCreditUnits, colRequirementType });
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvSubjects.DefaultCellStyle = dataGridViewCellStyle3;
            dgvSubjects.EnableHeadersVisualStyles = false;
            dgvSubjects.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgvSubjects.Location = new System.Drawing.Point(24, 78);
            dgvSubjects.MultiSelect = false;
            dgvSubjects.Name = "dgvSubjects";
            dgvSubjects.ReadOnly = true;
            dgvSubjects.RowHeadersVisible = false;
            dgvSubjects.RowTemplate.Height = 42;
            dgvSubjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvSubjects.Size = new System.Drawing.Size(836, 166);
            dgvSubjects.TabIndex = 2;
            // 
            // colSubjectId
            // 
            colSubjectId.DataPropertyName = "SubjectID";
            colSubjectId.FillWeight = 42F;
            colSubjectId.HeaderText = "ID";
            colSubjectId.Name = "colSubjectId";
            colSubjectId.ReadOnly = true;
            // 
            // colSubjectName
            // 
            colSubjectName.DataPropertyName = "SubjectName";
            colSubjectName.FillWeight = 130F;
            colSubjectName.HeaderText = "Subject Name";
            colSubjectName.Name = "colSubjectName";
            colSubjectName.ReadOnly = true;
            // 
            // colStudyYear
            // 
            colStudyYear.DataPropertyName = "StudyYearID";
            colStudyYear.FillWeight = 70F;
            colStudyYear.HeaderText = "Study Year";
            colStudyYear.Name = "colStudyYear";
            colStudyYear.ReadOnly = true;
            // 
            // colBranch
            // 
            colBranch.DataPropertyName = "BranchID";
            colBranch.FillWeight = 70F;
            colBranch.HeaderText = "Branch";
            colBranch.Name = "colBranch";
            colBranch.ReadOnly = true;
            // 
            // colSemester
            // 
            colSemester.DataPropertyName = "SemesterNumber";
            colSemester.FillWeight = 55F;
            colSemester.HeaderText = "Semester";
            colSemester.Name = "colSemester";
            colSemester.ReadOnly = true;
            // 
            // colTheoreticalHours
            // 
            colTheoreticalHours.DataPropertyName = "TheoreticalHours";
            colTheoreticalHours.FillWeight = 58F;
            colTheoreticalHours.HeaderText = "Theory";
            colTheoreticalHours.Name = "colTheoreticalHours";
            colTheoreticalHours.ReadOnly = true;
            // 
            // colPracticalHours
            // 
            colPracticalHours.DataPropertyName = "PracticalHours";
            colPracticalHours.FillWeight = 58F;
            colPracticalHours.HeaderText = "Practical";
            colPracticalHours.Name = "colPracticalHours";
            colPracticalHours.ReadOnly = true;
            // 
            // colCreditUnits
            // 
            colCreditUnits.DataPropertyName = "CreditUnits";
            colCreditUnits.FillWeight = 55F;
            colCreditUnits.HeaderText = "Units";
            colCreditUnits.Name = "colCreditUnits";
            colCreditUnits.ReadOnly = true;
            // 
            // colRequirementType
            // 
            colRequirementType.DataPropertyName = "RequirementType";
            colRequirementType.FillWeight = 92F;
            colRequirementType.HeaderText = "Requirement";
            colRequirementType.Name = "colRequirementType";
            colRequirementType.ReadOnly = true;
            // 
            // lblTableSubtitle
            // 
            lblTableSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblTableSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblTableSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTableSubtitle.Location = new System.Drawing.Point(24, 43);
            lblTableSubtitle.Name = "lblTableSubtitle";
            lblTableSubtitle.Size = new System.Drawing.Size(285, 17);
            lblTableSubtitle.TabIndex = 1;
            lblTableSubtitle.Text = "Review and select academic subject records.";
            // 
            // lblTableTitle
            // 
            lblTableTitle.BackColor = System.Drawing.Color.Transparent;
            lblTableTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTableTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblTableTitle.Location = new System.Drawing.Point(24, 18);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new System.Drawing.Size(100, 25);
            lblTableTitle.TabIndex = 0;
            lblTableTitle.Text = "Subjects List";
            // 
            // pnlSubjectEditor
            // 
            pnlSubjectEditor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlSubjectEditor.BackColor = System.Drawing.Color.Transparent;
            pnlSubjectEditor.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlSubjectEditor.BorderRadius = 8;
            pnlSubjectEditor.BorderThickness = 1;
            pnlSubjectEditor.Controls.Add(btnClearSubjectForm);
            pnlSubjectEditor.Controls.Add(btnDeleteSubject);
            pnlSubjectEditor.Controls.Add(btnUpdateSubject);
            pnlSubjectEditor.Controls.Add(btnAddSubject);
            pnlSubjectEditor.Controls.Add(txtCreditUnits);
            pnlSubjectEditor.Controls.Add(lblCreditUnits);
            pnlSubjectEditor.Controls.Add(txtPracticalHours);
            pnlSubjectEditor.Controls.Add(lblPracticalHours);
            pnlSubjectEditor.Controls.Add(txtTheoreticalHours);
            pnlSubjectEditor.Controls.Add(lblTheoreticalHours);
            pnlSubjectEditor.Controls.Add(cmbRequirementType);
            pnlSubjectEditor.Controls.Add(lblRequirementType);
            pnlSubjectEditor.Controls.Add(cmbSemester);
            pnlSubjectEditor.Controls.Add(lblSemester);
            pnlSubjectEditor.Controls.Add(cmbBranch);
            pnlSubjectEditor.Controls.Add(lblBranch);
            pnlSubjectEditor.Controls.Add(cmbStudyYear);
            pnlSubjectEditor.Controls.Add(lblStudyYear);
            pnlSubjectEditor.Controls.Add(txtSubjectName);
            pnlSubjectEditor.Controls.Add(lblSubjectName);
            pnlSubjectEditor.Controls.Add(txtSubjectId);
            pnlSubjectEditor.Controls.Add(lblSubjectId);
            pnlSubjectEditor.Controls.Add(lblEditorSubtitle);
            pnlSubjectEditor.Controls.Add(lblEditorTitle);
            pnlSubjectEditor.FillColor = System.Drawing.Color.White;
            pnlSubjectEditor.Location = new System.Drawing.Point(28, 24);
            pnlSubjectEditor.Name = "pnlSubjectEditor";
            pnlSubjectEditor.Size = new System.Drawing.Size(884, 290);
            pnlSubjectEditor.TabIndex = 0;
            // 
            // btnClearSubjectForm
            // 
            btnClearSubjectForm.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClearSubjectForm.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnClearSubjectForm.BorderRadius = 8;
            btnClearSubjectForm.BorderThickness = 1;
            btnClearSubjectForm.Cursor = System.Windows.Forms.Cursors.Hand;
            btnClearSubjectForm.FillColor = System.Drawing.Color.White;
            btnClearSubjectForm.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnClearSubjectForm.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            btnClearSubjectForm.HoverState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            btnClearSubjectForm.Location = new System.Drawing.Point(752, 142);
            btnClearSubjectForm.Name = "btnClearSubjectForm";
            btnClearSubjectForm.Size = new System.Drawing.Size(108, 38);
            btnClearSubjectForm.TabIndex = 23;
            btnClearSubjectForm.Text = "Clear";
            // 
            // btnDeleteSubject
            // 
            btnDeleteSubject.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnDeleteSubject.BorderRadius = 8;
            btnDeleteSubject.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDeleteSubject.FillColor = System.Drawing.Color.FromArgb(220, 38, 38);
            btnDeleteSubject.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnDeleteSubject.ForeColor = System.Drawing.Color.White;
            btnDeleteSubject.HoverState.FillColor = System.Drawing.Color.FromArgb(185, 28, 28);
            btnDeleteSubject.Location = new System.Drawing.Point(632, 142);
            btnDeleteSubject.Name = "btnDeleteSubject";
            btnDeleteSubject.Size = new System.Drawing.Size(108, 38);
            btnDeleteSubject.TabIndex = 22;
            btnDeleteSubject.Text = "Delete";
            // 
            // btnUpdateSubject
            // 
            btnUpdateSubject.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnUpdateSubject.BorderRadius = 8;
            btnUpdateSubject.Cursor = System.Windows.Forms.Cursors.Hand;
            btnUpdateSubject.FillColor = System.Drawing.Color.FromArgb(14, 116, 144);
            btnUpdateSubject.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnUpdateSubject.ForeColor = System.Drawing.Color.White;
            btnUpdateSubject.HoverState.FillColor = System.Drawing.Color.FromArgb(21, 94, 117);
            btnUpdateSubject.Location = new System.Drawing.Point(752, 98);
            btnUpdateSubject.Name = "btnUpdateSubject";
            btnUpdateSubject.Size = new System.Drawing.Size(108, 38);
            btnUpdateSubject.TabIndex = 21;
            btnUpdateSubject.Text = "Update";
            // 
            // btnAddSubject
            // 
            btnAddSubject.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAddSubject.BorderRadius = 8;
            btnAddSubject.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAddSubject.FillColor = System.Drawing.Color.FromArgb(22, 163, 74);
            btnAddSubject.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnAddSubject.ForeColor = System.Drawing.Color.White;
            btnAddSubject.HoverState.FillColor = System.Drawing.Color.FromArgb(21, 128, 61);
            btnAddSubject.Location = new System.Drawing.Point(632, 98);
            btnAddSubject.Name = "btnAddSubject";
            btnAddSubject.Size = new System.Drawing.Size(108, 38);
            btnAddSubject.TabIndex = 20;
            btnAddSubject.Text = "Add";
            // 
            // txtCreditUnits
            // 
            txtCreditUnits.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            txtCreditUnits.BorderRadius = 8;
            txtCreditUnits.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtCreditUnits.DefaultText = "";
            txtCreditUnits.DisabledState.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtCreditUnits.DisabledState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtCreditUnits.DisabledState.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtCreditUnits.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            txtCreditUnits.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtCreditUnits.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            txtCreditUnits.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            txtCreditUnits.Location = new System.Drawing.Point(402, 224);
            txtCreditUnits.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtCreditUnits.Name = "txtCreditUnits";
            txtCreditUnits.PasswordChar = '\0';
            txtCreditUnits.PlaceholderForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            txtCreditUnits.PlaceholderText = "Units";
            txtCreditUnits.SelectedText = "";
            txtCreditUnits.Size = new System.Drawing.Size(190, 42);
            txtCreditUnits.TabIndex = 19;
            // 
            // lblCreditUnits
            // 
            lblCreditUnits.BackColor = System.Drawing.Color.Transparent;
            lblCreditUnits.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblCreditUnits.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblCreditUnits.Location = new System.Drawing.Point(402, 199);
            lblCreditUnits.Name = "lblCreditUnits";
            lblCreditUnits.Size = new System.Drawing.Size(76, 19);
            lblCreditUnits.TabIndex = 18;
            lblCreditUnits.Text = "Credit Units";
            // 
            // txtPracticalHours
            // 
            txtPracticalHours.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            txtPracticalHours.BorderRadius = 8;
            txtPracticalHours.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtPracticalHours.DefaultText = "";
            txtPracticalHours.DisabledState.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtPracticalHours.DisabledState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtPracticalHours.DisabledState.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtPracticalHours.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            txtPracticalHours.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtPracticalHours.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            txtPracticalHours.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            txtPracticalHours.Location = new System.Drawing.Point(214, 224);
            txtPracticalHours.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtPracticalHours.Name = "txtPracticalHours";
            txtPracticalHours.PasswordChar = '\0';
            txtPracticalHours.PlaceholderForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            txtPracticalHours.PlaceholderText = "0";
            txtPracticalHours.SelectedText = "";
            txtPracticalHours.Size = new System.Drawing.Size(160, 42);
            txtPracticalHours.TabIndex = 17;
            // 
            // lblPracticalHours
            // 
            lblPracticalHours.BackColor = System.Drawing.Color.Transparent;
            lblPracticalHours.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblPracticalHours.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblPracticalHours.Location = new System.Drawing.Point(214, 199);
            lblPracticalHours.Name = "lblPracticalHours";
            lblPracticalHours.Size = new System.Drawing.Size(96, 19);
            lblPracticalHours.TabIndex = 16;
            lblPracticalHours.Text = "Practical Hours";
            // 
            // txtTheoreticalHours
            // 
            txtTheoreticalHours.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            txtTheoreticalHours.BorderRadius = 8;
            txtTheoreticalHours.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtTheoreticalHours.DefaultText = "";
            txtTheoreticalHours.DisabledState.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtTheoreticalHours.DisabledState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtTheoreticalHours.DisabledState.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtTheoreticalHours.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            txtTheoreticalHours.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtTheoreticalHours.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            txtTheoreticalHours.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            txtTheoreticalHours.Location = new System.Drawing.Point(24, 224);
            txtTheoreticalHours.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtTheoreticalHours.Name = "txtTheoreticalHours";
            txtTheoreticalHours.PasswordChar = '\0';
            txtTheoreticalHours.PlaceholderForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            txtTheoreticalHours.PlaceholderText = "0";
            txtTheoreticalHours.SelectedText = "";
            txtTheoreticalHours.Size = new System.Drawing.Size(160, 42);
            txtTheoreticalHours.TabIndex = 15;
            // 
            // lblTheoreticalHours
            // 
            lblTheoreticalHours.BackColor = System.Drawing.Color.Transparent;
            lblTheoreticalHours.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTheoreticalHours.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblTheoreticalHours.Location = new System.Drawing.Point(24, 199);
            lblTheoreticalHours.Name = "lblTheoreticalHours";
            lblTheoreticalHours.Size = new System.Drawing.Size(110, 19);
            lblTheoreticalHours.TabIndex = 14;
            lblTheoreticalHours.Text = "Theoretical Hours";
            // 
            // cmbRequirementType
            // 
            cmbRequirementType.BackColor = System.Drawing.Color.Transparent;
            cmbRequirementType.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            cmbRequirementType.BorderRadius = 8;
            cmbRequirementType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbRequirementType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbRequirementType.FocusedColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbRequirementType.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbRequirementType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cmbRequirementType.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            cmbRequirementType.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            cmbRequirementType.ItemHeight = 36;
            cmbRequirementType.Items.AddRange(new object[] { "Core", "Elective", "University Requirement", "College Requirement" });
            cmbRequirementType.Location = new System.Drawing.Point(402, 154);
            cmbRequirementType.Name = "cmbRequirementType";
            cmbRequirementType.Size = new System.Drawing.Size(190, 42);
            cmbRequirementType.TabIndex = 13;
            // 
            // lblRequirementType
            // 
            lblRequirementType.BackColor = System.Drawing.Color.Transparent;
            lblRequirementType.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblRequirementType.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblRequirementType.Location = new System.Drawing.Point(402, 129);
            lblRequirementType.Name = "lblRequirementType";
            lblRequirementType.Size = new System.Drawing.Size(108, 19);
            lblRequirementType.TabIndex = 12;
            lblRequirementType.Text = "Requirement Type";
            // 
            // cmbSemester
            // 
            cmbSemester.BackColor = System.Drawing.Color.Transparent;
            cmbSemester.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            cmbSemester.BorderRadius = 8;
            cmbSemester.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbSemester.FocusedColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbSemester.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            cmbSemester.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cmbSemester.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            cmbSemester.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            cmbSemester.ItemHeight = 36;
            cmbSemester.Items.AddRange(new object[] { "1", "2" });
            cmbSemester.Location = new System.Drawing.Point(214, 154);
            cmbSemester.Name = "cmbSemester";
            cmbSemester.Size = new System.Drawing.Size(160, 42);
            cmbSemester.TabIndex = 11;
            // 
            // lblSemester
            // 
            lblSemester.BackColor = System.Drawing.Color.Transparent;
            lblSemester.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblSemester.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblSemester.Location = new System.Drawing.Point(214, 129);
            lblSemester.Name = "lblSemester";
            lblSemester.Size = new System.Drawing.Size(58, 19);
            lblSemester.TabIndex = 10;
            lblSemester.Text = "Semester";
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
            cmbBranch.Location = new System.Drawing.Point(24, 154);
            cmbBranch.Name = "cmbBranch";
            cmbBranch.Size = new System.Drawing.Size(160, 42);
            cmbBranch.TabIndex = 9;
            // 
            // lblBranch
            // 
            lblBranch.BackColor = System.Drawing.Color.Transparent;
            lblBranch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblBranch.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblBranch.Location = new System.Drawing.Point(24, 129);
            lblBranch.Name = "lblBranch";
            lblBranch.Size = new System.Drawing.Size(45, 19);
            lblBranch.TabIndex = 8;
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
            cmbStudyYear.Location = new System.Drawing.Point(402, 84);
            cmbStudyYear.Name = "cmbStudyYear";
            cmbStudyYear.Size = new System.Drawing.Size(190, 42);
            cmbStudyYear.TabIndex = 7;
            // 
            // lblStudyYear
            // 
            lblStudyYear.BackColor = System.Drawing.Color.Transparent;
            lblStudyYear.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblStudyYear.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblStudyYear.Location = new System.Drawing.Point(402, 59);
            lblStudyYear.Name = "lblStudyYear";
            lblStudyYear.Size = new System.Drawing.Size(69, 19);
            lblStudyYear.TabIndex = 6;
            lblStudyYear.Text = "Study Year";
            // 
            // txtSubjectName
            // 
            txtSubjectName.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            txtSubjectName.BorderRadius = 8;
            txtSubjectName.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtSubjectName.DefaultText = "";
            txtSubjectName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtSubjectName.DisabledState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtSubjectName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtSubjectName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            txtSubjectName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtSubjectName.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            txtSubjectName.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            txtSubjectName.Location = new System.Drawing.Point(184, 84);
            txtSubjectName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtSubjectName.Name = "txtSubjectName";
            txtSubjectName.PasswordChar = '\0';
            txtSubjectName.PlaceholderForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            txtSubjectName.PlaceholderText = "Enter subject name";
            txtSubjectName.SelectedText = "";
            txtSubjectName.Size = new System.Drawing.Size(190, 42);
            txtSubjectName.TabIndex = 5;
            // 
            // lblSubjectName
            // 
            lblSubjectName.BackColor = System.Drawing.Color.Transparent;
            lblSubjectName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblSubjectName.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblSubjectName.Location = new System.Drawing.Point(184, 59);
            lblSubjectName.Name = "lblSubjectName";
            lblSubjectName.Size = new System.Drawing.Size(87, 19);
            lblSubjectName.TabIndex = 4;
            lblSubjectName.Text = "Subject Name";
            // 
            // txtSubjectId
            // 
            txtSubjectId.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtSubjectId.BorderRadius = 8;
            txtSubjectId.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtSubjectId.DefaultText = "";
            txtSubjectId.DisabledState.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtSubjectId.DisabledState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtSubjectId.DisabledState.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtSubjectId.Enabled = false;
            txtSubjectId.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtSubjectId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtSubjectId.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtSubjectId.Location = new System.Drawing.Point(24, 84);
            txtSubjectId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtSubjectId.Name = "txtSubjectId";
            txtSubjectId.PasswordChar = '\0';
            txtSubjectId.PlaceholderForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            txtSubjectId.PlaceholderText = "Auto";
            txtSubjectId.SelectedText = "";
            txtSubjectId.Size = new System.Drawing.Size(132, 42);
            txtSubjectId.TabIndex = 3;
            // 
            // lblSubjectId
            // 
            lblSubjectId.BackColor = System.Drawing.Color.Transparent;
            lblSubjectId.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblSubjectId.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblSubjectId.Location = new System.Drawing.Point(24, 59);
            lblSubjectId.Name = "lblSubjectId";
            lblSubjectId.Size = new System.Drawing.Size(65, 19);
            lblSubjectId.TabIndex = 2;
            lblSubjectId.Text = "Subject ID";
            // 
            // lblEditorSubtitle
            // 
            lblEditorSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblEditorSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblEditorSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblEditorSubtitle.Location = new System.Drawing.Point(24, 34);
            lblEditorSubtitle.Name = "lblEditorSubtitle";
            lblEditorSubtitle.Size = new System.Drawing.Size(322, 17);
            lblEditorSubtitle.TabIndex = 1;
            lblEditorSubtitle.Text = "Prepare subject details before applying an action.";
            // 
            // lblEditorTitle
            // 
            lblEditorTitle.BackColor = System.Drawing.Color.Transparent;
            lblEditorTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblEditorTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblEditorTitle.Location = new System.Drawing.Point(24, 9);
            lblEditorTitle.Name = "lblEditorTitle";
            lblEditorTitle.Size = new System.Drawing.Size(126, 25);
            lblEditorTitle.TabIndex = 0;
            lblEditorTitle.Text = "Subject Details";
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
            lblPageSubtitle.Size = new System.Drawing.Size(396, 19);
            lblPageSubtitle.TabIndex = 1;
            lblPageSubtitle.Text = "Manage subjects, semesters, credit units, and academic ownership.";
            // 
            // lblPageTitle
            // 
            lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            lblPageTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblPageTitle.Location = new System.Drawing.Point(32, 16);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new System.Drawing.Size(270, 34);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Subjects Management";
            // 
            // SubjectsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            ClientSize = new System.Drawing.Size(1180, 720);
            Controls.Add(pnlMain);
            Controls.Add(pnlSidebar);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            MinimumSize = new System.Drawing.Size(980, 600);
            Name = "SubjectsForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Subjects Management";
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlWorkspace.ResumeLayout(false);
            pnlSubjectsTable.ResumeLayout(false);
            pnlSubjectsTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSubjects).EndInit();
            pnlSubjectEditor.ResumeLayout(false);
            pnlSubjectEditor.PerformLayout();
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
        private Guna.UI2.WinForms.Guna2Button btnNavigationFaculty;
        private Guna.UI2.WinForms.Guna2Button btnNavigationSchedules;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSidebarFooter;
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageSubtitle;
        private Guna.UI2.WinForms.Guna2Panel pnlWorkspace;
        private Guna.UI2.WinForms.Guna2Panel pnlSubjectEditor;
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
        private Guna.UI2.WinForms.Guna2HtmlLabel lblRequirementType;
        private Guna.UI2.WinForms.Guna2ComboBox cmbRequirementType;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTheoreticalHours;
        private Guna.UI2.WinForms.Guna2TextBox txtTheoreticalHours;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPracticalHours;
        private Guna.UI2.WinForms.Guna2TextBox txtPracticalHours;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCreditUnits;
        private Guna.UI2.WinForms.Guna2TextBox txtCreditUnits;
        private Guna.UI2.WinForms.Guna2Button btnAddSubject;
        private Guna.UI2.WinForms.Guna2Button btnUpdateSubject;
        private Guna.UI2.WinForms.Guna2Button btnDeleteSubject;
        private Guna.UI2.WinForms.Guna2Button btnClearSubjectForm;
        private Guna.UI2.WinForms.Guna2Panel pnlSubjectsTable;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableSubtitle;
        private Guna.UI2.WinForms.Guna2DataGridView dgvSubjects;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubjectId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudyYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSemester;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTheoreticalHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPracticalHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreditUnits;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRequirementType;
    }
}
