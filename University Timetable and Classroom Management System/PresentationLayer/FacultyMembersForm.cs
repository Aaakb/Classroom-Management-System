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
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.FacultyMembers);
        }
    }
}
