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
            btnNavigationStudyYears.Enabled = false;
            btnNavigationBranches.Click += (_, _) => FormNavigation.Open(this, new BranchesForm());
            btnNavigationFaculty.Click += (_, _) => FormNavigation.Open(this, new FacultyMembersForm());
        }
    }
}
