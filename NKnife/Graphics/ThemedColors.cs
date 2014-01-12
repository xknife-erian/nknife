using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace NKnife.Graphics
{
    public class ThemedColors
    {
        #region "Variables and Constants "

        private const string NormalColor = "NormalColor";
        private const string HomeStead = "HomeStead";
        private const string Metallic = "Metallic";
        private const string NoTheme = "NoTheme";
        private static Color[] _ToolBorder;

        #endregion

        #region "Properties "

        public static int CurrentThemeIndex
        {
            get { return GetCurrentThemeIndex(); }
        }

        public static string CurrentThemeName
        {
            get { return GetCurrentThemeName(); }
        }

        public static Color ToolBorder
        {
            get { return _ToolBorder[CurrentThemeIndex]; }
        }

        #endregion

        #region "Constructors "

        static ThemedColors()
        {
            var colorArray1 = new[]
                                  {
                                      Color.FromArgb(127, 157, 185), Color.FromArgb(164, 185, 127),
                                      Color.FromArgb(165, 172, 178), Color.FromArgb(132, 130, 132)
                                  };
            _ToolBorder = colorArray1;
        }

        private ThemedColors()
        {
        }

        #endregion

        #region ColorScheme enum

        public enum ColorScheme
        {
            NormalColor = 0,
            HomeStead = 1,
            Metallic = 2,
            NoTheme = 3
        }

        #endregion

        private static int GetCurrentThemeIndex()
        {
            var theme = (int) ColorScheme.NoTheme;
            if (VisualStyleInformation.IsSupportedByOS && VisualStyleInformation.IsEnabledByUser &&
                Application.RenderWithVisualStyles)
            {
                switch (VisualStyleInformation.ColorScheme)
                {
                    case NormalColor:
                        theme = (int) ColorScheme.NormalColor;
                        break;
                    case HomeStead:
                        theme = (int) ColorScheme.HomeStead;
                        break;
                    case Metallic:
                        theme = (int) ColorScheme.Metallic;
                        break;
                    default:
                        theme = (int) ColorScheme.NoTheme;
                        break;
                }
            }
            return theme;
        }

        private static string GetCurrentThemeName()
        {
            string theme = NoTheme;
            if (VisualStyleInformation.IsSupportedByOS && VisualStyleInformation.IsEnabledByUser &&
                Application.RenderWithVisualStyles)
            {
                theme = VisualStyleInformation.ColorScheme;
            }
            return theme;
        }
    }
}