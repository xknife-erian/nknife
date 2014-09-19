using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class DirectionLineCtl : Control
    {
        #region 字段

        static int _length = 14;
        static int _offset = 2;
        static int _pensize = 1;
        //static Color _fillCorlor = new Color();
        bool _isRow;

        #endregion

        #region 属性

        /// <summary>
        /// 长度
        /// </summary>
        public static int Length
        {
            get { return DirectionLineCtl._length; }
            set { DirectionLineCtl._length = value; }
        }

        /// <summary>
        /// 中间的空偏移
        /// </summary>
        public static int Offset
        {
            get { return DirectionLineCtl._offset; }
            set { DirectionLineCtl._offset = value; }
        }

        /// <summary>
        /// 画笔宽
        /// </summary>
        public static int PenSize
        {
            get { return DirectionLineCtl._pensize; }
            set { DirectionLineCtl._pensize = value; }
        }
        /// <summary>
        /// 是否为横向
        /// </summary>
        public bool IsRow
        {
            get { return _isRow; }
            set 
            {
                if (_isRow != value)
                {
                    _isRow = value;

                    if (_isRow)
                    {
                        this.Size = new Size(DirectionLineCtl.Length,
                            2 * DirectionLineCtl.PenSize + DirectionLineCtl.Offset);
                    }
                    else
                    {
                        this.Size = new Size(2 * DirectionLineCtl.PenSize + DirectionLineCtl.Offset,
                            DirectionLineCtl.Length);
                    }
                    this.Invalidate();
                }
            }
        }

        #endregion

        public DirectionLineCtl(bool isRow)
        {
            if (isRow)
            {
                this.Size = new Size(DirectionLineCtl.Length,
                    2*DirectionLineCtl.PenSize + DirectionLineCtl.Offset);
            }
            else
            {
                this.Size = new Size(2 * DirectionLineCtl.PenSize + DirectionLineCtl.Offset,
                    DirectionLineCtl.Length);
            }
            this.IsRow = isRow;
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码

            // 调用基类 OnPaint
            Graphics g = pe.Graphics;

            if (IsRow)
            {
                g.DrawLine(Pens.Black, 0, 0, DirectionLineCtl.Length, 0);
                g.DrawLine(Pens.Black, 0, 
                    PenSize + Offset, 
                    DirectionLineCtl.Length, 
                    PenSize + Offset);
            }
            else
            {
                g.DrawLine(Pens.Black, 0, 0, 0, DirectionLineCtl.Length);
                g.DrawLine(Pens.Black, 
                    PenSize + Offset,
                    0,
                    PenSize + Offset,
                    DirectionLineCtl.Length);
            }
            g.Dispose();
            base.OnPaint(pe);
        }
    }
}
