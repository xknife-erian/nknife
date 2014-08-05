using System;
using System.Collections.Generic;
using System.Text;
using Gean.Module.Chess;

namespace Gean.Gui.ChessControl
{
    public interface IActiveRecord
    {
        Record ActiveRecord { get; }
        int CurrChessStepPair { get; }
        Enums.GameSide CurrGameSide { get; }
        Step GetStep();
    }
}
