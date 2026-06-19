using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SectionsForm : System.Windows.Forms.UserControl
    {
        private readonly SectionService sectionService = new();
        private readonly StudyYearService studyYearService = new();
        private readonly BranchService branchService = new();

        private List<StudyYear> studyYearsLookup = [];
        private List<Branch> branchesLookup = [];

        public SectionsForm()
        {
            InitializeComponent();
            ConfigureNavigation();
            ConfigureSectionsGrid();
            ConfigureSectionsEvents();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await RefreshSectionsAsync();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.Sections);
        }

        private void ConfigureSectionsGrid()
        {
            dgvSections.AutoGenerateColumns = false;
            dgvSections.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            colSectionId.DataPropertyName = nameof(SectionRow.SectionID);
            colSectionName.DataPropertyName = nameof(SectionRow.SectionName);
            colStudentCount.DataPropertyName = nameof(SectionRow.StudentCount);
            colStudyYear.DataPropertyName = nameof(SectionRow.StudyYearName);
            colBranch.DataPropertyName = nameof(SectionRow.BranchName);
        }

        private void ConfigureSectionsEvents()
        {
            dgvSections.SelectionChanged += (_, _) => PopulateSectionEditorFromSelection();
            txtSectionId.Leave += async (_, _) => await PopulateSectionEditorFromEnteredIdAsync();
            btnAddSection.Click += async (_, _) => await AddSectionAsync();
            btnUpdateSection.Click += async (_, _) => await UpdateSectionAsync();
            btnDeleteSection.Click += async (_, _) => await DeleteSectionAsync();
        }

        private async Task RefreshSectionsAsync()
        {
            SetSectionActionsEnabled(false);

            try
            {
                await LoadLookupsAsync();
                await LoadSectionsAsync();
                ClearSectionForm();
            }
            catch (Exception ex)
            {
                ShowError("Unable to refresh sections.", ex);
            }
            finally
            {
                SetSectionActionsEnabled(true);
            }
        }

        private async Task LoadLookupsAsync()
        {
            studyYearsLookup = await studyYearService.GetAllAsync();
            branchesLookup = await branchService.GetAllAsync();

            BindCombo(cmbStudyYear, studyYearsLookup.Select(studyYear => new ComboOption(studyYear.StudyYearID, studyYear.YearName)));
            BindBranchCombo();
        }

        private async Task LoadSectionsAsync()
        {
            var sections = await sectionService.GetAllAsync();
            dgvSections.DataSource = sections
                .Select(SectionRow.FromSection)
                .OrderBy(row => row.StudyYearID)
                .ThenBy(row => row.BranchID ?? 0)
                .ThenBy(row => row.SectionName)
                .ToList();
            dgvSections.ClearSelection();
        }

        private async Task AddSectionAsync()
        {
            if (!TryBuildSection(out var section))
            {
                return;
            }

            await ExecuteSectionActionAsync(
                async () => await sectionService.AddAsync(section),
                "Section added successfully.");
        }

        private async Task UpdateSectionAsync()
        {
            if (!TryBuildSection(out var section))
            {
                return;
            }

            await ExecuteSectionActionAsync(
                async () => await sectionService.UpdateAsync(section),
                "Section updated successfully.");
        }

        private async Task DeleteSectionAsync()
        {
            if (!TryGetSectionIdFromEditor(out int sectionId))
            {
                return;
            }

            var confirmation = MessageBox.Show(
                this,
                "Are you sure you want to delete the selected section?",
                "Delete Section",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            await ExecuteSectionActionAsync(
                async () => await sectionService.DeleteAsync(sectionId),
                "Section deleted successfully.");
        }

        private async Task ExecuteSectionActionAsync(Func<Task> action, string successMessage)
        {
            SetSectionActionsEnabled(false);

            try
            {
                await action();
                await LoadSectionsAsync();
                ClearSectionForm();
                ShowInformation(successMessage);
            }
            catch (Exception ex)
            {
                ShowError("Unable to complete the section operation.", ex);
            }
            finally
            {
                SetSectionActionsEnabled(true);
            }
        }

        private bool TryBuildSection(out Section section)
        {
            section = new Section
            {
                SectionName = txtSectionName.Text.Trim(),
                StudyYearID = GetSelectedRequiredId(cmbStudyYear),
                BranchID = GetSelectedOptionalId(cmbBranch)
            };

            if (!TryGetSectionIdFromEditor(out int sectionId))
            {
                return false;
            }

            section.SectionID = sectionId;

            if (string.IsNullOrWhiteSpace(section.SectionName))
            {
                ShowInformation("Section name is required.");
                txtSectionName.Focus();
                return false;
            }

            if (!int.TryParse(txtStudentCount.Text, out int studentCount) || studentCount < 0)
            {
                ShowInformation("Student count must be zero or greater.");
                txtStudentCount.Focus();
                return false;
            }

            if (section.StudyYearID <= 0)
            {
                ShowInformation("Study year is required.");
                cmbStudyYear.Focus();
                return false;
            }

            section.StudentCount = studentCount;
            return true;
        }

        private bool TryGetSectionIdFromEditor(out int sectionId)
        {
            if (int.TryParse(txtSectionId.Text, out sectionId) && sectionId > 0)
            {
                return true;
            }

            ShowInformation("Enter a valid section ID.");
            txtSectionId.Focus();
            return false;
        }

        private void PopulateSectionEditorFromSelection()
        {
            if (dgvSections.CurrentRow?.DataBoundItem is not SectionRow row)
            {
                return;
            }

            PopulateSectionEditor(row);
        }

        private async Task PopulateSectionEditorFromEnteredIdAsync()
        {
            if (!int.TryParse(txtSectionId.Text, out int sectionId) || sectionId <= 0)
            {
                return;
            }

            try
            {
                var section = await sectionService.GetByIdAsync(sectionId);

                if (section is null)
                {
                    return;
                }

                PopulateSectionEditor(SectionRow.FromSection(section));
                SelectSectionRow(section.SectionID);
            }
            catch (Exception ex)
            {
                ShowError("Unable to load section details.", ex);
            }
        }

        private void PopulateSectionEditor(SectionRow row)
        {
            txtSectionId.Text = row.SectionID.ToString();
            txtSectionName.Text = row.SectionName;
            txtStudentCount.Text = row.StudentCount.ToString();
            SelectComboValue(cmbStudyYear, row.StudyYearID);
            SelectComboValue(cmbBranch, row.BranchID);
        }

        private void SelectSectionRow(int sectionId)
        {
            foreach (DataGridViewRow row in dgvSections.Rows)
            {
                if (row.DataBoundItem is not SectionRow section || section.SectionID != sectionId)
                {
                    continue;
                }

                row.Selected = true;
                dgvSections.CurrentCell = row.Cells[0];
                break;
            }
        }

        private void ClearSectionForm()
        {
            txtSectionId.Clear();
            txtSectionName.Clear();
            txtStudentCount.Clear();
            ClearCombo(cmbStudyYear);
            ClearCombo(cmbBranch);
            dgvSections.ClearSelection();
            txtSectionId.Focus();
        }

        private void SetSectionActionsEnabled(bool enabled)
        {
            btnAddSection.Enabled = enabled;
            btnUpdateSection.Enabled = enabled;
            btnDeleteSection.Enabled = enabled;
            dgvSections.Enabled = enabled;
        }

        private void BindBranchCombo()
        {
            var branches = new List<ComboOption> { new(null, "General / no branch") };
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

        private static void ClearCombo(Guna.UI2.WinForms.Guna2ComboBox combo)
        {
            combo.SelectedIndex = -1;
            combo.Text = string.Empty;
        }

        private void ShowInformation(string message)
        {
            MessageBox.Show(this, message, "Sections", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show(this, $"{message}\n\n{ex.Message}", "Sections", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private sealed record ComboOption(int? Id, string Text);

        private sealed class SectionRow
        {
            public int SectionID { get; init; }
            public string SectionName { get; init; } = string.Empty;
            public int StudentCount { get; init; }
            public int StudyYearID { get; init; }
            public int? BranchID { get; init; }
            public string StudyYearName { get; init; } = string.Empty;
            public string BranchName { get; init; } = string.Empty;

            public static SectionRow FromSection(Section section)
            {
                return new SectionRow
                {
                    SectionID = section.SectionID,
                    SectionName = section.SectionName,
                    StudentCount = section.StudentCount,
                    StudyYearID = section.StudyYearID,
                    BranchID = section.BranchID,
                    StudyYearName = section.StudyYear?.YearName ?? "-",
                    BranchName = section.Branch?.BranchName ?? "General"
                };
            }
        }
    }
}
