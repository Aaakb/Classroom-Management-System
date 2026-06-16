using Guna.UI2.WinForms;
using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    internal sealed class ClassroomsPage : EntityManagementPage<Classroom>
    {
        private readonly ClassroomService _service = new();
        private readonly Guna2TextBox _txtClassroomId;
        private readonly Guna2TextBox _txtClassroomNumber;
        private readonly Guna2TextBox _txtCapacity;
        private readonly Guna2ComboBox _cmbRoomType;

        public ClassroomsPage()
            : base(
                "Classrooms",
                "Manage room capacity and classroom type information.",
                "Classroom Details",
                "Create and maintain classroom records.")
        {
            _txtClassroomId = AddTextField("Classroom ID", "Auto", true);
            _txtClassroomNumber = AddTextField("Classroom Number", "Enter room number");
            _txtCapacity = AddTextField("Capacity", "Enter capacity");
            _cmbRoomType = AddComboField("Room Type");
            _cmbRoomType.Items.AddRange(new object[] { "Lecture Hall", "Laboratory", "Seminar Room", "Computer Lab" });

            AddGridColumn("ID", classroom => classroom.ClassroomID, 30F);
            AddGridColumn("Room Number", classroom => classroom.ClassroomNumber, 90F);
            AddGridColumn("Capacity", classroom => classroom.Capacity, 60F);
            AddGridColumn("Room Type", classroom => classroom.RoomType ?? "None", 80F);
        }

        protected override async Task<IReadOnlyList<Classroom>> LoadEntitiesAsync()
        {
            return await _service.GetAllAsync();
        }

        protected override async Task AddEntityAsync(Classroom entity)
        {
            await _service.AddAsync(entity);
        }

        protected override async Task UpdateEntityAsync(Classroom entity)
        {
            await _service.UpdateAsync(entity);
        }

        protected override async Task DeleteEntityAsync(Classroom entity)
        {
            await _service.DeleteAsync(entity.ClassroomID);
        }

        protected override Classroom CreateEntityFromInputs(Classroom? selectedEntity)
        {
            return new Classroom
            {
                ClassroomID = selectedEntity?.ClassroomID ?? 0,
                ClassroomNumber = RequiredText(_txtClassroomNumber, "Classroom number"),
                Capacity = PositiveInt(_txtCapacity, "Capacity"),
                RoomType = string.IsNullOrWhiteSpace(_cmbRoomType.Text) ? null : _cmbRoomType.Text
            };
        }

        protected override void WriteEntityToInputs(Classroom entity)
        {
            _txtClassroomId.Text = entity.ClassroomID.ToString();
            _txtClassroomNumber.Text = entity.ClassroomNumber;
            _txtCapacity.Text = entity.Capacity.ToString();
            _cmbRoomType.Text = entity.RoomType ?? string.Empty;
        }

        protected override void ClearInputControls()
        {
            _txtClassroomId.Clear();
            _txtClassroomNumber.Clear();
            _txtCapacity.Clear();
            _cmbRoomType.SelectedIndex = -1;
        }
    }
}
