using NKnife.Chesses.Common;
using NKnife.Chesses.Common.Base;

namespace NKnife.Chesses.Controls.Interface
{
    public interface IChessBoard
    {
        Enums.GameSide CurrChessSide { get; }
        Game Game { get; }
    }
}
