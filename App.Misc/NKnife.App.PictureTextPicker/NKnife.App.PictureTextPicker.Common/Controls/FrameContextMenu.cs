using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NKnife.App.PictureTextPicker.Common.Base.Res;
using NKnife.App.PictureTextPicker.Common.Entities;

namespace NKnife.App.PictureTextPicker.Common.Controls
{
    public sealed class FrameContextMenu : ContextMenuStrip
    {
        private readonly ToolStripMenuItem _SetupItem = new ToolStripMenuItem("设置");
        private readonly ToolStripMenuItem _SelectAllItem = new ToolStripMenuItem("全选");
        private readonly ToolStripMenuItem _CutItem = new ToolStripMenuItem("剪切");
        private readonly ToolStripMenuItem _CopyItem = new ToolStripMenuItem("拷贝");
        private readonly ToolStripMenuItem _PasteItem = new ToolStripMenuItem("粘贴");
        private readonly ToolStripMenuItem _DeleteItem = new ToolStripMenuItem("删除");

        private PictureFrameDocument _Document;
        private bool _IsSimpleMode;
        private RectangleF _Rectangle;
        private SizeF _SrcImageSize;

        public FrameContextMenu()
        {
            Items.Add(_SetupItem);
            Items.Add(new ToolStripSeparator());
            Items.Add(_SelectAllItem);
            Items.Add(_CutItem);
            Items.Add(_CopyItem);
            Items.Add(_PasteItem);
            Items.Add(_DeleteItem);

            _SetupItem.Click += (s, e) => OnSetRealLocationComplete();

            _SelectAllItem.ShowShortcutKeys = true;
            _SelectAllItem.ShortcutKeys = Keys.A | Keys.Control;
            _SelectAllItem.Click += (s, e) => OnSelectingAll();

            _CutItem.Enabled = false;
            _CutItem.Image = OwnResources.dp_cut;
            _CutItem.ShowShortcutKeys = true;
            _CutItem.ShortcutKeys = Keys.X | Keys.Control;
            _CutItem.Click += (s, e) => OnCuting();

            _CopyItem.Enabled = false;
            _CopyItem.Image = OwnResources.dp_copy;
            _CopyItem.ShowShortcutKeys = true;
            _CopyItem.ShortcutKeys = Keys.C | Keys.Control;
            _CopyItem.Click += (s, e) => OnCopying();

            _PasteItem.Enabled = false;
            _PasteItem.Image = OwnResources.dp_paste;
            _PasteItem.ShowShortcutKeys = true;
            _PasteItem.ShortcutKeys = Keys.V | Keys.Control;
            _PasteItem.Click += (s, e) => OnPasteing();

            _DeleteItem.Enabled = false;
            _DeleteItem.Image = OwnResources.dp_delete;
            _DeleteItem.ShowShortcutKeys = true;
            _DeleteItem.ShortcutKeys = Keys.Delete;
            _DeleteItem.Click += (s, e) => OnDeleting();

            Opened += RectangleContextMenu_Opened;
        }

        public void SetParams(PictureFrameDocument document, RectangleF rectangle, SizeF srcImageSize)
        {
            _Document = document;
            _Rectangle = rectangle;
            _SrcImageSize = srcImageSize;
            _IsSimpleMode = (rectangle == Rectangle.Empty);
        }

        //当右键菜单展开时安装菜单项
        private void RectangleContextMenu_Opened(object sender, EventArgs e)
        {
            if (!_IsSimpleMode)
            {
                _SetupItem.Enabled = true;
            }
            else
            {
                _SetupItem.Enabled = false;
            }
        }

        //根据已知的参数计算所绘制的矩形的实际尺寸信息
        private RectangleF GetRealLocation()
        {
            float zoom = _Document.ImageWidth / _SrcImageSize.Width;
            var x = (float)Math.Round(_Rectangle.X * zoom, 1);
            var y = (float)Math.Round(_Rectangle.Y * zoom, 1);
            var w = (float)Math.Round(_Rectangle.Width * zoom, 1);
            var h = (float)Math.Round(_Rectangle.Height * zoom, 1);
            return new RectangleF(x, y, w, h);
        }

        /// <summary>
        /// 当设置实际尺寸坐标完成后发生
        /// </summary>
        public event EventHandler SetRealLocationComplete;

        private void OnSetRealLocationComplete()
        {
            EventHandler handler = SetRealLocationComplete;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler Cuting;

        private void OnCuting()
        {
            EventHandler handler = Cuting;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler Copying;

        private void OnCopying()
        {
            EventHandler handler = Copying;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler Pasteing;

        private void OnPasteing()
        {
            EventHandler handler = Pasteing;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler Deleting;

        private void OnDeleting()
        {
            EventHandler handler = Deleting;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler SelectingAll;

        private void OnSelectingAll()
        {
            EventHandler handler = SelectingAll;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void SetRectangleOperatorEnable(bool enable)
        {
            _DeleteItem.Enabled = enable;
            _CutItem.Enabled = enable;
            _CopyItem.Enabled = enable;
            _DeleteItem.Enabled = enable;
            if (!enable)
                _PasteItem.Enabled = false;
        }

        public void SetPasteEnable(bool enable)
        {
            _PasteItem.Enabled = enable;
        }
    }
}