using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.KeywordResonator.Client
{
    public partial class ModifyFrequency : BaseTmpltTreeMenuItem
    {

        public ModifyFrequency(AbstractView keywordListView)
            : base(keywordListView)
        {
            this.Text = "修改权重";
        }
        protected override void OnClick(EventArgs e)
        {
            ((KeywordListView)ListView).ModifyFrequency();
            base.OnClick(e);
        }
        public override void MenuOpening()
        {
            if (((KeywordListView)ListView).FormType == KeywordFormType.NewWord)
            {
                this.Visible = false;
            }
            else if (((KeywordListView)ListView).FormType == KeywordFormType.ExistWord)
            {
                if (((KeywordListView)ListView).CheckedListBox.CheckedIndices.Count == 1)
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
}
