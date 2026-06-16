namespace University_Timetable_and_Classroom_Management_System
{
    internal static class FormNavigation
    {
        public static void Open(System.Windows.Forms.Form currentForm, System.Windows.Forms.Form nextForm)
        {
            if (currentForm.GetType() == nextForm.GetType())
            {
                nextForm.Dispose();
                return;
            }

            nextForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            nextForm.Show();
            currentForm.Hide();
            nextForm.FormClosed += (_, _) => currentForm.Close();
        }
    }
}
