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
        /// ���캯��
        /// </summary>
        internal ToolStripItemXmlElement(XmlDocument doc)
            : base(string.Empty, "ToolStripItem", string.Empty, doc) { }

        #region �ؼ�����չ����

        /// <summary>
        /// ������, Control.Name
        /// </summary>
        internal string Fucation { get { return this.GetAttribute("fucation"); } }
        /// <summary>
        /// �ؼ����ı�, Control.Text
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
        /// �Ƿ��ǿ�ִ�еĹ���
        /// </summary>
        internal bool IsRun { get { return bool.Parse(this.GetAttribute("isRun")); } }
        /// <summary>
        /// ��ݼ�
        /// </summary>
        internal Keys ShortcutKey { get { return ParseShortcut(this.GetAttribute("shortcutKey")); } }
        /// <summary>
        /// �ؼ���λ�ã���:�˵��������������߶���
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
        /// �Ƿ��ǵ���ʽ�Ĺ���
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
        /// ��ȡ�ڹ������ϵĿؼ�����
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
                        throw new Exception("�����ڴ��󣺶����˴���Ŀؼ�����");
                }
            }
        } 
        /// <summary>
        /// ��ȡ�ڲ˵��ϵĿؼ�����
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
                    throw new Exception("�_�l���e�`���O�óɟoЧ�Ŀؼ����");
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
                        throw new Exception("�����ڴ��󣺶����˴���Ŀؼ�����");
                }
            }
        }
        /// <summary>
        /// Icon��·��
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
        /// �ؼ���ѡ��״̬
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
                        throw new Exception("�����ڴ��󣺲˵������ļ������޷������Ŀؼ�״̬���ͣ�CheckState��");
                }
            } 
        }
        /// <summary>
        /// �ؼ�����ʾ��ʽ
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
        /// �ؼ�����ʾ��ʽ
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
        /// ���ַ�������Ϊ��ݼ�
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
