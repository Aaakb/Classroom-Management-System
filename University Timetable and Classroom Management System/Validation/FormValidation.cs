using System;
using System.Linq;
using System.Windows.Forms;

namespace University_Timetable_and_Classroom_Management_System.PresentationLayer.Validation
{
    public static class FormValidation
    {
        private static bool ShowError(Control control, string message, ErrorProvider? errorProvider = null)
        {
            if (errorProvider != null)
            {
                errorProvider.SetError(control, message);
            }
            else
            {
                MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            control.Focus();
            return false;
        }

        private static void ClearError(Control control, ErrorProvider? errorProvider = null)
        {
            errorProvider?.SetError(control, string.Empty);
        }

        public static string Clean(TextBoxBase textBox)
        {
            return textBox.Text.Trim();
        }

        public static string? CleanNullable(TextBoxBase textBox)
        {
            string value = textBox.Text.Trim();
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        public static bool Required(TextBoxBase textBox, string fieldName, ErrorProvider? errorProvider = null)
        {
            ClearError(textBox, errorProvider);

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                return ShowError(textBox, $"{fieldName} is required.", errorProvider);
            }

            return true;
        }

        public static bool MaxLength(TextBoxBase textBox, int maxLength, string fieldName, ErrorProvider? errorProvider = null)
        {
            ClearError(textBox, errorProvider);

            if (!string.IsNullOrWhiteSpace(textBox.Text) && textBox.Text.Trim().Length > maxLength)
            {
                return ShowError(textBox, $"{fieldName} must not exceed {maxLength} characters.", errorProvider);
            }

            return true;
        }

        public static bool RequiredMaxLength(TextBoxBase textBox, int maxLength, string fieldName, ErrorProvider? errorProvider = null)
        {
            return Required(textBox, fieldName, errorProvider)
                && MaxLength(textBox, maxLength, fieldName, errorProvider);
        }

        public static bool PositiveInteger(TextBoxBase textBox, string fieldName, out int value, ErrorProvider? errorProvider = null)
        {
            ClearError(textBox, errorProvider);

            if (!int.TryParse(textBox.Text.Trim(), out value))
            {
                return ShowError(textBox, $"{fieldName} must be a valid number.", errorProvider);
            }

            if (value <= 0)
            {
                return ShowError(textBox, $"{fieldName} must be greater than zero.", errorProvider);
            }

            return true;
        }

        public static bool NonNegativeInteger(TextBoxBase textBox, string fieldName, out int value, ErrorProvider? errorProvider = null)
        {
            ClearError(textBox, errorProvider);

            if (!int.TryParse(textBox.Text.Trim(), out value))
            {
                return ShowError(textBox, $"{fieldName} must be a valid number.", errorProvider);
            }

            if (value < 0)
            {
                return ShowError(textBox, $"{fieldName} cannot be negative.", errorProvider);
            }

            return true;
        }

        public static bool SelectedComboBox(ComboBox comboBox, string fieldName, ErrorProvider? errorProvider = null)
        {
            ClearError(comboBox, errorProvider);

            if (comboBox.SelectedIndex < 0 || comboBox.SelectedValue == null)
            {
                return ShowError(comboBox, $"Please select {fieldName}.", errorProvider);
            }

            return true;
        }

        public static bool SelectedId(ComboBox comboBox, string fieldName, out int id, ErrorProvider? errorProvider = null)
        {
            id = 0;

            if (!SelectedComboBox(comboBox, fieldName, errorProvider))
            {
                return false;
            }

            if (!int.TryParse(comboBox.SelectedValue.ToString(), out id) || id <= 0)
            {
                return ShowError(comboBox, $"Please select a valid {fieldName}.", errorProvider);
            }

            return true;
        }

        public static bool ValidSemester(TextBoxBase textBox, out int semesterNumber, ErrorProvider? errorProvider = null)
        {
            ClearError(textBox, errorProvider);

            if (!int.TryParse(textBox.Text.Trim(), out semesterNumber))
            {
                return ShowError(textBox, "Semester number must be a valid number.", errorProvider);
            }

            if (semesterNumber != 1 && semesterNumber != 2)
            {
                return ShowError(textBox, "Semester number must be 1 or 2.", errorProvider);
            }

            return true;
        }

        public static bool ValidTimeRange(DateTimePicker startPicker, DateTimePicker endPicker, ErrorProvider? errorProvider = null)
        {
            ClearError(startPicker, errorProvider);
            ClearError(endPicker, errorProvider);

            TimeSpan startTime = startPicker.Value.TimeOfDay;
            TimeSpan endTime = endPicker.Value.TimeOfDay;

            if (endTime <= startTime)
            {
                return ShowError(endPicker, "End time must be after start time.", errorProvider);
            }

            return true;
        }

        public static bool ValidDayOfWeek(ComboBox comboBox, ErrorProvider? errorProvider = null)
        {
            ClearError(comboBox, errorProvider);

            string[] validDays =
            {
                "Sunday",
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday"
            };

            if (comboBox.SelectedItem == null)
            {
                return ShowError(comboBox, "Please select day of week.", errorProvider);
            }

            string day = comboBox.SelectedItem.ToString() ?? string.Empty;

            if (!validDays.Contains(day))
            {
                return ShowError(comboBox, "Invalid day of week.", errorProvider);
            }

            return true;
        }

        public static bool RequiredTextComboBox(ComboBox comboBox, string fieldName, ErrorProvider? errorProvider = null)
        {
            ClearError(comboBox, errorProvider);

            if (string.IsNullOrWhiteSpace(comboBox.Text))
            {
                return ShowError(comboBox, $"{fieldName} is required.", errorProvider);
            }

            return true;
        }

        public static void ClearAllErrors(ErrorProvider errorProvider, params Control[] controls)
        {
            foreach (Control control in controls)
            {
                errorProvider.SetError(control, string.Empty);
            }
        }
    }
}