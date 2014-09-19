using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Jeelu.Win;


namespace Jeelu.SimplusD.Client.Win
{
    public partial class NewTmpltSetupForm : BaseForm
    {
        #region 内部变量

        Image _backImage;// = Image.FromFile(Path.Combine(PathService.SoftwarePath, "Sina.com.png"));//背景图片
        Image image;
        Image image2; // 蒙上一层阴影
        int _defaultWidth = 0; //默认图片宽
        int _defaultHeight = 0;//默认图片高
        float _imageScale = 1;

        Pen _pen;
        Brush _cutedBrush = new SolidBrush(Color.FromArgb(70, Color.Black));  //覆盖的颜色

        bool _isCuting = false; // 是否正在切割
        bool _isCuted = false; // 是否切割完毕

        Cursor selelectRectCursor = Cursors.Cross; // 切割时的鼠标光标
        Cursor moveRectCursor = Cursors.Hand; //移动选择框时的鼠标光标

        int _tempWidth = 0;
        int _tempHeight = 0;
        int _tempX = 0;
        int _tempY = 0;

        Rectangle _curRect = new Rectangle(0, 0, 0, 0); //切割后得到的矩形

        Point _firstPoint = new Point(); //开始切割的起点

        FolderXmlElement _parentFolder;

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置模板的类型
        /// </summary>
        public TmpltType TmpltType { get; set; }

        /// <summary>
        /// 获取或设置画线的粗细
        /// </summary>
        public int PenSize { get; set; }

        /// <summary>
        /// 获取或设置画线的颜色
        /// </summary>
        public Color PenColor { get; set; }

        /// <summary>
        /// 获取或设置模板Title
        /// </summary>
        public string TmpltTitle
        {
            get { return textBoxTitle.Text; }
            set { textBoxTitle.Text = value; }
        }

        /// <summary>
        /// 获取或设置模板宽度
        /// </summary>
        public int TmpltWidth
        {
            get { return Convert.ToInt32(textBoxWidth.Text); }
        }

        /// <summary>
        /// 获取或设置模板高度
        /// </summary>
        public int TmpltHeight
        {
            get { return Convert.ToInt32(textBoxHeight.Text); }
        }

        /// <summary>
        /// 获取或设置模板背景图片
        /// </summary>
        public Image BackImage { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 为缩放提供参数
        /// </summary>
        /// <returns>一直为false</returns>
        public bool ThumbnailCallback()
        {
            return false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public NewTmpltSetupForm(FolderXmlElement parentFolder, TmpltType type)
        {
            this._parentFolder = parentFolder;
            InitializeComponent();
            PenSize = 1;
            PenColor = Color.Black;
            TmpltType = type;
            _imageScale = (float)panelGraghics.Width / (float)panelGraghics.Height;

            if (_backImage != null)
            {
                textBoxWidth.Text = _backImage.Width.ToString();
                textBoxHeight.Text = _backImage.Height.ToString();                
            }

            _defaultHeight = panelGraghics.Height;
            _defaultWidth = panelGraghics.Width;
            _pen = new Pen(PenColor, PenSize);
            _pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            textBoxTitle.Text = XmlUtilService.CreateIncreaseTmpltTitle(_parentFolder, type);
            string _newTmpltIsUseImg = Service.DesignData.GetValue("NewTmpltIsUseImg");
            if (_newTmpltIsUseImg == null)
            {
                checkBoxChooseImage.Checked = true;
                Service.DesignData.SetValue("NewTmpltIsUseImg", true.ToString());
            }
            else
            {
                checkBoxChooseImage.Checked = Utility.Convert.StringToBool(_newTmpltIsUseImg);
            }

            InitEvent();
            Resetimage();
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        private void InitEvent()
        {
            panelGraghics.MouseDown += new MouseEventHandler(panelGraghics_MouseDown);
            panelGraghics.MouseMove += new MouseEventHandler(panelGraghics_MouseMove);
            panelGraghics.MouseUp += new MouseEventHandler(panelGraghics_MouseUp);
            openNemImage.FileSelected += new EventHandler(openNemImage_FileSelected);
            textBoxTitle.TextChanged += new EventHandler(textBoxTitle_TextChanged);
            textBoxHeight.TextChanged += new EventHandler(textBoxHeight_TextChanged);
            textBoxWidth.TextChanged += new EventHandler(textBoxWidth_TextChanged);
        }

        void textBoxWidth_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxWidth.Text))
            {
                MessageService.Show("宽度不能为空！（范围：宽*高=(1-3200)*(1-6400)）");
                textBoxWidth.Text = "1";
            }
            int w = Convert.ToInt32(textBoxWidth.Text);
            if (w>3200)
            {
                MessageService.Show("宽度过大！（范围：宽*高=(1-3200)*(1-6400)）");
                textBoxWidth.Text = "3200";
            }
        }

        void textBoxHeight_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxHeight.Text))
            {
                MessageService.Show("高度不能为空！（范围：宽*高=(1-3200)*(1-6400)）");
                textBoxHeight.Text = "1";
            }
            int h = Convert.ToInt32(textBoxHeight.Text);
            if (h > 6400)
            {
                MessageService.Show("高度过大！（范围：宽*高=(1-3200)*(1-6400)）");
                textBoxHeight.Text = "6400";
            }
        }

        void textBoxTitle_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxTitle.Text))
            {
                btnSaveAndOpen.Enabled = false;
            }
            else
            {
                btnSaveAndOpen.Enabled = true;
            }
        }

        /// <summary>
        /// 打开本窗体时......
        /// </summary>
        private void NewTmpltSetupForm_Load(object sender, EventArgs e)
        {
            _curRect = new Rectangle(_tempX, _tempY, _tempWidth, _tempHeight);
            textBoxHeight.Text = "600";
            textBoxWidth.Text = "800";
        }

        #endregion

        #region 事件

        #region 鼠标事件

        void graphicsControl_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUp_SelectRect(e);
        }

        void graphicsControl_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMove_SelectRect(e);
        }

        void graphicsControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseLeftDown_SelectRect(e);
            }
        }

        void panelGraghics_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseLeftDown_SelectRect(e);
            }
        }

        void panelGraghics_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMove_SelectRect(e);
        }

        void panelGraghics_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUp_SelectRect(e);
        }

        private void MouseLeftDown_SelectRect(MouseEventArgs e)
        {
            _isCuting = true;
            _isCuted = false;
            _firstPoint = new Point(e.X, e.Y);
        }

        private void MouseMove_SelectRect(MouseEventArgs e)
        {
            if (!_isCuting || _isCuted)
                return;

            _isCuted = false;
            _curRect = Utility.Draw.PointToRectangle(_firstPoint, new Point(e.X, e.Y));

            //panelGraghics.Invalidate();
            //panelGraghics.Update();
            //graphicsControl.Invalidate();
            //graphicsControl.Update();

            Graphics g = panelGraghics.CreateGraphics();
            DrawImage(g);
            g.DrawRectangle(_pen, _curRect);
            g.Dispose();
        }

        private void MouseUp_SelectRect(MouseEventArgs e)
        {
            if (_isCuting)
            {
                _isCuting = false;
                if (!_isCuted)
                {
                    if (!ResetCurRect())
                        return;
                    //Graphics g = graphicsControl.CreateGraphics();
                    //g.FillRectangle(_cutedBrush, new Rectangle(_tempX, _tempY, _tempWidth, _tempHeight));
                    //g.DrawRectangle(_pen, _curRect);
                    //g.Dispose();
                    //graphicsControl.Invalidate(
                    //new Rectangle(_curRect.X + PenSize, _curRect.Y + PenSize,
                    //_curRect.Width - PenSize, _curRect.Height - PenSize));
                    //graphicsControl.Update();
                    _isCuted = true;
                    panelGraghics.Invalidate();
                    panelGraghics.Update();

                }
                else
                {
                    textBoxHeight.Text = _backImage.Height.ToString();
                    textBoxWidth.Text = _backImage.Width.ToString();
                }
            }

        }

        #endregion

        #region 各控件事件

        /// <summary>
        /// 打开新图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openNemImage_FileSelected(object sender, EventArgs e)
        {
            FileStream stm = new FileStream(openNemImage.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            Image imgTemp= Image.FromStream(stm);
            if (imgTemp.Width > 3200 || imgTemp.Height > 6400)
            {
                MessageService.Show("导入图片的宽或高过大！（范围：宽*高=(1-3200)*(1-6400)）");
                openNemImage.Text = "";
                return;
            }
            _backImage = imgTemp; 
            stm.Close();
            Resetimage();
            _curRect = new Rectangle(_tempX, _tempY, _backImage.Width, _backImage.Height);
            textBoxWidth.Text = _backImage.Width.ToString();
            textBoxHeight.Text = _backImage.Height.ToString();
            panelGraghics.Invalidate();
        }

        /// <summary>
        /// 展示区的paint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelGraghics_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawImage(g);
            //if(_isCuting)
            //    DrawCuteRect(g);
        }

        private void DrawCuteRectImg(Graphics g, Image img)
        {
            Bitmap bmp = new Bitmap(_curRect.Width, _curRect.Height);
            Graphics gbmp = Graphics.FromImage(bmp);
            gbmp.DrawImage(img, 0, 0, new Rectangle(_curRect.X - _tempX, _curRect.Y - _tempY, _curRect.Width, _curRect.Height), GraphicsUnit.Pixel);
            g.DrawImage(bmp, _curRect);
            //gbmp.Dispose();
        }

        private void DrawCuteRect(Graphics g)
        {
            g.DrawRectangle(_pen, _curRect);
        }
        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            ///检查是否有重名
            //if (File.Exists(Path.Combine(_parentFolder.AbsoluteFilePath, textBoxTitle.Text + Utility.Const.TmpltFileExt)))
            //{
            //    MessageService.Show("此文件已存在，请重新命名。");
            //    textBoxTitle.SelectAll();
            //    textBoxTitle.Focus();
            //    return;
            //}

            //this.TmpltTitle = textBoxTitle.Text;
            //this.TmpltWidth = int.Parse(textBoxTitle.Text);
            //this.TmpltHeight = int.Parse(textBoxHeight.Text);

            SaveTemplt();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 选择“使用背景图片”时
        /// </summary>
        private void checkBoxChooseImage_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxChooseImage.Checked == true)
            {
                //textBoxWidth.Text = _backImage.Width.ToString();
                //textBoxHeight.Text = _backImage.Height.ToString();
                IsUsedImage(true);
            }
            else if (checkBoxChooseImage.Checked == false)
            {
                textBoxHeight.Text = "600";
                textBoxWidth.Text = "800";
                IsUsedImage(false);
            }
            Service.DesignData.SetValue("NewTmpltIsUseImg", checkBoxChooseImage.Checked.ToString());
        }

        #endregion

        #endregion

        #region 内部方法
        /// <summary>
        /// 导入模板底面图片
        /// </summary>
        /// <param name="g"></param>
        private void DrawImage(Graphics g)
        {
            if (image == null)
            {
                return;
            }
            if (!_isCuting && _isCuted)
            {
                Bitmap bmp = new Bitmap(image);
                Graphics gbmp = Graphics.FromImage(bmp);
                gbmp.FillRectangle(_cutedBrush, 0, 0, image.Width, image.Height);
                gbmp.DrawImage(image, new Rectangle(_curRect.X - _tempX, _curRect.Y - _tempY, _curRect.Width, _curRect.Height), new Rectangle(_curRect.X - _tempX, _curRect.Y - _tempY, _curRect.Width, _curRect.Height), GraphicsUnit.Pixel);
                g.DrawImage(bmp, _tempX, _tempY, _tempWidth, _tempHeight);
                //DrawCuteRectImg(g,image);
                gbmp.Dispose();
            }
            if (!_isCuted)
                g.DrawImage(image, _tempX, _tempY, _tempWidth, _tempHeight);
            if (_isCuting || (!_isCuting && _isCuted))
            {
                DrawCuteRect(g);
            }
        }

        /// <summary>
        /// 保存模板
        /// </summary>
        private void SaveTemplt()
        {
            int imgW = 800;
            int imgH = 600;
            Bitmap bmp = new Bitmap(Convert.ToInt32(textBoxWidth.Text), Convert.ToInt32(textBoxHeight.Text));
            Graphics g = Graphics.FromImage(bmp);
            // 保存切割后的图片
            if (checkBoxChooseImage.Checked == true)
            {
                if (_backImage != null)
                {
                    imgW = _backImage.Width;
                    imgH = _backImage.Height;

                    int x = (_curRect.X - _tempX) * imgW / _tempWidth;
                    int y = (_curRect.Y - _tempY) * imgH / _tempHeight;
                    g.DrawImage(_backImage,
                        new Rectangle(0, 0, bmp.Width, bmp.Height),
                        x,
                        y,
                        bmp.Width,
                        bmp.Height,
                        GraphicsUnit.Pixel);
                    BackImage = bmp;
                }

            }
            else if (checkBoxChooseImage.Checked == false)
            {
                BackImage = null;
            }
            g.Flush();
            g.Dispose();
        }

        /// <summary>
        /// 当使用（不使用）底面图片时，窗口的状态
        /// </summary>
        /// <param name="p">为true时使用底面图片，为false时不使用</param>
        private void IsUsedImage(bool p)
        {
            openNemImage.Enabled = p;
            panelImage.Enabled = p;
            textBoxHeight.Enabled = !p;
            textBoxWidth.Enabled = !p;
        }

        /// <summary>
        /// 刷新当前得到的结果矩形
        /// </summary>
        /// <returns></returns>
        private bool ResetCurRect()
        {
            int x = _tempX;
            int y = _tempY;
            int w = _tempWidth;
            int h = _tempHeight;
            if (_curRect.X >= _tempX + _tempWidth || _curRect.X + _curRect.Width <= _tempX
                || _curRect.Y >= _tempY + _tempHeight || _curRect.Y + _curRect.Height <= _tempY)
                return false;
            if (_curRect.X < _tempX)
            {
                x = _tempX;
                if (_curRect.Y < _tempY)
                {
                    y = _tempY;
                    if (_curRect.X + _curRect.Width >= _tempX + _tempWidth)
                    {
                        w = _tempWidth;
                        if (_curRect.Y + _curRect.Height >= _tempY + _tempHeight)
                            h = _tempHeight;
                        else
                            h = _curRect.Y + _curRect.Height - _tempY;
                    }
                    else
                    {
                        w = _curRect.Width + _curRect.X - _tempX;
                        if (_curRect.Y + _curRect.Height >= _tempY + _tempHeight)
                            h = _tempHeight;
                        else
                            h = _curRect.Y + _curRect.Height - _tempY;
                    }
                }
                else
                {
                    y = _curRect.Y;
                    if (_curRect.X + _curRect.Width >= _tempX + _tempWidth)
                    {
                        w = _tempWidth;
                        if (_curRect.Y + _curRect.Height >= _tempY + _tempHeight)
                            h = _tempHeight + _tempY - _curRect.Y;
                        else
                            h = _curRect.Height;
                    }
                    else
                    {
                        w = _curRect.Width + _curRect.X - _tempX; ;
                        if (_curRect.Y + _curRect.Height >= _tempY + _tempHeight)
                            h = _tempHeight + _tempY - _curRect.Y;
                        else
                            h = _curRect.Height;
                    }
                }
            }
            else
            {
                x = _curRect.X;
                if (_curRect.Y < _tempY)
                {
                    y = _tempY;
                    if (_curRect.X + _curRect.Width >= _tempX + _tempWidth)
                    {
                        w = _tempX + _tempWidth - _curRect.X;
                        if (_curRect.Y + _curRect.Height >= _tempY + _tempHeight)
                            h = _tempHeight;
                        else
                            h = _curRect.Y + _curRect.Height - _tempY;
                    }
                    else
                    {
                        w = _curRect.Width;
                        if (_curRect.Y + _curRect.Height >= _tempY + _tempHeight)
                            h = _tempHeight;
                        else
                            h = _curRect.Y + _curRect.Height - _tempY;
                    }
                }
                else
                {
                    y = _curRect.Y;
                    if (_curRect.X + _curRect.Width >= _tempX + _tempWidth)
                    {
                        w = _tempX + _tempWidth - _curRect.X;
                        if (_curRect.Y + _curRect.Height >= _tempY + _tempHeight)
                            h = _tempY + _tempHeight - _curRect.Y;
                        else
                            h = _curRect.Height;
                    }
                    else
                    {
                        w = _curRect.Width;
                        if (_curRect.Y + _curRect.Height >= _tempY + _tempHeight)
                            h = _tempY + _tempHeight - _curRect.Y;
                        else
                            h = _curRect.Height;
                    }
                }
            }
            _curRect = new Rectangle(x, y, w, h);
            w = (int)((w * _backImage.Width) / _tempWidth);
            h = (int)((h * _backImage.Height) / _tempHeight);
            textBoxHeight.Text = h.ToString();
            textBoxWidth.Text = w.ToString();
            return true;
        }

        /// <summary>
        /// 刷新图片
        /// </summary>
        private void Resetimage()
        {
            image = _backImage;
            if (image == null)
            {
                return;
            }
            if (((float)_backImage.Width / (float)_backImage.Height) > _imageScale)
            {
                if (_backImage.Width == panelGraghics.Width)
                {
                    _tempWidth = _backImage.Width;
                    _tempHeight = _backImage.Height;
                    _tempX = 0;
                    _tempY = (panelGraghics.Height - _backImage.Height) / 2;
                }
                if (_backImage.Width < panelGraghics.Width)
                {
                    _tempWidth = _backImage.Width;
                    _tempHeight = _backImage.Height;
                    _tempX = (panelGraghics.Width - _backImage.Width) / 2;
                    _tempY = (panelGraghics.Height - _backImage.Height) / 2;
                }
                if (_backImage.Width > panelGraghics.Width)
                {
                    Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                    Bitmap bmp = new Bitmap(_backImage);
                    int height = ((int)(_defaultWidth * _backImage.Height)) / _backImage.Width;
                    image = bmp.GetThumbnailImage(_defaultWidth,
                        height, myCallback, IntPtr.Zero);

                    _tempWidth = image.Width;
                    _tempHeight = image.Height;
                    _tempX = (panelGraghics.Width - image.Width) / 2;
                    _tempY = (panelGraghics.Height - image.Height) / 2;
                }
            }
            else
            {
                if (_backImage.Height == panelGraghics.Height)
                {
                    _tempWidth = _backImage.Width;
                    _tempHeight = _backImage.Height;
                    _tempX = (panelGraghics.Width - _backImage.Width) / 2;
                    _tempY = 0;
                }
                if (_backImage.Height < panelGraghics.Height)
                {
                    _tempWidth = _backImage.Width;
                    _tempHeight = _backImage.Height;
                    _tempX = (panelGraghics.Width - _backImage.Width) / 2;
                    _tempY = (panelGraghics.Height - _backImage.Height) / 2;
                }
                if (_backImage.Height > panelGraghics.Height)
                {
                    Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                    Bitmap bmp = new Bitmap(_backImage);
                    int width = ((int)(_defaultHeight * _backImage.Width)) / _backImage.Height;
                    image = bmp.GetThumbnailImage(width, _defaultHeight, myCallback, IntPtr.Zero);

                    _tempWidth = image.Width;
                    _tempHeight = image.Height;
                    _tempX = (panelGraghics.Width - image.Width) / 2;
                    _tempY = (panelGraghics.Height - image.Height) / 2;
                }
            }
            _curRect = new Rectangle(_tempX, _tempY, _tempWidth, _tempHeight);
        }

        #endregion
    }
}