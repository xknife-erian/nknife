using System;
using System.Threading;
using System.Windows.Forms;

namespace NKnife.Window.Views
{
    /// <summary>
    /// 登录窗口
    /// </summary>
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            _LoginButton.Enabled = false;

            //判断自动登录与记住登录信息
            _RememberCheckBox.Checked = true;//DesktopUserDataOption.ME.IsRememberLoginInfo;

            //自动登录与记住登录信息的选择逻辑
            _LoginNameCmbox.SelectedIndexChanged += SelectedIndexChanged;
            _RememberCheckBox.CheckedChanged += RememberCheckedChanged;
        }

        private void InitLoginName()
        {
            _LoginNameCmbox.Text = "Staff";
        }

        /// <summary>选择一个较靠后的事件，用来加载控件所需的数据
        /// </summary>
        /// <param name="e">一个包含事件数据的 <see cref="T:System.EventArgs"/>。</param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        /// <summary>
        /// 登录按键点击后的执行方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButtonClick(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the _CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #region LoginedEventArgs

        #region Delegates

        public delegate void LoginedEventHandler(Object sender, LoginedEventArgs e);

        #endregion

        /// <summary>
        /// 尝试登录后发生的事件
        /// </summary>
        public event LoginedEventHandler LoginedEvent;

        protected virtual void OnLogined(LoginedEventArgs e)
        {
            if (LoginedEvent != null)
                LoginedEvent(this, e);
        }

        /// <summary>
        /// 当事件发生时包含事件数据的类
        /// </summary>
        public class LoginedEventArgs : EventArgs
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="LoginedEventArgs"/> class.
            /// </summary>
            /// <param name="isLoginSuceess">if set to <c>true</c> [是否登录成功].</param>
            public LoginedEventArgs(bool isLoginSuceess)
            {
                IsLoginSuceess = isLoginSuceess;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="LoginedEventArgs"/> class.
            /// </summary>
            /// <param name="errorMessage">The error message.</param>
            public LoginedEventArgs(string errorMessage)
            {
                IsLoginSuceess = false;
                LoginErrorMessage = errorMessage;
            }

            /// <summary>
            /// Gets or sets a value indicating whether this instance 是否登录成功。
            /// </summary>
            /// <value>
            /// 	<c>true</c> if this instance is login suceess; otherwise, <c>false</c>.
            /// </value>
            public bool IsLoginSuceess { get; private set; }

            public string LoginErrorMessage { get; private set; }
        }

        #endregion

        #region 界面逻辑的一些事件方法

        protected virtual void RememberCheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox) sender;
        }

        protected virtual void AutoLoginCheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox) sender;
            if (checkBox.Checked)
                _RememberCheckBox.Checked = true;
        }

        protected virtual void SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_LoginNameCmbox.SelectedItem != null)
                _LoginButton.Enabled = true;
        }

        /// <summary>保存登录信息到用户配置文件
        /// </summary>
        private void SaveLoginInformation()
        {
        }

        #endregion

        #region 填充窗口中的控件中的数据

        /// <summary>自动登录
        /// </summary>
        private void AutomaticLogin()
        {
            Thread.Sleep(2000);
            LoginButtonClick(null, EventArgs.Empty);
        }

        protected virtual void AysnSetCmbWorker(object[] datas)
        {
            if (InvokeRequired)
            {
                SetComboxData scd = AysnSetCmbWorker;
                BeginInvoke(scd, new object[] {datas});
            }
            else
            {
                _LoginNameCmbox.Items.AddRange(datas);
            }
        }

        protected virtual void AysnSetCmbCounter(object[] datas)
        {
            if (InvokeRequired)
            {
                SetComboxData scd = AysnSetCmbCounter;
                BeginInvoke(scd, new object[] {datas});
            }
            else
            {
            }
        }

        private delegate void SetComboxData(object[] datas);

        #endregion
    }
}