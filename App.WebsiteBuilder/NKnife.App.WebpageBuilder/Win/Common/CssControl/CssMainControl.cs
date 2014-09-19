using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;

namespace Jeelu.Win
{
    public class CssMainControl : BaseUserControl
    {
        public CssMainControl()
        {
            InitializeComponent();
        }

        [Browsable(false)]
        public CssSection Value { get; private set; }

        public virtual void EnterLoad()
        {
        }

        public virtual bool LeaveValidate()
        {
            return true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CssMainControl
            // 
            this.Name = "CssMainControl";
            this.Size = new System.Drawing.Size(430, 250);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// 传入面板名称，创建Css主面板。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static public CssMainControl Create(string name,CssSection value)
        {
            CssMainControl newMainControl;
            switch (name)
            {
                case "类型":
                    newMainControl = new GeneralCssControl();
                    break;

                case "背景":
                    newMainControl = new BackgroundCssControl();
                    break;

                case "区块":
                    newMainControl = new BlockCssControl();
                    break;

                case "方框":
                    newMainControl = new BoxCssControl();
                    break;

                case "列表":
                    newMainControl = new ListCssControl();
                    break;

                case "定位":
                    newMainControl = new PositionCssControl();
                    break;

                case "扩展":
                    newMainControl = new ExpandCssControl();
                    break;

                default:
                    Debug.Fail("未知的name:" + name);
                    newMainControl = null;
                    break;
            }
            newMainControl.Value = value;

            return newMainControl;
        }
    }
}
