namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SubjectsForm : System.Windows.Forms.Form
    {
        public SubjectsForm()
        {
            InitializeComponent();
            ConfigureNavigation();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.Configure(
                this,
                currentButton: btnNavigationSubjects,
                dashboard: btnNavigationDashboard,
                branches: btnNavigationBranches,
                studyYears: btnNavigationStudyYears,
                subjects: btnNavigationSubjects,
                classrooms: btnNavigationClassrooms,
                facultyMembers: btnNavigationFaculty);
        }

        private void btnNavigationSchedules_Click(object sender, EventArgs e)
        {
            FormNavigation.Open(this, new SchedulesForm());
        }
    }
}
