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
            ConfigureScheduleCommandText();
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

        private void ConfigureScheduleCommandText()
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
            dgvSchedules.ColumnHeadersHeight = 40;
            dgvSchedules.EnableHeadersVisualStyles = false;
            dgvSchedules.RowTemplate.Height = 36;
            dgvSchedules.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

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

            column.DisplayIndex = 0;
            colStudyYear.DisplayIndex = 1;
            colBranch.DisplayIndex = 2;
            colSection.DisplayIndex = 3;
            dgvSchedules.Columns["colGroupName"].DisplayIndex = 4;
            dgvSchedules.Columns["colLectureType"].DisplayIndex = 5;
            colSubject.DisplayIndex = 6;
            colFacultyMember.DisplayIndex = 7;
            colClassroom.DisplayIndex = 8;
            colDayOfWeek.DisplayIndex = 9;
            colTimeSlot.DisplayIndex = 10;
            dgvSchedules.Columns["colEndTime"].DisplayIndex = 11;
        }

        private void ApplyScheduleGridColumnLayout()
        {
            SetGridColumn(colStudyYear, "Year", 78);
            SetGridColumn(colBranch, "Branch", 92);
            SetGridColumn(colSection, "Section", 66);
            SetGridColumn(dgvSchedules.Columns["colGroupName"], "Group", 58);
            SetGridColumn(dgvSchedules.Columns["colLectureType"], "Lecture Type", 82);
            SetGridColumn(colSubject, "Subject", 148);
            SetGridColumn(colFacultyMember, "Faculty", 132);
            SetGridColumn(colClassroom, "Room/Lab", 78);
            SetGridColumn(colDayOfWeek, "Day", 74);
            SetGridColumn(colTimeSlot, "Start", 58);
            SetGridColumn(dgvSchedules.Columns["colEndTime"], "End", 58);

            dgvSchedules.DefaultCellStyle.Padding = new Padding(2, 0, 2, 0);
            dgvSchedules.ColumnHeadersDefaultCellStyle.Padding = new Padding(2, 0, 2, 0);
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
                (string.IsNullOrWhiteSpace(groupName) ||
                    groupName == "All" ||
                    row.GroupName == groupName) &&
                RowMatchesFacultyFilter(row, facultyId) &&
                RowMatchesSectionFilter(row, sectionId) &&
                RowMatchesStudyYearFilter(row, studyYearId) &&
                (string.IsNullOrWhiteSpace(day) || day == "All days" || row.DayOfWeek == day));
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
    }
}
