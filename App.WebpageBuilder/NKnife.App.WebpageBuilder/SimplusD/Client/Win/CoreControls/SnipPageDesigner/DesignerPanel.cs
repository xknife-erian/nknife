using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public class DesignerPanel : UserControl
    {
        #region ����

        /// <summary>
        /// ��ȡ�������ҳ��Ƭ�����
        /// </summary>
        public SnipPageDesigner Designer { get; private set; }
        
        /// <summary>
        /// ��ȡ��ֱ������
        /// </summary>
        public new VScrollBar VScroll { get; private set; }
        
        /// <summary>
        /// ��ȡˮƽ������
        /// </summary>
        public new HScrollBar HScroll { get; private set; }

        #endregion

        #region ���캯��

        public DesignerPanel(bool hasAutoKeyWordsSequenceType )
        {
            this.SuspendLayout();

            ///��ʼ������
            Designer = new SnipPageDesigner(hasAutoKeyWordsSequenceType);

            ///���������ù��������������
            VScroll = new VScrollBar();
            HScroll = new HScrollBar();
            VScroll.Dock = DockStyle.Right;
            HScroll.Dock = DockStyle.Bottom;
            VScroll.Scroll += new ScrollEventHandler(VScroll_Scroll);
            HScroll.Scroll += new ScrollEventHandler(HScroll_Scroll);
            HScroll.ValueChanged += delegate
            {
                Designer.Location = new Point(HScroll.Value * -1, Designer.Location.Y);
            };
            VScroll.ValueChanged += delegate
            {
                Designer.Location = new Point(Designer.Location.X, VScroll.Value * -1);
            };

            ///�ؼ���С�ı䣬�����¼���Scroll�Ĵ�Сλ��
            EventHandler resetScrollHandler = new EventHandler(
                delegate
                {
                    ResetScroll();
                }
            );
            HScroll.SizeChanged += resetScrollHandler;
            VScroll.SizeChanged += resetScrollHandler;
            Designer.SizeChanged += resetScrollHandler;
            this.Controls.Add(VScroll);
            this.Controls.Add(HScroll);
            this.Controls.Add(Designer);

            //Designer.Resize += new EventHandler(Designer_Resize);
            //Designer.SizeChanged += new EventHandler(Designer_SizeChanged);

            this.ResumeLayout();
        }

        protected override void OnLoad(EventArgs e)
        {
            ResetDesignerLocation();

            base.OnLoad(e);
        }
                
        #endregion

        #region �¼���Ӧ
        
        void VScroll_Scroll(object sender, ScrollEventArgs e)
        {
            Designer.Location = new Point(Designer.Location.X, e.NewValue * -1);
        }

        void HScroll_Scroll(object sender, ScrollEventArgs e)
        {
            Designer.Location = new Point(e.NewValue * -1, Designer.Location.Y);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (VScroll.Enabled)
                {
                    int result = VScroll.Value - VScroll.LargeChange / 3;
                    VScroll.Value = Math.Max(result, VScroll.Minimum);
                    Designer.Location = new Point(Designer.Location.X, VScroll.Value * -1);
                }
            }
            else
            {
                if (VScroll.Enabled)
                {
                    int result = VScroll.Value + VScroll.LargeChange / 3;
                    VScroll.Value = Math.Min(result, VScroll.Maximum - VScroll.LargeChange + 1);
                    Designer.Location = new Point(Designer.Location.X, VScroll.Value * -1);
                }
            }

            base.OnMouseWheel(e);
        }

        protected override void OnCreateControl()
        {
            ResetScroll();

            base.OnCreateControl();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Designer.Focus();
            Designer.Select();
            this.Designer.SelectedParts.Clear();
            base.OnMouseDown(e);
        }
        
        #endregion

        #region �ڲ�����

        /// <summary>
        /// ����Scroll
        /// </summary>
        void ResetScroll()
        {
            if (VScroll.Height > 1)
            {
                VScroll.LargeChange = VScroll.Height;
                VScroll.SmallChange = VScroll.Height / 10;

                VScroll.Maximum = Designer.Height;
                VScroll.Enabled = (VScroll.Maximum > VScroll.LargeChange);
                if (!VScroll.Enabled)
                {
                    VScroll.Value = VScroll.Minimum;
                }
                else if (VScroll.Value > (VScroll.Maximum-VScroll.LargeChange-1))
                {
                    VScroll.Value = VScroll.Maximum - VScroll.LargeChange - 1;
                }
                else if (VScroll.Value < VScroll.Minimum)
                {
                    VScroll.Value = VScroll.Minimum;
                }
            }
            if (HScroll.Height > 1)
            {
                HScroll.LargeChange = HScroll.Width;
                HScroll.SmallChange = HScroll.Width / 10;

                HScroll.Maximum = Designer.Width+VScroll.Width;
                HScroll.Enabled = (HScroll.Maximum > HScroll.LargeChange);
                if (!HScroll.Enabled)
                {
                    HScroll.Value = HScroll.Minimum;
                }
                else if (HScroll.Value > (HScroll.Maximum - HScroll.LargeChange - 1))
                {
                    HScroll.Value = HScroll.Maximum - HScroll.LargeChange - 1;
                }
                else if (HScroll.Value < HScroll.Minimum)
                {
                    HScroll.Value = HScroll.Minimum;
                }
            }
            Designer.LayoutParts();
        }

        /// <summary>
        /// ���¼���Designer��λ��
        /// </summary>
        private void ResetDesignerLocation()
        {
            int x = 0, y = 0;

            if (Designer.Width < this.Width)
                x = (this.Width - Designer.Width) / 2;

            if (Designer.Height < this.Height)
                y = (this.Height - Designer.Height) / 2;

            Designer.Location = new Point(x, y);
        }

        #endregion
    }
}
