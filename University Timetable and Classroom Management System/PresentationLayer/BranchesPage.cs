using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class BranchesPage : UserControl
    {
        private readonly BranchService _service = new();
        private readonly EntityPageController<Branch> _controller;

        public BranchesPage()
        {
            InitializeComponent();

            _controller = new EntityPageController<Branch>(
                dgvRecords,
                btnAdd,
                btnUpdate,
                btnDelete,
                btnClear,
                btnRefresh,
                async () => await _service.GetAllAsync(),
                async branch => await _service.AddAsync(branch),
                async branch => await _service.UpdateAsync(branch),
                async branch => await _service.DeleteAsync(branch.BranchID),
                CreateEntityFromInputs,
                WriteEntityToInputs,
                ClearInputControls);

            _controller.RegisterColumn(branch => branch.BranchID);
            _controller.RegisterColumn(branch => branch.BranchName);
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                await _controller.RefreshDataAsync();
            }
        }

        private Branch CreateEntityFromInputs(Branch? selectedEntity)
        {
            return new Branch
            {
                BranchID = selectedEntity?.BranchID ?? 0,
                BranchName = PageInput.RequiredText(txtBranchName, "Branch name")
            };
        }

        private void WriteEntityToInputs(Branch entity)
        {
            txtBranchId.Text = entity.BranchID.ToString();
            txtBranchName.Text = entity.BranchName;
        }

        private void ClearInputControls()
        {
            txtBranchId.Clear();
            txtBranchName.Clear();
        }
    }
}
