using University_Timetable_and_Classroom_Management_System.BusinessLayer;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class DashboardForm : System.Windows.Forms.UserControl
    {
        private readonly DatabaseHealthService _databaseHealthService = new();

        public DashboardForm()
        {
            InitializeComponent();
            ConfigureNavigation();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await RefreshDashboardAsync();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.Dashboard);
        }

        private async Task RefreshDashboardAsync()
        {
            lblDatabaseStatusValue.Text = "Checking...";
            lblDatabaseStatusValue.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);

            var health = await _databaseHealthService.CheckConnectionAsync();

            if (IsDisposed)
            {
                return;
            }

            lblDatabaseStatusValue.Text = health.CanConnect ? "Connected" : "Not Connected";
            lblDatabaseStatusValue.ForeColor = health.CanConnect
                ? System.Drawing.Color.FromArgb(22, 163, 74)
                : System.Drawing.Color.FromArgb(220, 38, 38);

            lblQuickStartBody.Text = health.Message;

            if (!health.CanConnect)
            {
                return;
            }

            var counts = await _databaseHealthService.GetEntityCountsAsync();

            if (IsDisposed)
            {
                return;
            }

            lblSubjectsMetricValue.Text = counts["Subjects"].ToString();
            lblFacultyMetricValue.Text = counts["Faculty"].ToString();
            lblClassroomsMetricValue.Text = counts["Classrooms"].ToString();
            lblSchedulesMetricValue.Text = counts["Schedules"].ToString();
        }
    }
}
