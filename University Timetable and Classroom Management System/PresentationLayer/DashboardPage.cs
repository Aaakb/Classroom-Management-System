using Guna.UI2.WinForms;
using University_Timetable_and_Classroom_Management_System.BusinessLayer;

namespace University_Timetable_and_Classroom_Management_System
{
    internal sealed class DashboardPage : UserControl
    {
        private const int PageMargin = 28;
        private const int MinimumContentWidth = 900;
        private const int CardWidth = 190;
        private const int CardHeight = 92;
        private const int CardGap = 16;

        private readonly DatabaseHealthService _databaseHealthService = new();
        private readonly Guna2Panel _rootPanel;
        private readonly Guna2Panel _healthPanel;
        private readonly Guna2Panel _metricsContainer;
        private readonly Guna2HtmlLabel _lblConnectionStatus;
        private readonly Guna2HtmlLabel _lblConnectionMessage;
        private readonly Guna2Panel _metricsPanel;
        private readonly Guna2Button _btnRefresh;

        public DashboardPage()
        {
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(245, 247, 250);

            _rootPanel = new Guna2Panel
            {
                AutoScroll = true,
                Dock = DockStyle.Fill,
                FillColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(PageMargin)
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

            _healthPanel = new Guna2Panel
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

            _healthPanel.Controls.Add(lblHealthTitle);
            _healthPanel.Controls.Add(_lblConnectionStatus);
            _healthPanel.Controls.Add(_lblConnectionMessage);
            _healthPanel.Controls.Add(_btnRefresh);

            _metricsContainer = new Guna2Panel
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
                AutoScroll = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent,
                FillColor = Color.White,
                Location = new Point(24, 62),
                Size = new Size(852, 300)
            };

            _metricsContainer.Controls.Add(lblMetricsTitle);
            _metricsContainer.Controls.Add(_metricsPanel);

            _rootPanel.Controls.Add(lblTitle);
            _rootPanel.Controls.Add(lblSubtitle);
            _rootPanel.Controls.Add(_healthPanel);
            _rootPanel.Controls.Add(_metricsContainer);
            Controls.Add(_rootPanel);

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

                foreach (var item in counts)
                {
                    var card = CreateMetricCard(item.Key, item.Value);
                    _metricsPanel.Controls.Add(card);
                }

                ArrangeMetricCards();
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

        private void ApplyResponsiveLayout()
        {
            if (_rootPanel is null || _healthPanel is null || _metricsContainer is null || _metricsPanel is null)
            {
                return;
            }

            int contentWidth = Math.Max(MinimumContentWidth, Width - (PageMargin * 2));
            _healthPanel.Size = new Size(contentWidth, 140);
            _btnRefresh.Location = new Point(contentWidth - 132, 52);
            _lblConnectionMessage.Size = new Size(Math.Max(320, contentWidth - 190), 42);

            int metricsTop = 256;
            int metricsHeight = Math.Max(360, Height - metricsTop - PageMargin);
            _metricsContainer.Location = new Point(PageMargin, metricsTop);
            _metricsContainer.Size = new Size(contentWidth, metricsHeight);
            _metricsPanel.Size = new Size(contentWidth - 48, Math.Max(240, metricsHeight - 90));
            _rootPanel.AutoScrollMinSize = new Size(contentWidth + (PageMargin * 2), metricsTop + metricsHeight + PageMargin);

            ArrangeMetricCards();
        }

        private void ArrangeMetricCards()
        {
            if (_metricsPanel is null || _metricsPanel.Controls.Count == 0)
            {
                return;
            }

            int columns = Math.Max(1, (_metricsPanel.ClientSize.Width + CardGap) / (CardWidth + CardGap));
            int index = 0;

            foreach (Control control in _metricsPanel.Controls)
            {
                int row = index / columns;
                int column = index % columns;
                control.Location = new Point(column * (CardWidth + CardGap), row * (CardHeight + CardGap));
                index++;
            }

            int rows = (int)Math.Ceiling(_metricsPanel.Controls.Count / (double)columns);
            _metricsPanel.AutoScrollMinSize = new Size(0, rows * (CardHeight + CardGap));
        }
    }
}
