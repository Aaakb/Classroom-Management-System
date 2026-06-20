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
            ConfigureAutoIdField();
            ConfigureNavigation();
            ConfigureStudyYearsGrid();
            ConfigureStudyYearsEvents();
        }

        private void ConfigureAutoIdField()
        {
            txtStudyYearId.ReadOnly = true;
            txtStudyYearId.TabStop = false;
            txtStudyYearId.PlaceholderText = "Auto";
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
            txtStudyYearId.Leave += async (_, _) => await PopulateStudyYearEditorFromEnteredIdAsync();
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
            if (!TryBuildStudyYear(out var studyYear, requireId: false))
            {
                return;
            }

            await ExecuteStudyYearActionAsync(
                async () => await studyYearService.AddAsync(studyYear),
                "Study year added successfully.");
        }

        private async Task UpdateStudyYearAsync()
        {
            if (!TryBuildStudyYear(out var studyYear))
            {
                return;
            }

            await ExecuteStudyYearActionAsync(
                async () => await studyYearService.UpdateAsync(studyYear),
                "Study year updated successfully.");
        }

        private async Task DeleteStudyYearAsync()
        {
            if (!TryGetStudyYearIdFromEditor(out int studyYearId))
            {
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

        private bool TryBuildStudyYear(out StudyYear studyYear, bool requireId = true)
        {
            studyYear = new StudyYear
            {
                YearName = txtYearName.Text.Trim()
            };

            var studyYearId = 0;
            if (requireId && !TryGetStudyYearIdFromEditor(out studyYearId))
            {
                return false;
            }

            studyYear.StudyYearID = requireId ? studyYearId : 0;

            if (!string.IsNullOrWhiteSpace(studyYear.YearName))
            {
                return true;
            }

            ShowInformation("Study year name is required.");
            txtYearName.Focus();
            return false;
        }

        private bool TryGetStudyYearIdFromEditor(out int studyYearId)
        {
            if (int.TryParse(txtStudyYearId.Text, out studyYearId) && studyYearId > 0)
            {
                return true;
            }

            ShowInformation("Select a study year row first.");
            return false;
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

        private async Task PopulateStudyYearEditorFromEnteredIdAsync()
        {
            if (!int.TryParse(txtStudyYearId.Text, out int studyYearId) || studyYearId <= 0)
            {
                return;
            }

            try
            {
                var studyYear = await studyYearService.GetByIdAsync(studyYearId);

                if (studyYear is null)
                {
                    return;
                }

                txtYearName.Text = studyYear.YearName;
                SelectStudyYearRow(studyYear.StudyYearID);
            }
            catch (Exception ex)
            {
                ShowError("Unable to load study year details.", ex);
            }
        }

        private void SelectStudyYearRow(int studyYearId)
        {
            foreach (DataGridViewRow row in dgvStudyYears.Rows)
            {
                if (row.DataBoundItem is not StudyYear studyYear || studyYear.StudyYearID != studyYearId)
                {
                    continue;
                }

                row.Selected = true;
                dgvStudyYears.CurrentCell = row.Cells[0];
                break;
            }
        }

        private void ClearStudyYearForm()
        {
            txtStudyYearId.Text = "Auto";
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
