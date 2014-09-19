namespace Jeelu.SimplusD
{
    /// <summary>
    /// 画布动作类型枚举
    /// </summary>
    public enum EnumDrawType
    {
        /// <summary>
        /// 不响应任何动作
        /// </summary>
        NoAction = 0,

        /// <summary>
        /// 绘线状态
        /// </summary>
        DrawLine = 1,

        /// <summary>
        /// 鼠标选择
        /// </summary>
        MouseSelect = 2,

        /// <summary>
        /// 只选择矩形
        /// </summary>
        MouseSelectRect = 3,

        /// <summary>
        /// 只选择线段
        /// </summary>
        MouseSelectLine = 4,

        /// <summary>
        /// 移动drawPanel
        /// </summary>
        PanelMove = 5,

        /// <summary>
        /// 改变大小比例，变大
        /// </summary>
        ZoomBigger = 6,

        /// <summary>
        /// 改变大小比例，变小
        /// </summary>
        ZoomSmaller = 7,

        /// <summary>
        /// 改变大小比例
        /// </summary>
        ZoomChang = 8,

        /// <summary>
        /// 移动绘画工具箱
        /// </summary>
        DrawToolsBoxMove = 9,

        /// <summary>
        /// 删除线
        /// </summary>
        MouseDeleteLine = 10

    }
}