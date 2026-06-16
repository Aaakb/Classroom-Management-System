namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SchedulesForm : System.Windows.Forms.Form
    {
        public SchedulesForm()
        {
            InitializeComponent();
            ConfigureNavigation();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.Configure(
                this,
                currentButton: btnNavigationSchedules,
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
