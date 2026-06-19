namespace University_Timetable_and_Classroom_Management_System
{
    public partial class MainShellForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.UserControl? activePage;

        internal static MainShellForm? Current { get; private set; }

        public MainShellForm()
        {
            InitializeComponent();
            Current = this;
            ShowPage(NavigationPage.Dashboard);
        }

        internal void ShowPage(NavigationPage page)
        {
            var nextPage = FormNavigation.CreatePage(page);
            nextPage.Dock = System.Windows.Forms.DockStyle.Fill;

            pnlPageHost.SuspendLayout();
            pnlPageHost.Controls.Clear();

            activePage?.Dispose();
            activePage = nextPage;

            pnlPageHost.Controls.Add(activePage);
            pnlPageHost.ResumeLayout();

            FormNavigation.ConfigureShellSidebar(this, pnlSidebar, page);
        }

        protected override void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)
        {
            if (ReferenceEquals(Current, this))
            {
                Current = null;
            }

            base.OnFormClosed(e);
        }
    }
}
