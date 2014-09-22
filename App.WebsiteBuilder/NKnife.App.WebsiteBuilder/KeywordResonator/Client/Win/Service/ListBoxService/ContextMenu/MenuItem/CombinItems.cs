using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.KeywordResonator.Client
{
    public partial class CombinItems : BaseTmpltTreeMenuItem
    {

        public CombinItems(AbstractView keywordListView)
            : base(keywordListView)
        {
            this.Text = "合并";
        }
        protected override void OnClick(EventArgs e)
        {
            ((KeywordListView)ListView).CombinCheckItem();
            base.OnClick(e);
        }
        public override void MenuOpening()
        {
            if (((KeywordListView)ListView).FormType == KeywordFormType.ExistWord)
            {
                this.Visible = false;
            }
            else if(((KeywordListView)ListView).FormType == KeywordFormType.NewWord)
            {
                if (((KeywordListView)ListView).CheckedListBox.CheckedIndices.Count > 1)
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
