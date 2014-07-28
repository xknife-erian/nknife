namespace NKnife.Draws.Common
{
    /// <summary>
    /// 图板的设计模式
    /// </summary>
    public enum ImagePanelDesignMode
    {
        /// <summary>
        /// 设计，画图
        /// </summary>
        Designing, 
        /// <summary>
        /// 选择
        /// </summary>
        Selecting, 
        /// <summary>
        /// 画板拖动
        /// </summary>
        Dragging,
        /// <summary>
        /// 缩小
        /// </summary>
        Zooming_Shrink,
        /// <summary>
        /// 放大
        /// </summary>
        Zooming_Enlarge,
    }
}
