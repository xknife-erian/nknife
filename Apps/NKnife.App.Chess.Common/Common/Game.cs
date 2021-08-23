using System;
using System.Collections.Generic;
using Gean.Module.Chess;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Interface;
using NKnife.Chesses.Common.Pieces;

namespace NKnife.Chesses.Common
{
    [Serializable]
    public class Game : Situation, IGame
    {

        #region ctor

        public Game()
        {
            this.ActivedPieces = new Pieces.Pieces();
            this.MovedPieces = new Pieces.Pieces();
            this.PlayMode = Enums.PlayMode.ReplayGame;
        }

        #endregion

        #region Override EnPassantTargetPosition
        public override Position EnPassantTargetPosition
        {
            get
            {
                if (_piecePawnByEnPassanted == null)
                {
                    return Position.Empty;
                }
                return _piecePawnByEnPassanted.Position;
            }
            internal set
            {
                base.EnPassantTargetPosition = value;
            }
        }
        /// <summary>
        /// 一个可以被吃过路兵的“兵”
        /// </summary>
        private PiecePawn _piecePawnByEnPassanted;
        #endregion

        #region Game Initializer

        /// <summary>
        /// 初始化棋局
        /// </summary>
        public void GameInitializer()
        {
            this.Parse(NewGameFENString);
            this._isInitialized = false;
        }

        /// <summary>
        /// 判断棋盘是在初始化
        /// </summary>
        private bool _isInitialized = true;

        #endregion

        #region Set Piece Position
        protected override void SetByPosition(int dot, char value)
        {
            base.SetByPosition(dot, value);
            if (!value.Equals('1'))
            {
                if (_isInitialized)//如果是初始化，增加棋子
                    this.ActivedPieces.Add(Piece.Creator(Enums.ToPieceType(value), Position.GetPositionByDot(dot)));
                else//如果不是初始化，改变棋子的Position
                    this.ActivedPieces[dot].Position = Position.GetPositionByDot(dot);
            }
        }
        #endregion

        #region IGame

        /// <summary>
        /// 可以使用的棋子(未被俘获)
        /// </summary>
        public Pieces.Pieces ActivedPieces { get; private set; }

        /// <summary>
        /// 已被俘获的棋子
        /// </summary>
        public Pieces.Pieces MovedPieces { get; private set; }

        /// <summary>
        /// 当前棋局的模式
        /// </summary>
        public Enums.PlayMode PlayMode { get; set; }

        /// <summary>
        /// 从“可以使用的棋子集合”中获取与指定棋格的位置相关联的棋子。
        /// </summary>
        /// <param name="dot">指定棋格的位置</param>
        /// <param name="piece">关联的棋子</param>
        public bool TryGetPiece(int dot, out Piece piece)
        {
            return this.ActivedPieces.TryGetPiece(dot, out piece);
        }

        /// <summary>
        /// 从“可以使用的棋子集合”中尝试从棋子集合中判断是否包含指定的棋子，并获取棋盘位置
        /// </summary>
        /// <param name="Piece">指定的棋子</param>
        /// <param name="dot">棋盘位置</param>
        public bool TryContains(Piece piece, out int dot)
        {
            return this.ActivedPieces.TryContains(piece, out dot);
        }

        #endregion

        #region IPieceMove

        #region MoveIn

        /// <summary>
        /// 移动指定位置棋格的棋子
        /// </summary>
        /// <param name="pair">指定的成对的位置,First值为源棋格,Second值是目标棋格</param>
        public void PieceMoveIn(PositionPair pair)
        {
            Enums.ActionCollection actions = new Enums.ActionCollection();
            Piece piece;
            if (this.TryGetPiece(pair.First.Dot, out piece))
            {
                //第1步，判断一下过路兵的格子的逻辑
                if (piece is PiecePawn)
                {
                    if (pair.First.Y == pair.Second.Y)
                    {
                        return;
                    }
                }
                actions.Add(Enums.Action.General);
                //第2步，判断目标棋格中是否有棋子。
                //如果有棋子，更改Action，调用PieceMoveOut方法。
                if (this.ContainsPiece(pair.Second.Dot))
                {
                    actions.Add(Enums.Action.Capture);
                    this.PieceMoveOut(pair.Second);
                }

                //第3步，调用内部方法PieceMoveIn，移动棋子，并更改FEN记录
                this.PieceMoveIn(piece, actions, pair.First, pair.Second);

                //第4步，如上一步棋出现过“过路兵”的局面，而现在进行了新的一步棋了，故而取消上一步棋的“过路兵”局面。
                if (_piecePawnByEnPassanted != null)
                {
                    _piecePawnByEnPassanted.EnableEnPassanted = false;
                    _piecePawnByEnPassanted = null;
                }

                //第5步，判断是否过路兵的局面
                if (piece is PiecePawn)
                {
                    #region 过路兵
                    //成为能被吃过路兵的“兵”
                    if ((pair.First.Y == 1 && pair.Second.Y == 3) || (pair.First.Y == 6 && pair.Second.Y == 4))
                    {
                        char pawnChar;
                        Position tmpPos = pair.Second.ShiftWest();
                        if (tmpPos != null)
                        {
                            if (this.TryGetPiece(tmpPos.Dot, out pawnChar))
                            {
                                Enums.GameSide side = char.IsLower(pawnChar) ? Enums.GameSide.Black : Enums.GameSide.White;
                                if (this.GameSide == side)
                                {
                                    _piecePawnByEnPassanted = piece as PiecePawn;
                                    _piecePawnByEnPassanted.EnableEnPassanted = true;
                                }
                            }
                        }
                        tmpPos = pair.Second.ShiftEast();
                        if (tmpPos != null)
                        {
                            if (this.TryGetPiece(tmpPos.Dot, out pawnChar))
                            {
                                Enums.GameSide side = char.IsLower(pawnChar) ? Enums.GameSide.Black : Enums.GameSide.White;
                                if (this.GameSide == side)
                                {
                                    _piecePawnByEnPassanted = piece as PiecePawn;
                                    _piecePawnByEnPassanted.EnableEnPassanted = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        ((PiecePawn)piece).EnableEnPassanted = false;
                    }
                    #endregion
                }

                //第6步，移动棋子后，根据移动棋子后的局面生成Action，主要是判断另一战方的“王”是否被将军
                actions.Add(this.IsCheckPieceKing(piece));


                //第7步，注册棋子移动后事件
                OnPieceMoveInEvent(piece, actions, pair);
            }
        }

        #region PieceMoveIn Sub Method

        /// <summary>
        /// 内部方法：移动棋子，并更改FEN记录
        /// </summary>
        /// <param name="piece">被移动的棋子</param>
        /// <param name="srcPos">源棋格</param>
        /// <param name="tgtPos">目标棋格</param>
        private void PieceMoveIn(Piece piece, Enums.ActionCollection actions, Position srcPos, Position tgtPos)
        {
            switch (piece.PieceType)
            {
                #region case 王车易位，过路兵，升变
                case Enums.PieceType.WhiteKing:
                case Enums.PieceType.BlackKing:
                    {
                        //TODO:在这里实现王车易位
                        break;
                    }
                case Enums.PieceType.WhitePawn:
                case Enums.PieceType.BlackPawn:
                    {
                        //移走被吃的过路兵
                        if (_piecePawnByEnPassanted != null)
                        {
                            if (tgtPos.X == _piecePawnByEnPassanted.Position.X)
                            {
                                actions.Add(Enums.Action.EnPassant);
                                PieceMoveOut(_piecePawnByEnPassanted.Position);
                            }
                        }
                        //TODO:在这里实现“升变”
                        break;
                    }
                case Enums.PieceType.WhiteQueen:
                case Enums.PieceType.WhiteRook:
                case Enums.PieceType.WhiteBishop:
                case Enums.PieceType.WhiteKnight:
                case Enums.PieceType.BlackQueen:
                case Enums.PieceType.BlackRook:
                case Enums.PieceType.BlackBishop:
                case Enums.PieceType.BlackKnight:
                case Enums.PieceType.AllKings:
                case Enums.PieceType.AllQueens:
                case Enums.PieceType.AllRooks:
                case Enums.PieceType.AllBishops:
                case Enums.PieceType.AllKnights:
                case Enums.PieceType.AllPawns:
                case Enums.PieceType.All:
                case Enums.PieceType.None:
                default:
                    break;
                #endregion
            }
            piece.Position = tgtPos;
            this.SetByPosition(srcPos.Dot, '1');
            this.SetByPosition(tgtPos.Dot, Enums.FromPieceType(piece.PieceType)[0]);
            this.GameSide = Enums.GetOtherGameSide(this.GameSide);
            if (this.GameSide == Enums.GameSide.White)
            {
                this.FullMoveNumber++;
            }
            this.HalfMoveClock++;
        }

        /// <summary>
        /// 返回另一战方的“王”是否被将军
        /// </summary>
        /// <param name="action">动作</param>
        /// <param name="piece">可能产生将军动作的棋子</param>
        private Enums.Action IsCheckPieceKing(Piece piece)
        {
            //Positions postions = piece.GetEnablePositions(this);
            //Position kingPos = this.ActivedPieces.GetOtherGameSideKing(this.GameSide).Position;
            //foreach (Position pos in postions)
            //{
            //    if (pos.Equals(kingPos))
            //    {
            //        if (action == Enums.Action.General)
            //            action = Enums.Action.Check;
            //        else
            //            action = Enums.Action.CaptureCheck;
            //        break;
            //    }
            //}
            return Enums.Action.General;
        }

        #endregion

        #endregion

        #region MoveOut
        /// <summary>
        /// 移除指定位置棋格的棋子
        /// </summary>
        /// <param name="pos">指定位置</param>
        public void PieceMoveOut(Position pos)
        {
            Piece piece = this.ActivedPieces[pos.Dot];
            piece.IsCaptured = true;
            this.ActivedPieces.Remove(piece);
            this.MovedPieces.Add(piece);
            this.HalfMoveClock = 0;
            OnPieceMoveOutEvent(piece, pos);//注册棋子被俘事件
        }
        #endregion

        #region Event

        /// <summary>
        /// 在棋子被俘后发生
        /// </summary>
        public event PieceMoveOut PieceMoveOutEvent;
        protected virtual void OnPieceMoveOutEvent(Piece piece, Position pos)
        {
            if (PieceMoveOutEvent != null)
            {
                PieceMoveOutEvent(piece, pos);
            }
        }

        /// <summary>
        /// 在棋子移动后发生
        /// </summary>
        public event PieceMoveIn PieceMoveInEvent;
        protected virtual void OnPieceMoveInEvent(Piece piece, Enums.ActionCollection actions, PositionPair pair)
        {
            if (PieceMoveInEvent != null)
            {
                PieceMoveInEvent(piece, actions, pair);
            }
        }

        #endregion

        #endregion

        #region IEnumerable<Piece> 成员

        public IEnumerator<Piece> GetEnumerator()
        {
            return this.ActivedPieces.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.ActivedPieces.GetEnumerator();
        }

        #endregion

    }
}
