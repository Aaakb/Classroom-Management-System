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
            ConfigureAutoIdField();
            ConfigureNavigation();
            ConfigureBranchesGrid();
            ConfigureBranchesEvents();
        }

        private void ConfigureAutoIdField()
        {
            txtBranchId.ReadOnly = true;
            txtBranchId.TabStop = false;
            txtBranchId.PlaceholderText = "Auto";
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
            txtBranchId.Leave += async (_, _) => await PopulateBranchEditorFromEnteredIdAsync();
            btnAddBranch.Click += async (_, _) => await AddBranchAsync();
            btnUpdateBranch.Click += async (_, _) => await UpdateBranchAsync();
            btnDeleteBranch.Click += async (_, _) => await DeleteBranchAsync();
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
            if (!TryBuildBranch(out var branch, requireId: false))
            {
                return;
            }

            await ExecuteBranchActionAsync(
                async () => await branchService.AddAsync(branch),
                "Branch added successfully.");
        }

        private async Task UpdateBranchAsync()
        {
            if (!TryBuildBranch(out var branch))
            {
                return;
            }

            await ExecuteBranchActionAsync(
                async () => await branchService.UpdateAsync(branch),
                "Branch updated successfully.");
        }

        private async Task DeleteBranchAsync()
        {
            if (!TryGetBranchIdFromEditor(out int branchId))
            {
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

        private bool TryBuildBranch(out Branch branch, bool requireId = true)
        {
            branch = new Branch
            {
                BranchName = txtBranchName.Text.Trim()
            };

            var branchId = 0;
            if (requireId && !TryGetBranchIdFromEditor(out branchId))
            {
                return false;
            }

            branch.BranchID = requireId ? branchId : 0;

            if (!string.IsNullOrWhiteSpace(branch.BranchName))
            {
                return true;
            }

            ShowInformation("Branch name is required.");
            txtBranchName.Focus();
            return false;
        }

        private bool TryGetBranchIdFromEditor(out int branchId)
        {
            if (int.TryParse(txtBranchId.Text, out branchId) && branchId > 0)
            {
                return true;
            }

            ShowInformation("Select a branch row first.");
            return false;
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

        private async Task PopulateBranchEditorFromEnteredIdAsync()
        {
            if (!int.TryParse(txtBranchId.Text, out int branchId) || branchId <= 0)
            {
                return;
            }

            try
            {
                var branch = await branchService.GetByIdAsync(branchId);

                if (branch is null)
                {
                    return;
                }

                txtBranchName.Text = branch.BranchName;
                SelectBranchRow(branch.BranchID);
            }
            catch (Exception ex)
            {
                ShowError("Unable to load branch details.", ex);
            }
        }

        private void SelectBranchRow(int branchId)
        {
            foreach (DataGridViewRow row in dgvBranches.Rows)
            {
                if (row.DataBoundItem is not Branch branch || branch.BranchID != branchId)
                {
                    continue;
                }

                row.Selected = true;
                dgvBranches.CurrentCell = row.Cells[0];
                break;
            }
        }

        private void ClearBranchForm()
        {
            txtBranchId.Text = "Auto";
            txtBranchName.Clear();
            dgvBranches.ClearSelection();
            txtBranchName.Focus();
        }

        private void SetBranchActionsEnabled(bool enabled)
        {
            btnAddBranch.Enabled = enabled;
            btnUpdateBranch.Enabled = enabled;
            btnDeleteBranch.Enabled = enabled;
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
    }
}
