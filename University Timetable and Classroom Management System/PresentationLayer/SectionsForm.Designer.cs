namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SectionsForm : System.Windows.Forms.Form
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
            btnNavigationSections = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationStudyYears = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationBranches = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationDashboard = new Guna.UI2.WinForms.Guna2Button();
            separatorSidebar = new Guna.UI2.WinForms.Guna2Separator();
            lblSidebarSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblApplicationName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            pnlWorkspace = new Guna.UI2.WinForms.Guna2Panel();
            pnlSectionsTable = new Guna.UI2.WinForms.Guna2Panel();
            dgvSections = new Guna.UI2.WinForms.Guna2DataGridView();
            colSectionId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSectionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colStudentCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colStudyYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblTableSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTableTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlSectionEditor = new Guna.UI2.WinForms.Guna2Panel();
            btnClearSectionForm = new Guna.UI2.WinForms.Guna2Button();
            btnDeleteSection = new Guna.UI2.WinForms.Guna2Button();
            btnUpdateSection = new Guna.UI2.WinForms.Guna2Button();
            btnAddSection = new Guna.UI2.WinForms.Guna2Button();
            cmbBranch = new Guna.UI2.WinForms.Guna2ComboBox();
            lblBranch = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbStudyYear = new Guna.UI2.WinForms.Guna2ComboBox();
            lblStudyYear = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtStudentCount = new Guna.UI2.WinForms.Guna2TextBox();
            lblStudentCount = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtSectionName = new Guna.UI2.WinForms.Guna2TextBox();
            lblSectionName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtSectionId = new Guna.UI2.WinForms.Guna2TextBox();
            lblSectionId = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEditorSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEditorTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            lblPageSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblPageTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlSidebar.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlWorkspace.SuspendLayout();
            pnlSectionsTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSections).BeginInit();
            pnlSectionEditor.SuspendLayout();
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
            lblSidebarFooter.TabIndex = 12;
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
            btnNavigationSchedules.Location = new System.Drawing.Point(24, 546);
            btnNavigationSchedules.Name = "btnNavigationSchedules";
            btnNavigationSchedules.Size = new System.Drawing.Size(192, 44);
            btnNavigationSchedules.TabIndex = 11;
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
            btnNavigationFaculty.Location = new System.Drawing.Point(24, 490);
            btnNavigationFaculty.Name = "btnNavigationFaculty";
            btnNavigationFaculty.Size = new System.Drawing.Size(192, 44);
            btnNavigationFaculty.TabIndex = 10;
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
            btnNavigationSections.Checked = true;
            btnNavigationSections.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationSections.Enabled = false;
            btnNavigationSections.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnNavigationSections.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationSections.ForeColor = System.Drawing.Color.White;
            btnNavigationSections.HoverState.FillColor = System.Drawing.Color.FromArgb(29, 78, 216);
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
            pnlWorkspace.Controls.Add(pnlSectionsTable);
            pnlWorkspace.Controls.Add(pnlSectionEditor);
            pnlWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlWorkspace.FillColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlWorkspace.Location = new System.Drawing.Point(0, 88);
            pnlWorkspace.Name = "pnlWorkspace";
            pnlWorkspace.Padding = new System.Windows.Forms.Padding(28, 24, 28, 28);
            pnlWorkspace.Size = new System.Drawing.Size(940, 632);
            pnlWorkspace.TabIndex = 1;
            // 
            // pnlSectionsTable
            // 
            pnlSectionsTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlSectionsTable.BackColor = System.Drawing.Color.Transparent;
            pnlSectionsTable.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlSectionsTable.BorderRadius = 8;
            pnlSectionsTable.BorderThickness = 1;
            pnlSectionsTable.Controls.Add(dgvSections);
            pnlSectionsTable.Controls.Add(lblTableSubtitle);
            pnlSectionsTable.Controls.Add(lblTableTitle);
            pnlSectionsTable.FillColor = System.Drawing.Color.White;
            pnlSectionsTable.Location = new System.Drawing.Point(28, 296);
            pnlSectionsTable.Name = "pnlSectionsTable";
            pnlSectionsTable.Size = new System.Drawing.Size(884, 308);
            pnlSectionsTable.TabIndex = 1;
            // 
            // dgvSections
            // 
            dgvSections.AllowUserToAddRows = false;
            dgvSections.AllowUserToDeleteRows = false;
            dgvSections.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            dgvSections.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvSections.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvSections.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvSections.BackgroundColor = System.Drawing.Color.White;
            dgvSections.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvSections.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSections.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvSections.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvSections.ColumnHeadersHeight = 44;
            dgvSections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvSections.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colSectionId, colSectionName, colStudentCount, colStudyYear, colBranch });
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvSections.DefaultCellStyle = dataGridViewCellStyle3;
            dgvSections.EnableHeadersVisualStyles = false;
            dgvSections.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgvSections.Location = new System.Drawing.Point(24, 78);
            dgvSections.MultiSelect = false;
            dgvSections.Name = "dgvSections";
            dgvSections.ReadOnly = true;
            dgvSections.RowHeadersVisible = false;
            dgvSections.RowTemplate.Height = 42;
            dgvSections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvSections.Size = new System.Drawing.Size(836, 204);
            dgvSections.TabIndex = 2;
            // 
            // colSectionId
            // 
            colSectionId.DataPropertyName = "SectionID";
            colSectionId.FillWeight = 45F;
            colSectionId.HeaderText = "Section ID";
            colSectionId.Name = "colSectionId";
            colSectionId.ReadOnly = true;
            // 
            // colSectionName
            // 
            colSectionName.DataPropertyName = "SectionName";
            colSectionName.FillWeight = 110F;
            colSectionName.HeaderText = "Section Name";
            colSectionName.Name = "colSectionName";
            colSectionName.ReadOnly = true;
            // 
            // colStudentCount
            // 
            colStudentCount.DataPropertyName = "StudentCount";
            colStudentCount.FillWeight = 65F;
            colStudentCount.HeaderText = "Students";
            colStudentCount.Name = "colStudentCount";
            colStudentCount.ReadOnly = true;
            // 
            // colStudyYear
            // 
            colStudyYear.DataPropertyName = "StudyYearID";
            colStudyYear.FillWeight = 85F;
            colStudyYear.HeaderText = "Study Year";
            colStudyYear.Name = "colStudyYear";
            colStudyYear.ReadOnly = true;
            // 
            // colBranch
            // 
            colBranch.DataPropertyName = "BranchID";
            colBranch.FillWeight = 85F;
            colBranch.HeaderText = "Branch";
            colBranch.Name = "colBranch";
            colBranch.ReadOnly = true;
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
            lblTableSubtitle.Text = "Review and select academic section records.";
            // 
            // lblTableTitle
            // 
            lblTableTitle.BackColor = System.Drawing.Color.Transparent;
            lblTableTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTableTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblTableTitle.Location = new System.Drawing.Point(24, 18);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new System.Drawing.Size(105, 25);
            lblTableTitle.TabIndex = 0;
            lblTableTitle.Text = "Sections List";
            // 
            // pnlSectionEditor
            // 
            pnlSectionEditor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlSectionEditor.BackColor = System.Drawing.Color.Transparent;
            pnlSectionEditor.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlSectionEditor.BorderRadius = 8;
            pnlSectionEditor.BorderThickness = 1;
            pnlSectionEditor.Controls.Add(btnClearSectionForm);
            pnlSectionEditor.Controls.Add(btnDeleteSection);
            pnlSectionEditor.Controls.Add(btnUpdateSection);
            pnlSectionEditor.Controls.Add(btnAddSection);
            pnlSectionEditor.Controls.Add(cmbBranch);
            pnlSectionEditor.Controls.Add(lblBranch);
            pnlSectionEditor.Controls.Add(cmbStudyYear);
            pnlSectionEditor.Controls.Add(lblStudyYear);
            pnlSectionEditor.Controls.Add(txtStudentCount);
            pnlSectionEditor.Controls.Add(lblStudentCount);
            pnlSectionEditor.Controls.Add(txtSectionName);
            pnlSectionEditor.Controls.Add(lblSectionName);
            pnlSectionEditor.Controls.Add(txtSectionId);
            pnlSectionEditor.Controls.Add(lblSectionId);
            pnlSectionEditor.Controls.Add(lblEditorSubtitle);
            pnlSectionEditor.Controls.Add(lblEditorTitle);
            pnlSectionEditor.FillColor = System.Drawing.Color.White;
            pnlSectionEditor.Location = new System.Drawing.Point(28, 24);
            pnlSectionEditor.Name = "pnlSectionEditor";
            pnlSectionEditor.Size = new System.Drawing.Size(884, 250);
            pnlSectionEditor.TabIndex = 0;
            // 
            // btnClearSectionForm
            // 
            btnClearSectionForm.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClearSectionForm.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnClearSectionForm.BorderRadius = 8;
            btnClearSectionForm.BorderThickness = 1;
            btnClearSectionForm.Cursor = System.Windows.Forms.Cursors.Hand;
            btnClearSectionForm.FillColor = System.Drawing.Color.White;
            btnClearSectionForm.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnClearSectionForm.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            btnClearSectionForm.HoverState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            btnClearSectionForm.Location = new System.Drawing.Point(752, 142);
            btnClearSectionForm.Name = "btnClearSectionForm";
            btnClearSectionForm.Size = new System.Drawing.Size(108, 38);
            btnClearSectionForm.TabIndex = 15;
            btnClearSectionForm.Text = "Clear";
            // 
            // btnDeleteSection
            // 
            btnDeleteSection.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnDeleteSection.BorderRadius = 8;
            btnDeleteSection.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDeleteSection.FillColor = System.Drawing.Color.FromArgb(220, 38, 38);
            btnDeleteSection.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnDeleteSection.ForeColor = System.Drawing.Color.White;
            btnDeleteSection.HoverState.FillColor = System.Drawing.Color.FromArgb(185, 28, 28);
            btnDeleteSection.Location = new System.Drawing.Point(632, 142);
            btnDeleteSection.Name = "btnDeleteSection";
            btnDeleteSection.Size = new System.Drawing.Size(108, 38);
            btnDeleteSection.TabIndex = 14;
            btnDeleteSection.Text = "Delete";
            // 
            // btnUpdateSection
            // 
            btnUpdateSection.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnUpdateSection.BorderRadius = 8;
            btnUpdateSection.Cursor = System.Windows.Forms.Cursors.Hand;
            btnUpdateSection.FillColor = System.Drawing.Color.FromArgb(14, 116, 144);
            btnUpdateSection.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnUpdateSection.ForeColor = System.Drawing.Color.White;
            btnUpdateSection.HoverState.FillColor = System.Drawing.Color.FromArgb(21, 94, 117);
            btnUpdateSection.Location = new System.Drawing.Point(752, 98);
            btnUpdateSection.Name = "btnUpdateSection";
            btnUpdateSection.Size = new System.Drawing.Size(108, 38);
            btnUpdateSection.TabIndex = 13;
            btnUpdateSection.Text = "Update";
            // 
            // btnAddSection
            // 
            btnAddSection.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAddSection.BorderRadius = 8;
            btnAddSection.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAddSection.FillColor = System.Drawing.Color.FromArgb(22, 163, 74);
            btnAddSection.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnAddSection.ForeColor = System.Drawing.Color.White;
            btnAddSection.HoverState.FillColor = System.Drawing.Color.FromArgb(21, 128, 61);
            btnAddSection.Location = new System.Drawing.Point(632, 98);
            btnAddSection.Name = "btnAddSection";
            btnAddSection.Size = new System.Drawing.Size(108, 38);
            btnAddSection.TabIndex = 12;
            btnAddSection.Text = "Add";
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
            cmbBranch.Location = new System.Drawing.Point(214, 182);
            cmbBranch.Name = "cmbBranch";
            cmbBranch.Size = new System.Drawing.Size(160, 42);
            cmbBranch.TabIndex = 11;
            // 
            // lblBranch
            // 
            lblBranch.BackColor = System.Drawing.Color.Transparent;
            lblBranch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblBranch.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblBranch.Location = new System.Drawing.Point(214, 157);
            lblBranch.Name = "lblBranch";
            lblBranch.Size = new System.Drawing.Size(45, 19);
            lblBranch.TabIndex = 10;
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
            cmbStudyYear.Location = new System.Drawing.Point(24, 182);
            cmbStudyYear.Name = "cmbStudyYear";
            cmbStudyYear.Size = new System.Drawing.Size(160, 42);
            cmbStudyYear.TabIndex = 9;
            // 
            // lblStudyYear
            // 
            lblStudyYear.BackColor = System.Drawing.Color.Transparent;
            lblStudyYear.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblStudyYear.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblStudyYear.Location = new System.Drawing.Point(24, 157);
            lblStudyYear.Name = "lblStudyYear";
            lblStudyYear.Size = new System.Drawing.Size(69, 19);
            lblStudyYear.TabIndex = 8;
            lblStudyYear.Text = "Study Year";
            // 
            // txtStudentCount
            // 
            txtStudentCount.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            txtStudentCount.BorderRadius = 8;
            txtStudentCount.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtStudentCount.DefaultText = "";
            txtStudentCount.DisabledState.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtStudentCount.DisabledState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtStudentCount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtStudentCount.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            txtStudentCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtStudentCount.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            txtStudentCount.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            txtStudentCount.Location = new System.Drawing.Point(402, 112);
            txtStudentCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtStudentCount.Name = "txtStudentCount";
            txtStudentCount.PasswordChar = '\0';
            txtStudentCount.PlaceholderForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            txtStudentCount.PlaceholderText = "Students";
            txtStudentCount.SelectedText = "";
            txtStudentCount.Size = new System.Drawing.Size(190, 42);
            txtStudentCount.TabIndex = 7;
            // 
            // lblStudentCount
            // 
            lblStudentCount.BackColor = System.Drawing.Color.Transparent;
            lblStudentCount.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblStudentCount.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblStudentCount.Location = new System.Drawing.Point(402, 87);
            lblStudentCount.Name = "lblStudentCount";
            lblStudentCount.Size = new System.Drawing.Size(89, 19);
            lblStudentCount.TabIndex = 6;
            lblStudentCount.Text = "Student Count";
            // 
            // txtSectionName
            // 
            txtSectionName.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            txtSectionName.BorderRadius = 8;
            txtSectionName.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtSectionName.DefaultText = "";
            txtSectionName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtSectionName.DisabledState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtSectionName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtSectionName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            txtSectionName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtSectionName.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            txtSectionName.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            txtSectionName.Location = new System.Drawing.Point(184, 112);
            txtSectionName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtSectionName.Name = "txtSectionName";
            txtSectionName.PasswordChar = '\0';
            txtSectionName.PlaceholderForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            txtSectionName.PlaceholderText = "Enter section name";
            txtSectionName.SelectedText = "";
            txtSectionName.Size = new System.Drawing.Size(190, 42);
            txtSectionName.TabIndex = 5;
            // 
            // lblSectionName
            // 
            lblSectionName.BackColor = System.Drawing.Color.Transparent;
            lblSectionName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblSectionName.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblSectionName.Location = new System.Drawing.Point(184, 87);
            lblSectionName.Name = "lblSectionName";
            lblSectionName.Size = new System.Drawing.Size(86, 19);
            lblSectionName.TabIndex = 4;
            lblSectionName.Text = "Section Name";
            // 
            // txtSectionId
            // 
            txtSectionId.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtSectionId.BorderRadius = 8;
            txtSectionId.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtSectionId.DefaultText = "";
            txtSectionId.DisabledState.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtSectionId.DisabledState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtSectionId.DisabledState.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtSectionId.Enabled = false;
            txtSectionId.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtSectionId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtSectionId.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtSectionId.Location = new System.Drawing.Point(24, 112);
            txtSectionId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtSectionId.Name = "txtSectionId";
            txtSectionId.PasswordChar = '\0';
            txtSectionId.PlaceholderForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            txtSectionId.PlaceholderText = "Auto";
            txtSectionId.SelectedText = "";
            txtSectionId.Size = new System.Drawing.Size(132, 42);
            txtSectionId.TabIndex = 3;
            // 
            // lblSectionId
            // 
            lblSectionId.BackColor = System.Drawing.Color.Transparent;
            lblSectionId.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblSectionId.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblSectionId.Location = new System.Drawing.Point(24, 87);
            lblSectionId.Name = "lblSectionId";
            lblSectionId.Size = new System.Drawing.Size(64, 19);
            lblSectionId.TabIndex = 2;
            lblSectionId.Text = "Section ID";
            // 
            // lblEditorSubtitle
            // 
            lblEditorSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblEditorSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblEditorSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblEditorSubtitle.Location = new System.Drawing.Point(24, 44);
            lblEditorSubtitle.Name = "lblEditorSubtitle";
            lblEditorSubtitle.Size = new System.Drawing.Size(284, 17);
            lblEditorSubtitle.TabIndex = 1;
            lblEditorSubtitle.Text = "Prepare section details before applying an action.";
            // 
            // lblEditorTitle
            // 
            lblEditorTitle.BackColor = System.Drawing.Color.Transparent;
            lblEditorTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblEditorTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblEditorTitle.Location = new System.Drawing.Point(24, 18);
            lblEditorTitle.Name = "lblEditorTitle";
            lblEditorTitle.Size = new System.Drawing.Size(125, 25);
            lblEditorTitle.TabIndex = 0;
            lblEditorTitle.Text = "Section Details";
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
            lblPageSubtitle.Size = new System.Drawing.Size(394, 19);
            lblPageSubtitle.TabIndex = 1;
            lblPageSubtitle.Text = "Manage academic sections by study year, branch, and capacity.";
            // 
            // lblPageTitle
            // 
            lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            lblPageTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblPageTitle.Location = new System.Drawing.Point(32, 16);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new System.Drawing.Size(265, 34);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Sections Management";
            // 
            // SectionsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            ClientSize = new System.Drawing.Size(1180, 720);
            Controls.Add(pnlMain);
            Controls.Add(pnlSidebar);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            MinimumSize = new System.Drawing.Size(980, 600);
            Name = "SectionsForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Sections Management";
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlWorkspace.ResumeLayout(false);
            pnlSectionsTable.ResumeLayout(false);
            pnlSectionsTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSections).EndInit();
            pnlSectionEditor.ResumeLayout(false);
            pnlSectionEditor.PerformLayout();
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
        private Guna.UI2.WinForms.Guna2Button btnNavigationFaculty;
        private Guna.UI2.WinForms.Guna2Button btnNavigationSchedules;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSidebarFooter;
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageSubtitle;
        private Guna.UI2.WinForms.Guna2Panel pnlWorkspace;
        private Guna.UI2.WinForms.Guna2Panel pnlSectionEditor;
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
        private Guna.UI2.WinForms.Guna2Button btnAddSection;
        private Guna.UI2.WinForms.Guna2Button btnUpdateSection;
        private Guna.UI2.WinForms.Guna2Button btnDeleteSection;
        private Guna.UI2.WinForms.Guna2Button btnClearSectionForm;
        private Guna.UI2.WinForms.Guna2Panel pnlSectionsTable;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableSubtitle;
        private Guna.UI2.WinForms.Guna2DataGridView dgvSections;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSectionId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSectionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudentCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudyYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBranch;
    }
}
