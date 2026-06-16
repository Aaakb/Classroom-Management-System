namespace University_Timetable_and_Classroom_Management_System
{
    internal static class FormNavigation
    {
        public static void Open(System.Windows.Forms.Form currentForm, System.Windows.Forms.Form nextForm)
        {
            if (currentForm.GetType() == nextForm.GetType())
            {
                nextForm.Dispose();
                return;
            }

            nextForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            nextForm.Show();
            currentForm.Hide();
            nextForm.FormClosed += (_, _) => currentForm.Close();
        }

        public static void Configure(
            System.Windows.Forms.Form currentForm,
            Guna.UI2.WinForms.Guna2Button? currentButton = null,
            Guna.UI2.WinForms.Guna2Button? dashboard = null,
            Guna.UI2.WinForms.Guna2Button? branches = null,
            Guna.UI2.WinForms.Guna2Button? studyYears = null,
            Guna.UI2.WinForms.Guna2Button? sections = null,
            Guna.UI2.WinForms.Guna2Button? subjects = null,
            Guna.UI2.WinForms.Guna2Button? classrooms = null,
            Guna.UI2.WinForms.Guna2Button? timeSlots = null,
            Guna.UI2.WinForms.Guna2Button? facultyAssignments = null,
            Guna.UI2.WinForms.Guna2Button? facultyMembers = null,
            Guna.UI2.WinForms.Guna2Button? schedules = null)
        {
            currentButton?.SetEnabled(false);
            dashboard?.SetEnabled(false);

            Wire(branches, currentForm, static () => new BranchesForm());
            Wire(studyYears, currentForm, static () => new StudyYearsForm());
            Wire(sections, currentForm, static () => new SectionsForm());
            Wire(subjects, currentForm, static () => new SubjectsForm());
            Wire(classrooms, currentForm, static () => new ClassroomsForm());
            Wire(timeSlots, currentForm, static () => new TimeSlotsForm());
            Wire(facultyAssignments, currentForm, static () => new FacultyMemberSubjectsForm());
            Wire(facultyMembers, currentForm, static () => new FacultyMembersForm());
            Wire(schedules, currentForm, static () => new SchedulesForm());
        }

        private static void Wire(
            Guna.UI2.WinForms.Guna2Button? button,
            System.Windows.Forms.Form currentForm,
            Func<System.Windows.Forms.Form> createForm)
        {
            if (button is null || !button.Enabled)
            {
                return;
            }

            button.Click += (_, _) => Open(currentForm, createForm());
        }
    }

    internal static class NavigationButtonExtensions
    {
        public static void SetEnabled(this Guna.UI2.WinForms.Guna2Button button, bool enabled)
        {
            button.Enabled = enabled;
            button.Cursor = enabled
                ? System.Windows.Forms.Cursors.Hand
                : System.Windows.Forms.Cursors.Default;
        }
    }
}
