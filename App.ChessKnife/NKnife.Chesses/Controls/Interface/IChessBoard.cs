using System;
using Gean.Module.Chess;

namespace Gean.Gui.ChessControl
{
    public interface IChessBoard
    {
        Enums.GameSide CurrChessSide { get; }
        Game Game { get; }
    }
}
