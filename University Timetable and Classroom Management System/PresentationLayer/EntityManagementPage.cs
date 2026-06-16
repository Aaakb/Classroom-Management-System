using System.Globalization;
using Guna.UI2.WinForms;

namespace University_Timetable_and_Classroom_Management_System
{
    internal abstract class EntityManagementPage<TEntity> : System.Windows.Forms.UserControl
        where TEntity : class
    {
        private const int PageMargin = 28;
        private const int MinimumContentWidth = 900;
        private const int PanelInnerMargin = 24;

        private readonly List<GridColumn<TEntity>> _gridColumns = new();
        private readonly int _fieldColumns;
        private readonly int _editorHeight;
        private readonly Guna2Panel _rootPanel;
        private readonly Guna2Panel _tablePanel;
        private int _fieldIndex;
        private TEntity? _selectedEntity;

        protected readonly Guna2Panel EditorPanel;
        protected readonly Guna2DataGridView DataGrid;
        protected readonly ErrorProvider ErrorProvider;

        private readonly Guna2Button _btnAdd;
        private readonly Guna2Button _btnUpdate;
        private readonly Guna2Button _btnDelete;
        private readonly Guna2Button _btnClear;
        private readonly Guna2Button _btnRefresh;

        protected EntityManagementPage(
            string pageTitle,
            string pageSubtitle,
            string editorTitle,
            string editorSubtitle,
            int editorHeight = 210,
            int fieldColumns = 3)
        {
            _editorHeight = editorHeight;
            _fieldColumns = fieldColumns;

            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(245, 247, 250);
            ErrorProvider = new ErrorProvider { BlinkStyle = ErrorBlinkStyle.NeverBlink };

            _rootPanel = new Guna2Panel
            {
                AutoScroll = true,
                Dock = DockStyle.Fill,
                FillColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(PageMargin)
            };

            var lblPageTitle = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(28, 22),
                Text = pageTitle
            };

            var lblPageSubtitle = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(30, 58),
                Text = pageSubtitle
            };

            EditorPanel = new Guna2Panel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent,
                BorderColor = Color.FromArgb(226, 232, 240),
                BorderRadius = 8,
                BorderThickness = 1,
                FillColor = Color.White,
                Location = new Point(28, 94),
                Size = new Size(900, _editorHeight)
            };

            var lblEditorTitle = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(24, 18),
                Text = editorTitle
            };

            var lblEditorSubtitle = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(24, 45),
                Text = editorSubtitle
            };

            _btnAdd = CreateActionButton("Add", Color.FromArgb(22, 163, 74), Color.FromArgb(21, 128, 61), 0, 0);
            _btnUpdate = CreateActionButton("Update", Color.FromArgb(14, 116, 144), Color.FromArgb(21, 94, 117), 1, 0);
            _btnDelete = CreateActionButton("Delete", Color.FromArgb(220, 38, 38), Color.FromArgb(185, 28, 28), 0, 1);
            _btnClear = CreateSecondaryButton("Clear", 1, 1);
            _btnRefresh = CreateSecondaryButton("Refresh", 0, 2);

            _btnAdd.Click += async (_, _) => await HandleAddAsync();
            _btnUpdate.Click += async (_, _) => await HandleUpdateAsync();
            _btnDelete.Click += async (_, _) => await HandleDeleteAsync();
            _btnClear.Click += (_, _) => ClearForm();
            _btnRefresh.Click += async (_, _) => await RefreshDataAsync();

            EditorPanel.Controls.Add(lblEditorTitle);
            EditorPanel.Controls.Add(lblEditorSubtitle);
            EditorPanel.Controls.Add(_btnAdd);
            EditorPanel.Controls.Add(_btnUpdate);
            EditorPanel.Controls.Add(_btnDelete);
            EditorPanel.Controls.Add(_btnClear);
            EditorPanel.Controls.Add(_btnRefresh);

            _tablePanel = new Guna2Panel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent,
                BorderColor = Color.FromArgb(226, 232, 240),
                BorderRadius = 8,
                BorderThickness = 1,
                FillColor = Color.White,
                Location = new Point(28, 116 + _editorHeight),
                Size = new Size(900, 420)
            };

            var lblTableTitle = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(24, 18),
                Text = $"{pageTitle} List"
            };

            DataGrid = CreateGrid();
            DataGrid.Location = new Point(24, 64);
            DataGrid.Size = new Size(852, 330);
            DataGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DataGrid.CellClick += (_, e) => SelectGridRow(e.RowIndex);

            _tablePanel.Controls.Add(lblTableTitle);
            _tablePanel.Controls.Add(DataGrid);

            _rootPanel.Controls.Add(lblPageTitle);
            _rootPanel.Controls.Add(lblPageSubtitle);
            _rootPanel.Controls.Add(EditorPanel);
            _rootPanel.Controls.Add(_tablePanel);
            Controls.Add(_rootPanel);

            SetSelectionState(false);
            ApplyResponsiveLayout();
        }

        protected virtual bool SupportsUpdate => true;

        protected abstract Task<IReadOnlyList<TEntity>> LoadEntitiesAsync();

        protected abstract Task AddEntityAsync(TEntity entity);

        protected abstract Task UpdateEntityAsync(TEntity entity);

        protected abstract Task DeleteEntityAsync(TEntity entity);

        protected abstract TEntity CreateEntityFromInputs(TEntity? selectedEntity);

        protected abstract void WriteEntityToInputs(TEntity entity);

        protected abstract void ClearInputControls();

        protected virtual Task LoadLookupDataAsync()
        {
            return Task.CompletedTask;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode)
            {
                return;
            }

            await InitializeAsync();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyResponsiveLayout();
        }

        protected void AddGridColumn(string headerText, Func<TEntity, object?> valueSelector, float fillWeight = 100F)
        {
            DataGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = headerText,
                Name = $"col{DataGrid.Columns.Count + 1}",
                ReadOnly = true,
                FillWeight = fillWeight
            });

            _gridColumns.Add(new GridColumn<TEntity>(valueSelector));
        }

        protected Guna2TextBox AddTextField(string labelText, string placeholderText, bool readOnly = false)
        {
            var bounds = GetNextFieldBounds();
            var label = CreateFieldLabel(labelText, bounds.LabelLocation);
            var textBox = new Guna2TextBox
            {
                BorderColor = readOnly ? Color.FromArgb(226, 232, 240) : Color.FromArgb(203, 213, 225),
                BorderRadius = 8,
                Cursor = Cursors.IBeam,
                Enabled = !readOnly,
                FillColor = readOnly ? Color.FromArgb(248, 250, 252) : Color.White,
                FocusedState = { BorderColor = Color.FromArgb(37, 99, 235) },
                Font = new Font("Segoe UI", 10F),
                ForeColor = readOnly ? Color.FromArgb(100, 116, 139) : Color.FromArgb(15, 23, 42),
                HoverState = { BorderColor = Color.FromArgb(59, 130, 246) },
                Location = bounds.ControlLocation,
                Name = $"txt{labelText.Replace(" ", string.Empty)}",
                PlaceholderForeColor = Color.FromArgb(148, 163, 184),
                PlaceholderText = placeholderText,
                Size = new Size(bounds.Width, 42)
            };

            EditorPanel.Controls.Add(label);
            EditorPanel.Controls.Add(textBox);
            return textBox;
        }

        protected Guna2ComboBox AddComboField(string labelText)
        {
            var bounds = GetNextFieldBounds();
            var label = CreateFieldLabel(labelText, bounds.LabelLocation);
            var comboBox = new Guna2ComboBox
            {
                BackColor = Color.Transparent,
                BorderColor = Color.FromArgb(203, 213, 225),
                BorderRadius = 8,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FocusedColor = Color.FromArgb(37, 99, 235),
                FocusedState = { BorderColor = Color.FromArgb(37, 99, 235) },
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(15, 23, 42),
                HoverState = { BorderColor = Color.FromArgb(59, 130, 246) },
                ItemHeight = 36,
                Location = bounds.ControlLocation,
                Name = $"cmb{labelText.Replace(" ", string.Empty)}",
                Size = new Size(bounds.Width, 42)
            };

            EditorPanel.Controls.Add(label);
            EditorPanel.Controls.Add(comboBox);
            return comboBox;
        }

        protected Guna2DateTimePicker AddTimeField(string labelText)
        {
            var bounds = GetNextFieldBounds();
            var label = CreateFieldLabel(labelText, bounds.LabelLocation);
            var picker = new Guna2DateTimePicker
            {
                BorderRadius = 8,
                Checked = true,
                CustomFormat = "HH:mm",
                FillColor = Color.White,
                Font = new Font("Segoe UI", 10F),
                Format = DateTimePickerFormat.Custom,
                Location = bounds.ControlLocation,
                Name = $"dtp{labelText.Replace(" ", string.Empty)}",
                ShowUpDown = true,
                Size = new Size(bounds.Width, 42)
            };

            EditorPanel.Controls.Add(label);
            EditorPanel.Controls.Add(picker);
            return picker;
        }

        protected Guna2ToggleSwitch AddToggleField(string labelText)
        {
            var bounds = GetNextFieldBounds();
            var label = CreateFieldLabel(labelText, bounds.LabelLocation);
            var toggle = new Guna2ToggleSwitch
            {
                CheckedState = { FillColor = Color.FromArgb(37, 99, 235) },
                Location = new Point(bounds.ControlLocation.X, bounds.ControlLocation.Y + 7),
                Name = $"tgl{labelText.Replace(" ", string.Empty)}",
                Size = new Size(52, 28),
                UncheckedState = { FillColor = Color.FromArgb(203, 213, 225) }
            };

            EditorPanel.Controls.Add(label);
            EditorPanel.Controls.Add(toggle);
            return toggle;
        }

        protected static string RequiredText(Guna2TextBox textBox, string fieldName)
        {
            string value = textBox.Text.Trim();
            return string.IsNullOrWhiteSpace(value)
                ? throw new ArgumentException($"{fieldName} is required.")
                : value;
        }

        protected static string? OptionalText(Guna2TextBox textBox)
        {
            string value = textBox.Text.Trim();
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        protected static int PositiveInt(Guna2TextBox textBox, string fieldName)
        {
            if (!int.TryParse(textBox.Text.Trim(), NumberStyles.Integer, CultureInfo.CurrentCulture, out int value) || value <= 0)
            {
                throw new ArgumentException($"{fieldName} must be greater than zero.");
            }

            return value;
        }

        protected static int NonNegativeInt(Guna2TextBox textBox, string fieldName)
        {
            if (!int.TryParse(textBox.Text.Trim(), NumberStyles.Integer, CultureInfo.CurrentCulture, out int value) || value < 0)
            {
                throw new ArgumentException($"{fieldName} cannot be negative.");
            }

            return value;
        }

        protected static double NonNegativeDouble(Guna2TextBox textBox, string fieldName)
        {
            if (!double.TryParse(textBox.Text.Trim(), NumberStyles.Float, CultureInfo.CurrentCulture, out double value) || value < 0)
            {
                throw new ArgumentException($"{fieldName} cannot be negative.");
            }

            return value;
        }

        protected static int RequiredComboId(Guna2ComboBox comboBox, string fieldName)
        {
            int? id = OptionalComboId(comboBox);
            return id ?? throw new ArgumentException($"Please select {fieldName}.");
        }

        protected static int? OptionalComboId(Guna2ComboBox comboBox)
        {
            if (comboBox.SelectedValue is null)
            {
                return null;
            }

            if (comboBox.SelectedValue is int id)
            {
                return id > 0 ? id : null;
            }

            if (int.TryParse(comboBox.SelectedValue.ToString(), out int parsedId))
            {
                return parsedId > 0 ? parsedId : null;
            }

            return null;
        }

        protected static void BindLookup(Guna2ComboBox comboBox, IEnumerable<LookupItem> items, bool includeEmpty = false)
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

        protected static void SetComboValue(Guna2ComboBox comboBox, int? id)
        {
            comboBox.SelectedValue = id;

            if (comboBox.SelectedIndex < 0 && comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        protected static DateTime TimeToPickerValue(TimeSpan time)
        {
            return DateTime.Today.Add(time);
        }

        protected static TimeSpan PickerToTime(Guna2DateTimePicker picker)
        {
            return picker.Value.TimeOfDay;
        }

        private async Task InitializeAsync()
        {
            await LoadLookupDataAsync();
            await RefreshDataAsync();
            ClearForm();
        }

        private async Task RefreshDataAsync()
        {
            try
            {
                UseWaitCursor = true;
                var items = await LoadEntitiesAsync();
                DataGrid.Rows.Clear();

                foreach (var item in items)
                {
                    object?[] values = _gridColumns
                        .Select(column => column.ValueSelector(item) ?? string.Empty)
                        .ToArray();

                    int rowIndex = DataGrid.Rows.Add(values);
                    DataGrid.Rows[rowIndex].Tag = item;
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private async Task HandleAddAsync()
        {
            try
            {
                var entity = CreateEntityFromInputs(null);
                await AddEntityAsync(entity);
                await RefreshDataAsync();
                ClearForm();
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
                var entity = CreateEntityFromInputs(_selectedEntity);
                await UpdateEntityAsync(entity);
                await RefreshDataAsync();
                ClearForm();
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
                await DeleteEntityAsync(_selectedEntity);
                await RefreshDataAsync();
                ClearForm();
                ShowInfo("Record deleted successfully.");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void SelectGridRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= DataGrid.Rows.Count)
            {
                return;
            }

            _selectedEntity = DataGrid.Rows[rowIndex].Tag as TEntity;

            if (_selectedEntity is null)
            {
                return;
            }

            WriteEntityToInputs(_selectedEntity);
            SetSelectionState(true);
        }

        private void ClearForm()
        {
            ErrorProvider.Clear();
            _selectedEntity = null;
            DataGrid.ClearSelection();
            ClearInputControls();
            SetSelectionState(false);
        }

        private void SetSelectionState(bool hasSelection)
        {
            _btnUpdate.Enabled = SupportsUpdate && hasSelection;
            _btnDelete.Enabled = hasSelection;
        }

        private void ApplyResponsiveLayout()
        {
            if (_rootPanel is null || _tablePanel is null || DataGrid is null)
            {
                return;
            }

            int contentWidth = Math.Max(MinimumContentWidth, Width - (PageMargin * 2));
            int tableTop = 116 + _editorHeight;
            int tableHeight = Math.Max(300, Height - tableTop - PageMargin);

            EditorPanel.Width = contentWidth;
            EditorPanel.Height = _editorHeight;
            _tablePanel.Location = new Point(PageMargin, tableTop);
            _tablePanel.Size = new Size(contentWidth, tableHeight);
            DataGrid.Size = new Size(contentWidth - (PanelInnerMargin * 2), Math.Max(220, tableHeight - 88));
            _rootPanel.AutoScrollMinSize = new Size(contentWidth + (PageMargin * 2), tableTop + tableHeight + PageMargin);
        }

        private Guna2DataGridView CreateGrid()
        {
            var grid = new Guna2DataGridView
            {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                ColumnHeadersHeight = 44,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                EnableHeadersVisualStyles = false,
                GridColor = Color.FromArgb(226, 232, 240),
                MultiSelect = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = Color.FromArgb(30, 41, 59),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(30, 64, 175)
            };

            grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                BackColor = Color.FromArgb(15, 23, 42),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(15, 23, 42),
                SelectionForeColor = Color.White,
                WrapMode = DataGridViewTriState.True
            };

            grid.DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(30, 41, 59),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(30, 64, 175),
                WrapMode = DataGridViewTriState.False
            };

            grid.RowTemplate.Height = 42;
            return grid;
        }

        private FieldBounds GetNextFieldBounds()
        {
            const int left = 24;
            const int top = 88;
            const int fieldWidth = 180;
            const int xSpacing = 204;
            const int ySpacing = 74;

            int column = _fieldIndex % _fieldColumns;
            int row = _fieldIndex / _fieldColumns;
            _fieldIndex++;

            int x = left + (column * xSpacing);
            int y = top + (row * ySpacing);
            return new FieldBounds(new Point(x, y), new Point(x, y + 25), fieldWidth);
        }

        private static Guna2HtmlLabel CreateFieldLabel(string text, Point location)
        {
            return new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                Location = location,
                Text = text
            };
        }

        private Guna2Button CreateActionButton(string text, Color fillColor, Color hoverColor, int column, int row)
        {
            var button = CreateButtonBase(text, column, row);
            button.FillColor = fillColor;
            button.ForeColor = Color.White;
            button.HoverState.FillColor = hoverColor;
            return button;
        }

        private Guna2Button CreateSecondaryButton(string text, int column, int row)
        {
            var button = CreateButtonBase(text, column, row);
            button.BorderColor = Color.FromArgb(203, 213, 225);
            button.BorderThickness = 1;
            button.FillColor = Color.White;
            button.ForeColor = Color.FromArgb(51, 65, 85);
            button.HoverState.FillColor = Color.FromArgb(248, 250, 252);
            return button;
        }

        private Guna2Button CreateButtonBase(string text, int column, int row)
        {
            const int panelWidth = 900;
            const int buttonWidth = 108;
            const int buttonHeight = 38;
            const int rightMargin = 24;
            const int gap = 12;

            int rightButtonX = panelWidth - rightMargin - buttonWidth;
            int leftButtonX = rightButtonX - buttonWidth - gap;
            int x = column == 0 ? leftButtonX : rightButtonX;
            int y = 88 + (row * 44);

            return new Guna2Button
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BorderRadius = 8,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                Location = new Point(x, y),
                Name = $"btn{text.Replace(" ", string.Empty)}",
                Size = new Size(buttonWidth, buttonHeight),
                Text = text
            };
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

        protected sealed record LookupItem(int? Id, string Text);

        private sealed record GridColumn<T>(Func<T, object?> ValueSelector);

        private sealed record FieldBounds(Point LabelLocation, Point ControlLocation, int Width);
    }
}
