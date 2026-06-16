using Guna.UI2.WinForms;
using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    internal sealed class SubjectsPage : EntityManagementPage<Subject>
    {
        private readonly SubjectService _service = new();
        private readonly StudyYearService _studyYearService = new();
        private readonly BranchService _branchService = new();
        private readonly Guna2TextBox _txtSubjectId;
        private readonly Guna2TextBox _txtSubjectName;
        private readonly Guna2ComboBox _cmbStudyYear;
        private readonly Guna2ComboBox _cmbBranch;
        private readonly Guna2ComboBox _cmbSemester;
        private readonly Guna2TextBox _txtTheoreticalHours;
        private readonly Guna2TextBox _txtPracticalHours;
        private readonly Guna2TextBox _txtCreditUnits;
        private readonly Guna2ComboBox _cmbRequirementType;

        public SubjectsPage()
            : base(
                "Subjects",
                "Manage subjects, credit units, semester, and academic ownership.",
                "Subject Details",
                "Create and maintain subject records.",
                340)
        {
            _txtSubjectId = AddTextField("Subject ID", "Auto", true);
            _txtSubjectName = AddTextField("Subject Name", "Enter subject name");
            _cmbStudyYear = AddComboField("Study Year");
            _cmbBranch = AddComboField("Branch");
            _cmbSemester = AddComboField("Semester");
            _txtTheoreticalHours = AddTextField("Theoretical Hours", "0");
            _txtPracticalHours = AddTextField("Practical Hours", "0");
            _txtCreditUnits = AddTextField("Credit Units", "0");
            _cmbRequirementType = AddComboField("Requirement Type");

            BindLookup(_cmbSemester, new[]
            {
                new LookupItem(1, "Semester 1"),
                new LookupItem(2, "Semester 2")
            });

            _cmbRequirementType.Items.AddRange(new object[]
            {
                "Core",
                "Elective",
                "University Requirement",
                "College Requirement"
            });

            AddGridColumn("ID", subject => subject.SubjectID, 28F);
            AddGridColumn("Subject", subject => subject.SubjectName, 115F);
            AddGridColumn("Study Year", subject => subject.StudyYear?.YearName ?? "None", 70F);
            AddGridColumn("Semester", subject => subject.SemesterNumber, 45F);
            AddGridColumn("Branch", subject => subject.Branch?.BranchName ?? "None", 70F);
            AddGridColumn("Units", subject => subject.CreditUnits, 45F);
            AddGridColumn("Requirement", subject => subject.RequirementType, 80F);
        }

        protected override async Task LoadLookupDataAsync()
        {
            var studyYears = await _studyYearService.GetAllAsync();
            var branches = await _branchService.GetAllAsync();

            BindLookup(_cmbStudyYear, studyYears.Select(item => new LookupItem(item.StudyYearID, item.YearName)));
            BindLookup(_cmbBranch, branches.Select(item => new LookupItem(item.BranchID, item.BranchName)), true);
        }

        protected override async Task<IReadOnlyList<Subject>> LoadEntitiesAsync()
        {
            return await _service.GetAllAsync();
        }

        protected override async Task AddEntityAsync(Subject entity)
        {
            await _service.AddAsync(entity);
        }

        protected override async Task UpdateEntityAsync(Subject entity)
        {
            await _service.UpdateAsync(entity);
        }

        protected override async Task DeleteEntityAsync(Subject entity)
        {
            await _service.DeleteAsync(entity.SubjectID);
        }

        protected override Subject CreateEntityFromInputs(Subject? selectedEntity)
        {
            string requirementType = string.IsNullOrWhiteSpace(_cmbRequirementType.Text)
                ? throw new ArgumentException("Requirement type is required.")
                : _cmbRequirementType.Text;

            return new Subject
            {
                SubjectID = selectedEntity?.SubjectID ?? 0,
                SubjectName = RequiredText(_txtSubjectName, "Subject name"),
                StudyYearID = RequiredComboId(_cmbStudyYear, "study year"),
                BranchID = OptionalComboId(_cmbBranch),
                SemesterNumber = RequiredComboId(_cmbSemester, "semester"),
                TheoreticalHours = NonNegativeDouble(_txtTheoreticalHours, "Theoretical hours"),
                PracticalHours = NonNegativeDouble(_txtPracticalHours, "Practical hours"),
                CreditUnits = NonNegativeDouble(_txtCreditUnits, "Credit units"),
                RequirementType = requirementType
            };
        }

        protected override void WriteEntityToInputs(Subject entity)
        {
            _txtSubjectId.Text = entity.SubjectID.ToString();
            _txtSubjectName.Text = entity.SubjectName;
            SetComboValue(_cmbStudyYear, entity.StudyYearID);
            SetComboValue(_cmbBranch, entity.BranchID);
            SetComboValue(_cmbSemester, entity.SemesterNumber);
            _txtTheoreticalHours.Text = entity.TheoreticalHours.ToString();
            _txtPracticalHours.Text = entity.PracticalHours.ToString();
            _txtCreditUnits.Text = entity.CreditUnits.ToString();
            _cmbRequirementType.Text = entity.RequirementType;
        }

        protected override void ClearInputControls()
        {
            _txtSubjectId.Clear();
            _txtSubjectName.Clear();
            _txtTheoreticalHours.Text = "0";
            _txtPracticalHours.Text = "0";
            _txtCreditUnits.Text = "0";
            if (_cmbStudyYear.Items.Count > 0) _cmbStudyYear.SelectedIndex = 0;
            if (_cmbBranch.Items.Count > 0) _cmbBranch.SelectedIndex = 0;
            if (_cmbSemester.Items.Count > 0) _cmbSemester.SelectedIndex = 0;
            _cmbRequirementType.SelectedIndex = -1;
        }
    }
}
