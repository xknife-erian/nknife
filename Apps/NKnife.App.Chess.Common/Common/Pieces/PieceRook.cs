using Gean.Module.Chess;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Interface;

namespace NKnife.Chesses.Common.Pieces
{
    /// <summary>
    /// 棋子：车。
    /// </summary>
    public class PieceRook : Piece
    {
        public static PieceRook Rook01 = new PieceRook(Enums.GameSide.Black, new Position(1, 8));
        public static PieceRook Rook08 = new PieceRook(Enums.GameSide.Black, new Position(8, 8));
        public static PieceRook Rook57 = new PieceRook(Enums.GameSide.White, new Position(1, 1));
        public static PieceRook Rook64 = new PieceRook(Enums.GameSide.White, new Position(8, 1));

        public PieceRook(Enums.GameSide manSide, Position position)
            : base(position)
        {
            this.GameSide = manSide;
            switch (manSide)
            {
                case Enums.GameSide.White:
                    this.PieceType = Enums.PieceType.WhiteRook;
                    break;
                case Enums.GameSide.Black:
                    this.PieceType = Enums.PieceType.BlackRook;
                    break;
            }
        }

        public override void SetEnablePositions(ISituation situation, out Positions enableMovein, out Positions enableCapture)
        {
            enableMovein = new Positions();
            enableCapture = new Positions();
            PieceRook.RookShift(this.GameSide, situation, this.Position, enableMovein, enableCapture);
        }

        /// <summary>
        /// 车的基本路线(即横平竖直)
        /// </summary>
        internal static void RookShift(
            Enums.GameSide side, ISituation situation, Position position,
            Positions moveInPs, Positions capturePs)
        {
            bool canContine = true;
            Position tgtPos = Position.Empty;

            canContine = true;
            tgtPos = position;
            while (canContine)
            {
                tgtPos = tgtPos.ShiftEast();
                canContine = Position.Shift(side, situation, tgtPos, moveInPs, capturePs);
            }

            canContine = true;
            tgtPos = position;
            while (canContine)
            {
                tgtPos = tgtPos.ShiftSouth();
                canContine = Position.Shift(side, situation, tgtPos, moveInPs, capturePs);
            }

            canContine = true;
            tgtPos = position;
            while (canContine)
            {
                tgtPos = tgtPos.ShiftWest();
                canContine = Position.Shift(side, situation, tgtPos, moveInPs, capturePs);
            }

            canContine = true;
            tgtPos = position;
            while (canContine)
            {
                tgtPos = tgtPos.ShiftNorth();
                canContine = Position.Shift(side, situation, tgtPos, moveInPs, capturePs);
            }
        }
    }
}
