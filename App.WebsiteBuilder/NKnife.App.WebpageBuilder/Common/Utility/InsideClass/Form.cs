using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace Jeelu
{
    static public partial class Utility
    {
        static public class Form
        {
            static public void SetEnabled(Control owner, bool isEnabled, params Control[] excepts)
            {
                foreach (Control con in owner.Controls)
                {
                    if (Array.IndexOf<Control>(excepts, con) < 0)
                    {
                        con.Enabled = isEnabled;
                    }
                }
            }

            static public Image CreatImageForColorButton(Color c)
            {
                Bitmap bitmap = new Bitmap(16, 16, PixelFormat.Format64bppPArgb);
                Graphics g = Graphics.FromImage(bitmap);
                Rectangle r = new Rectangle(new Point(1, 1), new Size(12, 12));
                Pen blackPen = new Pen(Brushes.Black);
                Brush cBrush = new SolidBrush(c);
                g.FillRectangle(cBrush, r);
                g.DrawRectangle(blackPen, r);
                g.Flush();
                g.Dispose();
                return (Image)bitmap;
            }
        }
    }
}