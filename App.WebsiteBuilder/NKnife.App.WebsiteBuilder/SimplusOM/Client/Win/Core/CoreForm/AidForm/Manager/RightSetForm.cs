using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class RightSetForm : Form
    {
        string _rightStr = "";
        string _bitArrayStr = "";

        public string BitArrayStr
        {
            get
            {
                string _bitAgentRight = _bitArrayStr.Substring(0, 30);
                string _bitUserRight = _bitArrayStr.Substring(30, 48);
                string _bitOtherRight = _bitArrayStr.Substring(78);

                foreach (Control con in panel1.Controls)
                {
                    if (con is CheckBox && con.TabIndex > 0)
                    {
                        CheckBox ckb = con as CheckBox;
                        string bitRight = ckb.Checked ? "1" : "0";
                        int rightIndex = ckb.TabIndex-1;
                        _bitAgentRight = _bitAgentRight.Remove(rightIndex, 1);
                        _bitAgentRight = _bitAgentRight.Insert(rightIndex, bitRight);
                    }
                }

                foreach (Control con in panel2.Controls)
                {
                    if (con is CheckBox && con.TabIndex > 0)
                    {
                        CheckBox ckb = con as CheckBox;
                        string bitRight = ckb.Checked ? "1" : "0";
                        int rightIndex = ckb.TabIndex-1;
                        _bitUserRight = _bitUserRight.Remove(rightIndex, 1);
                        _bitUserRight = _bitUserRight.Insert(rightIndex, bitRight);
                    }
                }

                foreach (Control con in panel3.Controls)
                {
                    if (con is CheckBox && con.TabIndex > 0)
                    {
                        CheckBox ckb = con as CheckBox;
                        string bitRight = ckb.Checked ? "1" : "0";
                        int rightIndex = ckb.TabIndex-1;
                        _bitOtherRight = _bitOtherRight.Remove(rightIndex, 1);
                        _bitOtherRight = _bitOtherRight.Insert(rightIndex, bitRight);
                    }
                }
                _bitArrayStr = _bitAgentRight + _bitUserRight + _bitOtherRight;
                return _bitArrayStr;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return ;
                string _bitAgentRight = value.Substring(0, 30);
                string _bitUserRight = value.Substring(30, 48);
                string _bitOtherRight = value.Substring(78);

                foreach (Control con in panel1.Controls)
                {
                    if (con is CheckBox && con.TabIndex > 0)
                    {
                        CheckBox ckb = con as CheckBox;
                        string bitRight = _bitAgentRight.Substring(ckb.TabIndex - 1, 1);
                        ckb.Checked = (bitRight == "1");
                    }
                }

                foreach (Control con in panel2.Controls)
                {
                    if (con is CheckBox && con.TabIndex > 0)
                    {
                        CheckBox ckb = con as CheckBox;
                        string bitRight = _bitUserRight.Substring(ckb.TabIndex - 1, 1);
                        ckb.Checked = (bitRight == "1");
                    }
                }
                foreach (Control con in panel3.Controls)
                {
                    if (con is CheckBox && con.TabIndex > 0)
                    {
                        CheckBox ckb = con as CheckBox;
                        string bitRight = _bitOtherRight.Substring(ckb.TabIndex - 1, 1);
                        ckb.Checked = (bitRight == "1");
                    }
                }
            }
        }

        public string RightStr
        {
            get { return _rightStr; }
            set { _rightStr = value; }
        }
        public RightSetForm(string rightStr)
        {
            InitializeComponent();
            _rightStr = rightStr;
        }

        private void RightSetForm_Load(object sender, EventArgs e)
        {
            OKBtn.Enabled = OMWorkBench.ModifyManager;
            for (int i = 0; i < _rightStr.Length; i++)
            {
                char rightArr = _rightStr[i];
                _bitArrayStr += Convert.ToString(rightArr, 2).PadLeft(8, '0');
            }
            BitArrayStr = OMWorkBench.StrToBitStr(_rightStr);
        }

        private void AllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox thisCheck = sender as CheckBox;

            switch (thisCheck.Name)
            {
                case "AllAgentcheckBox":
                case "AllUserCheckBox":
                case "OtherAllCheckBox":
                    {
                        foreach (Control con in thisCheck.Parent.Controls)
                        {
                            if (con is CheckBox)
                                ((CheckBox)con).Checked = thisCheck.Checked;
                        }
                        break;
                    }
                default: break;
            }

        }
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
        //    CheckBox thisCheck = sender as CheckBox;
        //    switch (thisCheck.TabIndex)
        //    {
        //        case 1:
        //        default:
        //            break;
        //    }
        }
        private void OKBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }




        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}
