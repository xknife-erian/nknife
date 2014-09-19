using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class BiddingAgent : UserControl 
    {
        AgentInfo _value = new AgentInfo();
        string _file;
        /// <summary>
        /// 获取或设置控件的值
        /// </summary>
        public AgentInfo Value
        {
            get { Save(); return _value; }
            set 
            {
                _value = value;
                SetControlValue();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BiddingAgent(string fileName)
        {
            _file = Path.Combine(PathService.CL_DataSources_Folder, fileName);
            InitializeComponent();
            this.AgentSelectGroup.Width = 120;
            this.AgentSelectGroup.HorizontalCount = 2;
            this.AgentSelectGroup.SelectedValue = 1;
            ReadFile();
           // ReadResourceText();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            //ReadResourceText();
        }
        /// <summary>
        /// 签入资源文本
        /// </summary>
        private void ReadResourceText()
        {
            this.lblIsAgent.Text = ResourceService.GetResourceText("IsAgent");
            this.lblAgent.Text = ResourceService.GetResourceText("AgentManager");
            this.lblPhone.Text = ResourceService.GetResourceText("AgentPhone");
            this.lblAgentUnit.Text = ResourceService.GetResourceText("AgentUnit");
            
        }

        private void selectGroup1_SelectedValueChanged(object sender, EventArgs e)
        {
            string text = AgentSelectGroup.SelectedValue.ToString();
            if (text == "1")
            {
                //有
                this.tbxAgent.Enabled = true;
                this.tbxAgentUnit.Enabled = true;
                this.tbxPhone.Enabled = true;
            }
            else
            {
                //无
                this.tbxAgent.Text = "";
                this.tbxAgentUnit.Text = "";
                this.tbxPhone.Text = "";
                this.tbxAgent.Enabled = false;
                this.tbxAgentUnit.Enabled = false;
                this.tbxPhone.Enabled = false;
            }
        }
        protected void Save()
        {
            if (this.AgentSelectGroup.SelectedValue != null)
            {
                _value.IsAgent = this.AgentSelectGroup.SelectedValue.ToString();
            }
            _value.Phone = this.tbxPhone.Text;
            _value.Agent = this.tbxAgent.Text;
            _value.AgentUnit = this.tbxAgentUnit.Text;

        }
        /// <summary>
        /// 读取Agent.xml文件
        /// </summary>
        private void ReadFile()
        {
            DataTable dt = GetDataTable(_file);
            AgentSelectGroup.DataSource = dt;
            AgentSelectGroup.DisplayMember = "text";
            AgentSelectGroup.ValueMember = "value";
            AgentSelectGroup.DataBinding();
        }

        /// <summary>
        /// 将其置入控件内
        /// </summary>
        private void SetControlValue()
        {
            this.AgentSelectGroup.SelectedValue = _value.IsAgent;
            this.tbxAgent.Text = _value.Agent;
            this.tbxAgentUnit.Text = _value.AgentUnit;
            this.tbxPhone.Text = _value.Phone;
        }

        private DataTable GetDataTable(string fileName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("value");
            dt.Columns.Add("text");

            Debug.Assert(File.Exists(fileName), "Configtion file isn't Exists");
            using (XmlTextReader reader = new XmlTextReader(fileName))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.GetAttribute("text") != null && reader.GetAttribute("value") != null)
                    {
                        dt.Rows.Add(reader.GetAttribute("value"), reader.GetAttribute("text"));
                    }
                }
            }
            return dt;
        }
    }
}
