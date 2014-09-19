using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Jeelu.Win
{
    internal partial class BottomColorPad : Control
    {
        private ColorSelector _ownerColorSelector;
        public ColorSelector OwnerColorSelector
        {
            get { return _ownerColorSelector; }
            set
            {
                _ownerColorSelector = value;

                if (value != null)
                {
                    OwnerColorSelector.ValueChanged += new EventHandler(OwnerColorSelector_ValueChanged);
                    OwnerColorSelector.TempValueChanged += new EventHandler(OwnerColorSelector_TempValueChanged);
                }
            }
        }

        void OwnerColorSelector_TempValueChanged(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            DrawNewColor(g);
            g.Dispose();
        }

        void OwnerColorSelector_ValueChanged(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            DrawOldColor(g);
            DrawNewColor(g);
            g.Dispose();
        }

        public BottomColorPad()
        {
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            DrawOldColor(pe.Graphics);
            DrawNewColor(pe.Graphics);

            base.OnPaint(pe);
        }

        private void DrawOldColor(Graphics g)
        {
            g.FillRectangle(new SolidBrush(OwnerColorSelector.Value), 0, this.Height / 2f, this.Width, this.Height / 2);
        }

        private void DrawNewColor(Graphics g)
        {
            g.FillRectangle(new SolidBrush(OwnerColorSelector.TempValue), 0, 0, this.Width, this.Height / 2f);
        }
    }
}
