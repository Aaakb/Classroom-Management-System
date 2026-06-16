namespace University_Timetable_and_Classroom_Management_System
{
    internal enum NavigationPage
    {
        Branches,
        StudyYears,
        Sections,
        Subjects,
        Classrooms,
        TimeSlots,
        FacultyAssignments,
        FacultyMembers,
        Schedules
    }

    internal static class FormNavigation
    {
        private static readonly IReadOnlyList<NavigationItem> NavigationItems =
        [
            new("btnNavigationDashboard", "Dashboard", null),
            new("btnNavigationBranches", "Branches", NavigationPage.Branches),
            new("btnNavigationStudyYears", "Study Years", NavigationPage.StudyYears),
            new("btnNavigationSections", "Sections", NavigationPage.Sections),
            new("btnNavigationSubjects", "Subjects", NavigationPage.Subjects),
            new("btnNavigationClassrooms", "Classrooms", NavigationPage.Classrooms),
            new("btnNavigationTimeSlots", "Time Slots", NavigationPage.TimeSlots),
            new("btnNavigationFacultyAssignments", "Faculty Assignments", NavigationPage.FacultyAssignments),
            new("btnNavigationFacultyMembers", "Faculty Members", NavigationPage.FacultyMembers),
            new("btnNavigationSchedules", "Schedules", NavigationPage.Schedules)
        ];

        public static void Open(System.Windows.Forms.Form currentForm, System.Windows.Forms.Form nextForm)
        {
            if (currentForm.GetType() == nextForm.GetType())
            {
                nextForm.Dispose();
                return;
            }

            nextForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            nextForm.Show();
            currentForm.Hide();
            nextForm.FormClosed += (_, _) => currentForm.Close();
        }

        public static void ConfigureSidebar(
            System.Windows.Forms.Form currentForm,
            Guna.UI2.WinForms.Guna2Panel sidebar,
            NavigationPage currentPage)
        {
            RemoveExistingNavigationButtons(sidebar);

            sidebar.AutoScroll = true;
            sidebar.AutoScrollMinSize = new System.Drawing.Size(0, 670);

            int top = 98;

            foreach (var item in NavigationItems)
            {
                var button = CreateNavigationButton(item, top);
                sidebar.Controls.Add(button);
                sidebar.Controls.SetChildIndex(button, 0);

                if (item.Page is null || item.Page == currentPage)
                {
                    SetActiveOrDisabled(button, item.Page == currentPage);
                }
                else
                {
                    button.Click += (_, _) => Open(currentForm, CreateForm(item.Page.Value));
                }

                top += 56;
            }
        }

        public static void Configure(
            System.Windows.Forms.Form currentForm,
            Guna.UI2.WinForms.Guna2Button? currentButton = null,
            Guna.UI2.WinForms.Guna2Button? dashboard = null,
            Guna.UI2.WinForms.Guna2Button? branches = null,
            Guna.UI2.WinForms.Guna2Button? studyYears = null,
            Guna.UI2.WinForms.Guna2Button? sections = null,
            Guna.UI2.WinForms.Guna2Button? subjects = null,
            Guna.UI2.WinForms.Guna2Button? classrooms = null,
            Guna.UI2.WinForms.Guna2Button? timeSlots = null,
            Guna.UI2.WinForms.Guna2Button? facultyAssignments = null,
            Guna.UI2.WinForms.Guna2Button? facultyMembers = null,
            Guna.UI2.WinForms.Guna2Button? schedules = null)
        {
            currentButton?.SetEnabled(false);
            dashboard?.SetEnabled(false);

            Wire(branches, currentForm, static () => new BranchesForm());
            Wire(studyYears, currentForm, static () => new StudyYearsForm());
            Wire(sections, currentForm, static () => new SectionsForm());
            Wire(subjects, currentForm, static () => new SubjectsForm());
            Wire(classrooms, currentForm, static () => new ClassroomsForm());
            Wire(timeSlots, currentForm, static () => new TimeSlotsForm());
            Wire(facultyAssignments, currentForm, static () => new FacultyMemberSubjectsForm());
            Wire(facultyMembers, currentForm, static () => new FacultyMembersForm());
            Wire(schedules, currentForm, static () => new SchedulesForm());
        }

        private static void Wire(
            Guna.UI2.WinForms.Guna2Button? button,
            System.Windows.Forms.Form currentForm,
            Func<System.Windows.Forms.Form> createForm)
        {
            if (button is null || !button.Enabled)
            {
                return;
            }

            button.Click += (_, _) => Open(currentForm, createForm());
        }

        private static void RemoveExistingNavigationButtons(Guna.UI2.WinForms.Guna2Panel sidebar)
        {
            var navigationButtons = sidebar.Controls
                .OfType<Guna.UI2.WinForms.Guna2Button>()
                .Where(control => control.Name.StartsWith("btnNavigation", StringComparison.Ordinal))
                .ToList();

            foreach (var button in navigationButtons)
            {
                sidebar.Controls.Remove(button);
                button.Dispose();
            }
        }

        private static Guna.UI2.WinForms.Guna2Button CreateNavigationButton(NavigationItem item, int top)
        {
            return new Guna.UI2.WinForms.Guna2Button
            {
                BorderRadius = 8,
                Cursor = System.Windows.Forms.Cursors.Hand,
                FillColor = System.Drawing.Color.FromArgb(24, 38, 62),
                Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point),
                ForeColor = System.Drawing.Color.FromArgb(226, 232, 240),
                HoverState = { FillColor = System.Drawing.Color.FromArgb(36, 55, 86) },
                Location = new System.Drawing.Point(24, top),
                Name = item.ButtonName,
                Size = new System.Drawing.Size(192, 44),
                Text = item.Text,
                TextAlign = System.Windows.Forms.HorizontalAlignment.Left,
                TextOffset = new System.Drawing.Point(14, 0)
            };
        }

        private static void SetActiveOrDisabled(Guna.UI2.WinForms.Guna2Button button, bool isActive)
        {
            button.Enabled = false;
            button.Cursor = System.Windows.Forms.Cursors.Default;

            if (!isActive)
            {
                return;
            }

            button.Checked = true;
            button.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            button.ForeColor = System.Drawing.Color.White;
            button.HoverState.FillColor = System.Drawing.Color.FromArgb(29, 78, 216);
        }

        private static System.Windows.Forms.Form CreateForm(NavigationPage page)
        {
            return page switch
            {
                NavigationPage.Branches => new BranchesForm(),
                NavigationPage.StudyYears => new StudyYearsForm(),
                NavigationPage.Sections => new SectionsForm(),
                NavigationPage.Subjects => new SubjectsForm(),
                NavigationPage.Classrooms => new ClassroomsForm(),
                NavigationPage.TimeSlots => new TimeSlotsForm(),
                NavigationPage.FacultyAssignments => new FacultyMemberSubjectsForm(),
                NavigationPage.FacultyMembers => new FacultyMembersForm(),
                NavigationPage.Schedules => new SchedulesForm(),
                _ => throw new ArgumentOutOfRangeException(nameof(page), page, null)
            };
        }

        private sealed record NavigationItem(string ButtonName, string Text, NavigationPage? Page);
    }

    internal static class NavigationButtonExtensions
    {
        public static void SetEnabled(this Guna.UI2.WinForms.Guna2Button button, bool enabled)
        {
            button.Enabled = enabled;
            button.Cursor = enabled
                ? System.Windows.Forms.Cursors.Hand
                : System.Windows.Forms.Cursors.Default;
        }
    }
}
