namespace University_Timetable_and_Classroom_Management_System
{
    public partial class ClassroomsForm : System.Windows.Forms.Form
    {
        public ClassroomsForm()
        {
            InitializeComponent();
            ConfigureNavigation();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.Configure(
                this,
                currentButton: btnNavigationClassrooms,
                dashboard: btnNavigationDashboard,
                branches: btnNavigationBranches,
                studyYears: btnNavigationStudyYears,
                subjects: btnNavigationSubjects,
                classrooms: btnNavigationClassrooms,
                timeSlots: btnNavigationTimeSlots,
                facultyMembers: btnNavigationFaculty,
                schedules: btnNavigationSchedules);
        }
    }
}
