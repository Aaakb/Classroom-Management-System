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

        public SchedulesForm()
        {
            InitializeComponent();
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

        private void ConfigureScheduleGrid()
        {
            dgvSchedules.AutoGenerateColumns = false;
            dgvSchedules.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            colScheduleId.DataPropertyName = nameof(ScheduleRow.ScheduleID);
            colDayOfWeek.DataPropertyName = nameof(ScheduleRow.DayOfWeek);
            colSubject.DataPropertyName = nameof(ScheduleRow.SubjectName);
            colFacultyMember.DataPropertyName = nameof(ScheduleRow.FacultyMemberName);
            colClassroom.DataPropertyName = nameof(ScheduleRow.ClassroomName);
            colTimeSlot.DataPropertyName = nameof(ScheduleRow.TimeSlotName);
            colStudyYear.DataPropertyName = nameof(ScheduleRow.StudyYearName);
            colBranch.DataPropertyName = nameof(ScheduleRow.BranchName);
            colSection.DataPropertyName = nameof(ScheduleRow.SectionName);
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

            dgvSchedules.SelectionChanged += (_, _) => PopulateScheduleEditorFromSelection();
            cmbSubject.SelectedIndexChanged += (_, _) => ApplySubjectSelection();
            cmbStudyYear.SelectedIndexChanged += (_, _) => ApplyStudyYearSelection();
            cmbBranch.SelectedIndexChanged += (_, _) => ApplyBranchSelection();
            cmbSection.SelectedIndexChanged += (_, _) => ApplySectionSelection();
            cmbFacultyFilter.SelectedIndexChanged += (_, _) => ApplyScheduleFilters();
            cmbSectionFilter.SelectedIndexChanged += (_, _) => ApplySectionFilterSelection();
            cmbStudyYearFilter.SelectedIndexChanged += (_, _) => ApplyStudyYearFilterSelection();
            cmbDayFilter.SelectedIndexChanged += (_, _) => ApplyScheduleFilters();
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
        }

        private async Task LoadSchedulesAsync()
        {
            var schedules = await scheduleService.GetAllAsync();
            scheduleRows = schedules
                .Select(ScheduleRow.FromSchedule)
                .OrderBy(row => DayOrder(row.DayOfWeek))
                .ThenBy(row => row.TimeSlotName)
                .ThenBy(row => row.StudyYearName)
                .ThenBy(row => row.SectionName)
                .ToList();

            ApplyScheduleFilters();
        }

        private async Task GenerateScheduleAsync()
        {
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
            string? day = cmbDayFilter.SelectedItem as string;

            return scheduleRows.Where(row =>
                (!facultyId.HasValue || row.FacultyMemberID == facultyId.Value) &&
                (!sectionId.HasValue || row.SectionID == sectionId.Value) &&
                (!studyYearId.HasValue || row.StudyYearID == studyYearId.Value) &&
                (string.IsNullOrWhiteSpace(day) || day == "All days" || row.DayOfWeek == day));
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

        private void PopulateScheduleEditorFromSelection()
        {
            if (dgvSchedules.CurrentRow?.DataBoundItem is not ScheduleRow row)
            {
                return;
            }

            txtScheduleId.Text = row.ScheduleID.ToString();
            cmbDayOfWeek.SelectedItem = row.DayOfWeek;
            SelectComboValue(cmbSubject, row.SubjectID);
            SelectComboValue(cmbFacultyMember, row.FacultyMemberID);
            SelectComboValue(cmbClassroom, row.ClassroomID);
            SelectComboValue(cmbTimeSlot, row.TimeSlotID);
            SelectComboValue(cmbStudyYear, row.StudyYearID);
            SelectComboValue(cmbBranch, row.BranchID);
            BindSectionsCombo(row.StudyYearID, row.BranchID, row.SectionID);
            BindSubjectsCombo(row.StudyYearID, row.BranchID, row.SubjectID);
            SelectComboValue(cmbSection, row.SectionID);
        }

        private void ClearGeneratedScheduleTable()
        {
            scheduleRows = [];
            ApplyScheduleFilters();
            ClearScheduleForm();
            ShowInformation("Schedule table cleared from the current view. Database records were not deleted.");
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
            BindSectionsCombo();
            BindSubjectsCombo();
            ClearCombo(cmbSection);
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
                .Where(subject => !studyYearId.HasValue || subject.StudyYearID == studyYearId.Value)
                .Where(subject => !branchId.HasValue || !subject.BranchID.HasValue || subject.BranchID == branchId.Value)
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
                .Where(section => !studyYearId.HasValue || section.StudyYearID == studyYearId.Value)
                .Where(section => !branchId.HasValue || section.BranchID == branchId.Value)
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
                .Where(section => !studyYearId.HasValue || section.StudyYearID == studyYearId.Value)
                .OrderBy(section => section.StudyYearID)
                .ThenBy(section => section.BranchID ?? 0)
                .ThenBy(section => section.SectionName)
                .Select(section => new ComboOption(section.SectionID, FormatSection(section)));

            BindFilterCombo(cmbSectionFilter, sections, "All sections");
            SelectComboValue(cmbSectionFilter, selectedSectionId);
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
            BindSectionsCombo(studyYearId, branchId);
            BindSubjectsCombo(studyYearId, branchId);
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
            BindSectionsCombo(studyYearId, branchId);
            BindSubjectsCombo(studyYearId, branchId);
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
            isUpdatingScheduleLookups = false;
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

        private static string FormatTimeSlot(TimeSlot timeSlot)
        {
            string label = $"{timeSlot.StartTime:hh\\:mm} - {timeSlot.EndTime:hh\\:mm}";
            return timeSlot.IsBreak ? $"{label} (Break)" : label;
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
            public int? StudyYearID { get; init; }
            public int? BranchID { get; init; }
            public int? SectionID { get; init; }
            public string DayOfWeek { get; init; } = string.Empty;
            public string SubjectName { get; init; } = string.Empty;
            public string FacultyMemberName { get; init; } = string.Empty;
            public string ClassroomName { get; init; } = string.Empty;
            public string TimeSlotName { get; init; } = string.Empty;
            public string StudyYearName { get; init; } = string.Empty;
            public string BranchName { get; init; } = string.Empty;
            public string SectionName { get; init; } = string.Empty;

            public static ScheduleRow FromSchedule(Schedule schedule)
            {
                return new ScheduleRow
                {
                    ScheduleID = schedule.ScheduleID,
                    SubjectID = schedule.SubjectID,
                    FacultyMemberID = schedule.FacultyMemberID,
                    ClassroomID = schedule.ClassroomID,
                    TimeSlotID = schedule.TimeSlotID,
                    StudyYearID = schedule.StudyYearID,
                    BranchID = schedule.BranchID,
                    SectionID = schedule.SectionID,
                    DayOfWeek = schedule.DayOfWeek,
                    SubjectName = schedule.Subject?.SubjectName ?? "-",
                    FacultyMemberName = schedule.FacultyMember?.FullName ?? "-",
                    ClassroomName = schedule.Classroom?.ClassroomNumber ?? "-",
                    TimeSlotName = schedule.TimeSlot is null ? "-" : FormatTimeSlot(schedule.TimeSlot),
                    StudyYearName = schedule.StudyYear?.YearName ?? "-",
                    BranchName = schedule.Branch?.BranchName ?? "-",
                    SectionName = FormatScheduleSection(schedule)
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
