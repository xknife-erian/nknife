using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public class MyListFolderItem : MyListItem
    {
        public MyListFolderItem(FolderXmlElement element, MyListItem parantFolder)
            : base(element)
        {
            _element = element;
            this.Text = _element.Title;
            ListShowView = View.LargeIcon;
            _myItemParnt = parantFolder;
            BreviaryMap = true; //缩略图
        }

        public ListViewItemType1 ItemType
        {
            get
            {
                return ListViewItemType1.Folder;
            }
        }

        private MyListItem _myItemParnt; //该文件夹的父文件夹,（使用目标 保存文件夹显示和上一级）
        public override MyListItem FolderItemParant
        {
            set
            {
                _myItemParnt = value;
            }
            get
            {
                return _myItemParnt;
            }
        }
        private View _listShowView;
        public override View ListShowView
        {
            set
            {
                _listShowView = value;
            }
            get
            {
                return _listShowView;
            }
        }
        //该级文件夹是否为缩略图显示模式
        private bool _breviaryMap;
        public override bool BreviaryMap
        {
            get
            {
                return _breviaryMap;
            }
            set
            {
                _breviaryMap = value;
            }
        }

    }
}