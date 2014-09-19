using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class BuildNumberControl : UserControl
    {
        private string _value;
        private string oldNumber;
        public string PageName { get; set; }
        /// <summary>
        /// 存储编号
        /// </summary>
        public string value
        {
            get 
            {
                Save();
                return _value;
            }
            set
            {
                this.textBox1.Text = value;
                oldNumber = this.textBox1.Text;
            }
        }

        private void Save()
        {
            _value = this.textBox1.Text;
            CreateNumberElement ele = new CreateNumberElement(Service.Sdsite.DesignDataDocument);
            UserCreateNumber uEle = new UserCreateNumber(Service.Sdsite.DesignDataDocument);
            if (oldNumber.Equals(this.textBox1.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(oldNumber) && string.IsNullOrEmpty(this.textBox1.Text))
                return;
            else if (!string.IsNullOrEmpty(oldNumber) && string.IsNullOrEmpty(this.textBox1.Text))
            {
                DelEle(ele, uEle);
            }
            else if (string.IsNullOrEmpty(oldNumber) && !string.IsNullOrEmpty(this.textBox1.Text))
            {
                AddEle(ele, uEle);
            }
            else
            {
                DelEle(ele, uEle);
                AddEle(ele, uEle);
            }
            oldNumber = this.textBox1.Text;
         }

        private void AddEle(CreateNumberElement ele, UserCreateNumber uEle)
        {
            //添加新节点
            if (this.textBox1.Text.Length > 4)
            {
                if (this.textBox1.Text.Substring(0, 4).Equals(PageName + "00"))
                {
                    ele.PageName = PageName;
                    ele.Number = this.textBox1.Text;
                    ele.AddElement();
                }
                else
                {
                    uEle.PageName = PageName;
                    uEle.UserAddNumber = this.textBox1.Text;
                    uEle.CreateElement();
                }
            }
            else
            {
                uEle.PageName = PageName;
                uEle.UserAddNumber = this.textBox1.Text;
                uEle.CreateElement();
            }
        }

        private void DelEle(CreateNumberElement ele, UserCreateNumber uEle)
        {
            //删除原节点
            if (oldNumber.Length > 4)
            {
                if (oldNumber.Substring(0, 4).Equals(PageName + "00"))
                {
                    ele.PageName = PageName;
                    ele.OldNumber = oldNumber;
                    ele.DelElement();
                }
                else
                {
                    uEle.PageName = PageName;
                    uEle.UserOldNumber = oldNumber;
                    uEle.DeleElement();
                }
            }
            else
            {
                uEle.PageName = PageName;
                uEle.UserOldNumber = oldNumber;
                uEle.DeleElement();
            }
        }
        

        public BuildNumberControl()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.btnBuildNumber.Text = "生成编码";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CreateNumberElement ele = new CreateNumberElement(Service.Sdsite.DesignDataDocument);
            ele.PageName = PageName;
            ele.CreateNumber();
            this.textBox1.Text = ele.Number;
        }

        public event EventHandler Changed;
        protected virtual void OnCheckChanged(EventArgs e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
        }
    }
}
