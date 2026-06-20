using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SchedulesPage : UserControl
    {
        private readonly ScheduleService _service = new();
        private readonly SubjectService _subjectService = new();
        private readonly FacultyMemberService _facultyMemberService = new();
        private readonly ClassroomService _classroomService = new();
        private readonly TimeSlotService _timeSlotService = new();
        private readonly StudyYearService _studyYearService = new();
        private readonly BranchService _branchService = new();
        private readonly SectionService _sectionService = new();
        private readonly EntityPageController<Schedule> _controller;

        public SchedulesPage()
        {
            InitializeComponent();

            _controller = new EntityPageController<Schedule>(
                dgvRecords,
                btnAdd,
                btnUpdate,
                btnDelete,
                btnClear,
                btnRefresh,
                async () => await _service.GetAllAsync(),
                async schedule => await _service.AddAsync(schedule),
                async schedule => await _service.UpdateAsync(schedule),
                async schedule => await _service.DeleteAsync(schedule.ScheduleID),
                CreateEntityFromInputs,
                WriteEntityToInputs,
                ClearInputControls);

            _controller.RegisterColumn(schedule => schedule.ScheduleID);
            _controller.RegisterColumn(schedule => schedule.DayOfWeek);
            _controller.RegisterColumn(schedule => schedule.Subject?.SubjectName ?? schedule.SubjectID.ToString());
            _controller.RegisterColumn(schedule => schedule.FacultyMember?.FullName ?? schedule.FacultyMemberID.ToString());
            _controller.RegisterColumn(schedule => schedule.Classroom?.ClassroomNumber ?? schedule.ClassroomID.ToString());
            _controller.RegisterColumn(schedule => FormatTimeSlot(schedule.TimeSlot));
            _controller.RegisterColumn(schedule => schedule.StudyYear?.YearName ?? "None");
            _controller.RegisterColumn(schedule => schedule.Branch?.BranchName ?? "None");
            _controller.RegisterColumn(schedule => schedule.Section?.SectionName ?? "None");

            btnGenerate.Click += async (_, _) => await GenerateScheduleAsync();
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
            var subjects = await _subjectService.GetAllAsync();
            var facultyMembers = await _facultyMemberService.GetAllAsync();
            var classrooms = await _classroomService.GetAllAsync();
            var timeSlots = await _timeSlotService.GetAllAsync();
            var studyYears = await _studyYearService.GetAllAsync();
            var branches = await _branchService.GetAllAsync();
            var sections = await _sectionService.GetAllAsync();

            PageInput.BindLookup(cmbSubject, subjects.Select(item => new LookupItem(item.SubjectID, item.SubjectName)));
            PageInput.BindLookup(cmbFacultyMember, facultyMembers.Select(item => new LookupItem(item.FacultyMemberID, item.FullName)));
            PageInput.BindLookup(cmbClassroom, classrooms.Select(item => new LookupItem(item.ClassroomID, item.ClassroomNumber)));
            PageInput.BindLookup(cmbTimeSlot, timeSlots.Select(item => new LookupItem(item.TimeSlotID, FormatTimeSlot(item))));
            PageInput.BindLookup(cmbStudyYear, studyYears.Select(item => new LookupItem(item.StudyYearID, item.YearName)), true);
            PageInput.BindLookup(cmbBranch, branches.Select(item => new LookupItem(item.BranchID, item.BranchName)), true);
            PageInput.BindLookup(cmbSection, sections.Select(item => new LookupItem(item.SectionID, item.SectionName)), true);
        }

        private Schedule CreateEntityFromInputs(Schedule? selectedEntity)
        {
            string dayOfWeek = string.IsNullOrWhiteSpace(cmbDayOfWeek.Text)
                ? throw new ArgumentException("Day of week is required.")
                : cmbDayOfWeek.Text;

            return new Schedule
            {
                ScheduleID = selectedEntity?.ScheduleID ?? 0,
                DayOfWeek = dayOfWeek,
                SubjectID = PageInput.RequiredComboId(cmbSubject, "subject"),
                FacultyMemberID = PageInput.RequiredComboId(cmbFacultyMember, "faculty member"),
                ClassroomID = PageInput.RequiredComboId(cmbClassroom, "classroom"),
                TimeSlotID = PageInput.RequiredComboId(cmbTimeSlot, "time slot"),
                StudyYearID = PageInput.OptionalComboId(cmbStudyYear),
                BranchID = PageInput.OptionalComboId(cmbBranch),
                SectionID = PageInput.OptionalComboId(cmbSection)
            };
        }

        private void WriteEntityToInputs(Schedule entity)
        {
            txtScheduleId.Text = entity.ScheduleID.ToString();
            cmbDayOfWeek.Text = entity.DayOfWeek;
            PageInput.SetComboValue(cmbSubject, entity.SubjectID);
            PageInput.SetComboValue(cmbFacultyMember, entity.FacultyMemberID);
            PageInput.SetComboValue(cmbClassroom, entity.ClassroomID);
            PageInput.SetComboValue(cmbTimeSlot, entity.TimeSlotID);
            PageInput.SetComboValue(cmbStudyYear, entity.StudyYearID);
            PageInput.SetComboValue(cmbBranch, entity.BranchID);
            PageInput.SetComboValue(cmbSection, entity.SectionID);
        }

        private void ClearInputControls()
        {
            txtScheduleId.Clear();
            if (cmbDayOfWeek.Items.Count > 0) cmbDayOfWeek.SelectedIndex = 0;
            if (cmbSubject.Items.Count > 0) cmbSubject.SelectedIndex = 0;
            if (cmbFacultyMember.Items.Count > 0) cmbFacultyMember.SelectedIndex = 0;
            if (cmbClassroom.Items.Count > 0) cmbClassroom.SelectedIndex = 0;
            if (cmbTimeSlot.Items.Count > 0) cmbTimeSlot.SelectedIndex = 0;
            if (cmbStudyYear.Items.Count > 0) cmbStudyYear.SelectedIndex = 0;
            if (cmbBranch.Items.Count > 0) cmbBranch.SelectedIndex = 0;
            if (cmbSection.Items.Count > 0) cmbSection.SelectedIndex = 0;
        }

        private static string FormatTimeSlot(TimeSlot? timeSlot)
        {
            return timeSlot is null
                ? "None"
                : $"{timeSlot.StartTime:hh\\:mm} - {timeSlot.EndTime:hh\\:mm}";
        }

        private async Task GenerateScheduleAsync()
        {
            btnGenerate.Enabled = false;

            try
            {
                var result = await _service.GenerateAsync();
                await _controller.RefreshDataAsync();
                MessageBox.Show(BuildGenerationMessage(result), "Schedule", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Schedule Generation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGenerate.Enabled = true;
            }
        }

        private static string BuildGenerationMessage(ScheduleGenerationResult result)
        {
            return
                $"Generation completed. Created: {result.CreatedCount}, Skipped: {result.SkippedCount}." +
                Environment.NewLine +
                $"Required sessions: {result.RequiredCount}." +
                Environment.NewLine +
                $"Unassigned subjects: {result.UnassignedSubjectsCount}, Missing sections: {result.MissingSectionCount}, No classroom: {result.NoClassroomCount}, Conflicts: {result.ConflictCount}." +
                Environment.NewLine +
                $"Available time slots: {result.TimeSlotCount}, Classrooms: {result.ClassroomCount}, Sections: {result.SectionCount}.";
        }
    }
}
