using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.App.PictureTextPicker.Common.Base;
using NKnife.Draws.Controls.Frames;
using NKnife.Ioc;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker.Views
{
    public class PictureDocumentView : DockContent
    {
        private readonly IPictureList _PictureList = DI.Get<IPictureList>();
        private ToolStripContainer PicutureDocumentToolStripContainer;
        private PictureFrame _PictureFrame;
        private bool _ImageLoaded;
        public PictureDocumentView()
        {
            InitializeComponent();
        }

        public void SetSelectedPicuture(string imageFileName)
        {
            LoadImage(imageFileName);
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
            this.PicutureDocumentToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this._PictureFrame = new NKnife.Draws.Controls.Frames.PictureFrame();
            this.PicutureDocumentToolStripContainer.ContentPanel.SuspendLayout();
            this.PicutureDocumentToolStripContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // PicutureDocumentToolStripContainer
            // 
            this.PicutureDocumentToolStripContainer.BottomToolStripPanelVisible = false;
            // 
            // PicutureDocumentToolStripContainer.ContentPanel
            // 
            this.PicutureDocumentToolStripContainer.ContentPanel.Controls.Add(this._PictureFrame);
            this.PicutureDocumentToolStripContainer.ContentPanel.Size = new System.Drawing.Size(426, 324);
            this.PicutureDocumentToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PicutureDocumentToolStripContainer.LeftToolStripPanelVisible = false;
            this.PicutureDocumentToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.PicutureDocumentToolStripContainer.Name = "PicutureDocumentToolStripContainer";
            this.PicutureDocumentToolStripContainer.RightToolStripPanelVisible = false;
            this.PicutureDocumentToolStripContainer.Size = new System.Drawing.Size(426, 349);
            this.PicutureDocumentToolStripContainer.TabIndex = 0;
            this.PicutureDocumentToolStripContainer.Text = "toolStripContainer1";
            // 
            // _PictureFrame
            // 
            this._PictureFrame.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this._PictureFrame.Blankness = 20;
            this._PictureFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this._PictureFrame.Location = new System.Drawing.Point(0, 0);
            this._PictureFrame.Name = "_PictureFrame";
            this._PictureFrame.Size = new System.Drawing.Size(426, 324);
            this._PictureFrame.TabIndex = 0;
            this._PictureFrame.Text = "pictureFrame1";
            // 
            // PictureDocumentView
            // 
            this.ClientSize = new System.Drawing.Size(426, 349);
            this.Controls.Add(this.PicutureDocumentToolStripContainer);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "PictureDocumentView";
            this.PicutureDocumentToolStripContainer.ContentPanel.ResumeLayout(false);
            this.PicutureDocumentToolStripContainer.ResumeLayout(false);
            this.PicutureDocumentToolStripContainer.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
