using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NKnife.App.PictureTextPicker.Common.Controls
{
    public class DesignModeToolStripRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            var item = e.Item as ToolStripButton;
            if (item != null && item.Image != null && item.CheckState == CheckState.Checked)
            {
                var bounds = new Rectangle(Point.Empty, item.Size);
                Rectangle clipRect = bounds;
                g.FillRectangle(Brushes.LightSteelBlue, clipRect);
                var x = clipRect.X;
                var y = clipRect.Y;
                var w = clipRect.Width - 1;
                var h = clipRect.Height - 1;
                g.DrawLine(Pens.DimGray, new PointF(x, y), new PointF(w, y));
                g.DrawLine(Pens.DimGray, new PointF(x, y), new Point(x, h));
                g.DrawLine(Pens.DimGray, new PointF(x, h), new PointF(w, h));
                g.DrawLine(Pens.DimGray, new PointF(w, h), new Point(w, y));
            }
            else
            {
                base.OnRenderButtonBackground(e);
            }
        }
    }
}
