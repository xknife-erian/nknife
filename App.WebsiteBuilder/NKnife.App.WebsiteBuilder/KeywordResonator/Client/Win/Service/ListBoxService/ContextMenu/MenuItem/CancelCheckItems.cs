using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.KeywordResonator.Client
{
    public partial class CancelCheckItems : BaseTmpltTreeMenuItem
    {

        public CancelCheckItems(AbstractView keywordListView)
            : base(keywordListView)
        {
            this.Text = "取消选中项";
        }
        protected override void OnClick(EventArgs e)
        {
            ((KeywordListView)ListView).SetCheckState(false);
            base.OnClick(e);
        }
        public override void MenuOpening()
        {
            if (((KeywordListView)ListView).CheckedListBox.CheckedIndices.Count > 0)
            {
                this.Enabled = true;
            }
            else
            {
                this.Enabled = false;
            }
        }
    }
}
