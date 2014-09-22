using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public class MyListItem : ListViewItem
    {
        public SimpleExIndexXmlElement _element = null;

        public SimpleExIndexXmlElement Element
        {
            get { return _element; }
            set { _element = value; }
        }
        public virtual MyListItem FolderItemParant { get; set; }
        public virtual MyListItem FolderItemChild { get; set; }
        public virtual bool BreviaryMap { get; set; }
        public virtual View ListShowView { get; set; }

        public MyListItem(SimpleExIndexXmlElement element)
        {
            _element = element;
        }
    }
    public enum ListViewItemType1
    {
        Folder,
        File,
    }
}