using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class FacultyMembersForm
    {
        private readonly FacultyMemberService facultyMemberService = new();

        public FacultyMembersForm()
        {
            InitializeComponent();
            ConfigureNavigation();
            ConfigureFacultyMembersGrid();
            ConfigureFacultyMembersEvents();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadFacultyMembersAsync();
            ClearFacultyMemberForm();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.FacultyMembers);
        }

        private void ConfigureFacultyMembersGrid()
        {
            dgvFacultyMembers.AutoGenerateColumns = false;
            dgvFacultyMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void ConfigureFacultyMembersEvents()
        {
            dgvFacultyMembers.SelectionChanged += (_, _) => PopulateFacultyMemberEditorFromSelection();
            txtFacultyMemberId.Leave += async (_, _) => await PopulateFacultyMemberEditorFromEnteredIdAsync();
            btnAddFacultyMember.Click += async (_, _) => await AddFacultyMemberAsync();
            btnUpdateFacultyMember.Click += async (_, _) => await UpdateFacultyMemberAsync();
            btnDeleteFacultyMember.Click += async (_, _) => await DeleteFacultyMemberAsync();
        }

        private async Task LoadFacultyMembersAsync()
        {
            SetFacultyMemberActionsEnabled(false);

            try
            {
                var facultyMembers = await facultyMemberService.GetAllAsync();
                dgvFacultyMembers.DataSource = facultyMembers;
                dgvFacultyMembers.ClearSelection();
            }
            catch (Exception ex)
            {
                ShowError("Unable to load faculty members.", ex);
            }
            finally
            {
                SetFacultyMemberActionsEnabled(true);
            }
        }

        private async Task AddFacultyMemberAsync()
        {
            if (!TryBuildFacultyMember(out var facultyMember))
            {
                return;
            }

            await ExecuteFacultyMemberActionAsync(
                async () => await facultyMemberService.AddAsync(facultyMember),
                "Faculty member added successfully.");
        }

        private async Task UpdateFacultyMemberAsync()
        {
            if (!TryBuildFacultyMember(out var facultyMember))
            {
                return;
            }

            await ExecuteFacultyMemberActionAsync(
                async () => await facultyMemberService.UpdateAsync(facultyMember),
                "Faculty member updated successfully.");
        }

        private async Task DeleteFacultyMemberAsync()
        {
            if (!TryGetFacultyMemberIdFromEditor(out int facultyMemberId))
            {
                return;
            }

            var confirmation = MessageBox.Show(
                this,
                "Are you sure you want to delete the selected faculty member?",
                "Delete Faculty Member",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            await ExecuteFacultyMemberActionAsync(
                async () => await facultyMemberService.DeleteAsync(facultyMemberId),
                "Faculty member deleted successfully.");
        }

        private async Task ExecuteFacultyMemberActionAsync(Func<Task> action, string successMessage)
        {
            SetFacultyMemberActionsEnabled(false);

            try
            {
                await action();
                await LoadFacultyMembersAsync();
                ClearFacultyMemberForm();
                ShowInformation(successMessage);
            }
            catch (Exception ex)
            {
                ShowError("Unable to complete the faculty member operation.", ex);
            }
            finally
            {
                SetFacultyMemberActionsEnabled(true);
            }
        }

        private bool TryBuildFacultyMember(out FacultyMember facultyMember)
        {
            facultyMember = new FacultyMember
            {
                FullName = txtFacultyMemberFullName.Text.Trim(),
                AcademicTitle = cmbAcademicTitle.Text.Trim()
            };

            if (!TryGetFacultyMemberIdFromEditor(out int facultyMemberId))
            {
                return false;
            }

            facultyMember.FacultyMemberID = facultyMemberId;

            if (string.IsNullOrWhiteSpace(facultyMember.FullName))
            {
                ShowInformation("Faculty member full name is required.");
                txtFacultyMemberFullName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(facultyMember.AcademicTitle))
            {
                facultyMember.AcademicTitle = null;
            }

            return true;
        }

        private bool TryGetFacultyMemberIdFromEditor(out int facultyMemberId)
        {
            if (int.TryParse(txtFacultyMemberId.Text, out facultyMemberId) && facultyMemberId > 0)
            {
                return true;
            }

            ShowInformation("Enter a valid faculty member ID.");
            txtFacultyMemberId.Focus();
            return false;
        }

        private void PopulateFacultyMemberEditorFromSelection()
        {
            if (dgvFacultyMembers.CurrentRow?.DataBoundItem is not FacultyMember facultyMember)
            {
                return;
            }

            txtFacultyMemberId.Text = facultyMember.FacultyMemberID.ToString();
            txtFacultyMemberFullName.Text = facultyMember.FullName;
            cmbAcademicTitle.Text = facultyMember.AcademicTitle ?? string.Empty;
        }

        private async Task PopulateFacultyMemberEditorFromEnteredIdAsync()
        {
            if (!int.TryParse(txtFacultyMemberId.Text, out int facultyMemberId) || facultyMemberId <= 0)
            {
                return;
            }

            try
            {
                var facultyMember = await facultyMemberService.GetByIdAsync(facultyMemberId);

                if (facultyMember is null)
                {
                    return;
                }

                txtFacultyMemberFullName.Text = facultyMember.FullName;
                cmbAcademicTitle.Text = facultyMember.AcademicTitle ?? string.Empty;
                SelectFacultyMemberRow(facultyMember.FacultyMemberID);
            }
            catch (Exception ex)
            {
                ShowError("Unable to load faculty member details.", ex);
            }
        }

        private void SelectFacultyMemberRow(int facultyMemberId)
        {
            foreach (DataGridViewRow row in dgvFacultyMembers.Rows)
            {
                if (row.DataBoundItem is not FacultyMember facultyMember || facultyMember.FacultyMemberID != facultyMemberId)
                {
                    continue;
                }

                row.Selected = true;
                dgvFacultyMembers.CurrentCell = row.Cells[0];
                break;
            }
        }

        private void ClearFacultyMemberForm()
        {
            txtFacultyMemberId.Clear();
            txtFacultyMemberFullName.Clear();
            cmbAcademicTitle.SelectedIndex = -1;
            dgvFacultyMembers.ClearSelection();
            txtFacultyMemberId.Focus();
        }

        private void SetFacultyMemberActionsEnabled(bool enabled)
        {
            btnAddFacultyMember.Enabled = enabled;
            btnUpdateFacultyMember.Enabled = enabled;
            btnDeleteFacultyMember.Enabled = enabled;
            dgvFacultyMembers.Enabled = enabled;
        }

        private void ShowInformation(string message)
        {
            MessageBox.Show(this, message, "Faculty Members", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show(this, $"{message}\n\n{ex.Message}", "Faculty Members", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
