namespace University_Timetable_and_Classroom_Management_System
{
    public partial class ClassroomsForm : System.Windows.Forms.Form
    {
        public ClassroomsForm()
        {
            InitializeComponent();
            ConfigureNavigation();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.Classrooms);
        }
    }
}
