using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Jeelu.Win
{
    public class CheckBoxExControl : Control
    {
        private string _labelText;
        private string _checkText;

        private CheckBox checkBox1;
        private Label label1;

        /// <summary>
        /// 返回当前checkBox的值
        /// </summary>
        public bool Value
        {
            get { return checkBox1.Checked; }
            set { checkBox1.Checked = value; }
        }

        public CheckBoxExControl(string labelText, string checkBoxText)
        {
            _labelText = labelText;
            _checkText = checkBoxText;
            InitCompoment();
        }

        private void InitCompoment()
        {
            //// 
            //// label1
            ////
            label1 = new Label();
            label1.AutoSize = true;
            label1.Text = _labelText;
            label1.Location = new System.Drawing.Point(0, 2);
            this.Controls.Add(label1);
            //得到label的实际宽度
            int wid = GetTextShowLength(label1.Text) + 3;
            //// 
            //// checkBox1
            //// 
            checkBox1 = new CheckBox();
            checkBox1.Text = _checkText;
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(wid, 0);
            checkBox1.CheckedChanged += new EventHandler(checkBox1_CheckedChanged);
            this.Controls.Add(checkBox1);

            this.Size = new System.Drawing.Size(wid + checkBox1.Width, checkBox1.Height);
        }

        void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
        }
        
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        private int GetTextShowLength(string text)
        {
            Graphics g = this.CreateGraphics();
            SizeF sizef = g.MeasureString(text, this.Font);
            g.Dispose();
            return (int)sizef.Width;
        }

        public event EventHandler CheckChanged;
        protected virtual void OnCheckChanged(EventArgs e)
        {
            if (CheckChanged != null )
            {
                CheckChanged(this , e);
            }
        }
    }
}
