using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using NKnife.Base;

namespace NKnife.Tools.Robot.CubeOctopus.Base
{
    public sealed partial class CubePaperControl : UserControl
    {
        private readonly Color[,] _Colors = new Color[14,11];
        private readonly RectangleF[,] _Rectangles = new RectangleF[14,11];
        private float _Side;

        public CubePaperControl()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            BackColor = Color.SlateGray;
            BorderStyle = BorderStyle.FixedSingle;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            float ww = (float) Width/14;
            float hw = (float) Height/11;
            _Side = ww > hw ? hw : ww;
            InitializePaint(e);
        }

        private void InitializePaint(PaintEventArgs e)
        {
            float initX = 0;
            if (_Side*14 <= Width)
                initX = (Width - _Side*14)/2;
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            BufferedGraphics bufferedGraphics = context.Allocate(e.Graphics, e.ClipRectangle);
            Graphics g = bufferedGraphics.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            g.Clear(BackColor);

            int xCount = _Rectangles.GetLength(0);
            int yCount = _Rectangles.GetLength(1);
            for (int x = 0; x < xCount; x++)
            {
                for (int y = 0; y < yCount; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        _Rectangles[0, 0] = new RectangleF(initX, 0, _Side, _Side);
                    }
                    else if (x > 0 && y > 0)
                    {
                        RectangleF top = _Rectangles[x - 1, y];
                        RectangleF left = _Rectangles[x, y - 1];
                        _Rectangles[x, y] = new RectangleF(top.X + _Side, left.Y + _Side, _Side, _Side);
                    }
                    else if (x <= 0)
                    {
                        RectangleF left = _Rectangles[x, y - 1];
                        _Rectangles[x, y] = new RectangleF(initX, left.Y + _Side, _Side, _Side);
                    }
                    else if (y <= 0)
                    {
                        RectangleF top = _Rectangles[x - 1, y];
                        _Rectangles[x, y] = new RectangleF(top.X + _Side, 0, _Side, _Side);
                    }

                    DrawCellBorder(g, _Rectangles[x, y].Location, x, y);

                    if (x >= 1 && x <= 3 && y >= 4 && y <= 6)
                    {
                        DrawCell(g, _Rectangles[x, y], Color.Orange, x, y);
                    }
                    else if (x >= 4 && x <= 6 && y >= 7 && y <= 9)
                    {
                        DrawCell(g, _Rectangles[x, y], Color.White, x, y);
                    }
                    else if (x >= 4 && x <= 6 && y >= 4 && y <= 6)
                    {
                        DrawCell(g, _Rectangles[x, y], Color.Blue, x, y);
                    }
                    else if (x >= 10 && x <= 12 && y >= 4 && y <= 6)
                    {
                        DrawCell(g, _Rectangles[x, y], Color.Green, x, y);
                    }
                    else if (x >= 7 && x <= 9 && y >= 4 && y <= 6)
                    {
                        DrawCell(g, _Rectangles[x, y], Color.Red, x, y);
                    }
                    else if (x >= 4 && x <= 6 && y >= 1 && y <= 3)
                    {
                        DrawCell(g, _Rectangles[x, y], Color.Yellow, x, y);
                    }
                }
            }
            bufferedGraphics.Render(e.Graphics);
            g.Dispose();
            bufferedGraphics.Dispose();
        }

        private void DrawCell(Graphics graphics, RectangleF rect, Color color, int x, int y)
        {
            var bgcolor = new SolidBrush(color);
            graphics.FillRectangle(bgcolor, rect);
            PointF loca = rect.Location;
            DrawCellBorder(graphics, loca, x, y);
            _Colors[x, y] = color;
            bgcolor.Dispose();
        }

        private void DrawCellBorder(Graphics graphics, PointF loca, int x, int y)
        {
            graphics.DrawString(x + "," + y, Font, Brushes.Black, loca.X + 3, loca.Y + 3);
            graphics.DrawLine(Pens.DimGray, loca, new PointF(loca.X, loca.Y + _Side));
            graphics.DrawLine(Pens.DimGray, loca, new PointF(loca.X + _Side, loca.Y));
            graphics.DrawLine(Pens.DimGray, new PointF(loca.X, loca.Y + _Side), new PointF(loca.X + _Side, loca.Y + _Side));
            graphics.DrawLine(Pens.DimGray, new PointF(loca.X + _Side, loca.Y), new PointF(loca.X + _Side, loca.Y + _Side));
        }

        public void Initialize()
        {
            this.InitializePaint(new PaintEventArgs(this.CreateGraphics(), new Rectangle(Location,new Size(Width,Height))));
        }

        /// <summary>L:左顺
        /// </summary>
        public void LeftCW90()
        {
            var list1 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(12, 4),
                Pair<int, int>.Build(12, 5),
                Pair<int, int>.Build(12, 6),
                Pair<int, int>.Build(4, 9),
                Pair<int, int>.Build(4, 8),
                Pair<int, int>.Build(4, 7),
                Pair<int, int>.Build(4, 6),
                Pair<int, int>.Build(4, 5),
                Pair<int, int>.Build(4, 4),
                Pair<int, int>.Build(4, 3),
                Pair<int, int>.Build(4, 2),
                Pair<int, int>.Build(4, 1),
            });
            var list2 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(1, 4),
                Pair<int, int>.Build(1, 5),
                Pair<int, int>.Build(1, 6),
                Pair<int, int>.Build(2, 6),
                Pair<int, int>.Build(3, 6),
                Pair<int, int>.Build(3, 5),
                Pair<int, int>.Build(3, 4),
                Pair<int, int>.Build(2, 4),
            }); 
            TurnRound(list1, list2);
        }

        /// <summary>L':左逆
        /// </summary>
        public void LeftCCW90()
        {
            var list1 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(12, 6),
                Pair<int, int>.Build(12, 5),
                Pair<int, int>.Build(12, 4),
                Pair<int, int>.Build(4, 1),
                Pair<int, int>.Build(4, 2),
                Pair<int, int>.Build(4, 3),
                Pair<int, int>.Build(4, 4),
                Pair<int, int>.Build(4, 5),
                Pair<int, int>.Build(4, 6),
                Pair<int, int>.Build(4, 7),
                Pair<int, int>.Build(4, 8),
                Pair<int, int>.Build(4, 9),
            });
            var list2 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(2, 4),
                Pair<int, int>.Build(3, 4),
                Pair<int, int>.Build(3, 5),
                Pair<int, int>.Build(3, 6),
                Pair<int, int>.Build(2, 6),
                Pair<int, int>.Build(1, 6),
                Pair<int, int>.Build(1, 5),
                Pair<int, int>.Build(1, 4),
            });
            TurnRound(list1, list2);
        }

        /// <summary>R:右顺
        /// </summary>
        public void RightCW90()
        {
            var list1 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(10, 6),
                Pair<int, int>.Build(10, 5),
                Pair<int, int>.Build(10, 4),
                Pair<int, int>.Build(6, 1),
                Pair<int, int>.Build(6, 2),
                Pair<int, int>.Build(6, 3),
                Pair<int, int>.Build(6, 4),
                Pair<int, int>.Build(6, 5),
                Pair<int, int>.Build(6, 6),
                Pair<int, int>.Build(6, 7),
                Pair<int, int>.Build(6, 8),
                Pair<int, int>.Build(6, 9),
            });
            var list2 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(7, 4),
                Pair<int, int>.Build(7, 5),
                Pair<int, int>.Build(7, 6),
                Pair<int, int>.Build(8, 6),
                Pair<int, int>.Build(9, 6),
                Pair<int, int>.Build(9, 5),
                Pair<int, int>.Build(9, 4),
                Pair<int, int>.Build(8, 4),
            });
            TurnRound(list1, list2);
        }

        /// <summary>R':右逆
        /// </summary>
        public void RightCCW90()
        {
            var list1 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(10, 4),
                Pair<int, int>.Build(10, 5),
                Pair<int, int>.Build(10, 6),
                Pair<int, int>.Build(6, 9),
                Pair<int, int>.Build(6, 8),
                Pair<int, int>.Build(6, 7),
                Pair<int, int>.Build(6, 6),
                Pair<int, int>.Build(6, 5),
                Pair<int, int>.Build(6, 4),
                Pair<int, int>.Build(6, 3),
                Pair<int, int>.Build(6, 2),
                Pair<int, int>.Build(6, 1),
            });
            var list2 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(8, 4),
                Pair<int, int>.Build(9, 4),
                Pair<int, int>.Build(9, 5),
                Pair<int, int>.Build(9, 6),
                Pair<int, int>.Build(8, 6),
                Pair<int, int>.Build(7, 6),
                Pair<int, int>.Build(7, 5),
                Pair<int, int>.Build(7, 4),
            });
            TurnRound(list1, list2);
        }

        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

        /// <summary>D:下顺
        /// </summary>
        public void DownCW90()
        {
            var list1 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(12, 6),
                Pair<int, int>.Build(11, 6),
                Pair<int, int>.Build(10, 6),
                Pair<int, int>.Build(9, 6),
                Pair<int, int>.Build(8, 6),
                Pair<int, int>.Build(7, 6),
                Pair<int, int>.Build(6, 6),
                Pair<int, int>.Build(5, 6),
                Pair<int, int>.Build(4, 6),
                Pair<int, int>.Build(3, 6),
                Pair<int, int>.Build(2, 6),
                Pair<int, int>.Build(1, 6),
            });
            var list2 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(4, 7),
                Pair<int, int>.Build(4, 8),
                Pair<int, int>.Build(4, 9),
                Pair<int, int>.Build(5, 9),
                Pair<int, int>.Build(6, 9),
                Pair<int, int>.Build(6, 8),
                Pair<int, int>.Build(6, 7),
                Pair<int, int>.Build(5, 7),
            });
            TurnRound(list1, list2);
        }

        /// <summary>D':下逆
        /// </summary>
        public void DownCCW90()
        {
            var list1 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(1, 6),
                Pair<int, int>.Build(2, 6),
                Pair<int, int>.Build(3, 6),
                Pair<int, int>.Build(4, 6),
                Pair<int, int>.Build(5, 6),
                Pair<int, int>.Build(6, 6),
                Pair<int, int>.Build(7, 6),
                Pair<int, int>.Build(8, 6),
                Pair<int, int>.Build(9, 6),
                Pair<int, int>.Build(10, 6),
                Pair<int, int>.Build(11, 6),
                Pair<int, int>.Build(12, 6),
            });
            var list2 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(5, 7),
                Pair<int, int>.Build(6, 7),
                Pair<int, int>.Build(6, 8),
                Pair<int, int>.Build(6, 9),
                Pair<int, int>.Build(5, 9),
                Pair<int, int>.Build(4, 9),
                Pair<int, int>.Build(4, 8),
                Pair<int, int>.Build(4, 7),
            });
            TurnRound(list1, list2);
        }

        /// <summary>U:上顺
        /// </summary>
        public void UpCW90()
        {
            var list1 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(1, 4),
                Pair<int, int>.Build(2, 4),
                Pair<int, int>.Build(3, 4),
                Pair<int, int>.Build(4, 4),
                Pair<int, int>.Build(5, 4),
                Pair<int, int>.Build(6, 4),
                Pair<int, int>.Build(7, 4),
                Pair<int, int>.Build(8, 4),
                Pair<int, int>.Build(9, 4),
                Pair<int, int>.Build(10, 4),
                Pair<int, int>.Build(11, 4),
                Pair<int, int>.Build(12, 4),
            });
            var list2 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(4, 1),
                Pair<int, int>.Build(4, 2),
                Pair<int, int>.Build(4, 3),
                Pair<int, int>.Build(5, 3),
                Pair<int, int>.Build(6, 3),
                Pair<int, int>.Build(6, 2),
                Pair<int, int>.Build(6, 1),
                Pair<int, int>.Build(5, 1),
            });
            TurnRound(list1, list2);
        }

        /// <summary>U':上逆
        /// </summary>
        public void UpCCW90()
        {
            var list1 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(12, 4),
                Pair<int, int>.Build(11, 4),
                Pair<int, int>.Build(10, 4),
                Pair<int, int>.Build(9, 4),
                Pair<int, int>.Build(8, 4),
                Pair<int, int>.Build(7, 4),
                Pair<int, int>.Build(6, 4),
                Pair<int, int>.Build(5, 4),
                Pair<int, int>.Build(4, 4),
                Pair<int, int>.Build(3, 4),
                Pair<int, int>.Build(2, 4),
                Pair<int, int>.Build(1, 4),
            });
            var list2 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(5, 1),
                Pair<int, int>.Build(6, 1),
                Pair<int, int>.Build(6, 2),
                Pair<int, int>.Build(6, 3),
                Pair<int, int>.Build(5, 3),
                Pair<int, int>.Build(4, 3),
                Pair<int, int>.Build(4, 2),
                Pair<int, int>.Build(4, 1),
            });
            TurnRound(list1, list2);
        }

        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

        /// <summary>F:前顺
        /// </summary>
        public void FrontCW90()
        {
            var list1 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(6,3),
                Pair<int, int>.Build(5,3),
                Pair<int, int>.Build(4,3),
                Pair<int, int>.Build(3,4),
                Pair<int, int>.Build(3,5),
                Pair<int, int>.Build(3,6),
                Pair<int, int>.Build(4,7),
                Pair<int, int>.Build(5,7),
                Pair<int, int>.Build(6,7),
                Pair<int, int>.Build(7,6),
                Pair<int, int>.Build(7,5),
                Pair<int, int>.Build(7,4),
            });
            var list2 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(4, 4),
                Pair<int, int>.Build(4, 5),
                Pair<int, int>.Build(4, 6),
                Pair<int, int>.Build(5, 6),
                Pair<int, int>.Build(6, 6),
                Pair<int, int>.Build(6, 5),
                Pair<int, int>.Build(6, 4),
                Pair<int, int>.Build(5, 4),
            });
            TurnRound(list1, list2);
        }

        /// <summary>F':前逆
        /// </summary>
        public void FrontCCW90()
        {
            var list1 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(7,4),
                Pair<int, int>.Build(7,5),
                Pair<int, int>.Build(7,6),
                Pair<int, int>.Build(6,7),
                Pair<int, int>.Build(5,7),
                Pair<int, int>.Build(4,7),
                Pair<int, int>.Build(3,6),
                Pair<int, int>.Build(3,5),
                Pair<int, int>.Build(3,4),
                Pair<int, int>.Build(4,3),
                Pair<int, int>.Build(5,3),
                Pair<int, int>.Build(6,3),
            });
            var list2 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(5, 4),
                Pair<int, int>.Build(6, 4),
                Pair<int, int>.Build(6, 5),
                Pair<int, int>.Build(6, 6),
                Pair<int, int>.Build(5, 6),
                Pair<int, int>.Build(4, 6),
                Pair<int, int>.Build(4, 5),
                Pair<int, int>.Build(4, 4),
            });
            TurnRound(list1, list2);
        }

        /// <summary>B:后顺
        /// </summary>
        public void BackCW90()
        {
            var list1 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(4, 1),
                Pair<int, int>.Build(5, 1),
                Pair<int, int>.Build(6, 1),
                Pair<int, int>.Build(9, 4),
                Pair<int, int>.Build(9, 5),
                Pair<int, int>.Build(9, 6),
                Pair<int, int>.Build(6, 9),
                Pair<int, int>.Build(5, 9),
                Pair<int, int>.Build(4, 9),
                Pair<int, int>.Build(1, 6),
                Pair<int, int>.Build(1, 5),
                Pair<int, int>.Build(1, 4),
            });
            var list2 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(10, 4),
                Pair<int, int>.Build(10, 5),
                Pair<int, int>.Build(10, 6),
                Pair<int, int>.Build(11, 6),
                Pair<int, int>.Build(12, 6),
                Pair<int, int>.Build(12, 5),
                Pair<int, int>.Build(12, 4),
                Pair<int, int>.Build(11, 4),
            });
            TurnRound(list1, list2);
        }

        /// <summary>B':后逆
        /// </summary>
        public void BackCCW90()
        {
            var list1 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(1, 4),
                Pair<int, int>.Build(1, 5),
                Pair<int, int>.Build(1, 6),
                Pair<int, int>.Build(4, 9),
                Pair<int, int>.Build(5, 9),
                Pair<int, int>.Build(6, 9),
                Pair<int, int>.Build(9, 6),
                Pair<int, int>.Build(9, 5),
                Pair<int, int>.Build(9, 4),
                Pair<int, int>.Build(6, 1),
                Pair<int, int>.Build(5, 1),
                Pair<int, int>.Build(4, 1),
            });
            var list2 = new List<Pair<int, int>>(new[]
            {
                Pair<int, int>.Build(11, 4),
                Pair<int, int>.Build(12, 4),
                Pair<int, int>.Build(12, 5),
                Pair<int, int>.Build(12, 6),
                Pair<int, int>.Build(11, 6),
                Pair<int, int>.Build(10, 6),
                Pair<int, int>.Build(10, 5),
                Pair<int, int>.Build(10, 4),
            });
            TurnRound(list1, list2);
        }

        private void TurnRound(List<Pair<int, int>> list1, List<Pair<int, int>> list2)
        {
            for (int k = 0; k < 3; k++)
            {
                Color tmpColor = _Colors[list1[0].First, list1[0].Second];
                for (int i = 0; i < list1.Count; i++)
                {
                    int x = list1[i].First;
                    int y = list1[i].Second;
                    if (i == list1.Count - 1)
                        _Colors[x, y] = tmpColor;
                    else
                        _Colors[x, y] = _Colors[list1[i + 1].First, list1[i + 1].Second];
                }
            }
            foreach (Pair<int, int> t in list1)
            {
                int x = t.First;
                int y = t.Second;
                DrawCell(CreateGraphics(), _Rectangles[x, y], _Colors[x, y], x, y);
            }

            if (list2 == null) return;
            for (int k = 0; k < 2; k++)
            {
                Color tmpColor = _Colors[list2[0].First, list2[0].Second];
                for (int i = 0; i < list2.Count; i++)
                {
                    int x = list2[i].First;
                    int y = list2[i].Second;
                    if (i == list2.Count - 1)
                        _Colors[x, y] = tmpColor;
                    else
                        _Colors[x, y] = _Colors[list2[i + 1].First, list2[i + 1].Second];
                }
            }
            foreach (Pair<int, int> t in list2)
            {
                int x = t.First;
                int y = t.Second;
                DrawCell(CreateGraphics(), _Rectangles[x, y], _Colors[x, y], x, y);
            }
        }

    }
}