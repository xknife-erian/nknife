using Gean.Module.Chess;
using NKnife.Chesses.Common.Base;

namespace NKnife.Chesses.Common.Interface
{
    public interface IPiece
    {
        Enums.GameSide GameSide { get; }
        Enums.PieceType PieceType { get; }
        Position Position { get; }
        bool IsCaptured { get; }
    }
}
