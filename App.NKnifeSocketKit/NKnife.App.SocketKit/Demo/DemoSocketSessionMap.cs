using System.Windows.Forms;
using SocketKnife.Generic;

namespace NKnife.App.SocketKit.Demo
{
    public class DemoSocketSessionMap : KnifeSocketSessionMap
    {
        public DemoSocketSessionMap()
        {
            MessageBox.Show(this.GetType().FullName);
        }
    }
}
