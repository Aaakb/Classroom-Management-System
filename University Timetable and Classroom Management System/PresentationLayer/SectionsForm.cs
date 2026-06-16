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
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.Sections);
        }
    }
}
