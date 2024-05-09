using Gean.Module.Chess;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Interface;
using NKnife.Chesses.Common.Position;

namespace NKnife.Chesses.Common.Pieces
{
    public class PieceKnight : Piece
    {
        public static PieceKnight Knight02 = new PieceKnight(Enums.GameSide.Black, new Position.Position(2, 8));
        public static PieceKnight Knight07 = new PieceKnight(Enums.GameSide.Black, new Position.Position(7, 8));
        public static PieceKnight Knight58 = new PieceKnight(Enums.GameSide.White, new Position.Position(2, 1));
        public static PieceKnight Knight63 = new PieceKnight(Enums.GameSide.White, new Position.Position(7, 1));

        public PieceKnight(Enums.GameSide manSide, Position.Position position)
            : base(position)
        {
            this.GameSide = manSide;
            switch (manSide)
            {
                case Enums.GameSide.White:
                    this.PieceType = Enums.PieceType.WhiteKnight;
                    break;
                case Enums.GameSide.Black:
                    this.PieceType = Enums.PieceType.BlackKnight;
                    break;
            }
        }

        public override void SetEnablePositions(ISituation situation, out Positions enableMovein, out Positions enableCapture)
        {
            enableCapture = new Positions();
            enableMovein = new Positions();

            Position.Position aPos = this.Position.ShiftWestNorth();
            Position.Position bPos = this.Position.ShiftEastNorth();
            Position.Position cPos = this.Position.ShiftWestSouth();
            Position.Position dPos = this.Position.ShiftEastSouth();
            if (aPos != Common.Position.Position.Empty)
            {
                Common.Position.Position.Shift(this.GameSide, situation, aPos.ShiftNorth(), enableMovein, enableCapture);
                Common.Position.Position.Shift(this.GameSide, situation, aPos.ShiftWest(), enableMovein, enableCapture);
            }
            if (bPos != Common.Position.Position.Empty)
            {
                Common.Position.Position.Shift(this.GameSide, situation, bPos.ShiftNorth(), enableMovein, enableCapture);
                Common.Position.Position.Shift(this.GameSide, situation, bPos.ShiftEast(), enableMovein, enableCapture);
            }
            if (cPos != Common.Position.Position.Empty)
            {
                Common.Position.Position.Shift(this.GameSide, situation, cPos.ShiftWest(), enableMovein, enableCapture);
                Common.Position.Position.Shift(this.GameSide, situation, cPos.ShiftSouth(), enableMovein, enableCapture);
            }
            if (dPos != Common.Position.Position.Empty)
            {
                Common.Position.Position.Shift(this.GameSide, situation, dPos.ShiftEast(), enableMovein, enableCapture);
                Common.Position.Position.Shift(this.GameSide, situation, dPos.ShiftSouth(), enableMovein, enableCapture);
            }
        }
    }
}
