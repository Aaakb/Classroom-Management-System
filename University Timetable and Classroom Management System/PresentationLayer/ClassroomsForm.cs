using University_Timetable_and_Classroom_Management_System.BusinessLayer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class ClassroomsForm : System.Windows.Forms.UserControl
    {
        private readonly ClassroomService classroomService = new();

        public ClassroomsForm()
        {
            InitializeComponent();
            ConfigureNavigation();
            ConfigureClassroomsGrid();
            ConfigureClassroomsEvents();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadClassroomsAsync();
            ClearClassroomForm();
        }

        private void ConfigureNavigation()
        {
            FormNavigation.ConfigureSidebar(this, pnlSidebar, NavigationPage.Classrooms);
        }

        private void ConfigureClassroomsGrid()
        {
            dgvClassrooms.AutoGenerateColumns = false;
            dgvClassrooms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void ConfigureClassroomsEvents()
        {
            dgvClassrooms.SelectionChanged += (_, _) => PopulateClassroomEditorFromSelection();
            btnAddClassroom.Click += async (_, _) => await AddClassroomAsync();
            btnUpdateClassroom.Click += async (_, _) => await UpdateClassroomAsync();
            btnDeleteClassroom.Click += async (_, _) => await DeleteClassroomAsync();
        }

        private async Task LoadClassroomsAsync()
        {
            SetClassroomActionsEnabled(false);

            try
            {
                var classrooms = await classroomService.GetAllAsync();
                dgvClassrooms.DataSource = classrooms;
                dgvClassrooms.ClearSelection();
            }
            catch (Exception ex)
            {
                ShowError("Unable to load classrooms.", ex);
            }
            finally
            {
                SetClassroomActionsEnabled(true);
            }
        }

        private async Task AddClassroomAsync()
        {
            if (!TryBuildClassroom(out var classroom))
            {
                return;
            }

            await ExecuteClassroomActionAsync(
                async () => await classroomService.AddAsync(classroom),
                "Classroom added successfully.");
        }

        private async Task UpdateClassroomAsync()
        {
            if (!TryGetSelectedClassroomId(out int classroomId) || !TryBuildClassroom(out var classroom))
            {
                ShowInformation("Select a classroom before updating.");
                return;
            }

            classroom.ClassroomID = classroomId;

            await ExecuteClassroomActionAsync(
                async () => await classroomService.UpdateAsync(classroom),
                "Classroom updated successfully.");
        }

        private async Task DeleteClassroomAsync()
        {
            if (!TryGetSelectedClassroomId(out int classroomId))
            {
                ShowInformation("Select a classroom before deleting.");
                return;
            }

            var confirmation = MessageBox.Show(
                this,
                "Are you sure you want to delete the selected classroom?",
                "Delete Classroom",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            await ExecuteClassroomActionAsync(
                async () => await classroomService.DeleteAsync(classroomId),
                "Classroom deleted successfully.");
        }

        private async Task ExecuteClassroomActionAsync(Func<Task> action, string successMessage)
        {
            SetClassroomActionsEnabled(false);

            try
            {
                await action();
                await LoadClassroomsAsync();
                ClearClassroomForm();
                ShowInformation(successMessage);
            }
            catch (Exception ex)
            {
                ShowError("Unable to complete the classroom operation.", ex);
            }
            finally
            {
                SetClassroomActionsEnabled(true);
            }
        }

        private bool TryBuildClassroom(out Classroom classroom)
        {
            classroom = new Classroom
            {
                ClassroomNumber = txtClassroomNumber.Text.Trim(),
                RoomType = cmbRoomType.Text.Trim()
            };

            if (string.IsNullOrWhiteSpace(classroom.ClassroomNumber))
            {
                ShowInformation("Classroom number is required.");
                txtClassroomNumber.Focus();
                return false;
            }

            if (!int.TryParse(txtCapacity.Text, out int capacity) || capacity <= 0)
            {
                ShowInformation("Capacity must be a positive number.");
                txtCapacity.Focus();
                return false;
            }

            classroom.Capacity = capacity;

            if (string.IsNullOrWhiteSpace(classroom.RoomType))
            {
                classroom.RoomType = null;
            }

            return true;
        }

        private bool TryGetSelectedClassroomId(out int classroomId)
        {
            if (int.TryParse(txtClassroomId.Text, out classroomId))
            {
                return true;
            }

            var selectedClassroom = dgvClassrooms.CurrentRow?.DataBoundItem as Classroom;
            classroomId = selectedClassroom?.ClassroomID ?? 0;
            return classroomId > 0;
        }

        private void PopulateClassroomEditorFromSelection()
        {
            if (dgvClassrooms.CurrentRow?.DataBoundItem is not Classroom classroom)
            {
                return;
            }

            txtClassroomId.Text = classroom.ClassroomID.ToString();
            txtClassroomNumber.Text = classroom.ClassroomNumber;
            txtCapacity.Text = classroom.Capacity.ToString();
            cmbRoomType.Text = classroom.RoomType ?? string.Empty;
        }

        private void ClearClassroomForm()
        {
            txtClassroomId.Clear();
            txtClassroomNumber.Clear();
            txtCapacity.Clear();
            cmbRoomType.SelectedIndex = -1;
            dgvClassrooms.ClearSelection();
            txtClassroomNumber.Focus();
        }

        private void SetClassroomActionsEnabled(bool enabled)
        {
            btnAddClassroom.Enabled = enabled;
            btnUpdateClassroom.Enabled = enabled;
            btnDeleteClassroom.Enabled = enabled;
            dgvClassrooms.Enabled = enabled;
        }

        private void ShowInformation(string message)
        {
            MessageBox.Show(this, message, "Classrooms", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show(this, $"{message}\n\n{ex.Message}", "Classrooms", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
