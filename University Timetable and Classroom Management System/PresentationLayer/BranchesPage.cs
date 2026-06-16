using Guna.UI2.WinForms;
using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    internal sealed class BranchesPage : EntityManagementPage<Branch>
    {
        private readonly BranchService _service = new();
        private readonly Guna2TextBox _txtBranchId;
        private readonly Guna2TextBox _txtBranchName;

        public BranchesPage()
            : base(
                "Branches",
                "Manage academic branches used by subjects, sections, and schedules.",
                "Branch Details",
                "Create and maintain branch records.")
        {
            _txtBranchId = AddTextField("Branch ID", "Auto", true);
            _txtBranchName = AddTextField("Branch Name", "Enter branch name");

            AddGridColumn("ID", branch => branch.BranchID, 30F);
            AddGridColumn("Branch Name", branch => branch.BranchName, 170F);
        }

        protected override async Task<IReadOnlyList<Branch>> LoadEntitiesAsync()
        {
            return await _service.GetAllAsync();
        }

        protected override async Task AddEntityAsync(Branch entity)
        {
            await _service.AddAsync(entity);
        }

        protected override async Task UpdateEntityAsync(Branch entity)
        {
            await _service.UpdateAsync(entity);
        }

        protected override async Task DeleteEntityAsync(Branch entity)
        {
            await _service.DeleteAsync(entity.BranchID);
        }

        protected override Branch CreateEntityFromInputs(Branch? selectedEntity)
        {
            return new Branch
            {
                BranchID = selectedEntity?.BranchID ?? 0,
                BranchName = RequiredText(_txtBranchName, "Branch name")
            };
        }

        protected override void WriteEntityToInputs(Branch entity)
        {
            _txtBranchId.Text = entity.BranchID.ToString();
            _txtBranchName.Text = entity.BranchName;
        }

        protected override void ClearInputControls()
        {
            _txtBranchId.Clear();
            _txtBranchName.Clear();
        }
    }
}
