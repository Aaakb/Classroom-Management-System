namespace University_Timetable_and_Classroom_Management_System
{
    public partial class BranchesForm : System.Windows.Forms.Form
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
            btnNavigationBranches = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationDashboard = new Guna.UI2.WinForms.Guna2Button();
            separatorSidebar = new Guna.UI2.WinForms.Guna2Separator();
            lblSidebarSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblApplicationName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            pnlWorkspace = new Guna.UI2.WinForms.Guna2Panel();
            pnlBranchesTable = new Guna.UI2.WinForms.Guna2Panel();
            dgvBranches = new Guna.UI2.WinForms.Guna2DataGridView();
            colBranchId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colBranchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblTableSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTableTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlBranchEditor = new Guna.UI2.WinForms.Guna2Panel();
            btnClearBranchForm = new Guna.UI2.WinForms.Guna2Button();
            btnDeleteBranch = new Guna.UI2.WinForms.Guna2Button();
            btnUpdateBranch = new Guna.UI2.WinForms.Guna2Button();
            btnAddBranch = new Guna.UI2.WinForms.Guna2Button();
            txtBranchName = new Guna.UI2.WinForms.Guna2TextBox();
            lblBranchName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtBranchId = new Guna.UI2.WinForms.Guna2TextBox();
            lblBranchId = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEditorSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEditorTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            lblPageSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblPageTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlSidebar.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlWorkspace.SuspendLayout();
            pnlBranchesTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBranches).BeginInit();
            pnlBranchEditor.SuspendLayout();
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
            lblSidebarFooter.TabIndex = 8;
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
            btnNavigationSchedules.Location = new System.Drawing.Point(24, 322);
            btnNavigationSchedules.Name = "btnNavigationSchedules";
            btnNavigationSchedules.Size = new System.Drawing.Size(192, 44);
            btnNavigationSchedules.TabIndex = 7;
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
            btnNavigationFaculty.Location = new System.Drawing.Point(24, 266);
            btnNavigationFaculty.Name = "btnNavigationFaculty";
            btnNavigationFaculty.Size = new System.Drawing.Size(192, 44);
            btnNavigationFaculty.TabIndex = 6;
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
            btnNavigationClassrooms.Location = new System.Drawing.Point(24, 210);
            btnNavigationClassrooms.Name = "btnNavigationClassrooms";
            btnNavigationClassrooms.Size = new System.Drawing.Size(192, 44);
            btnNavigationClassrooms.TabIndex = 5;
            btnNavigationClassrooms.Text = "Classrooms";
            btnNavigationClassrooms.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            btnNavigationClassrooms.TextOffset = new System.Drawing.Point(14, 0);
            // 
            // btnNavigationBranches
            // 
            btnNavigationBranches.BorderRadius = 8;
            btnNavigationBranches.Checked = true;
            btnNavigationBranches.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationBranches.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnNavigationBranches.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationBranches.ForeColor = System.Drawing.Color.White;
            btnNavigationBranches.HoverState.FillColor = System.Drawing.Color.FromArgb(29, 78, 216);
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
            pnlWorkspace.Controls.Add(pnlBranchesTable);
            pnlWorkspace.Controls.Add(pnlBranchEditor);
            pnlWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlWorkspace.FillColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlWorkspace.Location = new System.Drawing.Point(0, 88);
            pnlWorkspace.Name = "pnlWorkspace";
            pnlWorkspace.Padding = new System.Windows.Forms.Padding(28, 24, 28, 28);
            pnlWorkspace.Size = new System.Drawing.Size(940, 632);
            pnlWorkspace.TabIndex = 1;
            // 
            // pnlBranchesTable
            // 
            pnlBranchesTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlBranchesTable.BackColor = System.Drawing.Color.Transparent;
            pnlBranchesTable.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlBranchesTable.BorderRadius = 8;
            pnlBranchesTable.BorderThickness = 1;
            pnlBranchesTable.Controls.Add(dgvBranches);
            pnlBranchesTable.Controls.Add(lblTableSubtitle);
            pnlBranchesTable.Controls.Add(lblTableTitle);
            pnlBranchesTable.FillColor = System.Drawing.Color.White;
            pnlBranchesTable.Location = new System.Drawing.Point(28, 236);
            pnlBranchesTable.Name = "pnlBranchesTable";
            pnlBranchesTable.Size = new System.Drawing.Size(884, 368);
            pnlBranchesTable.TabIndex = 1;
            // 
            // dgvBranches
            // 
            dgvBranches.AllowUserToAddRows = false;
            dgvBranches.AllowUserToDeleteRows = false;
            dgvBranches.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            dgvBranches.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvBranches.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvBranches.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvBranches.BackgroundColor = System.Drawing.Color.White;
            dgvBranches.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvBranches.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvBranches.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvBranches.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvBranches.ColumnHeadersHeight = 44;
            dgvBranches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvBranches.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colBranchId, colBranchName });
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvBranches.DefaultCellStyle = dataGridViewCellStyle3;
            dgvBranches.EnableHeadersVisualStyles = false;
            dgvBranches.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgvBranches.Location = new System.Drawing.Point(24, 78);
            dgvBranches.MultiSelect = false;
            dgvBranches.Name = "dgvBranches";
            dgvBranches.ReadOnly = true;
            dgvBranches.RowHeadersVisible = false;
            dgvBranches.RowTemplate.Height = 42;
            dgvBranches.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvBranches.Size = new System.Drawing.Size(836, 266);
            dgvBranches.TabIndex = 2;
            // 
            // colBranchId
            // 
            colBranchId.DataPropertyName = "BranchID";
            colBranchId.FillWeight = 34F;
            colBranchId.HeaderText = "Branch ID";
            colBranchId.Name = "colBranchId";
            colBranchId.ReadOnly = true;
            // 
            // colBranchName
            // 
            colBranchName.DataPropertyName = "BranchName";
            colBranchName.FillWeight = 166F;
            colBranchName.HeaderText = "Branch Name";
            colBranchName.Name = "colBranchName";
            colBranchName.ReadOnly = true;
            // 
            // lblTableSubtitle
            // 
            lblTableSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblTableSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblTableSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTableSubtitle.Location = new System.Drawing.Point(24, 43);
            lblTableSubtitle.Name = "lblTableSubtitle";
            lblTableSubtitle.Size = new System.Drawing.Size(230, 17);
            lblTableSubtitle.TabIndex = 1;
            lblTableSubtitle.Text = "Review and select academic branch records.";
            // 
            // lblTableTitle
            // 
            lblTableTitle.BackColor = System.Drawing.Color.Transparent;
            lblTableTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTableTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblTableTitle.Location = new System.Drawing.Point(24, 18);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new System.Drawing.Size(114, 25);
            lblTableTitle.TabIndex = 0;
            lblTableTitle.Text = "Branches List";
            // 
            // pnlBranchEditor
            // 
            pnlBranchEditor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlBranchEditor.BackColor = System.Drawing.Color.Transparent;
            pnlBranchEditor.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlBranchEditor.BorderRadius = 8;
            pnlBranchEditor.BorderThickness = 1;
            pnlBranchEditor.Controls.Add(btnClearBranchForm);
            pnlBranchEditor.Controls.Add(btnDeleteBranch);
            pnlBranchEditor.Controls.Add(btnUpdateBranch);
            pnlBranchEditor.Controls.Add(btnAddBranch);
            pnlBranchEditor.Controls.Add(txtBranchName);
            pnlBranchEditor.Controls.Add(lblBranchName);
            pnlBranchEditor.Controls.Add(txtBranchId);
            pnlBranchEditor.Controls.Add(lblBranchId);
            pnlBranchEditor.Controls.Add(lblEditorSubtitle);
            pnlBranchEditor.Controls.Add(lblEditorTitle);
            pnlBranchEditor.FillColor = System.Drawing.Color.White;
            pnlBranchEditor.Location = new System.Drawing.Point(28, 24);
            pnlBranchEditor.Name = "pnlBranchEditor";
            pnlBranchEditor.Size = new System.Drawing.Size(884, 190);
            pnlBranchEditor.TabIndex = 0;
            // 
            // btnClearBranchForm
            // 
            btnClearBranchForm.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClearBranchForm.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnClearBranchForm.BorderRadius = 8;
            btnClearBranchForm.BorderThickness = 1;
            btnClearBranchForm.Cursor = System.Windows.Forms.Cursors.Hand;
            btnClearBranchForm.FillColor = System.Drawing.Color.White;
            btnClearBranchForm.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnClearBranchForm.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            btnClearBranchForm.HoverState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            btnClearBranchForm.Location = new System.Drawing.Point(752, 135);
            btnClearBranchForm.Name = "btnClearBranchForm";
            btnClearBranchForm.Size = new System.Drawing.Size(108, 38);
            btnClearBranchForm.TabIndex = 9;
            btnClearBranchForm.Text = "Clear";
            // 
            // btnDeleteBranch
            // 
            btnDeleteBranch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnDeleteBranch.BorderRadius = 8;
            btnDeleteBranch.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDeleteBranch.FillColor = System.Drawing.Color.FromArgb(220, 38, 38);
            btnDeleteBranch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnDeleteBranch.ForeColor = System.Drawing.Color.White;
            btnDeleteBranch.HoverState.FillColor = System.Drawing.Color.FromArgb(185, 28, 28);
            btnDeleteBranch.Location = new System.Drawing.Point(632, 135);
            btnDeleteBranch.Name = "btnDeleteBranch";
            btnDeleteBranch.Size = new System.Drawing.Size(108, 38);
            btnDeleteBranch.TabIndex = 8;
            btnDeleteBranch.Text = "Delete";
            // 
            // btnUpdateBranch
            // 
            btnUpdateBranch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnUpdateBranch.BorderRadius = 8;
            btnUpdateBranch.Cursor = System.Windows.Forms.Cursors.Hand;
            btnUpdateBranch.FillColor = System.Drawing.Color.FromArgb(14, 116, 144);
            btnUpdateBranch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnUpdateBranch.ForeColor = System.Drawing.Color.White;
            btnUpdateBranch.HoverState.FillColor = System.Drawing.Color.FromArgb(21, 94, 117);
            btnUpdateBranch.Location = new System.Drawing.Point(752, 91);
            btnUpdateBranch.Name = "btnUpdateBranch";
            btnUpdateBranch.Size = new System.Drawing.Size(108, 38);
            btnUpdateBranch.TabIndex = 7;
            btnUpdateBranch.Text = "Update";
            // 
            // btnAddBranch
            // 
            btnAddBranch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAddBranch.BorderRadius = 8;
            btnAddBranch.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAddBranch.FillColor = System.Drawing.Color.FromArgb(22, 163, 74);
            btnAddBranch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnAddBranch.ForeColor = System.Drawing.Color.White;
            btnAddBranch.HoverState.FillColor = System.Drawing.Color.FromArgb(21, 128, 61);
            btnAddBranch.Location = new System.Drawing.Point(632, 91);
            btnAddBranch.Name = "btnAddBranch";
            btnAddBranch.Size = new System.Drawing.Size(108, 38);
            btnAddBranch.TabIndex = 6;
            btnAddBranch.Text = "Add";
            // 
            // txtBranchName
            // 
            txtBranchName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtBranchName.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            txtBranchName.BorderRadius = 8;
            txtBranchName.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtBranchName.DefaultText = "";
            txtBranchName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtBranchName.DisabledState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtBranchName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtBranchName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            txtBranchName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtBranchName.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            txtBranchName.HoverState.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            txtBranchName.Location = new System.Drawing.Point(240, 112);
            txtBranchName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtBranchName.Name = "txtBranchName";
            txtBranchName.PasswordChar = '\0';
            txtBranchName.PlaceholderForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            txtBranchName.PlaceholderText = "Enter branch name";
            txtBranchName.SelectedText = "";
            txtBranchName.Size = new System.Drawing.Size(350, 42);
            txtBranchName.TabIndex = 5;
            // 
            // lblBranchName
            // 
            lblBranchName.BackColor = System.Drawing.Color.Transparent;
            lblBranchName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblBranchName.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblBranchName.Location = new System.Drawing.Point(240, 87);
            lblBranchName.Name = "lblBranchName";
            lblBranchName.Size = new System.Drawing.Size(83, 19);
            lblBranchName.TabIndex = 4;
            lblBranchName.Text = "Branch Name";
            // 
            // txtBranchId
            // 
            txtBranchId.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtBranchId.BorderRadius = 8;
            txtBranchId.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtBranchId.DefaultText = "";
            txtBranchId.DisabledState.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtBranchId.DisabledState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtBranchId.DisabledState.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtBranchId.Enabled = false;
            txtBranchId.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtBranchId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtBranchId.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtBranchId.Location = new System.Drawing.Point(24, 112);
            txtBranchId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtBranchId.Name = "txtBranchId";
            txtBranchId.PasswordChar = '\0';
            txtBranchId.PlaceholderForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            txtBranchId.PlaceholderText = "Auto";
            txtBranchId.SelectedText = "";
            txtBranchId.Size = new System.Drawing.Size(190, 42);
            txtBranchId.TabIndex = 3;
            // 
            // lblBranchId
            // 
            lblBranchId.BackColor = System.Drawing.Color.Transparent;
            lblBranchId.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblBranchId.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblBranchId.Location = new System.Drawing.Point(24, 87);
            lblBranchId.Name = "lblBranchId";
            lblBranchId.Size = new System.Drawing.Size(60, 19);
            lblBranchId.TabIndex = 2;
            lblBranchId.Text = "Branch ID";
            // 
            // lblEditorSubtitle
            // 
            lblEditorSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblEditorSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblEditorSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblEditorSubtitle.Location = new System.Drawing.Point(24, 44);
            lblEditorSubtitle.Name = "lblEditorSubtitle";
            lblEditorSubtitle.Size = new System.Drawing.Size(262, 17);
            lblEditorSubtitle.TabIndex = 1;
            lblEditorSubtitle.Text = "Prepare branch details before applying an action.";
            // 
            // lblEditorTitle
            // 
            lblEditorTitle.BackColor = System.Drawing.Color.Transparent;
            lblEditorTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblEditorTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblEditorTitle.Location = new System.Drawing.Point(24, 18);
            lblEditorTitle.Name = "lblEditorTitle";
            lblEditorTitle.Size = new System.Drawing.Size(120, 25);
            lblEditorTitle.TabIndex = 0;
            lblEditorTitle.Text = "Branch Details";
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
            lblPageSubtitle.Size = new System.Drawing.Size(295, 19);
            lblPageSubtitle.TabIndex = 1;
            lblPageSubtitle.Text = "Manage academic branches in a structured layout.";
            // 
            // lblPageTitle
            // 
            lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            lblPageTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblPageTitle.Location = new System.Drawing.Point(32, 16);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new System.Drawing.Size(268, 34);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Branches Management";
            // 
            // BranchesForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            ClientSize = new System.Drawing.Size(1180, 720);
            Controls.Add(pnlMain);
            Controls.Add(pnlSidebar);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            MinimumSize = new System.Drawing.Size(980, 600);
            Name = "BranchesForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Branches Management";
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlWorkspace.ResumeLayout(false);
            pnlBranchesTable.ResumeLayout(false);
            pnlBranchesTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBranches).EndInit();
            pnlBranchEditor.ResumeLayout(false);
            pnlBranchEditor.PerformLayout();
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
        private Guna.UI2.WinForms.Guna2Button btnNavigationClassrooms;
        private Guna.UI2.WinForms.Guna2Button btnNavigationFaculty;
        private Guna.UI2.WinForms.Guna2Button btnNavigationSchedules;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSidebarFooter;
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageSubtitle;
        private Guna.UI2.WinForms.Guna2Panel pnlWorkspace;
        private Guna.UI2.WinForms.Guna2Panel pnlBranchEditor;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEditorTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEditorSubtitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblBranchId;
        private Guna.UI2.WinForms.Guna2TextBox txtBranchId;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblBranchName;
        private Guna.UI2.WinForms.Guna2TextBox txtBranchName;
        private Guna.UI2.WinForms.Guna2Button btnAddBranch;
        private Guna.UI2.WinForms.Guna2Button btnUpdateBranch;
        private Guna.UI2.WinForms.Guna2Button btnDeleteBranch;
        private Guna.UI2.WinForms.Guna2Button btnClearBranchForm;
        private Guna.UI2.WinForms.Guna2Panel pnlBranchesTable;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableSubtitle;
        private Guna.UI2.WinForms.Guna2DataGridView dgvBranches;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBranchId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBranchName;
    }
}
