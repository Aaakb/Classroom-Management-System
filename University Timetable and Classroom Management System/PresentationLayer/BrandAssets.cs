namespace University_Timetable_and_Classroom_Management_System
{
    internal static class BrandAssets
    {
        private const string AssetsFolder = "Assets";
        private const string LogoFileName = "AppLogo.png";
        private const string IconFileName = "AppIcon.ico";

        public static Image? LoadLogoImage()
        {
            string path = GetAssetPath(LogoFileName);
            return File.Exists(path) ? Image.FromFile(path) : null;
        }

        public static Icon? LoadIcon()
        {
            string path = GetAssetPath(IconFileName);
            return File.Exists(path) ? new Icon(path) : null;
        }

        private static string GetAssetPath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, AssetsFolder, fileName);
        }
    }
}
