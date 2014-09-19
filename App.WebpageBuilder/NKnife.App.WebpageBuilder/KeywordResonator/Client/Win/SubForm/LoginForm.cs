using Jeelu.Win;
using System.Windows.Forms;
using Jeelu.Billboard;
using System.Diagnostics;

namespace Jeelu.KeywordResonator.Client
{
    /* 登录流程说明
     * 1.1 用户打开本窗体后，点击重置密码，远端将随机生成一个12位的密码，并发送一封含密码的邮件
     * 1.2 服务器接收到重置密码的信号后，如发现<“已有客户端登录”的标记>，并不生成密码，但发送一封
     *     有关<“已有客户端登录”的标记>信息的邮件：1.登录者, 2.登录者IP, 3.登录时间, 4.当前密码
     * 1.2.1 服务器发送不能生成密码的信号，并通知客户端：1.登录者, 2.登录者IP, 3.登录时间
     * 2. 用户通过邮件得到密码，输入密码，由窗体发送给远端
     * 3. 远端比较密码后，如果校验不正确，窗体将接收一个密码不符的信号
     * 4. 远端比较密码后，如果校验正确，远端将保存“已有客户端登录”的标记，并返回一个登录成功的信号
     * 5. 远端比较密码后，如果校验正确，但<“已有客户端登录”的标记>为真，
     *    远端将返回登录成功，但同时返回一个不允许客户端登录的信号,
     *    并通知客户端：1.登录者, 2.登录者IP, 3.登录时间
     * 6. 客户端提示用户是否强行登录。
     */

    /// <summary>
    /// 应用程序的登录窗体。
    /// </summary>
    internal partial class LoginForm : BaseForm
    {
        string password;
        string errorInfo;
        /// <summary>
        /// 构造函数
        /// </summary>
        internal LoginForm()
        {
            InitializeComponent();
        }

        private void _OkButton_Click(object sender, System.EventArgs e)
        {
            password = this._PwdTextBox.Text;
            
            ////验证

            if (CheckUser())
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                _ErrorLabel.Text = errorInfo;
                this._PwdTextBox.Focus();
                this._PwdTextBox.SelectAll();
                return;
            }
            
        }

        private void _CancelButton_Click(object sender, System.EventArgs e)
        {
            //维持原状
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }


        /// <summary>
        /// 验证用户
        /// </summary>
        private bool CheckUser()
        {
            ClientServer _client = new ClientServer();
            _client.StartConn();

            MessageHead head = new MessageHead ((int)KeywordMessageType.User ,0,1);
            byte[] bodyBytes = Service.GetBodyBytes(password, Service.DictVersion, Service.ComputerMac);
            MessageBag bag = new MessageBag(head,bodyBytes);
            
            _client.SendMessage(bag);

            MessageBag recBag = _client.ReceiveMessage();
            if (recBag == null)
            {
                Debug.Fail ("有错");
            }

            string returnVal = _client.AnalyzeResponeMessage(recBag);

            _client.CloseConn();

            if (string.Compare(returnVal, "true") != 0)
            {
                errorInfo = returnVal;
                return false;
            }
            return true;
        }

        private void _PwdTextBox_TextChanged(object sender, System.EventArgs e)
        {
            string text = this._PwdTextBox.Text;
            if (!string.IsNullOrEmpty(this._ErrorLabel.Text))
            {
                this._ErrorLabel.Text = "";
            }
        }

    }
}
