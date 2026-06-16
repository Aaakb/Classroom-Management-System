using Guna.UI2.WinForms;
using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    internal sealed class TimeSlotsPage : EntityManagementPage<TimeSlot>
    {
        private readonly TimeSlotService _service = new();
        private readonly Guna2TextBox _txtTimeSlotId;
        private readonly Guna2DateTimePicker _dtpStartTime;
        private readonly Guna2DateTimePicker _dtpEndTime;
        private readonly Guna2ToggleSwitch _tglIsBreak;

        public TimeSlotsPage()
            : base(
                "Time Slots",
                "Manage lecture and break time ranges used by schedules.",
                "Time Slot Details",
                "Create and maintain time slot records.")
        {
            _txtTimeSlotId = AddTextField("Time Slot ID", "Auto", true);
            _dtpStartTime = AddTimeField("Start Time");
            _dtpEndTime = AddTimeField("End Time");
            _tglIsBreak = AddToggleField("Is Break");

            AddGridColumn("ID", slot => slot.TimeSlotID, 30F);
            AddGridColumn("Start", slot => slot.StartTime.ToString(@"hh\:mm"), 70F);
            AddGridColumn("End", slot => slot.EndTime.ToString(@"hh\:mm"), 70F);
            AddGridColumn("Type", slot => slot.IsBreak ? "Break" : "Lecture", 70F);
        }

        protected override async Task<IReadOnlyList<TimeSlot>> LoadEntitiesAsync()
        {
            return await _service.GetAllAsync();
        }

        protected override async Task AddEntityAsync(TimeSlot entity)
        {
            await _service.AddAsync(entity);
        }

        protected override async Task UpdateEntityAsync(TimeSlot entity)
        {
            await _service.UpdateAsync(entity);
        }

        protected override async Task DeleteEntityAsync(TimeSlot entity)
        {
            await _service.DeleteAsync(entity.TimeSlotID);
        }

        protected override TimeSlot CreateEntityFromInputs(TimeSlot? selectedEntity)
        {
            return new TimeSlot
            {
                TimeSlotID = selectedEntity?.TimeSlotID ?? 0,
                StartTime = PickerToTime(_dtpStartTime),
                EndTime = PickerToTime(_dtpEndTime),
                IsBreak = _tglIsBreak.Checked
            };
        }

        protected override void WriteEntityToInputs(TimeSlot entity)
        {
            _txtTimeSlotId.Text = entity.TimeSlotID.ToString();
            _dtpStartTime.Value = TimeToPickerValue(entity.StartTime);
            _dtpEndTime.Value = TimeToPickerValue(entity.EndTime);
            _tglIsBreak.Checked = entity.IsBreak;
        }

        protected override void ClearInputControls()
        {
            _txtTimeSlotId.Clear();
            _dtpStartTime.Value = DateTime.Today.AddHours(8);
            _dtpEndTime.Value = DateTime.Today.AddHours(9);
            _tglIsBreak.Checked = false;
        }
    }
}
