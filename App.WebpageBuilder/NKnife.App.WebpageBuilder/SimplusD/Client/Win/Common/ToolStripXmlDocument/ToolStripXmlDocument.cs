using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
    public class ToolStripXmlDocument : XmlDocument
    {
        public ToolStripXmlDocument() { }

        public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
        {
            switch ( localName.ToUpper() ) {
                case "TOOLSTRIPITEM":
                    return new ToolStripItemXmlElement(this);
                case "SEPARATOR":
                    return new SeparatorXmlElement(this);
                case "ITEMVALUE":
                    return new ItemValueXmlElement(this);
                default:
                    return base.CreateElement(prefix, localName, namespaceURI);
            }
        }

        /// <summary>
        /// 获取定义菜单与工具栏的Element
        /// </summary>
        private XmlElement MenuAndToolStripsXmlElement
        {
            get
            {
                XmlElement ele = (XmlElement)this.DocumentElement.SelectSingleNode("MenuAndToolStrips");
                if (ele == null)
                {
                    throw new Exception("DesignTime: Has not Node!");
                }
                return ele;
            }
        }

        private StripType _striptype;
        /// <summary>
        /// 建立ToolStrip系统
        /// </summary>
        public virtual ToolStrip BuildToolStrip(StripType striptype)
        {
            _striptype = striptype;
            ToolStrip strip = null;
            switch ( striptype ) {
                case StripType.Menu:
                    strip = new MenuStrip();
                    BuildStripSubMethod(strip);
                    break;
                case StripType.Status:
                    strip = new StatusStrip();
                    BuildStripSubMethod(strip);
                    break;
                case StripType.ToolBar:
                    strip = new ToolStrip();
                    BuildStripSubMethod(strip);
                    break;
                case StripType.ContextMenu:
                    strip = new ContextMenuStrip();
                    BuildStripSubMethod(strip);
                    break;
                default:
                    throw new Exception("");
            }
            return strip;
        }
        private void BuildStripSubMethod(ToolStrip strip)
        {
            foreach ( XmlNode node in this.MenuAndToolStripsXmlElement.ChildNodes ) {
                if ( node.NodeType != XmlNodeType.Element ) {
                    continue;
                }
                XmlElement itemEle = (XmlElement)node;
                ToolStripItem item = BuildToolStripItem(itemEle);
                if ( item != null ) {
                    strip.Items.Add(item);
                }
                if ( strip.Items.Count > 1 ) { //处理两根相邻的分隔线
                    int i = strip.Items.Count;
                    if ( (strip.Items[ i - 1 ] is ToolStripSeparator) && (strip.Items[ i - 1 ].GetType() == strip.Items[ i - 2 ].GetType()) ) {
                        strip.Items.RemoveAt(i - 2);
                    }
                }
            }
        }

        /// <summary>
        /// 建立ToolStripItem
        /// </summary>
        virtual internal ToolStripItem BuildToolStripItem(XmlElement element)
        {
            ToolStripItem returnItem = null;

            if ( element.Name.ToUpper() == "TOOLSTRIPITEM" ) {
                ToolStripItemXmlElement itemEle = (ToolStripItemXmlElement)element;
                switch ( itemEle.Place ) {
                    case ControlPlace.Menu: {
                            if ( this._striptype == StripType.Menu || this._striptype == StripType.ContextMenu ) {
                                returnItem = MenuItemHelper(returnItem, itemEle);
                            }
                            break;
                        }
                    case ControlPlace.ToolBar: {
                            if ( this._striptype == StripType.Status || this._striptype == StripType.ToolBar ) {
                                returnItem = ToolStripItemHelper(returnItem, itemEle);
                            }
                            break;
                        }
                    case ControlPlace.MenuAndToolbar: {
                            switch ( this._striptype ) {
                                case StripType.Menu:
                                case StripType.ContextMenu:
                                    returnItem = MenuItemHelper(returnItem, itemEle);
                                    break;
                                case StripType.Status:
                                case StripType.ToolBar:
                                    returnItem = ToolStripItemHelper(returnItem, itemEle);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        }
                    case ControlPlace.Nothing:
                    default:
                        Debug.Fail("开发期错误: 定义了无效的控件放置位置");
                        break;
                }//switch
                switch ( itemEle.Fucation ) {
                    case "isDelete":
                        ((ToolStripMenuItem)returnItem).Checked = false;
                        break;
                    case "isNotDelete":
                        ((ToolStripMenuItem)returnItem).Checked = true;
                        break;
                    case "isPublish":
                        ((ToolStripMenuItem)returnItem).Checked = true;
                        break;
                    case "isNotPublish":
                        ((ToolStripMenuItem)returnItem).Checked = true;
                        break;
                    case "isModify":
                        ((ToolStripMenuItem)returnItem).Checked = true;
                        break;
                    case "isNotModify":
                        ((ToolStripMenuItem)returnItem).Checked = true;
                        break;
                    case "isAdvertisement":
                        ((ToolStripMenuItem)returnItem).Checked = true;
                        break;
                    case "isNotAdadvertisement":
                        ((ToolStripMenuItem)returnItem).Checked = true;
                        break;
                    default:
                        break;
                }
            }//if
            return returnItem;
        }

        private ToolStripItem ToolStripItemHelper(ToolStripItem returnItem, ToolStripItemXmlElement itemEle)
        {
            switch ( itemEle.ToolBarControlType ) {
                case ToolStripItemType.Button: {
                        ToolStripButton item = new ToolStripButton(this.ToSimpleText(itemEle.TextString));
                        item.Name = itemEle.Fucation;
                        item.DisplayStyle = itemEle.ToolBarDisplayStyle;
                        returnItem = item;
                        break;
                    }
                case ToolStripItemType.Label: {
                        ToolStripLabel item = new ToolStripLabel(itemEle.TextString);
                        item.Name = itemEle.Fucation;
                        item.DisplayStyle = itemEle.ToolBarDisplayStyle;
                        returnItem = item;
                        break;
                    }
                case ToolStripItemType.SplitButton: {
                        ToolStripSplitButton item = new ToolStripSplitButton(itemEle.TextString);
                        BuildToolStripItemSubMethod(itemEle, item, DropControlType.SplitButton);
                        item.Name = itemEle.Fucation;
                        item.DisplayStyle = itemEle.ToolBarDisplayStyle;
                        returnItem = item;
                        break;
                    }
                case ToolStripItemType.DropDownButton: {
                        ToolStripDropDownButton item = new ToolStripDropDownButton(itemEle.TextString);
                        BuildToolStripItemSubMethod(itemEle, item, DropControlType.DropDownButton);
                        item.Name = itemEle.Fucation;
                        item.DisplayStyle = itemEle.ToolBarDisplayStyle;
                        returnItem = item;
                        break;
                    }
                case ToolStripItemType.ComboBox: {
                        ToolStripComboBox item = new ToolStripComboBox();
                        item.Name = itemEle.Fucation;
                        item.DisplayStyle = itemEle.ToolBarDisplayStyle;
                        this.FillComboBoxValue(item, itemEle);
                        returnItem = item;
                        break;
                    }
                case ToolStripItemType.TextBox: {
                        ToolStripTextBox item = new ToolStripTextBox();
                        item.Name = itemEle.Fucation;
                        item.DisplayStyle = itemEle.ToolBarDisplayStyle;
                        returnItem = item;
                        break;
                    }
                case ToolStripItemType.ProgressBar: {
                        ToolStripProgressBar item = new ToolStripProgressBar();
                        item.Name = itemEle.Fucation;
                        item.DisplayStyle = itemEle.ToolBarDisplayStyle;
                        returnItem = item;
                        break;
                    }
                case ToolStripItemType.MenuItem: {
                        ToolStripMenuItem item = new ToolStripMenuItem(itemEle.TextString);
                        BuildToolStripItemSubMethod(itemEle, item, DropControlType.MenuItem);
                        item.Name = itemEle.Fucation;
                        item.DisplayStyle = itemEle.MenuDisplayStyle;
                        returnItem = item;
                        break;
                    }
                case ToolStripItemType.StatusLabel: {
                        ToolStripStatusLabel item = new ToolStripStatusLabel(itemEle.TextString);
                        item.Name = itemEle.Fucation;
                        item.DisplayStyle = itemEle.ToolBarDisplayStyle;
                        returnItem = item;
                        break;
                    }
                case ToolStripItemType.Separator: {
                        ToolStripSeparator item = new ToolStripSeparator();
                        returnItem = item;
                        break;
                    }
            }
            return returnItem;
        }

        private ToolStripItem MenuItemHelper(ToolStripItem returnItem, ToolStripItemXmlElement itemEle)
        {
            switch ( itemEle.MenuControlType ) {
                case MenuItemType.ComboBox: {
                        ToolStripComboBox item = new ToolStripComboBox();
                        item.Name = itemEle.Fucation;
                        item.DisplayStyle = itemEle.MenuDisplayStyle;
                        returnItem = item;
                        break;
                    }
                case MenuItemType.TextBox: {
                        ToolStripTextBox item = new ToolStripTextBox();
                        item.Name = itemEle.Fucation;
                        item.DisplayStyle = itemEle.MenuDisplayStyle;
                        returnItem = item;
                        break;
                    }
                case MenuItemType.MenuItem: {
                        ToolStripMenuItem item = new ToolStripMenuItem(itemEle.TextString);
                        BuildToolStripItemSubMethod(itemEle, item, DropControlType.MenuItem);
                        item.Name = itemEle.Fucation;
                        item.DisplayStyle = itemEle.MenuDisplayStyle;
                        returnItem = item;
                        break;
                    }
                case MenuItemType.Separator: {
                        ToolStripSeparator item = new ToolStripSeparator();
                        returnItem = item;
                        break;
                    }
            }
            return returnItem;
        }

        private void FillComboBoxValue(ToolStripComboBox item, ToolStripItemXmlElement itemEle)
        {
            foreach ( XmlNode node in itemEle.ChildNodes ) {
                if ( node.NodeType != XmlNodeType.Element ) {
                    continue;
                }
                ItemValueXmlElement ele = (ItemValueXmlElement)node;
                if ( !string.IsNullOrEmpty(ele.InnerText) ) {
                    item.Items.Add(ele.InnerText);
                    if ( ele.IsDefault ) {
                        item.Text = ele.InnerText;
                    }
                }
            }
        }

        /// <summary>
        /// 递归子程序（BuildToolStripItem）
        /// </summary>
        private void BuildToolStripItemSubMethod(ToolStripItemXmlElement itemEle, ToolStripItem item, DropControlType controlType)
        {
            ToolStripItem subItem = null;
            if ( itemEle.HasChildNodes ) {
                foreach ( XmlNode node in itemEle.ChildNodes ) {
                    if ( node.NodeType != XmlNodeType.Element ) {
                        continue;
                    }
                    ToolStripItemXmlElement subItemEle = (ToolStripItemXmlElement)node;
                    if ( subItemEle.Name.ToUpper() != "TOOLSTRIPITEM" ) {
                        continue;
                    }
                    subItem = BuildToolStripItem(subItemEle);
                    switch ( controlType ) {
                        case DropControlType.MenuItem:
                            ((ToolStripMenuItem)item).DropDownItems.Add(subItem);
                            break;
                        case DropControlType.SplitButton:
                            ((ToolStripSplitButton)item).DropDownItems.Add(subItem);
                            break;
                        case DropControlType.DropDownButton:
                            ((ToolStripDropDownButton)item).DropDownItems.Add(subItem);
                            break;
                        default:
                            break;
                    }

                }
            }
        }

        private string ToSimpleText(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            int i = str.IndexOf("(");
            if ( i >= 0 ) {
                str = str.Remove(i);
            }
            return str;
        }

    }
}
