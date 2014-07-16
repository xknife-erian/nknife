using System;
using System.Windows.Forms;
using NKnife.DockForm.Controls;

namespace NKnife.DockForm.Views.MdiForms
{
    public class UserManagerMdi : GridViewDockContent
    {
        private UserManagerMdi()
        {
            Text = "用户管理";
            EventRegister();
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void EventRegister()
        {
            
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            Form mainForm = FindForm();
            if (mainForm != null)
                mainForm.Cursor = Cursors.WaitCursor;
            if (mainForm != null)
                mainForm.Cursor = Cursors.Default;
        }

        #region 单件实例

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static UserManagerMdi ME
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            internal static readonly UserManagerMdi Instance;

            static Singleton()
            {
                Instance = new UserManagerMdi();
            }
        }

        #endregion
    }
}