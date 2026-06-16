namespace University_Timetable_and_Classroom_Management_System
{
    public partial class FacultyMemberSubjectsForm : System.Windows.Forms.Form
    {
        public FacultyMemberSubjectsForm()
        {
            InitializeComponent();
            ConfigureNavigation();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.Configure(
                this,
                currentButton: btnNavigationFacultyAssignments,
                dashboard: btnNavigationDashboard,
                branches: btnNavigationBranches,
                studyYears: btnNavigationStudyYears,
                sections: btnNavigationSections,
                subjects: btnNavigationSubjects,
                classrooms: btnNavigationClassrooms,
                timeSlots: btnNavigationTimeSlots,
                facultyAssignments: btnNavigationFacultyAssignments,
                facultyMembers: btnNavigationFacultyMembers,
                schedules: btnNavigationSchedules);
        }
    }
}
