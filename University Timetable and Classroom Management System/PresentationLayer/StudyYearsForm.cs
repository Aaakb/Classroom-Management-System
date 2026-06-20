using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class StudyYearsForm
    {
        private readonly StudyYearService studyYearService = new();
        private List<StudyYear> studyYears = [];

        public StudyYearsForm()
        {
            InitializeComponent();
            ConfigureNavigation();
            ConfigureStudyYearGrid();
            ConfigureStudyYearEvents();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadStudyYearsAsync();
        }

        private void ConfigureNavigation()
        {
            btnNavigationStudyYears.Enabled = false;
            btnNavigationBranches.Click += (_, _) => FormNavigation.Open(this, new BranchesForm());
            btnNavigationFaculty.Click += (_, _) => FormNavigation.Open(this, new FacultyMembersForm());
        }

        private void ConfigureStudyYearGrid()
        {
            dgvStudyYears.AutoGenerateColumns = false;
            dgvStudyYears.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            colStudyYearId.DataPropertyName = nameof(StudyYear.StudyYearID);
            colYearName.DataPropertyName = nameof(StudyYear.YearName);
        }

        private void ConfigureStudyYearEvents()
        {
            dgvStudyYears.SelectionChanged += (_, _) => PopulateEditorFromSelection();
            btnAddStudyYear.Click += async (_, _) => await AddStudyYearAsync();
            btnUpdateStudyYear.Click += async (_, _) => await UpdateStudyYearAsync();
            btnDeleteStudyYear.Click += async (_, _) => await DeleteStudyYearAsync();
            btnClearStudyYearForm.Click += (_, _) => ClearStudyYearForm();
        }

        private async Task LoadStudyYearsAsync()
        {
            SetStudyYearActionsEnabled(false);

            try
            {
                studyYears = await studyYearService.GetAllAsync();
                dgvStudyYears.DataSource = studyYears.ToList();
                dgvStudyYears.ClearSelection();
                ClearStudyYearForm();
            }
            catch (Exception ex)
            {
                ShowError("Unable to load study years.", ex);
            }
            finally
            {
                SetStudyYearActionsEnabled(true);
            }
        }

        private async Task AddStudyYearAsync()
        {
            if (!TryBuildStudyYear(out var studyYear))
            {
                return;
            }

            await RunStudyYearActionAsync(
                () => studyYearService.AddAsync(studyYear),
                "Study year added successfully.");
        }

        private async Task UpdateStudyYearAsync()
        {
            if (!TryGetSelectedStudyYearId(out int studyYearId) || !TryBuildStudyYear(out var studyYear))
            {
                ShowInformation("Select a study year to update.");
                return;
            }

            studyYear.StudyYearID = studyYearId;

            await RunStudyYearActionAsync(
                () => studyYearService.UpdateAsync(studyYear),
                "Study year updated successfully.");
        }

        private async Task DeleteStudyYearAsync()
        {
            if (!TryGetSelectedStudyYearId(out int studyYearId))
            {
                ShowInformation("Select a study year to delete.");
                return;
            }

            var confirmation = MessageBox.Show(
                this,
                "Delete the selected study year?",
                "Study Years",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            await RunStudyYearActionAsync(
                async () =>
                {
                    await studyYearService.DeleteAsync(studyYearId);
                    return new StudyYear();
                },
                "Study year deleted successfully.");
        }

        private async Task RunStudyYearActionAsync(Func<Task<StudyYear>> action, string successMessage)
        {
            SetStudyYearActionsEnabled(false);

            try
            {
                await action();
                await LoadStudyYearsAsync();
                ShowInformation(successMessage);
            }
            catch (Exception ex)
            {
                ShowError("Unable to save study year changes.", ex);
            }
            finally
            {
                SetStudyYearActionsEnabled(true);
            }
        }

        private bool TryBuildStudyYear(out StudyYear studyYear)
        {
            studyYear = new StudyYear
            {
                YearName = txtYearName.Text.Trim()
            };

            if (string.IsNullOrWhiteSpace(studyYear.YearName))
            {
                ShowInformation("Enter the study year name.");
                txtYearName.Focus();
                return false;
            }

            if (StudyYearRules.ResolveLevel(studyYear.YearName) == StudyYearLevel.Unknown)
            {
                ShowInformation("Use one of these values: First Year, Second Year, Third Year, Fourth Year.");
                txtYearName.Focus();
                return false;
            }

            return true;
        }

        private bool TryGetSelectedStudyYearId(out int studyYearId)
        {
            if (int.TryParse(txtStudyYearId.Text, out studyYearId) && studyYearId > 0)
            {
                return true;
            }

            var selectedStudyYear = dgvStudyYears.CurrentRow?.DataBoundItem as StudyYear;
            studyYearId = selectedStudyYear?.StudyYearID ?? 0;
            return studyYearId > 0;
        }

        private void PopulateEditorFromSelection()
        {
            if (dgvStudyYears.CurrentRow?.DataBoundItem is not StudyYear studyYear)
            {
                return;
            }

            txtStudyYearId.Text = studyYear.StudyYearID.ToString();
            txtYearName.Text = studyYear.YearName;
        }

        private void ClearStudyYearForm()
        {
            txtStudyYearId.Clear();
            txtYearName.Clear();
            dgvStudyYears.ClearSelection();
            txtYearName.Focus();
        }

        private void SetStudyYearActionsEnabled(bool enabled)
        {
            btnAddStudyYear.Enabled = enabled;
            btnUpdateStudyYear.Enabled = enabled;
            btnDeleteStudyYear.Enabled = enabled;
            btnClearStudyYearForm.Enabled = enabled;
            dgvStudyYears.Enabled = enabled;
        }

        private void ShowInformation(string message)
        {
            MessageBox.Show(this, message, "Study Years", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show(this, $"{message}{Environment.NewLine}{ex.Message}", "Study Years", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
