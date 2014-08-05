using System;
namespace Gean.Module.Chess
{
    public interface IPiece
    {
        Enums.GameSide GameSide { get; }
        Enums.PieceType PieceType { get; }
        Position Position { get; }
        bool IsCaptured { get; }
    }
}
