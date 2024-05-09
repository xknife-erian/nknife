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

            Font font = new Font(new FontFamily("����"), 100f);
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
                //��ͼ
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
                {//��������
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
        /// ��������ַ�����
        /// </summary>
        private void FillWordOutlines()
        {
            //���
            wordOutlines.Clear();
            tvList.Nodes.Clear();
            //���
            FillWordOutlines((int)numX.Value, (int)numY.Value, (int)numSpacing.Value, tbFont.Tag as Font);
            //�����߿�
            lineWidth = (float)numWidth.Value;
        }
        /// <summary>
        /// ��������ַ�����
        /// </summary>
        /// <param name="x">��ʼX</param>
        /// <param name="y">Yֵ</param>
        /// <param name="spacing">�ַ����</param>
        private void FillWordOutlines(int x, int y, int spacing, Font font)
        {
            string text = tbWords.Text;
            if (!string.IsNullOrEmpty(text))
            {
                foreach (char t in text)
                {
                    TreeNode wordNode = new TreeNode(t.ToString());
                    tvList.Nodes.Add(wordNode);
                    //��ȡ�ַ�����
                    uint ch = GetGB2312Coding(t);

                    //��ȡ��������
                    DOutline outline = WordGraph.GetOutline(ch, font);
                    //��������ʵ��
                    WordOutlineDrawing word = BuildWordOutline(outline, new PointF(x, y), wordNode);
                    wordOutlines.Add(word);

                    //�¸��ֵ���ʼλ�ã���ǰ��ʼλ�ã���ȣ����
                    x += (int)outline.Width + spacing;
                }
            }
        }
        #endregion

        #region BuildWordOutline
        /// <summary>
        /// ͨ���������ݹ�����������
        /// </summary>
        /// <param name="outline">��������</param>
        /// <param name="offset">ƫ����</param>
        /// <param name="wordNode"></param>
        /// <returns></returns>
        private WordOutlineDrawing BuildWordOutline(DOutline outline, PointF offset, TreeNode wordNode)
        {
            //��ȡ������С
            SizeF size = new SizeF(outline.Width, outline.Height);

            WordOutlineDrawing word = new WordOutlineDrawing(size);
            //---------------------
            wordNode.Tag = word;
            //---------------------
            int index = 0;
            //���������������
            foreach (DPolygon p in outline.Polygons)
            {
                //���������ʵ��
                PolygonDrawing polygon = new PolygonDrawing();

                //---------------------
                TreeNode polygonNode = new TreeNode("�����" + (++index));
                polygonNode.Tag = polygon;
                wordNode.Nodes.Add(polygonNode);
                //---------------------

                //��ʼ��
                PointF start = new PointF(offset.X + ConvertUtil.FixedToFloat(p.Start.x), offset.Y - ConvertUtil.FixedToFloat(p.Start.y));

                PointF point = start;
                foreach (DLine l in p.Lines)
                {
                    LineDrawing line = null;
                    //�������Ϊ1��Ϊ���ߣ�Ϊ2��Ϊ����
                    if (l.Type == 1) { line = new PolylineDrawing(); }
                    else { line = new CurvelineDrawing(); }

                    //������ʼ��
                    line.Points.Add(point);
                    //---------------------
                    StringBuilder builder = new StringBuilder(l.Type == 1 ? "��" : "��");
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
                    //���ӽ�������ʼ�ıպ��߶�
                    LineDrawing endLine = new BeelineDrawing();
                    endLine.Points.Add(point);
                    endLine.Points.Add(start);
                    polygon.Lines.Add(endLine);

                    //---------------------
                    TreeNode endNode = new TreeNode(string.Format("ֱ ({0},{1}) ({0},{1}) ", point.X, point.Y, start.X, start.Y));
                    endNode.Tag = endLine;
                    polygonNode.Nodes.Add(endNode);
                    //---------------------
                }
                //���뵽�ַ������Ķ�����б���
                word.Polygons.Add(polygon);
            }
            return word;
        }
        #endregion

        #region GetGB2312Coding
        /// <summary>
        /// ��ȡGB2312�ַ���
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
            //����ˢ��
            this.Invalidate();
        }

        private void tbFont_DoubleClick(object sender, EventArgs e)
        {
            SelectFont();
        }

        /// <summary>
        /// ѡ������
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