namespace NKnife.App.CharMatrix.Drawing
{
    /// <summary>
    /// 绘图接口
    /// </summary>
    public interface IDrawing
    {
        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="context">绘图上下文</param>
        void Draw(IDrawingContext context);
    }
}
