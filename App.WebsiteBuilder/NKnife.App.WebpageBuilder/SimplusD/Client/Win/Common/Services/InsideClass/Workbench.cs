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
        /// �빤��̨��ص�һЩ���Ժͷ���
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
            /// ��λ������
            /// </summary>
            static public void GotoTree(string workDocumentId)
            {
                _gotoTree(workDocumentId);
            }
            /// <summary>
            /// ��λ������
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
            /// ���¼����Ҳ����
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

            #region ����Workbench�Ӵ��ڵ�һЩ����������

            static private CloseAllWindowData _closeAllWindowData = new CloseAllWindowData();
            /// <summary>
            /// �������¼�Ƿ�ر�������Ŀ�Լ��ر��е�һЩѡ��
            /// </summary>
            static public CloseAllWindowData CloseAllWindowData
            {
                get { return _closeAllWindowData; }
            }

            static private string _activeId;
            /// <summary>
            /// ��������ǰ����ڵ�Id
            /// </summary>
            static public string ActiveId
            {
                get { return _activeId; }
            }

            static private WorkDocumentType _activeWorkDocumentType;
            /// <summary>
            /// ��������ǰ����ڵ�����
            /// </summary>
            static public WorkDocumentType ActiveWorkDocumentType
            {
                get { return _activeWorkDocumentType; }
            }

            /// <summary>
            /// ��������ǰ�����
            /// </summary>
            static public Form ActiveForm { get; private set; }

            /// <summary>
            /// �ڹ�������һ���ĵ�������Ѿ��򿪣��򼤻�
            /// </summary>
            static public Form OpenWorkDocument(WorkDocumentType type, string id)
            {
                return OpenWorkDocument(type, id, "");
            }
            /// <summary>
            /// �ڹ�������һ���ĵ�������Ѿ��򿪣��򼤻�
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

                    ///����WorkDocumentNewOpened�¼�
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
            /// ����Ŀ�괰���Ƿ����Ѿ��򿪴��ڵĽ��,�ڷ���TRUE ��֮FALSE
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
            /// �ر�һ������
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
            /// �ر�һ��������û�б�����ʾ
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
                    ///���ùر����д���ʱ��ѡ��:"ȫ��"
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
            /// ������һ��Url
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
            /// WorkbenchForm���Ӵ���ĵ�ǰ���ڸı���¼�
            /// </summary>
            static public event EventHandler<EventArgs<Form>> ActiveWorkDocumentChanged;

            /// <summary>
            /// �������ĵ����ڱ������¼�
            /// </summary>
            static public event EventHandler<WorkDocumentEventArgs> WorkDocumentSaved;

            /// <summary>
            /// һ�ֹ��������ڵ���Ӧ���Ӻ���¼�
            /// </summary>
            static public event EventHandler<WorkDocumentEventArgs> WorkDocumentAdded;

            static public event EventHandler<WorkDocumentEventArgs> WorkDocumentDeleted;

            #endregion

            #region internal�Ķ�������InternalService��ת

            /// <summary>
            /// ���ڲ����ã���Ӧ���ֶ����á�
            /// </summary>
            static internal void OnActiveWorkDocumentChanged(EventArgs<Form> e)
            {
                if (ActiveWorkDocumentChanged != null)
                {
                    ActiveWorkDocumentChanged(null, e);
                }
            }

            /// <summary>
            /// ���ڲ����ã���Ӧ���ֶ����á�
            /// </summary>
            static internal void OnWorkDocumentSaved(WorkDocumentEventArgs e)
            {
                if (WorkDocumentSaved != null)
                {
                    WorkDocumentSaved(null, e);
                }
            }

            /// <summary>
            /// ���ڲ����ã���Ӧ���ֶ����á�
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
            /// ��������ã������ڲ�����
            /// </summary>
            static internal void SetActiveWorkDocument(WorkDocumentType type, string id,Form activeForm)
            {
                _activeWorkDocumentType = type;
                _activeId = id;
                ActiveForm = activeForm;
            }

            #endregion

            #region Ctrl+Tab���ĵ���

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
                ///�����
                if (ele == null)
                {
                    return;
                }
                ele.RemoveAll();

                ///���л�ĵ�(ActiveWorkDocument)��ʱ�򣬱������������ָ�ʱʹ��
                if (ActiveWorkDocumentType != WorkDocumentType.None)
                {
                    string activeKeyId = GetKey(ActiveId, ActiveWorkDocumentType);
                    ele.SetAttribute("defaultForm", activeKeyId);
                }

                ///������ǰ�򿪵Ĵ����б�������Ҫ�Ĵ��ڹ��´δ�ʱʹ��
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
                            Debug.Fail("�����ڴ���δ֪�Ĵ�������:" + data.WorkDocumentType);
                            break;
                    }
                }
            }

            /// <summary>
            /// ��XmlElement���������봰���б�
            /// </summary>
            static public void LoadFormListFromXml(XmlElement ele)
            {
                try
                {
                    if (ele == null)
                    {
                        return;
                    }
                    ///snip��������(������snip��ǰ�ȴ�tmplt����)
                    List<XmlElement> snips = new List<XmlElement>();

                    foreach (XmlNode node in ele.ChildNodes)
                    {
                        if (node.NodeType == XmlNodeType.Element)
                        {
                            XmlElement eleForm = (XmlElement)node;

                            ///��������
                            WorkDocumentType type = (WorkDocumentType)Enum.Parse(typeof(WorkDocumentType),
                                eleForm.GetAttribute("type"), true);

                            if (type == WorkDocumentType.SnipDesigner)
                            {
                                snips.Add(eleForm);
                            }
                            else
                            {
                                ///id��ownerId
                                string id = eleForm.GetAttribute("id");
                                string owner = eleForm.GetAttribute("ownerId");

                                OpenWorkDocument(type, id, owner);
                            }
                        }
                    }

                    ///�����snip
                    foreach (XmlElement snip in snips)
                    {
                        ///id��ownerId
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
                ///��������ô��쳣�׳���������ʽ������Դ��쳣��
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
                            ///snip�����������tmplt�����֮ǰ�ر�
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
                                ///snip�����������tmplt�����֮ǰ�ر�
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
            /// Tmplt��Document�Ľ�����(��������ģ���Ƿ���������ҳ��Ƭ)�ĸ����¼���e.Item��ģ��ID
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
    /// ÿ��MdiChildForm����󶨵�����
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
    /// �ر����д���ʱ�����ݱ���
    /// </summary>
    public class CloseAllWindowData
    {
        private int _closingAllWindow;
        /// <summary>
        /// ��ȡ�������Ƿ����ڹر���Ŀ
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
        /// �ر���Ŀ��ѡ��
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