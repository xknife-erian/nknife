using Gean.Module.Chess;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Interface;

namespace NKnife.Chesses.Common.Pieces
{
    public class PieceBishop : Piece
    {

        public static PieceBishop Bishop03 = new PieceBishop(Enums.GameSide.Black, new Position(3, 8));
        public static PieceBishop Bishop06 = new PieceBishop(Enums.GameSide.Black, new Position(6, 8));
        public static PieceBishop Bishop59 = new PieceBishop(Enums.GameSide.White, new Position(3, 1));
        public static PieceBishop Bishop62 = new PieceBishop(Enums.GameSide.White, new Position(6, 1));

        public PieceBishop(Enums.GameSide manSide, Position position)
            : base(position)
        {
            this.GameSide = manSide;
            switch (manSide)
            {
                case Enums.GameSide.White:
                    this.PieceType = Enums.PieceType.WhiteBishop;
                    break;
                case Enums.GameSide.Black:
                    this.PieceType = Enums.PieceType.BlackBishop;
                    break;
            }
        }

        public override void SetEnablePositions(ISituation situation, out Positions enableMovein, out Positions enableCapture)
        {
            enableMovein = new Positions();
            enableCapture = new Positions();
            PieceBishop.BishopShift(this.GameSide, situation, this.Position, enableMovein, enableCapture);
        }

        /// <summary>
        /// 象的基本路线(即斜向)
        /// </summary>
        internal static void BishopShift(
            Enums.GameSide side, ISituation situation, Position position,
            Positions moveInPs, Positions capturePs)
        {
            bool canContine = true;
            Position tgtPos = Position.Empty;

            canContine = true; 
            tgtPos = position;
            while (canContine)
            {
                tgtPos = tgtPos.ShiftEastNorth();
                canContine = Position.Shift(side, situation, tgtPos, moveInPs, capturePs);
            }

            canContine = true;
            tgtPos = position;
            while (canContine)
            {
                tgtPos = tgtPos.ShiftEastSouth();
                canContine = Position.Shift(side, situation, tgtPos, moveInPs, capturePs);
            }

            canContine = true;
            tgtPos = position;
            while (canContine)
            {
                tgtPos = tgtPos.ShiftWestNorth();
                canContine = Position.Shift(side, situation, tgtPos, moveInPs, capturePs);
            }

            canContine = true;
            tgtPos = position;
            while (canContine)
            {
                tgtPos = tgtPos.ShiftWestSouth();
                canContine = Position.Shift(side, situation, tgtPos, moveInPs, capturePs);
            }
        }
    }
}
