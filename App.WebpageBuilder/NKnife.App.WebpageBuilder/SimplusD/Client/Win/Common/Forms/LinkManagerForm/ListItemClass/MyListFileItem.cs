using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
namespace Jeelu.SimplusD.Client.Win
{
    public class MyListFileItem : MyListItem
    {
        public MyListFileItem(SimpleExIndexXmlElement element)
            : base(element)
        {
            _element = element;
            this.Text = _element.Title;
        }

        MediaFileType _fileType;
        public MediaFileType ItemMediaType
        {
            get { return _fileType; }
            set { _fileType = value; }
        }

        public ListViewItemType1 ItemType
        {
            get
            {
                return ListViewItemType1.File;
            }
        }
        public XmlElement XmlAttribute { get; set; }
    }
}