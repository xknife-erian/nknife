using Gean.Module.Chess;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Exceptions;
using NKnife.Chesses.Common.Interface;

namespace NKnife.Chesses.Common.Pieces
{
    public class PieceQueen : Piece
    {
        public static PieceQueen _NewBlackQueen = new PieceQueen(Enums.GameSide.Black);
        public static PieceQueen _NewWhiteQueen = new PieceQueen(Enums.GameSide.White);

        public PieceQueen(Enums.GameSide side) : this(side, Position.Empty) { }
        public PieceQueen(Enums.GameSide side, Position position)
            : base(position)
        {
            this.GameSide = side;
            switch (side)
            {
                case Enums.GameSide.White:
                    this.PieceType = Enums.PieceType.WhiteQueen;
                    break;
                case Enums.GameSide.Black:
                    this.PieceType = Enums.PieceType.BlackQueen;
                    break;
            }
        }

        protected override Position InitPosition(Position position)
        {
            if (position == Position.Empty)
            {
                switch (this.GameSide)
                {
                    case Enums.GameSide.White:
                        return new Position(4, 1);
                    case Enums.GameSide.Black:
                        return new Position(4, 8);
                    default:
                        throw new PieceException(ExString.PositionIsEmpty);
                }
            }
            else
                return position;
        }

        public override void SetEnablePositions(ISituation situation, out Positions enableMovein, out Positions enableCapture)
        {
            enableMovein = new Positions();
            enableCapture = new Positions();
            PieceRook.RookShift(this.GameSide, situation, this.Position, enableMovein, enableCapture);
            PieceBishop.BishopShift(this.GameSide, situation, this.Position, enableMovein, enableCapture);
        }


    }
}
