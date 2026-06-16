namespace University_Timetable_and_Classroom_Management_System
{
    public partial class StudyYearsForm
    {
        public StudyYearsForm()
        {
            InitializeComponent();
            ConfigureNavigation();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.Configure(
                this,
                currentButton: btnNavigationStudyYears,
                dashboard: btnNavigationDashboard,
                branches: btnNavigationBranches,
                studyYears: btnNavigationStudyYears,
                classrooms: btnNavigationClassrooms,
                facultyMembers: btnNavigationFaculty,
                schedules: btnNavigationSchedules);
        }
    }
}
