using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    internal class SingerBoxItemControl : Control
    {

        #region 控件的变量声明
        private PictureBox _pictureBox; 
        private Label _label; 
        private BorderWidthControl _borderWidthControl;
        private BorderStyleControl _styleComboBox;
        private ColorGeneralButton _myColorButton;
        const int offset = 3;
        #endregion

        #region 属性

        private Image _image;

        public Image Image
        {
            get { return _image; }
            set 
            {
                this._pictureBox.Image = value;
                _image = value; 
            }
        }


        private bool _isBorder = false;
        /// <summary>
        /// 是否是边框设置
        /// </summary>
        public bool IsBorder
        {
            get { return _isBorder; }
            set { _isBorder = value; }
        }

        private string _picPath;
        /// <summary>
        /// 图片的路径
        /// </summary>
        public string PicPath
        {
            get { return _picPath; }
            set { _picPath = value; }
        }

        private string _labelName = "边框位置";
        /// <summary>
        /// 标签的名称
        /// </summary>
        public string LabelName
        {
            get { return _labelName; }
            set 
            {
                this._label.Text = value;
                _labelName = value; 
            }
        }

        private string _css = "";

        public string Css
        {
            get { return _css; }
            set { _css = value; }
        }


        #endregion

        /// <summary>
        /// 自定义控件 一行
        /// </summary>
        /// <param name="isBorder">是否“边框”</param>
        public SingerBoxItemControl(bool isBorder)
        {
            _isBorder = isBorder;
            InitControl();
        }

        private void InitControl()
        {
            this._myColorButton = new ColorGeneralButton();
            this._pictureBox = new PictureBox();
            this._borderWidthControl = new BorderWidthControl();
            this._styleComboBox = new BorderStyleControl();
            this._label = new Label();
            
            ///图片
            this._pictureBox.Location = new Point(0, offset);
            this._pictureBox.Size           = new Size(20, 21);
            this._pictureBox.Image          = Image;
            this._pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            this._pictureBox.BackColor = SystemColors.Info;

            ///标签
            this._label.AutoSize = true;
            this._label.Location = new Point(25, 3 + offset);
            this._label.Size                = new Size(50, 18);
            this._label.Text = LabelName;
            
            ///宽度
            this._borderWidthControl.Location = new Point(80, offset);
            this._borderWidthControl.Size       = new Size(170,21);
            this._borderWidthControl.CssChanged += new EventHandler(_borderWidthControl_CssChanged);

            ///样式
            this._styleComboBox.Location = new Point(260, offset);
            this._styleComboBox.Size        = new Size(80, 20);
            this._styleComboBox.SelectedIndexChanged += new EventHandler(_styleComboBox_SelectedIndexChanged);
            
            
            ///颜色
            this._myColorButton.Location = new Point(345, offset);
            this._myColorButton.Size        = new Size(21, 21);
            this._myColorButton.MyColor = Color.Empty;
            this._myColorButton.UseVisualStyleBackColor = true;
            this._myColorButton.MyColorChanged += new EventHandler(_myColorButton_MyColorChanged);

            this.Controls.Add(this._label); 
            this.Controls.Add(this._pictureBox);
            this.Controls.Add(this._borderWidthControl);

            if (_isBorder)
            {
                this.Controls.Add(this._styleComboBox);
                this.Controls.Add(this._myColorButton);
            }

            //if (_onlyRead)
            //{

            //}

            this.Name = "SingerBoxItemControl";
            this.Dock = DockStyle.Top;
        }

        

        #region 事件的实现

        void _styleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToCss();
            OnCssChanged(EventArgs.Empty);
        }

        void _borderWidthControl_CssChanged(object sender, EventArgs e)
        {
            ToCss();
            OnCssChanged(EventArgs.Empty);
        }

        void _myColorButton_MyColorChanged(object sender, EventArgs e)
        {
            ToCss();
            OnCssChanged(EventArgs.Empty);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.Size = new Size(370, 21 + offset);
        }

        #endregion

        private void ToCss()
        {
            _css =
                _borderWidthControl.ToCss() +" " +
                _styleComboBox.GetBorderStyle +" "+ 
                _myColorButton.ColorArgbString;
        }

        #region 自定义事件

        public event EventHandler CssChanged;
        protected virtual void OnCssChanged(EventArgs e)
        {
            if (CssChanged != null)
            {
                CssChanged(this, e);
            }
        }

        #endregion

        internal void InitValue(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return;
            }
            string[] ss = str.Split(new char[] { ' ' });
            if (ss.Length>0)
            {
                _borderWidthControl.InitValue(ss[0]);
                if (ss.Length > 1)
                {
                    _styleComboBox.InitValue(ss[1]);
                    if (ss.Length > 2)
                    {
                        try
                        {
                            _myColorButton.MyColor = ColorTranslator.FromHtml(ss[2]);
                        }
                        catch (Exception)
                        {
                            return;
                        }
                    }
                }
            }
        }
    }
}
