using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    public partial class ColorSelector : UserControl
    {
        public ColorSelector()
        {
            InitializeComponent();

            BottomColorPad.OwnerColorSelector = this;
            ColorPad.OwnerColorSelector = this;
            ColorSlideBar.OwnerColorSelector = this;

            this.BorderStyle = BorderStyle.FixedSingle;

            this.txtA.LostFocus += new EventHandler(argb_ValueChanged);
            this.txtR.LostFocus += new EventHandler(argb_ValueChanged);
            this.txtG.LostFocus += new EventHandler(argb_ValueChanged);
            this.txtB.LostFocus += new EventHandler(argb_ValueChanged);
            this.txtHtmlValue.LostFocus+= new EventHandler(txtHtmlValue_LostFocus);
            this.txtHtmlValue.KeyDown += new KeyEventHandler(txtHtmlValue_KeyDown);
        }

        void txtHtmlValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtHtmlValue_LostFocus(sender, EventArgs.Empty);
            }
        }

        void txtHtmlValue_LostFocus(object sender, EventArgs e)
        {
            Color clr = Color.Empty;
            try
            {
                clr = ColorTranslator.FromHtml(txtHtmlValue.Text);
            }
            catch
            {
                return;
            }
            if (this.txtR.Value != clr.R)
            {
                this.txtR.Value = clr.R;
            }
            if (this.txtG.Value != clr.G)
            {
                this.txtG.Value = clr.G;
            }
            if (this.txtB.Value != clr.B)
            {
                this.txtB.Value = clr.B;
            }
        }

        void argb_ValueChanged(object sender, EventArgs e)
        {
            Value = Value;
        }

        void txtHtmlValue_TextChanged(object sender, EventArgs e)
        {
        }

        internal BottomColorPad BottomColorPad
        {
            get { return bottomColorPad1; }
        }

        internal ColorPad ColorPad
        {
            get { return colorPad1; }
        }

        internal ColorSlideBar ColorSlideBar
        {
            get { return colorSlideBar1; }
        }

        public Color Value
        {
            get
            {
                return Color.FromArgb((int)((int)txtA.Value*2.55), (int)txtR.Value, (int)txtG.Value, (int)txtB.Value);
            }
            set
            {
                TempValue = value;

                OnValueChanged(EventArgs.Empty);
            }
        }

        private Color _tempValue;
        internal Color TempValue
        {
            get { return _tempValue; }
            set
            {
                _tempValue = value;

                if (txtA != null && !txtA.IsDisposed)
                {
                    txtA.Value = (int)(value.A/2.55f);
                    txtR.Text = value.R.ToString();
                    txtG.Text = value.G.ToString();
                    txtB.Text = value.B.ToString();
                    txtHtmlValue.Text = ColorTranslator.ToHtml(value);
                }

                OnTempValueChanged(EventArgs.Empty);
            }
        }

        private Color _baseValue;
        internal Color BaseValue
        {
            get { return _baseValue; }
            set
            {
                _baseValue = value;

                OnBaseValueChanged(EventArgs.Empty);
            }
        }

        public event EventHandler BaseValueChanged;
        protected virtual void OnBaseValueChanged(EventArgs e)
        {
            if (BaseValueChanged != null)
            {
                BaseValueChanged(this, e);
            }
        }

        public event EventHandler ValueChanged;
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }

        public event EventHandler TempValueChanged;
        protected virtual void OnTempValueChanged(EventArgs e)
        {
            if (TempValueChanged != null)
            {
                TempValueChanged(this, e);
            }
        }
    }
}
