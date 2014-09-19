using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        static public class Pad
        {
            static Dictionary<int, BottomData> _dicBottomData = new Dictionary<int, BottomData>();

            static public void ShowPanel(List<KeyValuePair<string, string>> showText, string imageLocation, Control panel2)
            {
                BottomData bottomData;
                ///创建BottomData
                if (!_dicBottomData.TryGetValue(showText.Count + (string.IsNullOrEmpty(imageLocation) ? 0 : 10000), out bottomData))
                {
                    ///初始化bottomData
                    if (string.IsNullOrEmpty(imageLocation))
                    {
                        bottomData = new BottomData(panel2, new TableLayoutPanel(), new KeyValuePair<Control, Control>[showText.Count]);
                    }
                    else
                    {
                        bottomData = new BottomData(panel2, new TableLayoutPanel(), new KeyValuePair<Control, Control>[showText.Count], new PictureBox(), new SplitContainer());
                    }
                    _dicBottomData.Add(showText.Count + (string.IsNullOrEmpty(imageLocation) ? 0 : 10000), bottomData);
                }

                ///将其添加到底部
                if (bottomData.HasImage)
                {
                    if (panel2.Controls.Count == 0 || panel2.Controls[0] != bottomData.ImageSplitContainer)
                    {
                        panel2.Controls.Clear();
                        panel2.Controls.Add(bottomData.ImageSplitContainer);
                    }
                }
                else
                {
                    if (panel2.Controls.Count == 0 || panel2.Controls[0] != bottomData.MainTableLayoutPanel)
                    {
                        panel2.Controls.Clear();
                        panel2.Controls.Add(bottomData.MainTableLayoutPanel);
                    }
                }

                ///设置Text值
                KeyValuePair<Control, Control>[] keyvalues = bottomData.KeyValueControls;
                for (int i = 0; i < showText.Count; i++)
                {
                    keyvalues[i].Key.Text = showText[i].Key;
                    keyvalues[i].Value.Text = showText[i].Value;
                }

                ///设置图片地址
                if (!string.IsNullOrEmpty(imageLocation))
                {
                    bottomData.PictureBox.ImageLocation = imageLocation;
                }
            }

            //   static public void ShowPanel(List<KeyValuePair<string, string>> showText, string imageLocation, Control panel2)
            //{
            //    //TableLayoutPanel panel = null;
            //    //Control returnControl = null;
            //    //Dictionary<int, TableLayoutPanel> dicTableLayoutPanel = (imageLocation == null ? _dicNoImageTableLayoutPanel : _dicImageTableLayoutPanel);
            //    //Dictionary<int, KeyValuePair<Control, Control>[]> dicControls = (imageLocation == null ? _dicNoImageControls : _dicImageControls);

            //    BottomData bottomData;
            //    ///创建BottomData
            //    if (!_dicBottomData.TryGetValue(showText.Count + (string.IsNullOrEmpty(imageLocation) ? 0 : 10000), out bottomData))
            //    {
            //        ///初始化bottomData
            //        if (string.IsNullOrEmpty(imageLocation))
            //        {
            //            bottomData = new BottomData(panel2, new TableLayoutPanel(), new KeyValuePair<Control, Control>[showText.Count]);
            //        }
            //        else
            //        {
            //            bottomData = new BottomData(panel2, new TableLayoutPanel(), new KeyValuePair<Control, Control>[showText.Count], new PictureBox(), new SplitContainer());
            //        }

            //        KeyValuePair<Control, Control>[] keyvalues = bottomData.KeyValueControls;
            //        for (int i = 0; i < showText.Count; i++)
            //        {
            //            keyvalues[i].Key.Text = showText[i].Key;
            //            keyvalues[i].Value.Text = showText[i].Value;
            //        }

            //        if (imageLocation != null)
            //        {
            //            bottomData.PictureBox.ImageLocation = imageLocation;
            //        }
            //    }

            //    //if (!dicTableLayoutPanel.TryGetValue(showText.Count, out panel))
            //    //{
            //    //    panel = new TableLayoutPanel();
            //    //    //panel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            //    //    //dicTableLayoutPanel.Add(showText.Count, panel);

            //    //    //panel.ColumnCount = 1;
            //    //    //panel.GrowStyle = TableLayoutPanelGrowStyle.AddRows;

            //    //    ///创建其下的Controls(Label,PictureBox)
            //    //    KeyValuePair<Control, Control>[] listControls = new KeyValuePair<Control, Control>[showText.Count];
            //    //    dicControls.Add(showText.Count, listControls);

            //    //    for (int i = 0; i < showText.Count; i++)
            //    //    {
            //    //        RowStyle rowStyle = new RowStyle(SizeType.Absolute,20);
            //    //        panel.RowStyles.Add(rowStyle);

            //    //        ///把keyLabel和valueLabel用SplitContainer分割
            //    //        SplitContainer containerKeyvalue = new SplitContainer();
            //    //        containerKeyvalue.Dock = DockStyle.Fill;
            //    //        containerKeyvalue.Panel1MinSize = 0;
            //    //        containerKeyvalue.Panel2MinSize = 0;
            //    //        containerKeyvalue.IsSplitterFixed = true;
            //    //        containerKeyvalue.SplitterWidth = 1;

            //    //        Label keyLabel = new Label();
            //    //        keyLabel.BackColor = Color.White;
            //    //        keyLabel.Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
            //    //        keyLabel.Margin = new Padding(0);
            //    //        keyLabel.AutoSize = true;
            //    //        //panel.Controls.Add(keyLabel);

            //    //        Label valueLabel = new Label();
            //    //        valueLabel.BackColor = Color.Wheat;
            //    //        valueLabel.Margin = new Padding(0);
            //    //        valueLabel.AutoSize = false;
            //    //        valueLabel.Font = SystemFonts.DialogFont;

            //    //        EventHandler resetLabelSize = delegate
            //    //        {
            //    //            Label senderLabel = valueLabel;
            //    //            Graphics testGraphics = valueLabel.CreateGraphics();
            //    //            SizeF textSizeF = testGraphics.MeasureString(senderLabel.Text, senderLabel.Font);
            //    //            testGraphics.Dispose();

            //    //            int lineWidth = (containerKeyvalue.Orientation == Orientation.Vertical) ? containerKeyvalue.Panel2.Width - keyLabel.Width : containerKeyvalue.Panel2.Width;
            //    //            valueLabel.Width = lineWidth;
            //    //            valueLabel.Height = (int)Math.Ceiling(((float)Math.Ceiling(textSizeF.Width / (float)lineWidth)) * (textSizeF.Height + 1.0f));
            //    //            rowStyle.Height = (containerKeyvalue.Orientation == Orientation.Horizontal) ? valueLabel.Height + keyLabel.Height : valueLabel.Height;
            //    //            return;
            //    //        };
            //    //        valueLabel.TextChanged += resetLabelSize;
            //    //        containerKeyvalue.Panel2.SizeChanged += resetLabelSize;

            //    //        //panel.Controls.Add(valueLabel);

            //    //        KeyValuePair<Control, Control> keyvalue = new KeyValuePair<Control, Control>(keyLabel, valueLabel);
            //    //        listControls[i] = keyvalue;

            //    //        ///设置SplitContainer的位置和外观
            //    //        containerKeyvalue.Orientation = (panel2.Size.Width > panel2.Size.Height) ? Orientation.Vertical : Orientation.Horizontal;
            //    //        switch (containerKeyvalue.Orientation)
            //    //        {
            //    //            case Orientation.Horizontal:
            //    //                containerKeyvalue.SplitterDistance = keyLabel.Height + 1;
            //    //                //container.Height = keyLabel.Height + keyLabel.Height;
            //    //                break;
            //    //            case Orientation.Vertical:
            //    //                containerKeyvalue.SplitterDistance = keyLabel.Width;
            //    //                //container.Height = keyLabel.Height;
            //    //                break;
            //    //        }
            //    //        panel.SizeChanged += delegate
            //    //        {
            //    //            switch (containerKeyvalue.Orientation)
            //    //            {
            //    //                case Orientation.Horizontal:
            //    //                    containerKeyvalue.SplitterDistance = keyLabel.Height + 1;
            //    //                    //container.Height = keyLabel.Height + keyLabel.Height;
            //    //                    break;
            //    //                case Orientation.Vertical:
            //    //                    containerKeyvalue.SplitterDistance = keyLabel.Width;
            //    //                    //container.Height = keyLabel.Height;
            //    //                    break;
            //    //            }
            //    //            containerKeyvalue.Orientation = (panel.Width > panel.Height) ? Orientation.Vertical : Orientation.Horizontal;
            //    //        };

            //    //        containerKeyvalue.Panel1.Controls.Add(keyLabel);
            //    //        containerKeyvalue.Panel2.Controls.Add(valueLabel);
            //    //        panel.Controls.Add(containerKeyvalue);
            //    //    }

            //    //    if (imageLocation != null)
            //    //    {
            //    //        SplitContainer imgSplitContainer = new SplitContainer();
            //    //        imgSplitContainer.Dock = DockStyle.Fill;
            //    //        imgSplitContainer.Panel1MinSize = 0;
            //    //        imgSplitContainer.Panel2MinSize = 0;
            //    //        imgSplitContainer.IsSplitterFixed = true;
            //    //        imgSplitContainer.SplitterWidth = 1;

            //    //        ///设置imgSplitContainer位置和外观
            //    //        imgSplitContainer.Orientation = (panel2.Size.Width < panel2.Size.Height) ? Orientation.Horizontal : Orientation.Vertical;
            //    //        switch (imgSplitContainer.Orientation)
            //    //        {
            //    //            case Orientation.Horizontal:
            //    //                imgSplitContainer.SplitterDistance = imgSplitContainer.Width-(Math.Min(imgSplitContainer.Height / 2,PictureHeight));
            //    //                break;
            //    //            case Orientation.Vertical:
            //    //                imgSplitContainer.SplitterDistance = imgSplitContainer.Width-(Math.Min(imgSplitContainer.Width / 2,PictureWidth));
            //    //                break;
            //    //        }
            //    //        imgSplitContainer.SizeChanged += delegate
            //    //        {
            //    //            imgSplitContainer.Orientation = (panel2.Size.Width < panel2.Size.Height) ? Orientation.Horizontal : Orientation.Vertical;

            //    //            switch (imgSplitContainer.Orientation)
            //    //            {
            //    //                case Orientation.Horizontal:
            //    //                    int temp = imgSplitContainer.Width - (Math.Min(imgSplitContainer.Height / 2, PictureHeight));
            //    //                    imgSplitContainer.SplitterDistance = temp > 0 ? temp : 1;
            //    //                    break;
            //    //                case Orientation.Vertical:
            //    //                    int temp2 = imgSplitContainer.Width - (Math.Min(imgSplitContainer.Width / 2, PictureWidth));
            //    //                    imgSplitContainer.SplitterDistance = temp2 > 0 ? temp2 : 1;
            //    //                    break;
            //    //            }
            //    //        };

            //    //        ///设置图片的位置和外观
            //    //        PictureBox picBox = new PictureBox();
            //    //        picBox.SizeMode = PictureBoxSizeMode.Zoom;
            //    //        picBox.Dock = DockStyle.Fill;
            //    //        _dicImage.Add(showText.Count, picBox);
            //    //        imgSplitContainer.Panel2.Controls.Add(picBox);
            //    //        panel.Dock = DockStyle.Fill;
            //    //        imgSplitContainer.Panel1.Controls.Add(panel);

            //    //        _dicSplit.Add(showText.Count, imgSplitContainer);
            //    //    }
            //    //}
            //    //returnControl = (imageLocation == null) ? (Control)panel : (Control)_dicSplit[showText.Count];

            //    ///设置Text
            //    //KeyValuePair<Control, Control>[] keyvalues = dicControls[showText.Count];
            //    //for (int i = 0; i < showText.Count; i++)
            //    //{
            //    //    keyvalues[i].Key.Text = showText[i].Key;
            //    //    keyvalues[i].Value.Text = showText[i].Value;
            //    //}

            //    //if (imageLocation != null)
            //    //{
            //    //    _dicImage[showText.Count].ImageLocation = imageLocation;
            //    //}

            //    //return returnControl;
            //}

        }

        /// <summary>
        /// 显示在右侧面板的底部的预览信息的控件数据
        /// </summary>
        class BottomData
        {
            const int PictureWidth = 200;
            const int PictureHeight = 200;

            readonly public Control OwnerControl;
            readonly public TableLayoutPanel MainTableLayoutPanel;
            readonly public KeyValuePair<Control, Control>[] KeyValueControls;
            readonly public bool HasImage;
            readonly public PictureBox PictureBox;
            readonly public SplitContainer ImageSplitContainer;
            private List<SplitContainer> containerKeyvalues = new List<SplitContainer>();

            public BottomData(Control ownerControl, TableLayoutPanel mainTableLayoutPanel, KeyValuePair<Control, Control>[] keyValueControls,
                PictureBox pictureBox, SplitContainer imageSplitContainer)
            {
                this.OwnerControl = ownerControl;
                this.MainTableLayoutPanel = mainTableLayoutPanel;
                this.KeyValueControls = keyValueControls;
                this.PictureBox = pictureBox;
                this.ImageSplitContainer = imageSplitContainer;
                this.HasImage = (pictureBox != null);

                ///初始化
                this.MainTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                this.MainTableLayoutPanel.Dock = DockStyle.Fill;
                this.MainTableLayoutPanel.ColumnCount = 1;
                this.MainTableLayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.AddRows;

                for (int i = 0; i < keyValueControls.Length; i++)
                {
                    ///MainTableLayoutPanel的RowStyle
                    RowStyle rowStyle = new RowStyle(SizeType.Absolute, 20);
                    this.MainTableLayoutPanel.RowStyles.Add(rowStyle);

                    ///把keyLabel和valueLabel用SplitContainer分割
                    SplitContainer containerKeyvalue = new SplitContainer();
                    containerKeyvalue.Margin = new Padding(0);
                    containerKeyvalue.Dock = DockStyle.Fill;
                    containerKeyvalue.Panel1MinSize = 0;
                    containerKeyvalue.Panel2MinSize = 0;
                    containerKeyvalue.IsSplitterFixed = true;
                    containerKeyvalue.SplitterWidth = 1;
                    containerKeyvalues.Add(containerKeyvalue);

                    ///初始化key和value的Label
                    Label keyLabel = new Label();
                    keyLabel.BackColor = Color.White;   //仅为了Debug
                    keyLabel.Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
                    keyLabel.Margin = new Padding(0);
                    keyLabel.AutoSize = true;

                    Label valueLabel = new Label();
                    valueLabel.BackColor = Color.Wheat;   //仅为了Debug
                    valueLabel.Margin = new Padding(0);
                    valueLabel.AutoSize = false;
                    valueLabel.Font = SystemFonts.DialogFont;

                    KeyValuePair<Control, Control> keyvalue = new KeyValuePair<Control, Control>(keyLabel, valueLabel);
                    this.KeyValueControls[i] = keyvalue;
                    containerKeyvalue.Panel1.Controls.Add(keyLabel);
                    containerKeyvalue.Panel2.Controls.Add(valueLabel);
                    this.MainTableLayoutPanel.Controls.Add(containerKeyvalue);

                    if (HasImage)
                    {
                        ///初始化
                        imageSplitContainer.Dock = DockStyle.Fill;
                        imageSplitContainer.Panel1MinSize = 0;
                        imageSplitContainer.Panel2MinSize = 0;
                        imageSplitContainer.IsSplitterFixed = true;
                        imageSplitContainer.SplitterWidth = 1;

                        ResetImageSplitContainerSize();
                        ImageSplitContainer.SizeChanged += delegate
                        {
                            ResetImageSplitContainerSize();
                        };

                        ///设置图片的位置和外观
                        PictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                        PictureBox.Dock = DockStyle.Fill;
                        ImageSplitContainer.Panel2.Controls.Add(PictureBox);

                        ///加载MainTableLayoutPanel
                        this.MainTableLayoutPanel.Dock = DockStyle.Fill;
                        ImageSplitContainer.Panel1.Controls.Add(this.MainTableLayoutPanel);
                    }

                    ResetKeyvalueContainerSize(containerKeyvalue);
                    ///重设valueLabel的Size
                    EventHandler resetValueLabelSize = delegate
                        {
                            //Graphics testGraphics = valueLabel.CreateGraphics();
                            //SizeF textSizeF = testGraphics.MeasureString(valueLabel.Text, valueLabel.Font);
                            //testGraphics.Dispose();

                            //int lineWidth = (containerKeyvalue.Orientation == Orientation.Vertical) ? containerKeyvalue.Width - keyLabel.Width : containerKeyvalue.Width;
                            //valueLabel.Width = lineWidth;
                            //valueLabel.Height = (int)Math.Ceiling(((float)Math.Ceiling(textSizeF.Width / (float)lineWidth)) * (textSizeF.Height + 1.0f));
                            //rowStyle.Height = (containerKeyvalue.Orientation == Orientation.Horizontal) ? valueLabel.Height + keyLabel.Height : valueLabel.Height;
                            //return;
                            ResetValueLabelSize();
                        };
                    valueLabel.TextChanged += resetValueLabelSize;
                    //containerKeyvalue.Panel2.SizeChanged += resetValueLabelSize;
                }

                this.MainTableLayoutPanel.SizeChanged += delegate
                {
                    foreach (SplitContainer split in this.containerKeyvalues)
                    {
                        ResetKeyvalueContainerSize(split);
                    }
                    ResetValueLabelSize();
                };
            }

            public BottomData(Control ownerControl, TableLayoutPanel mainTableLayoutPanel, KeyValuePair<Control, Control>[] keyValueControls)
                : this(ownerControl, mainTableLayoutPanel, keyValueControls, null, null)
            {
            }

            void ResetValueLabelSize()
            {
                //for (KeyValuePair<Control,Control> keyvalue in this.KeyValueControls)
                //{
                //    Graphics testGraphics = keyvalue.Value.CreateGraphics();
                //    SizeF textSizeF = testGraphics.MeasureString(keyvalue.Value.Text, keyvalue.Value.Font);
                //    testGraphics.Dispose();

                //    int lineWidth = (containerKeyvalue.Orientation == Orientation.Vertical) ? containerKeyvalue.Width - keyLabel.Width : containerKeyvalue.Width;
                //    valueLabel.Width = lineWidth;
                //    keyvalue.Value.Height = (int)Math.Ceiling(((float)Math.Ceiling(textSizeF.Width / (float)lineWidth)) * (textSizeF.Height + 1.0f));
                //    rowStyle.Height = (containerKeyvalue.Orientation == Orientation.Horizontal) ? keyvalue.Value.Height + keyvalue.Key.Height : keyvalue.Value.Height;
                //}
                for (int i = 0; i < this.KeyValueControls.Length; i++)
                {
                    Graphics testGraphics = KeyValueControls[i].Value.CreateGraphics();
                    SizeF textSizeF = testGraphics.MeasureString(KeyValueControls[i].Value.Text, KeyValueControls[i].Value.Font);
                    testGraphics.Dispose();

                    int lineWidth = (this.containerKeyvalues[i].Orientation == Orientation.Vertical) ? this.containerKeyvalues[i].Width - KeyValueControls[i].Key.Width : this.containerKeyvalues[i].Width;
                    KeyValueControls[i].Value.Width = lineWidth;
                    KeyValueControls[i].Value.Height = (int)Math.Ceiling(((float)Math.Ceiling(textSizeF.Width / (float)lineWidth)) * (textSizeF.Height + 1.0f));
                    this.MainTableLayoutPanel.RowStyles[i].Height = (this.containerKeyvalues[i].Orientation == Orientation.Horizontal) ? KeyValueControls[i].Value.Height + KeyValueControls[i].Key.Height : KeyValueControls[i].Value.Height;
                }
            }

            void ResetKeyvalueContainerSize(SplitContainer containerKeyvalue)
            {
                containerKeyvalue.Orientation = (this.MainTableLayoutPanel.Width > this.MainTableLayoutPanel.Height) ? Orientation.Vertical : Orientation.Horizontal;
                switch (containerKeyvalue.Orientation)
                {
                    case Orientation.Horizontal:
                        containerKeyvalue.SplitterDistance = containerKeyvalue.Panel1.Controls[0].Height + 1;
                        containerKeyvalue.Height = containerKeyvalue.Panel1.Controls[0].Height * 2;
                        break;
                    case Orientation.Vertical:
                        containerKeyvalue.SplitterDistance = containerKeyvalue.Panel1.Controls[0].Width;
                        containerKeyvalue.Height = containerKeyvalue.Panel1.Controls[0].Height;
                        break;
                    default:
                        Debug.Fail("");
                        break;
                }
            }

            void ResetImageSplitContainerSize()
            {
                ImageSplitContainer.Orientation = (OwnerControl.Size.Width < OwnerControl.Size.Height) ? Orientation.Horizontal : Orientation.Vertical;

                switch (ImageSplitContainer.Orientation)
                {
                    case Orientation.Horizontal:
                        int temp = ImageSplitContainer.Width - (Math.Min(ImageSplitContainer.Height / 2, PictureHeight));
                        ImageSplitContainer.SplitterDistance = temp > 0 ? temp : 1;
                        break;
                    case Orientation.Vertical:
                        int temp2 = ImageSplitContainer.Width - (Math.Min(ImageSplitContainer.Width / 2, PictureWidth));
                        ImageSplitContainer.SplitterDistance = temp2 > 0 ? temp2 : 1;
                        break;
                    default:
                        Debug.Fail("");
                        break;
                }
            }
        }
    }
}