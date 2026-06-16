using Guna.UI2.WinForms;
using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    internal sealed class FacultyMembersPage : EntityManagementPage<FacultyMember>
    {
        private readonly FacultyMemberService _service = new();
        private readonly Guna2TextBox _txtFacultyMemberId;
        private readonly Guna2TextBox _txtFullName;
        private readonly Guna2ComboBox _cmbAcademicTitle;

        public FacultyMembersPage()
            : base(
                "Faculty Members",
                "Manage teaching staff records used by schedules and assignments.",
                "Faculty Member Details",
                "Create and maintain faculty member records.")
        {
            _txtFacultyMemberId = AddTextField("Faculty ID", "Auto", true);
            _txtFullName = AddTextField("Full Name", "Enter full name");
            _cmbAcademicTitle = AddComboField("Academic Title");
            _cmbAcademicTitle.Items.AddRange(new object[]
            {
                "Professor",
                "Associate Professor",
                "Assistant Professor",
                "Lecturer",
                "Assistant Lecturer"
            });

            AddGridColumn("ID", faculty => faculty.FacultyMemberID, 30F);
            AddGridColumn("Full Name", faculty => faculty.FullName, 130F);
            AddGridColumn("Academic Title", faculty => faculty.AcademicTitle ?? "None", 90F);
        }

        protected override async Task<IReadOnlyList<FacultyMember>> LoadEntitiesAsync()
        {
            return await _service.GetAllAsync();
        }

        protected override async Task AddEntityAsync(FacultyMember entity)
        {
            await _service.AddAsync(entity);
        }

        protected override async Task UpdateEntityAsync(FacultyMember entity)
        {
            await _service.UpdateAsync(entity);
        }

        protected override async Task DeleteEntityAsync(FacultyMember entity)
        {
            await _service.DeleteAsync(entity.FacultyMemberID);
        }

        protected override FacultyMember CreateEntityFromInputs(FacultyMember? selectedEntity)
        {
            return new FacultyMember
            {
                FacultyMemberID = selectedEntity?.FacultyMemberID ?? 0,
                FullName = RequiredText(_txtFullName, "Full name"),
                AcademicTitle = string.IsNullOrWhiteSpace(_cmbAcademicTitle.Text) ? null : _cmbAcademicTitle.Text
            };
        }

        protected override void WriteEntityToInputs(FacultyMember entity)
        {
            _txtFacultyMemberId.Text = entity.FacultyMemberID.ToString();
            _txtFullName.Text = entity.FullName;
            _cmbAcademicTitle.Text = entity.AcademicTitle ?? string.Empty;
        }

        protected override void ClearInputControls()
        {
            _txtFacultyMemberId.Clear();
            _txtFullName.Clear();
            _cmbAcademicTitle.SelectedIndex = -1;
        }
    }
}
