using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace NKnife.App.SocketKit.Commands
{
    public class MenuCommandManager
    {
        public static RoutedUICommand ServerCreator = new ServerCreatorCommand();
    }
}
