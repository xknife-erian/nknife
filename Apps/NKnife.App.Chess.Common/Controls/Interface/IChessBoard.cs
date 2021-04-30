using System;
using Gean.Module.Chess;
using NKnife.Chesses.Common;
using NKnife.Chesses.Common.Base;

namespace Gean.Gui.ChessControl
{
    public interface IChessBoard
    {
        Enums.GameSide CurrChessSide { get; }
        Game Game { get; }
    }
}
