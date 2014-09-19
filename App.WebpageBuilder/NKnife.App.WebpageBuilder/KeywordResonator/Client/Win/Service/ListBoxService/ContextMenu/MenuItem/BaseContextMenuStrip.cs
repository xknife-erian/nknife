using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.KeywordResonator.Client
{
    public abstract class BaseTmpltTreeMenuItem : ToolStripMenuItem
    {
        public BaseTmpltTreeMenuItem(AbstractView keywordListView)
            :base()
        {
            this.ListView = keywordListView;
        }

        public abstract void MenuOpening();

        public AbstractView ListView { get; private set; }
    }
}
