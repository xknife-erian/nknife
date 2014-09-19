using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
    public class RowEx : DataGridViewRow
    {
        /// <summary>
        /// SimplusD,lukan,扩展DataGridViewRow类型
        /// </summary>
        public RowEx()
        {

        }

        private bool _isRowDataModify;
        /// <summary>
        /// Row中的数据是否发生改变,lukan,
        /// </summary>
        public bool IsRowDataModify
        {
            get { return _isRowDataModify; }
            set { _isRowDataModify = value; }
        }

        private PageXmlDocument _pageDoc;
        public PageXmlDocument PageDoc
        {
            get
            {
                if (_pageDoc == null)
                {
                    _pageDoc = Service.Sdsite.CurrentDocument.GetPageDocumentById((string)this.Tag);
                }
                return _pageDoc;
            }
        }

        public string Title
        {
            get
            {
                if (_pageDoc == null)
                {
                    _pageDoc = Service.Sdsite.CurrentDocument.GetPageDocumentById((string)this.Tag);
                }
                return _pageDoc.Title;
            }
            set
            {
                Service.Sdsite.CurrentDocument.GetPageElementById((string)this.Tag).Title = value;
            }
        }

        public bool IsDeleted
        {
            get
            {
                if (_pageDoc == null)
                {
                    _pageDoc = Service.Sdsite.CurrentDocument.GetPageDocumentById((string)this.Tag);
                }
                return _pageDoc.IsDeleted;
            }
            set
            {
                Service.Sdsite.CurrentDocument.GetPageElementById((string)this.Tag).IsDeleted = value;
            }
        }

        public bool IsPublish
        {
            get
            {
                if (_pageDoc == null)
                {
                    _pageDoc = Service.Sdsite.CurrentDocument.GetPageDocumentById((string)this.Tag);
                }
                return _pageDoc.IsPublish;
            }
            set
            {
                Service.Sdsite.CurrentDocument.GetPageElementById((string)this.Tag).IsPublish = value;
            }
        }

        public bool IsAlreadyPublished
        {
            get
            {
                if (_pageDoc == null)
                {
                    _pageDoc = Service.Sdsite.CurrentDocument.GetPageDocumentById((string)this.Tag);
                }
                return _pageDoc.IsAlreadyPublished;
            }
            set
            {
                Service.Sdsite.CurrentDocument.GetPageElementById((string)this.Tag).IsAlreadyPublished = value;
            }
        }

        public bool IsModify
        {
            get
            {
                if (_pageDoc == null)
                {
                    _pageDoc = Service.Sdsite.CurrentDocument.GetPageDocumentById((string)this.Tag);
                }
                return _pageDoc.IsModified;
            }
            set
            {
                Service.Sdsite.CurrentDocument.GetPageElementById((string)this.Tag).IsModified = value;
            }
        }

        static public RowEx GetDataGridViewRow(DataGridViewColumn[] columns, PageXmlDocument pageDoc)
        {
            RowEx row = new RowEx();
            row.Tag = pageDoc.Id;
            DataGridViewCell cell;
            SortedDictionary<int, DataGridViewCell> cellDic = new SortedDictionary<int, DataGridViewCell>();
            foreach (DataGridViewColumn column in columns)
            {
                int index = column.DisplayIndex;
                object obj = (pageDoc.GetType().GetProperty(column.Name).GetValue(pageDoc, null));

                bool tempBool;
                if (bool.TryParse(obj.ToString(), out tempBool))
                {
                    cell = new DataGridViewImageCell();
                    switch (tempBool.ToString().ToUpper())
                    {
                        case "TRUE":
                            switch (column.Name)
                            {
                                case "IsOnceAd":
                                    cell.ToolTipText = "TRUE";
                                    cell.Value = ResourceService.GetResourceImage("page.img.oldadbitmap");//GetImage(@"Image\del.png");
                                    break;
                                case "IsAd":
                                    cell.ToolTipText = "TRUE";
                                    cell.Value = ResourceService.GetResourceImage("page.img.adbitmap");// GetImage(@"Image\ad.png");
                                    break;
                                case "IsPublish":
                                    cell.ToolTipText = "TRUE";
                                    cell.Value = ResourceService.GetResourceImage("page.img.publishbitmap");// GetImage(@"Image\public.png");
                                    break;
                                default:
                                    cell.ToolTipText = "TRUE";
                                    cell.Value = ResourceService.GetResourceImage("page.img.savebitmap");// GetImage(@"Image\save.png");
                                    break;
                            }
                            break;
                        case "FALSE":
                            switch (column.Name)
                            {
                                case "IsOnceAd":
                                    cell.ToolTipText = "FALSE";
                                    cell.Value = ResourceService.GetResourceImage("page.img.notoldbitmap");//GetImage(@"Image\del.png");
                                    break;
                                case "IsAd":
                                    cell.ToolTipText = "FALSE";
                                    cell.Value = ResourceService.GetResourceImage("page.img.addbitmap");// GetImage(@"Image\notAd.png");
                                    break;
                                case "IsPublish":
                                    cell.ToolTipText = "FALSE";
                                    cell.Value = ResourceService.GetResourceImage("page.img.notPublishbitmap");// GetImage(@"Image\notPublic.png");
                                    break;
                                default:
                                    cell.ToolTipText = "FALSE";
                                    cell.Value = ResourceService.GetResourceImage("page.img.notsavebitmap");// GetImage(@"Image\save.png");
                                    break;
                            }
                            break;
                    }
                }
                else if (obj.GetType().IsEnum && obj.GetType() == typeof(PageSimpleState))
                {

                    PageSimpleState state = (PageSimpleState)obj;
                    cell = new DataGridViewImageCell();
                    switch (state)
                    {
                        case PageSimpleState.New:
                            cell.ToolTipText = "NEW";
                            cell.Value = ResourceService.GetResourceImage("page.img.newbitmap");// GetImage(@"Image\new.png");
                            break;
                        case PageSimpleState.Modified:
                            cell.ToolTipText = "MODIFIED";
                            cell.Value = ResourceService.GetResourceImage("page.img.modifedbitmap");//GetImage(@"Image\modified.png");
                            break;
                        case PageSimpleState.NotModified:
                            cell.ToolTipText = "NOTMODIFIED";
                            cell.Value = ResourceService.GetResourceImage("page.img.notModifiedbitmap");//GetImage(@"Image\notModified.png");
                            break;
                        default:
                            Debug.Assert(false);
                            break;
                    }
                }
                else
                {
                    cell = new DataGridViewTextBoxCell();
                    cell.Value = obj;
                    cell.ToolTipText = cell.Value.ToString();
                    if (column.Name.Equals("AdTime"))
                    {
                        if (obj.ToString().Equals("9999-12-31 23:31:59"))
                        {
                            cell.Value = null;
                            cell.ToolTipText = null;
                        }
                    }
                }
                cell.Tag = column.Name;
                cellDic.Add(index, cell);
            }

            foreach (KeyValuePair<int, DataGridViewCell> pair in cellDic)
            {
                row.Cells.Add(pair.Value);
            }

            return row;
        }

    }

}
