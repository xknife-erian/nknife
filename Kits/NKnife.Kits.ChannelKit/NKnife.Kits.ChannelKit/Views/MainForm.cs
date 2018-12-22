using System.Windows.Forms;
using NKnife.IoC;
using NKnife.Kits.ChannelKit.ViewModels;

namespace NKnife.Kits.ChannelKit.Views
{
    public partial class MainForm : Form
    {
        private MainViewmodel _viewmodel = Di.Get<MainViewmodel>();

        public MainForm()
        {
            InitializeComponent();
        }
    }
}
