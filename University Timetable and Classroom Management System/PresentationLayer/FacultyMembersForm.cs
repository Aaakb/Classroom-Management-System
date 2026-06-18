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
            if (!TryGetSelectedFacultyMemberId(out int facultyMemberId) || !TryBuildFacultyMember(out var facultyMember))
            {
                ShowInformation("Select a faculty member before updating.");
                return;
            }

            facultyMember.FacultyMemberID = facultyMemberId;

            await ExecuteFacultyMemberActionAsync(
                async () => await facultyMemberService.UpdateAsync(facultyMember),
                "Faculty member updated successfully.");
        }

        private async Task DeleteFacultyMemberAsync()
        {
            if (!TryGetSelectedFacultyMemberId(out int facultyMemberId))
            {
                ShowInformation("Select a faculty member before deleting.");
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

        private bool TryGetSelectedFacultyMemberId(out int facultyMemberId)
        {
            if (int.TryParse(txtFacultyMemberId.Text, out facultyMemberId))
            {
                return true;
            }

            var selectedFacultyMember = dgvFacultyMembers.CurrentRow?.DataBoundItem as FacultyMember;
            facultyMemberId = selectedFacultyMember?.FacultyMemberID ?? 0;
            return facultyMemberId > 0;
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

        private void ClearFacultyMemberForm()
        {
            txtFacultyMemberId.Clear();
            txtFacultyMemberFullName.Clear();
            cmbAcademicTitle.SelectedIndex = -1;
            dgvFacultyMembers.ClearSelection();
            txtFacultyMemberFullName.Focus();
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
