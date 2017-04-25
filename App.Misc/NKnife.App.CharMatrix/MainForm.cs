using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NKnife.App.CharMatrix.Drawing;
using NKnife.App.CharMatrix.Outline;
using NKnife.App.CharMatrix.Outline.Data;

namespace NKnife.App.CharMatrix
{
    public partial class MainForm : Form
    {
        private IList<WordOutlineDrawing> wordOutlines;
        private Graphics graphics = null;
        private float lineWidth = 1f;

        public MainForm()
        {
            InitializeComponent();
            wordOutlines = new List<WordOutlineDrawing>();

            Font font = new Font(new FontFamily("宋体"), 100f);
            tbFont.Text = font.ToString();
            tbFont.Tag = font;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            graphics = this.CreateGraphics();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            DrawingContext context = new DrawingContext(e.Graphics);
            using (Pen p = new Pen(Color.Red, lineWidth))
            {
                context.Pen = p;
                //画图
                foreach (WordOutlineDrawing outline in wordOutlines)
                {
                    outline.Draw(context);
                }
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = string.Format("({0},{1})", e.X, e.Y);
        }

        private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DrawingContext context = new DrawingContext(graphics);
            using (Pen pen = new Pen(Color.Blue, lineWidth))
            {
                context.Pen = pen;

                if (e.Node.Nodes.Count > 0)
                {//存在子项
                    pen.Color = Color.Green;

                    Cursor.Current = Cursors.WaitCursor;
                    foreach (TreeNode node in e.Node.Nodes)
                    {
                        IDrawing drawing = node.Tag as IDrawing;
                        if (drawing != null)
                        {
                            drawing.Draw(context);
                        }
                        Thread.Sleep(250);
                    }
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    IDrawing drawing = e.Node.Tag as IDrawing;
                    if (drawing != null)
                    {
                        drawing.Draw(context);
                    }
                }
            }
        }

        #region FillWordOutlines

        /// <summary>
        /// 填充所有字符轮廓
        /// </summary>
        private void FillWordOutlines()
        {
            //清空
            wordOutlines.Clear();
            tvList.Nodes.Clear();
            //填充
            FillWordOutlines((int)numX.Value, (int)numY.Value, (int)numSpacing.Value, tbFont.Tag as Font);
            //设置线宽
            lineWidth = (float)numWidth.Value;
        }
        /// <summary>
        /// 填充所有字符轮廓
        /// </summary>
        /// <param name="x">起始X</param>
        /// <param name="y">Y值</param>
        /// <param name="spacing">字符间隔</param>
        private void FillWordOutlines(int x, int y, int spacing, Font font)
        {
            string text = tbWords.Text;
            if (!string.IsNullOrEmpty(text))
            {
                foreach (char t in text)
                {
                    TreeNode wordNode = new TreeNode(t.ToString());
                    tvList.Nodes.Add(wordNode);
                    //获取字符编码
                    uint ch = GetGB2312Coding(t);

                    //获取轮廓数据
                    DOutline outline = WordGraph.GetOutline(ch, font);
                    //构建轮廓实例
                    WordOutlineDrawing word = BuildWordOutline(outline, new PointF(x, y), wordNode);
                    wordOutlines.Add(word);

                    //下个字的起始位置＝当前起始位置＋宽度＋间隔
                    x += (int)outline.Width + spacing;
                }
            }
        }
        #endregion

        #region BuildWordOutline
        /// <summary>
        /// 通过轮廓数据构建字体轮廓
        /// </summary>
        /// <param name="outline">轮廓数据</param>
        /// <param name="offset">偏移量</param>
        /// <param name="wordNode"></param>
        /// <returns></returns>
        private WordOutlineDrawing BuildWordOutline(DOutline outline, PointF offset, TreeNode wordNode)
        {
            //获取轮廓大小
            SizeF size = new SizeF(outline.Width, outline.Height);

            WordOutlineDrawing word = new WordOutlineDrawing(size);
            //---------------------
            wordNode.Tag = word;
            //---------------------
            int index = 0;
            //遍历填充轮廓数据
            foreach (DPolygon p in outline.Polygons)
            {
                //新增多边形实例
                PolygonDrawing polygon = new PolygonDrawing();

                //---------------------
                TreeNode polygonNode = new TreeNode("多边形" + (++index));
                polygonNode.Tag = polygon;
                wordNode.Nodes.Add(polygonNode);
                //---------------------

                //起始点
                PointF start = new PointF(offset.X + ConvertUtil.FixedToFloat(p.Start.x), offset.Y - ConvertUtil.FixedToFloat(p.Start.y));

                PointF point = start;
                foreach (DLine l in p.Lines)
                {
                    LineDrawing line = null;
                    //如果类型为1则为折线，为2则为曲线
                    if (l.Type == 1) { line = new PolylineDrawing(); }
                    else { line = new CurvelineDrawing(); }

                    //加入起始点
                    line.Points.Add(point);
                    //---------------------
                    StringBuilder builder = new StringBuilder(l.Type == 1 ? "折" : "曲");
                    builder.AppendFormat(" ({0},{1}) ", point.X, point.Y);
                    //---------------------
                    foreach (POINTFX fx in l.Points)
                    {
                        point = new PointF(offset.X + ConvertUtil.FixedToFloat(fx.x), offset.Y - ConvertUtil.FixedToFloat(fx.y));
                        line.Points.Add(point);

                        builder.AppendFormat("({0},{1}) ", point.X, point.Y);
                    }
                    polygon.Lines.Add(line);

                    //---------------------
                    TreeNode lineNode = new TreeNode(builder.ToString());
                    lineNode.Tag = line;
                    polygonNode.Nodes.Add(lineNode);
                    //---------------------
                }

                if (point != start)
                {
                    //增加结束到开始的闭合线段
                    LineDrawing endLine = new BeelineDrawing();
                    endLine.Points.Add(point);
                    endLine.Points.Add(start);
                    polygon.Lines.Add(endLine);

                    //---------------------
                    TreeNode endNode = new TreeNode(string.Format("直 ({0},{1}) ({0},{1}) ", point.X, point.Y, start.X, start.Y));
                    endNode.Tag = endLine;
                    polygonNode.Nodes.Add(endNode);
                    //---------------------
                }
                //加入到字符轮廓的多边形列表中
                word.Polygons.Add(polygon);
            }
            return word;
        }
        #endregion

        #region GetGB2312Coding
        /// <summary>
        /// 获取GB2312字符码
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        private uint GetGB2312Coding(char ch)
        {
            byte[] bts = Encoding.GetEncoding("GB2312").GetBytes(new char[] { ch });
            uint val = bts[0];
            if (bts.Length > 1)
            {
                val = val * 256 + bts[1];
            }
            return val;
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            FillWordOutlines();
            //请求刷新
            this.Invalidate();
        }

        private void tbFont_DoubleClick(object sender, EventArgs e)
        {
            SelectFont();
        }

        /// <summary>
        /// 选择字体
        /// </summary>
        private void SelectFont()
        {
            fontDlg.Font = tbFont.Tag as Font;

            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                Font font = fontDlg.Font;
                tbFont.Text = font.ToString();
                tbFont.Tag = font;
            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            SelectFont();
        }
    }
}