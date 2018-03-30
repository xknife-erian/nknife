using System.Windows.Forms;
using NKnife.Channels.Serials;

namespace NKnife.Kits.ChannelKit
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            SerialUtils.RefreshSerialPorts();
            InitializeComponent();
        }
    }
}
