namespace NKnife.App.CharMatrix.Drawing
{
    /// <summary>
    /// Ö±Ïß
    /// </summary>
    public class BeelineDrawing : LineDrawing
    {
        public override void Draw(IDrawingContext context)
        {
            context.Graphics.DrawLine(context.Pen, this.Points[0], this.Points[1]);
        }
    }
}
