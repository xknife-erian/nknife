using System.Collections.Generic;
using Gean.Module.Chess;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Pieces;

namespace NKnife.Chesses.Common.Interface
{
    public interface IGame : IPieceMove, IEnumerable<Piece>
    {
        Pieces.Pieces ActivedPieces { get; }
        Pieces.Pieces MovedPieces { get; }
        Enums.PlayMode PlayMode { get; set; }
        bool TryGetPiece(int dot, out Piece piece);
        bool TryContains(Piece piece, out int dot);
    }
}
