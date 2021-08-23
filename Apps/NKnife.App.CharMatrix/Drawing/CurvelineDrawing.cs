namespace NKnife.App.CharMatrix.Drawing
{
    /// <summary>
    /// ����
    /// </summary>
    public class CurvelineDrawing : LineDrawing
    {
        public override void Draw(IDrawingContext context)
        {
            context.Graphics.DrawCurve(context.Pen, this.Points.ToArray());
        }
    }
}
