using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class TimeSlotsPage : UserControl
    {
        private readonly TimeSlotService _service = new();
        private readonly EntityPageController<TimeSlot> _controller;

        public TimeSlotsPage()
        {
            InitializeComponent();

            _controller = new EntityPageController<TimeSlot>(
                dgvRecords,
                btnAdd,
                btnUpdate,
                btnDelete,
                btnClear,
                btnRefresh,
                async () => await _service.GetAllAsync(),
                async timeSlot => await _service.AddAsync(timeSlot),
                async timeSlot => await _service.UpdateAsync(timeSlot),
                async timeSlot => await _service.DeleteAsync(timeSlot.TimeSlotID),
                CreateEntityFromInputs,
                WriteEntityToInputs,
                ClearInputControls);

            _controller.RegisterColumn(timeSlot => timeSlot.TimeSlotID);
            _controller.RegisterColumn(timeSlot => timeSlot.StartTime.ToString(@"hh\:mm"));
            _controller.RegisterColumn(timeSlot => timeSlot.EndTime.ToString(@"hh\:mm"));
            _controller.RegisterColumn(timeSlot => timeSlot.IsBreak ? "Break" : "Lecture");
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                await _controller.RefreshDataAsync();
            }
        }

        private TimeSlot CreateEntityFromInputs(TimeSlot? selectedEntity)
        {
            return new TimeSlot
            {
                TimeSlotID = selectedEntity?.TimeSlotID ?? 0,
                StartTime = PageInput.PickerToTime(dtpStartTime),
                EndTime = PageInput.PickerToTime(dtpEndTime),
                IsBreak = tglIsBreak.Checked
            };
        }

        private void WriteEntityToInputs(TimeSlot entity)
        {
            txtTimeSlotId.Text = entity.TimeSlotID.ToString();
            dtpStartTime.Value = PageInput.TimeToPickerValue(entity.StartTime);
            dtpEndTime.Value = PageInput.TimeToPickerValue(entity.EndTime);
            tglIsBreak.Checked = entity.IsBreak;
        }

        private void ClearInputControls()
        {
            txtTimeSlotId.Clear();
            dtpStartTime.Value = DateTime.Today.AddHours(8);
            dtpEndTime.Value = DateTime.Today.AddHours(9);
            tglIsBreak.Checked = false;
        }
    }
}
