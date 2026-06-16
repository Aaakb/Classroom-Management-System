namespace University_Timetable_and_Classroom_Management_System
{
    public partial class BranchesForm
    {
        public BranchesForm()
        {
            InitializeComponent();
            ConfigureNavigation();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.Configure(
                this,
                currentButton: btnNavigationBranches,
                dashboard: btnNavigationDashboard,
                branches: btnNavigationBranches,
                studyYears: btnNavigationStudyYears,
                classrooms: btnNavigationClassrooms,
                facultyMembers: btnNavigationFaculty,
                schedules: btnNavigationSchedules);
        }

        private void lblApplicationName_Click(object sender, EventArgs e)
        {
        }
    }
}
