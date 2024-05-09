using System;
using System.Drawing;
using System.Windows.Forms;
using NKnife.Events;
using NKnife.Win.Forms.Common;
using NKnife.Win.Forms.EventParams;

namespace NKnife.Win.Forms.Frames.Base
{
    /// <summary>
    ///     描述一个画框的核心功能
    /// </summary>
    public interface IPictureFrame
    {
        /// <summary>
        ///     设计面板中已设计的矩形列表
        /// </summary>
        RectangleList Rectangles { get; }

        /// <summary>
        ///     当前设计图像的缩放率
        /// </summary>
        double Zoom { get; }

        /// <summary>
        ///     设计面板的工作模式
        /// </summary>
        /// <param name="mode">工作模式枚举</param>
        void SetDrawingBoardDesignMode(DrawingBoardDesignMode mode);

        /// <summary>
        ///     设置打算设计的图片
        /// </summary>
        /// <param name="image">指定的设计图片</param>
        void SetSelectedImage(Image image);

        /// <summary>
        ///     当图片的放大（缩小）率发生改变后发生
        /// </summary>
        event EventHandler<ChangedEventArgs<double>> ZoomChanged;

        /// <summary>
        ///     当矩形被移除后发生
        /// </summary>
        event EventHandler<RectangleListChangedEventArgs> RectangleRemoved;

        /// <summary>
        ///     当矩形被创建后发生
        /// </summary>
        event EventHandler<RectangleListChangedEventArgs> RectangleCreated;

        /// <summary>
        ///     当矩形的属性发生改变后发生
        /// </summary>
        event EventHandler<RectangleListChangedEventArgs> RectangleUpdated;

        /// <summary>
        ///     当矩形被双击后发生
        /// </summary>
        event EventHandler<RectangleClickEventArgs> RectangleDoubleClick;

        /// <summary>
        ///     当矩形被单击后发生
        /// </summary>
        event EventHandler<RectangleClickEventArgs> RectangleClick;

        /// <summary>
        ///     当设计图片载入后发生
        /// </summary>
        event EventHandler<ImageLoadEventArgs> ImageLoaded;

        /// <summary>
        ///     当矩形即将被选择时发生
        /// </summary>
        event EventHandler<RectangleSelectingEventArgs> Selecting;

        /// <summary>
        ///     当矩形被选择后发生
        /// </summary>
        event EventHandler<RectangleSelectedEventArgs> Selected;

        /// <summary>
        ///     当设计（矩形拖动时）时发生
        /// </summary>
        event EventHandler<BoardDesignDragParamsEventArgs> DesignDragging;

        /// <summary>
        ///     当设计（拖动矩形完成时）完成时发生
        /// </summary>
        event EventHandler<BoardDesignDragParamsEventArgs> DesignDragged;

        /// <summary>
        ///     当设计面板被双击后发生
        /// </summary>
        event EventHandler<MouseEventArgs> BenchDoubleClick;

        void UpdateRectangleList(RectangleList rectangleList);
        void DeleteSelected();
        void SelectAll();

        /// <summary>
        /// 根据指定的操作模式对矩形进行操作
        /// </summary>
        /// <param name="ro">指定的操作模式</param>
        void RectangleOperating(RectangleOperation ro);

        void PressShiftKey(bool isKeyDown);
    }
}