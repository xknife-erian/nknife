using Gean.Module.Chess;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Exceptions;
using NKnife.Chesses.Common.Interface;

namespace NKnife.Chesses.Common.Pieces
{
    public class PiecePawn : Piece
    {
        public static PiecePawn[] BlackPawns = new PiecePawn[]
            #region
            {
                new PiecePawn(Enums.GameSide.Black, new Position(1,7)),
                new PiecePawn(Enums.GameSide.Black, new Position(2,7)),
                new PiecePawn(Enums.GameSide.Black, new Position(3,7)),
                new PiecePawn(Enums.GameSide.Black, new Position(4,7)),
                new PiecePawn(Enums.GameSide.Black, new Position(5,7)),
                new PiecePawn(Enums.GameSide.Black, new Position(6,7)),
                new PiecePawn(Enums.GameSide.Black, new Position(7,7)),
                new PiecePawn(Enums.GameSide.Black, new Position(8,7))
            };
            #endregion
        public static PiecePawn[] WhitePawns = new PiecePawn[]
            #region
            {
                new PiecePawn(Enums.GameSide.White, new Position(1,2)),
                new PiecePawn(Enums.GameSide.White, new Position(2,2)),
                new PiecePawn(Enums.GameSide.White, new Position(3,2)),
                new PiecePawn(Enums.GameSide.White, new Position(4,2)),
                new PiecePawn(Enums.GameSide.White, new Position(5,2)),
                new PiecePawn(Enums.GameSide.White, new Position(6,2)),
                new PiecePawn(Enums.GameSide.White, new Position(7,2)),
                new PiecePawn(Enums.GameSide.White, new Position(8,2))
            };
            #endregion

        public PiecePawn(Enums.GameSide side, Position position)
            : base(position)
        {
            this.GameSide = side;
            this.EnableEnPassanted = false;
            switch (side)
            {
                case Enums.GameSide.White:
                    this.PieceType = Enums.PieceType.WhitePawn;
                    break;
                case Enums.GameSide.Black:
                    this.PieceType = Enums.PieceType.BlackPawn;
                    break;
            }
        }

        /// <summary>
        /// 获取与设置能被对方吃过路兵
        /// </summary>
        public bool EnableEnPassanted { get; internal set; }

        protected override Position InitPosition(Position position)
        {
            if (position.Equals(Position.Empty))
            {
                switch (this.GameSide)
                {
                    case Enums.GameSide.White:
                        return new Position(5, 1);
                    case Enums.GameSide.Black:
                        return new Position(5, 8);
                    default:
                        throw new PieceException(ExString.PositionIsError);
                }
            }
            else
                return position;
        }

        public override void SetEnablePositions(ISituation situation, out Positions enableMovein, out Positions enableCapture)
        {
            enableMovein = new Positions();
            enableCapture = new Positions();

            if (this.GameSide == Enums.GameSide.White)
            {
                #region 白兵
                if (this.Position.Y < 1) return;//白兵怎么也不可能在第一行上
                Position tmpPos = Position.Empty;
                //向北
                tmpPos = this.Position.ShiftNorth();
                if (tmpPos == Position.Empty) return;
                if (!situation.ContainsPiece(tmpPos.Dot))
                {
                    enableMovein.Add(tmpPos);
                }
                //再向北(仅当在兵从未动过时)
                if (this.Position.Y == 1)
                {
                    tmpPos = tmpPos.ShiftNorth();
                    if (!situation.ContainsPiece(tmpPos.Dot))
                    {
                        enableMovein.Add(tmpPos);
                    }
                }
                //吃过路兵的判断
                if (this.Position.Y == 4)
                {
                    #region 过路兵
                    //西北
                    tmpPos = this.Position.ShiftWest();
                    if (tmpPos != null)
                    {
                        Piece pawn;
                        Game game = (Game)situation;
                        if (game.TryGetPiece(tmpPos.Dot, out pawn))
                        {
                            if (pawn is PiecePawn)
                            {
                                if (((PiecePawn)pawn).EnableEnPassanted)
                                {
                                    enableMovein.Add(this.Position.ShiftWestNorth());
                                    enableCapture.Add(tmpPos);
                                }
                            }
                        }
                    }
                    //东北
                    tmpPos = this.Position.ShiftEast();
                    if (tmpPos != null)
                    {
                        Piece pawn;
                        Game game = (Game)situation;
                        if (game.TryGetPiece(tmpPos.Dot, out pawn))
                        {
                            if (pawn is PiecePawn)
                            {
                                if (((PiecePawn)pawn).EnableEnPassanted)
                                {
                                    enableMovein.Add(this.Position.ShiftEastNorth());
                                    enableCapture.Add(tmpPos);
                                }
                            }
                        }
                    }
                    #endregion
                }
                //剑指西北
                Position.Shift(this.GameSide, situation, this.Position.ShiftWestNorth(), enableMovein, enableCapture, false);
                //剑指东北
                Position.Shift(this.GameSide, situation, this.Position.ShiftEastNorth(), enableMovein, enableCapture, false);
                #endregion
            }
            else
            {
                #region 黑兵
                if (this.Position.Y > 6) return;
                Position tmpPos = Position.Empty;
                //向南
                tmpPos = this.Position.ShiftSouth();
                if (tmpPos == Position.Empty) return;
                if (!situation.ContainsPiece(tmpPos.Dot))
                {
                    enableMovein.Add(tmpPos);
                }
                //再向南(仅当在兵从未动过时)
                if (this.Position.Y == 6)
                {
                    tmpPos = tmpPos.ShiftSouth();
                    if (!situation.ContainsPiece(tmpPos.Dot))
                    {
                        enableMovein.Add(tmpPos);
                    }
                }
                //吃过路兵的判断
                if (this.Position.Y == 3)
                {
                    #region 过路兵
                    //西南
                    tmpPos = this.Position.ShiftWest();
                    if (tmpPos != null)
                    {
                        Piece pawn;
                        Game game = (Game)situation;
                        if (game.TryGetPiece(tmpPos.Dot, out pawn))
                        {
                            if (pawn is PiecePawn)
                            {
                                if (((PiecePawn)pawn).EnableEnPassanted)
                                {
                                    enableMovein.Add(this.Position.ShiftWestSouth());
                                    enableCapture.Add(tmpPos);
                                }
                            }
                        }
                    }
                    //东南
                    tmpPos = this.Position.ShiftEast();
                    if (tmpPos != null)
                    {
                        Piece pawn;
                        Game game = (Game)situation;
                        if (game.TryGetPiece(tmpPos.Dot, out pawn))
                        {
                            if (pawn is PiecePawn)
                            {
                                if (((PiecePawn)pawn).EnableEnPassanted)
                                {
                                    enableMovein.Add(this.Position.ShiftEastSouth());
                                    enableCapture.Add(tmpPos);
                                }
                            }
                        }
                    }
                    #endregion
                }
                //剑指西南
                Position.Shift(this.GameSide, situation, this.Position.ShiftWestSouth(), enableMovein, enableCapture, false);
                //剑指东南
                Position.Shift(this.GameSide, situation, this.Position.ShiftEastSouth(), enableMovein, enableCapture, false);
                #endregion
            }
        }
    }
}
