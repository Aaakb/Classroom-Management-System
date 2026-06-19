namespace University_Timetable_and_Classroom_Management_System
{
    internal sealed class FormPageHostControl : System.Windows.Forms.UserControl
    {
        private readonly System.Windows.Forms.Form hostedForm;

        public FormPageHostControl(System.Windows.Forms.Form hostedForm)
        {
            this.hostedForm = hostedForm;
            Dock = System.Windows.Forms.DockStyle.Fill;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 251);

            hostedForm.TopLevel = false;
            hostedForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            hostedForm.ShowInTaskbar = false;
            hostedForm.Dock = System.Windows.Forms.DockStyle.Fill;

            Controls.Add(hostedForm);
            hostedForm.Show();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                hostedForm.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
