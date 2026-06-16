namespace University_Timetable_and_Classroom_Management_System
{
    public partial class BranchesForm
    {
        public BranchesForm()
        {
            InitializeComponent();
            ConfigureNavigation();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.Branches);
        }

        private void lblApplicationName_Click(object sender, EventArgs e)
        {
        }
    }
}
