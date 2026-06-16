namespace University_Timetable_and_Classroom_Management_System
{
    public partial class FacultyMembersForm
    {
        public FacultyMembersForm()
        {
            InitializeComponent();
            ConfigureNavigation();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.Configure(
                this,
                currentButton: btnNavigationFaculty,
                dashboard: btnNavigationDashboard,
                branches: btnNavigationBranches,
                studyYears: btnNavigationStudyYears,
                classrooms: btnNavigationClassrooms,
                facultyMembers: btnNavigationFaculty,
                schedules: btnNavigationSchedules);
        }
    }
}
