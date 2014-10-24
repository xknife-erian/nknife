using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Gean.Module.Chess;
using NKnife.Chesses.Common;

namespace Gean.Gui.ChessControl
{
    public class RecordPlayToolStrip : ToolStrip, IRecordPlay
    {
        public RecordPlayToolStrip()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            ToolStripButton menuItem;

            menuItem = new ToolStripButton("BackStart");
            menuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.Items.Add(menuItem);
            menuItem = new ToolStripButton("FastBack");
            menuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.Items.Add(menuItem);
            menuItem = new ToolStripButton("Back");
            menuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.Items.Add(menuItem);
            menuItem = new ToolStripButton("AutoForward");
            menuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.Items.Add(menuItem);
            menuItem = new ToolStripButton("Forward");
            menuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.Items.Add(menuItem);
            menuItem = new ToolStripButton("Skip");
            menuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.Items.Add(menuItem);
            menuItem = new ToolStripButton("FastForward");
            menuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.Items.Add(menuItem);
            menuItem = new ToolStripButton("ForwardEnd");
            menuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.Items.Add(menuItem);

        }

        #region IRecordPlay 成员

        public IActiveRecord ActiveRecord { get; private set; }

        public IChessBoard ChessBoard { get; private set; }

        public void Forward()
        {
            Step step = this.ActiveRecord.GetStep();
            Position srcPos = step.SourcePosition;
            Position tgtPos = step.TargetPosition;
            if (srcPos == Position.Empty)
            {
                //srcPos = ChessPath.GetSourcePosition(step, this.ChessBoard.CurrChessSide, this.ChessBoard.OwnedGame);
            }
            //this.ChessBoard.MoveIn(srcPos, tgtPos);
        }

        public void Back()
        {
            throw new NotImplementedException();
        }

        public void FastForward()
        {
            throw new NotImplementedException();
        }

        public void FastBack()
        {
            throw new NotImplementedException();
        }

        public void ForwardEnd()
        {
            throw new NotImplementedException();
        }

        public void BackStart()
        {
            throw new NotImplementedException();
        }

        public void Skip(int number)
        {
            throw new NotImplementedException();
        }

        public void AutoForward()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
