using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        /// <summary>
        /// 与工作台相关的一些属性和方法
        /// </summary>
        static public class Workbench
        {
            static private bool _isInited;
            static private OpenWorkDocumentHandle _openHandle;
            static private NavigationUrl _navigationUrl;
            static private Action _reloadTree;
            static private Action _showDialogForCreateProject;
            static private Action _showDialogForOpenProject;
            static private Action<string> _gotoTree;
            static private Action<Form> _activateForm;

            static private Form _mainForm;
            static public Form MainForm
            {
                get { return _mainForm; }
            }

            static Dictionary<string, Form> _dicIdAndView = new Dictionary<string, Form>();

            static public void Initialize(Form mainForm, OpenWorkDocumentHandle openHandle,
                NavigationUrl navigationUrl, Action reloadTree, Action showDialogForCreateProject,
                Action showDialogForOpenProject, Action<string> gotoTree, Action<Form> activateForm)
            {
                if (_isInited)
                {
                    return;
                }

                _mainForm = mainForm;
                _openHandle = openHandle;
                _navigationUrl = navigationUrl;
                _reloadTree = reloadTree;
                _showDialogForCreateProject = showDialogForCreateProject;
                _showDialogForOpenProject = showDialogForOpenProject;
                _gotoTree = gotoTree;
                _activateForm = activateForm;

                _isInited = true;
            }

            /// <summary>
            /// 定位到树中
            /// </summary>
            static public void GotoTree(string workDocumentId)
            {
                _gotoTree(workDocumentId);
            }
            /// <summary>
            /// 定位到树中
            /// </summary>
            static public void GotoTree(Form form)
            {
                FormData formData = form.Tag as FormData;
                if (formData != null)
                {
                    if (formData.WorkDocumentType == WorkDocumentType.SnipDesigner)
                    {
                        GotoTree(formData.OwnerId);
                    }
                    else
                    {
                        GotoTree(formData.Id);
                    }
                }
            }

            /// <summary>
            /// 重新加载右侧的树
            /// </summary>
            static public void ReloadTree()
            {
                Debug.Assert(_isInited);
                Debug.Assert(_reloadTree != null);

                _reloadTree();
            }

            static public void ShowDialogForOpenProject()
            {
                _showDialogForOpenProject();
            }
            static public void ShowDialogForCreateProject()
            {
                _showDialogForCreateProject();
            }

            static public WorkDocumentType GetWorkDocumentType(SimpleExIndexXmlElement ele)
            {
                switch (ele.DataType)
                {
                    case DataType.Tmplt:
                        return WorkDocumentType.TmpltDesigner;
                    case DataType.Page:
                        {
                            switch (((PageSimpleExXmlElement)ele).PageType)
                            {
                                case PageType.General:
                                    return WorkDocumentType.HtmlDesigner;
                                case PageType.Product:
                                case PageType.Project:
                                case PageType.InviteBidding:
                                case PageType.Knowledge:
                                case PageType.Hr:
                                    return WorkDocumentType.Edit;
                                case PageType.Home:
                                    return WorkDocumentType.HomePage;
                                default:
                                    Debug.Fail("");
                                    break;
                            }
                            return WorkDocumentType.HtmlDesigner;
                        }
                }

                return WorkDocumentType.None;
            }

            #region 关于Workbench子窗口的一些方法和属性

            static private CloseAllWindowData _closeAllWindowData = new CloseAllWindowData();
            /// <summary>
            /// 在这里记录是否关闭整个项目以及关闭中的一些选项
            /// </summary>
            static public CloseAllWindowData CloseAllWindowData
            {
                get { return _closeAllWindowData; }
            }

            static private string _activeId;
            /// <summary>
            /// 工作区当前活动窗口的Id
            /// </summary>
            static public string ActiveId
            {
                get { return _activeId; }
            }

            static private WorkDocumentType _activeWorkDocumentType;
            /// <summary>
            /// 工作区当前活动窗口的类型
            /// </summary>
            static public WorkDocumentType ActiveWorkDocumentType
            {
                get { return _activeWorkDocumentType; }
            }

            /// <summary>
            /// 工作区当前活动窗口
            /// </summary>
            static public Form ActiveForm { get; private set; }

            /// <summary>
            /// 在工作区打开一个文档。如果已经打开，则激活
            /// </summary>
            static public Form OpenWorkDocument(WorkDocumentType type, string id)
            {
                return OpenWorkDocument(type, id, "");
            }
            /// <summary>
            /// 在工作区打开一个文档。如果已经打开，则激活
            /// </summary>
            static public Form OpenWorkDocument(WorkDocumentType type, string id, string ownerId)
            {
                Debug.Assert(type != WorkDocumentType.None);
                Debug.Assert(!string.IsNullOrEmpty(id));
                Debug.Assert(_isInited);
                Debug.Assert(_openHandle != null);

                string keyId = GetKey(id, type);
                Form form = null;
                if (_dicIdAndView.TryGetValue(keyId, out form))
                {
                    _activateForm(form);
                }
                else
                {
                    form = _openHandle(type, id, ownerId);
                    FormData formData = new FormData(type, id, ownerId, form);
                    form.Tag = formData;

                    _dicIdAndView.Add(keyId, form);
                    form.FormClosed += delegate
                    {
                        _dicIdAndView.Remove(keyId);
                    };

                    ///触发WorkDocumentNewOpened事件
                    OnWorkDocumentNewOpened(new EventArgs<FormData>(formData));
                }
                return form;
            }
            static private string GetKey(string id, WorkDocumentType type)
            {
                Debug.Assert(!string.IsNullOrEmpty(id));

                return id + "&" + type;
            }
            static public Form GetWorkDocumentById(string id, WorkDocumentType type)
            {
                Debug.Assert(!string.IsNullOrEmpty(id));

                Form form = null;
                string key = GetKey(id, type);
                _dicIdAndView.TryGetValue(key, out form);
                return form;
            }
            /// <summary>
            /// 返回目标窗口是否在已经打开窗口的结果,在返回TRUE 反之FALSE
            /// add by fenggy 2008-06-16
            /// </summary>
            /// <param name="id"></param>
            /// <param name="ownerId"></param>
            /// <returns></returns>
            static public bool GetResultKeyID(WorkDocumentType type, string id)
            {
                string keyId = GetKey(id, type);
                return _dicIdAndView.ContainsKey(keyId);
            }
            /// <summary>
            /// 关闭一个窗口
            /// </summary>
            static public Form CloseWorkDocument(string id, WorkDocumentType type)
            {
                Debug.Assert(!string.IsNullOrEmpty(id));

                if (string.IsNullOrEmpty(id))
                {
                    return null;
                }

                Form form;
                string key = GetKey(id, type);
                if (_dicIdAndView.TryGetValue(key, out form))
                {
                    form.Close();
                }
                return form;
            }
            /// <summary>
            /// 关闭一个窗口且没有保存提示
            /// </summary>
            static public Form CloseWorkDocumentWithoutSave(string id, WorkDocumentType type)
            {
                Debug.Assert(!string.IsNullOrEmpty(id));

                if (string.IsNullOrEmpty(id))
                {
                    return null;
                }

                Form form;
                string key = GetKey(id, type);
                if (_dicIdAndView.TryGetValue(key, out form))
                {
                    ///利用关闭所有窗口时的选项:"全否"
                    try
                    {
                        CloseAllWindowData.BeginCloseAllWindow();
                        CloseAllWindowData.Option = CloseAllWindowOption.AllNoSave;
                        form.Close();
                    }
                    finally
                    {
                        CloseAllWindowData.EndCloseAllWindow();
                    }
                }
                return form;
            }

            static public Form ActiveWorkDocument(string id, WorkDocumentType type)
            {
                Debug.Assert(!string.IsNullOrEmpty(id));

                if (string.IsNullOrEmpty(id))
                {
                    return null;
                }

                return ActiveWorkDocument(GetKey(id,type));
            }

            static public Form ActiveWorkDocument(string idAndType)
            {
                Debug.Assert(!string.IsNullOrEmpty(idAndType));

                Form form;
                if (_dicIdAndView.TryGetValue(idAndType, out form))
                {
                    _activateForm(form);
                }
                return form;
            }

            /// <summary>
            /// 导航到一个Url
            /// </summary>
            static public Form NavigationUrl(string url)
            {
                Debug.Assert(!string.IsNullOrEmpty(url));
                Debug.Assert(_isInited);
                Debug.Assert(_navigationUrl != null);

                Form form = _navigationUrl(url);
                form.Tag = new FormData(WorkDocumentType.WebBrowser, url, null, form);
                return form;
            }

            /// <summary>
            /// WorkbenchForm的子窗体的当前窗口改变的事件
            /// </summary>
            static public event EventHandler<EventArgs<Form>> ActiveWorkDocumentChanged;

            /// <summary>
            /// 工作区文档窗口保存后的事件
            /// </summary>
            static public event EventHandler<WorkDocumentEventArgs> WorkDocumentSaved;

            /// <summary>
            /// 一种工作区窗口的相应项被添加后的事件
            /// </summary>
            static public event EventHandler<WorkDocumentEventArgs> WorkDocumentAdded;

            static public event EventHandler<WorkDocumentEventArgs> WorkDocumentDeleted;

            #endregion

            #region internal的东西，由InternalService中转

            /// <summary>
            /// 供内部调用，不应该手动调用。
            /// </summary>
            static internal void OnActiveWorkDocumentChanged(EventArgs<Form> e)
            {
                if (ActiveWorkDocumentChanged != null)
                {
                    ActiveWorkDocumentChanged(null, e);
                }
            }

            /// <summary>
            /// 供内部调用，不应该手动调用。
            /// </summary>
            static internal void OnWorkDocumentSaved(WorkDocumentEventArgs e)
            {
                if (WorkDocumentSaved != null)
                {
                    WorkDocumentSaved(null, e);
                }
            }

            /// <summary>
            /// 供内部调用，不应该手动调用。
            /// </summary>
            static internal void OnWorkDocumentAdded(WorkDocumentEventArgs e)
            {
                if (WorkDocumentAdded != null)
                {
                    WorkDocumentAdded(null, e);
                }
            }

            static internal void OnWorkDocumentDeleted(WorkDocumentEventArgs e)
            {
                if (WorkDocumentDeleted != null)
                {
                    WorkDocumentDeleted(null, e);
                }
            }

            /// <summary>
            /// 不建议调用，仅供内部调用
            /// </summary>
            static internal void SetActiveWorkDocument(WorkDocumentType type, string id,Form activeForm)
            {
                _activeWorkDocumentType = type;
                _activeId = id;
                ActiveForm = activeForm;
            }

            #endregion

            #region Ctrl+Tab键的导航

            static private LinkedList<Form> _formList = new LinkedList<Form>();
            static public LinkedList<Form> FormList
            {
                get { return _formList; }
            }
            static public void AddForm(Form form)
            {
                _formList.AddFirst(form);
            }
            static public void ToFrontForm(Form form)
            {
                LinkedListNode<Form> formNode = _formList.Find(form);
                if (formNode != null)
                {
                    _formList.Remove(formNode);
                    _formList.AddFirst(form);
                }
            }
            static public void RemoveForm(Form form)
            {
                _formList.Remove(form);
            }
            static public void ClearForm()
            {
                _formList.Clear();
            }
            static public void SaveFormListToXml(XmlElement ele)
            {
                ///先清空
                if (ele == null)
                {
                    return;
                }
                ele.RemoveAll();

                ///当有活动文档(ActiveWorkDocument)的时候，保存起来，供恢复时使用
                if (ActiveWorkDocumentType != WorkDocumentType.None)
                {
                    string activeKeyId = GetKey(ActiveId, ActiveWorkDocumentType);
                    ele.SetAttribute("defaultForm", activeKeyId);
                }

                ///遍历当前打开的窗口列表，保存需要的窗口供下次打开时使用
                foreach (Form form in FormList)
                {
                    FormData data = form.Tag as FormData;
                    if (data == null)
                    {
                        continue;
                    }
                    switch (data.WorkDocumentType)
                    {
                        case WorkDocumentType.TmpltDesigner:
                        case WorkDocumentType.HtmlDesigner:
                        case WorkDocumentType.SnipDesigner:
                            XmlElement eleForm = ele.OwnerDocument.CreateElement("formItem");
                            eleForm.SetAttribute("type", data.WorkDocumentType.ToString());
                            eleForm.SetAttribute("id", data.Id);
                            eleForm.SetAttribute("ownerId", data.OwnerId);
                            ele.AppendChild(eleForm);
                            break;
                        case WorkDocumentType.None:
                        case WorkDocumentType.WebBrowser:
                        case WorkDocumentType.Manager:
                        case WorkDocumentType.Edit:

                        case WorkDocumentType.StartupPage:
                        case WorkDocumentType.HomePage:
                        case WorkDocumentType.SiteProperty:
                            break;
                        default:
                            Debug.Fail("开发期错误。未知的窗体类型:" + data.WorkDocumentType);
                            break;
                    }
                }
            }

            /// <summary>
            /// 从XmlElement数据里载入窗体列表
            /// </summary>
            static public void LoadFormListFromXml(XmlElement ele)
            {
                try
                {
                    if (ele == null)
                    {
                        return;
                    }
                    ///snip窗体最后打开(必须在snip打开前先打开tmplt窗体)
                    List<XmlElement> snips = new List<XmlElement>();

                    foreach (XmlNode node in ele.ChildNodes)
                    {
                        if (node.NodeType == XmlNodeType.Element)
                        {
                            XmlElement eleForm = (XmlElement)node;

                            ///窗体类型
                            WorkDocumentType type = (WorkDocumentType)Enum.Parse(typeof(WorkDocumentType),
                                eleForm.GetAttribute("type"), true);

                            if (type == WorkDocumentType.SnipDesigner)
                            {
                                snips.Add(eleForm);
                            }
                            else
                            {
                                ///id和ownerId
                                string id = eleForm.GetAttribute("id");
                                string owner = eleForm.GetAttribute("ownerId");

                                OpenWorkDocument(type, id, owner);
                            }
                        }
                    }

                    ///最后处理snip
                    foreach (XmlElement snip in snips)
                    {
                        ///id和ownerId
                        string id = snip.GetAttribute("id");
                        string owner = snip.GetAttribute("ownerId");

                        OpenWorkDocument(WorkDocumentType.SnipDesigner, id, owner);
                    }

                    string defaultFormKeyId = ele.GetAttribute("defaultForm");
                    if (!string.IsNullOrEmpty(defaultFormKeyId))
                    {
                        ActiveWorkDocument(defaultFormKeyId);
                    }
                }
#if !DEBUG
                ///在设计器让此异常抛出。但在正式版则忽略此异常。
                catch (System.Exception)
                {
                }
#endif
                finally
                {
                }
            }

            static public bool CloseProjectForm()
            {
                List<Form> listWillRemove = new List<Form>();
                foreach (Form form in FormList)
                {
                    FormData data = form.Tag as FormData;
                    Debug.Assert(data != null);
                    if (data == null)
                    {
                        continue;
                    }
                    switch (data.WorkDocumentType)
                    {
                        case WorkDocumentType.TmpltDesigner:
                        case WorkDocumentType.HtmlDesigner:
                        case WorkDocumentType.Manager:
                        case WorkDocumentType.SiteProperty:
                        case WorkDocumentType.Edit:
                        case WorkDocumentType.HomePage:
                            listWillRemove.Add(form);
                            break;
                        case WorkDocumentType.SnipDesigner:
                            ///snip设计器必须在tmplt设计器之前关闭
                            listWillRemove.Insert(0, form);
                            break;
                    }
                }

                try
                {
                    CloseAllWindowData.BeginCloseAllWindow();

                    foreach (Form form in listWillRemove)
                    {
                        form.Close();
                        if (!form.IsDisposed)
                        {
                            return false;
                        }
                    }
                }
                finally
                {
                    CloseAllWindowData.EndCloseAllWindow();
                }

                return true;
            }

            static public bool CloseAllForm()
            {
                return CloseAllForm(null);
            }

            static public bool CloseAllForm(Form widthoutForm)
            {
                try
                {
                    CloseAllWindowData.BeginCloseAllWindow();

                    List<Form> list = new List<Form>();
                    foreach (Form form in FormList)
                    {
                        if (form != widthoutForm)
                        {
                            FormData data = form.Tag as FormData;
                            if (data.WorkDocumentType == WorkDocumentType.SnipDesigner)
                            {
                                ///snip设计器必须在tmplt设计器之前关闭
                                list.Insert(0, form);
                            }
                            else
                            {
                                list.Add(form);
                            }
                        }
                    }

                    foreach (Form form in list)
                    {
                        if (form != widthoutForm)
                        {
                            form.Close();
                            if (!form.IsDisposed)
                            {
                                return false;
                            }
                        }
                    }

                    return true;
                }
                finally
                {
                    CloseAllWindowData.EndCloseAllWindow();
                }
            }

            #endregion

            static public event EventHandler<EventArgs<FormData>> WorkDocumentNewOpened;
            static private void OnWorkDocumentNewOpened(EventArgs<FormData> e)
            {
                if (WorkDocumentNewOpened != null)
                {
                    WorkDocumentNewOpened(null, e);
                }
            }

            /// <summary>
            /// Tmplt的Document的健康性(即正文型模板是否有正文型页面片)的更改事件。e.Item是模板ID
            /// </summary>
            static public event EventHandler<EventArgs<string>> TmpltDocumentHealthChanged;
            static internal void OnTmpltDocumentHealthChanged(EventArgs<string> e)
            {
                if (TmpltDocumentHealthChanged != null)
                {
                    TmpltDocumentHealthChanged(null, e);
                }
            }
        }
    }

    public class WorkDocumentEventArgs : EventArgs
    {
        private WorkDocumentType _type;
        public WorkDocumentType Type
        {
            get { return _type; }
        }
        private string _id;
        public string Id
        {
            get { return _id; }
        }

        public WorkDocumentEventArgs(WorkDocumentType type, string id)
        {
            _type = type;
            _id = id;
        }
    }

    /// <summary>
    /// 每个MdiChildForm窗体绑定的数据
    /// </summary>
    public class FormData
    {
        private WorkDocumentType _workDocumentType;
        public WorkDocumentType WorkDocumentType
        {
            get { return _workDocumentType; }
        }

        private string _ownerId;
        public string OwnerId
        {
            get { return _ownerId; }
        }


        private string _id;
        public string Id
        {
            get { return _id; }
        }

        private object _tag;
        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        private Form _form;
        public Form Form
        {
            get { return _form; }
        }

        public FormData(WorkDocumentType type, string id, string ownerId, Form form)
        {
            _workDocumentType = type;
            _id = id;
            _ownerId = ownerId;
            _form = form;
        }
    }

    /// <summary>
    /// 关闭所有窗口时的数据保存
    /// </summary>
    public class CloseAllWindowData
    {
        private int _closingAllWindow;
        /// <summary>
        /// 获取或设置是否正在关闭项目
        /// </summary>
        public bool ClosingAllWindow
        {
            get { return _closingAllWindow > 0; }
        }

        public void BeginCloseAllWindow()
        {
            _closingAllWindow++;
        }
        public void EndCloseAllWindow()
        {
            _closingAllWindow--;
            if (_closingAllWindow <= 0)
            {
                _option = CloseAllWindowOption.None;
                _closingAllWindow = 0;
            }
        }

        private CloseAllWindowOption _option;
        /// <summary>
        /// 关闭项目的选项
        /// </summary>
        public CloseAllWindowOption Option
        {
            get { return _option; }
            set { _option = value; }
        }
        public CloseAllWindowData()
        {
        }
    }

    public enum CloseAllWindowOption
    {
        None = 0,
        AllSave = 1,
        AllNoSave = 2,
    }

    public delegate Form OpenWorkDocumentHandle(WorkDocumentType type, string id, string ownerId);
    public delegate Form NavigationUrl(string url);

    public enum WorkDocumentType
    {
        None = 0,
        WebBrowser = 1,
        TmpltDesigner = 2,
        HtmlDesigner = 3,
        SnipDesigner = 4,

        Manager = 5,
        Edit = 6,

        StartupPage = 15,

        HomePage = 16,
        SiteProperty= 17,
    }
}