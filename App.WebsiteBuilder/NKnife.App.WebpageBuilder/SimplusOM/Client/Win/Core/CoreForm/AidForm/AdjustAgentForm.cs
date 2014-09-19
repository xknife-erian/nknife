using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class AdjustAgentForm : Form
    {
        public AdjustAgentForm(DataRow agentRow,DataTable areaTable)
        {
            InitializeComponent();

            label7.Text = agentRow["areaName"].ToString();
            label3.Text = agentRow["grade"].ToString();

            areaControl1.areaSource = OMWorkBench.BaseInfoDS.Tables["area"];
        }
    }
}
