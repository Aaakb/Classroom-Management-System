using Guna.UI2.WinForms;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SchedulesForm
    {
        private static void BindCombo(Guna2ComboBox combo, IEnumerable<ComboOption> options)
        {
            ComboBoxHelper.Bind(combo, options);
        }

        private static void BindFilterCombo(
            Guna2ComboBox combo,
            IEnumerable<ComboOption> options,
            string allText)
        {
            var items = new List<ComboOption> { new(null, allText) };
            items.AddRange(options);
            ComboBoxHelper.Bind(combo, items, selectedIndex: 0);
        }

        private static int GetSelectedRequiredId(Guna2ComboBox combo)
        {
            return ComboBoxHelper.GetSelectedRequiredId(combo);
        }

        private static int? GetSelectedOptionalId(Guna2ComboBox combo)
        {
            return ComboBoxHelper.GetSelectedOptionalId(combo);
        }

        private static void SelectComboValue(Guna2ComboBox combo, int? id)
        {
            ComboBoxHelper.SelectValue(combo, id, selectNullOption: false);
        }

        private static void ClearCombo(Guna2ComboBox combo)
        {
            ComboBoxHelper.Clear(combo);
        }

        private static string? GetSelectedPlainText(Guna2ComboBox combo)
        {
            return ComboBoxHelper.GetSelectedPlainText(combo);
        }

        private static void SelectComboText(Guna2ComboBox combo, string? text)
        {
            ComboBoxHelper.SelectText(combo, text);
        }
    }
}
