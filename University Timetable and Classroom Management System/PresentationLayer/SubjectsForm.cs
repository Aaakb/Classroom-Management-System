using System.Globalization;
using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SubjectsForm : System.Windows.Forms.UserControl
    {
        private readonly SubjectService subjectService = new();
        private readonly StudyYearService studyYearService = new();
        private readonly BranchService branchService = new();

        private List<StudyYear> studyYearsLookup = [];
        private List<Branch> branchesLookup = [];

        public SubjectsForm()
        {
            InitializeComponent();
            ConfigureNavigation();
            ConfigureSubjectsGrid();
            ConfigureSubjectsEvents();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await RefreshSubjectsAsync();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.Subjects);
        }

        private void ConfigureSubjectsGrid()
        {
            dgvSubjects.AutoGenerateColumns = false;
            dgvSubjects.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            colSubjectId.DataPropertyName = nameof(SubjectRow.SubjectID);
            colSubjectName.DataPropertyName = nameof(SubjectRow.SubjectName);
            colStudyYear.DataPropertyName = nameof(SubjectRow.StudyYearName);
            colBranch.DataPropertyName = nameof(SubjectRow.BranchName);
            colSemester.DataPropertyName = nameof(SubjectRow.SemesterNumber);
            colTheoreticalHours.DataPropertyName = nameof(SubjectRow.TheoreticalHours);
            colPracticalHours.DataPropertyName = nameof(SubjectRow.PracticalHours);
            colCreditUnits.DataPropertyName = nameof(SubjectRow.CreditUnits);
            colRequirementType.DataPropertyName = nameof(SubjectRow.RequirementType);
        }

        private void ConfigureSubjectsEvents()
        {
            dgvSubjects.SelectionChanged += (_, _) => PopulateSubjectEditorFromSelection();
            txtSubjectId.Leave += async (_, _) => await PopulateSubjectEditorFromEnteredIdAsync();
            btnAddSubject.Click += async (_, _) => await AddSubjectAsync();
            btnUpdateSubject.Click += async (_, _) => await UpdateSubjectAsync();
            btnDeleteSubject.Click += async (_, _) => await DeleteSubjectAsync();
        }

        private async Task RefreshSubjectsAsync()
        {
            SetSubjectActionsEnabled(false);

            try
            {
                await LoadLookupsAsync();
                await LoadSubjectsAsync();
                ClearSubjectForm();
            }
            catch (Exception ex)
            {
                ShowError("Unable to refresh subjects.", ex);
            }
            finally
            {
                SetSubjectActionsEnabled(true);
            }
        }

        private async Task LoadLookupsAsync()
        {
            studyYearsLookup = await studyYearService.GetAllAsync();
            branchesLookup = await branchService.GetAllAsync();

            BindCombo(cmbStudyYear, studyYearsLookup.Select(studyYear => new ComboOption(studyYear.StudyYearID, studyYear.YearName)));
            BindBranchCombo();
        }

        private async Task LoadSubjectsAsync()
        {
            var subjects = await subjectService.GetAllAsync();
            dgvSubjects.DataSource = subjects
                .Select(SubjectRow.FromSubject)
                .OrderBy(row => row.StudyYearID)
                .ThenBy(row => row.BranchID ?? 0)
                .ThenBy(row => row.SemesterNumber)
                .ThenBy(row => row.SubjectName)
                .ToList();
            dgvSubjects.ClearSelection();
        }

        private async Task AddSubjectAsync()
        {
            if (!TryBuildSubject(out var subject))
            {
                return;
            }

            await ExecuteSubjectActionAsync(
                async () => await subjectService.AddAsync(subject),
                "Subject added successfully.");
        }

        private async Task UpdateSubjectAsync()
        {
            if (!TryBuildSubject(out var subject))
            {
                return;
            }

            await ExecuteSubjectActionAsync(
                async () => await subjectService.UpdateAsync(subject),
                "Subject updated successfully.");
        }

        private async Task DeleteSubjectAsync()
        {
            if (!TryGetSubjectIdFromEditor(out int subjectId))
            {
                return;
            }

            var confirmation = MessageBox.Show(
                this,
                "Are you sure you want to delete the selected subject?",
                "Delete Subject",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            await ExecuteSubjectActionAsync(
                async () => await subjectService.DeleteAsync(subjectId),
                "Subject deleted successfully.");
        }

        private async Task ExecuteSubjectActionAsync(Func<Task> action, string successMessage)
        {
            SetSubjectActionsEnabled(false);

            try
            {
                await action();
                await LoadSubjectsAsync();
                ClearSubjectForm();
                ShowInformation(successMessage);
            }
            catch (Exception ex)
            {
                ShowError("Unable to complete the subject operation.", ex);
            }
            finally
            {
                SetSubjectActionsEnabled(true);
            }
        }

        private bool TryBuildSubject(out Subject subject)
        {
            subject = new Subject
            {
                SubjectName = txtSubjectName.Text.Trim(),
                StudyYearID = GetSelectedRequiredId(cmbStudyYear),
                BranchID = GetSelectedOptionalId(cmbBranch),
                RequirementType = cmbRequirementType.Text.Trim()
            };

            if (!TryGetSubjectIdFromEditor(out int subjectId))
            {
                return false;
            }

            subject.SubjectID = subjectId;

            if (string.IsNullOrWhiteSpace(subject.SubjectName))
            {
                ShowInformation("Subject name is required.");
                txtSubjectName.Focus();
                return false;
            }

            if (subject.StudyYearID <= 0)
            {
                ShowInformation("Study year is required.");
                cmbStudyYear.Focus();
                return false;
            }

            if (!int.TryParse(cmbSemester.Text, out int semester) || semester <= 0)
            {
                ShowInformation("Semester is required.");
                cmbSemester.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(subject.RequirementType))
            {
                ShowInformation("Requirement type is required.");
                cmbRequirementType.Focus();
                return false;
            }

            if (!TryReadNonNegativeDouble(txtTheoreticalHours.Text, "Theoretical hours", out double theoreticalHours))
            {
                txtTheoreticalHours.Focus();
                return false;
            }

            if (!TryReadNonNegativeDouble(txtPracticalHours.Text, "Practical hours", out double practicalHours))
            {
                txtPracticalHours.Focus();
                return false;
            }

            if (!TryReadNonNegativeDouble(txtCreditUnits.Text, "Credit units", out double creditUnits))
            {
                txtCreditUnits.Focus();
                return false;
            }

            subject.SemesterNumber = semester;
            subject.TheoreticalHours = theoreticalHours;
            subject.PracticalHours = practicalHours;
            subject.CreditUnits = creditUnits;
            return true;
        }

        private bool TryGetSubjectIdFromEditor(out int subjectId)
        {
            if (int.TryParse(txtSubjectId.Text, out subjectId) && subjectId > 0)
            {
                return true;
            }

            ShowInformation("Enter a valid subject ID.");
            txtSubjectId.Focus();
            return false;
        }

        private bool TryReadNonNegativeDouble(string text, string fieldName, out double value)
        {
            if ((double.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out value) ||
                 double.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value)) &&
                value >= 0)
            {
                return true;
            }

            ShowInformation($"{fieldName} must be zero or greater.");
            return false;
        }

        private void PopulateSubjectEditorFromSelection()
        {
            if (dgvSubjects.CurrentRow?.DataBoundItem is not SubjectRow row)
            {
                return;
            }

            PopulateSubjectEditor(row);
        }

        private async Task PopulateSubjectEditorFromEnteredIdAsync()
        {
            if (!int.TryParse(txtSubjectId.Text, out int subjectId) || subjectId <= 0)
            {
                return;
            }

            try
            {
                var subject = await subjectService.GetByIdAsync(subjectId);

                if (subject is null)
                {
                    return;
                }

                PopulateSubjectEditor(SubjectRow.FromSubject(subject));
                SelectSubjectRow(subject.SubjectID);
            }
            catch (Exception ex)
            {
                ShowError("Unable to load subject details.", ex);
            }
        }

        private void PopulateSubjectEditor(SubjectRow row)
        {
            txtSubjectId.Text = row.SubjectID.ToString();
            txtSubjectName.Text = row.SubjectName;
            SelectComboValue(cmbStudyYear, row.StudyYearID);
            SelectComboValue(cmbBranch, row.BranchID);
            SelectComboText(cmbSemester, row.SemesterNumber.ToString());
            SelectComboText(cmbRequirementType, row.RequirementType);
            txtTheoreticalHours.Text = row.TheoreticalHours.ToString("0.##");
            txtPracticalHours.Text = row.PracticalHours.ToString("0.##");
            txtCreditUnits.Text = row.CreditUnits.ToString("0.##");
        }

        private void SelectSubjectRow(int subjectId)
        {
            foreach (DataGridViewRow row in dgvSubjects.Rows)
            {
                if (row.DataBoundItem is not SubjectRow subject || subject.SubjectID != subjectId)
                {
                    continue;
                }

                row.Selected = true;
                dgvSubjects.CurrentCell = row.Cells[0];
                break;
            }
        }

        private void ClearSubjectForm()
        {
            txtSubjectId.Clear();
            txtSubjectName.Clear();
            ClearCombo(cmbStudyYear);
            ClearCombo(cmbBranch);
            ClearCombo(cmbSemester);
            ClearCombo(cmbRequirementType);
            txtTheoreticalHours.Clear();
            txtPracticalHours.Clear();
            txtCreditUnits.Clear();
            dgvSubjects.ClearSelection();
            txtSubjectId.Focus();
        }

        private void SetSubjectActionsEnabled(bool enabled)
        {
            btnAddSubject.Enabled = enabled;
            btnUpdateSubject.Enabled = enabled;
            btnDeleteSubject.Enabled = enabled;
            dgvSubjects.Enabled = enabled;
        }

        private void BindBranchCombo()
        {
            var branches = new List<ComboOption> { new(null, "General / all branches") };
            branches.AddRange(branchesLookup.Select(branch => new ComboOption(branch.BranchID, branch.BranchName)));
            BindCombo(cmbBranch, branches);
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
                foreach (var item in combo.Items)
                {
                    if (item is ComboOption { Id: null })
                    {
                        combo.SelectedItem = item;
                        return;
                    }
                }

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

        private static void SelectComboText(Guna.UI2.WinForms.Guna2ComboBox combo, string text)
        {
            foreach (var item in combo.Items)
            {
                if (string.Equals(item?.ToString(), text, StringComparison.OrdinalIgnoreCase))
                {
                    combo.SelectedItem = item;
                    return;
                }
            }

            combo.Items.Add(text);
            combo.SelectedItem = text;
        }

        private static void ClearCombo(Guna.UI2.WinForms.Guna2ComboBox combo)
        {
            combo.SelectedIndex = -1;
            combo.Text = string.Empty;
        }

        private void ShowInformation(string message)
        {
            MessageBox.Show(this, message, "Subjects", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show(this, $"{message}\n\n{ex.Message}", "Subjects", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private sealed record ComboOption(int? Id, string Text);

        private sealed class SubjectRow
        {
            public int SubjectID { get; init; }
            public string SubjectName { get; init; } = string.Empty;
            public int StudyYearID { get; init; }
            public int? BranchID { get; init; }
            public string StudyYearName { get; init; } = string.Empty;
            public string BranchName { get; init; } = string.Empty;
            public int SemesterNumber { get; init; }
            public double TheoreticalHours { get; init; }
            public double PracticalHours { get; init; }
            public double CreditUnits { get; init; }
            public string RequirementType { get; init; } = string.Empty;

            public static SubjectRow FromSubject(Subject subject)
            {
                return new SubjectRow
                {
                    SubjectID = subject.SubjectID,
                    SubjectName = subject.SubjectName,
                    StudyYearID = subject.StudyYearID,
                    BranchID = subject.BranchID,
                    StudyYearName = subject.StudyYear?.YearName ?? "-",
                    BranchName = subject.Branch?.BranchName ?? "General",
                    SemesterNumber = subject.SemesterNumber,
                    TheoreticalHours = subject.TheoreticalHours,
                    PracticalHours = subject.PracticalHours,
                    CreditUnits = subject.CreditUnits,
                    RequirementType = subject.RequirementType
                };
            }
        }
    }
}
