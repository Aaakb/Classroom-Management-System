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
            PolishDashboard();
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
            var readiness = await _databaseHealthService.GetScheduleReadinessAsync();

            if (IsDisposed)
            {
                return;
            }

            lblSubjectsMetricValue.Text = counts["Subjects"].ToString();
            lblFacultyMetricValue.Text = counts["Faculty"].ToString();
            lblClassroomsMetricValue.Text = counts["Classrooms"].ToString();
            lblSchedulesMetricValue.Text = counts["Schedules"].ToString();
            lblQuickStartTitle.Text = "Schedule readiness";
            lblQuickStartBody.Text = BuildReadinessMessage(readiness, counts);
        }

        private void PolishDashboard()
        {
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlSubjectsMetric.FillColor = System.Drawing.Color.FromArgb(239, 246, 255);
            pnlFacultyMetric.FillColor = System.Drawing.Color.FromArgb(240, 253, 244);
            pnlClassroomsMetric.FillColor = System.Drawing.Color.FromArgb(255, 247, 237);
            pnlSchedulesMetric.FillColor = System.Drawing.Color.FromArgb(245, 243, 255);

            pnlSubjectsMetric.BorderColor = System.Drawing.Color.FromArgb(191, 219, 254);
            pnlFacultyMetric.BorderColor = System.Drawing.Color.FromArgb(187, 247, 208);
            pnlClassroomsMetric.BorderColor = System.Drawing.Color.FromArgb(254, 215, 170);
            pnlSchedulesMetric.BorderColor = System.Drawing.Color.FromArgb(221, 214, 254);
            pnlQuickStart.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            pnlDatabaseStatus.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);

            lblPageTitle.Text = "Dashboard";
            lblPageSubtitle.Text = "Academic data overview and schedule readiness.";
            lblQuickStartTitle.Text = "Schedule readiness";
            lblQuickStartBody.Text = "Checking database readiness...";
            lblDatabaseStatusTitle.Text = "Database connection";
        }

        private static string BuildReadinessMessage(ScheduleReadinessResult readiness, IReadOnlyDictionary<string, int> counts)
        {
            var issues = readiness.UnassignedSubjects +
                readiness.SubjectsWithoutSections +
                readiness.OversizedSections +
                (readiness.NonBreakTimeSlots == 0 ? 1 : 0) +
                (readiness.ClassroomCount == 0 ? 1 : 0);

            var message = new List<string>
            {
                $"Teaching links: {counts["Teaching"]}",
                $"Sections: {readiness.SectionCount}",
                $"Available time slots: {readiness.NonBreakTimeSlots}",
                ""
            };

            if (issues == 0)
            {
                message.Add("Schedule data looks ready. You can generate a timetable with fewer skipped lessons.");
                return string.Join(Environment.NewLine, message);
            }

            message.Add("Items to fix before generating:");

            if (readiness.UnassignedSubjects > 0)
            {
                message.Add($"- Assign faculty to {readiness.UnassignedSubjects} subject(s).");
            }

            if (readiness.SubjectsWithoutSections > 0)
            {
                message.Add($"- Add matching sections for {readiness.SubjectsWithoutSections} subject(s).");
            }

            if (readiness.OversizedSections > 0)
            {
                message.Add($"- Add larger classrooms for {readiness.OversizedSections} section(s).");
            }

            if (readiness.NonBreakTimeSlots == 0)
            {
                message.Add("- Add at least one non-break time slot.");
            }

            if (readiness.ClassroomCount == 0)
            {
                message.Add("- Add classrooms before generating.");
            }

            return string.Join(Environment.NewLine, message);
        }
    }
}
