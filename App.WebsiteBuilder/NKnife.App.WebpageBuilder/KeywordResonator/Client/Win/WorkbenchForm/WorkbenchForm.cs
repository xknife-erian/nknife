using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Jeelu.Win;
using WeifenLuo.WinFormsUI.Docking;

namespace Jeelu.KeywordResonator.Client
{
    public partial class WorkbenchForm : BaseForm
    {

        #region ��̬
        /// <summary>
        /// ������Form(��̬)
        /// </summary>
        static internal WorkbenchForm MainForm { get; private set; }
        /// <summary>
        /// ��ʼ������,һ���ڳ�������ʱ��ʼ��
        /// </summary>
        /// <param name="args"></param>
        static internal void Initialize(string[] args)
        {
            CommandLineArgs = args;
            MainForm = new WorkbenchForm();
            OnWorkbenchCreated();
            if (Initialized != null)
            {
                Initialized(null, EventArgs.Empty);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        private WorkbenchForm()
        {
            this.InitializeComponent();
            if (!this.DesignMode)//��֤���������
            {
                this.ImeMode = ImeMode.On;
                this.MainDockPanel.Dock = DockStyle.Fill;
                this.MainDockPanel.DockTopPortion = 100;
                this.MainDockPanel.DockBottomPortion = 200;
                this.MainDockPanel.DockLeftPortion = 280;
                this.MainDockPanel.DockRightPortion = 100;

                this.IsInitForApplication = false;
                this.IsSnatchAtKewword = false;


                //����ĳ�ʼ��Ϊ�ؼ��ʹ������
                _wordsManager = new WordsManager(this);
                _urlManager = new UrlManager(this);
            }
        }
        #endregion

        #region ���������

        private DockPanel _mainDockPanel;
        internal DockPanel MainDockPanel
        {
            get { return _mainDockPanel; }
        }

        static string[] CommandLineArgs = null;

        internal Form ActiveView { get; set; }

        IWorkbenchLayout _layout;
        internal IWorkbenchLayout WorkbenchLayout
        {
            get { return _layout; }
            set
            {
                if (_layout != null)
                {
                    _layout.Detach();
                }
                value.Attach();
                _layout = value;
            }
        }

        private MenuStrip _MainMenuStrip;
        private StatusStrip _MainStatusStrip;
        private ToolStripMenuItem _FileMenuStrip;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem _SaveMenuStrip;
        private ToolStripMenuItem _SaveAsMenuStrip;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem _CloseMenuStrip;
        private ToolStripMenuItem _EditMenuStrip;
        private ToolStripMenuItem _CutMenuStrip;
        private ToolStripMenuItem _CopyMenuStrip;
        private ToolStripMenuItem _PasteMenuStrip;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem _AllSelectMenuStrip;
        private ToolStripMenuItem _ToolMenuStrip;
        private ToolStripMenuItem _OptionMenuStrip;
        private ToolStripMenuItem _HelpMenuStrip;
        private ToolStripMenuItem _AboutMenuStrip;
        private ToolStripMenuItem _InitMenuStrip;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem _UpdateMenuStrip;
        private ToolStripMenuItem _BackupMenuStrip;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem _ConfigMenuStrip;
        private ToolStripMenuItem _CleanCatchListMenuStrip;
        private ToolStripMenuItem _CatchMenuStrip;

        // ����������ֱ����ڹؼ��ʹؼ��ʹ����URL����
        private WordsManager _wordsManager;
        private ToolStripMenuItem _UpdateLocalWordMenuStrip;
        private ToolStripMenuItem _CatchOldMenuStrip;

        internal WordsManager WordsManager
        {
            get { return _wordsManager; }
            set { _wordsManager = value; }
        }
        private UrlManager _urlManager;

        internal UrlManager UrlManager
        {
            get { return _urlManager; }
            set { _urlManager = value; }
        }

        KeywordListView _newWordsView;
        private ToolStripMenuItem �鿴��ϢToolStripMenuItem;
        private ToolStripMenuItem urlRuleToolStripMenuItem;
        private ToolStripMenuItem buildRuleToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;

        internal KeywordListView NewWordsView
        {
            get { return _newWordsView; }
            set { _newWordsView = value; }
        }
        KeywordListView _existWordsView;

        internal KeywordListView ExistWordsView
        {
            get { return _existWordsView; }
            set { _existWordsView = value; }
        }
        #endregion

        #region �Զ�����¼�

        protected virtual void OnViewOpened(ViewContentEventArgs e)
        {
            if (ViewOpened != null)
            {
                ViewOpened(this, e);
            }
        }
        protected virtual void OnViewClosed(ViewContentEventArgs e)
        {
            if (ViewClosed != null)
            {
                ViewClosed(this, e);
            }
        }
        internal event ViewContentEventHandler ViewOpened;
        internal event ViewContentEventHandler ViewClosed;
        internal static event EventHandler WorkbenchCreated;
        internal static event EventHandler Initialized;
        static void OnWorkbenchCreated()
        {
            if (WorkbenchCreated != null)
            {
                WorkbenchCreated(null, EventArgs.Empty);
            }
        }

        #endregion

        #region ��д

        protected override void OnLoad(EventArgs e)
        {
            if (File.Exists(Service.PathService.File.WorkbenchLayoutConfig))
            {
                if (this.MainDockPanel.Contents.Count > 0)
                {
                    throw new Exception("�ؼ�DockPanel�ѳ�ʼ����");
                }
                try
                {
                    this.MainDockPanel.LoadFromXml(Service.PathService.File.WorkbenchLayoutConfig, new DeserializeDockContent(DockLoadHandler));
                }
                catch (Exception)
                {
                    //��������쳣,ɾ��ԭ�����ļ�
                    File.Delete(Service.PathService.File.WorkbenchLayoutConfig);
                    ResetAllPad();
                }
            }
            else
            {
                ResetAllPad();
            }

            base.OnLoad(e);
        }

        #endregion  

        #region internal ��������

        internal void ResetAllPad()
        {
            //MainStonePad.Hide();
            //MainPropertyPad.Hide();

            //MainPropertyPad.Show(MainDockPanel, DockState.DockBottom);
            //MainStonePad.Show(MainDockPanel, DockState.DockLeft);
        }

        /// <summary>
        /// ��������ڲ���Pad����
        /// </summary>
        internal void UnInitialize()
        {
            this.MainDockPanel.SaveAsXml(Service.PathService.File.WorkbenchLayoutConfig);
        }

        #endregion

        #region private �ڲ�����

        private IDockContent DockLoadHandler(string persistString)
        {
            if (persistString == typeof(FunctionPad).ToString())
            {
                //return _propertyPad;
            }
            else if (persistString == typeof(KeywordListView).ToString())
            {
                //return _stonePad;
            }
            return null;
        }

        private void ActivateForm(Form form)
        {
            if (form is IDockContent)
            {
                ((IDockContent)form).DockHandler.Activate();
            }
            else
            {
                Debug.Fail("��Formδʵ��IDockContent�ӿڡ�");
                form.Activate();
            }
        }

        /// <summary>
        /// Ӧ�ó���Ĺ��������Ƿ񱻳�ʼ��,���������ݿ�,����Url�б�ȡ�
        /// ����ֵ��ͬʱ����һ���ݲ˵���Ŀ��Enable����(���ݹ��������Ƿ񱻳�ʼ��)
        /// </summary>
        internal virtual bool IsInitForApplication
        {
            get { return _isInitForApplication; }
            set
            {
                _isInitForApplication = value;
                this._CatchMenuStrip.Enabled = this._isInitForApplication;
                this._CatchOldMenuStrip.Enabled = this._isInitForApplication;
                this._UpdateLocalWordMenuStrip.Enabled = this._isInitForApplication;
                this._BackupMenuStrip.Enabled = this._isInitForApplication;                
                this._ConfigMenuStrip.Enabled = this._isInitForApplication;
            }
        }


        /// <summary>
        /// �Ƿ�ץȡ���ؼ��ʣ�һЩ����������ץȡ���ؼ��ʺ� ���ܲ��� ���� �ϴ��´ʿ�
        /// </summary>
        private bool _isSnatchAtKewword;

        /// <summary>
        /// �Ƿ�ץȡ���ؼ��ʣ�һЩ����������ץȡ���ؼ��ʺ� ���ܲ��� ���� �ϴ��´ʿ�
        /// </summary>
        internal virtual bool IsSnatchAtKewword
        {
            get { return _isSnatchAtKewword; }
            set
            {
                _isSnatchAtKewword = value;
                this._UpdateMenuStrip.Enabled = this._isSnatchAtKewword;
                this._SaveAsMenuStrip.Enabled = this._isSnatchAtKewword;
                this._SaveMenuStrip.Enabled = this._isSnatchAtKewword;
            }
        }

        /// <summary>
        /// Ӧ�ó���Ĺ��������Ƿ񱻳�ʼ��,���������ݿ�,����Url�б�ȡ�
        /// ���ʹ�ã���ֱ��ʹ�ø��ֶε����ԣ��Ա��������ֶ�ֵʱͬʱ����MenuItem��Enable״̬
        /// </summary>
        private bool _isInitForApplication;

        #endregion

        #region ��ʼ�ؼ���InitializeComponent)

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkbenchForm));
            this._MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this._FileMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._InitMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._CatchMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._CatchOldMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._UpdateMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._UpdateLocalWordMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._BackupMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this._SaveMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._SaveAsMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._CloseMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._EditMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._CutMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._CopyMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._PasteMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this._AllSelectMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._ToolMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._OptionMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._ConfigMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._CleanCatchListMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.�鿴��ϢToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._HelpMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._AboutMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.urlRuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildRuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this._mainDockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this._MainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _MainMenuStrip
            // 
            this._MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._FileMenuStrip,
            this._EditMenuStrip,
            this._ToolMenuStrip,
            this._HelpMenuStrip,
            this.urlRuleToolStripMenuItem});
            this._MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this._MainMenuStrip.Name = "_MainMenuStrip";
            this._MainMenuStrip.Size = new System.Drawing.Size(632, 24);
            this._MainMenuStrip.TabIndex = 1;
            this._MainMenuStrip.Text = "menuStrip1";
            // 
            // _FileMenuStrip
            // 
            this._FileMenuStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._InitMenuStrip,
            this._CatchMenuStrip,
            this._CatchOldMenuStrip,
            this.toolStripSeparator2,
            this._UpdateMenuStrip,
            this._UpdateLocalWordMenuStrip,
            this._BackupMenuStrip,
            this.toolStripSeparator,
            this._SaveMenuStrip,
            this._SaveAsMenuStrip,
            this.toolStripSeparator1,
            this._CloseMenuStrip});
            this._FileMenuStrip.Name = "_FileMenuStrip";
            this._FileMenuStrip.Size = new System.Drawing.Size(59, 20);
            this._FileMenuStrip.Text = "�ļ�(&F)";
            // 
            // _InitMenuStrip
            // 
            this._InitMenuStrip.Name = "_InitMenuStrip";
            this._InitMenuStrip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this._InitMenuStrip.Size = new System.Drawing.Size(201, 22);
            this._InitMenuStrip.Text = "��ʼ��(&I)";
            this._InitMenuStrip.Click += new System.EventHandler(this._InitMenuStrip_Click);
            // 
            // _CatchMenuStrip
            // 
            this._CatchMenuStrip.Name = "_CatchMenuStrip";
            this._CatchMenuStrip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this._CatchMenuStrip.Size = new System.Drawing.Size(201, 22);
            this._CatchMenuStrip.Text = "��׽�¹ؼ���(&T)";
            this._CatchMenuStrip.Click += new System.EventHandler(this._CatchMenuStrip_Click);
            // 
            // _CatchOldMenuStrip
            // 
            this._CatchOldMenuStrip.Name = "_CatchOldMenuStrip";
            this._CatchOldMenuStrip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this._CatchOldMenuStrip.Size = new System.Drawing.Size(201, 22);
            this._CatchOldMenuStrip.Text = "��׽�ɹؼ���(&R)";
            this._CatchOldMenuStrip.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(198, 6);
            // 
            // _UpdateMenuStrip
            // 
            this._UpdateMenuStrip.Name = "_UpdateMenuStrip";
            this._UpdateMenuStrip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this._UpdateMenuStrip.Size = new System.Drawing.Size(201, 22);
            this._UpdateMenuStrip.Text = "�ϴ��´ʿ�(&U)";
            this._UpdateMenuStrip.Click += new System.EventHandler(this._UpdateMenuStrip_Click);
            // 
            // _UpdateLocalWordMenuStrip
            // 
            this._UpdateLocalWordMenuStrip.Name = "_UpdateLocalWordMenuStrip";
            this._UpdateLocalWordMenuStrip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this._UpdateLocalWordMenuStrip.Size = new System.Drawing.Size(201, 22);
            this._UpdateLocalWordMenuStrip.Text = "���±��شʿ�(&L)";
            this._UpdateLocalWordMenuStrip.Click += new System.EventHandler(this._UpdateLocalWordMenuStrip_Click);
            // 
            // _BackupMenuStrip
            // 
            this._BackupMenuStrip.Name = "_BackupMenuStrip";
            this._BackupMenuStrip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this._BackupMenuStrip.Size = new System.Drawing.Size(201, 22);
            this._BackupMenuStrip.Text = "���ݴʿ�(&B)";
            this._BackupMenuStrip.Click += new System.EventHandler(this._BackupMenuStrip_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(198, 6);
            // 
            // _SaveMenuStrip
            // 
            this._SaveMenuStrip.Image = ((System.Drawing.Image)(resources.GetObject("_SaveMenuStrip.Image")));
            this._SaveMenuStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._SaveMenuStrip.Name = "_SaveMenuStrip";
            this._SaveMenuStrip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this._SaveMenuStrip.Size = new System.Drawing.Size(201, 22);
            this._SaveMenuStrip.Text = "����(&S)";
            this._SaveMenuStrip.Click += new System.EventHandler(this._SaveMenuStrip_Click);
            // 
            // _SaveAsMenuStrip
            // 
            this._SaveAsMenuStrip.Name = "_SaveAsMenuStrip";
            this._SaveAsMenuStrip.Size = new System.Drawing.Size(201, 22);
            this._SaveAsMenuStrip.Text = "���Ϊ(&A)";
            this._SaveAsMenuStrip.Click += new System.EventHandler(this._SaveAsMenuStrip_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // _CloseMenuStrip
            // 
            this._CloseMenuStrip.Name = "_CloseMenuStrip";
            this._CloseMenuStrip.Size = new System.Drawing.Size(201, 22);
            this._CloseMenuStrip.Text = "�˳�(&X)";
            this._CloseMenuStrip.Click += new System.EventHandler(this._CloseMenuStrip_Click);
            // 
            // _EditMenuStrip
            // 
            this._EditMenuStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._CutMenuStrip,
            this._CopyMenuStrip,
            this._PasteMenuStrip,
            this.toolStripSeparator4,
            this._AllSelectMenuStrip});
            this._EditMenuStrip.Name = "_EditMenuStrip";
            this._EditMenuStrip.Size = new System.Drawing.Size(59, 20);
            this._EditMenuStrip.Text = "�༭(&E)";
            // 
            // _CutMenuStrip
            // 
            this._CutMenuStrip.Image = ((System.Drawing.Image)(resources.GetObject("_CutMenuStrip.Image")));
            this._CutMenuStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._CutMenuStrip.Name = "_CutMenuStrip";
            this._CutMenuStrip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this._CutMenuStrip.Size = new System.Drawing.Size(153, 22);
            this._CutMenuStrip.Text = "����(&T)";
            this._CutMenuStrip.Click += new System.EventHandler(this._CutMenuStrip_Click);
            // 
            // _CopyMenuStrip
            // 
            this._CopyMenuStrip.Image = ((System.Drawing.Image)(resources.GetObject("_CopyMenuStrip.Image")));
            this._CopyMenuStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._CopyMenuStrip.Name = "_CopyMenuStrip";
            this._CopyMenuStrip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this._CopyMenuStrip.Size = new System.Drawing.Size(153, 22);
            this._CopyMenuStrip.Text = "����(&C)";
            this._CopyMenuStrip.Click += new System.EventHandler(this._CopyMenuStrip_Click);
            // 
            // _PasteMenuStrip
            // 
            this._PasteMenuStrip.Image = ((System.Drawing.Image)(resources.GetObject("_PasteMenuStrip.Image")));
            this._PasteMenuStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._PasteMenuStrip.Name = "_PasteMenuStrip";
            this._PasteMenuStrip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this._PasteMenuStrip.Size = new System.Drawing.Size(153, 22);
            this._PasteMenuStrip.Text = "ճ��(&P)";
            this._PasteMenuStrip.Click += new System.EventHandler(this._PasteMenuStrip_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(150, 6);
            // 
            // _AllSelectMenuStrip
            // 
            this._AllSelectMenuStrip.Name = "_AllSelectMenuStrip";
            this._AllSelectMenuStrip.Size = new System.Drawing.Size(153, 22);
            this._AllSelectMenuStrip.Text = "ȫѡ(&A)";
            this._AllSelectMenuStrip.Click += new System.EventHandler(this._AllSelectMenuStrip_Click);
            // 
            // _ToolMenuStrip
            // 
            this._ToolMenuStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._OptionMenuStrip,
            this.toolStripSeparator3,
            this._ConfigMenuStrip,
            this.�鿴��ϢToolStripMenuItem});
            this._ToolMenuStrip.Name = "_ToolMenuStrip";
            this._ToolMenuStrip.Size = new System.Drawing.Size(59, 20);
            this._ToolMenuStrip.Text = "����(&T)";
            // 
            // _OptionMenuStrip
            // 
            this._OptionMenuStrip.Name = "_OptionMenuStrip";
            this._OptionMenuStrip.Size = new System.Drawing.Size(118, 22);
            this._OptionMenuStrip.Text = "ѡ��(&O)";
            this._OptionMenuStrip.Click += new System.EventHandler(this._OptionMenuStrip_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(115, 6);
            // 
            // _ConfigMenuStrip
            // 
            this._ConfigMenuStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._CleanCatchListMenuStrip});
            this._ConfigMenuStrip.Name = "_ConfigMenuStrip";
            this._ConfigMenuStrip.Size = new System.Drawing.Size(118, 22);
            this._ConfigMenuStrip.Text = "ά��(&R)";
            // 
            // _CleanCatchListMenuStrip
            // 
            this._CleanCatchListMenuStrip.Name = "_CleanCatchListMenuStrip";
            this._CleanCatchListMenuStrip.Size = new System.Drawing.Size(178, 22);
            this._CleanCatchListMenuStrip.Text = "��׽URL�б�ά��(&U)";
            this._CleanCatchListMenuStrip.Click += new System.EventHandler(this._CleanCatchListMenuStrip_Click);
            // 
            // �鿴��ϢToolStripMenuItem
            // 
            this.�鿴��ϢToolStripMenuItem.Name = "�鿴��ϢToolStripMenuItem";
            this.�鿴��ϢToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.�鿴��ϢToolStripMenuItem.Text = "�鿴��Ϣ";
            this.�鿴��ϢToolStripMenuItem.Click += new System.EventHandler(this.�鿴��ϢToolStripMenuItem_Click);
            // 
            // _HelpMenuStrip
            // 
            this._HelpMenuStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._AboutMenuStrip});
            this._HelpMenuStrip.Name = "_HelpMenuStrip";
            this._HelpMenuStrip.Size = new System.Drawing.Size(59, 20);
            this._HelpMenuStrip.Text = "����(&H)";
            // 
            // _AboutMenuStrip
            // 
            this._AboutMenuStrip.Name = "_AboutMenuStrip";
            this._AboutMenuStrip.Size = new System.Drawing.Size(130, 22);
            this._AboutMenuStrip.Text = "����(&A)...";
            this._AboutMenuStrip.Click += new System.EventHandler(this._AboutMenuStrip_Click);
            // 
            // urlRuleToolStripMenuItem
            // 
            this.urlRuleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildRuleToolStripMenuItem,
            this.toolStripSeparator5});
            this.urlRuleToolStripMenuItem.Name = "urlRuleToolStripMenuItem";
            this.urlRuleToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.urlRuleToolStripMenuItem.Text = "UrlRule";
            // 
            // buildRuleToolStripMenuItem
            // 
            this.buildRuleToolStripMenuItem.Name = "buildRuleToolStripMenuItem";
            this.buildRuleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.buildRuleToolStripMenuItem.Text = "BuildRule";
            this.buildRuleToolStripMenuItem.Click += new System.EventHandler(this.buildRuleToolStripMenuItem_Click);
            // 
            // _MainStatusStrip
            // 
            this._MainStatusStrip.Location = new System.Drawing.Point(0, 424);
            this._MainStatusStrip.Name = "_MainStatusStrip";
            this._MainStatusStrip.Size = new System.Drawing.Size(632, 22);
            this._MainStatusStrip.TabIndex = 2;
            this._MainStatusStrip.Text = "statusStrip1";
            // 
            // _mainDockPanel
            // 
            this._mainDockPanel.ActiveAutoHideContent = null;
            this._mainDockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainDockPanel.Location = new System.Drawing.Point(0, 24);
            this._mainDockPanel.Name = "_mainDockPanel";
            this._mainDockPanel.Size = new System.Drawing.Size(632, 400);
            this._mainDockPanel.TabIndex = 4;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(149, 6);
            // 
            // WorkbenchForm
            // 
            this.AllowDrop = true;
            this.ClientSize = new System.Drawing.Size(632, 446);
            this.Controls.Add(this._mainDockPanel);
            this.Controls.Add(this._MainMenuStrip);
            this.Controls.Add(this._MainStatusStrip);
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this._MainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "WorkbenchForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this._MainMenuStrip.ResumeLayout(false);
            this._MainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}
