namespace University_Timetable_and_Classroom_Management_System
{
    internal enum NavigationPage
    {
        Dashboard,
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
            new("btnNavigationDashboard", "Dashboard", NavigationPage.Dashboard),
            new("btnNavigationBranches", "Branches", NavigationPage.Branches),
            new("btnNavigationStudyYears", "Study Years", NavigationPage.StudyYears),
            new("btnNavigationSections", "Sections", NavigationPage.Sections),
            new("btnNavigationSubjects", "Subjects", NavigationPage.Subjects),
            new("btnNavigationClassrooms", "Classrooms", NavigationPage.Classrooms),
            new("btnNavigationTimeSlots", "Time Slots", NavigationPage.TimeSlots),
            new("btnNavigationFacultyAssignments", "Teaching", NavigationPage.FacultyAssignments),
            new("btnNavigationFacultyMembers", "Faculty", NavigationPage.FacultyMembers),
            new("btnNavigationSchedules", "Schedule", NavigationPage.Schedules)
        ];

        public static void ConfigureSidebar(
            System.Windows.Forms.Control currentPage,
            Guna.UI2.WinForms.Guna2Panel sidebar,
            NavigationPage currentPageKey)
        {
            PrepareContentPage(currentPage);
        }

        public static void ConfigureShellSidebar(
            MainShellForm shell,
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

                if (item.Page == currentPage)
                {
                    SetActive(button);
                }
                else
                {
                    button.Click += (_, _) => shell.ShowPage(item.Page);
                }

                top += 56;
            }
        }

        public static System.Windows.Forms.UserControl CreatePage(NavigationPage page)
        {
            return page switch
            {
                NavigationPage.Dashboard => new DashboardForm(),
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

        private static void PrepareContentPage(System.Windows.Forms.Control currentPage)
        {
            currentPage.Dock = System.Windows.Forms.DockStyle.Fill;
            currentPage.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);

            if (FindControl(currentPage, "pnlSidebar") is { } legacySidebar)
            {
                legacySidebar.Visible = false;
                legacySidebar.Enabled = false;
                legacySidebar.Dock = System.Windows.Forms.DockStyle.None;
                legacySidebar.Width = 0;
            }

            if (FindControl(currentPage, "pnlMain") is { } mainPanel)
            {
                mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
                mainPanel.Location = System.Drawing.Point.Empty;
                mainPanel.Margin = System.Windows.Forms.Padding.Empty;
            }
        }

        private static System.Windows.Forms.Control? FindControl(
            System.Windows.Forms.Control parent,
            string name)
        {
            foreach (System.Windows.Forms.Control child in parent.Controls)
            {
                if (child.Name == name)
                {
                    return child;
                }

                var match = FindControl(child, name);

                if (match is not null)
                {
                    return match;
                }
            }

            return null;
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

        private static void SetActive(Guna.UI2.WinForms.Guna2Button button)
        {
            button.Checked = true;
            button.Cursor = System.Windows.Forms.Cursors.Default;
            button.Enabled = false;
            button.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            button.ForeColor = System.Drawing.Color.White;
            button.HoverState.FillColor = System.Drawing.Color.FromArgb(29, 78, 216);
        }

        private sealed record NavigationItem(string ButtonName, string Text, NavigationPage Page);
    }
}
