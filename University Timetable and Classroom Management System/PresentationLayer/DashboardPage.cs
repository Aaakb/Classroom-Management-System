using Guna.UI2.WinForms;
using University_Timetable_and_Classroom_Management_System.BusinessLayer;

namespace University_Timetable_and_Classroom_Management_System
{
    public sealed partial class DashboardPage : UserControl
    {
        private const int PageMargin = 28;
        private const int MinimumContentWidth = 900;
        private const int CardWidth = 190;
        private const int CardHeight = 92;
        private const int CardGap = 16;

        private readonly DatabaseHealthService _databaseHealthService = new();

        public DashboardPage()
        {
            InitializeComponent();
            btnRefresh.Click += async (_, _) => await RefreshHealthAsync();
            ApplyResponsiveLayout();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                await RefreshHealthAsync();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyResponsiveLayout();
        }

        private async Task RefreshHealthAsync()
        {
            try
            {
                UseWaitCursor = true;
                btnRefresh.Enabled = false;
                lblConnectionStatus.Text = "Checking...";
                lblConnectionStatus.ForeColor = Color.FromArgb(100, 116, 139);
                lblConnectionMessage.Text = "Checking SQL Server connection.";
                metricsPanel.Controls.Clear();

                var health = await _databaseHealthService.CheckConnectionAsync();
                lblConnectionStatus.Text = health.CanConnect ? "Connected" : "Not Connected";
                lblConnectionStatus.ForeColor = health.CanConnect
                    ? Color.FromArgb(22, 163, 74)
                    : Color.FromArgb(220, 38, 38);
                lblConnectionMessage.Text = health.Message;

                if (!health.CanConnect)
                {
                    return;
                }

                var counts = await _databaseHealthService.GetEntityCountsAsync();

                foreach (var item in counts)
                {
                    var card = CreateMetricCard(item.Key, item.Value);
                    metricsPanel.Controls.Add(card);
                }

                ArrangeMetricCards();
            }
            catch (Exception ex)
            {
                lblConnectionStatus.Text = "Check Failed";
                lblConnectionStatus.ForeColor = Color.FromArgb(220, 38, 38);
                lblConnectionMessage.Text = ex.Message;
            }
            finally
            {
                btnRefresh.Enabled = true;
                UseWaitCursor = false;
            }
        }

        private static Guna2Panel CreateMetricCard(string title, int count)
        {
            var card = new Guna2Panel
            {
                BorderColor = Color.FromArgb(226, 232, 240),
                BorderRadius = 8,
                BorderThickness = 1,
                FillColor = Color.FromArgb(248, 250, 252),
                Size = new Size(190, 92)
            };

            var lblCount = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(18, 14),
                Text = count.ToString()
            };

            var lblTitle = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(20, 56),
                Text = title
            };

            card.Controls.Add(lblCount);
            card.Controls.Add(lblTitle);
            return card;
        }

        private void ApplyResponsiveLayout()
        {
            if (rootPanel is null || healthPanel is null || metricsContainer is null || metricsPanel is null)
            {
                return;
            }

            int contentWidth = Math.Max(MinimumContentWidth, Width - (PageMargin * 2));
            healthPanel.Size = new Size(contentWidth, 140);
            btnRefresh.Location = new Point(contentWidth - 132, 52);
            lblConnectionMessage.Size = new Size(Math.Max(320, contentWidth - 190), 42);

            int metricsTop = 256;
            int metricsHeight = Math.Max(360, Height - metricsTop - PageMargin);
            metricsContainer.Location = new Point(PageMargin, metricsTop);
            metricsContainer.Size = new Size(contentWidth, metricsHeight);
            metricsPanel.Size = new Size(contentWidth - 48, Math.Max(240, metricsHeight - 90));
            rootPanel.AutoScrollMinSize = new Size(contentWidth + (PageMargin * 2), metricsTop + metricsHeight + PageMargin);

            ArrangeMetricCards();
        }

        private void ArrangeMetricCards()
        {
            if (metricsPanel is null || metricsPanel.Controls.Count == 0)
            {
                return;
            }

            int columns = Math.Max(1, (metricsPanel.ClientSize.Width + CardGap) / (CardWidth + CardGap));
            int index = 0;

            foreach (Control control in metricsPanel.Controls)
            {
                int row = index / columns;
                int column = index % columns;
                control.Location = new Point(column * (CardWidth + CardGap), row * (CardHeight + CardGap));
                index++;
            }

            int rows = (int)Math.Ceiling(metricsPanel.Controls.Count / (double)columns);
            metricsPanel.AutoScrollMinSize = new Size(0, rows * (CardHeight + CardGap));
        }
    }
}
