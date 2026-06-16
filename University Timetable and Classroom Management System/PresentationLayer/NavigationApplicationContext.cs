namespace University_Timetable_and_Classroom_Management_System
{
    internal sealed class NavigationApplicationContext : System.Windows.Forms.ApplicationContext
    {
        private readonly Dictionary<Type, System.Windows.Forms.Form> cachedForms = [];
        private bool isNavigating;

        public static NavigationApplicationContext? Current { get; private set; }

        public NavigationApplicationContext(System.Windows.Forms.Form initialForm)
        {
            Current = this;
            cachedForms[initialForm.GetType()] = initialForm;
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

            Type requestedFormType = nextForm.GetType();

            if (currentForm.GetType() == requestedFormType)
            {
                nextForm.Dispose();
                return;
            }

            if (MainForm is not null && !ReferenceEquals(MainForm, currentForm))
            {
                currentForm = MainForm;
            }

            bool isNewCachedForm = false;

            if (cachedForms.TryGetValue(requestedFormType, out var cachedForm) && !cachedForm.IsDisposed)
            {
                nextForm.Dispose();
                nextForm = cachedForm;
            }
            else
            {
                cachedForms[requestedFormType] = nextForm;
                isNewCachedForm = true;
            }

            isNavigating = true;
            var previousMainForm = MainForm;

            try
            {
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
                isNavigating = false;
            }
            catch
            {
                isNavigating = false;

                if (MainForm is not null)
                {
                    MainForm.FormClosed -= HandleActiveFormClosed;
                }

                MainForm = previousMainForm;

                if (MainForm is not null)
                {
                    MainForm.FormClosed += HandleActiveFormClosed;
                }

                if (isNewCachedForm)
                {
                    cachedForms.Remove(requestedFormType);
                    nextForm.Dispose();
                }

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

            foreach (var form in cachedForms.Values.ToList())
            {
                if (!form.IsDisposed)
                {
                    form.FormClosed -= HandleActiveFormClosed;
                    form.Dispose();
                }
            }

            cachedForms.Clear();

            base.ExitThreadCore();
        }
    }
}
