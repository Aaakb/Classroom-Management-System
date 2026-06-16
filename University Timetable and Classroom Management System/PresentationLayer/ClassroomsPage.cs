using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class ClassroomsPage : UserControl
    {
        private readonly ClassroomService _service = new();
        private readonly EntityPageController<Classroom> _controller;

        public ClassroomsPage()
        {
            InitializeComponent();

            _controller = new EntityPageController<Classroom>(
                dgvRecords,
                btnAdd,
                btnUpdate,
                btnDelete,
                btnClear,
                btnRefresh,
                async () => await _service.GetAllAsync(),
                async classroom => await _service.AddAsync(classroom),
                async classroom => await _service.UpdateAsync(classroom),
                async classroom => await _service.DeleteAsync(classroom.ClassroomID),
                CreateEntityFromInputs,
                WriteEntityToInputs,
                ClearInputControls);

            _controller.RegisterColumn(classroom => classroom.ClassroomID);
            _controller.RegisterColumn(classroom => classroom.ClassroomNumber);
            _controller.RegisterColumn(classroom => classroom.Capacity);
            _controller.RegisterColumn(classroom => classroom.RoomType ?? "None");
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                await _controller.RefreshDataAsync();
            }
        }

        private Classroom CreateEntityFromInputs(Classroom? selectedEntity)
        {
            return new Classroom
            {
                ClassroomID = selectedEntity?.ClassroomID ?? 0,
                ClassroomNumber = PageInput.RequiredText(txtClassroomNumber, "Classroom number"),
                Capacity = PageInput.PositiveInt(txtCapacity, "Capacity"),
                RoomType = string.IsNullOrWhiteSpace(cmbRoomType.Text) ? null : cmbRoomType.Text
            };
        }

        private void WriteEntityToInputs(Classroom entity)
        {
            txtClassroomId.Text = entity.ClassroomID.ToString();
            txtClassroomNumber.Text = entity.ClassroomNumber;
            txtCapacity.Text = entity.Capacity.ToString();
            cmbRoomType.Text = entity.RoomType ?? string.Empty;
        }

        private void ClearInputControls()
        {
            txtClassroomId.Clear();
            txtClassroomNumber.Clear();
            txtCapacity.Clear();
            cmbRoomType.SelectedIndex = -1;
        }
    }
}
