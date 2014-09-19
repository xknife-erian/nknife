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
    public partial class DepartmentNameControl : UserControl
    {
        public List<string> list;
        public string _value;
        public string Value
        {
            get
            {
                Save();
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        public DepartmentNameControl()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AddDepartementName();
            Service.Sdsite.CurrentDocument.SitePropertySaved += delegate
            {
                AddDepartementName();
            };
        }
        private void AddDepartementName()
        {
            XmlNodeList nodes = Service.Sdsite.CurrentDocument.SelectNodes("//department");
            if (nodes != null)
            {
                list=new List<string>();
                foreach (XmlNode node in nodes)
                {
                    if (node != null && node is XmlElement)
                    {
                        SiteDepartmentXmlElement ele = (SiteDepartmentXmlElement)node;
                        list.Add(ele.DeptName);
                    }
                }
                this.conDepartmentName.DataSource = list;
            }
            if (_value != null)
            {
                this.conDepartmentName.Text = _value;
            }
        }
        private void Save()
        {
            _value = this.conDepartmentName.Text;
        }
        public event EventHandler Changed;
        protected virtual void OnCheckChanged(EventArgs e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }

        private void conDepartmentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
        }
    }
}
