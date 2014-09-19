using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace NKnife.App.SocketKit.Commands
{
    public class ServerCreatorCommand : RoutedUICommand
    {
        public ServerCreatorCommand()
            : base("新建服务器(_S)", "ServerCreator",
                typeof (MenuCommandManager),
                new InputGestureCollection(new InputGesture[] {new KeyGesture(Key.F3)}))
        {
            var cb = new CommandBinding();
            cb.Command = MenuCommandManager.ServerCreator;
            cb.Executed += new ExecutedRoutedEventHandler(Executed); 
        }

        private void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("新建Socket服务器端");
        }
    }
}
