using University_Timetable_and_Classroom_Management_System.BusinessLayer;

namespace University_Timetable_and_Classroom_Management_System
{
    public class LoginForm : Form
    {
        private readonly AuthService authService = new();

        private Guna.UI2.WinForms.Guna2Panel pnlBrand = null!;
        private Guna.UI2.WinForms.Guna2Panel pnlContent = null!;
        private Guna.UI2.WinForms.Guna2Panel pnlCard = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblBrandTitle = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblBrandSubtitle = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubtitle = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblUserName = null!;
        private Guna.UI2.WinForms.Guna2TextBox txtUserName = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPassword = null!;
        private Guna.UI2.WinForms.Guna2TextBox txtPassword = null!;
        private Guna.UI2.WinForms.Guna2Button btnSignIn = null!;
        private Guna.UI2.WinForms.Guna2Button btnCreateAccount = null!;

        public LoginForm()
        {
            InitializeComponent();
            WireEvents();
        }

        private void WireEvents()
        {
            btnSignIn.Click += async (_, _) => await SignInAsync();
            btnCreateAccount.Click += (_, _) => OpenRegisterForm();
            txtPassword.KeyDown += async (_, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    await SignInAsync();
                }
            };
        }

        private async Task SignInAsync()
        {
            SetBusy(true);

            try
            {
                var result = await authService.SignInAsync(txtUserName.Text, txtPassword.Text);

                if (!result.Succeeded)
                {
                    MessageBox.Show(this, result.Message, "Sign In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Sign In", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private void OpenRegisterForm()
        {
            using var registerForm = new RegisterForm();

            if (registerForm.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            txtUserName.Text = registerForm.CreatedUserName;
            txtPassword.Clear();
            txtPassword.Focus();
        }

        private void SetBusy(bool busy)
        {
            txtUserName.Enabled = !busy;
            txtPassword.Enabled = !busy;
            btnCreateAccount.Enabled = !busy;
            btnSignIn.Enabled = !busy;
            btnSignIn.Text = busy ? "Signing in..." : "Sign In";
        }

        private void InitializeComponent()
        {
            pnlBrand = new Guna.UI2.WinForms.Guna2Panel();
            pnlContent = new Guna.UI2.WinForms.Guna2Panel();
            pnlCard = new Guna.UI2.WinForms.Guna2Panel();
            lblBrandTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblBrandSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblUserName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtUserName = new Guna.UI2.WinForms.Guna2TextBox();
            lblPassword = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            btnSignIn = new Guna.UI2.WinForms.Guna2Button();
            btnCreateAccount = new Guna.UI2.WinForms.Guna2Button();
            pnlBrand.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlCard.SuspendLayout();
            SuspendLayout();

            pnlBrand.Dock = DockStyle.Left;
            pnlBrand.FillColor = Color.FromArgb(24, 38, 62);
            pnlBrand.Size = new Size(360, 620);
            pnlBrand.Controls.Add(lblBrandTitle);
            pnlBrand.Controls.Add(lblBrandSubtitle);

            lblBrandTitle.BackColor = Color.Transparent;
            lblBrandTitle.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            lblBrandTitle.ForeColor = Color.White;
            lblBrandTitle.Location = new Point(36, 70);
            lblBrandTitle.Text = "Timetable Studio";

            lblBrandSubtitle.BackColor = Color.Transparent;
            lblBrandSubtitle.Font = new Font("Segoe UI", 10F);
            lblBrandSubtitle.ForeColor = Color.FromArgb(203, 213, 225);
            lblBrandSubtitle.Location = new Point(38, 118);
            lblBrandSubtitle.Size = new Size(270, 72);
            lblBrandSubtitle.Text = "Secure access for classroom, subject, faculty, and schedule management.";

            pnlContent.Dock = DockStyle.Fill;
            pnlContent.FillColor = Color.FromArgb(245, 247, 250);
            pnlContent.Controls.Add(pnlCard);

            pnlCard.Anchor = AnchorStyles.None;
            pnlCard.BorderColor = Color.FromArgb(226, 232, 240);
            pnlCard.BorderRadius = 8;
            pnlCard.BorderThickness = 1;
            pnlCard.FillColor = Color.White;
            pnlCard.Location = new Point(112, 92);
            pnlCard.Size = new Size(408, 430);
            pnlCard.Controls.Add(lblTitle);
            pnlCard.Controls.Add(lblSubtitle);
            pnlCard.Controls.Add(lblUserName);
            pnlCard.Controls.Add(txtUserName);
            pnlCard.Controls.Add(lblPassword);
            pnlCard.Controls.Add(txtPassword);
            pnlCard.Controls.Add(btnSignIn);
            pnlCard.Controls.Add(btnCreateAccount);

            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(32, 30);
            lblTitle.Text = "Sign In";

            lblSubtitle.BackColor = Color.Transparent;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(34, 72);
            lblSubtitle.Text = "Enter your account details to continue.";

            lblUserName.BackColor = Color.Transparent;
            lblUserName.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblUserName.ForeColor = Color.FromArgb(51, 65, 85);
            lblUserName.Location = new Point(34, 122);
            lblUserName.Text = "User Name";

            txtUserName.BorderColor = Color.FromArgb(203, 213, 225);
            txtUserName.BorderRadius = 8;
            txtUserName.Font = new Font("Segoe UI", 10F);
            txtUserName.ForeColor = Color.FromArgb(15, 23, 42);
            txtUserName.Location = new Point(32, 150);
            txtUserName.PlaceholderText = "Enter user name";
            txtUserName.Size = new Size(344, 44);

            lblPassword.BackColor = Color.Transparent;
            lblPassword.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(51, 65, 85);
            lblPassword.Location = new Point(34, 214);
            lblPassword.Text = "Password";

            txtPassword.BorderColor = Color.FromArgb(203, 213, 225);
            txtPassword.BorderRadius = 8;
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.ForeColor = Color.FromArgb(15, 23, 42);
            txtPassword.Location = new Point(32, 242);
            txtPassword.PasswordChar = '*';
            txtPassword.PlaceholderText = "Enter password";
            txtPassword.Size = new Size(344, 44);

            btnSignIn.BorderRadius = 8;
            btnSignIn.Cursor = Cursors.Hand;
            btnSignIn.FillColor = Color.FromArgb(37, 99, 235);
            btnSignIn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnSignIn.ForeColor = Color.White;
            btnSignIn.HoverState.FillColor = Color.FromArgb(29, 78, 216);
            btnSignIn.Location = new Point(32, 316);
            btnSignIn.Size = new Size(344, 44);
            btnSignIn.Text = "Sign In";

            btnCreateAccount.BorderRadius = 8;
            btnCreateAccount.Cursor = Cursors.Hand;
            btnCreateAccount.FillColor = Color.White;
            btnCreateAccount.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnCreateAccount.ForeColor = Color.FromArgb(37, 99, 235);
            btnCreateAccount.HoverState.FillColor = Color.FromArgb(239, 246, 255);
            btnCreateAccount.Location = new Point(32, 368);
            btnCreateAccount.Size = new Size(344, 38);
            btnCreateAccount.Text = "Create New Account";

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(980, 620);
            Controls.Add(pnlContent);
            Controls.Add(pnlBrand);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimumSize = new Size(980, 620);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sign In";
            pnlBrand.ResumeLayout(false);
            pnlBrand.PerformLayout();
            pnlContent.ResumeLayout(false);
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            ResumeLayout(false);
        }
    }
}
