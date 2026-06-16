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
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.Subjects);
        }
    }
}
