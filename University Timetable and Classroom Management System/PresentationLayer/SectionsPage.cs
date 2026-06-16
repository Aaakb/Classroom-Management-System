using Guna.UI2.WinForms;
using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    internal sealed class SectionsPage : EntityManagementPage<Section>
    {
        private readonly SectionService _service = new();
        private readonly StudyYearService _studyYearService = new();
        private readonly BranchService _branchService = new();
        private readonly Guna2TextBox _txtSectionId;
        private readonly Guna2TextBox _txtSectionName;
        private readonly Guna2TextBox _txtStudentCount;
        private readonly Guna2ComboBox _cmbStudyYear;
        private readonly Guna2ComboBox _cmbBranch;

        public SectionsPage()
            : base(
                "Sections",
                "Manage student sections by study year and branch.",
                "Section Details",
                "Create and maintain section records.",
                260)
        {
            _txtSectionId = AddTextField("Section ID", "Auto", true);
            _txtSectionName = AddTextField("Section Name", "Enter section name");
            _txtStudentCount = AddTextField("Student Count", "Enter count");
            _cmbStudyYear = AddComboField("Study Year");
            _cmbBranch = AddComboField("Branch");

            AddGridColumn("ID", section => section.SectionID, 30F);
            AddGridColumn("Section", section => section.SectionName, 80F);
            AddGridColumn("Students", section => section.StudentCount, 60F);
            AddGridColumn("Study Year", section => section.StudyYear?.YearName ?? "None", 90F);
            AddGridColumn("Branch", section => section.Branch?.BranchName ?? "None", 90F);
        }

        protected override async Task LoadLookupDataAsync()
        {
            var studyYears = await _studyYearService.GetAllAsync();
            var branches = await _branchService.GetAllAsync();

            BindLookup(_cmbStudyYear, studyYears.Select(item => new LookupItem(item.StudyYearID, item.YearName)));
            BindLookup(_cmbBranch, branches.Select(item => new LookupItem(item.BranchID, item.BranchName)), true);
        }

        protected override async Task<IReadOnlyList<Section>> LoadEntitiesAsync()
        {
            return await _service.GetAllAsync();
        }

        protected override async Task AddEntityAsync(Section entity)
        {
            await _service.AddAsync(entity);
        }

        protected override async Task UpdateEntityAsync(Section entity)
        {
            await _service.UpdateAsync(entity);
        }

        protected override async Task DeleteEntityAsync(Section entity)
        {
            await _service.DeleteAsync(entity.SectionID);
        }

        protected override Section CreateEntityFromInputs(Section? selectedEntity)
        {
            return new Section
            {
                SectionID = selectedEntity?.SectionID ?? 0,
                SectionName = RequiredText(_txtSectionName, "Section name"),
                StudentCount = NonNegativeInt(_txtStudentCount, "Student count"),
                StudyYearID = RequiredComboId(_cmbStudyYear, "study year"),
                BranchID = OptionalComboId(_cmbBranch)
            };
        }

        protected override void WriteEntityToInputs(Section entity)
        {
            _txtSectionId.Text = entity.SectionID.ToString();
            _txtSectionName.Text = entity.SectionName;
            _txtStudentCount.Text = entity.StudentCount.ToString();
            SetComboValue(_cmbStudyYear, entity.StudyYearID);
            SetComboValue(_cmbBranch, entity.BranchID);
        }

        protected override void ClearInputControls()
        {
            _txtSectionId.Clear();
            _txtSectionName.Clear();
            _txtStudentCount.Clear();
            if (_cmbStudyYear.Items.Count > 0) _cmbStudyYear.SelectedIndex = 0;
            if (_cmbBranch.Items.Count > 0) _cmbBranch.SelectedIndex = 0;
        }
    }
}
