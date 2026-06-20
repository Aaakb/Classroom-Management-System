using System.Globalization;
using Guna.UI2.WinForms;
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
        private List<SubjectRow> subjectRows = [];
        private bool isUpdatingSubjectFilters;
        private Guna2HtmlLabel lblStudyYearFilter = null!;
        private Guna2HtmlLabel lblBranchFilter = null!;
        private Guna2ComboBox cmbStudyYearFilter = null!;
        private Guna2ComboBox cmbBranchFilter = null!;

        public SubjectsForm()
        {
            InitializeComponent();
            ConfigureAutoIdField();
            ConfigureNavigation();
            ConfigureSubjectsGrid();
            ConfigureSubjectFilters();
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

        private void ConfigureAutoIdField()
        {
            txtSubjectId.ReadOnly = true;
            txtSubjectId.TabStop = false;
            txtSubjectId.PlaceholderText = "Auto";
        }

        private void ConfigureSubjectsEvents()
        {
            dgvSubjects.SelectionChanged += (_, _) => PopulateSubjectEditorFromSelection();
            txtSubjectId.Leave += async (_, _) => await PopulateSubjectEditorFromEnteredIdAsync();
            btnAddSubject.Click += async (_, _) => await AddSubjectAsync();
            btnUpdateSubject.Click += async (_, _) => await UpdateSubjectAsync();
            btnDeleteSubject.Click += async (_, _) => await DeleteSubjectAsync();
            cmbStudyYear.SelectedIndexChanged += (_, _) => ApplyStudyYearRulesToEditor();
            cmbStudyYearFilter.SelectedIndexChanged += (_, _) => ApplyStudyYearFilterSelection();
            cmbBranchFilter.SelectedIndexChanged += (_, _) => ApplySubjectFilters();
        }

        private void ConfigureSubjectFilters()
        {
            lblStudyYearFilter = CreateFilterLabel("Study year filter", new Point(470, 18));
            cmbStudyYearFilter = CreateFilterCombo(new Point(470, 39), 180);
            lblBranchFilter = CreateFilterLabel("Branch filter", new Point(666, 18));
            cmbBranchFilter = CreateFilterCombo(new Point(666, 39), 194);

            pnlSubjectsTable.Controls.Add(lblStudyYearFilter);
            pnlSubjectsTable.Controls.Add(cmbStudyYearFilter);
            pnlSubjectsTable.Controls.Add(lblBranchFilter);
            pnlSubjectsTable.Controls.Add(cmbBranchFilter);
        }

        private static Guna2HtmlLabel CreateFilterLabel(string text, Point location)
        {
            return new Guna2HtmlLabel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                Location = location,
                Text = text
            };
        }

        private static Guna2ComboBox CreateFilterCombo(Point location, int width)
        {
            return new Guna2ComboBox
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.Transparent,
                BorderColor = Color.FromArgb(203, 213, 225),
                BorderRadius = 6,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FocusedColor = Color.FromArgb(37, 99, 235),
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(15, 23, 42),
                HoverState = { BorderColor = Color.FromArgb(148, 163, 184) },
                ItemHeight = 28,
                Location = location,
                Size = new Size(width, 34)
            };
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
            BindSubjectFilterLookups();
        }

        private async Task LoadSubjectsAsync()
        {
            var subjects = await subjectService.GetAllAsync();
            subjectRows = subjects
                .Select(SubjectRow.FromSubject)
                .OrderBy(row => row.StudyYearID)
                .ThenBy(row => row.BranchID ?? 0)
                .ThenBy(row => row.SemesterNumber)
                .ThenBy(row => row.SubjectName)
                .ToList();

            ApplySubjectFilters();
        }

        private void ApplyStudyYearFilterSelection()
        {
            if (isUpdatingSubjectFilters)
            {
                return;
            }

            var selectedStudyYearId = GetSelectedOptionalId(cmbStudyYearFilter);
            var selectedBranchId = GetSelectedOptionalId(cmbBranchFilter);

            isUpdatingSubjectFilters = true;
            BindBranchFilterCombo(selectedStudyYearId, selectedBranchId);
            isUpdatingSubjectFilters = false;

            ApplySubjectFilters();
        }

        private void ApplySubjectFilters()
        {
            if (isUpdatingSubjectFilters)
            {
                return;
            }

            var selectedStudyYearId = GetSelectedOptionalId(cmbStudyYearFilter);
            var selectedBranchId = GetSelectedOptionalId(cmbBranchFilter);

            dgvSubjects.DataSource = subjectRows
                .Where(row => !selectedStudyYearId.HasValue || row.StudyYearID == selectedStudyYearId.Value)
                .Where(row => !selectedBranchId.HasValue || row.BranchID == selectedBranchId.Value)
                .ToList();
            dgvSubjects.ClearSelection();
        }

        private async Task AddSubjectAsync()
        {
            if (!TryBuildSubject(out var subject, requireId: false))
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

        private bool TryBuildSubject(out Subject subject, bool requireId = true)
        {
            subject = new Subject
            {
                SubjectName = txtSubjectName.Text.Trim(),
                StudyYearID = GetSelectedRequiredId(cmbStudyYear),
                BranchID = GetSelectedOptionalId(cmbBranch),
                RequirementType = cmbRequirementType.Text.Trim()
            };

            var subjectId = 0;
            if (requireId && !TryGetSubjectIdFromEditor(out subjectId))
            {
                return false;
            }

            subject.SubjectID = requireId ? subjectId : 0;

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

            if (AcademicStructureRules.UsesGeneralSections(subject.StudyYearID))
            {
                subject.BranchID = null;
            }

            if (AcademicStructureRules.UsesBranches(subject.StudyYearID) && !subject.BranchID.HasValue)
            {
                ShowInformation("Branch is required for third and fourth year subjects.");
                cmbBranch.Focus();
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

            ShowInformation("Select a subject row first.");
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
            ApplyStudyYearRulesToEditor();
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
            txtSubjectId.Text = "Auto";
            txtSubjectName.Clear();
            ClearCombo(cmbStudyYear);
            ClearCombo(cmbBranch);
            cmbBranch.Enabled = true;
            ClearCombo(cmbSemester);
            ClearCombo(cmbRequirementType);
            txtTheoreticalHours.Clear();
            txtPracticalHours.Clear();
            txtCreditUnits.Clear();
            dgvSubjects.ClearSelection();
            txtSubjectName.Focus();
        }

        private void ApplyStudyYearRulesToEditor()
        {
            var studyYearId = GetSelectedRequiredId(cmbStudyYear);

            if (studyYearId <= 0)
            {
                cmbBranch.Enabled = true;
                return;
            }

            if (AcademicStructureRules.UsesGeneralSections(studyYearId))
            {
                SelectComboValue(cmbBranch, null);
                cmbBranch.Enabled = false;
                return;
            }

            cmbBranch.Enabled = true;
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

        private void BindSubjectFilterLookups()
        {
            isUpdatingSubjectFilters = true;

            var studyYears = new List<ComboOption> { new(null, "All study years") };
            studyYears.AddRange(studyYearsLookup.Select(studyYear => new ComboOption(studyYear.StudyYearID, studyYear.YearName)));
            BindCombo(cmbStudyYearFilter, studyYears);
            cmbStudyYearFilter.SelectedIndex = 0;

            BindBranchFilterCombo(null, null);

            isUpdatingSubjectFilters = false;
        }

        private void BindBranchFilterCombo(int? studyYearId, int? selectedBranchId)
        {
            var availableBranchIds = subjectRows
                .Where(row => !studyYearId.HasValue || row.StudyYearID == studyYearId.Value)
                .Select(row => row.BranchID)
                .ToHashSet();

            var branches = new List<ComboOption> { new(null, "All branches") };
            branches.AddRange(branchesLookup
                .Where(branch => availableBranchIds.Count == 0 || availableBranchIds.Contains(branch.BranchID))
                .Select(branch => new ComboOption(branch.BranchID, branch.BranchName)));

            BindCombo(cmbBranchFilter, branches);

            if (selectedBranchId.HasValue && branches.Any(branch => branch.Id == selectedBranchId.Value))
            {
                SelectComboValue(cmbBranchFilter, selectedBranchId);
            }
            else
            {
                cmbBranchFilter.SelectedIndex = 0;
            }
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
    
        // Code-only UI initialization (merged from former Designer.cs).

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges51 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges52 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges47 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges48 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges45 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges46 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges29 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges30 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges31 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges32 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges33 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges34 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges35 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges36 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges37 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges38 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges39 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges40 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges41 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges42 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges43 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges44 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges49 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges50 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlSidebar = new Guna.UI2.WinForms.Guna2Panel();
            lblSidebarFooter = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnNavigationSchedules = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationFaculty = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationClassrooms = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationSubjects = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationStudyYears = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationBranches = new Guna.UI2.WinForms.Guna2Button();
            btnNavigationDashboard = new Guna.UI2.WinForms.Guna2Button();
            separatorSidebar = new Guna.UI2.WinForms.Guna2Separator();
            lblSidebarSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblApplicationName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            pnlWorkspace = new Guna.UI2.WinForms.Guna2Panel();
            pnlSubjectsTable = new Guna.UI2.WinForms.Guna2Panel();
            dgvSubjects = new Guna.UI2.WinForms.Guna2DataGridView();
            colSubjectId = new DataGridViewTextBoxColumn();
            colSubjectName = new DataGridViewTextBoxColumn();
            colStudyYear = new DataGridViewTextBoxColumn();
            colBranch = new DataGridViewTextBoxColumn();
            colSemester = new DataGridViewTextBoxColumn();
            colTheoreticalHours = new DataGridViewTextBoxColumn();
            colPracticalHours = new DataGridViewTextBoxColumn();
            colCreditUnits = new DataGridViewTextBoxColumn();
            colRequirementType = new DataGridViewTextBoxColumn();
            lblTableSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTableTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlSubjectEditor = new Guna.UI2.WinForms.Guna2Panel();
            btnClearSubjectForm = new Guna.UI2.WinForms.Guna2Button();
            btnDeleteSubject = new Guna.UI2.WinForms.Guna2Button();
            btnUpdateSubject = new Guna.UI2.WinForms.Guna2Button();
            btnAddSubject = new Guna.UI2.WinForms.Guna2Button();
            txtCreditUnits = new Guna.UI2.WinForms.Guna2TextBox();
            lblCreditUnits = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtPracticalHours = new Guna.UI2.WinForms.Guna2TextBox();
            lblPracticalHours = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtTheoreticalHours = new Guna.UI2.WinForms.Guna2TextBox();
            lblTheoreticalHours = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbRequirementType = new Guna.UI2.WinForms.Guna2ComboBox();
            lblRequirementType = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbSemester = new Guna.UI2.WinForms.Guna2ComboBox();
            lblSemester = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbBranch = new Guna.UI2.WinForms.Guna2ComboBox();
            lblBranch = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbStudyYear = new Guna.UI2.WinForms.Guna2ComboBox();
            lblStudyYear = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtSubjectName = new Guna.UI2.WinForms.Guna2TextBox();
            lblSubjectName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtSubjectId = new Guna.UI2.WinForms.Guna2TextBox();
            lblSubjectId = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEditorSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEditorTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            lblPageSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblPageTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlSidebar.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlWorkspace.SuspendLayout();
            pnlSubjectsTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSubjects).BeginInit();
            pnlSubjectEditor.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.Transparent;
            pnlSidebar.Controls.Add(lblSidebarFooter);
            pnlSidebar.Controls.Add(btnNavigationSchedules);
            pnlSidebar.Controls.Add(btnNavigationFaculty);
            pnlSidebar.Controls.Add(btnNavigationClassrooms);
            pnlSidebar.Controls.Add(btnNavigationSubjects);
            pnlSidebar.Controls.Add(btnNavigationStudyYears);
            pnlSidebar.Controls.Add(btnNavigationBranches);
            pnlSidebar.Controls.Add(btnNavigationDashboard);
            pnlSidebar.Controls.Add(separatorSidebar);
            pnlSidebar.Controls.Add(lblSidebarSubtitle);
            pnlSidebar.Controls.Add(lblApplicationName);
            pnlSidebar.CustomizableEdges = customizableEdges15;
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.FillColor = Color.FromArgb(24, 38, 62);
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.ShadowDecoration.CustomizableEdges = customizableEdges16;
            pnlSidebar.Size = new Size(240, 720);
            pnlSidebar.TabIndex = 0;
            // 
            // lblSidebarFooter
            // 
            lblSidebarFooter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblSidebarFooter.BackColor = Color.Transparent;
            lblSidebarFooter.Font = new Font("Segoe UI", 9F);
            lblSidebarFooter.ForeColor = Color.FromArgb(148, 163, 184);
            lblSidebarFooter.Location = new Point(24, 671);
            lblSidebarFooter.Name = "lblSidebarFooter";
            lblSidebarFooter.Size = new Size(147, 17);
            lblSidebarFooter.TabIndex = 10;
            lblSidebarFooter.Text = "Academic Scheduling Suite";
            // 
            // btnNavigationSchedules
            // 
            btnNavigationSchedules.BorderRadius = 8;
            btnNavigationSchedules.Cursor = Cursors.Hand;
            btnNavigationSchedules.CustomizableEdges = customizableEdges1;
            btnNavigationSchedules.FillColor = Color.FromArgb(24, 38, 62);
            btnNavigationSchedules.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnNavigationSchedules.ForeColor = Color.FromArgb(226, 232, 240);
            btnNavigationSchedules.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnNavigationSchedules.Location = new Point(24, 434);
            btnNavigationSchedules.Name = "btnNavigationSchedules";
            btnNavigationSchedules.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnNavigationSchedules.Size = new Size(192, 44);
            btnNavigationSchedules.TabIndex = 9;
            btnNavigationSchedules.Text = "Schedules";
            btnNavigationSchedules.TextAlign = HorizontalAlignment.Left;
            btnNavigationSchedules.TextOffset = new Point(14, 0);
            // 
            // btnNavigationFaculty
            // 
            btnNavigationFaculty.BorderRadius = 8;
            btnNavigationFaculty.Cursor = Cursors.Hand;
            btnNavigationFaculty.CustomizableEdges = customizableEdges3;
            btnNavigationFaculty.FillColor = Color.FromArgb(24, 38, 62);
            btnNavigationFaculty.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnNavigationFaculty.ForeColor = Color.FromArgb(226, 232, 240);
            btnNavigationFaculty.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnNavigationFaculty.Location = new Point(24, 378);
            btnNavigationFaculty.Name = "btnNavigationFaculty";
            btnNavigationFaculty.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnNavigationFaculty.Size = new Size(192, 44);
            btnNavigationFaculty.TabIndex = 8;
            btnNavigationFaculty.Text = "Faculty Members";
            btnNavigationFaculty.TextAlign = HorizontalAlignment.Left;
            btnNavigationFaculty.TextOffset = new Point(14, 0);
            // 
            // btnNavigationClassrooms
            // 
            btnNavigationClassrooms.BorderRadius = 8;
            btnNavigationClassrooms.Cursor = Cursors.Hand;
            btnNavigationClassrooms.CustomizableEdges = customizableEdges5;
            btnNavigationClassrooms.FillColor = Color.FromArgb(24, 38, 62);
            btnNavigationClassrooms.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnNavigationClassrooms.ForeColor = Color.FromArgb(226, 232, 240);
            btnNavigationClassrooms.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnNavigationClassrooms.Location = new Point(24, 322);
            btnNavigationClassrooms.Name = "btnNavigationClassrooms";
            btnNavigationClassrooms.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnNavigationClassrooms.Size = new Size(192, 44);
            btnNavigationClassrooms.TabIndex = 7;
            btnNavigationClassrooms.Text = "Classrooms";
            btnNavigationClassrooms.TextAlign = HorizontalAlignment.Left;
            btnNavigationClassrooms.TextOffset = new Point(14, 0);
            // 
            // btnNavigationSubjects
            // 
            btnNavigationSubjects.BorderRadius = 8;
            btnNavigationSubjects.Checked = true;
            btnNavigationSubjects.Cursor = Cursors.Hand;
            btnNavigationSubjects.CustomizableEdges = customizableEdges7;
            btnNavigationSubjects.Enabled = false;
            btnNavigationSubjects.FillColor = Color.FromArgb(37, 99, 235);
            btnNavigationSubjects.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnNavigationSubjects.ForeColor = Color.White;
            btnNavigationSubjects.HoverState.FillColor = Color.FromArgb(29, 78, 216);
            btnNavigationSubjects.Location = new Point(24, 266);
            btnNavigationSubjects.Name = "btnNavigationSubjects";
            btnNavigationSubjects.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnNavigationSubjects.Size = new Size(192, 44);
            btnNavigationSubjects.TabIndex = 6;
            btnNavigationSubjects.Text = "Subjects";
            btnNavigationSubjects.TextAlign = HorizontalAlignment.Left;
            btnNavigationSubjects.TextOffset = new Point(14, 0);
            // 
            // btnNavigationStudyYears
            // 
            btnNavigationStudyYears.BorderRadius = 8;
            btnNavigationStudyYears.Cursor = Cursors.Hand;
            btnNavigationStudyYears.CustomizableEdges = customizableEdges9;
            btnNavigationStudyYears.FillColor = Color.FromArgb(24, 38, 62);
            btnNavigationStudyYears.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnNavigationStudyYears.ForeColor = Color.FromArgb(226, 232, 240);
            btnNavigationStudyYears.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnNavigationStudyYears.Location = new Point(24, 210);
            btnNavigationStudyYears.Name = "btnNavigationStudyYears";
            btnNavigationStudyYears.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnNavigationStudyYears.Size = new Size(192, 44);
            btnNavigationStudyYears.TabIndex = 5;
            btnNavigationStudyYears.Text = "Study Years";
            btnNavigationStudyYears.TextAlign = HorizontalAlignment.Left;
            btnNavigationStudyYears.TextOffset = new Point(14, 0);
            // 
            // btnNavigationBranches
            // 
            btnNavigationBranches.BorderRadius = 8;
            btnNavigationBranches.Cursor = Cursors.Hand;
            btnNavigationBranches.CustomizableEdges = customizableEdges11;
            btnNavigationBranches.FillColor = Color.FromArgb(24, 38, 62);
            btnNavigationBranches.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnNavigationBranches.ForeColor = Color.FromArgb(226, 232, 240);
            btnNavigationBranches.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnNavigationBranches.Location = new Point(24, 154);
            btnNavigationBranches.Name = "btnNavigationBranches";
            btnNavigationBranches.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnNavigationBranches.Size = new Size(192, 44);
            btnNavigationBranches.TabIndex = 4;
            btnNavigationBranches.Text = "Branches";
            btnNavigationBranches.TextAlign = HorizontalAlignment.Left;
            btnNavigationBranches.TextOffset = new Point(14, 0);
            // 
            // btnNavigationDashboard
            // 
            btnNavigationDashboard.BorderRadius = 8;
            btnNavigationDashboard.Cursor = Cursors.Hand;
            btnNavigationDashboard.CustomizableEdges = customizableEdges13;
            btnNavigationDashboard.FillColor = Color.FromArgb(24, 38, 62);
            btnNavigationDashboard.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnNavigationDashboard.ForeColor = Color.FromArgb(226, 232, 240);
            btnNavigationDashboard.HoverState.FillColor = Color.FromArgb(36, 55, 86);
            btnNavigationDashboard.Location = new Point(24, 98);
            btnNavigationDashboard.Name = "btnNavigationDashboard";
            btnNavigationDashboard.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnNavigationDashboard.Size = new Size(192, 44);
            btnNavigationDashboard.TabIndex = 3;
            btnNavigationDashboard.Text = "Dashboard";
            btnNavigationDashboard.TextAlign = HorizontalAlignment.Left;
            btnNavigationDashboard.TextOffset = new Point(14, 0);
            // 
            // separatorSidebar
            // 
            separatorSidebar.FillColor = Color.FromArgb(51, 65, 85);
            separatorSidebar.Location = new Point(24, 78);
            separatorSidebar.Name = "separatorSidebar";
            separatorSidebar.Size = new Size(192, 10);
            separatorSidebar.TabIndex = 2;
            // 
            // lblSidebarSubtitle
            // 
            lblSidebarSubtitle.BackColor = Color.Transparent;
            lblSidebarSubtitle.Font = new Font("Segoe UI", 9F);
            lblSidebarSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSidebarSubtitle.Location = new Point(26, 52);
            lblSidebarSubtitle.Name = "lblSidebarSubtitle";
            lblSidebarSubtitle.Size = new Size(133, 17);
            lblSidebarSubtitle.TabIndex = 1;
            lblSidebarSubtitle.Text = "Classroom Management";
            // 
            // lblApplicationName
            // 
            lblApplicationName.BackColor = Color.Transparent;
            lblApplicationName.Font = new Font("Segoe UI Semibold", 17F, FontStyle.Bold);
            lblApplicationName.ForeColor = Color.White;
            lblApplicationName.Location = new Point(24, 20);
            lblApplicationName.Name = "lblApplicationName";
            lblApplicationName.Size = new Size(216, 33);
            lblApplicationName.TabIndex = 0;
            lblApplicationName.Text = "University Timetable";
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(pnlWorkspace);
            pnlMain.Controls.Add(pnlHeader);
            pnlMain.CustomizableEdges = customizableEdges51;
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.FillColor = Color.FromArgb(245, 247, 250);
            pnlMain.Location = new Point(240, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.ShadowDecoration.CustomizableEdges = customizableEdges52;
            pnlMain.Size = new Size(940, 720);
            pnlMain.TabIndex = 1;
            // 
            // pnlWorkspace
            // 
            pnlWorkspace.Controls.Add(pnlSubjectsTable);
            pnlWorkspace.Controls.Add(pnlSubjectEditor);
            pnlWorkspace.CustomizableEdges = customizableEdges47;
            pnlWorkspace.Dock = DockStyle.Fill;
            pnlWorkspace.FillColor = Color.FromArgb(245, 247, 250);
            pnlWorkspace.Location = new Point(0, 88);
            pnlWorkspace.Name = "pnlWorkspace";
            pnlWorkspace.Padding = new Padding(28, 24, 28, 28);
            pnlWorkspace.ShadowDecoration.CustomizableEdges = customizableEdges48;
            pnlWorkspace.Size = new Size(940, 632);
            pnlWorkspace.TabIndex = 1;
            // 
            // pnlSubjectsTable
            // 
            pnlSubjectsTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlSubjectsTable.BackColor = Color.Transparent;
            pnlSubjectsTable.BorderColor = Color.FromArgb(226, 232, 240);
            pnlSubjectsTable.BorderRadius = 8;
            pnlSubjectsTable.BorderThickness = 1;
            pnlSubjectsTable.Controls.Add(dgvSubjects);
            pnlSubjectsTable.Controls.Add(lblTableSubtitle);
            pnlSubjectsTable.Controls.Add(lblTableTitle);
            pnlSubjectsTable.CustomizableEdges = customizableEdges17;
            pnlSubjectsTable.FillColor = Color.White;
            pnlSubjectsTable.Location = new Point(28, 336);
            pnlSubjectsTable.Name = "pnlSubjectsTable";
            pnlSubjectsTable.ShadowDecoration.CustomizableEdges = customizableEdges18;
            pnlSubjectsTable.Size = new Size(884, 268);
            pnlSubjectsTable.TabIndex = 1;
            // 
            // dgvSubjects
            // 
            dgvSubjects.AllowUserToAddRows = false;
            dgvSubjects.AllowUserToDeleteRows = false;
            dgvSubjects.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(30, 64, 175);
            dgvSubjects.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvSubjects.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvSubjects.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvSubjects.ColumnHeadersHeight = 44;
            dgvSubjects.Columns.AddRange(new DataGridViewColumn[] { colSubjectId, colSubjectName, colStudyYear, colBranch, colSemester, colTheoreticalHours, colPracticalHours, colCreditUnits, colRequirementType });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(30, 64, 175);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvSubjects.DefaultCellStyle = dataGridViewCellStyle3;
            dgvSubjects.GridColor = Color.FromArgb(226, 232, 240);
            dgvSubjects.Location = new Point(24, 78);
            dgvSubjects.MultiSelect = false;
            dgvSubjects.Name = "dgvSubjects";
            dgvSubjects.ReadOnly = true;
            dgvSubjects.RowHeadersVisible = false;
            dgvSubjects.RowTemplate.Height = 42;
            dgvSubjects.Size = new Size(836, 166);
            dgvSubjects.TabIndex = 2;
            dgvSubjects.ThemeStyle.AlternatingRowsStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvSubjects.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.FromArgb(30, 41, 59);
            dgvSubjects.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvSubjects.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.FromArgb(30, 64, 175);
            dgvSubjects.ThemeStyle.GridColor = Color.FromArgb(226, 232, 240);
            dgvSubjects.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(15, 23, 42);
            dgvSubjects.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dgvSubjects.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvSubjects.ThemeStyle.HeaderStyle.Height = 44;
            dgvSubjects.ThemeStyle.ReadOnly = true;
            dgvSubjects.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 10F);
            dgvSubjects.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(30, 41, 59);
            dgvSubjects.ThemeStyle.RowsStyle.Height = 42;
            dgvSubjects.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvSubjects.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(30, 64, 175);
            // 
            // colSubjectId
            // 
            colSubjectId.DataPropertyName = "SubjectID";
            colSubjectId.FillWeight = 42F;
            colSubjectId.HeaderText = "ID";
            colSubjectId.Name = "colSubjectId";
            colSubjectId.ReadOnly = true;
            // 
            // colSubjectName
            // 
            colSubjectName.DataPropertyName = "SubjectName";
            colSubjectName.FillWeight = 130F;
            colSubjectName.HeaderText = "Subject Name";
            colSubjectName.Name = "colSubjectName";
            colSubjectName.ReadOnly = true;
            // 
            // colStudyYear
            // 
            colStudyYear.DataPropertyName = "StudyYearID";
            colStudyYear.FillWeight = 70F;
            colStudyYear.HeaderText = "Study Year";
            colStudyYear.Name = "colStudyYear";
            colStudyYear.ReadOnly = true;
            // 
            // colBranch
            // 
            colBranch.DataPropertyName = "BranchID";
            colBranch.FillWeight = 70F;
            colBranch.HeaderText = "Branch";
            colBranch.Name = "colBranch";
            colBranch.ReadOnly = true;
            // 
            // colSemester
            // 
            colSemester.DataPropertyName = "SemesterNumber";
            colSemester.FillWeight = 55F;
            colSemester.HeaderText = "Semester";
            colSemester.Name = "colSemester";
            colSemester.ReadOnly = true;
            // 
            // colTheoreticalHours
            // 
            colTheoreticalHours.DataPropertyName = "TheoreticalHours";
            colTheoreticalHours.FillWeight = 58F;
            colTheoreticalHours.HeaderText = "Theory";
            colTheoreticalHours.Name = "colTheoreticalHours";
            colTheoreticalHours.ReadOnly = true;
            // 
            // colPracticalHours
            // 
            colPracticalHours.DataPropertyName = "PracticalHours";
            colPracticalHours.FillWeight = 58F;
            colPracticalHours.HeaderText = "Practical";
            colPracticalHours.Name = "colPracticalHours";
            colPracticalHours.ReadOnly = true;
            // 
            // colCreditUnits
            // 
            colCreditUnits.DataPropertyName = "CreditUnits";
            colCreditUnits.FillWeight = 55F;
            colCreditUnits.HeaderText = "Units";
            colCreditUnits.Name = "colCreditUnits";
            colCreditUnits.ReadOnly = true;
            // 
            // colRequirementType
            // 
            colRequirementType.DataPropertyName = "RequirementType";
            colRequirementType.FillWeight = 92F;
            colRequirementType.HeaderText = "Requirement";
            colRequirementType.Name = "colRequirementType";
            colRequirementType.ReadOnly = true;
            // 
            // lblTableSubtitle
            // 
            lblTableSubtitle.BackColor = Color.Transparent;
            lblTableSubtitle.Font = new Font("Segoe UI", 9F);
            lblTableSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblTableSubtitle.Location = new Point(24, 43);
            lblTableSubtitle.Name = "lblTableSubtitle";
            lblTableSubtitle.Size = new Size(236, 17);
            lblTableSubtitle.TabIndex = 1;
            lblTableSubtitle.Text = "Review and select academic subject records.";
            // 
            // lblTableTitle
            // 
            lblTableTitle.BackColor = Color.Transparent;
            lblTableTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            lblTableTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTableTitle.Location = new Point(24, 18);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new Size(96, 25);
            lblTableTitle.TabIndex = 0;
            lblTableTitle.Text = "Subjects List";
            // 
            // pnlSubjectEditor
            // 
            pnlSubjectEditor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlSubjectEditor.BackColor = Color.Transparent;
            pnlSubjectEditor.BorderColor = Color.FromArgb(226, 232, 240);
            pnlSubjectEditor.BorderRadius = 8;
            pnlSubjectEditor.BorderThickness = 1;
            pnlSubjectEditor.Controls.Add(btnClearSubjectForm);
            pnlSubjectEditor.Controls.Add(btnDeleteSubject);
            pnlSubjectEditor.Controls.Add(btnUpdateSubject);
            pnlSubjectEditor.Controls.Add(btnAddSubject);
            pnlSubjectEditor.Controls.Add(txtCreditUnits);
            pnlSubjectEditor.Controls.Add(lblCreditUnits);
            pnlSubjectEditor.Controls.Add(txtPracticalHours);
            pnlSubjectEditor.Controls.Add(lblPracticalHours);
            pnlSubjectEditor.Controls.Add(txtTheoreticalHours);
            pnlSubjectEditor.Controls.Add(lblTheoreticalHours);
            pnlSubjectEditor.Controls.Add(cmbRequirementType);
            pnlSubjectEditor.Controls.Add(lblRequirementType);
            pnlSubjectEditor.Controls.Add(cmbSemester);
            pnlSubjectEditor.Controls.Add(lblSemester);
            pnlSubjectEditor.Controls.Add(cmbBranch);
            pnlSubjectEditor.Controls.Add(lblBranch);
            pnlSubjectEditor.Controls.Add(cmbStudyYear);
            pnlSubjectEditor.Controls.Add(lblStudyYear);
            pnlSubjectEditor.Controls.Add(txtSubjectName);
            pnlSubjectEditor.Controls.Add(lblSubjectName);
            pnlSubjectEditor.Controls.Add(txtSubjectId);
            pnlSubjectEditor.Controls.Add(lblSubjectId);
            pnlSubjectEditor.Controls.Add(lblEditorSubtitle);
            pnlSubjectEditor.Controls.Add(lblEditorTitle);
            pnlSubjectEditor.CustomizableEdges = customizableEdges45;
            pnlSubjectEditor.FillColor = Color.White;
            pnlSubjectEditor.Location = new Point(28, 24);
            pnlSubjectEditor.Name = "pnlSubjectEditor";
            pnlSubjectEditor.ShadowDecoration.CustomizableEdges = customizableEdges46;
            pnlSubjectEditor.Size = new Size(884, 290);
            pnlSubjectEditor.TabIndex = 0;
            // 
            // btnClearSubjectForm
            // 
            btnClearSubjectForm.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearSubjectForm.BorderColor = Color.FromArgb(203, 213, 225);
            btnClearSubjectForm.BorderRadius = 8;
            btnClearSubjectForm.BorderThickness = 1;
            btnClearSubjectForm.Cursor = Cursors.Hand;
            btnClearSubjectForm.CustomizableEdges = customizableEdges19;
            btnClearSubjectForm.FillColor = Color.White;
            btnClearSubjectForm.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnClearSubjectForm.ForeColor = Color.FromArgb(51, 65, 85);
            btnClearSubjectForm.HoverState.FillColor = Color.FromArgb(248, 250, 252);
            btnClearSubjectForm.Location = new Point(752, 142);
            btnClearSubjectForm.Name = "btnClearSubjectForm";
            btnClearSubjectForm.ShadowDecoration.CustomizableEdges = customizableEdges20;
            btnClearSubjectForm.Size = new Size(108, 38);
            btnClearSubjectForm.TabIndex = 23;
            btnClearSubjectForm.Text = "Clear";
            btnClearSubjectForm.Visible = false;
            // 
            // btnDeleteSubject
            // 
            btnDeleteSubject.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDeleteSubject.BorderRadius = 8;
            btnDeleteSubject.Cursor = Cursors.Hand;
            btnDeleteSubject.CustomizableEdges = customizableEdges21;
            btnDeleteSubject.FillColor = Color.FromArgb(220, 38, 38);
            btnDeleteSubject.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnDeleteSubject.ForeColor = Color.White;
            btnDeleteSubject.HoverState.FillColor = Color.FromArgb(185, 28, 28);
            btnDeleteSubject.Location = new Point(632, 142);
            btnDeleteSubject.Name = "btnDeleteSubject";
            btnDeleteSubject.ShadowDecoration.CustomizableEdges = customizableEdges22;
            btnDeleteSubject.Size = new Size(108, 38);
            btnDeleteSubject.TabIndex = 22;
            btnDeleteSubject.Text = "Delete";
            // 
            // btnUpdateSubject
            // 
            btnUpdateSubject.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnUpdateSubject.BorderRadius = 8;
            btnUpdateSubject.Cursor = Cursors.Hand;
            btnUpdateSubject.CustomizableEdges = customizableEdges23;
            btnUpdateSubject.FillColor = Color.FromArgb(14, 116, 144);
            btnUpdateSubject.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnUpdateSubject.ForeColor = Color.White;
            btnUpdateSubject.HoverState.FillColor = Color.FromArgb(21, 94, 117);
            btnUpdateSubject.Location = new Point(752, 98);
            btnUpdateSubject.Name = "btnUpdateSubject";
            btnUpdateSubject.ShadowDecoration.CustomizableEdges = customizableEdges24;
            btnUpdateSubject.Size = new Size(108, 38);
            btnUpdateSubject.TabIndex = 21;
            btnUpdateSubject.Text = "Update";
            // 
            // btnAddSubject
            // 
            btnAddSubject.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddSubject.BorderRadius = 8;
            btnAddSubject.Cursor = Cursors.Hand;
            btnAddSubject.CustomizableEdges = customizableEdges25;
            btnAddSubject.FillColor = Color.FromArgb(22, 163, 74);
            btnAddSubject.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnAddSubject.ForeColor = Color.White;
            btnAddSubject.HoverState.FillColor = Color.FromArgb(21, 128, 61);
            btnAddSubject.Location = new Point(632, 98);
            btnAddSubject.Name = "btnAddSubject";
            btnAddSubject.ShadowDecoration.CustomizableEdges = customizableEdges26;
            btnAddSubject.Size = new Size(108, 38);
            btnAddSubject.TabIndex = 20;
            btnAddSubject.Text = "Add";
            // 
            // txtCreditUnits
            // 
            txtCreditUnits.BorderColor = Color.FromArgb(203, 213, 225);
            txtCreditUnits.BorderRadius = 8;
            txtCreditUnits.Cursor = Cursors.IBeam;
            txtCreditUnits.CustomizableEdges = customizableEdges27;
            txtCreditUnits.DefaultText = "";
            txtCreditUnits.DisabledState.BorderColor = Color.FromArgb(226, 232, 240);
            txtCreditUnits.DisabledState.FillColor = Color.FromArgb(248, 250, 252);
            txtCreditUnits.DisabledState.ForeColor = Color.FromArgb(100, 116, 139);
            txtCreditUnits.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            txtCreditUnits.Font = new Font("Segoe UI", 10F);
            txtCreditUnits.ForeColor = Color.FromArgb(15, 23, 42);
            txtCreditUnits.HoverState.BorderColor = Color.FromArgb(59, 130, 246);
            txtCreditUnits.Location = new Point(402, 224);
            txtCreditUnits.Margin = new Padding(3, 4, 3, 4);
            txtCreditUnits.Name = "txtCreditUnits";
            txtCreditUnits.PlaceholderForeColor = Color.FromArgb(148, 163, 184);
            txtCreditUnits.PlaceholderText = "Units";
            txtCreditUnits.SelectedText = "";
            txtCreditUnits.ShadowDecoration.CustomizableEdges = customizableEdges28;
            txtCreditUnits.Size = new Size(190, 42);
            txtCreditUnits.TabIndex = 19;
            // 
            // lblCreditUnits
            // 
            lblCreditUnits.BackColor = Color.Transparent;
            lblCreditUnits.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblCreditUnits.ForeColor = Color.FromArgb(51, 65, 85);
            lblCreditUnits.Location = new Point(402, 199);
            lblCreditUnits.Name = "lblCreditUnits";
            lblCreditUnits.Size = new Size(74, 19);
            lblCreditUnits.TabIndex = 18;
            lblCreditUnits.Text = "Credit Units";
            // 
            // txtPracticalHours
            // 
            txtPracticalHours.BorderColor = Color.FromArgb(203, 213, 225);
            txtPracticalHours.BorderRadius = 8;
            txtPracticalHours.Cursor = Cursors.IBeam;
            txtPracticalHours.CustomizableEdges = customizableEdges29;
            txtPracticalHours.DefaultText = "";
            txtPracticalHours.DisabledState.BorderColor = Color.FromArgb(226, 232, 240);
            txtPracticalHours.DisabledState.FillColor = Color.FromArgb(248, 250, 252);
            txtPracticalHours.DisabledState.ForeColor = Color.FromArgb(100, 116, 139);
            txtPracticalHours.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            txtPracticalHours.Font = new Font("Segoe UI", 10F);
            txtPracticalHours.ForeColor = Color.FromArgb(15, 23, 42);
            txtPracticalHours.HoverState.BorderColor = Color.FromArgb(59, 130, 246);
            txtPracticalHours.Location = new Point(214, 224);
            txtPracticalHours.Margin = new Padding(3, 4, 3, 4);
            txtPracticalHours.Name = "txtPracticalHours";
            txtPracticalHours.PlaceholderForeColor = Color.FromArgb(148, 163, 184);
            txtPracticalHours.PlaceholderText = "0";
            txtPracticalHours.SelectedText = "";
            txtPracticalHours.ShadowDecoration.CustomizableEdges = customizableEdges30;
            txtPracticalHours.Size = new Size(160, 42);
            txtPracticalHours.TabIndex = 17;
            // 
            // lblPracticalHours
            // 
            lblPracticalHours.BackColor = Color.Transparent;
            lblPracticalHours.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblPracticalHours.ForeColor = Color.FromArgb(51, 65, 85);
            lblPracticalHours.Location = new Point(214, 199);
            lblPracticalHours.Name = "lblPracticalHours";
            lblPracticalHours.Size = new Size(94, 19);
            lblPracticalHours.TabIndex = 16;
            lblPracticalHours.Text = "Practical Hours";
            // 
            // txtTheoreticalHours
            // 
            txtTheoreticalHours.BorderColor = Color.FromArgb(203, 213, 225);
            txtTheoreticalHours.BorderRadius = 8;
            txtTheoreticalHours.Cursor = Cursors.IBeam;
            txtTheoreticalHours.CustomizableEdges = customizableEdges31;
            txtTheoreticalHours.DefaultText = "";
            txtTheoreticalHours.DisabledState.BorderColor = Color.FromArgb(226, 232, 240);
            txtTheoreticalHours.DisabledState.FillColor = Color.FromArgb(248, 250, 252);
            txtTheoreticalHours.DisabledState.ForeColor = Color.FromArgb(100, 116, 139);
            txtTheoreticalHours.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            txtTheoreticalHours.Font = new Font("Segoe UI", 10F);
            txtTheoreticalHours.ForeColor = Color.FromArgb(15, 23, 42);
            txtTheoreticalHours.HoverState.BorderColor = Color.FromArgb(59, 130, 246);
            txtTheoreticalHours.Location = new Point(24, 224);
            txtTheoreticalHours.Margin = new Padding(3, 4, 3, 4);
            txtTheoreticalHours.Name = "txtTheoreticalHours";
            txtTheoreticalHours.PlaceholderForeColor = Color.FromArgb(148, 163, 184);
            txtTheoreticalHours.PlaceholderText = "0";
            txtTheoreticalHours.SelectedText = "";
            txtTheoreticalHours.ShadowDecoration.CustomizableEdges = customizableEdges32;
            txtTheoreticalHours.Size = new Size(160, 42);
            txtTheoreticalHours.TabIndex = 15;
            // 
            // lblTheoreticalHours
            // 
            lblTheoreticalHours.BackColor = Color.Transparent;
            lblTheoreticalHours.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblTheoreticalHours.ForeColor = Color.FromArgb(51, 65, 85);
            lblTheoreticalHours.Location = new Point(24, 199);
            lblTheoreticalHours.Name = "lblTheoreticalHours";
            lblTheoreticalHours.Size = new Size(110, 19);
            lblTheoreticalHours.TabIndex = 14;
            lblTheoreticalHours.Text = "Theoretical Hours";
            // 
            // cmbRequirementType
            // 
            cmbRequirementType.BackColor = Color.Transparent;
            cmbRequirementType.BorderColor = Color.FromArgb(203, 213, 225);
            cmbRequirementType.BorderRadius = 8;
            cmbRequirementType.CustomizableEdges = customizableEdges33;
            cmbRequirementType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbRequirementType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRequirementType.FocusedColor = Color.FromArgb(37, 99, 235);
            cmbRequirementType.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbRequirementType.Font = new Font("Segoe UI", 10F);
            cmbRequirementType.ForeColor = Color.FromArgb(15, 23, 42);
            cmbRequirementType.HoverState.BorderColor = Color.FromArgb(59, 130, 246);
            cmbRequirementType.ItemHeight = 36;
            cmbRequirementType.Items.AddRange(new object[] { "Core", "Elective", "University Requirement", "College Requirement" });
            cmbRequirementType.Location = new Point(402, 154);
            cmbRequirementType.Name = "cmbRequirementType";
            cmbRequirementType.ShadowDecoration.CustomizableEdges = customizableEdges34;
            cmbRequirementType.Size = new Size(190, 42);
            cmbRequirementType.TabIndex = 13;
            // 
            // lblRequirementType
            // 
            lblRequirementType.BackColor = Color.Transparent;
            lblRequirementType.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblRequirementType.ForeColor = Color.FromArgb(51, 65, 85);
            lblRequirementType.Location = new Point(402, 129);
            lblRequirementType.Name = "lblRequirementType";
            lblRequirementType.Size = new Size(114, 19);
            lblRequirementType.TabIndex = 12;
            lblRequirementType.Text = "Requirement Type";
            // 
            // cmbSemester
            // 
            cmbSemester.BackColor = Color.Transparent;
            cmbSemester.BorderColor = Color.FromArgb(203, 213, 225);
            cmbSemester.BorderRadius = 8;
            cmbSemester.CustomizableEdges = customizableEdges35;
            cmbSemester.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSemester.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSemester.FocusedColor = Color.FromArgb(37, 99, 235);
            cmbSemester.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbSemester.Font = new Font("Segoe UI", 10F);
            cmbSemester.ForeColor = Color.FromArgb(15, 23, 42);
            cmbSemester.HoverState.BorderColor = Color.FromArgb(59, 130, 246);
            cmbSemester.ItemHeight = 36;
            cmbSemester.Items.AddRange(new object[] { "1", "2" });
            cmbSemester.Location = new Point(214, 154);
            cmbSemester.Name = "cmbSemester";
            cmbSemester.ShadowDecoration.CustomizableEdges = customizableEdges36;
            cmbSemester.Size = new Size(160, 42);
            cmbSemester.TabIndex = 11;
            // 
            // lblSemester
            // 
            lblSemester.BackColor = Color.Transparent;
            lblSemester.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblSemester.ForeColor = Color.FromArgb(51, 65, 85);
            lblSemester.Location = new Point(214, 129);
            lblSemester.Name = "lblSemester";
            lblSemester.Size = new Size(59, 19);
            lblSemester.TabIndex = 10;
            lblSemester.Text = "Semester";
            // 
            // cmbBranch
            // 
            cmbBranch.BackColor = Color.Transparent;
            cmbBranch.BorderColor = Color.FromArgb(203, 213, 225);
            cmbBranch.BorderRadius = 8;
            cmbBranch.CustomizableEdges = customizableEdges37;
            cmbBranch.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBranch.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBranch.FocusedColor = Color.FromArgb(37, 99, 235);
            cmbBranch.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbBranch.Font = new Font("Segoe UI", 10F);
            cmbBranch.ForeColor = Color.FromArgb(15, 23, 42);
            cmbBranch.HoverState.BorderColor = Color.FromArgb(59, 130, 246);
            cmbBranch.ItemHeight = 36;
            cmbBranch.Location = new Point(24, 154);
            cmbBranch.Name = "cmbBranch";
            cmbBranch.ShadowDecoration.CustomizableEdges = customizableEdges38;
            cmbBranch.Size = new Size(160, 42);
            cmbBranch.TabIndex = 9;
            // 
            // lblBranch
            // 
            lblBranch.BackColor = Color.Transparent;
            lblBranch.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblBranch.ForeColor = Color.FromArgb(51, 65, 85);
            lblBranch.Location = new Point(24, 129);
            lblBranch.Name = "lblBranch";
            lblBranch.Size = new Size(45, 19);
            lblBranch.TabIndex = 8;
            lblBranch.Text = "Branch";
            // 
            // cmbStudyYear
            // 
            cmbStudyYear.BackColor = Color.Transparent;
            cmbStudyYear.BorderColor = Color.FromArgb(203, 213, 225);
            cmbStudyYear.BorderRadius = 8;
            cmbStudyYear.CustomizableEdges = customizableEdges39;
            cmbStudyYear.DrawMode = DrawMode.OwnerDrawFixed;
            cmbStudyYear.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStudyYear.FocusedColor = Color.FromArgb(37, 99, 235);
            cmbStudyYear.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            cmbStudyYear.Font = new Font("Segoe UI", 10F);
            cmbStudyYear.ForeColor = Color.FromArgb(15, 23, 42);
            cmbStudyYear.HoverState.BorderColor = Color.FromArgb(59, 130, 246);
            cmbStudyYear.ItemHeight = 36;
            cmbStudyYear.Location = new Point(402, 84);
            cmbStudyYear.Name = "cmbStudyYear";
            cmbStudyYear.ShadowDecoration.CustomizableEdges = customizableEdges40;
            cmbStudyYear.Size = new Size(190, 42);
            cmbStudyYear.TabIndex = 7;
            // 
            // lblStudyYear
            // 
            lblStudyYear.BackColor = Color.Transparent;
            lblStudyYear.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblStudyYear.ForeColor = Color.FromArgb(51, 65, 85);
            lblStudyYear.Location = new Point(402, 59);
            lblStudyYear.Name = "lblStudyYear";
            lblStudyYear.Size = new Size(69, 19);
            lblStudyYear.TabIndex = 6;
            lblStudyYear.Text = "Study Year";
            // 
            // txtSubjectName
            // 
            txtSubjectName.BorderColor = Color.FromArgb(203, 213, 225);
            txtSubjectName.BorderRadius = 8;
            txtSubjectName.Cursor = Cursors.IBeam;
            txtSubjectName.CustomizableEdges = customizableEdges41;
            txtSubjectName.DefaultText = "";
            txtSubjectName.DisabledState.BorderColor = Color.FromArgb(226, 232, 240);
            txtSubjectName.DisabledState.FillColor = Color.FromArgb(248, 250, 252);
            txtSubjectName.DisabledState.ForeColor = Color.FromArgb(100, 116, 139);
            txtSubjectName.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            txtSubjectName.Font = new Font("Segoe UI", 10F);
            txtSubjectName.ForeColor = Color.FromArgb(15, 23, 42);
            txtSubjectName.HoverState.BorderColor = Color.FromArgb(59, 130, 246);
            txtSubjectName.Location = new Point(184, 84);
            txtSubjectName.Margin = new Padding(3, 4, 3, 4);
            txtSubjectName.Name = "txtSubjectName";
            txtSubjectName.PlaceholderForeColor = Color.FromArgb(148, 163, 184);
            txtSubjectName.PlaceholderText = "Enter subject name";
            txtSubjectName.SelectedText = "";
            txtSubjectName.ShadowDecoration.CustomizableEdges = customizableEdges42;
            txtSubjectName.Size = new Size(190, 42);
            txtSubjectName.TabIndex = 5;
            // 
            // lblSubjectName
            // 
            lblSubjectName.BackColor = Color.Transparent;
            lblSubjectName.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblSubjectName.ForeColor = Color.FromArgb(51, 65, 85);
            lblSubjectName.Location = new Point(184, 59);
            lblSubjectName.Name = "lblSubjectName";
            lblSubjectName.Size = new Size(87, 19);
            lblSubjectName.TabIndex = 4;
            lblSubjectName.Text = "Subject Name";
            // 
            // txtSubjectId
            // 
            txtSubjectId.BorderColor = Color.FromArgb(226, 232, 240);
            txtSubjectId.BorderRadius = 8;
            txtSubjectId.Cursor = Cursors.IBeam;
            txtSubjectId.CustomizableEdges = customizableEdges43;
            txtSubjectId.DefaultText = "";
            txtSubjectId.DisabledState.BorderColor = Color.FromArgb(226, 232, 240);
            txtSubjectId.DisabledState.FillColor = Color.FromArgb(248, 250, 252);
            txtSubjectId.DisabledState.ForeColor = Color.FromArgb(100, 116, 139);
            txtSubjectId.Enabled = true;
            txtSubjectId.FillColor = Color.White;
            txtSubjectId.Font = new Font("Segoe UI", 10F);
            txtSubjectId.ForeColor = Color.FromArgb(15, 23, 42);
            txtSubjectId.Location = new Point(24, 84);
            txtSubjectId.Margin = new Padding(3, 4, 3, 4);
            txtSubjectId.Name = "txtSubjectId";
            txtSubjectId.PlaceholderForeColor = Color.FromArgb(148, 163, 184);
            txtSubjectId.PlaceholderText = "Enter ID";
            txtSubjectId.SelectedText = "";
            txtSubjectId.ShadowDecoration.CustomizableEdges = customizableEdges44;
            txtSubjectId.Size = new Size(132, 42);
            txtSubjectId.TabIndex = 3;
            // 
            // lblSubjectId
            // 
            lblSubjectId.BackColor = Color.Transparent;
            lblSubjectId.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblSubjectId.ForeColor = Color.FromArgb(51, 65, 85);
            lblSubjectId.Location = new Point(24, 59);
            lblSubjectId.Name = "lblSubjectId";
            lblSubjectId.Size = new Size(64, 19);
            lblSubjectId.TabIndex = 2;
            lblSubjectId.Text = "Subject ID";
            // 
            // lblEditorSubtitle
            // 
            lblEditorSubtitle.BackColor = Color.Transparent;
            lblEditorSubtitle.Font = new Font("Segoe UI", 9F);
            lblEditorSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblEditorSubtitle.Location = new Point(24, 34);
            lblEditorSubtitle.Name = "lblEditorSubtitle";
            lblEditorSubtitle.Size = new Size(262, 17);
            lblEditorSubtitle.TabIndex = 1;
            lblEditorSubtitle.Text = "Prepare subject details before applying an action.";
            // 
            // lblEditorTitle
            // 
            lblEditorTitle.BackColor = Color.Transparent;
            lblEditorTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            lblEditorTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblEditorTitle.Location = new Point(24, 9);
            lblEditorTitle.Name = "lblEditorTitle";
            lblEditorTitle.Size = new Size(115, 25);
            lblEditorTitle.TabIndex = 0;
            lblEditorTitle.Text = "Subject Details";
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblPageSubtitle);
            pnlHeader.Controls.Add(lblPageTitle);
            pnlHeader.CustomizableEdges = customizableEdges49;
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.FillColor = Color.White;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.ShadowDecoration.CustomizableEdges = customizableEdges50;
            pnlHeader.Size = new Size(940, 88);
            pnlHeader.TabIndex = 0;
            // 
            // lblPageSubtitle
            // 
            lblPageSubtitle.BackColor = Color.Transparent;
            lblPageSubtitle.Font = new Font("Segoe UI", 10F);
            lblPageSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblPageSubtitle.Location = new Point(32, 50);
            lblPageSubtitle.Name = "lblPageSubtitle";
            lblPageSubtitle.Size = new Size(394, 19);
            lblPageSubtitle.TabIndex = 1;
            lblPageSubtitle.Text = "Manage subjects, semesters, credit units, and academic ownership.";
            // 
            // lblPageTitle
            // 
            lblPageTitle.BackColor = Color.Transparent;
            lblPageTitle.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblPageTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblPageTitle.Location = new Point(32, 16);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new Size(246, 34);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Subjects Management";
            // 
            // SubjectsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1180, 720);
            Controls.Add(pnlMain);
            Controls.Add(pnlSidebar);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(980, 600);
            Name = "SubjectsForm";
            Text = "Subjects Management";
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlWorkspace.ResumeLayout(false);
            pnlSubjectsTable.ResumeLayout(false);
            pnlSubjectsTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSubjects).EndInit();
            pnlSubjectEditor.ResumeLayout(false);
            pnlSubjectEditor.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlSidebar;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblApplicationName;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSidebarSubtitle;
        private Guna.UI2.WinForms.Guna2Separator separatorSidebar;
        private Guna.UI2.WinForms.Guna2Button btnNavigationDashboard;
        private Guna.UI2.WinForms.Guna2Button btnNavigationBranches;
        private Guna.UI2.WinForms.Guna2Button btnNavigationStudyYears;
        private Guna.UI2.WinForms.Guna2Button btnNavigationSubjects;
        private Guna.UI2.WinForms.Guna2Button btnNavigationClassrooms;
        private Guna.UI2.WinForms.Guna2Button btnNavigationFaculty;
        private Guna.UI2.WinForms.Guna2Button btnNavigationSchedules;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSidebarFooter;
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageSubtitle;
        private Guna.UI2.WinForms.Guna2Panel pnlWorkspace;
        private Guna.UI2.WinForms.Guna2Panel pnlSubjectEditor;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEditorTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEditorSubtitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubjectId;
        private Guna.UI2.WinForms.Guna2TextBox txtSubjectId;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubjectName;
        private Guna.UI2.WinForms.Guna2TextBox txtSubjectName;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblStudyYear;
        private Guna.UI2.WinForms.Guna2ComboBox cmbStudyYear;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblBranch;
        private Guna.UI2.WinForms.Guna2ComboBox cmbBranch;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSemester;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSemester;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblRequirementType;
        private Guna.UI2.WinForms.Guna2ComboBox cmbRequirementType;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTheoreticalHours;
        private Guna.UI2.WinForms.Guna2TextBox txtTheoreticalHours;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPracticalHours;
        private Guna.UI2.WinForms.Guna2TextBox txtPracticalHours;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCreditUnits;
        private Guna.UI2.WinForms.Guna2TextBox txtCreditUnits;
        private Guna.UI2.WinForms.Guna2Button btnAddSubject;
        private Guna.UI2.WinForms.Guna2Button btnUpdateSubject;
        private Guna.UI2.WinForms.Guna2Button btnDeleteSubject;
        private Guna.UI2.WinForms.Guna2Button btnClearSubjectForm;
        private Guna.UI2.WinForms.Guna2Panel pnlSubjectsTable;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTableSubtitle;
        private Guna.UI2.WinForms.Guna2DataGridView dgvSubjects;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubjectId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudyYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSemester;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTheoreticalHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPracticalHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreditUnits;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRequirementType;
}
}
