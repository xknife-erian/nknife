using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    internal class ToolStripItemXmlElement : XmlElement
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        internal ToolStripItemXmlElement(XmlDocument doc)
            : base(string.Empty, "ToolStripItem", string.Empty, doc) { }

        #region 控件的扩展属性

        /// <summary>
        /// 功能名, Control.Name
        /// </summary>
        internal string Fucation { get { return this.GetAttribute("fucation"); } }
        /// <summary>
        /// 控件的文本, Control.Text
        /// </summary>
        internal string TextString
        {
            get
            {
                string textString = this.GetAttribute("text");//(&F)
                if (!string.IsNullOrEmpty(this.GetAttribute("hotkey")))
                {
                    textString += "(&" + this.GetAttribute("hotkey").ToUpper() + ")";
                } 
                if (this.IsPop)
                {
                    textString += "...";
                }
                return textString;
            }
        }
        /// <summary>
        /// 是否是可执行的功能
        /// </summary>
        internal bool IsRun { get { return bool.Parse(this.GetAttribute("isRun")); } }
        /// <summary>
        /// 快捷键
        /// </summary>
        internal Keys ShortcutKey { get { return ParseShortcut(this.GetAttribute("shortcutKey")); } }
        /// <summary>
        /// 控件的位置，如:菜单、工具栏，两者都在
        /// </summary>
        internal ControlPlace Place
        {
            get 
            {
                if (!this.HasAttribute("place"))
                {
                    return ControlPlace.Nothing;
                }
                string place = this.GetAttribute("place");
                switch (place)
                {
                    case "ToolBar|Menu":
                    case "Menu|ToolBar":
                        return ControlPlace.MenuAndToolbar;
                    case "Menu":
                        return ControlPlace.Menu;
                    case "ToolBar":
                        return ControlPlace.ToolBar;
                    case "":
                    default:
                        return ControlPlace.Nothing;
                }
            }
        }
        /// <summary>
        /// 是否是弹出式的功能
        /// </summary>
        internal bool IsPop
        { 
            get 
            {
                if (this.HasAttribute("isPop"))
                {
                    if (!string.IsNullOrEmpty(this.GetAttribute("isPop")))
                    {
                        return bool.Parse(this.GetAttribute("isPop"));
                    }
                }
                return false;
            } 
        }
        /// <summary>
        /// 获取在工具栏上的控件类型
        /// </summary>
        internal ToolStripItemType ToolBarControlType
        {
            get
            {
                string myValue = this.GetAttribute("toolBarControlType");
                switch (myValue)
                {
                    case "Button":
                        return ToolStripItemType.Button;
                    case "ComboBox":
                        return ToolStripItemType.ComboBox;
                    case "DropDownButton":
                        return ToolStripItemType.DropDownButton;
                    case "Label":
                        return ToolStripItemType.Label;
                    case "ProgressBar":
                        return ToolStripItemType.ProgressBar;
                    case "SplitButton":
                        return ToolStripItemType.SplitButton;
                    case "StatusLabel":
                        return ToolStripItemType.StatusLabel;
                    case "TextBox":
                        return ToolStripItemType.TextBox;
                    case "MenuItem":
                        return ToolStripItemType.MenuItem;
                    case "Separator":
                        return ToolStripItemType.Separator;
                    default:
                        throw new Exception("开发期错误：定义了错误的控件类型");
                }
            }
        } 
        /// <summary>
        /// 获取在菜单上的控件类型
        /// </summary>
        internal MenuItemType MenuControlType
        {
            set 
            {
                if (value == MenuItemType.MenuItem)
                {
                    if (this.Place == ControlPlace.Menu)
                    {
                        if (this.GetAttribute("menuControlType") != "Separator")
                        {
                            this.SetAttribute("menuControlType", "MenuItem");
                        }
                    }
                }
                else
                {
                    throw new Exception("開發期錯誤：設置成無效的控件類型");
                }
            }
            get
            {
                string myValue = this.GetAttribute("menuControlType");
                switch (myValue)
                {
                    case "ComboBox":
                        return MenuItemType.ComboBox;
                    case "MenuItem":
                        return MenuItemType.MenuItem;
                    case "TextBox":
                        return MenuItemType.TextBox;
                    case "Separator":
                        return MenuItemType.Separator;
                    default:
                        throw new Exception("开发期错误：定义了错误的控件类型");
                }
            }
        }
        /// <summary>
        /// Icon的路径
        /// </summary>
        internal Image IconForElement 
        { 
            get 
            {
                string icoValue = this.GetAttribute("hasIcon");
                if (icoValue.ToUpper() != "FALSE")
                {
                    //Bitmap bitmap = new Bitmap(icoValue);
                    return null;
                }
                return null; 
            } 
        }
        /// <summary>
        /// 控件的选中状态
        /// </summary>
        internal ControlCheckState MyCheckState 
        { 
            get 
            {
                switch (this.GetAttribute("CheckState").ToUpper())
                {
                    case "NULL":
                        return ControlCheckState.NoSetting;
                    case "TRUE":
                        return ControlCheckState.isChecked;
                    case "FALSE":
                        return ControlCheckState.isNotChecked;
                    default:
                        throw new Exception("开发期错误：菜单配置文件中有无法解析的控件状态类型（CheckState）");
                }
            } 
        }
        /// <summary>
        /// 控件的显示样式
        /// </summary>
        internal ToolStripItemDisplayStyle ToolBarDisplayStyle
        {
            get
            {
                switch (this.GetAttribute("inToolBarStyle").ToUpper())
                {
                    case "IMAGE":
                        return ToolStripItemDisplayStyle.Image;
                    case "IMAGE|TEXT":
                        return ToolStripItemDisplayStyle.ImageAndText;
                    case "TEXT":
                        return ToolStripItemDisplayStyle.Text;
                    case "":
                    case "NOTHING":
                    default:
                        return ToolStripItemDisplayStyle.None;
                }
            }
        }
        /// <summary>
        /// 控件的显示样式
        /// </summary>
        internal ToolStripItemDisplayStyle MenuDisplayStyle
        {
            get
            {
                switch (this.GetAttribute("inMenuStyle").ToUpper())
                {
                    case "IMAGE":
                        return ToolStripItemDisplayStyle.Image;
                    case "IMAGE|TEXT":
                        return ToolStripItemDisplayStyle.ImageAndText;
                    case "TEXT":
                        return ToolStripItemDisplayStyle.Text;
                    case "":
                    case "NOTHING":
                    default:
                        return ToolStripItemDisplayStyle.None;
                }
            }
        }

        #endregion

        /// <summary>
        /// 将字符串解析为快捷键
        /// </summary>
        private Keys ParseShortcut(string strShortcut)
        {
            if (string.IsNullOrEmpty(strShortcut))
            {
                return Keys.None;
            }

            Keys myKeys = Keys.None;
            string[] strs = strShortcut.Split('+');
            foreach (string str in strs)
            {
                switch (str.ToUpper())
                {
                    case "DEL":
                    case "DELETE":
                        myKeys |= Keys.Delete;
                        break;
                    case "CTRL":
                        myKeys |= Keys.Control;
                        break;
                    default:
                        try
                        {
                            myKeys |= (Keys)Enum.Parse(typeof(Keys), str);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.ToString());
                        }
                        break;
                }
            }
            return myKeys;
        }
    }
}
