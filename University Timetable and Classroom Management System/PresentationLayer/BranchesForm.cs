using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class BranchesForm
    {
        private readonly BranchService branchService = new();

        public BranchesForm()
        {
            InitializeComponent();
            ConfigureNavigation();
            ConfigureBranchesGrid();
            ConfigureBranchesEvents();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadBranchesAsync();
            ClearBranchForm();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.Branches);
        }

        private void ConfigureBranchesGrid()
        {
            dgvBranches.AutoGenerateColumns = false;
            dgvBranches.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void ConfigureBranchesEvents()
        {
            dgvBranches.SelectionChanged += (_, _) => PopulateBranchEditorFromSelection();
            btnAddBranch.Click += async (_, _) => await AddBranchAsync();
            btnUpdateBranch.Click += async (_, _) => await UpdateBranchAsync();
            btnDeleteBranch.Click += async (_, _) => await DeleteBranchAsync();
            btnClearBranchForm.Click += (_, _) => ClearBranchForm();
        }

        private async Task LoadBranchesAsync()
        {
            SetBranchActionsEnabled(false);

            try
            {
                var branches = await branchService.GetAllAsync();
                dgvBranches.DataSource = branches;
                dgvBranches.ClearSelection();
            }
            catch (Exception ex)
            {
                ShowError("Unable to load branches.", ex);
            }
            finally
            {
                SetBranchActionsEnabled(true);
            }
        }

        private async Task AddBranchAsync()
        {
            if (!TryBuildBranch(out var branch))
            {
                return;
            }

            await ExecuteBranchActionAsync(
                async () => await branchService.AddAsync(branch),
                "Branch added successfully.");
        }

        private async Task UpdateBranchAsync()
        {
            if (!TryGetSelectedBranchId(out int branchId) || !TryBuildBranch(out var branch))
            {
                ShowInformation("Select a branch before updating.");
                return;
            }

            branch.BranchID = branchId;

            await ExecuteBranchActionAsync(
                async () => await branchService.UpdateAsync(branch),
                "Branch updated successfully.");
        }

        private async Task DeleteBranchAsync()
        {
            if (!TryGetSelectedBranchId(out int branchId))
            {
                ShowInformation("Select a branch before deleting.");
                return;
            }

            var confirmation = MessageBox.Show(
                this,
                "Are you sure you want to delete the selected branch?",
                "Delete Branch",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            await ExecuteBranchActionAsync(
                async () => await branchService.DeleteAsync(branchId),
                "Branch deleted successfully.");
        }

        private async Task ExecuteBranchActionAsync(Func<Task> action, string successMessage)
        {
            SetBranchActionsEnabled(false);

            try
            {
                await action();
                await LoadBranchesAsync();
                ClearBranchForm();
                ShowInformation(successMessage);
            }
            catch (Exception ex)
            {
                ShowError("Unable to complete the branch operation.", ex);
            }
            finally
            {
                SetBranchActionsEnabled(true);
            }
        }

        private bool TryBuildBranch(out Branch branch)
        {
            branch = new Branch
            {
                BranchName = txtBranchName.Text.Trim()
            };

            if (!string.IsNullOrWhiteSpace(branch.BranchName))
            {
                return true;
            }

            ShowInformation("Branch name is required.");
            txtBranchName.Focus();
            return false;
        }

        private bool TryGetSelectedBranchId(out int branchId)
        {
            if (int.TryParse(txtBranchId.Text, out branchId))
            {
                return true;
            }

            var selectedBranch = dgvBranches.CurrentRow?.DataBoundItem as Branch;
            branchId = selectedBranch?.BranchID ?? 0;
            return branchId > 0;
        }

        private void PopulateBranchEditorFromSelection()
        {
            if (dgvBranches.CurrentRow?.DataBoundItem is not Branch branch)
            {
                return;
            }

            txtBranchId.Text = branch.BranchID.ToString();
            txtBranchName.Text = branch.BranchName;
        }

        private void ClearBranchForm()
        {
            txtBranchId.Clear();
            txtBranchName.Clear();
            dgvBranches.ClearSelection();
            txtBranchName.Focus();
        }

        private void SetBranchActionsEnabled(bool enabled)
        {
            btnAddBranch.Enabled = enabled;
            btnUpdateBranch.Enabled = enabled;
            btnDeleteBranch.Enabled = enabled;
            btnClearBranchForm.Enabled = enabled;
            dgvBranches.Enabled = enabled;
        }

        private void ShowInformation(string message)
        {
            MessageBox.Show(this, message, "Branches", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show(this, $"{message}\n\n{ex.Message}", "Branches", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void lblApplicationName_Click(object sender, EventArgs e)
        {
        }
    }
}
