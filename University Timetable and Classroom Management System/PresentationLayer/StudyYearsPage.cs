using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class StudyYearsPage : UserControl
    {
        private readonly StudyYearService _service = new();
        private readonly EntityPageController<StudyYear> _controller;

        public StudyYearsPage()
        {
            InitializeComponent();

            _controller = new EntityPageController<StudyYear>(
                dgvRecords,
                btnAdd,
                btnUpdate,
                btnDelete,
                btnClear,
                btnRefresh,
                async () => await _service.GetAllAsync(),
                async studyYear => await _service.AddAsync(studyYear),
                async studyYear => await _service.UpdateAsync(studyYear),
                async studyYear => await _service.DeleteAsync(studyYear.StudyYearID),
                CreateEntityFromInputs,
                WriteEntityToInputs,
                ClearInputControls);

            _controller.RegisterColumn(studyYear => studyYear.StudyYearID);
            _controller.RegisterColumn(studyYear => studyYear.YearName);
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                await _controller.RefreshDataAsync();
            }
        }

        private StudyYear CreateEntityFromInputs(StudyYear? selectedEntity)
        {
            return new StudyYear
            {
                StudyYearID = selectedEntity?.StudyYearID ?? 0,
                YearName = PageInput.RequiredText(txtYearName, "Year name")
            };
        }

        private void WriteEntityToInputs(StudyYear entity)
        {
            txtStudyYearId.Text = entity.StudyYearID.ToString();
            txtYearName.Text = entity.YearName;
        }

        private void ClearInputControls()
        {
            txtStudyYearId.Clear();
            txtYearName.Clear();
        }
    }
}
