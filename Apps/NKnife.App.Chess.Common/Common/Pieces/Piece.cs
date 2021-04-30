using Gean.Module.Chess;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Exceptions;
using NKnife.Chesses.Common.Interface;
using NKnife.Events;

namespace NKnife.Chesses.Common.Pieces
{
    public abstract class Piece : IPiece
    {
        #region ctor

        /// <summary>
        /// 构造函数
        /// </summary>
        protected Piece(Position position) 
        {
            this.Position = this.InitPosition(position);
            this.IsCaptured = false;
        }

        protected virtual Position InitPosition(Position position)
        {
            return position;
        }

        #endregion

        #region Property

        /// <summary>
        /// 棋子的类型
        /// </summary>
        public virtual Enums.PieceType PieceType { get; protected set; }
        /// <summary>
        /// 棋子的战方
        /// </summary>
        public virtual Enums.GameSide GameSide { get; protected set; }
        /// <summary>
        /// 获取或设置该棋子是否已被杀死
        /// </summary>
        public virtual bool IsCaptured { get; internal set; }
        /// <summary>
        /// 棋子所在位置
        /// </summary>
        public virtual Position Position { get; internal set; }

        #endregion

        #region Abstract

        public abstract void SetEnablePositions(ISituation situation, out Positions enableMovein, out Positions enableCapture);

        #endregion

        #region Override

        /// <summary>
        /// 重写。生成该棋子的字符表示。
        /// </summary>
        /// <returns>大写表示为白棋，小写表示黑棋</returns>
        public override string ToString()
        {
            return Enums.FromPieceType(this.PieceType);
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            Piece man = obj as Piece;
            if (!this.PieceType.Equals(man.PieceType))
                return false;
            if (!this.GameSide.Equals(man.GameSide))
                return false;
            if (!this.IsCaptured.Equals(man.IsCaptured))
                return false;
            if (!this.Position.Equals(man.Position))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked(3 * (
                this.GameSide.GetHashCode() ^
                this.PieceType.GetHashCode() ^
                this.IsCaptured.GetHashCode() ^
                this.Position.GetHashCode()
                ));
        }

        #endregion

        #region Static

        /// <summary>
        /// 表示棋子为空(null)时。此变量为只读。
        /// </summary>
        public static readonly Piece Empty = null;
        /// <summary>
        /// 指示指定的 Piece 对象是 null 还是 Piece.Empty。
        /// </summary>
        /// <param name="chessman">指定的 Piece 对象</param>
        public static bool IsNullOrEmpty(Piece chessman)
        {
            if (chessman == null)
                return true;
            if (chessman == Piece.Empty)
                return true;
            return false;
        }

        public static Piece Creator(Enums.PieceType type, Position pos)
        {
            switch (type)
            {
                case Enums.PieceType.WhiteKing:
                    return new PieceKing(Enums.GameSide.White, pos);
                case Enums.PieceType.WhiteQueen:
                    return new PieceQueen(Enums.GameSide.White, pos);
                case Enums.PieceType.WhiteRook:
                    return new PieceRook(Enums.GameSide.White, pos);
                case Enums.PieceType.WhiteBishop:
                    return new PieceBishop(Enums.GameSide.White, pos);
                case Enums.PieceType.WhiteKnight:
                    return new PieceKnight(Enums.GameSide.White, pos);
                case Enums.PieceType.WhitePawn:
                    return new PiecePawn(Enums.GameSide.White, pos);
                case Enums.PieceType.BlackKing:
                    return new PieceKing(Enums.GameSide.Black, pos);
                case Enums.PieceType.BlackQueen:
                    return new PieceQueen(Enums.GameSide.Black, pos);
                case Enums.PieceType.BlackRook:
                    return new PieceRook(Enums.GameSide.Black, pos);
                case Enums.PieceType.BlackBishop:
                    return new PieceBishop(Enums.GameSide.Black, pos);
                case Enums.PieceType.BlackKnight:
                    return new PieceKnight(Enums.GameSide.Black, pos);
                case Enums.PieceType.BlackPawn:
                    return new PiecePawn(Enums.GameSide.Black, pos);
                case Enums.PieceType.None:
                case Enums.PieceType.AllKings:
                case Enums.PieceType.AllQueens:
                case Enums.PieceType.AllRooks:
                case Enums.PieceType.AllBishops:
                case Enums.PieceType.AllKnights:
                case Enums.PieceType.AllPawns:
                case Enums.PieceType.All:
                default:
                    throw new PieceException(string.Format(ExString.PieceTypeIsError, type.ToString()));
            }
        }

        /// <summary>
        /// 根据指定的棋子战方与指定的棋子X坐标获取开局的棋子坐标
        /// </summary>
        /// <param name="side">指定的棋子战方</param>
        /// <param name="x">指定的棋子X坐标</param>
        protected static Position GetOpenningsPosition(Enums.GameSide side, int x)
        {
            Position point = Position.Empty;
            switch (side)
            {
                case Enums.GameSide.White:
                    point = new Position(x, 1);
                    break;
                case Enums.GameSide.Black:
                    point = new Position(x, 8);
                    break;
            }
            return point;
        }

        #endregion

        #region PositionChangedEvent

        public event PositionChangedEventHandler PositionChangedEvent;
        protected virtual void OnPositionChanged(PositionChangedEventArgs e)
        {
            if (PositionChangedEvent != null)
                PositionChangedEvent(this, e);
        }
        public delegate void PositionChangedEventHandler(object sender, PositionChangedEventArgs e);
        public class PositionChangedEventArgs : ChangedEventArgs<Position>
        {
            public Enums.Action Action { get; private set; }
            public Piece MovedPiece { get; private set; }
            public Piece CaptruedPiece { get; private set; }
            public PositionChangedEventArgs(Enums.Action action, Piece movedPiece, Piece captruedPiece, Position srcPosition, Position tgtPosition)
                : base(srcPosition, tgtPosition)
            {
                this.Action = action;
                this.MovedPiece = movedPiece;
                this.CaptruedPiece = captruedPiece;
            }
        }

        #endregion
    }

}