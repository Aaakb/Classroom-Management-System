using Guna.UI2.WinForms;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class MainForm
    {
        private Guna2Button? _activeNavigationButton;

        public MainForm()
        {
            InitializeComponent();
            ConfigureNavigation();
            ShowPage(btnDashboard, new DashboardPage());
        }

        private void ConfigureNavigation()
        {
            btnDashboard.Click += (_, _) => ShowPage(btnDashboard, new DashboardPage());
            btnBranches.Click += (_, _) => ShowPage(btnBranches, new BranchesPage());
            btnStudyYears.Click += (_, _) => ShowPage(btnStudyYears, new StudyYearsPage());
            btnSections.Click += (_, _) => ShowPage(btnSections, new SectionsPage());
            btnSubjects.Click += (_, _) => ShowPage(btnSubjects, new SubjectsPage());
            btnFacultyMembers.Click += (_, _) => ShowPage(btnFacultyMembers, new FacultyMembersPage());
            btnFacultyAssignments.Click += (_, _) => ShowPage(btnFacultyAssignments, new FacultyAssignmentsPage());
            btnClassrooms.Click += (_, _) => ShowPage(btnClassrooms, new ClassroomsPage());
            btnTimeSlots.Click += (_, _) => ShowPage(btnTimeSlots, new TimeSlotsPage());
            btnSchedules.Click += (_, _) => ShowPage(btnSchedules, new SchedulesPage());
        }

        private void ShowPage(Guna2Button navigationButton, UserControl page)
        {
            pnlContent.SuspendLayout();

            foreach (Control control in pnlContent.Controls)
            {
                control.Dispose();
            }

            pnlContent.Controls.Clear();
            page.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(page);
            pnlContent.ResumeLayout();
            SetActiveNavigationButton(navigationButton);
        }

        private void SetActiveNavigationButton(Guna2Button button)
        {
            if (_activeNavigationButton is not null)
            {
                _activeNavigationButton.FillColor = Color.FromArgb(24, 38, 62);
                _activeNavigationButton.ForeColor = Color.FromArgb(226, 232, 240);
            }

            _activeNavigationButton = button;
            _activeNavigationButton.FillColor = Color.FromArgb(37, 99, 235);
            _activeNavigationButton.ForeColor = Color.White;
        }
    }
}
