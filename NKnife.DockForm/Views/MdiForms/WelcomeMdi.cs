using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Window.Views.MdiForms
{
    public partial class WelcomeMdi : DockContent
    {
        public WelcomeMdi()
        {
            InitializeComponent();
        }

        #region 单件实例

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static WelcomeMdi ME
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            internal static readonly WelcomeMdi Instance;

            static Singleton()
            {
                Instance = new WelcomeMdi();
            }
        }

        #endregion
    }
}