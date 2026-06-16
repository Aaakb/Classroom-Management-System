using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SectionsPage : UserControl
    {
        private readonly SectionService _service = new();
        private readonly StudyYearService _studyYearService = new();
        private readonly BranchService _branchService = new();
        private readonly EntityPageController<Section> _controller;

        public SectionsPage()
        {
            InitializeComponent();

            _controller = new EntityPageController<Section>(
                dgvRecords,
                btnAdd,
                btnUpdate,
                btnDelete,
                btnClear,
                btnRefresh,
                async () => await _service.GetAllAsync(),
                async section => await _service.AddAsync(section),
                async section => await _service.UpdateAsync(section),
                async section => await _service.DeleteAsync(section.SectionID),
                CreateEntityFromInputs,
                WriteEntityToInputs,
                ClearInputControls);

            _controller.RegisterColumn(section => section.SectionID);
            _controller.RegisterColumn(section => section.SectionName);
            _controller.RegisterColumn(section => section.StudentCount);
            _controller.RegisterColumn(section => section.StudyYear?.YearName ?? "None");
            _controller.RegisterColumn(section => section.Branch?.BranchName ?? "None");
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                await LoadLookupDataAsync();
                await _controller.RefreshDataAsync();
            }
        }

        private async Task LoadLookupDataAsync()
        {
            var studyYears = await _studyYearService.GetAllAsync();
            var branches = await _branchService.GetAllAsync();

            PageInput.BindLookup(cmbStudyYear, studyYears.Select(item => new LookupItem(item.StudyYearID, item.YearName)));
            PageInput.BindLookup(cmbBranch, branches.Select(item => new LookupItem(item.BranchID, item.BranchName)), true);
        }

        private Section CreateEntityFromInputs(Section? selectedEntity)
        {
            return new Section
            {
                SectionID = selectedEntity?.SectionID ?? 0,
                SectionName = PageInput.RequiredText(txtSectionName, "Section name"),
                StudentCount = PageInput.NonNegativeInt(txtStudentCount, "Student count"),
                StudyYearID = PageInput.RequiredComboId(cmbStudyYear, "study year"),
                BranchID = PageInput.OptionalComboId(cmbBranch)
            };
        }

        private void WriteEntityToInputs(Section entity)
        {
            txtSectionId.Text = entity.SectionID.ToString();
            txtSectionName.Text = entity.SectionName;
            txtStudentCount.Text = entity.StudentCount.ToString();
            PageInput.SetComboValue(cmbStudyYear, entity.StudyYearID);
            PageInput.SetComboValue(cmbBranch, entity.BranchID);
        }

        private void ClearInputControls()
        {
            txtSectionId.Clear();
            txtSectionName.Clear();
            txtStudentCount.Clear();
            if (cmbStudyYear.Items.Count > 0) cmbStudyYear.SelectedIndex = 0;
            if (cmbBranch.Items.Count > 0) cmbBranch.SelectedIndex = 0;
        }
    }
}
