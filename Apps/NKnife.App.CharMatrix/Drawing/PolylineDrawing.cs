namespace NKnife.App.CharMatrix.Drawing
{
    /// <summary>
    /// ’€œﬂ
    /// </summary>
    public class PolylineDrawing : LineDrawing
    {
        public override void Draw(IDrawingContext context)
        {
            context.Graphics.DrawLines(context.Pen, this.Points.ToArray());
        }
    }
}
