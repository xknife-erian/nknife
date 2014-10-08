using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace NKnife.App.SocketKit.Mvvm.Commands
{
    class ServerCreatorCommand : RoutedUICommand
    {
        public ServerCreatorCommand()
            : base("新建服务器(_S)...", "ServerCreator",
                    typeof(Workbench),new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F3) }))
        {

        }
        
        
    }
}
