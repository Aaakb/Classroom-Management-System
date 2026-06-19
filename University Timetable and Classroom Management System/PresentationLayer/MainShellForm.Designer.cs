namespace University_Timetable_and_Classroom_Management_System
{
    public partial class MainShellForm
    {
        private System.ComponentModel.IContainer components = null!;
        private Guna.UI2.WinForms.Guna2Panel pnlPageHost;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components is not null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlPageHost = new Guna.UI2.WinForms.Guna2Panel();
            SuspendLayout();
            // 
            // pnlPageHost
            // 
            pnlPageHost.BackColor = System.Drawing.Color.FromArgb(245, 247, 251);
            pnlPageHost.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlPageHost.Location = new System.Drawing.Point(0, 0);
            pnlPageHost.Name = "pnlPageHost";
            pnlPageHost.Size = new System.Drawing.Size(1280, 800);
            pnlPageHost.TabIndex = 0;
            // 
            // MainShellForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 251);
            ClientSize = new System.Drawing.Size(1280, 800);
            Controls.Add(pnlPageHost);
            MinimumSize = new System.Drawing.Size(1100, 700);
            Name = "MainShellForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Timetable Studio";
            ResumeLayout(false);
        }
    }
}
