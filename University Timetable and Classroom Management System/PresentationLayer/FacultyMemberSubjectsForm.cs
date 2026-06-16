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
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.FacultyAssignments);
        }

        private void dgvFacultyMemberSubjects_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblTableTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
