namespace University_Timetable_and_Classroom_Management_System
{
    internal sealed class NavigationApplicationContext : System.Windows.Forms.ApplicationContext
    {
        private bool isNavigating;

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
            if (isNavigating)
            {
                nextForm.Dispose();
                return;
            }

            if (currentForm.GetType() == nextForm.GetType())
            {
                nextForm.Dispose();
                return;
            }

            if (MainForm is not null && !ReferenceEquals(MainForm, currentForm))
            {
                currentForm = MainForm;
            }

            isNavigating = true;
            try
            {
                var previousMainForm = MainForm;

                if (previousMainForm is not null)
                {
                    previousMainForm.FormClosed -= HandleActiveFormClosed;
                }

                FormNavigation.ApplyWindowState(currentForm, nextForm);

                MainForm = nextForm;
                MainForm.FormClosed += HandleActiveFormClosed;

                if (!currentForm.IsDisposed)
                {
                    currentForm.Hide();
                }

                MainForm.Show();

                MainForm.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        if (!currentForm.IsDisposed)
                        {
                            currentForm.Dispose();
                        }
                    }
                    finally
                    {
                        isNavigating = false;
                    }
                }));
            }
            catch
            {
                isNavigating = false;
                nextForm.Dispose();

                if (!currentForm.IsDisposed)
                {
                    currentForm.Show();
                }

                throw;
            }
        }

        private void HandleActiveFormClosed(object? sender, System.Windows.Forms.FormClosedEventArgs e)
        {
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
