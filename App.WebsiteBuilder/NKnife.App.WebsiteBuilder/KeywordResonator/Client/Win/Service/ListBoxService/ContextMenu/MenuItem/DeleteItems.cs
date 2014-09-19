using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.KeywordResonator.Client
{
    public partial class DeleteItems : BaseTmpltTreeMenuItem
    {

        public DeleteItems(AbstractView keywordListView)
            : base(keywordListView)
        {
            this.Text = "删除";
        }
        protected override void OnClick(EventArgs e)
        {
            //有需要 命令模式 实现undo和redo 现在不考虑
            ((KeywordListView)ListView).DeleteItemFromCheckBox();
            base.OnClick(e);
        }
        public override void MenuOpening()
        {
            if (((KeywordListView)ListView).FormType == KeywordFormType.ExistWord)
            {
                this.Visible = false;
            }
            else if (((KeywordListView)ListView).FormType == KeywordFormType.NewWord)
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
}
