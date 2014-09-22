using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class MergeRectForm : BaseForm
    {
        #region 字段成员定义

        
        private int _offset = 5;
        private int x, y;
        private int w = 0;
        private int h = 0;

        Brush backBrush = new SolidBrush(Color.FromArgb(70,Color.White));
        Brush selectedBrush = new SolidBrush(Color.FromArgb(70,Color.Blue));
        Pen pen = new Pen(Color.Black,1);

        Image holdImg;
        Image bmp;

        #endregion

        #region 属性成员定义


        public List<GroupBox> GroupBoxList { get; set; }

        private List<NeighbourRect> NeighbourRectList { get; set; }

        public List<Rect> SelectedRects { get; set; }

        public Rect HoldRect { get; set; }

        #endregion

        #region 构造函数

        public MergeRectForm(List<NeighbourRect> neighbourRectList,Image img )
        {
            holdImg = img;
            GroupBoxList = new List<GroupBox>();
            SelectedRects = new List<Rect>();
            NeighbourRectList = neighbourRectList;
            if (neighbourRectList.Count > 0)
            {
                HoldRect = NeighbourRectList[0].FirstRect;
            }
            foreach (NeighbourRect rect in neighbourRectList)
            {
                SelectedRects.Add(rect.FirstRect);
                SelectedRects.Add(rect.SecondRect);
            }
            InitializeComponent();
            GetPanelInfo();
            foreach (NeighbourRect nbRect in NeighbourRectList)
            {
                radioButtonFirst.Text = ((SnipRect)nbRect.FirstRect).SnipName;
                radioButtonSenond.Text = ((SnipRect)nbRect.SecondRect).SnipName;
                radioButtonFirst.Checked = true;
               
                groupBox.Controls.Add(radioButtonFirst);
                groupBox.Controls.Add(radioButtonSenond);
                GroupBoxList.Add(groupBox);
            }
        }

        public MergeRectForm(List<Rect> selectedRects,Image img)
        {
            holdImg = img;
            GroupBoxList = new List<GroupBox>();
            SelectedRects = new List<Rect>();
            InitializeComponent();
            groupBox.Controls.Clear();
            SelectedRects = selectedRects;
            HoldRect = SelectedRects[0];
            GetPanelInfo();
            int groupBoxHeight = 20;
           
            RadioButton radioBtn;

            ///动态生成groupBox,和其内部的radioButton
            foreach (Rect rect in SelectedRects)
            {
                radioBtn = new RadioButton();
                radioBtn.AutoSize = true;
                radioBtn.Name = ((SnipRect)rect).SnipName;
                radioBtn.Text = ((SnipRect)rect).SnipName;
               
                groupBox.Controls.Add(radioBtn);
                radioBtn.Location = new Point(16, groupBoxHeight);
                groupBoxHeight += radioBtn.Height + 10;                
            }
            groupBoxHeight += 10;
            this.GroupBoxList.Add(groupBox);
            groupBox.Size = new Size(groupBox.Width, groupBoxHeight);
            ((RadioButton)groupBox.Controls[0]).Checked = true;
        }

        #endregion

        #region 内部函数

        private void GetPanelInfo()
        {
            x = SelectedRects[0].X;
            y = SelectedRects[0].Y;
            foreach (Rect rect in SelectedRects)
            {
                if (x >= rect.X)
                    x = rect.X;
                if (y >= rect.Y)
                    y = rect.Y;
            }
            foreach (Rect rect in SelectedRects)
            {
                if (rect.X == x && rect.Y == y)
                    HoldRect = rect;
                if (rect.X == x)
                    h += rect.Height;
                if (rect.Y == y)
                    w += rect.Width;
            }

            bmp = new Bitmap(w, h);
            if (holdImg != null)
            {

                Graphics gBmp = Graphics.FromImage(bmp);
                gBmp.DrawImage(holdImg, new Rectangle(0, 0, bmp.Width, bmp.Height), HoldRect.X, HoldRect.Y, bmp.Width, bmp.Height, GraphicsUnit.Pixel);
            }
            else
            {
                Graphics gNull = Graphics.FromImage(bmp);
                gNull.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);
            }

            if (w < 210)
                w = 210;
            if (h < 85)
                h = 85;
            this.selectPanel.Width = 15 + w;
            this.selectPanel.Height = 15 + h;
            this.selectPanel.Paint += new PaintEventHandler(selectPanel_Paint);
            this.selectPanel.MouseClick += new MouseEventHandler(selectPanel_MouseClick);
            Width = 45 + w;
            Height = 105 + h;
            x = x - _offset;
            y = y - _offset;
        }

        private void PaintRects(Graphics g)
        {
            //g.FillRectangle(backBrush, _offset, _offset, w, h);
            g.DrawImage(bmp, _offset, _offset);
            PaintHoldRect(g);
            //g.FillRectangle(selectedBrush,_holdRect.X-x,_holdRect.Y-y,_holdRect.Width,_holdRect.Height);
            foreach (Rect rect in SelectedRects)
            {
                g.DrawRectangle(pen,rect.X-x,rect.Y-y,rect.Width,rect.Height);
            }
        }

        private void PaintHoldRect(Graphics g)
        {
            if (HoldRect == null)
            {
                return;
            }
            Color c = Color.FromArgb(70, Color.Blue);
            Bitmap bmp = new Bitmap(HoldRect.Width, HoldRect.Height);
            if (holdImg != null)
            {

                Graphics gBmp = Graphics.FromImage(bmp);
                gBmp.DrawImage(holdImg, new Rectangle(0, 0, bmp.Width, bmp.Height), HoldRect.X, HoldRect.Y, bmp.Width, bmp.Height, GraphicsUnit.Pixel);
                gBmp.FillRectangle(new SolidBrush(c), 0, 0, bmp.Width, bmp.Height);

            }
            else
            {
                Graphics gNull = Graphics.FromImage(bmp);
                gNull.FillRectangle(new SolidBrush(c), 0, 0, bmp.Width, bmp.Height);
            }

            g.DrawImage(bmp, HoldRect.X - x, HoldRect.Y - y);

        }

        private bool InRect(Rect rect, MouseEventArgs e)
        {
            int eX = e.X;
            int eY = e.Y;
            if (eX<=rect.X-x ||eX > rect.X-x + rect.Width || eY <= rect.Y - y || eY > rect.Y-y+rect.Height)
            {
                return false;
            }
            return true;
        }

        void selectPanel_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (Rect rect in SelectedRects)
            {
                if (InRect(rect,e))
                {
                    HoldRect = rect;
                    selectPanel.Invalidate();
                    selectPanel.Update();
                    foreach (Control btn in groupBox.Controls)
                    {
                        if (btn.Name == ((SnipRect)rect).SnipName)
                        {
                            ((RadioButton)btn).Checked = true;
                        }
                    }
                }
            }
        }

        void selectPanel_Paint(object sender, PaintEventArgs e)
        {
            PaintRects(e.Graphics);
        }

        #endregion

        #region 消息处理

        #endregion
    }
}