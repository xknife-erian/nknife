using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Jeelu;

namespace Demo
{
    public partial class DemoForm : Form
    {
        public DemoForm()
        {
            InitializeComponent();
            this.Test();
        }

        private void Test()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("XMLFile1.xml");
            IdManager idm = IdManager.Initialize(doc.DocumentElement);
        }

        #region MyRegion

        private void 新建NToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 快速测试F5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("快速测试,按F5键");
        }

        #endregion
    }
}
