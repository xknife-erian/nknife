using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class WorkbenchForm : Form
    {
        static string _willOpenFile;

        private FormWindowState defaultWindowState = FormWindowState.Maximized;
        private Rectangle normalBounds = new Rectangle(0, 0, 640, 480);
        //下面几个变量用来在全屏时保存面板的可见状态
        private bool _propertyPadVisible = true;
        private bool _wizardPadVisible = true;
        private bool _treePadVisible = true;
        private bool _tmpltViewPadVisible = true;
        private bool _previewPadVisible = true;
        private bool _needSavePadLayout = true;
        private bool _resultPadVisible = true;

        static WorkbenchForm _mainForm;
        static public WorkbenchForm MainForm
        {
            get { return _mainForm; }
        }

        MenuStrip _topMenu = null;
        public MenuStrip TopMenu
        {
            get { return _topMenu; }
            set { _topMenu = value; }
        }
        ToolStrip[] _toolBars = null;
        public ToolStrip[] ToolBars
        {
            get { return _toolBars; }
            set { _toolBars = value; }
        }

        List<IViewContent> _viewContents = new List<IViewContent>();
        public List<IViewContent> ViewContentCollection
        {
            get { return _viewContents; }
        }

        DockPanel _mainDockPanel = new DockPanel();
        public DockPanel MainDockPanel
        {
            get { return _mainDockPanel; }
        }

        PropertyPad _propertyPad = new PropertyPad();
        public PropertyPad MainPropertyPad
        {
            get { return _propertyPad; }
        }

        PreviewPad _previewPad = new PreviewPad();
        public PreviewPad MainPreviewPad
        {
            get { return _previewPad; }
        }

        ToolsBoxPad _wizardPad = new ToolsBoxPad();
        public ToolsBoxPad MainWizardPad
        {
            get { return _wizardPad; }
        }

        TreePad _treePad = TreePad.TreePadSingle();
        public TreePad MainTreePad
        {
            get { return _treePad; }
        }
        ResultsPad _resultPad = new ResultsPad();
        public ResultsPad MainResultPad
        {
            get { return _resultPad; }
        }
        public TmpltViewPad MainTmpltViewPad { get; private set; }

        public BaseViewForm ActiveView
        {
            get { return MainDockPanel.ActiveDocument as BaseViewForm; }
        }

        IWorkbenchLayout _layout;
        public IWorkbenchLayout WorkbenchLayout
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

        private bool _isHideAllPad;
        public bool IsHideAllPad
        {
            get { return _isHideAllPad; }
        }


        TabNavigationForm _tabNavigationForm;
        public TabNavigationForm TabNavigation
        {
            get { return _tabNavigationForm; }
        }

    }
}
