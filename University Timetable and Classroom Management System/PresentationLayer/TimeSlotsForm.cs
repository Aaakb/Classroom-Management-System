using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class TimeSlotsForm : System.Windows.Forms.UserControl
    {
        private readonly TimeSlotService timeSlotService = new();

        public TimeSlotsForm()
        {
            InitializeComponent();
            ConfigureAutoIdField();
            ConfigureNavigation();
            ConfigureTimeSlotsGrid();
            ConfigureTimeSlotsEvents();
        }

        private void ConfigureAutoIdField()
        {
            txtTimeSlotId.ReadOnly = true;
            txtTimeSlotId.TabStop = false;
            txtTimeSlotId.PlaceholderText = "Auto";
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadTimeSlotsAsync();
            ClearTimeSlotForm();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.TimeSlots);
        }

        private void ConfigureTimeSlotsGrid()
        {
            dgvTimeSlots.AutoGenerateColumns = false;
            dgvTimeSlots.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void ConfigureTimeSlotsEvents()
        {
            dgvTimeSlots.SelectionChanged += (_, _) => PopulateTimeSlotEditorFromSelection();
            txtTimeSlotId.Leave += async (_, _) => await PopulateTimeSlotEditorFromEnteredIdAsync();
            btnAddTimeSlot.Click += async (_, _) => await AddTimeSlotAsync();
            btnUpdateTimeSlot.Click += async (_, _) => await UpdateTimeSlotAsync();
            btnDeleteTimeSlot.Click += async (_, _) => await DeleteTimeSlotAsync();
        }

        private async Task LoadTimeSlotsAsync()
        {
            SetTimeSlotActionsEnabled(false);

            try
            {
                var timeSlots = await timeSlotService.GetAllAsync();
                dgvTimeSlots.DataSource = timeSlots;
                dgvTimeSlots.ClearSelection();
            }
            catch (Exception ex)
            {
                ShowError("Unable to load time slots.", ex);
            }
            finally
            {
                SetTimeSlotActionsEnabled(true);
            }
        }

        private async Task AddTimeSlotAsync()
        {
            if (!TryBuildTimeSlot(out var timeSlot, requireId: false))
            {
                return;
            }

            await ExecuteTimeSlotActionAsync(
                async () => await timeSlotService.AddAsync(timeSlot),
                "Time slot added successfully.");
        }

        private async Task UpdateTimeSlotAsync()
        {
            if (!TryBuildTimeSlot(out var timeSlot))
            {
                return;
            }

            await ExecuteTimeSlotActionAsync(
                async () => await timeSlotService.UpdateAsync(timeSlot),
                "Time slot updated successfully.");
        }

        private async Task DeleteTimeSlotAsync()
        {
            if (!TryGetTimeSlotIdFromEditor(out int timeSlotId))
            {
                return;
            }

            var confirmation = MessageBox.Show(
                this,
                "Are you sure you want to delete the selected time slot?",
                "Delete Time Slot",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            await ExecuteTimeSlotActionAsync(
                async () => await timeSlotService.DeleteAsync(timeSlotId),
                "Time slot deleted successfully.");
        }

        private async Task ExecuteTimeSlotActionAsync(Func<Task> action, string successMessage)
        {
            SetTimeSlotActionsEnabled(false);

            try
            {
                await action();
                await LoadTimeSlotsAsync();
                ClearTimeSlotForm();
                ShowInformation(successMessage);
            }
            catch (Exception ex)
            {
                ShowError("Unable to complete the time slot operation.", ex);
            }
            finally
            {
                SetTimeSlotActionsEnabled(true);
            }
        }

        private bool TryBuildTimeSlot(out TimeSlot timeSlot, bool requireId = true)
        {
            timeSlot = new TimeSlot
            {
                StartTime = ToMinutePrecision(dtpStartTime.Value),
                EndTime = ToMinutePrecision(dtpEndTime.Value),
                IsBreak = tglIsBreak.Checked
            };

            var timeSlotId = 0;
            if (requireId && !TryGetTimeSlotIdFromEditor(out timeSlotId))
            {
                return false;
            }

            timeSlot.TimeSlotID = requireId ? timeSlotId : 0;

            if (timeSlot.EndTime > timeSlot.StartTime)
            {
                return true;
            }

            ShowInformation("End time must be after start time.");
            dtpEndTime.Focus();
            return false;
        }

        private bool TryGetTimeSlotIdFromEditor(out int timeSlotId)
        {
            if (int.TryParse(txtTimeSlotId.Text, out timeSlotId) && timeSlotId > 0)
            {
                return true;
            }

            ShowInformation("Select a time slot row first.");
            return false;
        }

        private void PopulateTimeSlotEditorFromSelection()
        {
            if (dgvTimeSlots.CurrentRow?.DataBoundItem is not TimeSlot timeSlot)
            {
                return;
            }

            txtTimeSlotId.Text = timeSlot.TimeSlotID.ToString();
            dtpStartTime.Value = DateTime.Today.Add(timeSlot.StartTime);
            dtpEndTime.Value = DateTime.Today.Add(timeSlot.EndTime);
            tglIsBreak.Checked = timeSlot.IsBreak;
        }

        private async Task PopulateTimeSlotEditorFromEnteredIdAsync()
        {
            if (!int.TryParse(txtTimeSlotId.Text, out int timeSlotId) || timeSlotId <= 0)
            {
                return;
            }

            try
            {
                var timeSlot = await timeSlotService.GetByIdAsync(timeSlotId);

                if (timeSlot is null)
                {
                    return;
                }

                dtpStartTime.Value = DateTime.Today.Add(timeSlot.StartTime);
                dtpEndTime.Value = DateTime.Today.Add(timeSlot.EndTime);
                tglIsBreak.Checked = timeSlot.IsBreak;
                SelectTimeSlotRow(timeSlot.TimeSlotID);
            }
            catch (Exception ex)
            {
                ShowError("Unable to load time slot details.", ex);
            }
        }

        private void SelectTimeSlotRow(int timeSlotId)
        {
            foreach (DataGridViewRow row in dgvTimeSlots.Rows)
            {
                if (row.DataBoundItem is not TimeSlot timeSlot || timeSlot.TimeSlotID != timeSlotId)
                {
                    continue;
                }

                row.Selected = true;
                dgvTimeSlots.CurrentCell = row.Cells[0];
                break;
            }
        }

        private void ClearTimeSlotForm()
        {
            txtTimeSlotId.Text = "Auto";
            dtpStartTime.Value = DateTime.Today.AddHours(8);
            dtpEndTime.Value = DateTime.Today.AddHours(9);
            tglIsBreak.Checked = false;
            dgvTimeSlots.ClearSelection();
            dtpStartTime.Focus();
        }

        private void SetTimeSlotActionsEnabled(bool enabled)
        {
            btnAddTimeSlot.Enabled = enabled;
            btnUpdateTimeSlot.Enabled = enabled;
            btnDeleteTimeSlot.Enabled = enabled;
            dgvTimeSlots.Enabled = enabled;
        }

        private static TimeSpan ToMinutePrecision(DateTime value)
        {
            return new TimeSpan(value.Hour, value.Minute, 0);
        }

        private void ShowInformation(string message)
        {
            MessageBox.Show(this, message, "Time Slots", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show(this, $"{message}\n\n{ex.Message}", "Time Slots", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
