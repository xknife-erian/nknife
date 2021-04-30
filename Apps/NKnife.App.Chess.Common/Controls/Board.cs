using System;
using System.Drawing;
using System.Windows.Forms;
using Gean.Module.Chess;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Collections.Specialized;
using System.Drawing.Text;
using NKnife.Chesses.Common;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Pieces;
using NKnife.Events;

namespace Gean.Gui.ChessControl
{
    /// <summary>
    /// 国际象棋棋盘控件
    /// </summary>
    public class Board : Control, IChessBoard
    {

        #region protected

        protected virtual Image OccImage { get; private set; }

        /// <summary>
        /// 获取此棋盘上所拥有的所有的棋格矩形(8*8)
        /// </summary>
        protected virtual Rectangle[,] Rectangles { get; private set; }

        /// <summary>
        /// 棋盘左上角X坐标
        /// </summary>
        protected int _XofPanel = 0;
        /// <summary>
        /// 棋盘左上角Y坐标
        /// </summary>
        protected int _YofPanel = 0;
        /// <summary>
        /// 矩形宽度
        /// </summary>
        protected int _rectangleWidth = 0;
        /// <summary>
        /// 矩形高度
        /// </summary>
        protected int _rectangleHeight = 0;

        #endregion

        #region Property

        /// <summary>
        /// 获取此棋盘所拥有(包含)的Game
        /// </summary>
        public virtual Game Game
        {
            get { return _Game; }
            set
            {
                Game oldGame = _Game;
                _Game = value;
                OnGameChanged(new GameChangedEventArgs(oldGame, value));
            }
        }
        private Game _Game;
        /// <summary>
        /// 回合编号
        /// </summary>
        public int Number { get { return _Game.FullMoveNumber; } }
        /// <summary>
        /// 获取此棋盘当前的战方
        /// </summary>
        public virtual Enums.GameSide CurrChessSide { get { return _Game.GameSide; } }

        #endregion

        #region ctor

        /// <summary>
        /// 构造函数
        /// </summary>
        public Board()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);//通知系统，当控件大小发生改变的时候应该进行重绘
            this.BackColor = Color.Chocolate;
            this.MinimumSize = new Size(300, 300);

            this.EnableMoveInPosition = new Positions();
            this.EnableCapturePosition = new Positions();

            this.Rectangles = new Rectangle[8, 8];
            this.BackgroundImage = Servicer.BoardImage;

            Board.GetRectangleSize(this.Size, out _XofPanel, out _YofPanel, out _rectangleWidth, out _rectangleHeight);

            Servicer.BoardImageChangedEvent += new Servicer.BoardImageChangedEventHandler(ChessBoardHelper_BoardImageChangedEvent);
            Servicer.GridImagesChangedEvent += new Servicer.GridImagesChangedEventHandler(ChessBoardHelper_GridImagesChangedEvent);
            Servicer.PieceImagesChangedEvent += new Servicer.PieceImagesChangedEventHandler(ChessBoardHelper_PieceImagesChangedEvent);
        }

        #endregion

        #region LoadSituation Method

        /// <summary>
        /// 载入新局面
        /// </summary>
        public virtual void LoadSituation()
        {
            _Game = new Game();
            _Game.GameInitializer();
            this.Refresh();
        }
        /// <summary>
        /// 载入新局面
        /// </summary>
        /// <param name="fenString">指定的棋子集合，一般是指残局或中盘棋局</param>
        public virtual void LoadSituation(string fenString)
        {
            _Game = new Game();
            this.Refresh();
        }

        #endregion

        #region Override: OnResize, OnCreateControl

        //1.
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.OccImage = new Bitmap(this.Width, this.Height);
            Board.GetRectangleSize(this.Size, out _XofPanel, out _YofPanel, out _rectangleWidth, out _rectangleHeight);
        }

        //2. 该方法仅会在最初执行一次
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        #endregion

        #region Override: OnPaint……

        //3.
        protected override void OnPaintBackground(PaintEventArgs pe)
        {
            base.OnPaintBackground(pe);
            this.Paint_ChessBoardGrid();
            this.Paint_BoardFrame();
            this.Paint_PositionNumber();
            if (_Game != null)
            {
                this.Paint_PieceImage();
                this.Paint_EnableMoveInPosition();
                this.Paint_MouseMovedPosition();
                this.Paint_SelectedPosition();
                this.Paint_SelectedMovedPiece();
            }
            //绘制控件图片
            Graphics g = pe.Graphics;
            g.DrawImage(this.OccImage, new Point(0, 0));
            g.Flush();
        }

        //4.
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        #endregion

        #region Override: OnKey……

        /* OnKeyUp
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (_Game == null) return;

            switch (e.KeyCode)
            {
                #region 方向控制
                case Keys.Down:
                    if (this.KeySelectPosition.Y > 1)
                    {
                        this.KeySelectPosition = new Position(this.KeySelectPosition.X, this.KeySelectPosition.Y - 1);
                        this.SetSelectedPointByKey();
                    }
                    break;
                case Keys.Up:
                    if (this.KeySelectPosition.Y < 8)
                    {
                        this.KeySelectPosition = new Position(this.KeySelectPosition.X, this.KeySelectPosition.Y + 1);
                        this.SetSelectedPointByKey();
                    }
                    break;
                case Keys.Left:
                    if (this.KeySelectPosition.X > 1)
                    {
                        this.KeySelectPosition = new Position(this.KeySelectPosition.X - 1, this.KeySelectPosition.Y);
                        this.SetSelectedPointByKey();
                    }
                    break;
                case Keys.Right:
                    if (this.KeySelectPosition.Y < 8)
                    {
                        this.KeySelectPosition = new Position(this.KeySelectPosition.X + 1, this.KeySelectPosition.Y);
                        this.SetSelectedPointByKey();
                    }
                    break;
                #endregion

                #region 选择
                case Keys.Space:
                    this.ViewKeyRectangle = true;
                    this.SetSelectedPointByKey();
                    break;
                case Keys.Enter:
                    break;
                case Keys.Escape:
                    this.ViewKeyRectangle = false;
                    break;
                #endregion

                #region 棋子快捷键
                case Keys.P:
                    break;
                case Keys.R:
                    break;
                case Keys.N:
                    break;
                case Keys.B:
                    break;
                case Keys.K:
                    break;
                case Keys.Q:
                    break;
                #endregion

                default:
                    break;
            }
            this.Invalidate();
        }
        */

        #endregion

        #region Override: OnMouse……

        /// <summary>
        /// 鼠标左键是否被按下
        /// </summary>
        private bool _isMouseDown = false;
        /// <summary>
        /// 被选中的棋子能杀死的棋子的位置
        /// </summary>
        protected Positions EnableCapturePosition;
        /// <summary>
        /// 被选中的棋子能移动到的位置
        /// </summary>
        protected Positions EnableMoveInPosition;
        /// <summary>
        /// 获取已选择的棋子所在的棋格坐标
        /// </summary>
        protected Position SelectedPosition;
        /// <summary>
        /// 获取当鼠标悬浮移动到的棋格坐标(包含有效棋子)
        /// </summary>
        protected Position SelectPositionByMouseMoved;
        /// <summary>
        /// 被选择的棋子
        /// </summary>
        protected Piece SelectedPiece;
        /// <summary>
        /// 被选择的棋子的图像
        /// </summary>
        protected Image SelectedPieceImage;
        /// <summary>
        /// 被选择的矩形(该矩形已被验证含有符合规则的棋子，该棋子为this.SelectPiece)
        /// </summary>
        protected Rectangle SelectedMovedRectangle;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _isMouseDown = true;
            if (_Game == null) return;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    #region 选择Position,并MoveIn
                    for (int x = 1; x <= 8; x++)
                    {
                        for (int y = 1; y <= 8; y++)
                        {
                            if (!this.Rectangles[x - 1, y - 1].Contains(e.Location))
                                continue;
                            if (_Game.TryGetPiece(Position.CalculateDot(x, y), out this.SelectedPiece))   //Pieces.TryGetPiece(position.Dot, out this.SelectedPiece))
                            {
                                if (this.SelectedPiece.GameSide != this.CurrChessSide)
                                {
                                    this.SelectedPiece = null;
                                    return;//找到棋子，但棋子战方不符
                                }
                                this.SelectedPosition = new Position(x, y);
                                this.SelectedPiece.SetEnablePositions(_Game, out this.EnableMoveInPosition, out this.EnableCapturePosition);
                                this.SelectedPieceImage = Servicer.GetPieceImage(this.SelectedPiece.PieceType);
                                this.Refresh();
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    #endregion
                    break;
                case MouseButtons.Middle:
                case MouseButtons.Right:
                case MouseButtons.None:
                case MouseButtons.XButton1:
                case MouseButtons.XButton2:
                default:
                    break;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isMouseDown = false;
            if (_Game == null) return;
            for (int x = 1; x <= 8; x++)
            {
                for (int y = 1; y <= 8; y++)
                {
                    if (!this.Rectangles[x - 1, y - 1].Contains(e.Location) || this.SelectedPiece == null)
                    {
                        continue;
                    }
                    //开始移动
                    Position tgtPosition = new Position(x, y);
                    //吃子
                    foreach (Position pos in EnableCapturePosition)
                    {
                        if (pos.Equals(tgtPosition))
                        {
                            PositionPair pair = new PositionPair(this.SelectedPosition, tgtPosition);
                            _Game.PieceMoveIn(pair);
                            OnPlay(new PlayEventArgs());
                            goto forEnd;
                        }
                    }
                    //仅移动
                    foreach (Position pos in EnableMoveInPosition)
                    {
                        if (pos.Equals(tgtPosition))
                        {
                            PositionPair pair = new PositionPair(this.SelectedPosition, tgtPosition);
                            _Game.PieceMoveIn(pair);
                            OnPlay(new PlayEventArgs());
                            goto forEnd;
                        }
                    }
                    break;
                }
            }
        forEnd:
            this.SelectedPosition = Position.Empty;
            this.EnableMoveInPosition = null;
            this.SelectedPiece = null;
            this.SelectedPieceImage = null;
            this.SelectedMovedRectangle = Rectangle.Empty;
            this.Cursor = Cursors.Default;
            this.Refresh();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_Game == null) return;
            for (int x = 1; x <= 8; x++)
            {
                for (int y = 1; y <= 8; y++)
                {
                    if (!this.Rectangles[x - 1, y - 1].Contains(e.Location))
                    {
                        if (!_isMouseDown)
                            this.Cursor = Cursors.Default;
                        continue;
                    }
                    if (_isMouseDown)
                    {
                        #region 拖拽可移动棋子
                        if (this.SelectedPiece != null) this.Cursor = Cursors.Hand;
                        this.SelectedMovedRectangle = new Rectangle(
                            new Point(e.X - _currManRect.Width / 2, e.Y - _currManRect.Height / 2), _currManRect.Size);
                        this.Refresh();
                        #endregion
                    }
                    else
                    {
                        #region 高亮可移动棋子
                        int dot = Position.CalculateDot(x, y);
                        if (!_Game.ContainsPiece(dot))
                        {
                            this.SelectPositionByMouseMoved = null;
                            this.Refresh();
                            return;//找到矩形，但矩形中无棋子，退出
                        }
                        Piece selectPiece;
                        if (_Game.TryGetPiece(dot, out selectPiece))
                        {
                            if (selectPiece.GameSide != this.CurrChessSide)
                            {
                                return;//找到棋子，但棋子战方不符
                            }
                            this.Cursor = Cursors.Hand;
                            this.SelectPositionByMouseMoved = new Position(x, y);
                            this.Refresh();
                            return;
                        }
                        #endregion
                    }
                }
            }//for x
        }

        #endregion

        #region protected virtual: === Paint ===

        #region Paint_ChessBoardGrid
        /// <summary>
        /// 绘制棋盘
        /// </summary>
        protected virtual void Paint_ChessBoardGrid()
        {
            Graphics g = Graphics.FromImage(this.OccImage);
            for (int x = 1; x <= 8; x++)
            {
                for (int y = 1; y <= 8; y++)
                {
                    _currRect = Board.GetRectangle(x, y, _XofPanel, _YofPanel, _rectangleWidth, _rectangleHeight);
                    this.Rectangles[x - 1, y - 1] = _currRect;
                    if ((x + y) % 2 != 0)
                    {
                        g.DrawImage(Servicer.WhiteGridImage, _currRect);
                        g.DrawRectangle(Pens.Black, _currRect);
                    }
                    else
                    {
                        g.DrawImage(Servicer.BlackGridImage, _currRect);
                        g.DrawRectangle(Pens.Black, _currRect);
                    }
                }
            }
            _currRect = Rectangle.Empty;
        }
        /// <summary>
        /// 仅供棋盘绘制时声明的棋子背景图片Image变量。防止反复在OnPaint事件中声明造成内存碎片。
        /// </summary>
        private Image _currManImage;
        /// <summary>
        /// 仅供棋盘绘制时声明的矩形变量。防止反复在OnPaint事件中声明造成内存碎片。
        /// </summary>
        private Rectangle _currRect = Rectangle.Empty;
        #endregion

        #region Paint_PieceImage
        /// <summary>1. 用指定的棋子图片集合绘制棋子
        /// </summary>
        protected virtual void Paint_PieceImage()
        {
            Graphics g = Graphics.FromImage(this.OccImage);
            foreach (Piece piece in _Game)
            {
                if (piece.IsCaptured)
                    continue;
                _currManRect = Board.GetPieceRectangle(this, piece.Position);
                _currManImage = Servicer.GetPieceImage(piece.PieceType);
                g.DrawImage(_currManImage, _currManRect);

            }
            _currManImage = null;
        }
        /// <summary>
        /// 仅供棋盘绘制时声明棋子背景图片的矩形变量。防止反复在OnPaint事件中声明造成内存碎片。
        /// </summary>
        private Rectangle _currManRect;
        #endregion

        #region Paint_EnableMoveInPosition
        /// <summary>2. 绘制能移动到的矩形位置
        /// </summary>
        protected virtual void Paint_EnableMoveInPosition()
        {
            if (this.SelectedPosition == null)
                return;
            if (this.EnableMoveInPosition == null)
                return;
            Graphics g = Graphics.FromImage(this.OccImage);
            Rectangle rect = Rectangle.Empty;
            foreach (Position position in this.EnableMoveInPosition)
            {
                rect = this.Rectangles[position.X, position.Y];
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(80, Color.YellowGreen)))
                {
                    g.FillRectangle(brush, rect);
                }
            }
            foreach (Position position in this.EnableCapturePosition)
            {
                rect = this.Rectangles[position.X, position.Y];
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(120, Color.Blue)))
                {
                    g.FillRectangle(brush, rect);
                }
            }
        }
        #endregion

        #region Paint_MouseMovedPosition
        /// <summary>3. 绘制鼠标移动到的矩形位置
        /// </summary>
        protected virtual void Paint_MouseMovedPosition()
        {
            if (this.SelectPositionByMouseMoved == null)
                return;
            Graphics g = Graphics.FromImage(this.OccImage);
            Rectangle rect = Rectangle.Empty;
            rect = this.Rectangles[this.SelectPositionByMouseMoved.X, this.SelectPositionByMouseMoved.Y];
            for (int i = -1; i <= 1; i++)
            {
                g.DrawRectangle(Pens.Red, Rectangle.Inflate(rect, i, i));
            }
        }
        #endregion

        #region Paint_SelectedPosition
        /// <summary>4. 绘制已选择的矩形位置
        /// </summary>
        protected virtual void Paint_SelectedPosition()
        {
            if (this.SelectedPosition == null)
                return;
            Graphics g = Graphics.FromImage(this.OccImage);
            Rectangle rect = Rectangle.Empty;
            rect = this.Rectangles[this.SelectedPosition.X, this.SelectedPosition.Y];
            for (int i = -1; i <= 1; i++)
            {
                g.DrawRectangle(Pens.Yellow, Rectangle.Inflate(rect, i, i));
            }
        }
        #endregion

        #region Paint_SelectedMovedPiece
        /// <summary>5. 绘制移动的棋子
        /// </summary>
        private void Paint_SelectedMovedPiece()
        {
            if (this.SelectedPieceImage == null)
                return;
            Graphics g = Graphics.FromImage(this.OccImage);
            g.DrawImage(this.SelectedPieceImage, this.SelectedMovedRectangle);
        }

        #endregion

        #region Paint_BoardFrame
        private int _frameWidth = 0;
        /// <summary>
        /// 6.绘制棋盘外边框
        /// </summary>
        private void Paint_BoardFrame()
        {
            _frameWidth = _rectangleWidth / 5;
            Graphics g = Graphics.FromImage(this.OccImage);
            Rectangle rect = new Rectangle(_XofPanel, _YofPanel, _rectangleWidth * 8, _rectangleHeight * 8);
            for (int i = 1; i < _frameWidth; i++)
            {
                g.DrawRectangle(Pens.Khaki, Rectangle.Inflate(rect, i, i));
            }

        }
        #endregion

        #region Paint_PositionNumber
        /// <summary>
        /// 7.绘制棋盘坐标
        /// </summary>
        private void Paint_PositionNumber()
        {
            //3磅 = 4像素
            string str = "abcdefgh";
            Graphics g = Graphics.FromImage(this.OccImage);
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            Font font = new Font("Verdana", (_rectangleHeight / 3));// * (4 / 3));
            SizeF sf = g.MeasureString("8", font);

            for (int i = 8; i >= 1; i--)
            {
                int x = (int)(_XofPanel - _frameWidth - sf.Width - 3);
                int y = (int)(_YofPanel - _frameWidth + ((8 - i) * _rectangleHeight) + _rectangleHeight / 2.3);
                RectangleF rectF = new RectangleF(x, y, sf.Width, sf.Height);
                g.DrawString(i.ToString(), font, Brushes.Black, rectF);
            }
            for (int i = 0; i < 8; i++)
            {
                int x = (int)(_XofPanel + (i * _rectangleHeight) + _rectangleHeight / 2.8);
                int y = (int)(_YofPanel + (8 * _rectangleHeight) + _frameWidth  + 3);
                RectangleF rectF = new RectangleF(x, y, sf.Width, sf.Height);
                g.DrawString(str.Substring(i, 1), font, Brushes.Black, rectF);
            }
        }
        #endregion

        #endregion

        #region private Invalidate Method

        /// <summary>
        /// 使控件指定的 "rectangle" 以外的区域无效，重新绘制更新区域
        /// </summary>
        /// <param name="rectangle">指定的 "rectangle"</param>
        private void InvalidateBoard(Rectangle rectangle)
        {
            this.InvalidateBoard(rectangle, null);
        }
        /// <summary>
        /// 使控件指定的 "rectangle" 以外的区域无效，重新绘制更新区域
        /// </summary>
        /// <param name="rectangle">指定的 "rectangle"</param>
        /// <param name="image">指定的 "rectangle" 的背景图片</param>
        private void InvalidateBoard(Rectangle rectangle, Image image)
        {
            Graphics g = this.CreateGraphics();
            g.FillRectangle(Brushes.Transparent, rectangle);
            if (image != null)
            {
                g.DrawImage(image, rectangle);
            }

            // 使控件的 "rectangle" 以外的区域无效，重新绘制更新区域
            Point pt = rectangle.Location;
            Rectangle top = new Rectangle(0, 0, this.Width, pt.Y);
            Rectangle left = new Rectangle(0, pt.Y, pt.X, rectangle.Height + 1);
            Rectangle right = new Rectangle
                (pt.X + rectangle.Width + 1, pt.Y, this.Width - pt.Y - rectangle.Width, rectangle.Height + 1);
            Rectangle bottom = new Rectangle
                (0, pt.Y + rectangle.Height + 1, this.Width, this.Height - pt.Y - rectangle.Height);
            this.Invalidate(top, false);
            this.Invalidate(left, false);
            this.Invalidate(right, false);
            this.Invalidate(bottom, false);
        }

        #endregion

        #region Image Changed Event

        protected virtual void ChessBoardHelper_BoardImageChangedEvent(Servicer.BoardImageChangedEventArgs e)
        {
            this.BackgroundImage = e.BoardImage;
            this.Invalidate();
        }
        protected virtual void ChessBoardHelper_GridImagesChangedEvent(Servicer.GridImagesChangedEventArgs e)
        {
            this.Invalidate();
        }
        protected virtual void ChessBoardHelper_PieceImagesChangedEvent(Servicer.PieceImagesChangedEventArgs e)
        {
            this.Invalidate();
        }

        #endregion

        #region static

        /// <summary>
        /// 获取棋子将绘制的矩形(棋子填充比棋格小15%，以保证美观)
        /// </summary>
        /// <param name="board">传递引用的ChessBoard类型</param>
        /// <param name="chessGrid">ChessGrid</param>
        private static Rectangle GetPieceRectangle(Board board, Position pos)
        {
            int offset = (int)(board._rectangleWidth * 0.15);//棋子填充比棋格小20%，以保证美观
            Rectangle boardRect = board.Rectangles[pos.X, pos.Y];
            Rectangle rtnRect = new Rectangle();
            rtnRect.Location = new Point(boardRect.X + offset, boardRect.Y + offset);
            rtnRect.Size = new Size(boardRect.Width - offset * 2, boardRect.Height - offset * 2);
            return rtnRect;
        }

        /// <summary>
        /// 根据棋盘棋格位置计算出实际棋格的绝对位置信息
        /// </summary>
        /// <param name="x">棋格X坐标</param>
        /// <param name="y">棋格Y坐标</param>
        /// <param name="XofPanel">棋盘左上角X坐标</param>
        /// <param name="YofPanel">棋盘左上角Y坐标</param>
        /// <param name="rectangleWidth">矩形宽度</param>
        /// <param name="rectangleHeight">矩形高度</param>
        /// <returns></returns>
        private static Rectangle GetRectangle
            (int x, int y, int XofPanel, int YofPanel, int rectangleWidth, int rectangleHeight)
        {
            Point point = new Point((x - 1) * rectangleWidth + XofPanel, (8 - y) * rectangleHeight + YofPanel);
            Size size = new Size(rectangleWidth, rectangleHeight);
            return new Rectangle(point, size);
        }

        /// <summary>
        /// 根据桌面的尺寸获取棋格的相关尺寸信息
        /// </summary>
        /// <param name="panelSize">桌面大小</param>
        /// <param name="offsetPanelX">棋盘在桌面的左上角的X坐标</param>
        /// <param name="offsetPanelY">棋盘在桌面的左上角的Y坐标</param>
        /// <param name="rectangleWidth">棋盘矩形的宽度</param>
        /// <param name="rectangleHeight">棋盘矩形的高度</param>
        private static void GetRectangleSize
            (Size panelSize, out int XofPanel, out int YofPanel, out int rectangleWidth, out int rectangleHeight)
        {
            if (panelSize.Height <= panelSize.Width)
            {
                rectangleWidth = (int)(panelSize.Height / 10);
                rectangleHeight = rectangleWidth;
                XofPanel = (int)((panelSize.Width - (rectangleWidth * 8)) / 2);
                YofPanel = rectangleWidth;
            }
            else
            {
                rectangleWidth = (int)(panelSize.Width / 10);
                rectangleHeight = rectangleWidth;
                XofPanel = rectangleWidth;
                YofPanel = (int)((panelSize.Height - (rectangleWidth * 8)) / 2);
            }
        }

        #endregion

        #region Custom EVENT

        /// <summary>
        /// 当棋盘拥有的棋局发生变化后发生
        /// </summary>
        public event GameChangedEventHandler GameChangedEvent;
        protected virtual void OnGameChanged(GameChangedEventArgs e)
        {
            if (GameChangedEvent != null)
                GameChangedEvent(this, e);
        }
        public delegate void GameChangedEventHandler(object sender, GameChangedEventArgs e);
        public class GameChangedEventArgs : ChangedEventArgs<Game>
        {
            public GameChangedEventArgs(Game oldGame,Game newGame)
                : base(oldGame, newGame)
            { 
            }
        }

        /// <summary>
        /// 在行棋后发生
        /// </summary>
        public event PlayEventHandler PlayEvent;
        protected virtual void OnPlay(PlayEventArgs e)
        {
            if (PlayEvent != null)
                PlayEvent(this, e);
        }
        public delegate void PlayEventHandler(object sender, PlayEventArgs e);
        public class PlayEventArgs : EventArgs
        {
        }

        #endregion


    }

}
