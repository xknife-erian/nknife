using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using NKnife.Events;
using SocketKnife.Generic;
using SocketKnife.Interfaces;

namespace NKnife.App.SocketKit.Mvvm.ViewModels
{
    public class DemoSocketSessionMap : KnifeSocketSessionMap
    {
        public DemoSocketSessionMap()
        {
            MessageBox.Show(this.GetType().FullName);
        }
    }
}
