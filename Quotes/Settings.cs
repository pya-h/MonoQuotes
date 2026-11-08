using System;


namespace Quotes
{
    public enum FontSizes
    {
        Small = 0, Medium , Large
    };

    public enum Themes
    {
        Light = 10, Dark
    };

    static class Settings
    {
        private static FontSizes appFontSize = FontSizes.Small;
        private static Themes appTheme = Themes.Light;

        public static FontSizes AppFontSize
        {
            get { return appFontSize; }
            set
            {
                appFontSize = value;
                if (!Enum.IsDefined(typeof(FontSizes), appFontSize))
                    appFontSize = FontSizes.Small;
            }
        }
        public static Themes AppTheme
        {
            get { return appTheme; }
            set
            {
                appTheme = value;
                if (!Enum.IsDefined(typeof(Themes), appTheme))
                    appTheme = Themes.Light;
            }
        }

        public static bool Save()
        {

            return false;
        }

        public static void Load()
        {

        }
    }
}