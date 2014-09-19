using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class Department : UserControl
    {
        List<DepartmentData> DepartmentList = new List<DepartmentData>();
        DepartmentData depart = new DepartmentData();
        /// <summary>
        /// 提供给外界使用的
        /// </summary>
        public DepartmentData value
        {
            get { Save(); return depart; }
            set { depart = value; }
        }
        public Department()
        {
            InitializeComponent();
            this.comboBox1.SelectedValueChanged += new EventHandler(comboBox1_SelectedValueChanged);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AddNameToCombox();
            Service.Sdsite.CurrentDocument.SitePropertySaved += delegate
          {
              AddNameToCombox();
          };
        }
        private void AddNameToCombox()
        {
            
            //将网站属性中的部门添加到combox中去
            XmlNodeList nodes= Service.Sdsite.CurrentDocument.SelectNodes("//department");
            if (nodes != null)
            {
                DepartmentList.Clear();
                foreach (SiteDepartmentXmlElement item in nodes)
                {
                    DepartmentData dep = new DepartmentData();
                    dep.Name = item.DeptName;
                    dep.LinkMan = item.LinkMan;
                    dep.Email = item.LinkEmail; ;
                    dep.Address = item.LinkAddress;
                    dep.Fax = item.LinkFax;
                    dep.Phone = item.LinkPhone;
                    dep.MobilePhone = item.LinkMobelPhone;
                    dep.PostCode = item.LinkPostCode;
                    DepartmentList.Add(dep);
                }
                this.comboBox1.DataSource = null;
                this.comboBox1.DataSource = DepartmentList;
                this.comboBox1.DisplayMember = "Name";
                this.comboBox1.ValueMember = "Name";
                this.comboBox1.SelectedValue = string.Empty;
                if (!string.IsNullOrEmpty(depart.Name))
                {
                    this.comboBox1.SelectedValue = depart.Name;
                    showForm();
                }
                else
                    showUserForm();
                GetValue(depart);
            }
            
        }
        void showForm()
        {
            this.tbxLinkAddress.Enabled = false;
            this.tbxLinkEmail.Enabled = false;
            this.tbxLinkFax.Enabled = false;
            this.tbxLinkMan.Enabled = false;
            this.tbxLinkPhone.Enabled = false;
            this.tbxPhone.Enabled = false;
            this.tbxLinkPostCode.Enabled = false; 
        }
        void showUserForm()
        {
            this.tbxLinkAddress.Enabled = true;
            this.tbxLinkEmail.Enabled = true;
            this.tbxLinkFax.Enabled = true;
            this.tbxLinkMan.Enabled = true;
            this.tbxLinkPhone.Enabled = true;
            this.tbxPhone.Enabled = true;
            this.tbxLinkPostCode.Enabled = true;
        }

        private void ClearText()
        {
            this.tbxLinkAddress.Text = string.Empty;
            this.tbxLinkEmail.Text = string.Empty;
            this.tbxLinkFax.Text = string.Empty;
            this.tbxLinkMan.Text = string.Empty;
            this.tbxLinkPhone.Text = string.Empty;
            this.tbxPhone.Text = string.Empty;
            this.tbxLinkPostCode.Text = string.Empty;
        }
        void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((DepartmentData)this.comboBox1.SelectedItem != null)
            {
                GetValue((DepartmentData)this.comboBox1.SelectedItem);
                showForm();
                if (string.IsNullOrEmpty(((DepartmentData)this.comboBox1.SelectedItem).Name))
                    showUserForm();
            }

        }
        private void GetValue(DepartmentData dep)
        {
            this.tbxLinkMan.Text = dep.LinkMan;
            this.tbxLinkEmail.Text = dep.Email;
            this.tbxLinkAddress.Text = dep.Address;
            this.tbxLinkFax.Text = dep.Fax;
            this.tbxLinkPhone.Text = dep.Phone;
            this.tbxPhone.Text = dep.MobilePhone;
            this.tbxLinkPostCode.Text = dep.PostCode;
        }
        private void Save()
        {
            depart.Name = this.comboBox1.Text;
            depart.LinkMan = this.tbxLinkMan.Text;
            depart.Email = this.tbxLinkEmail.Text;
            depart.Address = this.tbxLinkAddress.Text;
            depart.Fax = this.tbxLinkFax.Text;
            depart.Phone = this.tbxLinkPhone.Text;
            depart.MobilePhone = this.tbxPhone.Text;
            depart.PostCode = this.tbxLinkPostCode.Text;
        }

        private void btnUserAdd_Click(object sender, EventArgs e)
        {
            this.comboBox1.SelectedValue = string.Empty;
            showUserForm();
            ClearText();
        }
        public event EventHandler Changed;
        protected virtual void OnCheckChanged(EventArgs e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
        }

        private void tbxLinkMan_TextChanged(object sender, EventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
        }

        private void tbxLinkPhone_TextChanged(object sender, EventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
        }

        private void tbxLinkEmail_TextChanged(object sender, EventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
        }

        private void tbxPhone_TextChanged(object sender, EventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
        }

        private void tbxLinkPostCode_TextChanged(object sender, EventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
        }

        private void tbxLinkFax_TextChanged(object sender, EventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
        }

        private void tbxLinkAddress_TextChanged(object sender, EventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
        }

    }
}
