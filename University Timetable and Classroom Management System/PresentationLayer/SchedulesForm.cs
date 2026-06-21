using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SchedulesForm : System.Windows.Forms.UserControl
    {
        private readonly ScheduleService scheduleService = new();
        private readonly SchedulePdfExportService schedulePdfExportService = new();
        private readonly SubjectService subjectService = new();
        private readonly FacultyMemberService facultyMemberService = new();
        private readonly ClassroomService classroomService = new();
        private readonly TimeSlotService timeSlotService = new();
        private readonly StudyYearService studyYearService = new();
        private readonly BranchService branchService = new();
        private readonly SectionService sectionService = new();

        private List<ScheduleRow> scheduleRows = [];
        private List<Subject> subjectsLookup = [];
        private List<StudyYear> studyYearsLookup = [];
        private List<Branch> branchesLookup = [];
        private List<Section> sectionsLookup = [];
        private bool isUpdatingScheduleLookups;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSemesterFilter = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSemesterFilter = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbLectureTypeFilter = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLectureTypeFilter = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbGroupFilter = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblGroupFilter = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbLectureType = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLectureType = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbGroupName = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblGroupName = null!;

        public SchedulesForm()
        {
            InitializeComponent();
            ConfigureSemesterFilterControl();
            ConfigureLectureTypeAndGroupControls();
            ConfigureScheduleCommands();
            ConfigureNavigation();
            ConfigureScheduleGrid();
            ConfigureScheduleEvents();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await RefreshSchedulesAsync();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.Schedules);
        }

        private void ConfigureScheduleCommands()
        {
            btnGenerateSchedule.Text = "Generate";
            btnClearScheduleForm.Text = "Clear View";

            var toolTip = new ToolTip
            {
                AutoPopDelay = 8000,
                InitialDelay = 500,
                ReshowDelay = 200,
                ShowAlways = true
            };

            toolTip.SetToolTip(btnGenerateSchedule, "Builds a new generated timetable and replaces the current generated records.");
            toolTip.SetToolTip(btnClearScheduleForm, "Clears the current table view only. Database records are not deleted.");
            toolTip.SetToolTip(cmbLectureType, "Theory uses the whole section. Practical requires a group.");
            toolTip.SetToolTip(cmbGroupName, "Available only for practical sessions in first and second year.");
        }

        private void ConfigureScheduleGrid()
        {
            dgvSchedules.AutoGenerateColumns = false;
            dgvSchedules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSchedules.BackgroundColor = Color.White;
            dgvSchedules.BorderStyle = BorderStyle.None;
            dgvSchedules.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSchedules.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvSchedules.ColumnHeadersHeight = 42;
            dgvSchedules.EnableHeadersVisualStyles = false;
            dgvSchedules.RowTemplate.Height = 38;
            dgvSchedules.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridStyle.Apply(dgvSchedules);
            colScheduleId.DataPropertyName = nameof(ScheduleRow.ScheduleID);
            colScheduleId.Visible = false;
            EnsureSemesterColumn();
            colDayOfWeek.DataPropertyName = nameof(ScheduleRow.DayOfWeek);
            colSubject.DataPropertyName = nameof(ScheduleRow.SubjectName);
            colFacultyMember.DataPropertyName = nameof(ScheduleRow.FacultyMemberName);
            colClassroom.DataPropertyName = nameof(ScheduleRow.ClassroomName);
            colTimeSlot.DataPropertyName = nameof(ScheduleRow.StartTimeText);
            colTimeSlot.HeaderText = "Start";
            colStudyYear.DataPropertyName = nameof(ScheduleRow.StudyYearName);
            colBranch.DataPropertyName = nameof(ScheduleRow.BranchName);
            colSection.DataPropertyName = nameof(ScheduleRow.SectionName);
            ApplyScheduleGridColumnLayout();
        }

        private void ConfigureSemesterFilterControl()
        {
            lblSemesterFilter = new Guna.UI2.WinForms.Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(984, 14),
                Name = "lblSemesterFilter",
                Size = new Size(55, 17),
                TabIndex = 8,
                Text = "Semester"
            };

            cmbSemesterFilter = new Guna.UI2.WinForms.Guna2ComboBox
            {
                BackColor = Color.Transparent,
                BorderColor = Color.FromArgb(203, 213, 225),
                BorderRadius = 8,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FocusedColor = Color.FromArgb(37, 99, 235),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(15, 23, 42),
                ItemHeight = 30,
                Location = new Point(984, 36),
                Name = "cmbSemesterFilter",
                Size = new Size(92, 36),
                TabIndex = 9
            };

            lblGroupFilter = new Guna.UI2.WinForms.Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(896, 14),
                Name = "lblGroupFilter",
                Size = new Size(38, 17),
                TabIndex = 10,
                Text = "Group"
            };

            cmbGroupFilter = new Guna.UI2.WinForms.Guna2ComboBox
            {
                BackColor = Color.Transparent,
                BorderColor = Color.FromArgb(203, 213, 225),
                BorderRadius = 8,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FocusedColor = Color.FromArgb(37, 99, 235),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(15, 23, 42),
                ItemHeight = 30,
                Location = new Point(896, 36),
                Name = "cmbGroupFilter",
                Size = new Size(72, 36),
                TabIndex = 11
            };

            lblLectureTypeFilter = new Guna.UI2.WinForms.Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(1092, 14),
                Name = "lblLectureTypeFilter",
                Size = new Size(29, 17),
                TabIndex = 12,
                Text = "Type"
            };

            cmbLectureTypeFilter = new Guna.UI2.WinForms.Guna2ComboBox
            {
                BackColor = Color.Transparent,
                BorderColor = Color.FromArgb(203, 213, 225),
                BorderRadius = 8,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FocusedColor = Color.FromArgb(37, 99, 235),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(15, 23, 42),
                ItemHeight = 30,
                Location = new Point(1092, 36),
                Name = "cmbLectureTypeFilter",
                Size = new Size(88, 36),
                TabIndex = 13
            };

            cmbSemesterFilter.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbGroupFilter.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbLectureTypeFilter.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            pnlScheduleFilters.Controls.Add(cmbGroupFilter);
            pnlScheduleFilters.Controls.Add(lblGroupFilter);
            pnlScheduleFilters.Controls.Add(cmbSemesterFilter);
            pnlScheduleFilters.Controls.Add(lblSemesterFilter);
            pnlScheduleFilters.Controls.Add(cmbLectureTypeFilter);
            pnlScheduleFilters.Controls.Add(lblLectureTypeFilter);
        }

        private void ConfigureLectureTypeAndGroupControls()
        {
            lblLectureType = CreateEditorLabel("Lecture Type", new Point(200, 119), "lblLectureType");
            cmbLectureType = CreateEditorComboBox(new Point(200, 144), "cmbLectureType");
            cmbLectureType.Items.AddRange(new object[] { "Theory", "Practical" });
            cmbLectureType.SelectedIndex = 0;

            lblGroupName = CreateEditorLabel("Group", new Point(376, 119), "lblGroupName");
            cmbGroupName = CreateEditorComboBox(new Point(376, 144), "cmbGroupName");
            cmbGroupName.Enabled = false;

            pnlScheduleEditor.Controls.Add(cmbGroupName);
            pnlScheduleEditor.Controls.Add(lblGroupName);
            pnlScheduleEditor.Controls.Add(cmbLectureType);
            pnlScheduleEditor.Controls.Add(lblLectureType);
        }

        private static Guna.UI2.WinForms.Guna2HtmlLabel CreateEditorLabel(
            string text,
            Point location,
            string name)
        {
            return new Guna.UI2.WinForms.Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                Location = location,
                Name = name,
                Size = new Size(90, 19),
                Text = text
            };
        }

        private static Guna.UI2.WinForms.Guna2ComboBox CreateEditorComboBox(Point location, string name)
        {
            var combo = new Guna.UI2.WinForms.Guna2ComboBox
            {
                BackColor = Color.Transparent,
                BorderColor = Color.FromArgb(203, 213, 225),
                BorderRadius = 8,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FocusedColor = Color.FromArgb(37, 99, 235),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(15, 23, 42),
                HoverState = { BorderColor = Color.FromArgb(59, 130, 246) },
                ItemHeight = 36,
                Location = location,
                Name = name,
                Size = new Size(160, 42)
            };

            combo.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            return combo;
        }

        private void EnsureSemesterColumn()
        {
            if (dgvSchedules.Columns.Contains("colSemester"))
            {
                return;
            }

            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ScheduleRow.SemesterNumber),
                FillWeight = 58F,
                HeaderText = "Semester",
                Name = "colSemester",
                ReadOnly = true
            };

            dgvSchedules.Columns.Insert(1, column);

            dgvSchedules.Columns.Insert(4, new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ScheduleRow.GroupName),
                FillWeight = 56F,
                HeaderText = "Group",
                Name = "colGroupName",
                ReadOnly = true
            });

            dgvSchedules.Columns.Insert(5, new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ScheduleRow.LectureType),
                FillWeight = 64F,
                HeaderText = "Type",
                Name = "colLectureType",
                ReadOnly = true
            });

            dgvSchedules.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ScheduleRow.EndTimeText),
                FillWeight = 60F,
                HeaderText = "End",
                Name = "colEndTime",
                ReadOnly = true
            });

            colDayOfWeek.DisplayIndex = 0;
            colTimeSlot.DisplayIndex = 1;
            dgvSchedules.Columns["colEndTime"].DisplayIndex = 2;
            colSubject.DisplayIndex = 3;
            colFacultyMember.DisplayIndex = 4;
            colClassroom.DisplayIndex = 5;
            dgvSchedules.Columns["colLectureType"].DisplayIndex = 6;
            dgvSchedules.Columns["colGroupName"].DisplayIndex = 7;
            colStudyYear.DisplayIndex = 8;
            colBranch.DisplayIndex = 9;
            colSection.DisplayIndex = 10;
            column.DisplayIndex = 11;
        }

        private void ApplyScheduleGridColumnLayout()
        {
            SetGridColumn(colDayOfWeek, "Day", 78);
            SetGridColumn(colTimeSlot, "Start", 62);
            SetGridColumn(dgvSchedules.Columns["colEndTime"], "End", 62);
            SetGridColumn(colSubject, "Subject", 160);
            SetGridColumn(colFacultyMember, "Faculty", 132);
            SetGridColumn(colClassroom, "Room/Lab", 76);
            SetGridColumn(dgvSchedules.Columns["colLectureType"], "Lecture Type", 82);
            SetGridColumn(dgvSchedules.Columns["colGroupName"], "Group", 58);
            SetGridColumn(colStudyYear, "Year", 78);
            SetGridColumn(colBranch, "Branch", 92);
            SetGridColumn(colSection, "Section", 66);
            SetGridColumn(dgvSchedules.Columns["colSemester"], "Semester", 58);

            dgvSchedules.DefaultCellStyle.Padding = new Padding(6, 0, 6, 0);
            dgvSchedules.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 6, 0);
            dgvSchedules.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvSchedules.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dgvSchedules.RowsDefaultCellStyle.BackColor = Color.White;
            dgvSchedules.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvSchedules.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvSchedules.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
        }

        private static void SetGridColumn(DataGridViewColumn column, string headerText, float fillWeight)
        {
            column.HeaderText = headerText;
            column.FillWeight = fillWeight;
            column.MinimumWidth = 48;
        }

        private void ConfigureScheduleEvents()
        {
            btnRefreshSchedule.Click += async (_, _) => await RefreshSchedulesAsync();
            btnGenerateSchedule.Click += async (_, _) => await GenerateScheduleAsync();
            btnAddSchedule.Click += async (_, _) => await AddScheduleAsync();
            btnUpdateSchedule.Click += async (_, _) => await UpdateScheduleAsync();
            btnDeleteSchedule.Click += async (_, _) => await DeleteScheduleAsync();
            btnClearScheduleForm.Click += (_, _) => ClearGeneratedScheduleTable();
            btnExportSchedulePdf.Click += async (_, _) => await ExportSchedulePdfAsync();

            dgvSchedules.SelectionChanged += async (_, _) => await PopulateScheduleEditorFromSelectionAsync();
            cmbSubject.SelectedIndexChanged += (_, _) => ApplySubjectSelection();
            cmbStudyYear.SelectedIndexChanged += (_, _) => ApplyStudyYearSelection();
            cmbBranch.SelectedIndexChanged += (_, _) => ApplyBranchSelection();
            cmbSection.SelectedIndexChanged += (_, _) => ApplySectionSelection();
            cmbFacultyFilter.SelectedIndexChanged += (_, _) => ApplyScheduleFilters();
            cmbSectionFilter.SelectedIndexChanged += (_, _) => ApplySectionFilterSelection();
            cmbStudyYearFilter.SelectedIndexChanged += (_, _) => ApplyStudyYearFilterSelection();
            cmbDayFilter.SelectedIndexChanged += (_, _) => ApplyScheduleFilters();
            cmbSemesterFilter.SelectedIndexChanged += (_, _) => ApplyScheduleFilters();
            cmbLectureTypeFilter.SelectedIndexChanged += (_, _) => ApplyScheduleFilters();
            cmbGroupFilter.SelectedIndexChanged += (_, _) => ApplyScheduleFilters();
            cmbLectureType.SelectedIndexChanged += (_, _) => ApplyLectureTypeSelection();
        }

        private async Task RefreshSchedulesAsync()
        {
            SetScheduleActionsEnabled(false);

            try
            {
                await LoadLookupsAsync();
                await LoadSchedulesAsync();
                ClearScheduleForm();
            }
            catch (Exception ex)
            {
                ShowError("Unable to refresh schedule data.", ex);
            }
            finally
            {
                SetScheduleActionsEnabled(true);
            }
        }

        private async Task LoadLookupsAsync()
        {
            subjectsLookup = await subjectService.GetAllAsync();
            var facultyMembers = await facultyMemberService.GetAllAsync();
            var classrooms = await classroomService.GetAllAsync();
            var timeSlots = await timeSlotService.GetAllAsync();
            studyYearsLookup = await studyYearService.GetAllAsync();
            branchesLookup = await branchService.GetAllAsync();
            sectionsLookup = await sectionService.GetAllAsync();

            BindSubjectsCombo();
            BindCombo(cmbFacultyMember, facultyMembers.Select(faculty => new ComboOption(faculty.FacultyMemberID, faculty.FullName)));
            BindCombo(cmbClassroom, classrooms.Select(classroom => new ComboOption(classroom.ClassroomID, classroom.ClassroomNumber)));
            BindCombo(cmbTimeSlot, timeSlots.Select(slot => new ComboOption(slot.TimeSlotID, FormatTimeSlot(slot))));
            BindCombo(cmbStudyYear, studyYearsLookup.Select(studyYear => new ComboOption(studyYear.StudyYearID, studyYear.YearName)));
            BindCombo(cmbBranch, branchesLookup.Select(branch => new ComboOption(branch.BranchID, branch.BranchName)));
            BindSectionsCombo();

            BindFilterCombo(cmbFacultyFilter, facultyMembers.Select(faculty => new ComboOption(faculty.FacultyMemberID, faculty.FullName)), "All faculty");
            BindFilterCombo(cmbStudyYearFilter, studyYearsLookup.Select(studyYear => new ComboOption(studyYear.StudyYearID, studyYear.YearName)), "All study years");
            BindSectionFilterCombo();
            BindDayCombos();
            BindSemesterFilterCombo();
            BindLectureTypeFilterCombo();
            BindGroupFilterCombo();
            BindGroupNameCombo();
        }

        private async Task LoadSchedulesAsync()
        {
            var schedules = await scheduleService.GetScheduleDetailsAsync();
            scheduleRows = schedules
                .Select(ScheduleRow.FromDetails)
                .OrderBy(row => row.SemesterNumber)
                .ThenBy(row => StudyYearOrder(row.StudyYearName))
                .ThenBy(row => row.BranchName)
                .ThenBy(row => row.SectionName)
                .ThenBy(row => DayOrder(row.DayOfWeek))
                .ThenBy(row => row.TimeSlotName)
                .ToList();

            ApplyScheduleFilters();
        }

        private async Task GenerateScheduleAsync()
        {
            if (!ConfirmScheduleGeneration())
            {
                return;
            }

            SetScheduleActionsEnabled(false);

            try
            {
                var result = await scheduleService.GenerateAsync();
                await LoadSchedulesAsync();
                ShowInformation(BuildGenerationMessage(result));
            }
            catch (Exception ex)
            {
                ShowError("Unable to generate schedule.", ex);
            }
            finally
            {
                SetScheduleActionsEnabled(true);
            }
        }

        private bool ConfirmScheduleGeneration()
        {
            var confirmation = MessageBox.Show(
                this,
                "Generating a timetable will replace the current generated schedule records. Continue?",
                "Generate Schedule",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            return confirmation == DialogResult.Yes;
        }

        private static string BuildGenerationMessage(ScheduleGenerationResult result)
        {
            var message = new List<string>
            {
                $"Generation completed. Created: {result.CreatedCount}, Skipped: {result.SkippedCount}.",
                $"Required subject-section lessons: {result.RequiredCount}.",
                $"Available setup: {result.TimeSlotCount} time slots, {result.ClassroomCount} classrooms, {result.SectionCount} sections."
            };

            if (result.SkippedCount == 0 && result.UnassignedSubjectsCount == 0)
            {
                message.Add("The generated schedule is complete.");
                return string.Join(Environment.NewLine, message);
            }

            message.Add("");
            message.Add("To create a complete schedule, check:");

            if (result.UnassignedSubjectsCount > 0)
            {
                message.Add($"- Assign faculty members to {result.UnassignedSubjectsCount} subject(s).");
            }

            if (result.MissingSectionCount > 0)
            {
                message.Add($"- Add matching sections for {result.MissingSectionCount} subject assignment(s).");
            }

            if (result.NoClassroomCount > 0)
            {
                message.Add($"- Add classrooms or increase classroom capacity for {result.NoClassroomCount} lesson(s).");
            }

            if (result.ConflictCount > 0)
            {
                message.Add($"- Add more non-break time slots, classrooms, or faculty coverage for {result.ConflictCount} conflicted lesson(s).");
            }

            if (result.DuplicateAssignmentCount > 0)
            {
                message.Add($"- Remove {result.DuplicateAssignmentCount} duplicate faculty-subject assignment(s).");
            }

            return string.Join(Environment.NewLine, message);
        }

        private async Task AddScheduleAsync()
        {
            if (!TryBuildSchedule(out var schedule))
            {
                return;
            }

            await ExecuteScheduleActionAsync(
                async () => await scheduleService.AddAsync(schedule),
                "Schedule added successfully.");
        }

        private async Task UpdateScheduleAsync()
        {
            if (!TryGetSelectedScheduleId(out int scheduleId) || !TryBuildSchedule(out var schedule))
            {
                ShowInformation("Select a schedule row before updating.");
                return;
            }

            schedule.ScheduleID = scheduleId;

            await ExecuteScheduleActionAsync(
                async () => await scheduleService.UpdateAsync(schedule),
                "Schedule updated successfully.");
        }

        private async Task DeleteScheduleAsync()
        {
            if (!TryGetSelectedScheduleId(out int scheduleId))
            {
                ShowInformation("Select a schedule row before deleting.");
                return;
            }

            var confirmation = MessageBox.Show(
                this,
                "Are you sure you want to delete the selected schedule?",
                "Delete Schedule",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            await ExecuteScheduleActionAsync(
                async () => await scheduleService.DeleteAsync(scheduleId),
                "Schedule deleted successfully.");
        }

        private async Task ExecuteScheduleActionAsync(Func<Task> action, string successMessage)
        {
            SetScheduleActionsEnabled(false);

            try
            {
                await action();
                await LoadSchedulesAsync();
                ClearScheduleForm();
                ShowInformation(successMessage);
            }
            catch (Exception ex)
            {
                ShowError("Unable to complete the schedule operation.", ex);
            }
            finally
            {
                SetScheduleActionsEnabled(true);
            }
        }

        private async Task ExportSchedulePdfAsync()
        {
            var rows = GetFilteredRows()
                .Select(row => new SchedulePdfRow(
                    row.DayOfWeek,
                    row.TimeSlotName,
                    row.SubjectName,
                    row.FacultyMemberName,
                    row.ClassroomName,
                    row.StudyYearName,
                    row.BranchName,
                    row.SectionName))
                .ToList();

            using var dialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = $"Schedule-{DateTime.Now:yyyyMMdd-HHmm}.pdf",
                Title = "Export schedule to PDF"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                await schedulePdfExportService.ExportAsync(dialog.FileName, rows);
                ShowInformation("Schedule exported successfully.");
            }
            catch (Exception ex)
            {
                ShowError("Unable to export schedule PDF.", ex);
            }
        }

        private void ApplyScheduleFilters()
        {
            dgvSchedules.DataSource = GetFilteredRows().ToList();
            dgvSchedules.ClearSelection();
            StyleScheduleRows();
        }

        private void StyleScheduleRows()
        {
            foreach (DataGridViewRow row in dgvSchedules.Rows)
            {
                if (row.DataBoundItem is not ScheduleRow scheduleRow)
                {
                    continue;
                }

                bool isPractical = scheduleRow.LectureType == "Practical";
                row.DefaultCellStyle.BackColor = isPractical
                    ? Color.FromArgb(240, 253, 244)
                    : Color.White;
                row.DefaultCellStyle.SelectionBackColor = isPractical
                    ? Color.FromArgb(187, 247, 208)
                    : Color.FromArgb(219, 234, 254);
                row.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);

                row.Cells["colDayOfWeek"].Style.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
                row.Cells["colTimeSlot"].Style.ForeColor = Color.FromArgb(37, 99, 235);
                row.Cells["colEndTime"].Style.ForeColor = Color.FromArgb(37, 99, 235);

                if (row.Cells["colSubject"] is DataGridViewCell subjectCell)
                {
                    subjectCell.ToolTipText = scheduleRow.SubjectName;
                }

                if (row.Cells["colLectureType"] is DataGridViewCell typeCell)
                {
                    typeCell.ToolTipText = isPractical
                        ? "Practical session for the selected group."
                        : "Theory session for the whole section.";
                }
            }
        }

        private void ApplySectionFilterSelection()
        {
            if (isUpdatingScheduleLookups)
            {
                return;
            }

            int? sectionId = GetSelectedOptionalId(cmbSectionFilter);

            if (sectionId.HasValue)
            {
                var section = sectionsLookup.FirstOrDefault(item => item.SectionID == sectionId.Value);

                if (section is not null)
                {
                    isUpdatingScheduleLookups = true;
                    SelectComboValue(cmbStudyYearFilter, section.StudyYearID);
                    isUpdatingScheduleLookups = false;
                }
            }

            ApplyScheduleFilters();
        }

        private void ApplyStudyYearFilterSelection()
        {
            if (isUpdatingScheduleLookups)
            {
                return;
            }

            int? selectedStudyYearId = GetSelectedOptionalId(cmbStudyYearFilter);
            int? selectedSectionId = GetSelectedOptionalId(cmbSectionFilter);
            var selectedSection = selectedSectionId.HasValue
                ? sectionsLookup.FirstOrDefault(section => section.SectionID == selectedSectionId.Value)
                : null;

            if (selectedStudyYearId.HasValue &&
                selectedSection is not null &&
                selectedSection.StudyYearID != selectedStudyYearId.Value)
            {
                selectedSectionId = null;
            }

            isUpdatingScheduleLookups = true;
            BindSectionFilterCombo(selectedStudyYearId, selectedSectionId);
            isUpdatingScheduleLookups = false;

            ApplyScheduleFilters();
        }

        private IEnumerable<ScheduleRow> GetFilteredRows()
        {
            int? facultyId = GetSelectedOptionalId(cmbFacultyFilter);
            int? sectionId = GetSelectedOptionalId(cmbSectionFilter);
            int? studyYearId = GetSelectedOptionalId(cmbStudyYearFilter);
            int? semesterNumber = GetSelectedOptionalId(cmbSemesterFilter);
            string? lectureType = cmbLectureTypeFilter.SelectedItem is ComboOption option ? option.Text : null;
            string? groupName = cmbGroupFilter.SelectedItem is ComboOption groupOption ? groupOption.Text : null;
            string? day = cmbDayFilter.SelectedItem as string;

            return scheduleRows.Where(row =>
                (!semesterNumber.HasValue || row.SemesterNumber == semesterNumber.Value) &&
                (string.IsNullOrWhiteSpace(lectureType) ||
                    lectureType == "All" ||
                    row.LectureType == lectureType) &&
                RowMatchesGroupFilter(row, groupName) &&
                RowMatchesFacultyFilter(row, facultyId) &&
                RowMatchesSectionFilter(row, sectionId) &&
                RowMatchesStudyYearFilter(row, studyYearId) &&
                (string.IsNullOrWhiteSpace(day) || day == "All days" || row.DayOfWeek == day));
        }

        private static bool RowMatchesGroupFilter(ScheduleRow row, string? groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName) || groupName == "All")
            {
                return true;
            }

            string normalizedGroup = groupName.Trim().ToUpperInvariant();

            if (string.Equals(row.LectureType, "Practical", StringComparison.OrdinalIgnoreCase))
            {
                return string.Equals(row.GroupName, normalizedGroup, StringComparison.OrdinalIgnoreCase);
            }

            string baseSectionName = AcademicStructureRules.GetBaseSectionName(normalizedGroup);
            return IsFirstOrSecondYear(row.StudyYearName) &&
                string.Equals(row.SectionName, baseSectionName, StringComparison.OrdinalIgnoreCase);
        }

        private bool RowMatchesFacultyFilter(ScheduleRow row, int? facultyId)
        {
            if (!facultyId.HasValue)
            {
                return true;
            }

            var selectedFaculty = cmbFacultyFilter.SelectedItem as ComboOption;
            return string.Equals(row.FacultyMemberName, selectedFaculty?.Text, StringComparison.OrdinalIgnoreCase);
        }

        private bool RowMatchesStudyYearFilter(ScheduleRow row, int? studyYearId)
        {
            if (!studyYearId.HasValue)
            {
                return true;
            }

            var studyYear = studyYearsLookup.FirstOrDefault(item => item.StudyYearID == studyYearId.Value);
            return string.Equals(row.StudyYearName, studyYear?.YearName, StringComparison.OrdinalIgnoreCase);
        }

        private bool RowMatchesSectionFilter(ScheduleRow row, int? sectionId)
        {
            if (!sectionId.HasValue)
            {
                return true;
            }

            var section = sectionsLookup.FirstOrDefault(item => item.SectionID == sectionId.Value);

            if (section is null ||
                !string.Equals(row.SectionName, section.SectionName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var studyYear = studyYearsLookup.FirstOrDefault(item => item.StudyYearID == section.StudyYearID);

            if (studyYear is not null &&
                !string.Equals(row.StudyYearName, studyYear.YearName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var branchName = section.BranchID.HasValue
                ? branchesLookup.FirstOrDefault(item => item.BranchID == section.BranchID.Value)?.BranchName
                : null;

            return string.IsNullOrWhiteSpace(branchName)
                ? string.IsNullOrWhiteSpace(row.BranchName) || row.BranchName == "-"
                : string.Equals(row.BranchName, branchName, StringComparison.OrdinalIgnoreCase);
        }

        private bool TryBuildSchedule(out Schedule schedule)
        {
            ApplySectionSelection();

            schedule = new Schedule
            {
                DayOfWeek = cmbDayOfWeek.Text.Trim(),
                SubjectID = GetSelectedRequiredId(cmbSubject),
                FacultyMemberID = GetSelectedRequiredId(cmbFacultyMember),
                ClassroomID = GetSelectedRequiredId(cmbClassroom),
                TimeSlotID = GetSelectedRequiredId(cmbTimeSlot),
                LectureType = GetSelectedPlainText(cmbLectureType) ?? "Theory",
                GroupName = GetSelectedPlainText(cmbGroupName),
                StudyYearID = GetSelectedOptionalId(cmbStudyYear),
                BranchID = GetSelectedOptionalId(cmbBranch),
                SectionID = GetSelectedOptionalId(cmbSection)
            };

            if (string.IsNullOrWhiteSpace(schedule.DayOfWeek))
            {
                ShowInformation("Select a day.");
                cmbDayOfWeek.Focus();
                return false;
            }

            if (schedule.SubjectID <= 0 || schedule.FacultyMemberID <= 0 ||
                schedule.ClassroomID <= 0 || schedule.TimeSlotID <= 0)
            {
                ShowInformation("Subject, faculty, classroom, and time slot are required.");
                return false;
            }

            if (!schedule.SectionID.HasValue)
            {
                ShowInformation("Section is required.");
                cmbSection.Focus();
                return false;
            }

            int subjectId = schedule.SubjectID;
            int? sectionId = schedule.SectionID;
            var subject = subjectsLookup.FirstOrDefault(item => item.SubjectID == subjectId);
            var section = sectionId.HasValue
                ? sectionsLookup.FirstOrDefault(item => item.SectionID == sectionId.Value)
                : null;

            if (section is not null)
            {
                schedule.StudyYearID = section.StudyYearID;
                schedule.BranchID = section.BranchID;
            }

            if (subject is not null &&
                schedule.StudyYearID.HasValue &&
                subject.StudyYearID != schedule.StudyYearID.Value)
            {
                ShowInformation("The selected subject does not belong to the selected section study year.");
                return false;
            }

            if (subject?.BranchID.HasValue == true &&
                schedule.BranchID.HasValue &&
                subject.BranchID.Value != schedule.BranchID.Value)
            {
                ShowInformation("The selected subject does not belong to the selected section branch.");
                return false;
            }

            if (subject is not null &&
                section is not null &&
                !AcademicStructureRules.SectionMatchesSubject(
                    subject.StudyYearID,
                    subject.BranchID,
                    section.BranchID,
                    section.SectionName))
            {
                ShowInformation("The selected section is not valid for this subject.");
                return false;
            }

            return true;
        }

        private bool TryGetSelectedScheduleId(out int scheduleId)
        {
            if (int.TryParse(txtScheduleId.Text, out scheduleId))
            {
                return true;
            }

            var selectedSchedule = dgvSchedules.CurrentRow?.DataBoundItem as ScheduleRow;
            scheduleId = selectedSchedule?.ScheduleID ?? 0;
            return scheduleId > 0;
        }

        private async Task PopulateScheduleEditorFromSelectionAsync()
        {
            if (dgvSchedules.CurrentRow?.DataBoundItem is not ScheduleRow row)
            {
                return;
            }

            try
            {
                var schedule = await scheduleService.GetByIdAsync(row.ScheduleID);

                if (schedule is null)
                {
                    return;
                }

                txtScheduleId.Text = schedule.ScheduleID.ToString();
                cmbDayOfWeek.SelectedItem = schedule.DayOfWeek;
                SelectComboValue(cmbSubject, schedule.SubjectID);
                SelectComboValue(cmbFacultyMember, schedule.FacultyMemberID);
                SelectComboValue(cmbClassroom, schedule.ClassroomID);
                SelectComboValue(cmbTimeSlot, schedule.TimeSlotID);
                SelectComboValue(cmbStudyYear, schedule.StudyYearID);
                SelectComboValue(cmbBranch, schedule.BranchID);
                BindSectionsCombo(schedule.StudyYearID, schedule.BranchID, schedule.SectionID);
                BindSubjectsCombo(schedule.StudyYearID, schedule.BranchID, schedule.SubjectID);
                SelectComboValue(cmbSection, schedule.SectionID);
                SelectComboText(cmbLectureType, schedule.LectureType);
                BindGroupNameCombo(schedule.GroupName);
            }
            catch (Exception ex)
            {
                ShowError("Unable to load the selected schedule.", ex);
            }
        }

        private void ClearGeneratedScheduleTable()
        {
            scheduleRows = [];
            ApplyScheduleFilters();
            ClearScheduleForm();
            ShowInformation("Schedule view cleared. Database records were not deleted.");
        }

        private void ClearScheduleForm()
        {
            txtScheduleId.Clear();
            ClearCombo(cmbSubject);
            ClearCombo(cmbFacultyMember);
            ClearCombo(cmbClassroom);
            ClearCombo(cmbTimeSlot);
            ClearCombo(cmbDayOfWeek);
            ClearCombo(cmbStudyYear);
            ClearCombo(cmbBranch);
            cmbBranch.Enabled = true;
            BindSectionsCombo();
            BindSubjectsCombo();
            ClearCombo(cmbSection);
            SelectComboText(cmbLectureType, "Theory");
            BindGroupNameCombo();
            ClearCombo(cmbGroupName);
            dgvSchedules.ClearSelection();
        }

        private void SetScheduleActionsEnabled(bool enabled)
        {
            btnRefreshSchedule.Enabled = enabled;
            btnGenerateSchedule.Enabled = enabled;
            btnAddSchedule.Enabled = enabled;
            btnUpdateSchedule.Enabled = enabled;
            btnDeleteSchedule.Enabled = enabled;
            btnClearScheduleForm.Enabled = enabled;
            btnExportSchedulePdf.Enabled = enabled;
            dgvSchedules.Enabled = enabled;
        }

        private static void BindCombo(Guna.UI2.WinForms.Guna2ComboBox combo, IEnumerable<ComboOption> options)
        {
            combo.DataSource = options.ToList();
            combo.DisplayMember = nameof(ComboOption.Text);
            combo.ValueMember = nameof(ComboOption.Id);
            combo.SelectedIndex = -1;
        }

        private static void BindFilterCombo(
            Guna.UI2.WinForms.Guna2ComboBox combo,
            IEnumerable<ComboOption> options,
            string allText)
        {
            var items = new List<ComboOption> { new(null, allText) };
            items.AddRange(options);
            combo.DataSource = items;
            combo.DisplayMember = nameof(ComboOption.Text);
            combo.ValueMember = nameof(ComboOption.Id);
            combo.SelectedIndex = 0;
        }

        private void BindSubjectsCombo(int? studyYearId = null, int? branchId = null, int? selectedSubjectId = null)
        {
            var subjects = subjectsLookup
                .Where(subject => SubjectMatchesFilter(subject, studyYearId, branchId))
                .OrderBy(subject => subject.StudyYearID)
                .ThenBy(subject => subject.BranchID ?? 0)
                .ThenBy(subject => subject.SubjectID)
                .Select((subject, index) => new ComboOption(subject.SubjectID, $"{index + 1}. {subject.SubjectName}"));

            BindCombo(cmbSubject, subjects);
            SelectComboValue(cmbSubject, selectedSubjectId);
        }

        private void BindSectionsCombo(int? studyYearId = null, int? branchId = null, int? selectedSectionId = null)
        {
            var sections = sectionsLookup
                .Where(section => SectionMatchesFilter(section, studyYearId, branchId))
                .OrderBy(section => section.StudyYearID)
                .ThenBy(section => section.BranchID ?? 0)
                .ThenBy(section => section.SectionName)
                .Select(section => new ComboOption(section.SectionID, FormatSection(section)));

            BindCombo(cmbSection, sections);
            SelectComboValue(cmbSection, selectedSectionId);
        }

        private void BindSectionFilterCombo(int? studyYearId = null, int? selectedSectionId = null)
        {
            var sections = sectionsLookup
                .Where(section => SectionMatchesFilter(section, studyYearId, null))
                .OrderBy(section => section.StudyYearID)
                .ThenBy(section => section.BranchID ?? 0)
                .ThenBy(section => section.SectionName)
                .Select(section => new ComboOption(section.SectionID, FormatSection(section)));

            BindFilterCombo(cmbSectionFilter, sections, "All sections");
            SelectComboValue(cmbSectionFilter, selectedSectionId);
        }

        private void BindSemesterFilterCombo()
        {
            BindFilterCombo(
                cmbSemesterFilter,
                [
                    new ComboOption(1, "Semester 1"),
                    new ComboOption(2, "Semester 2")
                ],
                "All semesters");
        }

        private void BindLectureTypeFilterCombo()
        {
            BindFilterCombo(
                cmbLectureTypeFilter,
                [
                    new ComboOption(null, "Theory"),
                    new ComboOption(null, "Practical")
                ],
                "All");
        }

        private void BindGroupFilterCombo()
        {
            BindFilterCombo(
                cmbGroupFilter,
                GetPracticalGroupNames().Select(group => new ComboOption(null, group)),
                "All");
        }

        private void BindGroupNameCombo(string? selectedGroupName = null)
        {
            string? currentSelection = selectedGroupName ?? GetSelectedPlainText(cmbGroupName);
            var section = GetSelectedOptionalId(cmbSection) is int sectionId
                ? sectionsLookup.FirstOrDefault(item => item.SectionID == sectionId)
                : null;

            cmbGroupName.Items.Clear();
            cmbGroupName.Items.AddRange(GetAllowedGroupsForSection(section).Cast<object>().ToArray());
            SelectComboText(cmbGroupName, currentSelection);
            ApplyLectureTypeSelection();
        }

        private void ApplySubjectSelection()
        {
            if (isUpdatingScheduleLookups)
            {
                return;
            }

            int? subjectId = GetSelectedOptionalId(cmbSubject);
            var subject = subjectId.HasValue
                ? subjectsLookup.FirstOrDefault(item => item.SubjectID == subjectId.Value)
                : null;

            if (subject is null)
            {
                return;
            }

            isUpdatingScheduleLookups = true;
            SelectComboValue(cmbStudyYear, subject.StudyYearID);

            if (subject.BranchID.HasValue)
            {
                SelectComboValue(cmbBranch, subject.BranchID);
            }

            BindSectionsCombo(subject.StudyYearID, subject.BranchID);
            BindGroupNameCombo();
            isUpdatingScheduleLookups = false;
        }

        private void ApplyStudyYearSelection()
        {
            if (isUpdatingScheduleLookups)
            {
                return;
            }

            int? studyYearId = GetSelectedOptionalId(cmbStudyYear);
            int? branchId = GetSelectedOptionalId(cmbBranch);

            isUpdatingScheduleLookups = true;

            if (studyYearId.HasValue && AcademicStructureRules.UsesGeneralSections(studyYearId.Value))
            {
                ClearCombo(cmbBranch);
                cmbBranch.Enabled = false;
                branchId = null;
            }
            else
            {
                cmbBranch.Enabled = true;
            }

            BindSectionsCombo(studyYearId, branchId);
            BindSubjectsCombo(studyYearId, branchId);
            BindGroupNameCombo();
            isUpdatingScheduleLookups = false;
        }

        private void ApplyBranchSelection()
        {
            if (isUpdatingScheduleLookups)
            {
                return;
            }

            int? studyYearId = GetSelectedOptionalId(cmbStudyYear);
            int? branchId = GetSelectedOptionalId(cmbBranch);

            isUpdatingScheduleLookups = true;

            if (studyYearId.HasValue && AcademicStructureRules.UsesGeneralSections(studyYearId.Value))
            {
                ClearCombo(cmbBranch);
                branchId = null;
            }

            BindSectionsCombo(studyYearId, branchId);
            BindSubjectsCombo(studyYearId, branchId);
            BindGroupNameCombo();
            isUpdatingScheduleLookups = false;
        }

        private void ApplySectionSelection()
        {
            if (isUpdatingScheduleLookups)
            {
                return;
            }

            int? sectionId = GetSelectedOptionalId(cmbSection);
            var section = sectionId.HasValue
                ? sectionsLookup.FirstOrDefault(item => item.SectionID == sectionId.Value)
                : null;

            if (section is null)
            {
                return;
            }

            int? selectedSubjectId = GetSelectedOptionalId(cmbSubject);

            isUpdatingScheduleLookups = true;
            SelectComboValue(cmbStudyYear, section.StudyYearID);
            SelectComboValue(cmbBranch, section.BranchID);
            BindSubjectsCombo(section.StudyYearID, section.BranchID, selectedSubjectId);
            cmbBranch.Enabled = !AcademicStructureRules.UsesGeneralSections(section.StudyYearID);
            BindGroupNameCombo();
            isUpdatingScheduleLookups = false;
        }

        private void ApplyLectureTypeSelection()
        {
            bool isPractical = string.Equals(
                GetSelectedPlainText(cmbLectureType),
                "Practical",
                StringComparison.OrdinalIgnoreCase);

            cmbGroupName.Enabled = isPractical && cmbGroupName.Items.Count > 0;

            if (!isPractical)
            {
                ClearCombo(cmbGroupName);
            }
        }

        private static bool SubjectMatchesFilter(Subject subject, int? studyYearId, int? branchId)
        {
            if (studyYearId.HasValue && subject.StudyYearID != studyYearId.Value)
            {
                return false;
            }

            if (AcademicStructureRules.UsesGeneralSections(subject.StudyYearID))
            {
                return !subject.BranchID.HasValue;
            }

            if (!branchId.HasValue)
            {
                return true;
            }

            return subject.BranchID == branchId.Value;
        }

        private static bool SectionMatchesFilter(Section section, int? studyYearId, int? branchId)
        {
            if (studyYearId.HasValue && section.StudyYearID != studyYearId.Value)
            {
                return false;
            }

            if (AcademicStructureRules.UsesGeneralSections(section.StudyYearID))
            {
                return !section.BranchID.HasValue &&
                    AcademicStructureRules.GetAllowedSectionNames(section.StudyYearID)
                        .Contains(section.SectionName.Trim(), StringComparer.OrdinalIgnoreCase);
            }

            if (!branchId.HasValue)
            {
                return section.BranchID.HasValue;
            }

            return section.BranchID == branchId.Value;
        }

        private void BindDayCombos()
        {
            string[] days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday"];

            cmbDayOfWeek.Items.Clear();
            cmbDayOfWeek.Items.AddRange(days);
            cmbDayOfWeek.SelectedIndex = -1;

            cmbDayFilter.Items.Clear();
            cmbDayFilter.Items.Add("All days");
            cmbDayFilter.Items.AddRange(days);
            cmbDayFilter.SelectedIndex = 0;
        }

        private static int GetSelectedRequiredId(Guna.UI2.WinForms.Guna2ComboBox combo)
        {
            return GetSelectedOptionalId(combo) ?? 0;
        }

        private static int? GetSelectedOptionalId(Guna.UI2.WinForms.Guna2ComboBox combo)
        {
            return combo.SelectedItem is ComboOption option ? option.Id : null;
        }

        private static void SelectComboValue(Guna.UI2.WinForms.Guna2ComboBox combo, int? id)
        {
            if (!id.HasValue)
            {
                combo.SelectedIndex = -1;
                return;
            }

            foreach (var item in combo.Items)
            {
                if (item is ComboOption option && option.Id == id.Value)
                {
                    combo.SelectedItem = item;
                    return;
                }
            }

            combo.SelectedIndex = -1;
        }

        private static void ClearCombo(Guna.UI2.WinForms.Guna2ComboBox combo)
        {
            combo.SelectedIndex = -1;
            combo.Text = string.Empty;
        }

        private static string? GetSelectedPlainText(Guna.UI2.WinForms.Guna2ComboBox combo)
        {
            return combo.SelectedItem?.ToString();
        }

        private static void SelectComboText(Guna.UI2.WinForms.Guna2ComboBox combo, string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                combo.SelectedIndex = -1;
                return;
            }

            foreach (var item in combo.Items)
            {
                if (string.Equals(item?.ToString(), text, StringComparison.OrdinalIgnoreCase))
                {
                    combo.SelectedItem = item;
                    return;
                }
            }

            combo.SelectedIndex = -1;
        }

        private static IReadOnlyList<string> GetAllowedGroupsForSection(Section? section)
        {
            if (section is null || !AcademicStructureRules.UsesGeneralSections(section.StudyYearID))
            {
                return [];
            }

            return AcademicStructureRules.GetBaseSectionName(section.SectionName).ToUpperInvariant() switch
            {
                "A" => ["A1", "A2"],
                "B" => ["B1", "B2"],
                _ => []
            };
        }

        private static IReadOnlyList<string> GetPracticalGroupNames()
        {
            return ["A1", "A2", "B1", "B2"];
        }

        private static string FormatTimeSlot(TimeSlot timeSlot)
        {
            string label = $"{timeSlot.StartTime:hh\\:mm} - {timeSlot.EndTime:hh\\:mm}";
            return timeSlot.IsBreak ? $"{label} (Break)" : label;
        }

        private static string FormatTimeSlot(TimeSpan startTime, TimeSpan endTime)
        {
            return $"{startTime:hh\\:mm} - {endTime:hh\\:mm}";
        }

        private static string FormatSection(Section section)
        {
            string year = section.StudyYear?.YearName ?? "Year";

            return section.Branch is null
                ? $"{section.SectionName} - {year}"
                : $"{section.SectionName} - {year} - {section.Branch.BranchName}";
        }

        private static int DayOrder(string day)
        {
            return day switch
            {
                "Sunday" => 1,
                "Monday" => 2,
                "Tuesday" => 3,
                "Wednesday" => 4,
                "Thursday" => 5,
                _ => 99
            };
        }

        private static int StudyYearOrder(string studyYearName)
        {
            return studyYearName switch
            {
                "First Year" => 1,
                "Second Year" => 2,
                "Third Year" => 3,
                "Fourth Year" => 4,
                _ => 99
            };
        }

        private static bool IsFirstOrSecondYear(string studyYearName)
        {
            return StudyYearOrder(studyYearName) is 1 or 2;
        }

        private void ShowInformation(string message)
        {
            MessageBox.Show(this, message, "Schedule", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show(this, $"{message}\n\n{ex.Message}", "Schedule", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private sealed record ComboOption(int? Id, string Text);

        private sealed class ScheduleRow
        {
            public int ScheduleID { get; init; }
            public int SubjectID { get; init; }
            public int FacultyMemberID { get; init; }
            public int ClassroomID { get; init; }
            public int TimeSlotID { get; init; }
            public int SemesterNumber { get; init; }
            public int? StudyYearID { get; init; }
            public int? BranchID { get; init; }
            public int? SectionID { get; init; }
            public string DayOfWeek { get; init; } = string.Empty;
            public string SubjectName { get; init; } = string.Empty;
            public string FacultyMemberName { get; init; } = string.Empty;
            public string ClassroomName { get; init; } = string.Empty;
            public string TimeSlotName { get; init; } = string.Empty;
            public string StartTimeText { get; init; } = string.Empty;
            public string EndTimeText { get; init; } = string.Empty;
            public string StudyYearName { get; init; } = string.Empty;
            public string BranchName { get; init; } = string.Empty;
            public string SectionName { get; init; } = string.Empty;
            public string GroupName { get; init; } = "All";
            public string LectureType { get; init; } = "Theory";

            public static ScheduleRow FromSchedule(Schedule schedule)
            {
                string timeSlotName = schedule.TimeSlot is null ? "-" : FormatTimeSlot(schedule.TimeSlot);

                return new ScheduleRow
                {
                    ScheduleID = schedule.ScheduleID,
                    SubjectID = schedule.SubjectID,
                    FacultyMemberID = schedule.FacultyMemberID,
                    ClassroomID = schedule.ClassroomID,
                    TimeSlotID = schedule.TimeSlotID,
                    SemesterNumber = schedule.SemesterNumber,
                    StudyYearID = schedule.StudyYearID,
                    BranchID = schedule.BranchID,
                    SectionID = schedule.SectionID,
                    DayOfWeek = schedule.DayOfWeek,
                    SubjectName = schedule.Subject?.SubjectName ?? "-",
                    FacultyMemberName = schedule.FacultyMember?.FullName ?? "-",
                    ClassroomName = schedule.Classroom?.ClassroomNumber ?? "-",
                    TimeSlotName = timeSlotName,
                    StartTimeText = schedule.TimeSlot is null ? "-" : $"{schedule.TimeSlot.StartTime:hh\\:mm}",
                    EndTimeText = schedule.TimeSlot is null ? "-" : $"{schedule.TimeSlot.EndTime:hh\\:mm}",
                    StudyYearName = schedule.StudyYear?.YearName ?? "-",
                    BranchName = schedule.Branch?.BranchName ?? "-",
                    SectionName = FormatScheduleSection(schedule),
                    GroupName = string.IsNullOrWhiteSpace(schedule.GroupName) ? "All" : schedule.GroupName,
                    LectureType = schedule.LectureType
                };
            }

            public static ScheduleRow FromDetails(ScheduleDetailsView details)
            {
                return new ScheduleRow
                {
                    ScheduleID = details.ScheduleID,
                    SemesterNumber = details.SemesterNumber,
                    DayOfWeek = details.DayOfWeek,
                    SubjectName = details.SubjectName,
                    FacultyMemberName = details.FacultyMemberName,
                    ClassroomName = details.ClassroomNumber,
                    TimeSlotName = FormatTimeSlot(details.StartTime, details.EndTime),
                    StartTimeText = $"{details.StartTime:hh\\:mm}",
                    EndTimeText = $"{details.EndTime:hh\\:mm}",
                    StudyYearName = details.YearName,
                    BranchName = string.IsNullOrWhiteSpace(details.BranchName) ? "-" : details.BranchName,
                    SectionName = details.SectionName,
                    GroupName = string.IsNullOrWhiteSpace(details.GroupName) ? "All" : details.GroupName,
                    LectureType = details.LectureType
                };
            }
        }

        private static string FormatScheduleSection(Schedule schedule)
        {
            if (schedule.Section is null)
            {
                return "-";
            }

            string year = schedule.StudyYear?.YearName ?? schedule.Section.StudyYear?.YearName ?? "Year";
            string? branch = schedule.Branch?.BranchName ?? schedule.Section.Branch?.BranchName;

            return string.IsNullOrWhiteSpace(branch)
                ? $"{schedule.Section.SectionName} - {year}"
                : $"{schedule.Section.SectionName} - {year} - {branch}";
        }
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
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
            pnlSchedulesTable = new Guna.UI2.WinForms.Guna2Panel();
            dgvSchedules = new Guna.UI2.WinForms.Guna2DataGridView();
            colScheduleId = new DataGridViewTextBoxColumn();
            colDayOfWeek = new DataGridViewTextBoxColumn();
            colSubject = new DataGridViewTextBoxColumn();
            colFacultyMember = new DataGridViewTextBoxColumn();
            colClassroom = new DataGridViewTextBoxColumn();
            colTimeSlot = new DataGridViewTextBoxColumn();
            colStudyYear = new DataGridViewTextBoxColumn();
            colBranch = new DataGridViewTextBoxColumn();
            colSection = new DataGridViewTextBoxColumn();
            lblTableSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTableTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlScheduleFilters = new Guna.UI2.WinForms.Guna2Panel();
            cmbDayFilter = new Guna.UI2.WinForms.Guna2ComboBox();
            lblDayFilter = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbStudyYearFilter = new Guna.UI2.WinForms.Guna2ComboBox();
            lblStudyYearFilter = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbSectionFilter = new Guna.UI2.WinForms.Guna2ComboBox();
            lblSectionFilter = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbFacultyFilter = new Guna.UI2.WinForms.Guna2ComboBox();
            lblFacultyFilter = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlScheduleEditor = new Guna.UI2.WinForms.Guna2Panel();
            btnClearScheduleForm = new Guna.UI2.WinForms.Guna2Button();
            btnExportSchedulePdf = new Guna.UI2.WinForms.Guna2Button();
            btnGenerateSchedule = new Guna.UI2.WinForms.Guna2Button();
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
            btnRefreshSchedule = new Guna.UI2.WinForms.Guna2Button();
            lblPageTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlSidebar.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlWorkspace.SuspendLayout();
            pnlSchedulesTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSchedules).BeginInit();
            pnlScheduleFilters.SuspendLayout();
            pnlScheduleEditor.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            pnlSidebar.BackColor = Color.Transparent;
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
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.FillColor = Color.FromArgb(24, 38, 62);
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(240, 820);
            pnlSidebar.TabIndex = 0;
            lblSidebarFooter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblSidebarFooter.BackColor = Color.Transparent;
            lblSidebarFooter.Font = new Font("Segoe UI", 9F);
            lblSidebarFooter.ForeColor = Color.FromArgb(148, 163, 184);
            lblSidebarFooter.Location = new Point(24, 771);
            lblSidebarFooter.Name = "lblSidebarFooter";
            lblSidebarFooter.Size = new Size(147, 17);
            lblSidebarFooter.TabIndex = 11;
            lblSidebarFooter.Text = "Academic Scheduling Suite";
            btnNavigationSchedules.BorderRadius = 8;
            btnNavigationSchedules.Checked = true;
            btnNavigationSchedules.Cursor = Cursors.Hand;
            btnNavigationSchedules.Enabled = false;
            btnNavigationSchedules.FillColor = Color.FromArgb(37, 99, 235);
            btnNavigationSchedules.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnNavigationSchedules.ForeColor = Color.White;
            btnNavigationSchedules.HoverState.FillColor = Color.FromArgb(29, 78, 216);
            btnNavigationSchedules.Location = new Point(24, 490);
            btnNavigationSchedules.Name = "btnNavigationSchedules";
            btnNavigationSchedules.Size = new Size(192, 44);
            btnNavigationSchedules.TabIndex = 10;
            btnNavigationSchedules.Text = "Schedules";
            btnNavigationSchedules.TextAlign = HorizontalAlignment.Left;
            btnNavigationSchedules.TextOffset = new Point(14, 0);
            btnNavigationFaculty.BorderRadius = 8;
            btnNavigationFaculty.Cursor = Cursors.Hand;
            btnNavigationFaculty.FillColor = Color.FromArgb(24, 38, 62);
            btnNavigationFaculty.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnNavigationFaculty.ForeColor = Color.FromArgb(226, 232, 240);
            btnNavigationFaculty.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnNavigationFaculty.Location = new Point(24, 434);
            btnNavigationFaculty.Name = "btnNavigationFaculty";
            btnNavigationFaculty.Size = new Size(192, 44);
            btnNavigationFaculty.TabIndex = 9;
            btnNavigationFaculty.Text = "Faculty Members";
            btnNavigationFaculty.TextAlign = HorizontalAlignment.Left;
            btnNavigationFaculty.TextOffset = new Point(14, 0);
            btnNavigationTimeSlots.BorderRadius = 8;
            btnNavigationTimeSlots.Cursor = Cursors.Hand;
            btnNavigationTimeSlots.FillColor = Color.FromArgb(24, 38, 62);
            btnNavigationTimeSlots.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnNavigationTimeSlots.ForeColor = Color.FromArgb(226, 232, 240);
            btnNavigationTimeSlots.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnNavigationTimeSlots.Location = new Point(24, 378);
            btnNavigationTimeSlots.Name = "btnNavigationTimeSlots";
            btnNavigationTimeSlots.Size = new Size(192, 44);
            btnNavigationTimeSlots.TabIndex = 8;
            btnNavigationTimeSlots.Text = "Time Slots";
            btnNavigationTimeSlots.TextAlign = HorizontalAlignment.Left;
            btnNavigationTimeSlots.TextOffset = new Point(14, 0);
            btnNavigationClassrooms.BorderRadius = 8;
            btnNavigationClassrooms.Cursor = Cursors.Hand;
            btnNavigationClassrooms.FillColor = Color.FromArgb(24, 38, 62);
            btnNavigationClassrooms.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnNavigationClassrooms.ForeColor = Color.FromArgb(226, 232, 240);
            btnNavigationClassrooms.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnNavigationClassrooms.Location = new Point(24, 322);
            btnNavigationClassrooms.Name = "btnNavigationClassrooms";
            btnNavigationClassrooms.Size = new Size(192, 44);
            btnNavigationClassrooms.TabIndex = 7;
            btnNavigationClassrooms.Text = "Classrooms";
            btnNavigationClassrooms.TextAlign = HorizontalAlignment.Left;
            btnNavigationClassrooms.TextOffset = new Point(14, 0);
            btnNavigationSubjects.BorderRadius = 8;
            btnNavigationSubjects.Cursor = Cursors.Hand;
            btnNavigationSubjects.FillColor = Color.FromArgb(24, 38, 62);
            btnNavigationSubjects.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnNavigationSubjects.ForeColor = Color.FromArgb(226, 232, 240);
            btnNavigationSubjects.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnNavigationSubjects.Location = new Point(24, 266);
            btnNavigationSubjects.Name = "btnNavigationSubjects";
            btnNavigationSubjects.Size = new Size(192, 44);
            btnNavigationSubjects.TabIndex = 6;
            btnNavigationSubjects.Text = "Subjects";
            btnNavigationSubjects.TextAlign = HorizontalAlignment.Left;
            btnNavigationSubjects.TextOffset = new Point(14, 0);
            btnNavigationStudyYears.BorderRadius = 8;
            btnNavigationStudyYears.Cursor = Cursors.Hand;
            btnNavigationStudyYears.FillColor = Color.FromArgb(24, 38, 62);
            btnNavigationStudyYears.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnNavigationStudyYears.ForeColor = Color.FromArgb(226, 232, 240);
            btnNavigationStudyYears.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnNavigationStudyYears.Location = new Point(24, 210);
            btnNavigationStudyYears.Name = "btnNavigationStudyYears";
            btnNavigationStudyYears.Size = new Size(192, 44);
            btnNavigationStudyYears.TabIndex = 5;
            btnNavigationStudyYears.Text = "Study Years";
            btnNavigationStudyYears.TextAlign = HorizontalAlignment.Left;
            btnNavigationStudyYears.TextOffset = new Point(14, 0);
            btnNavigationBranches.BorderRadius = 8;
            btnNavigationBranches.Cursor = Cursors.Hand;
            btnNavigationBranches.FillColor = Color.FromArgb(24, 38, 62);
            btnNavigationBranches.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnNavigationBranches.ForeColor = Color.FromArgb(226, 232, 240);
            btnNavigationBranches.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnNavigationBranches.Location = new Point(24, 154);
            btnNavigationBranches.Name = "btnNavigationBranches";
            btnNavigationBranches.Size = new Size(192, 44);
            btnNavigationBranches.TabIndex = 4;
            btnNavigationBranches.Text = "Branches";
            btnNavigationBranches.TextAlign = HorizontalAlignment.Left;
            btnNavigationBranches.TextOffset = new Point(14, 0);
            separatorSidebar.FillColor = Color.FromArgb(51, 65, 85);
            separatorSidebar.Location = new Point(24, 78);
            separatorSidebar.Name = "separatorSidebar";
            separatorSidebar.Size = new Size(192, 10);
            separatorSidebar.TabIndex = 2;
            lblSidebarSubtitle.BackColor = Color.Transparent;
            lblSidebarSubtitle.Font = new Font("Segoe UI", 9F);
            lblSidebarSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSidebarSubtitle.Location = new Point(26, 52);
            lblSidebarSubtitle.Name = "lblSidebarSubtitle";
            lblSidebarSubtitle.Size = new Size(133, 17);
            lblSidebarSubtitle.TabIndex = 1;
            lblSidebarSubtitle.Text = "Classroom Management";
            lblApplicationName.BackColor = Color.Transparent;
            lblApplicationName.Font = new Font("Segoe UI Semibold", 17F, FontStyle.Bold);
            lblApplicationName.ForeColor = Color.White;
            lblApplicationName.Location = new Point(24, 20);
            lblApplicationName.Name = "lblApplicationName";
            lblApplicationName.Size = new Size(216, 33);
            lblApplicationName.TabIndex = 0;
            lblApplicationName.Text = "University Timetable";
            pnlMain.Controls.Add(pnlWorkspace);
            pnlMain.Controls.Add(pnlHeader);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.FillColor = Color.FromArgb(245, 247, 250);
            pnlMain.Location = new Point(240, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1260, 820);
            pnlMain.TabIndex = 1;
            pnlWorkspace.Controls.Add(pnlSchedulesTable);
            pnlWorkspace.Controls.Add(pnlScheduleFilters);
            pnlWorkspace.Controls.Add(pnlScheduleEditor);
            pnlWorkspace.Dock = DockStyle.Fill;
            pnlWorkspace.FillColor = Color.FromArgb(245, 247, 250);
            pnlWorkspace.Location = new Point(0, 88);
            pnlWorkspace.Name = "pnlWorkspace";
            pnlWorkspace.Padding = new Padding(28, 24, 28, 28);
            pnlWorkspace.Size = new Size(1260, 732);
            pnlWorkspace.TabIndex = 1;
            pnlSchedulesTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlSchedulesTable.BackColor = Color.Transparent;
            pnlSchedulesTable.BorderColor = Color.FromArgb(226, 232, 240);
            pnlSchedulesTable.BorderRadius = 8;
            pnlSchedulesTable.BorderThickness = 1;
            pnlSchedulesTable.Controls.Add(dgvSchedules);
            pnlSchedulesTable.Controls.Add(lblTableSubtitle);
            pnlSchedulesTable.Controls.Add(lblTableTitle);
            pnlSchedulesTable.FillColor = Color.White;
            pnlSchedulesTable.Location = new Point(28, 416);
            pnlSchedulesTable.Name = "pnlSchedulesTable";
            pnlSchedulesTable.Size = new Size(1204, 288);
            pnlSchedulesTable.TabIndex = 1;
            dgvSchedules.AllowUserToAddRows = false;
            dgvSchedules.AllowUserToDeleteRows = false;
            dgvSchedules.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(30, 64, 175);
            dgvSchedules.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvSchedules.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvSchedules.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvSchedules.ColumnHeadersHeight = 44;
            dgvSchedules.Columns.AddRange(new DataGridViewColumn[] { colScheduleId, colDayOfWeek, colSubject, colFacultyMember, colClassroom, colTimeSlot, colStudyYear, colBranch, colSection });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(30, 64, 175);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvSchedules.DefaultCellStyle = dataGridViewCellStyle3;
            dgvSchedules.GridColor = Color.FromArgb(226, 232, 240);
            dgvSchedules.Location = new Point(24, 78);
            dgvSchedules.MultiSelect = false;
            dgvSchedules.Name = "dgvSchedules";
            dgvSchedules.ReadOnly = true;
            dgvSchedules.RowHeadersVisible = false;
            dgvSchedules.RowTemplate.Height = 42;
            dgvSchedules.Size = new Size(1156, 186);
            dgvSchedules.TabIndex = 2;
            dgvSchedules.ThemeStyle.AlternatingRowsStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvSchedules.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.FromArgb(30, 41, 59);
            dgvSchedules.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvSchedules.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.FromArgb(30, 64, 175);
            dgvSchedules.ThemeStyle.GridColor = Color.FromArgb(226, 232, 240);
            dgvSchedules.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(15, 23, 42);
            dgvSchedules.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dgvSchedules.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvSchedules.ThemeStyle.HeaderStyle.Height = 44;
            dgvSchedules.ThemeStyle.ReadOnly = true;
            dgvSchedules.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 10F);
            dgvSchedules.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(30, 41, 59);
            dgvSchedules.ThemeStyle.RowsStyle.Height = 42;
            dgvSchedules.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvSchedules.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(30, 64, 175);
            colScheduleId.DataPropertyName = "ScheduleID";
            colScheduleId.FillWeight = 38F;
            colScheduleId.HeaderText = "ID";
            colScheduleId.Name = "colScheduleId";
            colScheduleId.ReadOnly = true;
            colDayOfWeek.DataPropertyName = "DayOfWeek";
            colDayOfWeek.FillWeight = 65F;
            colDayOfWeek.HeaderText = "Day";
            colDayOfWeek.Name = "colDayOfWeek";
            colDayOfWeek.ReadOnly = true;
            colSubject.DataPropertyName = "SubjectID";
            colSubject.HeaderText = "Subject";
            colSubject.Name = "colSubject";
            colSubject.ReadOnly = true;
            colFacultyMember.DataPropertyName = "FacultyMemberID";
            colFacultyMember.HeaderText = "Faculty";
            colFacultyMember.Name = "colFacultyMember";
            colFacultyMember.ReadOnly = true;
            colClassroom.DataPropertyName = "ClassroomID";
            colClassroom.FillWeight = 68F;
            colClassroom.HeaderText = "Room";
            colClassroom.Name = "colClassroom";
            colClassroom.ReadOnly = true;
            colTimeSlot.DataPropertyName = "TimeSlotID";
            colTimeSlot.FillWeight = 75F;
            colTimeSlot.HeaderText = "Time";
            colTimeSlot.Name = "colTimeSlot";
            colTimeSlot.ReadOnly = true;
            colStudyYear.DataPropertyName = "StudyYearID";
            colStudyYear.FillWeight = 72F;
            colStudyYear.HeaderText = "Year";
            colStudyYear.Name = "colStudyYear";
            colStudyYear.ReadOnly = true;
            colBranch.DataPropertyName = "BranchID";
            colBranch.FillWeight = 72F;
            colBranch.HeaderText = "Branch";
            colBranch.Name = "colBranch";
            colBranch.ReadOnly = true;
            colSection.DataPropertyName = "SectionID";
            colSection.FillWeight = 72F;
            colSection.HeaderText = "Section";
            colSection.Name = "colSection";
            colSection.ReadOnly = true;
            lblTableSubtitle.BackColor = Color.Transparent;
            lblTableSubtitle.Font = new Font("Segoe UI", 9F);
            lblTableSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblTableSubtitle.Location = new Point(24, 43);
            lblTableSubtitle.Name = "lblTableSubtitle";
            lblTableSubtitle.Size = new Size(235, 17);
            lblTableSubtitle.TabIndex = 1;
            lblTableSubtitle.Text = "Review and select timetable session records.";
            lblTableTitle.BackColor = Color.Transparent;
            lblTableTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            lblTableTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTableTitle.Location = new Point(24, 18);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new Size(109, 25);
            lblTableTitle.TabIndex = 0;
            lblTableTitle.Text = "Schedules List";
            pnlScheduleFilters.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlScheduleFilters.BackColor = Color.Transparent;
            pnlScheduleFilters.BorderRadius = 10;
            pnlScheduleFilters.Controls.Add(cmbDayFilter);
            pnlScheduleFilters.Controls.Add(lblDayFilter);
            pnlScheduleFilters.Controls.Add(cmbStudyYearFilter);
            pnlScheduleFilters.Controls.Add(lblStudyYearFilter);
            pnlScheduleFilters.Controls.Add(cmbSectionFilter);
            pnlScheduleFilters.Controls.Add(lblSectionFilter);
            pnlScheduleFilters.Controls.Add(cmbFacultyFilter);
            pnlScheduleFilters.Controls.Add(lblFacultyFilter);
            pnlScheduleFilters.FillColor = Color.White;
            pnlScheduleFilters.Location = new Point(28, 306);
            pnlScheduleFilters.Name = "pnlScheduleFilters";
            pnlScheduleFilters.Size = new Size(1204, 84);
            pnlScheduleFilters.TabIndex = 2;
            cmbDayFilter.BackColor = Color.Transparent;
            cmbDayFilter.BorderColor = Color.FromArgb(203, 213, 225);
            cmbDayFilter.BorderRadius = 8;
            cmbDayFilter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbDayFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDayFilter.FocusedColor = Color.FromArgb(37, 99, 235);
            cmbDayFilter.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbDayFilter.Font = new Font("Segoe UI", 10F);
            cmbDayFilter.ForeColor = Color.FromArgb(15, 23, 42);
            cmbDayFilter.ItemHeight = 30;
            cmbDayFilter.Location = new Point(664, 36);
            cmbDayFilter.Name = "cmbDayFilter";
            cmbDayFilter.Size = new Size(220, 36);
            cmbDayFilter.TabIndex = 7;
            lblDayFilter.BackColor = Color.Transparent;
            lblDayFilter.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblDayFilter.ForeColor = Color.FromArgb(15, 23, 42);
            lblDayFilter.Location = new Point(664, 14);
            lblDayFilter.Name = "lblDayFilter";
            lblDayFilter.Size = new Size(51, 17);
            lblDayFilter.TabIndex = 6;
            lblDayFilter.Text = "Day filter";
            cmbStudyYearFilter.BackColor = Color.Transparent;
            cmbStudyYearFilter.BorderColor = Color.FromArgb(203, 213, 225);
            cmbStudyYearFilter.BorderRadius = 8;
            cmbStudyYearFilter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbStudyYearFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStudyYearFilter.FocusedColor = Color.FromArgb(37, 99, 235);
            cmbStudyYearFilter.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbStudyYearFilter.Font = new Font("Segoe UI", 10F);
            cmbStudyYearFilter.ForeColor = Color.FromArgb(15, 23, 42);
            cmbStudyYearFilter.ItemHeight = 30;
            cmbStudyYearFilter.Location = new Point(456, 36);
            cmbStudyYearFilter.Name = "cmbStudyYearFilter";
            cmbStudyYearFilter.Size = new Size(180, 36);
            cmbStudyYearFilter.TabIndex = 5;
            lblStudyYearFilter.BackColor = Color.Transparent;
            lblStudyYearFilter.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblStudyYearFilter.ForeColor = Color.FromArgb(15, 23, 42);
            lblStudyYearFilter.Location = new Point(456, 14);
            lblStudyYearFilter.Name = "lblStudyYearFilter";
            lblStudyYearFilter.Size = new Size(86, 17);
            lblStudyYearFilter.TabIndex = 4;
            lblStudyYearFilter.Text = "Study year filter";
            cmbSectionFilter.BackColor = Color.Transparent;
            cmbSectionFilter.BorderColor = Color.FromArgb(203, 213, 225);
            cmbSectionFilter.BorderRadius = 8;
            cmbSectionFilter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSectionFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSectionFilter.FocusedColor = Color.FromArgb(37, 99, 235);
            cmbSectionFilter.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbSectionFilter.Font = new Font("Segoe UI", 10F);
            cmbSectionFilter.ForeColor = Color.FromArgb(15, 23, 42);
            cmbSectionFilter.ItemHeight = 30;
            cmbSectionFilter.Location = new Point(208, 36);
            cmbSectionFilter.Name = "cmbSectionFilter";
            cmbSectionFilter.Size = new Size(220, 36);
            cmbSectionFilter.TabIndex = 3;
            lblSectionFilter.BackColor = Color.Transparent;
            lblSectionFilter.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblSectionFilter.ForeColor = Color.FromArgb(15, 23, 42);
            lblSectionFilter.Location = new Point(208, 14);
            lblSectionFilter.Name = "lblSectionFilter";
            lblSectionFilter.Size = new Size(70, 17);
            lblSectionFilter.TabIndex = 2;
            lblSectionFilter.Text = "Section filter";
            cmbFacultyFilter.BackColor = Color.Transparent;
            cmbFacultyFilter.BorderColor = Color.FromArgb(203, 213, 225);
            cmbFacultyFilter.BorderRadius = 8;
            cmbFacultyFilter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFacultyFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFacultyFilter.FocusedColor = Color.FromArgb(37, 99, 235);
            cmbFacultyFilter.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbFacultyFilter.Font = new Font("Segoe UI", 10F);
            cmbFacultyFilter.ForeColor = Color.FromArgb(15, 23, 42);
            cmbFacultyFilter.ItemHeight = 30;
            cmbFacultyFilter.Location = new Point(24, 36);
            cmbFacultyFilter.Name = "cmbFacultyFilter";
            cmbFacultyFilter.Size = new Size(156, 36);
            cmbFacultyFilter.TabIndex = 1;
            lblFacultyFilter.BackColor = Color.Transparent;
            lblFacultyFilter.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblFacultyFilter.ForeColor = Color.FromArgb(15, 23, 42);
            lblFacultyFilter.Location = new Point(24, 14);
            lblFacultyFilter.Name = "lblFacultyFilter";
            lblFacultyFilter.Size = new Size(68, 17);
            lblFacultyFilter.TabIndex = 0;
            lblFacultyFilter.Text = "Faculty filter";
            pnlScheduleEditor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlScheduleEditor.BackColor = Color.Transparent;
            pnlScheduleEditor.BorderColor = Color.FromArgb(226, 232, 240);
            pnlScheduleEditor.BorderRadius = 8;
            pnlScheduleEditor.BorderThickness = 1;
            pnlScheduleEditor.Controls.Add(btnClearScheduleForm);
            pnlScheduleEditor.Controls.Add(btnExportSchedulePdf);
            pnlScheduleEditor.Controls.Add(btnGenerateSchedule);
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
            pnlScheduleEditor.FillColor = Color.White;
            pnlScheduleEditor.Location = new Point(28, 24);
            pnlScheduleEditor.Name = "pnlScheduleEditor";
            pnlScheduleEditor.Size = new Size(1204, 260);
            pnlScheduleEditor.TabIndex = 0;
            btnClearScheduleForm.BorderColor = Color.FromArgb(203, 213, 225);
            btnClearScheduleForm.BorderRadius = 8;
            btnClearScheduleForm.BorderThickness = 1;
            btnClearScheduleForm.Cursor = Cursors.Hand;
            btnClearScheduleForm.FillColor = Color.FromArgb(100, 116, 139);
            btnClearScheduleForm.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnClearScheduleForm.ForeColor = Color.White;
            btnClearScheduleForm.HoverState.FillColor = Color.FromArgb(248, 250, 252);
            btnClearScheduleForm.Location = new Point(512, 204);
            btnClearScheduleForm.Name = "btnClearScheduleForm";
            btnClearScheduleForm.Size = new Size(108, 38);
            btnClearScheduleForm.TabIndex = 23;
            btnClearScheduleForm.Text = "Clear";
            btnExportSchedulePdf.BorderRadius = 8;
            btnExportSchedulePdf.Cursor = Cursors.Hand;
            btnExportSchedulePdf.FillColor = Color.FromArgb(16, 185, 129);
            btnExportSchedulePdf.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnExportSchedulePdf.ForeColor = Color.White;
            btnExportSchedulePdf.HoverState.FillColor = Color.FromArgb(5, 150, 105);
            btnExportSchedulePdf.Location = new Point(640, 204);
            btnExportSchedulePdf.Name = "btnExportSchedulePdf";
            btnExportSchedulePdf.Size = new Size(130, 38);
            btnExportSchedulePdf.TabIndex = 25;
            btnExportSchedulePdf.Text = "Export PDF";
            btnGenerateSchedule.BorderRadius = 8;
            btnGenerateSchedule.Cursor = Cursors.Hand;
            btnGenerateSchedule.FillColor = Color.FromArgb(15, 23, 42);
            btnGenerateSchedule.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnGenerateSchedule.ForeColor = Color.White;
            btnGenerateSchedule.HoverState.FillColor = Color.FromArgb(30, 41, 59);
            btnGenerateSchedule.Location = new Point(24, 204);
            btnGenerateSchedule.Name = "btnGenerateSchedule";
            btnGenerateSchedule.Size = new Size(130, 38);
            btnGenerateSchedule.TabIndex = 24;
            btnGenerateSchedule.Text = "Generate";
            btnDeleteSchedule.BorderRadius = 8;
            btnDeleteSchedule.Cursor = Cursors.Hand;
            btnDeleteSchedule.FillColor = Color.FromArgb(220, 38, 38);
            btnDeleteSchedule.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnDeleteSchedule.ForeColor = Color.White;
            btnDeleteSchedule.HoverState.FillColor = Color.FromArgb(185, 28, 28);
            btnDeleteSchedule.Location = new Point(392, 204);
            btnDeleteSchedule.Name = "btnDeleteSchedule";
            btnDeleteSchedule.Size = new Size(108, 38);
            btnDeleteSchedule.TabIndex = 22;
            btnDeleteSchedule.Text = "Delete";
            btnUpdateSchedule.BorderRadius = 8;
            btnUpdateSchedule.Cursor = Cursors.Hand;
            btnUpdateSchedule.FillColor = Color.FromArgb(37, 99, 235);
            btnUpdateSchedule.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnUpdateSchedule.ForeColor = Color.White;
            btnUpdateSchedule.HoverState.FillColor = Color.FromArgb(21, 94, 117);
            btnUpdateSchedule.Location = new Point(272, 204);
            btnUpdateSchedule.Name = "btnUpdateSchedule";
            btnUpdateSchedule.Size = new Size(108, 38);
            btnUpdateSchedule.TabIndex = 21;
            btnUpdateSchedule.Text = "Update";
            btnAddSchedule.BorderRadius = 8;
            btnAddSchedule.Cursor = Cursors.Hand;
            btnAddSchedule.FillColor = Color.FromArgb(22, 163, 74);
            btnAddSchedule.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnAddSchedule.ForeColor = Color.White;
            btnAddSchedule.HoverState.FillColor = Color.FromArgb(21, 128, 61);
            btnAddSchedule.Location = new Point(160, 204);
            btnAddSchedule.Name = "btnAddSchedule";
            btnAddSchedule.Size = new Size(108, 38);
            btnAddSchedule.TabIndex = 20;
            btnAddSchedule.Text = "Add";
            cmbSection.BackColor = Color.Transparent;
            cmbSection.BorderColor = Color.FromArgb(203, 213, 225);
            cmbSection.BorderRadius = 8;
            cmbSection.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSection.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSection.FocusedColor = Color.FromArgb(37, 99, 235);
            cmbSection.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbSection.Font = new Font("Segoe UI", 10F);
            cmbSection.ForeColor = Color.FromArgb(15, 23, 42);
            cmbSection.HoverState.BorderColor = Color.FromArgb(59, 130, 246);
            cmbSection.ItemHeight = 36;
            cmbSection.Location = new Point(24, 144);
            cmbSection.Name = "cmbSection";
            cmbSection.Size = new Size(160, 42);
            cmbSection.TabIndex = 19;
            lblSection.BackColor = Color.Transparent;
            lblSection.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblSection.ForeColor = Color.FromArgb(51, 65, 85);
            lblSection.Location = new Point(24, 119);
            lblSection.Name = "lblSection";
            lblSection.Size = new Size(47, 19);
            lblSection.TabIndex = 18;
            lblSection.Text = "Section";
            cmbBranch.BackColor = Color.Transparent;
            cmbBranch.BorderColor = Color.FromArgb(203, 213, 225);
            cmbBranch.BorderRadius = 8;
            cmbBranch.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBranch.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBranch.FocusedColor = Color.FromArgb(37, 99, 235);
            cmbBranch.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbBranch.Font = new Font("Segoe UI", 10F);
            cmbBranch.ForeColor = Color.FromArgb(15, 23, 42);
            cmbBranch.HoverState.BorderColor = Color.FromArgb(59, 130, 246);
            cmbBranch.ItemHeight = 36;
            cmbBranch.Location = new Point(1080, 74);
            cmbBranch.Name = "cmbBranch";
            cmbBranch.Size = new Size(100, 42);
            cmbBranch.TabIndex = 17;
            lblBranch.BackColor = Color.Transparent;
            lblBranch.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblBranch.ForeColor = Color.FromArgb(51, 65, 85);
            lblBranch.Location = new Point(1080, 49);
            lblBranch.Name = "lblBranch";
            lblBranch.Size = new Size(45, 19);
            lblBranch.TabIndex = 16;
            lblBranch.Text = "Branch";
            cmbStudyYear.BackColor = Color.Transparent;
            cmbStudyYear.BorderColor = Color.FromArgb(203, 213, 225);
            cmbStudyYear.BorderRadius = 8;
            cmbStudyYear.DrawMode = DrawMode.OwnerDrawFixed;
            cmbStudyYear.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStudyYear.FocusedColor = Color.FromArgb(37, 99, 235);
            cmbStudyYear.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbStudyYear.Font = new Font("Segoe UI", 10F);
            cmbStudyYear.ForeColor = Color.FromArgb(15, 23, 42);
            cmbStudyYear.HoverState.BorderColor = Color.FromArgb(59, 130, 246);
            cmbStudyYear.ItemHeight = 36;
            cmbStudyYear.Location = new Point(904, 74);
            cmbStudyYear.Name = "cmbStudyYear";
            cmbStudyYear.Size = new Size(160, 42);
            cmbStudyYear.TabIndex = 15;
            lblStudyYear.BackColor = Color.Transparent;
            lblStudyYear.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblStudyYear.ForeColor = Color.FromArgb(51, 65, 85);
            lblStudyYear.Location = new Point(904, 49);
            lblStudyYear.Name = "lblStudyYear";
            lblStudyYear.Size = new Size(69, 19);
            lblStudyYear.TabIndex = 14;
            lblStudyYear.Text = "Study Year";
            cmbTimeSlot.BackColor = Color.Transparent;
            cmbTimeSlot.BorderColor = Color.FromArgb(203, 213, 225);
            cmbTimeSlot.BorderRadius = 8;
            cmbTimeSlot.DrawMode = DrawMode.OwnerDrawFixed;
            cmbTimeSlot.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTimeSlot.FocusedColor = Color.FromArgb(37, 99, 235);
            cmbTimeSlot.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbTimeSlot.Font = new Font("Segoe UI", 10F);
            cmbTimeSlot.ForeColor = Color.FromArgb(15, 23, 42);
            cmbTimeSlot.HoverState.BorderColor = Color.FromArgb(59, 130, 246);
            cmbTimeSlot.ItemHeight = 36;
            cmbTimeSlot.Location = new Point(552, 74);
            cmbTimeSlot.Name = "cmbTimeSlot";
            cmbTimeSlot.Size = new Size(160, 42);
            cmbTimeSlot.TabIndex = 13;
            lblTimeSlot.BackColor = Color.Transparent;
            lblTimeSlot.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblTimeSlot.ForeColor = Color.FromArgb(51, 65, 85);
            lblTimeSlot.Location = new Point(552, 49);
            lblTimeSlot.Name = "lblTimeSlot";
            lblTimeSlot.Size = new Size(59, 19);
            lblTimeSlot.TabIndex = 12;
            lblTimeSlot.Text = "Time Slot";
            cmbClassroom.BackColor = Color.Transparent;
            cmbClassroom.BorderColor = Color.FromArgb(203, 213, 225);
            cmbClassroom.BorderRadius = 8;
            cmbClassroom.DrawMode = DrawMode.OwnerDrawFixed;
            cmbClassroom.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClassroom.FocusedColor = Color.FromArgb(37, 99, 235);
            cmbClassroom.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbClassroom.Font = new Font("Segoe UI", 10F);
            cmbClassroom.ForeColor = Color.FromArgb(15, 23, 42);
            cmbClassroom.HoverState.BorderColor = Color.FromArgb(59, 130, 246);
            cmbClassroom.ItemHeight = 36;
            cmbClassroom.Location = new Point(376, 74);
            cmbClassroom.Name = "cmbClassroom";
            cmbClassroom.Size = new Size(160, 42);
            cmbClassroom.TabIndex = 11;
            lblClassroom.BackColor = Color.Transparent;
            lblClassroom.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblClassroom.ForeColor = Color.FromArgb(51, 65, 85);
            lblClassroom.Location = new Point(376, 49);
            lblClassroom.Name = "lblClassroom";
            lblClassroom.Size = new Size(66, 19);
            lblClassroom.TabIndex = 10;
            lblClassroom.Text = "Classroom";
            cmbFacultyMember.BackColor = Color.Transparent;
            cmbFacultyMember.BorderColor = Color.FromArgb(203, 213, 225);
            cmbFacultyMember.BorderRadius = 8;
            cmbFacultyMember.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFacultyMember.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFacultyMember.FocusedColor = Color.FromArgb(37, 99, 235);
            cmbFacultyMember.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbFacultyMember.Font = new Font("Segoe UI", 10F);
            cmbFacultyMember.ForeColor = Color.FromArgb(15, 23, 42);
            cmbFacultyMember.HoverState.BorderColor = Color.FromArgb(59, 130, 246);
            cmbFacultyMember.ItemHeight = 36;
            cmbFacultyMember.Location = new Point(200, 74);
            cmbFacultyMember.Name = "cmbFacultyMember";
            cmbFacultyMember.Size = new Size(160, 42);
            cmbFacultyMember.TabIndex = 9;
            lblFacultyMember.BackColor = Color.Transparent;
            lblFacultyMember.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblFacultyMember.ForeColor = Color.FromArgb(51, 65, 85);
            lblFacultyMember.Location = new Point(200, 49);
            lblFacultyMember.Name = "lblFacultyMember";
            lblFacultyMember.Size = new Size(101, 19);
            lblFacultyMember.TabIndex = 8;
            lblFacultyMember.Text = "Faculty Member";
            cmbSubject.BackColor = Color.Transparent;
            cmbSubject.BorderColor = Color.FromArgb(203, 213, 225);
            cmbSubject.BorderRadius = 8;
            cmbSubject.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSubject.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSubject.FocusedColor = Color.FromArgb(37, 99, 235);
            cmbSubject.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbSubject.Font = new Font("Segoe UI", 10F);
            cmbSubject.ForeColor = Color.FromArgb(15, 23, 42);
            cmbSubject.HoverState.BorderColor = Color.FromArgb(59, 130, 246);
            cmbSubject.ItemHeight = 36;
            cmbSubject.Location = new Point(24, 74);
            cmbSubject.Name = "cmbSubject";
            cmbSubject.Size = new Size(160, 42);
            cmbSubject.TabIndex = 7;
            lblSubject.BackColor = Color.Transparent;
            lblSubject.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblSubject.ForeColor = Color.FromArgb(51, 65, 85);
            lblSubject.Location = new Point(24, 49);
            lblSubject.Name = "lblSubject";
            lblSubject.Size = new Size(47, 19);
            lblSubject.TabIndex = 6;
            lblSubject.Text = "Subject";
            cmbDayOfWeek.BackColor = Color.Transparent;
            cmbDayOfWeek.BorderColor = Color.FromArgb(203, 213, 225);
            cmbDayOfWeek.BorderRadius = 8;
            cmbDayOfWeek.DrawMode = DrawMode.OwnerDrawFixed;
            cmbDayOfWeek.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDayOfWeek.FocusedColor = Color.FromArgb(37, 99, 235);
            cmbDayOfWeek.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbDayOfWeek.Font = new Font("Segoe UI", 10F);
            cmbDayOfWeek.ForeColor = Color.FromArgb(15, 23, 42);
            cmbDayOfWeek.HoverState.BorderColor = Color.FromArgb(59, 130, 246);
            cmbDayOfWeek.ItemHeight = 36;
            cmbDayOfWeek.Items.AddRange(new object[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday" });
            cmbDayOfWeek.Location = new Point(728, 74);
            cmbDayOfWeek.Name = "cmbDayOfWeek";
            cmbDayOfWeek.Size = new Size(160, 42);
            cmbDayOfWeek.TabIndex = 5;
            lblDayOfWeek.BackColor = Color.Transparent;
            lblDayOfWeek.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblDayOfWeek.ForeColor = Color.FromArgb(51, 65, 85);
            lblDayOfWeek.Location = new Point(728, 49);
            lblDayOfWeek.Name = "lblDayOfWeek";
            lblDayOfWeek.Size = new Size(82, 19);
            lblDayOfWeek.TabIndex = 4;
            lblDayOfWeek.Text = "Day Of Week";
            txtScheduleId.BorderColor = Color.FromArgb(226, 232, 240);
            txtScheduleId.BorderRadius = 8;
            txtScheduleId.Cursor = Cursors.IBeam;
            txtScheduleId.DefaultText = "";
            txtScheduleId.DisabledState.BorderColor = Color.FromArgb(226, 232, 240);
            txtScheduleId.DisabledState.FillColor = Color.FromArgb(248, 250, 252);
            txtScheduleId.DisabledState.ForeColor = Color.FromArgb(100, 116, 139);
            txtScheduleId.Enabled = false;
            txtScheduleId.FillColor = Color.FromArgb(248, 250, 252);
            txtScheduleId.Font = new Font("Segoe UI", 10F);
            txtScheduleId.ForeColor = Color.FromArgb(100, 116, 139);
            txtScheduleId.Location = new Point(24, 84);
            txtScheduleId.Margin = new Padding(3, 4, 3, 4);
            txtScheduleId.Name = "txtScheduleId";
            txtScheduleId.PlaceholderForeColor = Color.FromArgb(148, 163, 184);
            txtScheduleId.PlaceholderText = "Auto";
            txtScheduleId.SelectedText = "";
            txtScheduleId.Size = new Size(132, 42);
            txtScheduleId.TabIndex = 3;
            txtScheduleId.Visible = false;
            lblScheduleId.BackColor = Color.Transparent;
            lblScheduleId.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblScheduleId.ForeColor = Color.FromArgb(51, 65, 85);
            lblScheduleId.Location = new Point(24, 59);
            lblScheduleId.Name = "lblScheduleId";
            lblScheduleId.Size = new Size(74, 19);
            lblScheduleId.TabIndex = 2;
            lblScheduleId.Text = "Schedule ID";
            lblScheduleId.Visible = false;
            lblEditorSubtitle.BackColor = Color.Transparent;
            lblEditorSubtitle.Font = new Font("Segoe UI", 9F);
            lblEditorSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblEditorSubtitle.Location = new Point(24, 34);
            lblEditorSubtitle.Name = "lblEditorSubtitle";
            lblEditorSubtitle.Size = new Size(312, 17);
            lblEditorSubtitle.TabIndex = 1;
            lblEditorSubtitle.Text = "Prepare schedule session details before applying an action.";
            lblEditorTitle.BackColor = Color.Transparent;
            lblEditorTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            lblEditorTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblEditorTitle.Location = new Point(24, 9);
            lblEditorTitle.Name = "lblEditorTitle";
            lblEditorTitle.Size = new Size(128, 25);
            lblEditorTitle.TabIndex = 0;
            lblEditorTitle.Text = "Schedule Details";
            pnlHeader.Controls.Add(lblPageSubtitle);
            pnlHeader.Controls.Add(btnRefreshSchedule);
            pnlHeader.Controls.Add(lblPageTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.FillColor = Color.White;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1260, 88);
            pnlHeader.TabIndex = 0;
            lblPageSubtitle.BackColor = Color.Transparent;
            lblPageSubtitle.Font = new Font("Segoe UI", 10F);
            lblPageSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblPageSubtitle.Location = new Point(32, 50);
            lblPageSubtitle.Name = "lblPageSubtitle";
            lblPageSubtitle.Size = new Size(395, 19);
            lblPageSubtitle.TabIndex = 1;
            lblPageSubtitle.Text = "Manage final timetable sessions by day, room, instructor, and time.";
            btnRefreshSchedule.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefreshSchedule.BorderRadius = 8;
            btnRefreshSchedule.Cursor = Cursors.Hand;
            btnRefreshSchedule.FillColor = Color.FromArgb(74, 115, 217);
            btnRefreshSchedule.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnRefreshSchedule.ForeColor = Color.White;
            btnRefreshSchedule.HoverState.FillColor = Color.FromArgb(37, 99, 235);
            btnRefreshSchedule.Location = new Point(1106, 26);
            btnRefreshSchedule.Name = "btnRefreshSchedule";
            btnRefreshSchedule.Size = new Size(112, 38);
            btnRefreshSchedule.TabIndex = 2;
            btnRefreshSchedule.Text = "Refresh";
            lblPageTitle.BackColor = Color.Transparent;
            lblPageTitle.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblPageTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblPageTitle.Location = new Point(32, 16);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new Size(264, 34);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Schedules Management";
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1500, 820);
            Controls.Add(pnlMain);
            Controls.Add(pnlSidebar);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1180, 720);
            Name = "SchedulesForm";
            Text = "Schedules Management";
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlWorkspace.ResumeLayout(false);
            pnlSchedulesTable.ResumeLayout(false);
            pnlSchedulesTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSchedules).EndInit();
            pnlScheduleFilters.ResumeLayout(false);
            pnlScheduleFilters.PerformLayout();
            pnlScheduleEditor.ResumeLayout(false);
            pnlScheduleEditor.PerformLayout();
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
        private Guna.UI2.WinForms.Guna2Button btnRefreshSchedule = null!;
        private Guna.UI2.WinForms.Guna2Panel pnlWorkspace = null!;
        private Guna.UI2.WinForms.Guna2Panel pnlScheduleFilters = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbFacultyFilter = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblFacultyFilter = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSectionFilter = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSectionFilter = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbStudyYearFilter = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblStudyYearFilter = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbDayFilter = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDayFilter = null!;
        private Guna.UI2.WinForms.Guna2Panel pnlScheduleEditor = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEditorTitle = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEditorSubtitle = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblScheduleId = null!;
        private Guna.UI2.WinForms.Guna2TextBox txtScheduleId = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDayOfWeek = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbDayOfWeek = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubject = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSubject = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblFacultyMember = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbFacultyMember = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblClassroom = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbClassroom = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTimeSlot = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbTimeSlot = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblStudyYear = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbStudyYear = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblBranch = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbBranch = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSection = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSection = null!;
        private Guna.UI2.WinForms.Guna2Button btnGenerateSchedule = null!;
        private Guna.UI2.WinForms.Guna2Button btnAddSchedule = null!;
        private Guna.UI2.WinForms.Guna2Button btnUpdateSchedule = null!;
        private Guna.UI2.WinForms.Guna2Button btnDeleteSchedule = null!;
        private Guna.UI2.WinForms.Guna2Button btnClearScheduleForm = null!;
        private Guna.UI2.WinForms.Guna2Button btnExportSchedulePdf = null!;
        private Guna.UI2.WinForms.Guna2Panel pnlSchedulesTable = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableTitle = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableSubtitle = null!;
        private Guna.UI2.WinForms.Guna2DataGridView dgvSchedules = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScheduleId = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDayOfWeek = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubject = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFacultyMember = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClassroom = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeSlot = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudyYear = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBranch = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSection = null!;
}
}
