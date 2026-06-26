namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SchedulesForm
    {
        private async Task RefreshSchedulesAsync()
        {
            SetScheduleActionsEnabled(false);

            try
            {
                await LoadLookupsAsync();
                await LoadSchedulesAsync();
                ClearScheduleForm();
            }
            catch (Exception ex)
            {
                ShowError("Unable to refresh schedule data.", ex);
            }
            finally
            {
                SetScheduleActionsEnabled(true);
            }
        }

        private async Task LoadLookupsAsync()
        {
            subjectsLookup = await subjectService.GetAllAsync();
            facultyMembersLookup = await facultyMemberService.GetAllAsync();
            facultySubjectAssignmentsLookup = await facultyMemberSubjectService.GetAllAsync();
            var classrooms = await classroomService.GetAllAsync();
            var timeSlots = await timeSlotService.GetAllAsync();
            studyYearsLookup = await studyYearService.GetAllAsync();
            branchesLookup = await branchService.GetAllAsync();
            sectionsLookup = await sectionService.GetAllAsync();

            BindSubjectsCombo();
            BindFacultyMembersForSubject(null);
            BindCombo(cmbClassroom, classrooms.Select(classroom => new ComboOption(classroom.ClassroomID, classroom.ClassroomNumber)));
            BindCombo(cmbTimeSlot, timeSlots.Select(slot => new ComboOption(slot.TimeSlotID, FormatTimeSlot(slot))));
            BindCombo(cmbStudyYear, studyYearsLookup.Select(studyYear => new ComboOption(studyYear.StudyYearID, studyYear.YearName)));
            BindCombo(cmbBranch, branchesLookup.Select(branch => new ComboOption(branch.BranchID, branch.BranchName)));
            BindSectionsCombo();

            BindFilterCombo(cmbFacultyFilter, facultyMembersLookup.Select(faculty => new ComboOption(faculty.FacultyMemberID, faculty.FullName)), "All faculty");
            BindFilterCombo(cmbStudyYearFilter, studyYearsLookup.Select(studyYear => new ComboOption(studyYear.StudyYearID, studyYear.YearName)), "All study years");
            BindSectionFilterCombo();
            BindDayCombos();
            BindSemesterFilterCombo();
            BindGroupFilterCombo();
            BindGroupNameCombo();
        }

        private async Task LoadSchedulesAsync()
        {
            var schedules = await scheduleService.GetScheduleDetailsAsync();
            scheduleRows = schedules
                .Select(ScheduleRow.FromDetails)
                .OrderBy(row => StudyYearOrder(row.StudyYearName))
                .ThenBy(row => row.BranchName)
                .ThenBy(row => row.SectionName)
                .ThenBy(row => row.SemesterNumber)
                .ThenBy(row => DayOrder(row.DayOfWeek))
                .ThenBy(row => row.StartTime)
                .ToList();

            ApplyScheduleFilters();
        }
    }
}
