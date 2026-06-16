namespace University_Timetable_and_Classroom_Management_System
{
    internal sealed class NavigationApplicationContext : System.Windows.Forms.ApplicationContext
    {
        private bool isSwitchingForm;

        public static NavigationApplicationContext? Current { get; private set; }

        public NavigationApplicationContext(System.Windows.Forms.Form initialForm)
        {
            Current = this;
            MainForm = initialForm;
            MainForm.FormClosed += HandleActiveFormClosed;
            MainForm.Show();
        }

        public void Navigate(
            System.Windows.Forms.Form currentForm,
            System.Windows.Forms.Form nextForm)
        {
            if (currentForm.GetType() == nextForm.GetType())
            {
                nextForm.Dispose();
                return;
            }

            FormNavigation.ApplyWindowState(currentForm, nextForm);

            isSwitchingForm = true;
            try
            {
                var previousMainForm = MainForm;

                if (previousMainForm is not null)
                {
                    previousMainForm.FormClosed -= HandleActiveFormClosed;
                }

                MainForm = nextForm;
                MainForm.FormClosed += HandleActiveFormClosed;
                MainForm.Show();

                if (!currentForm.IsDisposed)
                {
                    currentForm.Close();
                }
            }
            finally
            {
                isSwitchingForm = false;
            }
        }

        private void HandleActiveFormClosed(object? sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            if (isSwitchingForm)
            {
                return;
            }

            if (sender is System.Windows.Forms.Form closedForm)
            {
                closedForm.FormClosed -= HandleActiveFormClosed;
            }

            ExitThread();
        }

        protected override void ExitThreadCore()
        {
            if (ReferenceEquals(Current, this))
            {
                Current = null;
            }

            base.ExitThreadCore();
        }
    }
}
