namespace University_Timetable_and_Classroom_Management_System
{
    public partial class MainShellForm : System.Windows.Forms.Form
    {
        private FormPageHostControl? activePageHost;

        internal static MainShellForm? Current { get; private set; }

        public MainShellForm()
        {
            InitializeComponent();
            Current = this;
            ShowPage(new DashboardForm());
        }

        internal void ShowPage(System.Windows.Forms.Form pageForm)
        {
            var nextPageHost = new FormPageHostControl(pageForm);

            pnlPageHost.SuspendLayout();
            pnlPageHost.Controls.Clear();

            activePageHost?.Dispose();
            activePageHost = nextPageHost;

            pnlPageHost.Controls.Add(activePageHost);
            pnlPageHost.ResumeLayout();
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
