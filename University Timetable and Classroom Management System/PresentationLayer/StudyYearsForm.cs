using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class StudyYearsForm
    {
        private readonly StudyYearService studyYearService = new();

        public StudyYearsForm()
        {
            InitializeComponent();
            ConfigureNavigation();
            ConfigureStudyYearsGrid();
            ConfigureStudyYearsEvents();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadStudyYearsAsync();
            ClearStudyYearForm();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.StudyYears);
        }

        private void ConfigureStudyYearsGrid()
        {
            dgvStudyYears.AutoGenerateColumns = false;
            dgvStudyYears.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void ConfigureStudyYearsEvents()
        {
            dgvStudyYears.SelectionChanged += (_, _) => PopulateStudyYearEditorFromSelection();
            btnAddStudyYear.Click += async (_, _) => await AddStudyYearAsync();
            btnUpdateStudyYear.Click += async (_, _) => await UpdateStudyYearAsync();
            btnDeleteStudyYear.Click += async (_, _) => await DeleteStudyYearAsync();
        }

        private async Task LoadStudyYearsAsync()
        {
            SetStudyYearActionsEnabled(false);

            try
            {
                var studyYears = await studyYearService.GetAllAsync();
                dgvStudyYears.DataSource = studyYears;
                dgvStudyYears.ClearSelection();
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

            await ExecuteStudyYearActionAsync(
                async () => await studyYearService.AddAsync(studyYear),
                "Study year added successfully.");
        }

        private async Task UpdateStudyYearAsync()
        {
            if (!TryGetSelectedStudyYearId(out int studyYearId) || !TryBuildStudyYear(out var studyYear))
            {
                ShowInformation("Select a study year before updating.");
                return;
            }

            studyYear.StudyYearID = studyYearId;

            await ExecuteStudyYearActionAsync(
                async () => await studyYearService.UpdateAsync(studyYear),
                "Study year updated successfully.");
        }

        private async Task DeleteStudyYearAsync()
        {
            if (!TryGetSelectedStudyYearId(out int studyYearId))
            {
                ShowInformation("Select a study year before deleting.");
                return;
            }

            var confirmation = MessageBox.Show(
                this,
                "Are you sure you want to delete the selected study year?",
                "Delete Study Year",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            await ExecuteStudyYearActionAsync(
                async () => await studyYearService.DeleteAsync(studyYearId),
                "Study year deleted successfully.");
        }

        private async Task ExecuteStudyYearActionAsync(Func<Task> action, string successMessage)
        {
            SetStudyYearActionsEnabled(false);

            try
            {
                await action();
                await LoadStudyYearsAsync();
                ClearStudyYearForm();
                ShowInformation(successMessage);
            }
            catch (Exception ex)
            {
                ShowError("Unable to complete the study year operation.", ex);
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

            if (!string.IsNullOrWhiteSpace(studyYear.YearName))
            {
                return true;
            }

            ShowInformation("Study year name is required.");
            txtYearName.Focus();
            return false;
        }

        private bool TryGetSelectedStudyYearId(out int studyYearId)
        {
            if (int.TryParse(txtStudyYearId.Text, out studyYearId))
            {
                return true;
            }

            var selectedStudyYear = dgvStudyYears.CurrentRow?.DataBoundItem as StudyYear;
            studyYearId = selectedStudyYear?.StudyYearID ?? 0;
            return studyYearId > 0;
        }

        private void PopulateStudyYearEditorFromSelection()
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
            dgvStudyYears.Enabled = enabled;
        }

        private void ShowInformation(string message)
        {
            MessageBox.Show(this, message, "Study Years", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show(this, $"{message}\n\n{ex.Message}", "Study Years", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
