using Guna.UI2.WinForms;
using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    internal sealed class SchedulesPage : EntityManagementPage<Schedule>
    {
        private readonly ScheduleService _service = new();
        private readonly SubjectService _subjectService = new();
        private readonly FacultyMemberService _facultyMemberService = new();
        private readonly ClassroomService _classroomService = new();
        private readonly TimeSlotService _timeSlotService = new();
        private readonly StudyYearService _studyYearService = new();
        private readonly BranchService _branchService = new();
        private readonly SectionService _sectionService = new();
        private readonly Guna2TextBox _txtScheduleId;
        private readonly Guna2ComboBox _cmbDayOfWeek;
        private readonly Guna2ComboBox _cmbSubject;
        private readonly Guna2ComboBox _cmbFacultyMember;
        private readonly Guna2ComboBox _cmbClassroom;
        private readonly Guna2ComboBox _cmbTimeSlot;
        private readonly Guna2ComboBox _cmbStudyYear;
        private readonly Guna2ComboBox _cmbBranch;
        private readonly Guna2ComboBox _cmbSection;

        public SchedulesPage()
            : base(
                "Schedules",
                "Create timetable entries while respecting classroom, faculty, and cohort conflicts.",
                "Schedule Details",
                "Create and maintain timetable records.",
                340)
        {
            _txtScheduleId = AddTextField("Schedule ID", "Auto", true);
            _cmbDayOfWeek = AddComboField("Day Of Week");
            _cmbSubject = AddComboField("Subject");
            _cmbFacultyMember = AddComboField("Faculty Member");
            _cmbClassroom = AddComboField("Classroom");
            _cmbTimeSlot = AddComboField("Time Slot");
            _cmbStudyYear = AddComboField("Study Year");
            _cmbBranch = AddComboField("Branch");
            _cmbSection = AddComboField("Section");

            _cmbDayOfWeek.Items.AddRange(new object[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday" });

            AddGridColumn("ID", schedule => schedule.ScheduleID, 28F);
            AddGridColumn("Day", schedule => schedule.DayOfWeek, 55F);
            AddGridColumn("Subject", schedule => schedule.Subject?.SubjectName ?? schedule.SubjectID.ToString(), 100F);
            AddGridColumn("Faculty", schedule => schedule.FacultyMember?.FullName ?? schedule.FacultyMemberID.ToString(), 110F);
            AddGridColumn("Room", schedule => schedule.Classroom?.ClassroomNumber ?? schedule.ClassroomID.ToString(), 55F);
            AddGridColumn("Time", schedule => FormatTimeSlot(schedule.TimeSlot), 70F);
            AddGridColumn("Year", schedule => schedule.StudyYear?.YearName ?? "None", 65F);
            AddGridColumn("Branch", schedule => schedule.Branch?.BranchName ?? "None", 70F);
            AddGridColumn("Section", schedule => schedule.Section?.SectionName ?? "None", 65F);
        }

        protected override async Task LoadLookupDataAsync()
        {
            var subjects = await _subjectService.GetAllAsync();
            var facultyMembers = await _facultyMemberService.GetAllAsync();
            var classrooms = await _classroomService.GetAllAsync();
            var timeSlots = await _timeSlotService.GetAllAsync();
            var studyYears = await _studyYearService.GetAllAsync();
            var branches = await _branchService.GetAllAsync();
            var sections = await _sectionService.GetAllAsync();

            BindLookup(_cmbSubject, subjects.Select(item => new LookupItem(item.SubjectID, item.SubjectName)));
            BindLookup(_cmbFacultyMember, facultyMembers.Select(item => new LookupItem(item.FacultyMemberID, item.FullName)));
            BindLookup(_cmbClassroom, classrooms.Select(item => new LookupItem(item.ClassroomID, item.ClassroomNumber)));
            BindLookup(_cmbTimeSlot, timeSlots.Select(item => new LookupItem(item.TimeSlotID, FormatTimeSlot(item))));
            BindLookup(_cmbStudyYear, studyYears.Select(item => new LookupItem(item.StudyYearID, item.YearName)), true);
            BindLookup(_cmbBranch, branches.Select(item => new LookupItem(item.BranchID, item.BranchName)), true);
            BindLookup(_cmbSection, sections.Select(item => new LookupItem(item.SectionID, item.SectionName)), true);
        }

        protected override async Task<IReadOnlyList<Schedule>> LoadEntitiesAsync()
        {
            return await _service.GetAllAsync();
        }

        protected override async Task AddEntityAsync(Schedule entity)
        {
            await _service.AddAsync(entity);
        }

        protected override async Task UpdateEntityAsync(Schedule entity)
        {
            await _service.UpdateAsync(entity);
        }

        protected override async Task DeleteEntityAsync(Schedule entity)
        {
            await _service.DeleteAsync(entity.ScheduleID);
        }

        protected override Schedule CreateEntityFromInputs(Schedule? selectedEntity)
        {
            string dayOfWeek = string.IsNullOrWhiteSpace(_cmbDayOfWeek.Text)
                ? throw new ArgumentException("Day of week is required.")
                : _cmbDayOfWeek.Text;

            return new Schedule
            {
                ScheduleID = selectedEntity?.ScheduleID ?? 0,
                DayOfWeek = dayOfWeek,
                SubjectID = RequiredComboId(_cmbSubject, "subject"),
                FacultyMemberID = RequiredComboId(_cmbFacultyMember, "faculty member"),
                ClassroomID = RequiredComboId(_cmbClassroom, "classroom"),
                TimeSlotID = RequiredComboId(_cmbTimeSlot, "time slot"),
                StudyYearID = OptionalComboId(_cmbStudyYear),
                BranchID = OptionalComboId(_cmbBranch),
                SectionID = OptionalComboId(_cmbSection)
            };
        }

        protected override void WriteEntityToInputs(Schedule entity)
        {
            _txtScheduleId.Text = entity.ScheduleID.ToString();
            _cmbDayOfWeek.Text = entity.DayOfWeek;
            SetComboValue(_cmbSubject, entity.SubjectID);
            SetComboValue(_cmbFacultyMember, entity.FacultyMemberID);
            SetComboValue(_cmbClassroom, entity.ClassroomID);
            SetComboValue(_cmbTimeSlot, entity.TimeSlotID);
            SetComboValue(_cmbStudyYear, entity.StudyYearID);
            SetComboValue(_cmbBranch, entity.BranchID);
            SetComboValue(_cmbSection, entity.SectionID);
        }

        protected override void ClearInputControls()
        {
            _txtScheduleId.Clear();
            if (_cmbDayOfWeek.Items.Count > 0) _cmbDayOfWeek.SelectedIndex = 0;
            if (_cmbSubject.Items.Count > 0) _cmbSubject.SelectedIndex = 0;
            if (_cmbFacultyMember.Items.Count > 0) _cmbFacultyMember.SelectedIndex = 0;
            if (_cmbClassroom.Items.Count > 0) _cmbClassroom.SelectedIndex = 0;
            if (_cmbTimeSlot.Items.Count > 0) _cmbTimeSlot.SelectedIndex = 0;
            if (_cmbStudyYear.Items.Count > 0) _cmbStudyYear.SelectedIndex = 0;
            if (_cmbBranch.Items.Count > 0) _cmbBranch.SelectedIndex = 0;
            if (_cmbSection.Items.Count > 0) _cmbSection.SelectedIndex = 0;
        }

        private static string FormatTimeSlot(TimeSlot? timeSlot)
        {
            return timeSlot is null
                ? "None"
                : $"{timeSlot.StartTime:hh\\:mm} - {timeSlot.EndTime:hh\\:mm}";
        }
    }
}
