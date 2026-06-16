using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SubjectsPage : UserControl
    {
        private readonly SubjectService _service = new();
        private readonly StudyYearService _studyYearService = new();
        private readonly BranchService _branchService = new();
        private readonly EntityPageController<Subject> _controller;

        public SubjectsPage()
        {
            InitializeComponent();

            _controller = new EntityPageController<Subject>(
                dgvRecords,
                btnAdd,
                btnUpdate,
                btnDelete,
                btnClear,
                btnRefresh,
                async () => await _service.GetAllAsync(),
                async subject => await _service.AddAsync(subject),
                async subject => await _service.UpdateAsync(subject),
                async subject => await _service.DeleteAsync(subject.SubjectID),
                CreateEntityFromInputs,
                WriteEntityToInputs,
                ClearInputControls);

            _controller.RegisterColumn(subject => subject.SubjectID);
            _controller.RegisterColumn(subject => subject.SubjectName);
            _controller.RegisterColumn(subject => subject.StudyYear?.YearName ?? "None");
            _controller.RegisterColumn(subject => subject.SemesterNumber);
            _controller.RegisterColumn(subject => subject.Branch?.BranchName ?? "None");
            _controller.RegisterColumn(subject => subject.CreditUnits);
            _controller.RegisterColumn(subject => subject.RequirementType);
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
            PageInput.BindLookup(cmbSemester, new[]
            {
                new LookupItem(1, "Semester 1"),
                new LookupItem(2, "Semester 2")
            });
        }

        private Subject CreateEntityFromInputs(Subject? selectedEntity)
        {
            string requirementType = string.IsNullOrWhiteSpace(cmbRequirementType.Text)
                ? throw new ArgumentException("Requirement type is required.")
                : cmbRequirementType.Text;

            return new Subject
            {
                SubjectID = selectedEntity?.SubjectID ?? 0,
                SubjectName = PageInput.RequiredText(txtSubjectName, "Subject name"),
                StudyYearID = PageInput.RequiredComboId(cmbStudyYear, "study year"),
                BranchID = PageInput.OptionalComboId(cmbBranch),
                SemesterNumber = PageInput.RequiredComboId(cmbSemester, "semester"),
                TheoreticalHours = PageInput.NonNegativeDouble(txtTheoreticalHours, "Theoretical hours"),
                PracticalHours = PageInput.NonNegativeDouble(txtPracticalHours, "Practical hours"),
                CreditUnits = PageInput.NonNegativeDouble(txtCreditUnits, "Credit units"),
                RequirementType = requirementType
            };
        }

        private void WriteEntityToInputs(Subject entity)
        {
            txtSubjectId.Text = entity.SubjectID.ToString();
            txtSubjectName.Text = entity.SubjectName;
            PageInput.SetComboValue(cmbStudyYear, entity.StudyYearID);
            PageInput.SetComboValue(cmbBranch, entity.BranchID);
            PageInput.SetComboValue(cmbSemester, entity.SemesterNumber);
            txtTheoreticalHours.Text = entity.TheoreticalHours.ToString();
            txtPracticalHours.Text = entity.PracticalHours.ToString();
            txtCreditUnits.Text = entity.CreditUnits.ToString();
            cmbRequirementType.Text = entity.RequirementType;
        }

        private void ClearInputControls()
        {
            txtSubjectId.Clear();
            txtSubjectName.Clear();
            txtTheoreticalHours.Text = "0";
            txtPracticalHours.Text = "0";
            txtCreditUnits.Text = "0";
            if (cmbStudyYear.Items.Count > 0) cmbStudyYear.SelectedIndex = 0;
            if (cmbBranch.Items.Count > 0) cmbBranch.SelectedIndex = 0;
            if (cmbSemester.Items.Count > 0) cmbSemester.SelectedIndex = 0;
            cmbRequirementType.SelectedIndex = -1;
        }
    }
}
