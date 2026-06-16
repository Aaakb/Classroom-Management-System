using Guna.UI2.WinForms;
using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    internal sealed class FacultyAssignmentsPage : EntityManagementPage<FacultyMemberSubject>
    {
        private readonly FacultyMemberSubjectService _service = new();
        private readonly FacultyMemberService _facultyMemberService = new();
        private readonly SubjectService _subjectService = new();
        private readonly Guna2ComboBox _cmbFacultyMember;
        private readonly Guna2ComboBox _cmbSubject;

        public FacultyAssignmentsPage()
            : base(
                "Faculty Assignments",
                "Assign faculty members to subjects before scheduling.",
                "Assignment Details",
                "Create and remove faculty-subject assignments.")
        {
            _cmbFacultyMember = AddComboField("Faculty Member");
            _cmbSubject = AddComboField("Subject");

            AddGridColumn("Faculty Member", item => item.FacultyMember?.FullName ?? item.FacultyMemberID.ToString(), 130F);
            AddGridColumn("Subject", item => item.Subject?.SubjectName ?? item.SubjectID.ToString(), 130F);
        }

        protected override bool SupportsUpdate => false;

        protected override async Task LoadLookupDataAsync()
        {
            var facultyMembers = await _facultyMemberService.GetAllAsync();
            var subjects = await _subjectService.GetAllAsync();

            BindLookup(_cmbFacultyMember, facultyMembers.Select(item => new LookupItem(item.FacultyMemberID, item.FullName)));
            BindLookup(_cmbSubject, subjects.Select(item => new LookupItem(item.SubjectID, item.SubjectName)));
        }

        protected override async Task<IReadOnlyList<FacultyMemberSubject>> LoadEntitiesAsync()
        {
            return await _service.GetAllAsync();
        }

        protected override async Task AddEntityAsync(FacultyMemberSubject entity)
        {
            await _service.AddAsync(entity);
        }

        protected override Task UpdateEntityAsync(FacultyMemberSubject entity)
        {
            return Task.CompletedTask;
        }

        protected override async Task DeleteEntityAsync(FacultyMemberSubject entity)
        {
            await _service.DeleteAsync(entity.FacultyMemberID, entity.SubjectID);
        }

        protected override FacultyMemberSubject CreateEntityFromInputs(FacultyMemberSubject? selectedEntity)
        {
            return new FacultyMemberSubject
            {
                FacultyMemberID = RequiredComboId(_cmbFacultyMember, "faculty member"),
                SubjectID = RequiredComboId(_cmbSubject, "subject")
            };
        }

        protected override void WriteEntityToInputs(FacultyMemberSubject entity)
        {
            SetComboValue(_cmbFacultyMember, entity.FacultyMemberID);
            SetComboValue(_cmbSubject, entity.SubjectID);
        }

        protected override void ClearInputControls()
        {
            if (_cmbFacultyMember.Items.Count > 0) _cmbFacultyMember.SelectedIndex = 0;
            if (_cmbSubject.Items.Count > 0) _cmbSubject.SelectedIndex = 0;
        }
    }
}
