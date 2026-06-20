namespace University_Timetable_and_Classroom_Management_System
{
    public sealed partial class DashboardPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            rootPanel = new Guna.UI2.WinForms.Guna2Panel();
            lblTitle = PageDesignerHelper.CreateLabel("Dashboard", 28, 22, 18F);
            lblSubtitle = PageDesignerHelper.CreateLabel("Review database status and available academic records.", 30, 58, 10F, false);
            healthPanel = CreatePanel(28, 94, 900, 140);
            lblHealthTitle = PageDesignerHelper.CreateLabel("Database Connection", 24, 18, 13F);
            lblConnectionStatus = PageDesignerHelper.CreateLabel("Checking...", 24, 58, 12F);
            lblConnectionMessage = PageDesignerHelper.CreateLabel("Waiting for database check.", 24, 88, 9.5F, false);
            btnRefresh = PageDesignerHelper.CreateButton("btnRefresh", "Refresh", 768, 52, Color.FromArgb(37, 99, 235), Color.White);
            metricsContainer = CreatePanel(28, 256, 900, 390);
            lblMetricsTitle = PageDesignerHelper.CreateLabel("Records Overview", 24, 18, 13F);
            metricsPanel = new Guna.UI2.WinForms.Guna2Panel();
            rootPanel.SuspendLayout();
            healthPanel.SuspendLayout();
            metricsContainer.SuspendLayout();
            SuspendLayout();
            // 
            // rootPanel
            // 
            rootPanel.AutoScroll = true;
            rootPanel.Controls.Add(lblTitle);
            rootPanel.Controls.Add(lblSubtitle);
            rootPanel.Controls.Add(healthPanel);
            rootPanel.Controls.Add(metricsContainer);
            rootPanel.Dock = DockStyle.Fill;
            rootPanel.FillColor = Color.FromArgb(245, 247, 250);
            rootPanel.Name = "rootPanel";
            rootPanel.Padding = new Padding(28);
            rootPanel.Size = new Size(960, 720);
            // 
            // healthPanel
            // 
            healthPanel.Controls.Add(lblHealthTitle);
            healthPanel.Controls.Add(lblConnectionStatus);
            healthPanel.Controls.Add(lblConnectionMessage);
            healthPanel.Controls.Add(btnRefresh);
            // 
            // lblConnectionMessage
            // 
            lblConnectionMessage.Size = new Size(640, 42);
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.HoverState.FillColor = Color.FromArgb(29, 78, 216);
            // 
            // metricsContainer
            // 
            metricsContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            metricsContainer.Controls.Add(lblMetricsTitle);
            metricsContainer.Controls.Add(metricsPanel);
            // 
            // metricsPanel
            // 
            metricsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            metricsPanel.AutoScroll = true;
            metricsPanel.BackColor = Color.Transparent;
            metricsPanel.FillColor = Color.White;
            metricsPanel.Location = new Point(24, 62);
            metricsPanel.Name = "metricsPanel";
            metricsPanel.Size = new Size(852, 300);
            // 
            // DashboardPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(rootPanel);
            Name = "DashboardPage";
            Size = new Size(960, 720);
            rootPanel.ResumeLayout(false);
            rootPanel.PerformLayout();
            healthPanel.ResumeLayout(false);
            healthPanel.PerformLayout();
            metricsContainer.ResumeLayout(false);
            metricsContainer.PerformLayout();
            ResumeLayout(false);
        }

        private static Guna.UI2.WinForms.Guna2Panel CreatePanel(int x, int y, int width, int height)
        {
            return new Guna.UI2.WinForms.Guna2Panel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent,
                BorderColor = Color.FromArgb(226, 232, 240),
                BorderRadius = 8,
                BorderThickness = 1,
                FillColor = Color.White,
                Location = new Point(x, y),
                Size = new Size(width, height)
            };
        }

        private Guna.UI2.WinForms.Guna2Panel rootPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubtitle;
        private Guna.UI2.WinForms.Guna2Panel healthPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblHealthTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblConnectionStatus;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblConnectionMessage;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Guna.UI2.WinForms.Guna2Panel metricsContainer;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMetricsTitle;
        private Guna.UI2.WinForms.Guna2Panel metricsPanel;
    }
}
