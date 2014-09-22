using System.IO;
using System.Windows.Forms;
using NKnife.App.PictureTextPicker.Views;
using NKnife.Ioc;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker
{
    public partial class MainForm : Form
    {
        private readonly DockPanel _DockPanel = new DockPanel();
        private readonly DockContent _ProjectView = DI.Get<ProjectView>();
        private readonly DockContent _PropertyGridView = DI.Get<PropertyGridView>();
        private readonly DockContent _RectangleListView = DI.Get<RectangleListView>();

        public MainForm()
        {
            InitializeComponent();
            InitializeDockPanel();
            InitializeViews();
        }

        private void InitializeDockPanel()
        {
            MainToolStripContainer.ContentPanel.Controls.Add(_DockPanel);
            _DockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            _DockPanel.Dock = DockStyle.Fill;
            _DockPanel.BringToFront();

            var deserialize = new DeserializeDockContent(GetContentForm);
            string configFile = Path.Combine(Application.StartupPath, "dockpanel.config");
            if (File.Exists(configFile))
            {
                _DockPanel.LoadFromXml(configFile, deserialize);
            }
        }

        private IDockContent GetContentForm(string xml)
        {
            if (xml == typeof(ProjectView).ToString())
                return _ProjectView;
            if (xml == typeof(PropertyGridView).ToString())
                return _PropertyGridView;
            if (xml == typeof(RectangleListView).ToString())
                return _RectangleListView;
            return null;
        }

        private void InitializeViews()
        {
            _ProjectView.Show(_DockPanel, DockState.DockLeft);
            _RectangleListView.Show(_DockPanel, DockState.DockRight);
            _PropertyGridView.Show(_DockPanel, DockState.DockRight);
        }
    }
}
