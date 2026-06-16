namespace University_Timetable_and_Classroom_Management_System
{
    public partial class DashboardForm : System.Windows.Forms.Form
    {
        public DashboardForm()
        {
            InitializeComponent();
            ConfigureNavigation();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.Dashboard);
        }
    }
}
