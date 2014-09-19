using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    public class ColorBaseControl : Control
    {
        public ColorBaseControl()
        {
        }

        protected override void OnCreateControl()
        {
            ///配置捕获窗口
            CaptureLossWindow helperWindow = new CaptureLossWindow();
            helperWindow.control = this;
            helperWindow.AssignHandle(Handle);

            base.OnCreateControl();
        }

        #region 处理鼠标捕获丢失的情况

        public event EventHandler LostCapture;
        protected virtual void OnLostCapture(EventArgs e)
        {
            if (LostCapture != null)
            {
                LostCapture(this, e);
            }
        }

        class CaptureLossWindow : NativeWindow
        {
            public ColorBaseControl control;

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == 533)   //WM_CAPTURECHANGED
                {
                    control.OnLostCapture(EventArgs.Empty);
                }

                base.WndProc(ref m);
            }
        }

        #endregion
    }
}
