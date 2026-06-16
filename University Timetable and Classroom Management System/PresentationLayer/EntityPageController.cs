using System.Globalization;
using Guna.UI2.WinForms;

namespace University_Timetable_and_Classroom_Management_System
{
    internal sealed class EntityPageController<TEntity>
        where TEntity : class
    {
        private readonly Guna2DataGridView _grid;
        private readonly Guna2Button _btnAdd;
        private readonly Guna2Button _btnUpdate;
        private readonly Guna2Button _btnDelete;
        private readonly Guna2Button _btnClear;
        private readonly Guna2Button _btnRefresh;
        private readonly Func<Task<IReadOnlyList<TEntity>>> _loadEntitiesAsync;
        private readonly Func<TEntity, Task> _addEntityAsync;
        private readonly Func<TEntity, Task> _updateEntityAsync;
        private readonly Func<TEntity, Task> _deleteEntityAsync;
        private readonly Func<TEntity?, TEntity> _createEntityFromInputs;
        private readonly Action<TEntity> _writeEntityToInputs;
        private readonly Action _clearInputs;
        private readonly bool _supportsUpdate;
        private readonly List<Func<TEntity, object?>> _valueSelectors = new();

        private TEntity? _selectedEntity;

        public EntityPageController(
            Guna2DataGridView grid,
            Guna2Button btnAdd,
            Guna2Button btnUpdate,
            Guna2Button btnDelete,
            Guna2Button btnClear,
            Guna2Button btnRefresh,
            Func<Task<IReadOnlyList<TEntity>>> loadEntitiesAsync,
            Func<TEntity, Task> addEntityAsync,
            Func<TEntity, Task> updateEntityAsync,
            Func<TEntity, Task> deleteEntityAsync,
            Func<TEntity?, TEntity> createEntityFromInputs,
            Action<TEntity> writeEntityToInputs,
            Action clearInputs,
            bool supportsUpdate = true)
        {
            _grid = grid;
            _btnAdd = btnAdd;
            _btnUpdate = btnUpdate;
            _btnDelete = btnDelete;
            _btnClear = btnClear;
            _btnRefresh = btnRefresh;
            _loadEntitiesAsync = loadEntitiesAsync;
            _addEntityAsync = addEntityAsync;
            _updateEntityAsync = updateEntityAsync;
            _deleteEntityAsync = deleteEntityAsync;
            _createEntityFromInputs = createEntityFromInputs;
            _writeEntityToInputs = writeEntityToInputs;
            _clearInputs = clearInputs;
            _supportsUpdate = supportsUpdate;

            _grid.CellClick += (_, e) => SelectGridRow(e.RowIndex);
            _btnAdd.Click += async (_, _) => await HandleAddAsync();
            _btnUpdate.Click += async (_, _) => await HandleUpdateAsync();
            _btnDelete.Click += async (_, _) => await HandleDeleteAsync();
            _btnClear.Click += (_, _) => ClearForm();
            _btnRefresh.Click += async (_, _) => await RefreshDataAsync();

            SetSelectionState(false);
        }

        public void RegisterColumn(Func<TEntity, object?> valueSelector)
        {
            _valueSelectors.Add(valueSelector);
        }

        public async Task RefreshDataAsync()
        {
            try
            {
                var items = await _loadEntitiesAsync();
                _grid.Rows.Clear();

                foreach (var item in items)
                {
                    object?[] values = _valueSelectors
                        .Select(selector => selector(item) ?? string.Empty)
                        .ToArray();

                    int rowIndex = _grid.Rows.Add(values);
                    _grid.Rows[rowIndex].Tag = item;
                }

                ClearForm();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private async Task HandleAddAsync()
        {
            try
            {
                var entity = _createEntityFromInputs(null);
                await _addEntityAsync(entity);
                await RefreshDataAsync();
                ShowInfo("Record added successfully.");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private async Task HandleUpdateAsync()
        {
            if (_selectedEntity is null)
            {
                ShowWarning("Please select a record first.");
                return;
            }

            try
            {
                var entity = _createEntityFromInputs(_selectedEntity);
                await _updateEntityAsync(entity);
                await RefreshDataAsync();
                ShowInfo("Record updated successfully.");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private async Task HandleDeleteAsync()
        {
            if (_selectedEntity is null)
            {
                ShowWarning("Please select a record first.");
                return;
            }

            var result = MessageBox.Show(
                "Are you sure you want to delete the selected record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                await _deleteEntityAsync(_selectedEntity);
                await RefreshDataAsync();
                ShowInfo("Record deleted successfully.");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void SelectGridRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= _grid.Rows.Count)
            {
                return;
            }

            _selectedEntity = _grid.Rows[rowIndex].Tag as TEntity;

            if (_selectedEntity is null)
            {
                return;
            }

            _writeEntityToInputs(_selectedEntity);
            SetSelectionState(true);
        }

        private void ClearForm()
        {
            _selectedEntity = null;
            _grid.ClearSelection();
            _clearInputs();
            SetSelectionState(false);
        }

        private void SetSelectionState(bool hasSelection)
        {
            _btnUpdate.Enabled = _supportsUpdate && hasSelection;
            _btnDelete.Enabled = hasSelection;
        }

        private static void ShowInfo(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void ShowWarning(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    internal static class PageInput
    {
        public static string RequiredText(Guna2TextBox textBox, string fieldName)
        {
            string value = textBox.Text.Trim();
            return string.IsNullOrWhiteSpace(value)
                ? throw new ArgumentException($"{fieldName} is required.")
                : value;
        }

        public static int PositiveInt(Guna2TextBox textBox, string fieldName)
        {
            if (!int.TryParse(textBox.Text.Trim(), NumberStyles.Integer, CultureInfo.CurrentCulture, out int value) || value <= 0)
            {
                throw new ArgumentException($"{fieldName} must be greater than zero.");
            }

            return value;
        }

        public static int NonNegativeInt(Guna2TextBox textBox, string fieldName)
        {
            if (!int.TryParse(textBox.Text.Trim(), NumberStyles.Integer, CultureInfo.CurrentCulture, out int value) || value < 0)
            {
                throw new ArgumentException($"{fieldName} cannot be negative.");
            }

            return value;
        }

        public static double NonNegativeDouble(Guna2TextBox textBox, string fieldName)
        {
            if (!double.TryParse(textBox.Text.Trim(), NumberStyles.Float, CultureInfo.CurrentCulture, out double value) || value < 0)
            {
                throw new ArgumentException($"{fieldName} cannot be negative.");
            }

            return value;
        }

        public static int RequiredComboId(Guna2ComboBox comboBox, string fieldName)
        {
            int? id = OptionalComboId(comboBox);
            return id ?? throw new ArgumentException($"Please select {fieldName}.");
        }

        public static int? OptionalComboId(Guna2ComboBox comboBox)
        {
            if (comboBox.SelectedValue is null)
            {
                return null;
            }

            if (comboBox.SelectedValue is int id)
            {
                return id > 0 ? id : null;
            }

            return int.TryParse(comboBox.SelectedValue.ToString(), out int parsedId) && parsedId > 0
                ? parsedId
                : null;
        }

        public static void BindLookup(Guna2ComboBox comboBox, IEnumerable<LookupItem> items, bool includeEmpty = false)
        {
            var lookupItems = new List<LookupItem>();

            if (includeEmpty)
            {
                lookupItems.Add(new LookupItem(null, "None"));
            }

            lookupItems.AddRange(items);
            comboBox.DataSource = lookupItems;
            comboBox.DisplayMember = nameof(LookupItem.Text);
            comboBox.ValueMember = nameof(LookupItem.Id);
            comboBox.SelectedIndex = lookupItems.Count > 0 ? 0 : -1;
        }

        public static void SetComboValue(Guna2ComboBox comboBox, int? id)
        {
            comboBox.SelectedValue = id;

            if (comboBox.SelectedIndex < 0 && comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        public static DateTime TimeToPickerValue(TimeSpan time)
        {
            return DateTime.Today.Add(time);
        }

        public static TimeSpan PickerToTime(Guna2DateTimePicker picker)
        {
            return picker.Value.TimeOfDay;
        }
    }

    internal sealed record LookupItem(int? Id, string Text);
}
