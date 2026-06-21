using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class TimeSlotsForm : System.Windows.Forms.UserControl
    {
        private readonly TimeSlotService timeSlotService = new();

        public TimeSlotsForm()
        {
            InitializeComponent();
            ConfigureAutoIdField();
            ConfigureNavigation();
            ConfigureTimeSlotsGrid();
            ConfigureTimeSlotsEvents();
        }

        private void ConfigureAutoIdField()
        {
            txtTimeSlotId.ReadOnly = true;
            txtTimeSlotId.TabStop = false;
            txtTimeSlotId.PlaceholderText = "Auto";
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadTimeSlotsAsync();
            ClearTimeSlotForm();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.TimeSlots);
        }

        private void ConfigureTimeSlotsGrid()
        {
            dgvTimeSlots.AutoGenerateColumns = false;
            dgvTimeSlots.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridStyle.Apply(dgvTimeSlots);
            dgvTimeSlots.CellFormatting += FormatTimeSlotCell;
            dgvTimeSlots.DataBindingComplete += (_, _) => ApplyTimeSlotRowStyles();
        }

        private void ConfigureTimeSlotsEvents()
        {
            dgvTimeSlots.SelectionChanged += (_, _) => PopulateTimeSlotEditorFromSelection();
            txtTimeSlotId.Leave += async (_, _) => await PopulateTimeSlotEditorFromEnteredIdAsync();
            btnAddTimeSlot.Click += async (_, _) => await AddTimeSlotAsync();
            btnUpdateTimeSlot.Click += async (_, _) => await UpdateTimeSlotAsync();
            btnDeleteTimeSlot.Click += async (_, _) => await DeleteTimeSlotAsync();
            tglIsBreak.CheckedChanged += (_, _) => UpdateSlotTypePreview();
        }

        private async Task LoadTimeSlotsAsync()
        {
            SetTimeSlotActionsEnabled(false);

            try
            {
                var timeSlots = await timeSlotService.GetAllAsync();
                dgvTimeSlots.DataSource = timeSlots;
                ApplyTimeSlotRowStyles();
                dgvTimeSlots.ClearSelection();
            }
            catch (Exception ex)
            {
                ShowError("Unable to load time slots.", ex);
            }
            finally
            {
                SetTimeSlotActionsEnabled(true);
            }
        }

        private async Task AddTimeSlotAsync()
        {
            if (!TryBuildTimeSlot(out var timeSlot, requireId: false))
            {
                return;
            }

            await ExecuteTimeSlotActionAsync(
                async () => await timeSlotService.AddAsync(timeSlot),
                "Time slot added successfully.");
        }

        private async Task UpdateTimeSlotAsync()
        {
            if (!TryBuildTimeSlot(out var timeSlot))
            {
                return;
            }

            await ExecuteTimeSlotActionAsync(
                async () => await timeSlotService.UpdateAsync(timeSlot),
                "Time slot updated successfully.");
        }

        private async Task DeleteTimeSlotAsync()
        {
            if (!TryGetTimeSlotIdFromEditor(out int timeSlotId))
            {
                return;
            }

            var confirmation = MessageBox.Show(
                this,
                "Are you sure you want to delete the selected time slot?",
                "Delete Time Slot",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            await ExecuteTimeSlotActionAsync(
                async () => await timeSlotService.DeleteAsync(timeSlotId),
                "Time slot deleted successfully.");
        }

        private async Task ExecuteTimeSlotActionAsync(Func<Task> action, string successMessage)
        {
            SetTimeSlotActionsEnabled(false);

            try
            {
                await action();
                await LoadTimeSlotsAsync();
                ClearTimeSlotForm();
                ShowInformation(successMessage);
            }
            catch (Exception ex)
            {
                ShowError("Unable to complete the time slot operation.", ex);
            }
            finally
            {
                SetTimeSlotActionsEnabled(true);
            }
        }

        private bool TryBuildTimeSlot(out TimeSlot timeSlot, bool requireId = true)
        {
            timeSlot = new TimeSlot
            {
                StartTime = ToMinutePrecision(dtpStartTime.Value),
                EndTime = ToMinutePrecision(dtpEndTime.Value),
                IsBreak = tglIsBreak.Checked
            };

            var timeSlotId = 0;
            if (requireId && !TryGetTimeSlotIdFromEditor(out timeSlotId))
            {
                return false;
            }

            timeSlot.TimeSlotID = requireId ? timeSlotId : 0;

            if (timeSlot.EndTime > timeSlot.StartTime)
            {
                return true;
            }

            ShowInformation("End time must be after start time.");
            dtpEndTime.Focus();
            return false;
        }

        private bool TryGetTimeSlotIdFromEditor(out int timeSlotId)
        {
            if (int.TryParse(txtTimeSlotId.Text, out timeSlotId) && timeSlotId > 0)
            {
                return true;
            }

            ShowInformation("Select a time slot row first.");
            return false;
        }

        private void PopulateTimeSlotEditorFromSelection()
        {
            if (dgvTimeSlots.CurrentRow?.DataBoundItem is not TimeSlot timeSlot)
            {
                return;
            }

            txtTimeSlotId.Text = timeSlot.TimeSlotID.ToString();
            dtpStartTime.Value = DateTime.Today.Add(timeSlot.StartTime);
            dtpEndTime.Value = DateTime.Today.Add(timeSlot.EndTime);
            tglIsBreak.Checked = timeSlot.IsBreak;
            UpdateSlotTypePreview();
        }

        private async Task PopulateTimeSlotEditorFromEnteredIdAsync()
        {
            if (!int.TryParse(txtTimeSlotId.Text, out int timeSlotId) || timeSlotId <= 0)
            {
                return;
            }

            try
            {
                var timeSlot = await timeSlotService.GetByIdAsync(timeSlotId);

                if (timeSlot is null)
                {
                    return;
                }

                dtpStartTime.Value = DateTime.Today.Add(timeSlot.StartTime);
                dtpEndTime.Value = DateTime.Today.Add(timeSlot.EndTime);
                tglIsBreak.Checked = timeSlot.IsBreak;
                UpdateSlotTypePreview();
                SelectTimeSlotRow(timeSlot.TimeSlotID);
            }
            catch (Exception ex)
            {
                ShowError("Unable to load time slot details.", ex);
            }
        }

        private void SelectTimeSlotRow(int timeSlotId)
        {
            foreach (DataGridViewRow row in dgvTimeSlots.Rows)
            {
                if (row.DataBoundItem is not TimeSlot timeSlot || timeSlot.TimeSlotID != timeSlotId)
                {
                    continue;
                }

                row.Selected = true;
                dgvTimeSlots.CurrentCell = row.Cells[0];
                break;
            }
        }

        private void ClearTimeSlotForm()
        {
            txtTimeSlotId.Text = "Auto";
            dtpStartTime.Value = DateTime.Today.AddHours(8);
            dtpEndTime.Value = DateTime.Today.AddHours(9);
            tglIsBreak.Checked = false;
            UpdateSlotTypePreview();
            dgvTimeSlots.ClearSelection();
            dtpStartTime.Focus();
        }

        private void SetTimeSlotActionsEnabled(bool enabled)
        {
            btnAddTimeSlot.Enabled = enabled;
            btnUpdateTimeSlot.Enabled = enabled;
            btnDeleteTimeSlot.Enabled = enabled;
            dgvTimeSlots.Enabled = enabled;
        }

        private void FormatTimeSlotCell(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if ((dgvTimeSlots.Columns[e.ColumnIndex] == colStartTime ||
                 dgvTimeSlots.Columns[e.ColumnIndex] == colEndTime) &&
                e.Value is TimeSpan time)
            {
                e.Value = time.ToString(@"hh\:mm");
                e.FormattingApplied = true;
                return;
            }

            if (dgvTimeSlots.Columns[e.ColumnIndex] == colIsBreak && e.Value is bool isBreak)
            {
                e.Value = isBreak ? "Break period" : "Lecture";
                if (e.CellStyle is not null)
                {
                    e.CellStyle.ForeColor = isBreak
                        ? System.Drawing.Color.FromArgb(146, 64, 14)
                        : System.Drawing.Color.FromArgb(30, 41, 59);
                }

                e.FormattingApplied = true;
            }
        }

        private void ApplyTimeSlotRowStyles()
        {
            foreach (DataGridViewRow row in dgvTimeSlots.Rows)
            {
                if (row.DataBoundItem is not TimeSlot timeSlot || !timeSlot.IsBreak)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.Empty;
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.Empty;
                    row.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Empty;
                    row.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Empty;
                    continue;
                }

                row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 251, 235);
                row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(120, 53, 15);
                row.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(37, 99, 235);
                row.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            }
        }

        private void UpdateSlotTypePreview()
        {
            bool isBreak = tglIsBreak.Checked;

            btnSlotTypeBadge.Text = isBreak ? "Break" : "Lecture";
            btnSlotTypeBadge.FillColor = isBreak
                ? System.Drawing.Color.FromArgb(245, 158, 11)
                : System.Drawing.Color.FromArgb(37, 99, 235);
            btnSlotTypeBadge.HoverState.FillColor = btnSlotTypeBadge.FillColor;
            btnSlotTypeBadge.PressedColor = btnSlotTypeBadge.FillColor;
            tglIsBreak.CheckedState.BorderColor = System.Drawing.Color.FromArgb(245, 158, 11);
            tglIsBreak.CheckedState.FillColor = System.Drawing.Color.FromArgb(245, 158, 11);
        }

        private static TimeSpan ToMinutePrecision(DateTime value)
        {
            return new TimeSpan(value.Hour, value.Minute, 0);
        }

        private void ShowInformation(string message)
        {
            MessageBox.Show(this, message, "Time Slots", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show(this, $"{message}\n\n{ex.Message}", "Time Slots", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            pnlSidebar = new Guna.UI2.WinForms.Guna2Panel();
            lblSidebarFooter = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnNavigationSchedules = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationFaculty = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationTimeSlots = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationClassrooms = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationSubjects = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationStudyYears = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationBranches = new Guna.UI2.WinForms.Guna2Button();
            separatorSidebar = new Guna.UI2.WinForms.Guna2Separator();
            lblSidebarSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblApplicationName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            pnlWorkspace = new Guna.UI2.WinForms.Guna2Panel();
            pnlTimeSlotsTable = new Guna.UI2.WinForms.Guna2Panel();
            dgvTimeSlots = new Guna.UI2.WinForms.Guna2DataGridView();
            colTimeSlotId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colIsBreak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblTableSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTableTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlTimeSlotEditor = new Guna.UI2.WinForms.Guna2Panel();
            btnClearTimeSlotForm = new Guna.UI2.WinForms.Guna2Button();
            btnDeleteTimeSlot = new Guna.UI2.WinForms.Guna2Button();
            btnUpdateTimeSlot = new Guna.UI2.WinForms.Guna2Button();
            btnAddTimeSlot = new Guna.UI2.WinForms.Guna2Button();
            btnSlotTypeBadge = new Guna.UI2.WinForms.Guna2Button();
            tglIsBreak = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            lblIsBreak = new Guna.UI2.WinForms.Guna2HtmlLabel();
            dtpEndTime = new Guna.UI2.WinForms.Guna2DateTimePicker();
            lblEndTime = new Guna.UI2.WinForms.Guna2HtmlLabel();
            dtpStartTime = new Guna.UI2.WinForms.Guna2DateTimePicker();
            lblStartTime = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtTimeSlotId = new Guna.UI2.WinForms.Guna2TextBox();
            lblTimeSlotId = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEditorSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEditorTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            lblPageSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblPageTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlSidebar.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlWorkspace.SuspendLayout();
            pnlTimeSlotsTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTimeSlots).BeginInit();
            pnlTimeSlotEditor.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            pnlSidebar.BackColor = System.Drawing.Color.Transparent;
            pnlSidebar.Controls.Add(lblSidebarFooter);
            pnlSidebar.Controls.Add(btnNavigationSchedules);
            pnlSidebar.Controls.Add(btnNavigationFaculty);
            pnlSidebar.Controls.Add(btnNavigationTimeSlots);
            pnlSidebar.Controls.Add(btnNavigationClassrooms);
            pnlSidebar.Controls.Add(btnNavigationSubjects);
            pnlSidebar.Controls.Add(btnNavigationStudyYears);
            pnlSidebar.Controls.Add(btnNavigationBranches);
            pnlSidebar.Controls.Add(separatorSidebar);
            pnlSidebar.Controls.Add(lblSidebarSubtitle);
            pnlSidebar.Controls.Add(lblApplicationName);
            pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            pnlSidebar.FillColor = System.Drawing.Color.FromArgb(24, 38, 62);
            pnlSidebar.Location = new System.Drawing.Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new System.Drawing.Size(240, 720);
            pnlSidebar.TabIndex = 0;
            lblSidebarFooter.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblSidebarFooter.BackColor = System.Drawing.Color.Transparent;
            lblSidebarFooter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblSidebarFooter.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblSidebarFooter.Location = new System.Drawing.Point(24, 671);
            lblSidebarFooter.Name = "lblSidebarFooter";
            lblSidebarFooter.Size = new System.Drawing.Size(146, 17);
            lblSidebarFooter.TabIndex = 11;
            lblSidebarFooter.Text = "Academic Scheduling Suite";
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
            btnNavigationTimeSlots.BorderRadius = 8;
            btnNavigationTimeSlots.Checked = true;
            btnNavigationTimeSlots.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNavigationTimeSlots.Enabled = false;
            btnNavigationTimeSlots.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnNavigationTimeSlots.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnNavigationTimeSlots.ForeColor = System.Drawing.Color.White;
            btnNavigationTimeSlots.HoverState.FillColor = System.Drawing.Color.FromArgb(29, 78, 216);
            btnNavigationTimeSlots.Location = new System.Drawing.Point(24, 378);
            btnNavigationTimeSlots.Name = "btnNavigationTimeSlots";
            btnNavigationTimeSlots.Size = new System.Drawing.Size(192, 44);
            btnNavigationTimeSlots.TabIndex = 8;
            btnNavigationTimeSlots.Text = "Time Slots";
            btnNavigationTimeSlots.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            btnNavigationTimeSlots.TextOffset = new System.Drawing.Point(14, 0);
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
            separatorSidebar.FillColor = System.Drawing.Color.FromArgb(51, 65, 85);
            separatorSidebar.Location = new System.Drawing.Point(24, 78);
            separatorSidebar.Name = "separatorSidebar";
            separatorSidebar.Size = new System.Drawing.Size(192, 10);
            separatorSidebar.TabIndex = 2;
            lblSidebarSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblSidebarSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblSidebarSubtitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblSidebarSubtitle.Location = new System.Drawing.Point(26, 52);
            lblSidebarSubtitle.Name = "lblSidebarSubtitle";
            lblSidebarSubtitle.Size = new System.Drawing.Size(130, 17);
            lblSidebarSubtitle.TabIndex = 1;
            lblSidebarSubtitle.Text = "Classroom Management";
            lblApplicationName.BackColor = System.Drawing.Color.Transparent;
            lblApplicationName.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblApplicationName.ForeColor = System.Drawing.Color.White;
            lblApplicationName.Location = new System.Drawing.Point(24, 20);
            lblApplicationName.Name = "lblApplicationName";
            lblApplicationName.Size = new System.Drawing.Size(206, 33);
            lblApplicationName.TabIndex = 0;
            lblApplicationName.Text = "University Timetable";
            pnlMain.Controls.Add(pnlWorkspace);
            pnlMain.Controls.Add(pnlHeader);
            pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlMain.FillColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlMain.Location = new System.Drawing.Point(240, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new System.Drawing.Size(940, 720);
            pnlMain.TabIndex = 1;
            pnlWorkspace.Controls.Add(pnlTimeSlotsTable);
            pnlWorkspace.Controls.Add(pnlTimeSlotEditor);
            pnlWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlWorkspace.FillColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlWorkspace.Location = new System.Drawing.Point(0, 88);
            pnlWorkspace.Name = "pnlWorkspace";
            pnlWorkspace.Padding = new System.Windows.Forms.Padding(28, 24, 28, 28);
            pnlWorkspace.Size = new System.Drawing.Size(940, 632);
            pnlWorkspace.TabIndex = 1;
            pnlTimeSlotsTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlTimeSlotsTable.BackColor = System.Drawing.Color.Transparent;
            pnlTimeSlotsTable.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlTimeSlotsTable.BorderRadius = 8;
            pnlTimeSlotsTable.BorderThickness = 1;
            pnlTimeSlotsTable.Controls.Add(dgvTimeSlots);
            pnlTimeSlotsTable.Controls.Add(lblTableSubtitle);
            pnlTimeSlotsTable.Controls.Add(lblTableTitle);
            pnlTimeSlotsTable.FillColor = System.Drawing.Color.White;
            pnlTimeSlotsTable.Location = new System.Drawing.Point(28, 248);
            pnlTimeSlotsTable.Name = "pnlTimeSlotsTable";
            pnlTimeSlotsTable.Size = new System.Drawing.Size(884, 356);
            pnlTimeSlotsTable.TabIndex = 1;
            dgvTimeSlots.AllowUserToAddRows = false;
            dgvTimeSlots.AllowUserToDeleteRows = false;
            dgvTimeSlots.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            dgvTimeSlots.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvTimeSlots.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvTimeSlots.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvTimeSlots.BackgroundColor = System.Drawing.Color.White;
            dgvTimeSlots.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvTimeSlots.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTimeSlots.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvTimeSlots.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvTimeSlots.ColumnHeadersHeight = 44;
            dgvTimeSlots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTimeSlots.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colTimeSlotId, colStartTime, colEndTime, colIsBreak });
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvTimeSlots.DefaultCellStyle = dataGridViewCellStyle3;
            dgvTimeSlots.EnableHeadersVisualStyles = false;
            dgvTimeSlots.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgvTimeSlots.Location = new System.Drawing.Point(24, 78);
            dgvTimeSlots.MultiSelect = false;
            dgvTimeSlots.Name = "dgvTimeSlots";
            dgvTimeSlots.ReadOnly = true;
            dgvTimeSlots.RowHeadersVisible = false;
            dgvTimeSlots.RowTemplate.Height = 42;
            dgvTimeSlots.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvTimeSlots.Size = new System.Drawing.Size(836, 254);
            dgvTimeSlots.TabIndex = 2;
            colTimeSlotId.DataPropertyName = "TimeSlotID";
            colTimeSlotId.FillWeight = 45F;
            colTimeSlotId.HeaderText = "Time Slot ID";
            colTimeSlotId.Name = "colTimeSlotId";
            colTimeSlotId.ReadOnly = true;
            colStartTime.DataPropertyName = "StartTime";
            colStartTime.FillWeight = 85F;
            colStartTime.HeaderText = "Start Time";
            colStartTime.Name = "colStartTime";
            colStartTime.ReadOnly = true;
            colEndTime.DataPropertyName = "EndTime";
            colEndTime.FillWeight = 85F;
            colEndTime.HeaderText = "End Time";
            colEndTime.Name = "colEndTime";
            colEndTime.ReadOnly = true;
            colIsBreak.DataPropertyName = "IsBreak";
            colIsBreak.FillWeight = 65F;
            colIsBreak.HeaderText = "Slot Type";
            colIsBreak.Name = "colIsBreak";
            colIsBreak.ReadOnly = true;
            lblTableSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblTableSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblTableSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTableSubtitle.Location = new System.Drawing.Point(24, 43);
            lblTableSubtitle.Name = "lblTableSubtitle";
            lblTableSubtitle.Size = new System.Drawing.Size(246, 17);
            lblTableSubtitle.TabIndex = 1;
            lblTableSubtitle.Text = "Lecture slots and protected break periods.";
            lblTableTitle.BackColor = System.Drawing.Color.Transparent;
            lblTableTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTableTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblTableTitle.Location = new System.Drawing.Point(24, 18);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new System.Drawing.Size(119, 25);
            lblTableTitle.TabIndex = 0;
            lblTableTitle.Text = "Time Slots List";
            pnlTimeSlotEditor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlTimeSlotEditor.BackColor = System.Drawing.Color.Transparent;
            pnlTimeSlotEditor.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlTimeSlotEditor.BorderRadius = 8;
            pnlTimeSlotEditor.BorderThickness = 1;
            pnlTimeSlotEditor.Controls.Add(btnClearTimeSlotForm);
            pnlTimeSlotEditor.Controls.Add(btnDeleteTimeSlot);
            pnlTimeSlotEditor.Controls.Add(btnUpdateTimeSlot);
            pnlTimeSlotEditor.Controls.Add(btnAddTimeSlot);
            pnlTimeSlotEditor.Controls.Add(btnSlotTypeBadge);
            pnlTimeSlotEditor.Controls.Add(tglIsBreak);
            pnlTimeSlotEditor.Controls.Add(lblIsBreak);
            pnlTimeSlotEditor.Controls.Add(dtpEndTime);
            pnlTimeSlotEditor.Controls.Add(lblEndTime);
            pnlTimeSlotEditor.Controls.Add(dtpStartTime);
            pnlTimeSlotEditor.Controls.Add(lblStartTime);
            pnlTimeSlotEditor.Controls.Add(txtTimeSlotId);
            pnlTimeSlotEditor.Controls.Add(lblTimeSlotId);
            pnlTimeSlotEditor.Controls.Add(lblEditorSubtitle);
            pnlTimeSlotEditor.Controls.Add(lblEditorTitle);
            pnlTimeSlotEditor.FillColor = System.Drawing.Color.White;
            pnlTimeSlotEditor.Location = new System.Drawing.Point(28, 24);
            pnlTimeSlotEditor.Name = "pnlTimeSlotEditor";
            pnlTimeSlotEditor.Size = new System.Drawing.Size(884, 202);
            pnlTimeSlotEditor.TabIndex = 0;
            btnClearTimeSlotForm.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClearTimeSlotForm.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnClearTimeSlotForm.BorderRadius = 8;
            btnClearTimeSlotForm.BorderThickness = 1;
            btnClearTimeSlotForm.Cursor = System.Windows.Forms.Cursors.Hand;
            btnClearTimeSlotForm.FillColor = System.Drawing.Color.White;
            btnClearTimeSlotForm.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnClearTimeSlotForm.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            btnClearTimeSlotForm.HoverState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            btnClearTimeSlotForm.Location = new System.Drawing.Point(752, 142);
            btnClearTimeSlotForm.Name = "btnClearTimeSlotForm";
            btnClearTimeSlotForm.Size = new System.Drawing.Size(108, 38);
            btnClearTimeSlotForm.TabIndex = 13;
            btnClearTimeSlotForm.Text = "Clear";
            btnClearTimeSlotForm.Visible = false;
            btnDeleteTimeSlot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnDeleteTimeSlot.BorderRadius = 8;
            btnDeleteTimeSlot.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDeleteTimeSlot.FillColor = System.Drawing.Color.FromArgb(220, 38, 38);
            btnDeleteTimeSlot.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnDeleteTimeSlot.ForeColor = System.Drawing.Color.White;
            btnDeleteTimeSlot.HoverState.FillColor = System.Drawing.Color.FromArgb(185, 28, 28);
            btnDeleteTimeSlot.Location = new System.Drawing.Point(632, 142);
            btnDeleteTimeSlot.Name = "btnDeleteTimeSlot";
            btnDeleteTimeSlot.Size = new System.Drawing.Size(108, 38);
            btnDeleteTimeSlot.TabIndex = 12;
            btnDeleteTimeSlot.Text = "Delete";
            btnUpdateTimeSlot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnUpdateTimeSlot.BorderRadius = 8;
            btnUpdateTimeSlot.Cursor = System.Windows.Forms.Cursors.Hand;
            btnUpdateTimeSlot.FillColor = System.Drawing.Color.FromArgb(14, 116, 144);
            btnUpdateTimeSlot.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnUpdateTimeSlot.ForeColor = System.Drawing.Color.White;
            btnUpdateTimeSlot.HoverState.FillColor = System.Drawing.Color.FromArgb(21, 94, 117);
            btnUpdateTimeSlot.Location = new System.Drawing.Point(752, 98);
            btnUpdateTimeSlot.Name = "btnUpdateTimeSlot";
            btnUpdateTimeSlot.Size = new System.Drawing.Size(108, 38);
            btnUpdateTimeSlot.TabIndex = 11;
            btnUpdateTimeSlot.Text = "Update";
            btnAddTimeSlot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAddTimeSlot.BorderRadius = 8;
            btnAddTimeSlot.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAddTimeSlot.FillColor = System.Drawing.Color.FromArgb(22, 163, 74);
            btnAddTimeSlot.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnAddTimeSlot.ForeColor = System.Drawing.Color.White;
            btnAddTimeSlot.HoverState.FillColor = System.Drawing.Color.FromArgb(21, 128, 61);
            btnAddTimeSlot.Location = new System.Drawing.Point(632, 98);
            btnAddTimeSlot.Name = "btnAddTimeSlot";
            btnAddTimeSlot.Size = new System.Drawing.Size(108, 38);
            btnAddTimeSlot.TabIndex = 10;
            btnAddTimeSlot.Text = "Add";
            btnSlotTypeBadge.BorderRadius = 16;
            btnSlotTypeBadge.Cursor = System.Windows.Forms.Cursors.Default;
            btnSlotTypeBadge.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnSlotTypeBadge.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnSlotTypeBadge.ForeColor = System.Drawing.Color.White;
            btnSlotTypeBadge.Location = new System.Drawing.Point(500, 116);
            btnSlotTypeBadge.Name = "btnSlotTypeBadge";
            btnSlotTypeBadge.PressedColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnSlotTypeBadge.Size = new System.Drawing.Size(104, 32);
            btnSlotTypeBadge.TabIndex = 14;
            btnSlotTypeBadge.TabStop = false;
            btnSlotTypeBadge.Text = "Lecture";
            tglIsBreak.CheckedState.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            tglIsBreak.CheckedState.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            tglIsBreak.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            tglIsBreak.CheckedState.InnerColor = System.Drawing.Color.White;
            tglIsBreak.Location = new System.Drawing.Point(438, 119);
            tglIsBreak.Name = "tglIsBreak";
            tglIsBreak.Size = new System.Drawing.Size(52, 28);
            tglIsBreak.TabIndex = 9;
            tglIsBreak.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            tglIsBreak.UncheckedState.FillColor = System.Drawing.Color.FromArgb(203, 213, 225);
            tglIsBreak.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            tglIsBreak.UncheckedState.InnerColor = System.Drawing.Color.White;
            lblIsBreak.BackColor = System.Drawing.Color.Transparent;
            lblIsBreak.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblIsBreak.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblIsBreak.Location = new System.Drawing.Point(438, 91);
            lblIsBreak.Name = "lblIsBreak";
            lblIsBreak.Size = new System.Drawing.Size(56, 19);
            lblIsBreak.TabIndex = 8;
            lblIsBreak.Text = "Slot Type";
            dtpEndTime.BorderRadius = 8;
            dtpEndTime.Checked = true;
            dtpEndTime.CustomFormat = "HH:mm";
            dtpEndTime.FillColor = System.Drawing.Color.White;
            dtpEndTime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtpEndTime.Location = new System.Drawing.Point(314, 112);
            dtpEndTime.Name = "dtpEndTime";
            dtpEndTime.ShowUpDown = true;
            dtpEndTime.Size = new System.Drawing.Size(96, 42);
            dtpEndTime.TabIndex = 7;
            lblEndTime.BackColor = System.Drawing.Color.Transparent;
            lblEndTime.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblEndTime.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblEndTime.Location = new System.Drawing.Point(314, 87);
            lblEndTime.Name = "lblEndTime";
            lblEndTime.Size = new System.Drawing.Size(60, 19);
            lblEndTime.TabIndex = 6;
            lblEndTime.Text = "End Time";
            dtpStartTime.BorderRadius = 8;
            dtpStartTime.Checked = true;
            dtpStartTime.CustomFormat = "HH:mm";
            dtpStartTime.FillColor = System.Drawing.Color.White;
            dtpStartTime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtpStartTime.Location = new System.Drawing.Point(190, 112);
            dtpStartTime.Name = "dtpStartTime";
            dtpStartTime.ShowUpDown = true;
            dtpStartTime.Size = new System.Drawing.Size(96, 42);
            dtpStartTime.TabIndex = 5;
            lblStartTime.BackColor = System.Drawing.Color.Transparent;
            lblStartTime.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblStartTime.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblStartTime.Location = new System.Drawing.Point(190, 87);
            lblStartTime.Name = "lblStartTime";
            lblStartTime.Size = new System.Drawing.Size(64, 19);
            lblStartTime.TabIndex = 4;
            lblStartTime.Text = "Start Time";
            txtTimeSlotId.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtTimeSlotId.BorderRadius = 8;
            txtTimeSlotId.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtTimeSlotId.DefaultText = "";
            txtTimeSlotId.DisabledState.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtTimeSlotId.DisabledState.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtTimeSlotId.DisabledState.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            txtTimeSlotId.Enabled = true;
            txtTimeSlotId.FillColor = System.Drawing.Color.White;
            txtTimeSlotId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtTimeSlotId.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            txtTimeSlotId.Location = new System.Drawing.Point(24, 112);
            txtTimeSlotId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtTimeSlotId.Name = "txtTimeSlotId";
            txtTimeSlotId.PasswordChar = '\0';
            txtTimeSlotId.PlaceholderForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            txtTimeSlotId.PlaceholderText = "Enter ID";
            txtTimeSlotId.SelectedText = "";
            txtTimeSlotId.Size = new System.Drawing.Size(132, 42);
            txtTimeSlotId.TabIndex = 3;
            lblTimeSlotId.BackColor = System.Drawing.Color.Transparent;
            lblTimeSlotId.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTimeSlotId.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblTimeSlotId.Location = new System.Drawing.Point(24, 87);
            lblTimeSlotId.Name = "lblTimeSlotId";
            lblTimeSlotId.Size = new System.Drawing.Size(76, 19);
            lblTimeSlotId.TabIndex = 2;
            lblTimeSlotId.Text = "Time Slot ID";
            lblEditorSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblEditorSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblEditorSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblEditorSubtitle.Location = new System.Drawing.Point(24, 44);
            lblEditorSubtitle.Name = "lblEditorSubtitle";
            lblEditorSubtitle.Size = new System.Drawing.Size(288, 17);
            lblEditorSubtitle.TabIndex = 1;
            lblEditorSubtitle.Text = "Prepare time slot details before applying an action.";
            lblEditorTitle.BackColor = System.Drawing.Color.Transparent;
            lblEditorTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblEditorTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblEditorTitle.Location = new System.Drawing.Point(24, 18);
            lblEditorTitle.Name = "lblEditorTitle";
            lblEditorTitle.Size = new System.Drawing.Size(141, 25);
            lblEditorTitle.TabIndex = 0;
            lblEditorTitle.Text = "Time Slot Details";
            pnlHeader.Controls.Add(lblPageSubtitle);
            pnlHeader.Controls.Add(lblPageTitle);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.FillColor = System.Drawing.Color.White;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(940, 88);
            pnlHeader.TabIndex = 0;
            lblPageSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblPageSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblPageSubtitle.Location = new System.Drawing.Point(32, 50);
            lblPageSubtitle.Name = "lblPageSubtitle";
            lblPageSubtitle.Size = new System.Drawing.Size(333, 19);
            lblPageSubtitle.TabIndex = 1;
            lblPageSubtitle.Text = "Manage lecture and break time ranges used by schedules.";
            lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            lblPageTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblPageTitle.Location = new System.Drawing.Point(32, 16);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new System.Drawing.Size(296, 34);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Time Slots Management";
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            ClientSize = new System.Drawing.Size(1180, 720);
            Controls.Add(pnlMain);
            Controls.Add(pnlSidebar);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            MinimumSize = new System.Drawing.Size(980, 600);
            Name = "TimeSlotsForm";
            Text = "Time Slots Management";
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlWorkspace.ResumeLayout(false);
            pnlTimeSlotsTable.ResumeLayout(false);
            pnlTimeSlotsTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTimeSlots).EndInit();
            pnlTimeSlotEditor.ResumeLayout(false);
            pnlTimeSlotEditor.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
        }
        private Guna.UI2.WinForms.Guna2Panel pnlSidebar = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblApplicationName = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSidebarSubtitle = null!;
        private Guna.UI2.WinForms.Guna2Separator separatorSidebar = null!;
        private Guna.UI2.WinForms.Guna2Button btnNavigationBranches = null!;
        private Guna.UI2.WinForms.Guna2Button btnNavigationStudyYears = null!;
        private Guna.UI2.WinForms.Guna2Button btnNavigationSubjects = null!;
        private Guna.UI2.WinForms.Guna2Button btnNavigationClassrooms = null!;
        private Guna.UI2.WinForms.Guna2Button btnNavigationTimeSlots = null!;
        private Guna.UI2.WinForms.Guna2Button btnNavigationFaculty = null!;
        private Guna.UI2.WinForms.Guna2Button btnNavigationSchedules = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSidebarFooter = null!;
        private Guna.UI2.WinForms.Guna2Panel pnlMain = null!;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageTitle = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageSubtitle = null!;
        private Guna.UI2.WinForms.Guna2Panel pnlWorkspace = null!;
        private Guna.UI2.WinForms.Guna2Panel pnlTimeSlotEditor = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEditorTitle = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEditorSubtitle = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTimeSlotId = null!;
        private Guna.UI2.WinForms.Guna2TextBox txtTimeSlotId = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblStartTime = null!;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpStartTime = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEndTime = null!;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpEndTime = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblIsBreak = null!;
        private Guna.UI2.WinForms.Guna2ToggleSwitch tglIsBreak = null!;
        private Guna.UI2.WinForms.Guna2Button btnSlotTypeBadge = null!;
        private Guna.UI2.WinForms.Guna2Button btnAddTimeSlot = null!;
        private Guna.UI2.WinForms.Guna2Button btnUpdateTimeSlot = null!;
        private Guna.UI2.WinForms.Guna2Button btnDeleteTimeSlot = null!;
        private Guna.UI2.WinForms.Guna2Button btnClearTimeSlotForm = null!;
        private Guna.UI2.WinForms.Guna2Panel pnlTimeSlotsTable = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableTitle = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableSubtitle = null!;
        private Guna.UI2.WinForms.Guna2DataGridView dgvTimeSlots = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeSlotId = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartTime = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEndTime = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsBreak = null!;
}
}
