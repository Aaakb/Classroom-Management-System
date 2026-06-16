using Guna.UI2.WinForms;
using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    internal sealed class StudyYearsPage : EntityManagementPage<StudyYear>
    {
        private readonly StudyYearService _service = new();
        private readonly Guna2TextBox _txtStudyYearId;
        private readonly Guna2TextBox _txtYearName;

        public StudyYearsPage()
            : base(
                "Study Years",
                "Manage academic year records used across the timetable.",
                "Study Year Details",
                "Create and maintain study year records.")
        {
            _txtStudyYearId = AddTextField("Study Year ID", "Auto", true);
            _txtYearName = AddTextField("Year Name", "Enter year name");

            AddGridColumn("ID", studyYear => studyYear.StudyYearID, 30F);
            AddGridColumn("Year Name", studyYear => studyYear.YearName, 170F);
        }

        protected override async Task<IReadOnlyList<StudyYear>> LoadEntitiesAsync()
        {
            return await _service.GetAllAsync();
        }

        protected override async Task AddEntityAsync(StudyYear entity)
        {
            await _service.AddAsync(entity);
        }

        protected override async Task UpdateEntityAsync(StudyYear entity)
        {
            await _service.UpdateAsync(entity);
        }

        protected override async Task DeleteEntityAsync(StudyYear entity)
        {
            await _service.DeleteAsync(entity.StudyYearID);
        }

        protected override StudyYear CreateEntityFromInputs(StudyYear? selectedEntity)
        {
            return new StudyYear
            {
                StudyYearID = selectedEntity?.StudyYearID ?? 0,
                YearName = RequiredText(_txtYearName, "Year name")
            };
        }

        protected override void WriteEntityToInputs(StudyYear entity)
        {
            _txtStudyYearId.Text = entity.StudyYearID.ToString();
            _txtYearName.Text = entity.YearName;
        }

        protected override void ClearInputControls()
        {
            _txtStudyYearId.Clear();
            _txtYearName.Clear();
        }
    }
}
