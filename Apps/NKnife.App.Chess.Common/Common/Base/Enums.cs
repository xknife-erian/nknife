using System;
using System.Collections.Generic;
using Gean.Module.Chess;
using NKnife.Chesses.Common.Exceptions;
using NKnife.Chesses.Common.Record.StepTree;

namespace NKnife.Chesses.Common.Base
{
    public class Enums
    {

        #region Action

        /// <summary>
        /// 棋招动作枚举,未分战方
        /// </summary>
        public enum Action
        {
            #region
            /// <summary>
            /// 一般棋招动作
            /// </summary>
            General = 0,
            /// <summary>
            /// 将军(“将军”在棋步中仅是某一棋步的结果，他事实是General或Kill棋步的结果)。
            /// 故该枚举单独使用时指的是普通棋招，并该棋招产生了“将军”的结果。
            /// </summary>
            Check = 1,
            /// <summary>
            /// 开局摆棋
            /// </summary>
            Opennings = 2,
            /// <summary>
            /// 有棋被杀死(捕获, 夺得, 俘获) 
            /// </summary>
            Capture = 4,
            /// <summary>
            /// 将死，记号为“#”
            /// </summary>
            CheckMate = 8,
            /// <summary>
            /// 吃过路兵
            /// </summary>
            EnPassant = 16,
            /// <summary>
            /// 升变为后
            /// </summary>
            PromoteToQueen = 32,
            /// <summary>
            /// 升变为车
            /// </summary>
            PromoteToRook = 64,
            /// <summary>
            /// 升变为马
            /// </summary>
            PromoteToKnight = 128,
            /// <summary>
            /// 升变为象
            /// </summary>
            PromoteToBishop = 256,
            /// <summary>
            /// 王车长易位
            /// </summary>
            QueenSideCastling = 512,
            /// <summary>
            /// 王车短易位
            /// </summary>
            KingSideCastling = 1024,
            /// <summary>
            /// 无效
            /// </summary>
            Invalid = 2048,
            /// <summary>
            /// 杀棋并将军
            /// </summary>
            CaptureCheck = (Capture | Check),
            /// <summary>
            /// 王车易位
            /// </summary>
            Castling = (QueenSideCastling | KingSideCastling),
            /// <summary>
            /// 升变
            /// </summary>
            Promotion = (PromoteToBishop | PromoteToKnight | PromoteToQueen | PromoteToRook),
            /// <summary>
            /// 不升变
            /// </summary>
            NoPromotion = ~(PromoteToBishop | PromoteToKnight | PromoteToQueen | PromoteToRook),
            #endregion
        }

        public static Action ActionAdd(Action actionA, Action actionB)
        {
            switch (actionA)
            {
                case Action.General:
                    break;
                case Action.Check:
                    break;
                    break;
                case Action.Capture:
                    break;
                    break;
                case Action.EnPassant:
                    break;
                case Action.PromoteToQueen:
                    break;
                case Action.PromoteToRook:
                    break;
                case Action.PromoteToKnight:
                    break;
                case Action.PromoteToBishop:
                    break;
                case Action.QueenSideCastling:
                    break;
                case Action.KingSideCastling:
                    break;
                case Action.Invalid:
                    break;
                case Action.CaptureCheck:
                    break;
                case Action.Castling:
                    break;
                case Action.Promotion:
                    break;
                case Action.CheckMate:
                case Action.NoPromotion:
                case Action.Opennings:
                    break;
                default:
                    break;
            }
            return Action.Invalid;
        }
        public static Action ToAction(string value)
        {
            Action action = Action.General;
            if (!string.IsNullOrEmpty(value))
            {
                foreach (char c in value.ToLower())
                {
                    switch (c)
                    {
                        #region case
                        case '+':
                            action |= Action.Check;
                            break;
                        case '#':
                            action |= Action.CheckMate;
                            break;
                        case 'x':
                        case ':':
                            action |= Action.Capture;
                            break;
                        case 'q':
                            action |= Action.PromoteToQueen;
                            break;
                        case 'r':
                            action |= Action.PromoteToRook;
                            break;
                        case 'n':
                            action |= Action.PromoteToKnight;
                            break;
                        case 'b':
                            action |= Action.PromoteToBishop;
                            break;
                        #endregion
                    }
                }
            }
            else
                return action;
            return action;
        }
        public static Action ToPromoteAction(char c)
        {
            switch (c)
            {
                case 'Q':
                    return Action.PromoteToQueen;
                case 'N':
                    return Action.PromoteToKnight;
                case 'B':
                    return Action.PromoteToBishop;
                case 'R':
                    return Action.PromoteToRook;
                default:
                    return Action.General;
            }
        }
        public static string FromAction(Action action)
        {
            switch (action)
            {
                case Action.Check:
                    return "+";
                case Action.CheckMate:
                    return "#";
                case Action.Capture:
                    return "x";
                case Action.PromoteToQueen:
                    return "q";
                case Action.PromoteToRook:
                    return "r";
                case Action.PromoteToKnight:
                    return "n";
                case Action.PromoteToBishop:
                    return "b";
                case Action.EnPassant:
                    return "x";

                #region return string.Empty;

                case Action.General:
                case Action.Opennings:
                    return string.Empty;

                case Action.QueenSideCastling:
                case Action.KingSideCastling:
                case Action.Castling:
                    return string.Empty;

                case Action.Promotion:
                case Action.NoPromotion:
                    return string.Empty;

                case Action.Invalid:
                default:
                    return string.Empty;

                #endregion

            }
        }

        public class ActionCollection : ICollection<Action>
        {
            List<Action> _actions = new List<Action>();
            
            public void AddRange(IEnumerable<Action> actions)
            {
                _actions.AddRange(actions);
            }

            public Action[] ToArray()
            {
                return _actions.ToArray();
            }

            #region ICollection<Action> 成员

            public void Add(Action item)
            {
                _actions.Add(item);
            }

            public void Clear()
            {
                _actions.Clear();
            }

            public bool Contains(Action item)
            {
                return _actions.Contains(item);
            }

            public void CopyTo(Action[] array, int arrayIndex)
            {
                _actions.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return _actions.Count; }
            }

            public bool IsReadOnly
            {
                get { return false; }
            }

            public bool Remove(Action item)
            {
                return _actions.Remove(item);
            }

            #endregion

            #region IEnumerable<Action> 成员

            public IEnumerator<Action> GetEnumerator()
            {
                return _actions.GetEnumerator();
            }

            #endregion

            #region IEnumerable 成员

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return _actions.GetEnumerator();
            }

            #endregion
        }

        #endregion

        #region Result

        /// <summary>
        /// 棋局结果
        /// </summary>
        public enum Result
        {
            /// <summary>
            /// 白胜
            /// </summary>
            WhiteWin,
            /// <summary>
            /// 黑胜
            /// </summary>
            BlackWin,
            /// <summary>
            /// 和棋
            /// </summary>
            Draw,
            /// <summary>
            /// 未知
            /// </summary>
            UnKnown,
        }

        public static Result ToResult(string value)
        {
            value = value.Replace(" ", "");
            if (value.Equals(GameResult.ResultWhiteWin))
            {
                return Result.WhiteWin;
            }
            else if (value.Equals(GameResult.ResultBlackWin))
            {
                return Result.BlackWin;
            }
            else if (value.Equals(GameResult.ResultDraw1))
            {
                return Result.Draw;
            }
            else if (value.Equals(GameResult.ResultDraw2))
            {
                return Result.Draw;
            }
            else if (value.Equals(GameResult.ResultDraw3))
            {
                return Result.Draw;
            }
            else if (value.Equals(GameResult.ResultUnKnown))
            {
                return Result.UnKnown;
            }
            else
            {
                return Result.UnKnown;
            }
        }
        public static string FromResult(Result result)
        {
            switch (result)
            {
                case Result.WhiteWin:
                    return GameResult.ResultWhiteWin;
                case Result.BlackWin:
                    return GameResult.ResultBlackWin;
                case Result.Draw:
                    return GameResult.ResultDraw1;
                case Result.UnKnown:
                default:
                    return GameResult.ResultUnKnown;
            }
        }

        #endregion

        #region PlayMode

        /// <summary>
        /// 当前棋局的模式
        /// </summary>
        public enum PlayMode
        {
            /// <summary>
            /// 复盘打谱
            /// </summary>
            ReplayGame,
            /// <summary>
            /// 人机对战
            /// </summary>
            Player2Compute,
            /// <summary>
            /// 人人对战
            /// </summary>
            Player2Player,
            /// <summary>
            /// 机机对战
            /// </summary>
            Compute2Compute,
            /// <summary>
            /// 引擎分析
            /// </summary>
            EngineMode,
        }

        #endregion

        #region GamePhase

        /// <summary>
        /// 棋局阶段
        /// </summary>
        public enum GamePhase
        {
            /// <summary>
            /// 开局
            /// </summary>
            Opening,
            /// <summary>
            ///  中局
            /// </summary>
            Middlegame,
            /// <summary>
            /// 残局
            /// </summary>
            Ending,
        }

        #endregion

        #region GridSide

        /// <summary>
        /// 黑格，白格
        /// </summary>
        public enum GridSide
        {
            White = 0, Black = 1,
        }
        /// <summary>
        /// 获取棋格的另一方
        /// </summary>
        public static GridSide GetOtherGridSide(GridSide side)
        {
            if (side == GridSide.Black)
                return GridSide.White;
            return GridSide.Black;
        }

        #endregion

        #region GameSide

        /// <summary>
        /// 棋局当前战方：黑棋，白棋
        /// </summary>
        public enum GameSide
        {
            White = 0, Black = 1
        }

        public static char FormGameSide(GameSide side)
        {
            if (side == GameSide.White) return 'w';
            if (side == GameSide.Black) return 'b';
            throw new ChessException();
        }
        public static GameSide ToGameSide(char c)
        {
            if (c == 'w') return GameSide.White;
            if (c == 'b') return GameSide.Black;
            throw new ChessException();
        }
        /// <summary>
        /// 获取棋的另一战方
        /// </summary>
        public static GameSide GetOtherGameSide(GameSide currSide)
        {
            if (currSide == GameSide.Black)
                return GameSide.White;
            return GameSide.Black;
        }

        #endregion

        #region PieceType

        /// <summary>
        /// 棋子类型的枚举。
        /// 车Rook，马Knight，象Bishop，后Queen，王King，兵Pawn。
        /// 中文简称王后车象马兵英文简称K Q R B N P
        /// </summary>
        public enum PieceType
        {
            /// <summary>
            /// 嘛也不是
            /// </summary>
            None = 0,
            /// <summary>
            /// White's king
            /// </summary>
            WhiteKing = 1,
            /// <summary>
            /// White's queen
            /// </summary>
            WhiteQueen = 2,
            /// <summary>
            /// White's rook
            /// </summary>
            WhiteRook = 4,
            /// <summary>
            /// White's bishop
            /// </summary>
            WhiteBishop = 8,
            /// <summary>
            /// White's knight 
            /// </summary>
            WhiteKnight = 16,
            /// <summary>
            /// White's pawn
            /// </summary>
            WhitePawn = 32,
            /// <summary>
            /// Black's king
            /// </summary>
            BlackKing = 64,
            /// <summary>
            /// Black's queen
            /// </summary>
            BlackQueen = 128,
            /// <summary>
            /// Black's rook
            /// </summary>
            BlackRook = 256,
            /// <summary>
            /// Black's bishop
            /// </summary>
            BlackBishop = 512,
            /// <summary>
            /// Black's knight
            /// </summary>
            BlackKnight = 1024,
            /// <summary>
            /// Black's pawn
            /// </summary>
            BlackPawn = 2048,
            /// <summary>
            /// Used to hide all kings from displaying on the board.
            /// </summary>
            AllKings = WhiteKing | BlackKing,
            /// <summary>
            /// Used to hide all kings from displaying on the board.
            /// </summary>
            AllQueens = WhiteQueen | BlackQueen,
            /// <summary>
            /// Used to hide all queens from displaying on the board.
            /// </summary>
            AllRooks = WhiteRook | BlackRook,
            /// <summary>
            /// Used to hide all rooks from displaying on the board.
            /// </summary>
            AllBishops = WhiteBishop | BlackBishop,
            /// <summary>
            /// Used to hide all bishops from displaying on the board.
            /// </summary>
            AllKnights = WhiteKnight | BlackKnight,
            /// <summary>
            /// Used to hide all knights from displaying on the board.
            /// </summary>
            AllPawns = WhitePawn | BlackPawn,
            /// <summary>
            /// Used to hide all the pieces.
            /// </summary>
            All = AllBishops | AllKings | AllKnights | AllPawns | AllQueens | AllRooks
        }

        /// <summary>
        /// 返回将指定的字符解析出的棋子类型
        /// </summary>
        /// <param name="c">指定的字符</param>
        /// <returns></returns>
        public static PieceType ToPieceType(char c)
        {
            return Enums.ToPieceType(c.ToString());
        }
        public static PieceType ToPieceType(string c)
        {
            if (string.IsNullOrEmpty(c))
                throw new ArgumentNullException("Cannot Null or Empty");
            if (c.Length != 1)
                throw new FormatException(c);

            PieceType manType = PieceType.None;
            switch (c[0])
            {
                #region case
                case 'K':
                    manType = PieceType.WhiteKing;
                    break;
                case 'Q':
                    manType = PieceType.WhiteQueen;
                    break;
                case 'R':
                    manType = PieceType.WhiteRook;
                    break;
                case 'B':
                    manType = PieceType.WhiteBishop;
                    break;
                case 'N':
                    manType = PieceType.WhiteKnight;
                    break;
                case 'P':
                    manType = PieceType.WhitePawn;
                    break;
                case 'k':
                    manType = PieceType.BlackKing;
                    break;
                case 'q':
                    manType = PieceType.BlackQueen;
                    break;
                case 'r':
                    manType = PieceType.BlackRook;
                    break;
                case 'b':
                    manType = PieceType.BlackBishop;
                    break;
                case 'n':
                    manType = PieceType.BlackKnight;
                    break;
                case 'p':
                    manType = PieceType.BlackPawn;
                    break;
                #endregion
            }
            return manType;
        }
        public static PieceType ToPieceType(char c, GameSide side)
        { 
            switch (c)
            {
                #region case
                case 'P':
                    switch (side)
                    {
                        case GameSide.White:
                            return PieceType.WhitePawn;
                        case GameSide.Black:
                            return PieceType.BlackPawn;
                    }
                    break;
                case 'Q':
                    switch (side)
                    {
                        case GameSide.White:
                            return PieceType.WhiteQueen;
                        case GameSide.Black:
                            return PieceType.BlackQueen;
                    }
                    break;
                case 'K':
                    switch (side)
                    {
                        case GameSide.White:
                            return PieceType.WhiteKing;
                        case GameSide.Black:
                            return PieceType.BlackKing;
                    }
                    break;
                case 'R':
                    switch (side)
                    {
                        case GameSide.White:
                            return PieceType.WhiteRook;
                        case GameSide.Black:
                            return PieceType.BlackRook;
                    }
                    break;
                case 'N':
                    switch (side)
                    {
                        case GameSide.White:
                            return PieceType.WhiteKnight;
                        case GameSide.Black:
                            return PieceType.BlackKnight;
                    }
                    break;
                case 'B':
                    switch (side)
                    {
                        case GameSide.White:
                            return PieceType.WhiteBishop;
                        case GameSide.Black:
                            return PieceType.BlackBishop;
                    }
                    break;
                #endregion
            }
            return PieceType.None;
        }
        public static PieceType ToPieceType(GameSide side)
        {
            switch (side)
            {
                case GameSide.White:
                    return PieceType.WhitePawn;
                case GameSide.Black:
                    return PieceType.BlackPawn;
            }
            return PieceType.None;
        }
        public static string FromPieceType(PieceType type)
        {
            string man = "";
            switch (type)
            {
                #region case
                case PieceType.WhiteKing:
                    man = "K";
                    break;
                case PieceType.WhiteQueen:
                    man = "Q";
                    break;
                case PieceType.WhiteRook:
                    man = "R";
                    break;
                case PieceType.WhiteBishop:
                    man = "B";
                    break;
                case PieceType.WhiteKnight:
                    man = "N";
                    break;
                case PieceType.WhitePawn:
                    man = "P";
                    break;
                case PieceType.BlackKing:
                    man = "k";
                    break;
                case PieceType.BlackQueen:
                    man = "q";
                    break;
                case PieceType.BlackRook:
                    man = "r";
                    break;
                case PieceType.BlackBishop:
                    man = "b";
                    break;
                case PieceType.BlackKnight:
                    man = "n";
                    break;
                case PieceType.BlackPawn:
                    man = "p";
                    break;
                #endregion
            }
            return man;
        }
        public static string FromPieceTypeToStep(PieceType type)
        {
            switch (type)
            {
                case PieceType.WhiteKing:
                case PieceType.BlackKing:
                    return "K";
                case PieceType.WhiteQueen:
                case PieceType.BlackQueen:
                    return "Q";
                case PieceType.WhiteRook:
                case PieceType.BlackRook:
                    return "R";
                case PieceType.WhiteBishop:
                case PieceType.BlackBishop:
                    return "B";
                case PieceType.WhiteKnight:
                case PieceType.BlackKnight:
                    return "N";
                case PieceType.WhitePawn:
                case PieceType.BlackPawn:
                case PieceType.AllKings:
                case PieceType.AllQueens:
                case PieceType.AllRooks:
                case PieceType.AllBishops:
                case PieceType.AllKnights:
                case PieceType.AllPawns:
                case PieceType.All:
                case PieceType.None:
                default:
                    return "";
            }
        }
        public static GameSide ToGameSideByPieceType(PieceType type)
        {
            switch (type)
            {
                case PieceType.WhiteKing:
                case PieceType.WhiteQueen:
                case PieceType.WhiteRook:
                case PieceType.WhiteBishop:
                case PieceType.WhiteKnight:
                case PieceType.WhitePawn:
                    return GameSide.White;
                case PieceType.BlackKing:
                case PieceType.BlackQueen:
                case PieceType.BlackRook:
                case PieceType.BlackBishop:
                case PieceType.BlackKnight:
                case PieceType.BlackPawn:
                    return GameSide.Black;
                case PieceType.AllKings:
                case PieceType.AllQueens:
                case PieceType.AllRooks:
                case PieceType.AllBishops:
                case PieceType.AllKnights:
                case PieceType.AllPawns:
                case PieceType.All:
                case PieceType.None:
                default:
                    throw new RecordException(string.Format(ExString.PieceTypeIsError, type));
            }
        }

        #endregion

        #region PGNReaderState
        /// <summary>
        /// 当PGN文件解析时的解析状态
        /// </summary>
        public enum PGNReaderState
        {
            /// <summary>
            /// Parsing the header information
            /// </summary>
            Header,
            /// <summary>
            /// Parsing the number of a move
            /// </summary>
            Number,
            /// <summary>
            /// Parsing the color to move
            /// </summary>
            Color,
            /// <summary>
            /// Parsing white's move information
            /// </summary>
            White,
            /// <summary>
            /// Parsing black's move information.
            /// </summary>
            Black,
            /// <summary>
            /// Parsing a comment.
            /// </summary>
            Comment,
            /// <summary>
            /// Finished parsing a comment.
            /// </summary>
            EndComment,
            /// <summary>
            /// Parsing a NAG.
            /// </summary>
            Nags,
            /// <summary>
            /// Convert a Nag to text.
            /// </summary>
            ConvertNag,
            /// <summary>
            /// END.
            /// </summary>
            EndMarker,
        }
        #endregion

        #region Orientation
        /// <summary>
        /// 棋盘中的方向枚举。
        /// </summary>
        public enum Orientation
        {
            None = 0,
            /// <summary>
            /// 横方向
            /// </summary>
            Rank = 1,
            /// <summary>
            /// 纵方向
            /// </summary>
            File = 2,
            /// <summary>
            /// 斜方向
            /// </summary>
            Diagonal = 4,
        }
        #endregion

    }
}
