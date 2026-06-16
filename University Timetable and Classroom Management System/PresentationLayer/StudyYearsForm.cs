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
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.StudyYears);
        }
    }
}
