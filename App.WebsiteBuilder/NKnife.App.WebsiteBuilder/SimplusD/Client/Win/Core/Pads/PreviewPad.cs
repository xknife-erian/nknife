using System;
using System.Drawing;
using System.Windows.Forms;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public class PreviewPad : PadContent
    {
        #region 构造函数
        static private PreviewPad _instance;

        /// <summary>
        /// 预览面板
        /// </summary>
        public PreviewPad()
        {
            _instance = this;
            this.Text = StringParserService.Parse("${res:Pad.Preview.text}");
            this.Icon = Icon.FromHandle(ResourceService.GetResourceImage("MainMenu.file.preview").GetHicon());
            _browser.Dock = DockStyle.Fill;
            this.Controls.Add(_browser);
        }

        #endregion

        #region 内部变量

        WebBrowser _browser = new WebBrowser();
        #endregion

        #region 公共属性
        
        /// <summary>
        /// 获取或设置预览的HTML内容
        /// </summary>
        public string DocumentText
        {
            get { return _browser.DocumentText; }
            set
            {
                ///有可能粘贴一些外部代码，而导致这里找不到js函数之类的，会出现异常
                try
                {
                    _browser.DocumentText = value;
                }
                catch { }
            }
        }

        private MdiSnipDesignerForm _currentDesignerForm;

        public void RefreshWebBrowser()
        {
            ///先将上次记录的窗口的事件注销
            if (_currentDesignerForm != null)
            {
                _currentDesignerForm.SnipPageDesigner.PartsLayouted -= new EventHandler(SnipPageDesigner_PartsLayouted);
                _currentDesignerForm = null;
            }

            ///在活动窗口为页面片设计器时生效
            if (Service.Workbench.ActiveWorkDocumentType == WorkDocumentType.SnipDesigner)
            {
                ShowDocumentText();
                _currentDesignerForm.SnipPageDesigner.PartsLayouted += new EventHandler(SnipPageDesigner_PartsLayouted);
            }
            ///活动窗口不是页面片设计器则隐藏内容
            else
            {
                if (_browser.IsHandleCreated && !_browser.IsDisposed)
                {
                    _browser.DocumentText = "";
                    _browser.Visible = false;
                }
            }
        }

        void SnipPageDesigner_PartsLayouted(object sender, EventArgs e)
        {
           // ShowDocumentText();
        }

        public void ShowDocumentText()
        {
            _currentDesignerForm = (MdiSnipDesignerForm)Service.Workbench.ActiveForm;

            ///若当前面板可见，则继续
            if (this.Visible)
            {
                string html = _currentDesignerForm.SnipPageDesigner.ToHtmlPreview();

                _browser.DocumentText = html;
                _browser.Visible = true;
            }
            else
            {
                _browser.DocumentText = "";
            }
        }

        #endregion
    }
}
