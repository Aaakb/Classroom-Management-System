namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SectionsForm : System.Windows.Forms.Form
    {
        public SectionsForm()
        {
            InitializeComponent();
            ConfigureNavigation();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.Configure(
                this,
                currentButton: btnNavigationSections,
                dashboard: btnNavigationDashboard,
                branches: btnNavigationBranches,
                studyYears: btnNavigationStudyYears,
                sections: btnNavigationSections,
                subjects: btnNavigationSubjects,
                classrooms: btnNavigationClassrooms,
                timeSlots: btnNavigationTimeSlots,
                facultyMembers: btnNavigationFaculty,
                schedules: btnNavigationSchedules);
        }
    }
}
