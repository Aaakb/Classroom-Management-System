using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class FacultyMembersPage : UserControl
    {
        private readonly FacultyMemberService _service = new();
        private readonly EntityPageController<FacultyMember> _controller;

        public FacultyMembersPage()
        {
            InitializeComponent();

            _controller = new EntityPageController<FacultyMember>(
                dgvRecords,
                btnAdd,
                btnUpdate,
                btnDelete,
                btnClear,
                btnRefresh,
                async () => await _service.GetAllAsync(),
                async facultyMember => await _service.AddAsync(facultyMember),
                async facultyMember => await _service.UpdateAsync(facultyMember),
                async facultyMember => await _service.DeleteAsync(facultyMember.FacultyMemberID),
                CreateEntityFromInputs,
                WriteEntityToInputs,
                ClearInputControls);

            _controller.RegisterColumn(facultyMember => facultyMember.FacultyMemberID);
            _controller.RegisterColumn(facultyMember => facultyMember.FullName);
            _controller.RegisterColumn(facultyMember => facultyMember.AcademicTitle ?? "None");
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                await _controller.RefreshDataAsync();
            }
        }

        private FacultyMember CreateEntityFromInputs(FacultyMember? selectedEntity)
        {
            return new FacultyMember
            {
                FacultyMemberID = selectedEntity?.FacultyMemberID ?? 0,
                FullName = PageInput.RequiredText(txtFullName, "Full name"),
                AcademicTitle = string.IsNullOrWhiteSpace(cmbAcademicTitle.Text) ? null : cmbAcademicTitle.Text
            };
        }

        private void WriteEntityToInputs(FacultyMember entity)
        {
            txtFacultyMemberId.Text = entity.FacultyMemberID.ToString();
            txtFullName.Text = entity.FullName;
            cmbAcademicTitle.Text = entity.AcademicTitle ?? string.Empty;
        }

        private void ClearInputControls()
        {
            txtFacultyMemberId.Clear();
            txtFullName.Clear();
            cmbAcademicTitle.SelectedIndex = -1;
        }
    }
}
