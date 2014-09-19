using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Jeelu.SimplusOM.Client
{
    public partial class OMBaseForm : DockContent, IOMCommand
    {
        public OMBaseForm()
        {
            InitializeComponent();

            foreach (ToolStripItem tsBtn in mainToolStrip.Items)
            {
                tsBtn.Visible = false;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            this.TabText = this.Text;

            base.OnTextChanged(e);
        }

        #region IOMCommand 成员

        virtual public void NewCmd()
        {

        }

        virtual public void EditCmd()
        {

        }

        virtual public void FrozedCmd()
        {

        }

        virtual public void DeleteCmd()
        {

        }

        virtual public void CancelCmd()
        {

        }

        virtual public void SaveCmd()
        {

        }

        virtual public void ChargeCmd()
        {

        }

        virtual public void ReturnSetCmd()
        {

        }

        virtual public void ReturnCmd()
        {

        }

        virtual public void CheckCmd()
        {

        }

        virtual public void CloseCmd()
        {
            this.Close();
        }

        #endregion

        #region 工具栏
        /// <summary>
        /// 工具栏按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "NewTSButton": NewCmd(); break;
                case "EditTSButton": EditCmd(); break;
                case "FrozedTSButton": FrozedCmd(); break;
                case "DeleteTSButton": DeleteCmd(); break;
                case "CancelTSButton": CancelCmd(); break;
                case "SaveTSButton": SaveCmd(); break;

                case "ChargeTSButton": ChargeCmd(); break;
                case "ReturnSetTSButton": ReturnSetCmd(); break;
                case "ReturnTSButton": ReturnCmd(); break;

                case "CheckTSButton": CheckCmd(); break;
                case "CloseTSButton": CloseCmd(); break;
            }
        }
        #endregion


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
                OnLoad(null);
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
