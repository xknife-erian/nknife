using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// ������Ϊ�˵��Ľ���״̬����
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MenuEnabledAttribute : Attribute
    {
        string _menuKey;
        /// <summary>
        /// �˵����Key����ʽ�磺MainMenu.file.save
        /// </summary>
        public string MenuKey
        {
            get { return _menuKey; }
        }
        private bool _isMustProjectOpened;
        /// <summary>
        /// �Ƿ�������Ŀ�˲˵��ſ���
        /// </summary>
        public bool IsMustProjectOpened
        {
            get { return _isMustProjectOpened; }
        }
        private bool _isMustActiveDocument;
        /// <summary>
        /// �Ƿ������ActiveDocument�˲˵��ſ���
        /// </summary>
        public bool IsMustActiveDocument
        {
            get { return _isMustActiveDocument; }
        }

        public MenuEnabledAttribute(string menuKey,bool isMustProjectOpened,bool isMustActiveDocument)
        {
            this._menuKey = menuKey;
            this._isMustProjectOpened = isMustProjectOpened;
            this._isMustActiveDocument = isMustActiveDocument;
        }
    }

    public static class MenuStateManager
    {
        class PropertyData
        {
            private string _menuKey;
            public string MenuKey
            {
                get { return _menuKey; }
            }
            private PropertyInfo _propInfo;
            public PropertyInfo PropInfo
            {
                get { return _propInfo; }
            }
            private MenuEnabledAttribute _menuEnabledAtt;
            public MenuEnabledAttribute MenuEnabledAtt
            {
                get { return _menuEnabledAtt; }
            }

            public PropertyData(string menuKey,PropertyInfo propInfo,MenuEnabledAttribute att)
            {
                _menuKey = menuKey;
                _propInfo = propInfo;
                _menuEnabledAtt = att;
            }
        }
        //static List<KeyValuePair<PropertyInfo, string>> _propertyInfoList = new List<KeyValuePair<PropertyInfo, string>>();
        static Dictionary<string, PropertyData> _dicPropertyInfo = new Dictionary<string, PropertyData>();
        public static bool GetAndSetMenuEnabled(string keyId)
        {
            PropertyData info;
            if (!_dicPropertyInfo.TryGetValue(keyId, out info))
            {
                return true;
            }

            try
            {
                ///��ȡֵ��������Ӧ�˵���Enabled����
                string enabled = (string)info.PropInfo.GetValue(null, null);
                MenuStripManager._allMenuItem[keyId].Enabled = bool.Parse(enabled);
                return bool.Parse(enabled);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Service.Exception.ShowException(ex);
                }
                else
                {
                    Service.Exception.ShowException(ex);
                }
                return false;
            }
        }
        public static void Initialize()
        {
            ///���ҵ�ǰ�������Property
            PropertyInfo[] properties = typeof(MenuStateManager).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty);

            ///���������ҵ�������Property
            foreach (PropertyInfo info in properties)
            {
                ///��ȡMenuEnabledAttribute����
                object[] attributes = info.GetCustomAttributes(typeof(MenuEnabledAttribute), false);
                if (attributes.Length > 0)
                {
                    ///����MenuKey��Property������ӽ�List
                    MenuEnabledAttribute menuEnabled = (MenuEnabledAttribute)attributes[0];
                    //_propertyInfoList.Add(new KeyValuePair<PropertyInfo, string>(info, menuEnabled.MenuKey));
                    _dicPropertyInfo.Add(menuEnabled.MenuKey, new PropertyData(menuEnabled.MenuKey,info,(MenuEnabledAttribute)attributes[0]));
                }
            }

            ///����¼�����������ʱˢ�²˵�״̬
            System.Windows.Forms.Application.Idle += new EventHandler(Application_Idle);
        }

        static void Application_Idle(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string,ToolStripMenuItem> keyvalue in MenuStripManager._peakMenuItem)
            {
                if (keyvalue.Value is MyMenuItem)
                {
                    if (((MyMenuItem)keyvalue.Value).IsMustOpenSite)
                    {
                        foreach (ToolStripItem menuItem in keyvalue.Value.DropDownItems)
                        {
                            menuItem.Enabled = Service.Project.IsProjectOpened;
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, PropertyData> keyvalue in _dicPropertyInfo)
            {
                ///�ȼ��˲˵����Ƿ�������Ŀ
                if (keyvalue.Value.MenuEnabledAtt.IsMustProjectOpened && !Service.Project.IsProjectOpened)
                {
                    ToolStripMenuItem menuItem = MenuStripManager._allMenuItem[keyvalue.Key];
                    SetMenuItemEnabled(menuItem, false);
                }
                ///�ټ��˲˵����Ƿ����Ҫ��һ���򿪴���
                else if (keyvalue.Value.MenuEnabledAtt.IsMustActiveDocument && WorkbenchForm.MainForm.ActiveView == null)
                {
                    ToolStripMenuItem menuItem = MenuStripManager._allMenuItem[keyvalue.Key];
                    SetMenuItemEnabled(menuItem, false);
                }
                else
                {
                    try
                    {
                        ///��ȡֵ��������Ӧ�˵���Enabled����
                        string enabled = (string)keyvalue.Value.PropInfo.GetValue(null, null);
                        ToolStripMenuItem menuItem = MenuStripManager._allMenuItem[keyvalue.Key];
                        SetMenuItemEnabled(menuItem, bool.Parse(enabled));
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            Service.Exception.ShowException(ex.InnerException);
                        }
                        else
                        {
                            Service.Exception.ShowException(ex);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ����Enabled״̬�����Ǵ˲˵������������ôֻ�����������Enabled
        /// </summary>
        static private void SetMenuItemEnabled(ToolStripMenuItem menuItem,bool value)
        {
            if (menuItem.HasDropDownItems)
            {
                foreach (ToolStripMenuItem item in menuItem.DropDownItems)
                {
                    SetMenuItemEnabled(item, value);
                }
            }
            else
            {
                menuItem.Enabled = value;
            }
        }

        //�˵���״̬�ı�
        [MenuEnabled("MainMenu.file.newFile", true, false)]
        public static string NewFileEnabled
        {
            get { return bool.TrueString; }
        }
        [MenuEnabled("MainMenu.file.close",true,true)]
        public static string CloseEnabled
        {
            get { return bool.TrueString; }
        }
        [MenuEnabled("MainMenu.file.closeProj",true,false)]
        public static string CloseProjEnabled
        {
            get { return Service.Project.IsProjectOpened.ToString(); }
        }
        [MenuEnabled("MainMenu.file.save",true,true)]
        public static string SaveEnabled
        {
            get
            {
                if (WorkbenchForm.MainForm.ActiveView is BaseEditViewForm)
                {
                    return ((BaseEditViewForm)WorkbenchForm.MainForm.ActiveView).IsModified.ToString();
                }
                return bool.FalseString;
            }
        }
        [MenuEnabled("MainMenu.file.saveAll", true, true)]
        public static string SaveAllEnabled
        {
            get { return bool.TrueString; }
        }
        [MenuEnabled("MainMenu.file.pub", true, false)]
        public static string PubEnabled
        {
            get { return bool.TrueString; }
        }
        [MenuEnabled("MainMenu.file.opennet", true, false)]
        public static string OpenNet
        {
            get
            {
                if (!Service.User.IsLogined)
                {
                    return bool.FalseString;
                }
                if (Service.Sdsite.CurrentDocument == null || Service.Sdsite.CurrentDocument.RootChannel == null)
                {
                    return bool.FalseString;
                }
                if (!Service.Sdsite.CurrentDocument.RootChannel.IsAlreadyPublished)
                {
                    return bool.FalseString;
                }
                return bool.TrueString;
            }
        }
        [MenuEnabled("MainMenu.file.preview", true, true)]
        public static string PreviewEnabled
        {
            get
            {
                if (WorkbenchForm.MainForm.ActiveView.WorkDocumentType == WorkDocumentType.HtmlDesigner
                    || WorkbenchForm.MainForm.ActiveView.WorkDocumentType == WorkDocumentType.Edit
                    || WorkbenchForm.MainForm.ActiveView.WorkDocumentType == WorkDocumentType.TmpltDesigner
                    || WorkbenchForm.MainForm.ActiveView.WorkDocumentType == WorkDocumentType.HomePage)
                {
                    return bool.TrueString;
                }

                return bool.FalseString;
            }
        }
        //[MenuEnabled("MainMenu.file.print", true, true)]
        //public static string PrintEnabled
        //{
        //    //todo:δ��
        //    get { return bool.FalseString; }
        //}
        //[MenuEnabled("MainMenu.file.batchBuild", true, true)]
        //public static string BatchBuildEnabled
        //{
        //    //todo:δ��
        //    get { return bool.FalseString; }
        //}
        //[MenuEnabled("MainMenu.file.fromDataBuild", true, true)]
        //public static string FromDataBuildEnabled
        //{
        //    //todo:δ��
        //    get { return bool.FalseString; }
        //}
        //[MenuEnabled("MainMenu.file.fromThemeBuild", true, true)]
        //public static string FromThemeBuildEnabled
        //{
        //    //todo:δ��
        //    get { return bool.FalseString; }
        //}

        [MenuEnabled("MainMenu.edit.undo",false,true)]
        public static string UndoEnabled
        {
            get
            {
                if (WorkbenchForm.MainForm.MainDockPanel.ActiveContent is BaseViewForm)
                {
                    return WorkbenchForm.MainForm.ActiveView.CanUndo().ToString();
                }
                return bool.FalseString;
            }
        }
        [MenuEnabled("MainMenu.edit.redo",false,true)]
        public static string RedoEnabled
        {
            get
            {
                if (WorkbenchForm.MainForm.MainDockPanel.ActiveContent is BaseViewForm)
                {
                    return WorkbenchForm.MainForm.ActiveView.CanRedo().ToString();
                }
                return bool.FalseString;
            }
        }
        [MenuEnabled("MainMenu.edit.cut", false, true)]
        public static string CutEnabled
        {
            get
            {
                if (WorkbenchForm.MainForm.MainDockPanel.ActiveContent is BaseViewForm)
                {
                    return WorkbenchForm.MainForm.ActiveView.CanCut().ToString();
                }
                return bool.FalseString;
            }
        }
        [MenuEnabled("MainMenu.edit.copy", false, true)]
        public static string CopyEnabled
        {
            get
            {
                if (WorkbenchForm.MainForm.MainDockPanel.ActiveContent is BaseViewForm)
                {
                    return WorkbenchForm.MainForm.ActiveView.CanCopy().ToString();
                }
                return bool.FalseString;
            }
        }
        [MenuEnabled("MainMenu.edit.paste", false, true)]
        public static string PasteEnabled
        {
            get
            {
                if (WorkbenchForm.MainForm.MainDockPanel.ActiveContent is BaseViewForm)
                {
                    return WorkbenchForm.MainForm.ActiveView.CanPaste().ToString();
                }
                return bool.FalseString;
            }
        }
        [MenuEnabled("MainMenu.edit.delete", false, true)]
        public static string DeleteEnabled
        {
            get
            {
                if (WorkbenchForm.MainForm.MainDockPanel.ActiveContent is BaseViewForm)
                {
                    return WorkbenchForm.MainForm.ActiveView.CanDelete().ToString();
                }
                return bool.FalseString;
            }
        }
        [MenuEnabled("MainMenu.edit.selectAll", false, true)]
        public static string SelectAllEnabled
        {
            get
            {
                if (WorkbenchForm.MainForm.MainDockPanel.ActiveContent is BaseViewForm)
                {
                    return WorkbenchForm.MainForm.ActiveView.CanSelectAll().ToString();
                }
                return bool.FalseString;
            }
        }
        [MenuEnabled("MainMenu.edit.find", true, false)]
        public static string Find
        {
            get
            {
                return bool.TrueString;
            }
        }
        [MenuEnabled("MainMenu.edit.replace", true, false)]
        public static string Replace
        {
            get
            {
                return bool.TrueString;
            }
        }
        [MenuEnabled("MainMenu.edit.findNext", true, false)]
        public static string FindNext
        {
            get
            {
                return bool.TrueString;
            }
        }
        //[MenuEnabled("MainMenu.edit.find", true, false)]
        //public static string FindEnabled
        //{
        //    get
        //    {
        //        return bool.TrueString;
        //    }
        //}
        //[MenuEnabled("MainMenu.edit.replace", true, false)]
        //public static string ReplaceEnabled
        //{
        //    get
        //    {
        //        return bool.TrueString;
        //    }
        //}
        //[MenuEnabled("MainMenu.edit.findNext", true, false)]
        //public static string FindNextEnabled
        //{
        //    get
        //    {
        //        return bool.TrueString;
        //    }
        //}

        //[MenuEnabled("MainMenu.page.contentProperty", true, true)]
        //public static string PageProperty
        //{
        //    get
        //    {
        //        if (WorkbenchForm.MainForm.ActiveView is MdiHtmlDesignForm)
        //        {
        //            return bool.TrueString;
        //        }
        //        return bool.FalseString;
        //    }
        //}

        [MenuEnabled("MainMenu.window.closeAllWindow", false, true)]
        public static string CloseAllWindow
        {
            get
            {
                return bool.TrueString;
            }
        }
    }
}
