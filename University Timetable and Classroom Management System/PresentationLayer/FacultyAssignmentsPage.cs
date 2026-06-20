using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class FacultyAssignmentsPage : UserControl
    {
        private readonly FacultyMemberSubjectService _service = new();
        private readonly FacultyMemberService _facultyMemberService = new();
        private readonly SubjectService _subjectService = new();
        private readonly EntityPageController<FacultyMemberSubject> _controller;

        public FacultyAssignmentsPage()
        {
            InitializeComponent();

            _controller = new EntityPageController<FacultyMemberSubject>(
                dgvRecords,
                btnAdd,
                btnUpdate,
                btnDelete,
                btnClear,
                btnRefresh,
                async () => await _service.GetAllAsync(),
                async assignment => await _service.AddAsync(assignment),
                _ => Task.CompletedTask,
                async assignment => await _service.DeleteAsync(assignment.FacultyMemberID, assignment.SubjectID),
                CreateEntityFromInputs,
                WriteEntityToInputs,
                ClearInputControls,
                supportsUpdate: false);

            _controller.RegisterColumn(assignment => assignment.FacultyMember?.FullName ?? assignment.FacultyMemberID.ToString());
            _controller.RegisterColumn(assignment => assignment.Subject?.SubjectName ?? assignment.SubjectID.ToString());
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
            var facultyMembers = await _facultyMemberService.GetAllAsync();
            var subjects = await _subjectService.GetAllAsync();

            PageInput.BindLookup(cmbFacultyMember, facultyMembers.Select(item => new LookupItem(item.FacultyMemberID, item.FullName)));
            PageInput.BindLookup(cmbSubject, subjects.Select(item => new LookupItem(item.SubjectID, item.SubjectName)));
        }

        private FacultyMemberSubject CreateEntityFromInputs(FacultyMemberSubject? selectedEntity)
        {
            return new FacultyMemberSubject
            {
                FacultyMemberID = PageInput.RequiredComboId(cmbFacultyMember, "faculty member"),
                SubjectID = PageInput.RequiredComboId(cmbSubject, "subject")
            };
        }

        private void WriteEntityToInputs(FacultyMemberSubject entity)
        {
            PageInput.SetComboValue(cmbFacultyMember, entity.FacultyMemberID);
            PageInput.SetComboValue(cmbSubject, entity.SubjectID);
        }

        private void ClearInputControls()
        {
            if (cmbFacultyMember.Items.Count > 0) cmbFacultyMember.SelectedIndex = 0;
            if (cmbSubject.Items.Count > 0) cmbSubject.SelectedIndex = 0;
        }
    }
}
