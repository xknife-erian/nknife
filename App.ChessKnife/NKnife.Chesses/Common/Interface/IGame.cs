using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    public interface IGame : IPieceMove, IEnumerable<Piece>
    {
        Pieces ActivedPieces { get; }
        Pieces MovedPieces { get; }
        Enums.PlayMode PlayMode { get; set; }
        bool TryGetPiece(int dot, out Piece piece);
        bool TryContains(Piece piece, out int dot);
    }
}
