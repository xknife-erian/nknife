using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.App.PictureTextPicker.Common.Base;
using NKnife.App.PictureTextPicker.Common.Base.Res;
using NKnife.App.PictureTextPicker.Common.Controls;
using NKnife.App.PictureTextPicker.Common.Entities;
using NKnife.Draws.Controls.Base;
using NKnife.Draws.Controls.Frames;
using NKnife.Draws.Controls.Frames.Base;
using NKnife.Draws.Controls.Frames.Event;
using NKnife.Events;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker.Views
{
    public class PictureDocumentView : DockContent
    {
        private readonly IPictureList _PictureList = DI.Get<IPictureList>();
        private ToolStripContainer _PicutureDocumentToolStripContainer;
        private PictureFrame _PictureFrame;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel _ModeLabel;
        private ToolStripStatusLabel _ZoomLabel;
        private bool _ImageLoaded;
        public PictureFrameDocument Document { get; private set; }
        private readonly FrameContextMenu _FrameMenu;

        #region 工具栏
        private ToolStrip _ArrowToolStrip;
        private ToolStripButton _ArrowInButton;
        private ToolStripButton _ArrowOutButton;
        private ToolStripSeparator _ToolStripSeparator;
        private ToolStripButton _ArrowLeftButton;
        private ToolStripButton _ArrowRightButton;
        private ToolStripButton _ArrowDownButton;
        private ToolStripButton _ArrowUpButton;
        private ToolStrip _EditToolStrip;
        private ToolStripButton _CutButton;
        private ToolStripButton _CopyButton;
        private ToolStripButton _PasteButton;
        private ToolStripButton _DeleteButton;
        private ToolStrip _AlignToolStrip;
        private ToolStripButton _AlignLeftButton;
        private ToolStripButton _AlignRightButton;
        private ToolStripButton _AlignBottomButton;
        private ToolStripButton _AlignTopButton;
        private ToolStripButton _AlignCenterButton;
        private ToolStripButton _AlignMiddleButton;
        private ToolStripButton _AlignSameButton;
        private ToolStripButton _AlignHeightButton;
        private ToolStripButton _AlignWidthButton;
        private ToolStrip _BaseToolStrip;
        private ToolStripButton _SelectingButton;
        private ToolStripButton _DesignButton;
        private ToolStripButton _DraggingButton;
        private ToolStripButton _ZoomButton;
        private ToolStripStatusLabel _RealRectangleLabel;
        private ToolStripStatusLabel _CurrentRectangleLabel;
        private ToolStripStatusLabel _DraggingInfoLabel;
        #endregion



        #region 构造函数
        private PictureDocumentView(){ }

        /// <summary>
        /// 构造函数，每个View一定有一个与对应PictureFrame对应的document
        /// </summary>
        /// <param name="document"></param>
        public PictureDocumentView(PictureFrameDocument document)
        {
            Document = document;
            InitializeComponent();

            SetToolStripImage(); // 设置工具栏的图标

            _PictureFrame.UpdateRectangleList(Document.RectangleList);

            _FrameMenu = new FrameContextMenu();
            _FrameMenu.Cuting += (s, e) => _PictureFrame.SetSelectedMode(RectangleList.SelectedMode.Cut);
            _FrameMenu.Copying += (s, e) => _PictureFrame.SetSelectedMode(RectangleList.SelectedMode.Copy);
            _FrameMenu.Pasteing += (s, e) =>
            {
                var screenPoint = this.PointToScreen(_FrameMenu.Location);
                _PictureFrame.PasteSelected(screenPoint);
            };
            _FrameMenu.Deleting += (s, e) => _PictureFrame.DeleteSelected();
            _FrameMenu.SelectingAll += (s, e) => _PictureFrame.SelectAll();
            _FrameMenu.SetRealLocationComplete += (s, e) => OnSetRealLocationComplete();

            _BaseToolStrip.Renderer = new DesignModeToolStripRenderer();

            _BaseToolStrip.Location = new Point(0, 0);
            _EditToolStrip.Location = new Point(_BaseToolStrip.Width + 5, 0);
            _AlignToolStrip.Location = new Point(_BaseToolStrip.Width + _EditToolStrip.Width + 5, 0);
            _ArrowToolStrip.Location = new Point(
                _BaseToolStrip.Width + _EditToolStrip.Width + _AlignToolStrip.Width + 5, 0);

            _PictureFrame.BenchDoubleClick += _Frame_BenchDoubleClick;
            _PictureFrame.RectangleClick += _Frame_RectangleClick;
            _PictureFrame.RectangleCreated += _Frame_RectangleCreated;
            _PictureFrame.ZoomChanged += _Frame_ZoomChanged;

            _SelectingButton.Click += _SelectingButton_Click;
            _DesignButton.Click += _DesignButton_Click;
            _DraggingButton.Click += _DraggingButton_Click;
            _ZoomButton.Click += _ZoomButton_Click;

            _CutButton.Click += (s, e) => _PictureFrame.SetSelectedMode(RectangleList.SelectedMode.Cut);
            _CopyButton.Click += (s, e) => _PictureFrame.SetSelectedMode(RectangleList.SelectedMode.Copy);
            _PasteButton.Click += (s, e) => _PictureFrame.PasteSelected(Point.Empty);
            _DeleteButton.Click += (s, e) => _PictureFrame.DeleteSelected();

            _PictureFrame.Rectangles.Current.SelectedModeChanged += (s, e) =>
            {
                if (e.NewItem == RectangleList.SelectedMode.Copy || e.NewItem == RectangleList.SelectedMode.Cut)
                {
                    _PasteButton.Enabled = true;
                    _FrameMenu.SetPasteEnable(true);
                }
                else
                {
                    _PasteButton.Enabled = false;
                    _FrameMenu.SetPasteEnable(false);
                }
            };

            _PictureFrame.Rectangles.Current.CollectionChanged += (s, e) =>
            {
                if (_PictureFrame.Rectangles.Current.Count > 0)
                {
                    _CutButton.Enabled = true;
                    _CopyButton.Enabled = true;
                    _DeleteButton.Enabled = true;
                    _FrameMenu.SetRectangleOperatorEnable(true);
                }
                else
                {
                    _CutButton.Enabled = false;
                    _CopyButton.Enabled = false;
                    _PasteButton.Enabled = false;
                    _DeleteButton.Enabled = false;
                    _FrameMenu.SetRectangleOperatorEnable(false);
                }
            };

            _SelectingButton.CheckStateChanged += (s, e) =>
            {
                _AlignToolStrip.Enabled = _SelectingButton.Checked;
                _ArrowToolStrip.Enabled = _SelectingButton.Checked;
                _EditToolStrip.Enabled = _SelectingButton.Checked;
            };
            _SelectingButton.Checked = true;

            /*以下是矩形操作的工作条按钮对应的操作方法*/
            _AlignBottomButton.Click += (s, e) => _PictureFrame.RectangleOperating(RectangleOperation.AlignBottom);
            _AlignCenterButton.Click += (s, e) => _PictureFrame.RectangleOperating(RectangleOperation.AlignCenter);
            _AlignHeightButton.Click += (s, e) => _PictureFrame.RectangleOperating(RectangleOperation.AlignHeight);
            _AlignLeftButton.Click += (s, e) => _PictureFrame.RectangleOperating(RectangleOperation.AlignLeft);
            _AlignMiddleButton.Click += (s, e) => _PictureFrame.RectangleOperating(RectangleOperation.AlignMiddle);
            _AlignRightButton.Click += (s, e) => _PictureFrame.RectangleOperating(RectangleOperation.AlignRight);
            _AlignSameButton.Click += (s, e) => _PictureFrame.RectangleOperating(RectangleOperation.AlignSame);
            _AlignTopButton.Click += (s, e) => _PictureFrame.RectangleOperating(RectangleOperation.AlignTop);
            _AlignWidthButton.Click += (s, e) => _PictureFrame.RectangleOperating(RectangleOperation.AlignWidth);

            _ArrowDownButton.Click += (s, e) => _PictureFrame.RectangleOperating(RectangleOperation.ArrowDown);
            _ArrowInButton.Click += (s, e) => _PictureFrame.RectangleOperating(RectangleOperation.ArrowIn);
            _ArrowLeftButton.Click += (s, e) => _PictureFrame.RectangleOperating(RectangleOperation.ArrowLeft);
            _ArrowOutButton.Click += (s, e) => _PictureFrame.RectangleOperating(RectangleOperation.ArrowOut);
            _ArrowRightButton.Click += (s, e) => _PictureFrame.RectangleOperating(RectangleOperation.ArrowRight);
            _ArrowUpButton.Click += (s, e) => _PictureFrame.RectangleOperating(RectangleOperation.ArrowUp);

            LoadImage(Document.ImageFullFileName);
        }

        private void LoadImage(string imgFile)
        {
            if (!string.IsNullOrEmpty(imgFile))
            {
                Image image = null;
                try
                {
                    //防止文件被锁定，将文件读入内存流中使用
                    byte[] bytes = File.ReadAllBytes(imgFile);
                    var mm = new MemoryStream(bytes);
                    image = Image.FromStream(mm);
                }
                catch (Exception e)
                {
                    Debug.Fail("载入图片异常");
                    MessageBox.Show("图片可能已损坏,请更换图片,或重试", "载入图片", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _ImageLoaded = false;
                }
                if (image != null)
                {
                    _PictureFrame.SetSelectedImage(image);
                    _ImageLoaded = true;
                }
                else
                {
                    _ImageLoaded = false;
                }
            }
            else
            {
                _ImageLoaded = false;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PictureDocumentView));
            this._PicutureDocumentToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._ModeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._ZoomLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._PictureFrame = new NKnife.Draws.Controls.Frames.PictureFrame();
            this._BaseToolStrip = new System.Windows.Forms.ToolStrip();
            this._SelectingButton = new System.Windows.Forms.ToolStripButton();
            this._DesignButton = new System.Windows.Forms.ToolStripButton();
            this._DraggingButton = new System.Windows.Forms.ToolStripButton();
            this._ZoomButton = new System.Windows.Forms.ToolStripButton();
            this._AlignToolStrip = new System.Windows.Forms.ToolStrip();
            this._AlignLeftButton = new System.Windows.Forms.ToolStripButton();
            this._AlignRightButton = new System.Windows.Forms.ToolStripButton();
            this._AlignBottomButton = new System.Windows.Forms.ToolStripButton();
            this._AlignTopButton = new System.Windows.Forms.ToolStripButton();
            this._AlignCenterButton = new System.Windows.Forms.ToolStripButton();
            this._AlignMiddleButton = new System.Windows.Forms.ToolStripButton();
            this._AlignSameButton = new System.Windows.Forms.ToolStripButton();
            this._AlignHeightButton = new System.Windows.Forms.ToolStripButton();
            this._AlignWidthButton = new System.Windows.Forms.ToolStripButton();
            this._EditToolStrip = new System.Windows.Forms.ToolStrip();
            this._CutButton = new System.Windows.Forms.ToolStripButton();
            this._CopyButton = new System.Windows.Forms.ToolStripButton();
            this._PasteButton = new System.Windows.Forms.ToolStripButton();
            this._DeleteButton = new System.Windows.Forms.ToolStripButton();
            this._ArrowToolStrip = new System.Windows.Forms.ToolStrip();
            this._ArrowInButton = new System.Windows.Forms.ToolStripButton();
            this._ArrowOutButton = new System.Windows.Forms.ToolStripButton();
            this._ToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this._ArrowLeftButton = new System.Windows.Forms.ToolStripButton();
            this._ArrowRightButton = new System.Windows.Forms.ToolStripButton();
            this._ArrowDownButton = new System.Windows.Forms.ToolStripButton();
            this._ArrowUpButton = new System.Windows.Forms.ToolStripButton();
            this._RealRectangleLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._CurrentRectangleLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._DraggingInfoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._PicutureDocumentToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this._PicutureDocumentToolStripContainer.ContentPanel.SuspendLayout();
            this._PicutureDocumentToolStripContainer.TopToolStripPanel.SuspendLayout();
            this._PicutureDocumentToolStripContainer.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this._BaseToolStrip.SuspendLayout();
            this._AlignToolStrip.SuspendLayout();
            this._EditToolStrip.SuspendLayout();
            this._ArrowToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _PicutureDocumentToolStripContainer
            // 
            // 
            // _PicutureDocumentToolStripContainer.BottomToolStripPanel
            // 
            this._PicutureDocumentToolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // _PicutureDocumentToolStripContainer.ContentPanel
            // 
            this._PicutureDocumentToolStripContainer.ContentPanel.Controls.Add(this._PictureFrame);
            this._PicutureDocumentToolStripContainer.ContentPanel.Size = new System.Drawing.Size(697, 398);
            this._PicutureDocumentToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._PicutureDocumentToolStripContainer.LeftToolStripPanelVisible = false;
            this._PicutureDocumentToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this._PicutureDocumentToolStripContainer.Name = "_PicutureDocumentToolStripContainer";
            this._PicutureDocumentToolStripContainer.RightToolStripPanelVisible = false;
            this._PicutureDocumentToolStripContainer.Size = new System.Drawing.Size(697, 470);
            this._PicutureDocumentToolStripContainer.TabIndex = 0;
            this._PicutureDocumentToolStripContainer.Text = "toolStripContainer1";
            // 
            // _PicutureDocumentToolStripContainer.TopToolStripPanel
            // 
            this._PicutureDocumentToolStripContainer.TopToolStripPanel.Controls.Add(this._ArrowToolStrip);
            this._PicutureDocumentToolStripContainer.TopToolStripPanel.Controls.Add(this._EditToolStrip);
            this._PicutureDocumentToolStripContainer.TopToolStripPanel.Controls.Add(this._AlignToolStrip);
            this._PicutureDocumentToolStripContainer.TopToolStripPanel.Controls.Add(this._BaseToolStrip);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ModeLabel,
            this._ZoomLabel,
            this._RealRectangleLabel,
            this._CurrentRectangleLabel,
            this._DraggingInfoLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(697, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // _ModeLabel
            // 
            this._ModeLabel.Name = "_ModeLabel";
            this._ModeLabel.Size = new System.Drawing.Size(32, 17);
            this._ModeLabel.Text = "选择";
            // 
            // _ZoomLabel
            // 
            this._ZoomLabel.Name = "_ZoomLabel";
            this._ZoomLabel.Size = new System.Drawing.Size(57, 17);
            this._ZoomLabel.Text = "100.00%";
            // 
            // _PictureFrame
            // 
            this._PictureFrame.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this._PictureFrame.Blankness = 20;
            this._PictureFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this._PictureFrame.Location = new System.Drawing.Point(0, 0);
            this._PictureFrame.Name = "_PictureFrame";
            this._PictureFrame.Size = new System.Drawing.Size(697, 398);
            this._PictureFrame.TabIndex = 0;
            this._PictureFrame.Text = "pictureFrame1";
            // 
            // _BaseToolStrip
            // 
            this._BaseToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._BaseToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._SelectingButton,
            this._DesignButton,
            this._DraggingButton,
            this._ZoomButton});
            this._BaseToolStrip.Location = new System.Drawing.Point(3, 0);
            this._BaseToolStrip.Name = "_BaseToolStrip";
            this._BaseToolStrip.Size = new System.Drawing.Size(133, 25);
            this._BaseToolStrip.TabIndex = 2;
            // 
            // _SelectingButton
            // 
            this._SelectingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._SelectingButton.Image = ((System.Drawing.Image)(resources.GetObject("_SelectingButton.Image")));
            this._SelectingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._SelectingButton.Name = "_SelectingButton";
            this._SelectingButton.Size = new System.Drawing.Size(23, 22);
            this._SelectingButton.Text = "选择(S)";
            // 
            // _DesignButton
            // 
            this._DesignButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._DesignButton.Image = ((System.Drawing.Image)(resources.GetObject("_DesignButton.Image")));
            this._DesignButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DesignButton.Name = "_DesignButton";
            this._DesignButton.Size = new System.Drawing.Size(23, 22);
            this._DesignButton.Text = "设计(D)";
            // 
            // _DraggingButton
            // 
            this._DraggingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._DraggingButton.Image = ((System.Drawing.Image)(resources.GetObject("_DraggingButton.Image")));
            this._DraggingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DraggingButton.Name = "_DraggingButton";
            this._DraggingButton.Size = new System.Drawing.Size(23, 22);
            this._DraggingButton.Text = "拖动(H)";
            // 
            // _ZoomButton
            // 
            this._ZoomButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ZoomButton.Image = ((System.Drawing.Image)(resources.GetObject("_ZoomButton.Image")));
            this._ZoomButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ZoomButton.Name = "_ZoomButton";
            this._ZoomButton.Size = new System.Drawing.Size(23, 22);
            this._ZoomButton.Text = "缩放(Z)";
            // 
            // _AlignToolStrip
            // 
            this._AlignToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._AlignToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._AlignLeftButton,
            this._AlignRightButton,
            this._AlignBottomButton,
            this._AlignTopButton,
            this._AlignCenterButton,
            this._AlignMiddleButton,
            this._AlignSameButton,
            this._AlignHeightButton,
            this._AlignWidthButton});
            this._AlignToolStrip.Location = new System.Drawing.Point(136, 0);
            this._AlignToolStrip.Name = "_AlignToolStrip";
            this._AlignToolStrip.Size = new System.Drawing.Size(217, 25);
            this._AlignToolStrip.TabIndex = 3;
            // 
            // _AlignLeftButton
            // 
            this._AlignLeftButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._AlignLeftButton.Image = ((System.Drawing.Image)(resources.GetObject("_AlignLeftButton.Image")));
            this._AlignLeftButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._AlignLeftButton.Name = "_AlignLeftButton";
            this._AlignLeftButton.Size = new System.Drawing.Size(23, 22);
            this._AlignLeftButton.Text = "左对齐(Ctrl+L)";
            // 
            // _AlignRightButton
            // 
            this._AlignRightButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._AlignRightButton.Image = ((System.Drawing.Image)(resources.GetObject("_AlignRightButton.Image")));
            this._AlignRightButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._AlignRightButton.Name = "_AlignRightButton";
            this._AlignRightButton.Size = new System.Drawing.Size(23, 22);
            this._AlignRightButton.Text = "右对齐(Ctrl+R)";
            // 
            // _AlignBottomButton
            // 
            this._AlignBottomButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._AlignBottomButton.Image = ((System.Drawing.Image)(resources.GetObject("_AlignBottomButton.Image")));
            this._AlignBottomButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._AlignBottomButton.Name = "_AlignBottomButton";
            this._AlignBottomButton.Size = new System.Drawing.Size(23, 22);
            this._AlignBottomButton.Text = "下对齐(Ctrl+B)";
            // 
            // _AlignTopButton
            // 
            this._AlignTopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._AlignTopButton.Image = ((System.Drawing.Image)(resources.GetObject("_AlignTopButton.Image")));
            this._AlignTopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._AlignTopButton.Name = "_AlignTopButton";
            this._AlignTopButton.Size = new System.Drawing.Size(23, 22);
            this._AlignTopButton.Text = "上对齐(Ctrl+T)";
            // 
            // _AlignCenterButton
            // 
            this._AlignCenterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._AlignCenterButton.Image = ((System.Drawing.Image)(resources.GetObject("_AlignCenterButton.Image")));
            this._AlignCenterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._AlignCenterButton.Name = "_AlignCenterButton";
            this._AlignCenterButton.Size = new System.Drawing.Size(23, 22);
            this._AlignCenterButton.Text = "纵向居中对齐(Ctrl+Alt+C)";
            // 
            // _AlignMiddleButton
            // 
            this._AlignMiddleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._AlignMiddleButton.Image = ((System.Drawing.Image)(resources.GetObject("_AlignMiddleButton.Image")));
            this._AlignMiddleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._AlignMiddleButton.Name = "_AlignMiddleButton";
            this._AlignMiddleButton.Size = new System.Drawing.Size(23, 22);
            this._AlignMiddleButton.Text = "横向居中对齐(Ctrl+Alt+M)";
            // 
            // _AlignSameButton
            // 
            this._AlignSameButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._AlignSameButton.Image = ((System.Drawing.Image)(resources.GetObject("_AlignSameButton.Image")));
            this._AlignSameButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._AlignSameButton.Name = "_AlignSameButton";
            this._AlignSameButton.Size = new System.Drawing.Size(23, 22);
            this._AlignSameButton.Text = "使所有大小相同(Ctrl+Alt+S)";
            // 
            // _AlignHeightButton
            // 
            this._AlignHeightButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._AlignHeightButton.Image = ((System.Drawing.Image)(resources.GetObject("_AlignHeightButton.Image")));
            this._AlignHeightButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._AlignHeightButton.Name = "_AlignHeightButton";
            this._AlignHeightButton.Size = new System.Drawing.Size(23, 22);
            this._AlignHeightButton.Text = "使所有高度相同(Ctrl+Alt+H)";
            // 
            // _AlignWidthButton
            // 
            this._AlignWidthButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._AlignWidthButton.Image = ((System.Drawing.Image)(resources.GetObject("_AlignWidthButton.Image")));
            this._AlignWidthButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._AlignWidthButton.Name = "_AlignWidthButton";
            this._AlignWidthButton.Size = new System.Drawing.Size(23, 22);
            this._AlignWidthButton.Text = "使所有宽度相同(Ctrl+Alt+W)";
            // 
            // _EditToolStrip
            // 
            this._EditToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._EditToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._CutButton,
            this._CopyButton,
            this._PasteButton,
            this._DeleteButton});
            this._EditToolStrip.Location = new System.Drawing.Point(3, 25);
            this._EditToolStrip.Name = "_EditToolStrip";
            this._EditToolStrip.Size = new System.Drawing.Size(102, 25);
            this._EditToolStrip.TabIndex = 5;
            // 
            // _CutButton
            // 
            this._CutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._CutButton.Enabled = false;
            this._CutButton.Image = ((System.Drawing.Image)(resources.GetObject("_CutButton.Image")));
            this._CutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._CutButton.Name = "_CutButton";
            this._CutButton.Size = new System.Drawing.Size(23, 22);
            this._CutButton.Text = "剪切(Ctrl+X)";
            // 
            // _CopyButton
            // 
            this._CopyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._CopyButton.Enabled = false;
            this._CopyButton.Image = ((System.Drawing.Image)(resources.GetObject("_CopyButton.Image")));
            this._CopyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._CopyButton.Name = "_CopyButton";
            this._CopyButton.Size = new System.Drawing.Size(23, 22);
            this._CopyButton.Text = "拷贝(Ctrl+C)";
            // 
            // _PasteButton
            // 
            this._PasteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._PasteButton.Enabled = false;
            this._PasteButton.Image = ((System.Drawing.Image)(resources.GetObject("_PasteButton.Image")));
            this._PasteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._PasteButton.Name = "_PasteButton";
            this._PasteButton.Size = new System.Drawing.Size(23, 22);
            this._PasteButton.Text = "粘贴(Ctrl+V)";
            // 
            // _DeleteButton
            // 
            this._DeleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._DeleteButton.Enabled = false;
            this._DeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("_DeleteButton.Image")));
            this._DeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteButton.Name = "_DeleteButton";
            this._DeleteButton.Size = new System.Drawing.Size(23, 22);
            this._DeleteButton.Text = "删除(Delete)";
            // 
            // _ArrowToolStrip
            // 
            this._ArrowToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._ArrowToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ArrowInButton,
            this._ArrowOutButton,
            this._ToolStripSeparator,
            this._ArrowLeftButton,
            this._ArrowRightButton,
            this._ArrowDownButton,
            this._ArrowUpButton});
            this._ArrowToolStrip.Location = new System.Drawing.Point(105, 25);
            this._ArrowToolStrip.Name = "_ArrowToolStrip";
            this._ArrowToolStrip.Size = new System.Drawing.Size(154, 25);
            this._ArrowToolStrip.TabIndex = 6;
            // 
            // _ArrowInButton
            // 
            this._ArrowInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ArrowInButton.Image = ((System.Drawing.Image)(resources.GetObject("_ArrowInButton.Image")));
            this._ArrowInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ArrowInButton.Name = "_ArrowInButton";
            this._ArrowInButton.Size = new System.Drawing.Size(23, 22);
            this._ArrowInButton.Text = "减小面积(Ctrl+Alt+I)";
            // 
            // _ArrowOutButton
            // 
            this._ArrowOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ArrowOutButton.Image = ((System.Drawing.Image)(resources.GetObject("_ArrowOutButton.Image")));
            this._ArrowOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ArrowOutButton.Name = "_ArrowOutButton";
            this._ArrowOutButton.Size = new System.Drawing.Size(23, 22);
            this._ArrowOutButton.Text = "增大面积(Ctrl+Alt+O)";
            // 
            // _ToolStripSeparator
            // 
            this._ToolStripSeparator.Name = "_ToolStripSeparator";
            this._ToolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // _ArrowLeftButton
            // 
            this._ArrowLeftButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ArrowLeftButton.Image = ((System.Drawing.Image)(resources.GetObject("_ArrowLeftButton.Image")));
            this._ArrowLeftButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ArrowLeftButton.Name = "_ArrowLeftButton";
            this._ArrowLeftButton.Size = new System.Drawing.Size(23, 22);
            this._ArrowLeftButton.Text = "向左偏移(Ctrl+Alt+L)";
            // 
            // _ArrowRightButton
            // 
            this._ArrowRightButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ArrowRightButton.Image = ((System.Drawing.Image)(resources.GetObject("_ArrowRightButton.Image")));
            this._ArrowRightButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ArrowRightButton.Name = "_ArrowRightButton";
            this._ArrowRightButton.Size = new System.Drawing.Size(23, 22);
            this._ArrowRightButton.Text = "向右偏移(Ctrl+Alt+R)";
            // 
            // _ArrowDownButton
            // 
            this._ArrowDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ArrowDownButton.Image = ((System.Drawing.Image)(resources.GetObject("_ArrowDownButton.Image")));
            this._ArrowDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ArrowDownButton.Name = "_ArrowDownButton";
            this._ArrowDownButton.Size = new System.Drawing.Size(23, 22);
            this._ArrowDownButton.Text = "向下偏移(Ctrl+Alt+D)";
            // 
            // _ArrowUpButton
            // 
            this._ArrowUpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ArrowUpButton.Image = ((System.Drawing.Image)(resources.GetObject("_ArrowUpButton.Image")));
            this._ArrowUpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ArrowUpButton.Name = "_ArrowUpButton";
            this._ArrowUpButton.Size = new System.Drawing.Size(23, 22);
            this._ArrowUpButton.Text = "向上偏移(Ctrl+Alt+U)";
            // 
            // _RealRectangleLabel
            // 
            this._RealRectangleLabel.Name = "_RealRectangleLabel";
            this._RealRectangleLabel.Size = new System.Drawing.Size(16, 17);
            this._RealRectangleLabel.Text = "[]";
            // 
            // _CurrentRectangleLabel
            // 
            this._CurrentRectangleLabel.Name = "_CurrentRectangleLabel";
            this._CurrentRectangleLabel.Size = new System.Drawing.Size(16, 17);
            this._CurrentRectangleLabel.Text = "[]";
            // 
            // _DraggingInfoLabel
            // 
            this._DraggingInfoLabel.Name = "_DraggingInfoLabel";
            this._DraggingInfoLabel.Size = new System.Drawing.Size(20, 17);
            this._DraggingInfoLabel.Text = " []";
            // 
            // PictureDocumentView
            // 
            this.ClientSize = new System.Drawing.Size(697, 470);
            this.Controls.Add(this._PicutureDocumentToolStripContainer);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "PictureDocumentView";
            this._PicutureDocumentToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this._PicutureDocumentToolStripContainer.BottomToolStripPanel.PerformLayout();
            this._PicutureDocumentToolStripContainer.ContentPanel.ResumeLayout(false);
            this._PicutureDocumentToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this._PicutureDocumentToolStripContainer.TopToolStripPanel.PerformLayout();
            this._PicutureDocumentToolStripContainer.ResumeLayout(false);
            this._PicutureDocumentToolStripContainer.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this._BaseToolStrip.ResumeLayout(false);
            this._BaseToolStrip.PerformLayout();
            this._AlignToolStrip.ResumeLayout(false);
            this._AlignToolStrip.PerformLayout();
            this._EditToolStrip.ResumeLayout(false);
            this._EditToolStrip.PerformLayout();
            this._ArrowToolStrip.ResumeLayout(false);
            this._ArrowToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        /// <summary>
        ///     设置工具栏的图标
        /// </summary>
        private void SetToolStripImage()
        {
            _SelectingButton.Image = OwnResources.dp_selecting;
            _DraggingButton.Image = OwnResources.dp_dragging;
            _DesignButton.Image = OwnResources.dp_pen;
            _ZoomButton.Image = OwnResources.dp_zoom;

            _CutButton.Image = OwnResources.dp_cut;
            _CopyButton.Image = OwnResources.dp_copy;
            _PasteButton.Image = OwnResources.dp_paste;
            _DeleteButton.Image = OwnResources.dp_delete;

            _AlignBottomButton.Image = OwnResources.align_bottom;
            _AlignCenterButton.Image = OwnResources.align_center;
            _AlignHeightButton.Image = OwnResources.align_height;
            _AlignLeftButton.Image = OwnResources.align_left;
            _AlignMiddleButton.Image = OwnResources.align_middle;
            _AlignRightButton.Image = OwnResources.align_right;
            _AlignSameButton.Image = OwnResources.align_same;
            _AlignTopButton.Image = OwnResources.align_top;
            _AlignWidthButton.Image = OwnResources.align_width;

            _ArrowDownButton.Image = OwnResources.arrow_down;
            _ArrowInButton.Image = OwnResources.arrow_in;
            _ArrowLeftButton.Image = OwnResources.arrow_left;
            _ArrowOutButton.Image = OwnResources.arrow_out;
            _ArrowRightButton.Image = OwnResources.arrow_right;
            _ArrowUpButton.Image = OwnResources.arrow_up;
        }
        #endregion

        #region 设计面板功能调用，事件响应

        public void SendKeyEvent(KeyEventArgs keys)
        {
            switch (keys.KeyData) //四个主要功能的快捷键
            {
                case Keys.S:
                    _SelectingButton_Click(null, EventArgs.Empty);
                    break;
                case Keys.D:
                    _DesignButton_Click(null, EventArgs.Empty);
                    break;
                case Keys.Z:
                    _ZoomButton_Click(null, EventArgs.Empty);
                    break;
                case Keys.H:
                    _DraggingButton_Click(null, EventArgs.Empty);
                    break;
                case Keys.Delete:
                    _PictureFrame.DeleteSelected();
                    break;
                case (Keys.ShiftKey | Keys.Shift):
                    _PictureFrame.PressShiftKey(true);
                    break;
                case (Keys.Control | Keys.A):
                    _PictureFrame.SelectAll();
                    break;
                case (Keys.Control | Keys.L):
                    _PictureFrame.RectangleOperating(RectangleOperation.AlignLeft);
                    break;
                case (Keys.Control | Keys.R):
                    _PictureFrame.RectangleOperating(RectangleOperation.AlignLeft);
                    break;
                case (Keys.Control | Keys.B):
                    _PictureFrame.RectangleOperating(RectangleOperation.AlignBottom);
                    break;
                case (Keys.Control | Keys.T):
                    _PictureFrame.RectangleOperating(RectangleOperation.AlignTop);
                    break;
                case (Keys.Control | Keys.S):
                    _PictureFrame.RectangleOperating(RectangleOperation.AlignSame);
                    break;
                case (Keys.Control | Keys.H):
                    _PictureFrame.RectangleOperating(RectangleOperation.AlignHeight);
                    break;
                case (Keys.Control | Keys.W):
                    _PictureFrame.RectangleOperating(RectangleOperation.AlignWidth);
                    break;
                case (Keys.Control | Keys.Alt | Keys.C):
                    _PictureFrame.RectangleOperating(RectangleOperation.AlignCenter);
                    break;
                case (Keys.Control | Keys.Alt | Keys.M):
                    _PictureFrame.RectangleOperating(RectangleOperation.AlignMiddle);
                    break;
                case (Keys.Control | Keys.Alt | Keys.I):
                    _PictureFrame.RectangleOperating(RectangleOperation.ArrowIn);
                    break;
                case (Keys.Control | Keys.Alt | Keys.O):
                    _PictureFrame.RectangleOperating(RectangleOperation.ArrowOut);
                    break;
                case (Keys.Control | Keys.Alt | Keys.L):
                    _PictureFrame.RectangleOperating(RectangleOperation.ArrowLeft);
                    break;
                case (Keys.Control | Keys.Alt | Keys.R):
                    _PictureFrame.RectangleOperating(RectangleOperation.ArrowRight);
                    break;
                case (Keys.Control | Keys.Alt | Keys.D):
                    _PictureFrame.RectangleOperating(RectangleOperation.ArrowDown);
                    break;
                case (Keys.Control | Keys.Alt | Keys.U):
                    _PictureFrame.RectangleOperating(RectangleOperation.ArrowUp);
                    break;
            }
        }

        public void UpShiftKey()
        {
            _PictureFrame.PressShiftKey(false);
        }

        private void OnSetRealLocationComplete()
        {
            //TODO:
        }

        private void _Frame_ZoomChanged(object sender, ChangedEventArgs<double> e)
        {
            string zoom = string.Format("{0}%", (e.NewItem * 100).ToString("0"));
            _ZoomLabel.Text = zoom;
        }

        private void _Frame_BenchDoubleClick(object sender, EventArgs e)
        {
        }

        private void _Frame_RectangleCreated(object sender, RectangleListChangedEventArgs e)
        {
            //_Centre.RectangleListChangedEventArgs = e;
            RectangleF rect = e.ActivedRectangle;
            string r = string.Format("{0},{1}, {2},{3}", rect.X.ToString("0"), rect.Y.ToString("0"),
                rect.Width.ToString("0"), rect.Height.ToString("0"));
            _CurrentRectangleLabel.Text = r;
        }

        private void _Frame_RectangleClick(object sender, RectangleClickEventArgs e)
        {
            //右键单击时弹出右键菜单
            if (e.Clicks == 1 && e.Button == MouseButtons.Right && e.Mode == DrawingBoardDesignMode.Selecting)
            {
                //_FrameMenu.SetParams(_Document, e.Rectangle, _ImageSourceSize);
                _FrameMenu.Show(MousePosition);
            }
        }

        #endregion

        #region 工具栏功能

        private void _SelectingButton_Click(object sender, EventArgs e)
        {
            SetDesignMode(DrawingBoardDesignMode.Selecting);
            _SelectingButton.CheckState = CheckState.Checked;
            _ModeLabel.Text = "选择";
        }

        private void _DesignButton_Click(object sender, EventArgs e)
        {
            SetDesignMode(DrawingBoardDesignMode.Designing);
            _DesignButton.CheckState = CheckState.Checked;
            _ModeLabel.Text = "设计";
        }

        private void _DraggingButton_Click(object sender, EventArgs e)
        {
            SetDesignMode(DrawingBoardDesignMode.Dragging);
            _DraggingButton.CheckState = CheckState.Checked;
            _ModeLabel.Text = "拖动";
        }

        private void _ZoomButton_Click(object sender, EventArgs e)
        {
            SetDesignMode(DrawingBoardDesignMode.Zooming);
            _ZoomButton.CheckState = CheckState.Checked;
            _ModeLabel.Text = "缩放";
        }

        private void SetDesignMode(DrawingBoardDesignMode mode)
        {
            _SelectingButton.CheckState = CheckState.Unchecked;
            _DraggingButton.CheckState = CheckState.Unchecked;
            _ZoomButton.CheckState = CheckState.Unchecked;
            _DesignButton.CheckState = CheckState.Unchecked;
            _PictureFrame.SetDrawingBoardDesignMode(mode);
        }

        #endregion

        #region 私有函数


        #endregion
    }
}
