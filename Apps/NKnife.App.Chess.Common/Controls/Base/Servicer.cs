using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Gean.Module.Chess;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Controls.Resource;

namespace Gean.Gui.ChessControl
{
    public static class Servicer
    {
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static Servicer()
        {
            Servicer.PieceImages = new Dictionary<Enums.PieceType, Image>(12);
            Servicer.InitializeBoardImage();
            Servicer.InitializeGridImages();
            Servicer.InitializePieceImages();
        }

        #region Logger
        #endregion

        #region Board Image

        internal static Image BoardImage { get; private set; }

        private static void InitializeBoardImage()
        {
            Servicer.BoardImage = ImgResource.board_4;
            OnBoardImageChanged(new BoardImageChangedEventArgs(Servicer.BoardImage));
        }

        /// <summary>
        /// 更换棋盘所在桌面的背景图片
        /// </summary>
        /// <param name="boardImage">棋盘所在桌面的背景图片</param>
        public static void ChangeBoardImage(Image boardImage)
        {
            Servicer.BoardImage = boardImage;
            OnBoardImageChanged(new BoardImageChangedEventArgs(Servicer.BoardImage));
        }

        /// <summary>
        /// 在更换棋盘所在桌面的背景图片时发生
        /// </summary>
        public static event BoardImageChangedEventHandler BoardImageChangedEvent;
        private static void OnBoardImageChanged(BoardImageChangedEventArgs e)
        {
            if (BoardImageChangedEvent != null)
                BoardImageChangedEvent(e);
        }
        public delegate void BoardImageChangedEventHandler(BoardImageChangedEventArgs e);
        public class BoardImageChangedEventArgs : EventArgs
        {
            public Image BoardImage { get; private set; }
            public BoardImageChangedEventArgs(Image boardImage)
            {
                this.BoardImage = boardImage;
            }
        }

        #endregion

        #region Grid Image

        internal static Image WhiteGridImage { get; private set; }
        internal static Image BlackGridImage { get; private set; }

        private static void InitializeGridImages()
        {
            Servicer.WhiteGridImage = ImgResource.white_grid_01;
            Servicer.BlackGridImage = ImgResource.black_grid_01;

            OnGridImagesChanged(new GridImagesChangedEventArgs(Servicer.WhiteGridImage, Servicer.BlackGridImage));
        }

        /// <summary>
        /// 更换棋格的背景图片
        /// </summary>
        /// <param name="white">白棋格的背景图片</param>
        /// <param name="black">黑棋格的背景图片</param>
        public static void ChangeGridImages(Image white, Image black)
        {
            if (white == null || black == null)
            {
                Debug.Fail("Image cannot NULL.");
                return;
            }
            Servicer.WhiteGridImage = white;
            Servicer.BlackGridImage = black;

            OnGridImagesChanged(new GridImagesChangedEventArgs(Servicer.WhiteGridImage, Servicer.BlackGridImage));
        }

        /// <summary>
        /// 在更换棋格的背景图片时发生
        /// </summary>
        public static event GridImagesChangedEventHandler GridImagesChangedEvent;
        private static void OnGridImagesChanged(GridImagesChangedEventArgs e)
        {
            if (GridImagesChangedEvent != null)
                GridImagesChangedEvent(e);
        }
        public delegate void GridImagesChangedEventHandler(GridImagesChangedEventArgs e);
        public class GridImagesChangedEventArgs : EventArgs
        {
            public Image WhiteImage { get; private set; }
            public Image BlackImage { get; private set; }
            public GridImagesChangedEventArgs(Image white, Image black)
            {
                this.WhiteImage = white;
                this.BlackImage = black;
            }
        }

        #endregion

        #region Piece Images

        /// <summary>
        /// 棋子背景图片集合
        /// </summary>
        private static Dictionary<Enums.PieceType, Image> PieceImages { get; set; }
        /// <summary>
        /// 初始化默认棋子背景图片集合
        /// </summary>
        private static void InitializePieceImages()
        {
            #region Initialize Dictionary<Enums.PieceType, Image>

            Servicer.PieceImages.Add(Enums.PieceType.BlackBishop, ImgResource.black_bishop);
            Servicer.PieceImages.Add(Enums.PieceType.BlackKing, ImgResource.black_king);
            Servicer.PieceImages.Add(Enums.PieceType.BlackKnight, ImgResource.black_knight);
            Servicer.PieceImages.Add(Enums.PieceType.BlackPawn, ImgResource.black_pawn);
            Servicer.PieceImages.Add(Enums.PieceType.BlackQueen, ImgResource.black_queen);
            Servicer.PieceImages.Add(Enums.PieceType.BlackRook, ImgResource.black_rook);
            Servicer.PieceImages.Add(Enums.PieceType.WhiteBishop, ImgResource.white_bishop);
            Servicer.PieceImages.Add(Enums.PieceType.WhiteKing, ImgResource.white_king);
            Servicer.PieceImages.Add(Enums.PieceType.WhiteKnight, ImgResource.white_knight);
            Servicer.PieceImages.Add(Enums.PieceType.WhitePawn, ImgResource.white_pawn);
            Servicer.PieceImages.Add(Enums.PieceType.WhiteQueen, ImgResource.white_queen);
            Servicer.PieceImages.Add(Enums.PieceType.WhiteRook, ImgResource.white_rook);

            #endregion

            //注册棋子背景图片更换事件
            OnPieceImagesChanged(new PieceImagesChangedEventArgs(Servicer.PieceImages));
        }

        internal static Image GetPieceImage(Enums.PieceType chessmanType)
        {
            return PieceImages[chessmanType];
        }

        /// <summary>
        /// 更换棋子的背景图片集合
        /// </summary>
        /// <param name="images">棋子的背景图片集合</param>
        public static void ChangePieceImages(Dictionary<Enums.PieceType, Image> images)
        {
            Servicer.PieceImages = images;

            //注册棋子背景图片更换事件
            OnPieceImagesChanged(new PieceImagesChangedEventArgs(Servicer.PieceImages));
        }

        /// <summary>
        /// 在棋子背景图片发生更换时发生
        /// </summary>
        public static event PieceImagesChangedEventHandler PieceImagesChangedEvent;
        private static void OnPieceImagesChanged(PieceImagesChangedEventArgs e)
        {
            if (PieceImagesChangedEvent != null)
                PieceImagesChangedEvent(e);
        }
        public delegate void PieceImagesChangedEventHandler(PieceImagesChangedEventArgs e);
        public class PieceImagesChangedEventArgs : EventArgs
        {
            public Dictionary<Enums.PieceType, Image> Images { get; private set; }
            public PieceImagesChangedEventArgs(Dictionary<Enums.PieceType, Image> images)
            {
                this.Images = images;
            }
        }

        #endregion

        #region Option

        /// <summary>
        /// 自动演示(摆棋)的间隔时间(毫秒)
        /// </summary>
        public static int AutoForwardTime
        {
            get { return _autoForwardTime; }
        }
        private static int _autoForwardTime = 2000;

        #endregion

    }
}
