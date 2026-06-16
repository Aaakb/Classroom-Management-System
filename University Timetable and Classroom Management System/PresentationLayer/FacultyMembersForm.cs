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
            btnNavigationFaculty.Enabled = false;
            btnNavigationBranches.Click += (_, _) => FormNavigation.Open(this, new BranchesForm());
            btnNavigationStudyYears.Click += (_, _) => FormNavigation.Open(this, new StudyYearsForm());
        }
    }
}
