using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class FacultyMemberSubjectsForm : System.Windows.Forms.UserControl
    {
        private readonly FacultyMemberSubjectService assignmentService = new();
        private readonly FacultyMemberService facultyMemberService = new();
        private readonly SubjectService subjectService = new();

        private int? selectedFacultyMemberId;
        private int? selectedSubjectId;

        public FacultyMemberSubjectsForm()
        {
            InitializeComponent();
            ConfigureNavigation();
            ConfigureAssignmentsGrid();
            ConfigureAssignmentEvents();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await RefreshAssignmentsAsync();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.FacultyAssignments);
        }

        private void ConfigureAssignmentsGrid()
        {
            dgvFacultyMemberSubjects.AutoGenerateColumns = false;
            dgvFacultyMemberSubjects.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            colFacultyMemberId.DataPropertyName = nameof(AssignmentRow.FacultyMemberID);
            colFacultyMember.DataPropertyName = nameof(AssignmentRow.FacultyMemberName);
            colSubjectId.DataPropertyName = nameof(AssignmentRow.SubjectID);
            colSubject.DataPropertyName = nameof(AssignmentRow.SubjectName);
        }

        private void ConfigureAssignmentEvents()
        {
            dgvFacultyMemberSubjects.SelectionChanged += (_, _) => PopulateAssignmentEditorFromSelection();
            btnAddAssignment.Click += async (_, _) => await AddAssignmentAsync();
            btnUpdateAssignment.Click += async (_, _) => await UpdateAssignmentAsync();
            btnDeleteAssignment.Click += async (_, _) => await DeleteAssignmentAsync();
        }

        private async Task RefreshAssignmentsAsync()
        {
            SetAssignmentActionsEnabled(false);

            try
            {
                await LoadLookupsAsync();
                await LoadAssignmentsAsync();
                ClearAssignmentForm();
            }
            catch (Exception ex)
            {
                ShowError("Unable to refresh teaching assignments.", ex);
            }
            finally
            {
                SetAssignmentActionsEnabled(true);
            }
        }

        private async Task LoadLookupsAsync()
        {
            var facultyMembers = await facultyMemberService.GetAllAsync();
            var subjects = await subjectService.GetAllAsync();

            BindCombo(
                cmbFacultyMember,
                facultyMembers
                    .OrderBy(facultyMember => facultyMember.FullName)
                    .Select(facultyMember => new ComboOption(facultyMember.FacultyMemberID, facultyMember.FullName)));

            BindCombo(
                cmbSubject,
                subjects
                    .OrderBy(subject => subject.StudyYearID)
                    .ThenBy(subject => subject.BranchID ?? 0)
                    .ThenBy(subject => subject.SubjectName)
                    .Select(subject => new ComboOption(subject.SubjectID, FormatSubject(subject))));
        }

        private async Task LoadAssignmentsAsync()
        {
            var assignments = await assignmentService.GetAllAsync();
            dgvFacultyMemberSubjects.DataSource = assignments
                .Select(AssignmentRow.FromAssignment)
                .OrderBy(row => row.SubjectName)
                .ThenBy(row => row.FacultyMemberName)
                .ToList();
            dgvFacultyMemberSubjects.ClearSelection();
        }

        private async Task AddAssignmentAsync()
        {
            if (!TryBuildAssignment(out var assignment))
            {
                return;
            }

            await ExecuteAssignmentActionAsync(
                async () => await assignmentService.AddAsync(assignment),
                "Teaching assignment added successfully.");
        }

        private async Task UpdateAssignmentAsync()
        {
            if (!selectedFacultyMemberId.HasValue || !selectedSubjectId.HasValue)
            {
                ShowInformation("Select a teaching assignment before updating.");
                return;
            }

            if (!TryBuildAssignment(out var assignment))
            {
                return;
            }

            await ExecuteAssignmentActionAsync(
                async () => await assignmentService.UpdateAsync(
                    selectedFacultyMemberId.Value,
                    selectedSubjectId.Value,
                    assignment),
                "Teaching assignment updated successfully.");
        }

        private async Task DeleteAssignmentAsync()
        {
            if (!selectedFacultyMemberId.HasValue || !selectedSubjectId.HasValue)
            {
                ShowInformation("Select a teaching assignment before deleting.");
                return;
            }

            var confirmation = MessageBox.Show(
                this,
                "Are you sure you want to delete the selected teaching assignment?",
                "Delete Teaching Assignment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            await ExecuteAssignmentActionAsync(
                async () => await assignmentService.DeleteAsync(selectedFacultyMemberId.Value, selectedSubjectId.Value),
                "Teaching assignment deleted successfully.");
        }

        private async Task ExecuteAssignmentActionAsync(Func<Task> action, string successMessage)
        {
            SetAssignmentActionsEnabled(false);

            try
            {
                await action();
                await LoadAssignmentsAsync();
                ClearAssignmentForm();
                ShowInformation(successMessage);
            }
            catch (Exception ex)
            {
                ShowError("Unable to complete the teaching assignment operation.", ex);
            }
            finally
            {
                SetAssignmentActionsEnabled(true);
            }
        }

        private bool TryBuildAssignment(out FacultyMemberSubject assignment)
        {
            assignment = new FacultyMemberSubject
            {
                FacultyMemberID = GetSelectedRequiredId(cmbFacultyMember),
                SubjectID = GetSelectedRequiredId(cmbSubject)
            };

            if (assignment.FacultyMemberID <= 0)
            {
                ShowInformation("Faculty member is required.");
                cmbFacultyMember.Focus();
                return false;
            }

            if (assignment.SubjectID <= 0)
            {
                ShowInformation("Subject is required.");
                cmbSubject.Focus();
                return false;
            }

            return true;
        }

        private void PopulateAssignmentEditorFromSelection()
        {
            if (dgvFacultyMemberSubjects.CurrentRow?.DataBoundItem is not AssignmentRow row)
            {
                return;
            }

            selectedFacultyMemberId = row.FacultyMemberID;
            selectedSubjectId = row.SubjectID;
            SelectComboValue(cmbFacultyMember, row.FacultyMemberID);
            SelectComboValue(cmbSubject, row.SubjectID);
        }

        private void ClearAssignmentForm()
        {
            selectedFacultyMemberId = null;
            selectedSubjectId = null;
            ClearCombo(cmbFacultyMember);
            ClearCombo(cmbSubject);
            dgvFacultyMemberSubjects.ClearSelection();
        }

        private void SetAssignmentActionsEnabled(bool enabled)
        {
            btnAddAssignment.Enabled = enabled;
            btnUpdateAssignment.Enabled = enabled;
            btnDeleteAssignment.Enabled = enabled;
            dgvFacultyMemberSubjects.Enabled = enabled;
        }

        private static void BindCombo(Guna.UI2.WinForms.Guna2ComboBox combo, IEnumerable<ComboOption> options)
        {
            combo.DataSource = options.ToList();
            combo.DisplayMember = nameof(ComboOption.Text);
            combo.ValueMember = nameof(ComboOption.Id);
            combo.SelectedIndex = -1;
        }

        private static int GetSelectedRequiredId(Guna.UI2.WinForms.Guna2ComboBox combo)
        {
            return combo.SelectedItem is ComboOption option ? option.Id : 0;
        }

        private static void SelectComboValue(Guna.UI2.WinForms.Guna2ComboBox combo, int id)
        {
            foreach (var item in combo.Items)
            {
                if (item is ComboOption option && option.Id == id)
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

        private static string FormatSubject(Subject subject)
        {
            var year = subject.StudyYear?.YearName ?? "Year";
            var branch = subject.Branch?.BranchName ?? "General";
            return $"{subject.SubjectName} - {year} - {branch}";
        }

        private void ShowInformation(string message)
        {
            MessageBox.Show(this, message, "Teaching Assignments", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show(this, $"{message}\n\n{ex.Message}", "Teaching Assignments", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private sealed record ComboOption(int Id, string Text);

        private sealed class AssignmentRow
        {
            public int FacultyMemberID { get; init; }
            public string FacultyMemberName { get; init; } = string.Empty;
            public int SubjectID { get; init; }
            public string SubjectName { get; init; } = string.Empty;

            public static AssignmentRow FromAssignment(FacultyMemberSubject assignment)
            {
                return new AssignmentRow
                {
                    FacultyMemberID = assignment.FacultyMemberID,
                    FacultyMemberName = assignment.FacultyMember?.FullName ?? "-",
                    SubjectID = assignment.SubjectID,
                    SubjectName = assignment.Subject is null ? "-" : FormatSubject(assignment.Subject)
                };
            }
        }
    }
}
