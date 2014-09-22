﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.KeywordResonator.Client
{
    public partial class SplitItems : BaseTmpltTreeMenuItem
    {

        public SplitItems(AbstractView keywordListView)
            : base(keywordListView)
        {
            this.Text = "拆分";
        }
        protected override void OnClick(EventArgs e)
        {
            ((KeywordListView)ListView).SplitCheckItem();
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
