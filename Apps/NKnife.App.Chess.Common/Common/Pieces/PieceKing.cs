using Gean.Module.Chess;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Exceptions;
using NKnife.Chesses.Common.Interface;
using NKnife.Chesses.Common.Position;

namespace NKnife.Chesses.Common.Pieces
{
    public class PieceKing : Piece
    {
        public static PieceKing _NewBlackKing = new PieceKing(Enums.GameSide.Black);
        public static PieceKing _NewWhiteKing = new PieceKing(Enums.GameSide.White);

        public PieceKing(Enums.GameSide side) : this(side, Common.Position.Position.Empty) { }
        public PieceKing(Enums.GameSide side, Position.Position position)
            : base(position)
        {
            this.GameSide = side;
            this.IsMoved = false;
            switch (side)
            {
                case Enums.GameSide.White:
                    this.PieceType = Enums.PieceType.WhiteKing;
                    break;
                case Enums.GameSide.Black:
                    this.PieceType = Enums.PieceType.BlackKing;
                    break;
            }
        }

        /// <summary>
        /// 获取与设置该“王”棋子是否移动过，如移动过，将不能再易位。
        /// </summary>
        public bool IsMoved { get; internal set; }

        protected override Position.Position InitPosition(Position.Position position)
        {
            if (position == Common.Position.Position.Empty)
            {
                switch (this.GameSide)
                {
                    case Enums.GameSide.White:
                        return new Position.Position(5, 1);
                    case Enums.GameSide.Black:
                        return new Position.Position(5, 8);
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

            Common.Position.Position.Shift(this.GameSide, situation, this.Position.ShiftEast(), enableMovein, enableCapture);
            Common.Position.Position.Shift(this.GameSide, situation, this.Position.ShiftSouth(), enableMovein, enableCapture);
            Common.Position.Position.Shift(this.GameSide, situation, this.Position.ShiftWest(), enableMovein, enableCapture);
            Common.Position.Position.Shift(this.GameSide, situation, this.Position.ShiftNorth(), enableMovein, enableCapture);
            Common.Position.Position.Shift(this.GameSide, situation, this.Position.ShiftEastNorth(), enableMovein, enableCapture);
            Common.Position.Position.Shift(this.GameSide, situation, this.Position.ShiftEastSouth(), enableMovein, enableCapture);
            Common.Position.Position.Shift(this.GameSide, situation, this.Position.ShiftWestNorth(), enableMovein, enableCapture);
            Common.Position.Position.Shift(this.GameSide, situation, this.Position.ShiftWestSouth(), enableMovein, enableCapture);
        }

    }
}
