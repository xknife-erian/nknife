using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    public partial class HTMLDesignerContrl : BaseUserControl
    {    
        public HTMLDesignerContrl()
        {
            InitializeComponent();
            InitControls();
        }

        void InitControls()
        {           
            InitMainToolStrip();
            InitDesignSplit();
            this.Dock = DockStyle.Fill;
        }

        public InsertMode InsertUseMode { get; set; }

        #region 工具栏按钮
        HTMLToolStrip mainToolStrip = null;
        public HTMLToolStrip InitMainToolStrip()
        {
            mainToolStrip = new HTMLToolStrip();
            mainToolStrip.OwnerHtmlDesigner = this;
            mainToolStrip.Dock = DockStyle.Top;
            mainToolStrip.BringToFront();
            this.Controls.Add(mainToolStrip);

          /*  switch (_useMode)
            {
                case DesignerUseMode.General:
                    break;
                case DesignerUseMode.Other:
                case DesignerUseMode.SnipPart:
                    {
                        mainToolStrip.TitleToolStripLabel.Visible =
                        mainToolStrip.TitleToolStripTextBox.Visible = false;
                        mainToolStrip.PropToolStripButton.Visible = false;
                        break;
                    }
            }*/
            return mainToolStrip;
        }
        #endregion

        #region 拆分窗口：包含设计窗口和代码窗口
        public SplitContainer splitCon = new SplitContainer();
        public SplitContainer InitDesignSplit()
        {
            splitCon.Panel1.Controls.Add(InitDesignPart());
            splitCon.Panel2.Controls.Add(InitCodePart());
            splitCon.Orientation = Orientation.Horizontal;
            this.Controls.Add(splitCon);

            splitCon.Dock = DockStyle.Fill;
            splitCon.BringToFront();
            splitCon.Panel2Collapsed = true;
            return splitCon;
        }

        HTMLCodePart codeRichText = null;

        public HTMLCodePart CodeRichText
        {
            get { return codeRichText; }
            set { codeRichText = value; }
        }
        private HTMLCodePart InitCodePart()
        {
            codeRichText = new HTMLCodePart();
            codeRichText.Dock = DockStyle.Fill;
            return codeRichText;
        }

        HTMLDesignPart designWebBrowser = null;

        public HTMLDesignPart DesignWebBrowser
        {
            get { return designWebBrowser; }
            set { designWebBrowser = value; }
        }
        private HTMLDesignPart InitDesignPart()
        {
            designWebBrowser = new HTMLDesignPart();
            string strDocumentText = "<html><head><link href='" + System.Windows.Forms.Application.StartupPath + @"\style.css' rel='stylesheet' type='text/css' /></head><body></body></html>";
            designWebBrowser.DocumentText = strDocumentText;
            designWebBrowser.Dock = DockStyle.Fill;

            designWebBrowser.idoc2.designMode = "on";
            return designWebBrowser;
        }

        #endregion

        #region 加入资源虚方法
        string openfileDig(string filter)
        {
            OpenFileDialog openD = new OpenFileDialog();
            openD.Filter = filter;
            if (openD.ShowDialog() == DialogResult.OK)
            {
                return openD.FileName;
            }
            else
                return "";
        }
        public virtual string OpenPicDialog()
        {
           return  openfileDig("PIC files (*.jpg,*.png,*.gif)|*.jpg;*.png;*.gif");
        }

        public virtual string OpenAudioDialog()
        {
            return openfileDig("Audio files (*.mp3,*.mdi,*.wma,*.wav)|*.mp3;*.mid;*.wma;*.wav");
        }

        public virtual string OpenFlashDialog()
        {
            return openfileDig("Flash files (.swf)|*.swf");
        }

        public virtual string OpenMediaDialog()
        {
            return openfileDig("Media files (*.rmvb,*.rm,*.avi,*.wmv)|*.rmvb;*.rm;*.avi;*.wmv");
        }


        public virtual object InsertLink()
        {
            return this.designWebBrowser.idoc2.execCommand("CreateLink", true, true);
            //return openfileDig("Media files (*.rmvb,*.rm,*.avi,*.wmv)|*.rmvb;*.rm;*.avi;*.wmv");
        }

        public virtual string GetResourceAbsolutePath(string picId)
        {
            return "";
        }
        #endregion


    }
}
