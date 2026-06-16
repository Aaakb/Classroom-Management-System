using Guna.UI2.WinForms;
using University_Timetable_and_Classroom_Management_System.BusinessLayer;

namespace University_Timetable_and_Classroom_Management_System
{
    internal sealed class DashboardPage : UserControl
    {
        private readonly DatabaseHealthService _databaseHealthService = new();
        private readonly Guna2HtmlLabel _lblConnectionStatus;
        private readonly Guna2HtmlLabel _lblConnectionMessage;
        private readonly Guna2Panel _metricsPanel;
        private readonly Guna2Button _btnRefresh;

        public DashboardPage()
        {
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(245, 247, 250);

            var rootPanel = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                FillColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(28)
            };

            var lblTitle = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(28, 22),
                Text = "Dashboard"
            };

            var lblSubtitle = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(30, 58),
                Text = "Review database status and available academic records."
            };

            var healthPanel = new Guna2Panel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent,
                BorderColor = Color.FromArgb(226, 232, 240),
                BorderRadius = 8,
                BorderThickness = 1,
                FillColor = Color.White,
                Location = new Point(28, 94),
                Size = new Size(900, 140)
            };

            var lblHealthTitle = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(24, 18),
                Text = "Database Connection"
            };

            _lblConnectionStatus = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(24, 58),
                Text = "Checking..."
            };

            _lblConnectionMessage = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(24, 88),
                Size = new Size(640, 24),
                Text = "Waiting for database check."
            };

            _btnRefresh = new Guna2Button
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BorderRadius = 8,
                Cursor = Cursors.Hand,
                FillColor = Color.FromArgb(37, 99, 235),
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                HoverState = { FillColor = Color.FromArgb(29, 78, 216) },
                Location = new Point(768, 52),
                Size = new Size(108, 38),
                Text = "Refresh"
            };
            _btnRefresh.Click += async (_, _) => await RefreshHealthAsync();

            healthPanel.Controls.Add(lblHealthTitle);
            healthPanel.Controls.Add(_lblConnectionStatus);
            healthPanel.Controls.Add(_lblConnectionMessage);
            healthPanel.Controls.Add(_btnRefresh);

            var metricsContainer = new Guna2Panel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent,
                BorderColor = Color.FromArgb(226, 232, 240),
                BorderRadius = 8,
                BorderThickness = 1,
                FillColor = Color.White,
                Location = new Point(28, 256),
                Size = new Size(900, 390)
            };

            var lblMetricsTitle = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(24, 18),
                Text = "Records Overview"
            };

            _metricsPanel = new Guna2Panel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent,
                FillColor = Color.White,
                Location = new Point(24, 62),
                Size = new Size(852, 300)
            };

            metricsContainer.Controls.Add(lblMetricsTitle);
            metricsContainer.Controls.Add(_metricsPanel);

            rootPanel.Controls.Add(lblTitle);
            rootPanel.Controls.Add(lblSubtitle);
            rootPanel.Controls.Add(healthPanel);
            rootPanel.Controls.Add(metricsContainer);
            Controls.Add(rootPanel);
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                await RefreshHealthAsync();
            }
        }

        private async Task RefreshHealthAsync()
        {
            try
            {
                UseWaitCursor = true;
                _btnRefresh.Enabled = false;
                _lblConnectionStatus.Text = "Checking...";
                _lblConnectionStatus.ForeColor = Color.FromArgb(100, 116, 139);
                _lblConnectionMessage.Text = "Checking SQL Server connection.";
                _metricsPanel.Controls.Clear();

                var health = await _databaseHealthService.CheckConnectionAsync();
                _lblConnectionStatus.Text = health.CanConnect ? "Connected" : "Not Connected";
                _lblConnectionStatus.ForeColor = health.CanConnect
                    ? Color.FromArgb(22, 163, 74)
                    : Color.FromArgb(220, 38, 38);
                _lblConnectionMessage.Text = health.Message;

                if (!health.CanConnect)
                {
                    return;
                }

                var counts = await _databaseHealthService.GetEntityCountsAsync();

                int index = 0;
                foreach (var item in counts)
                {
                    var card = CreateMetricCard(item.Key, item.Value);
                    card.Location = new Point((index % 4) * 206, (index / 4) * 108);
                    _metricsPanel.Controls.Add(card);
                    index++;
                }
            }
            catch (Exception ex)
            {
                _lblConnectionStatus.Text = "Check Failed";
                _lblConnectionStatus.ForeColor = Color.FromArgb(220, 38, 38);
                _lblConnectionMessage.Text = ex.Message;
            }
            finally
            {
                _btnRefresh.Enabled = true;
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
    }
}
