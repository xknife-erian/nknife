using System.Windows.Input;

namespace NKnife.App.SocketKit.Mvvm.Commands
{
    public class MenuCommandManager
    {
        public static RoutedUICommand ServerCreator = new RoutedUICommand("新建服务器(_S)...", "ServerCreator",
            typeof (MenuCommandManager),
            new InputGestureCollection(new InputGesture[] {new KeyGesture(Key.F3)}));

        public static RoutedUICommand ClientCreator = new RoutedUICommand("新建客户端(_S)...", "ClientCreator",
            typeof (MenuCommandManager),
            new InputGestureCollection(new InputGesture[] {new KeyGesture(Key.F4)}));

        public static RoutedUICommand Option = new RoutedUICommand("选项(_O)...", "Option",
            typeof (MenuCommandManager),
            new InputGestureCollection(new InputGesture[] {new KeyGesture(Key.O, ModifierKeys.Control)}));

        public static RoutedUICommand Exit = new RoutedUICommand("退出(_X)", "Exit",
            typeof(MenuCommandManager),
            new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F4, ModifierKeys.Alt) }));

        public static RoutedUICommand ParamsView = new RoutedUICommand("终端参数管理器(_M)", "Params",
            typeof(MenuCommandManager),
            new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.M, ModifierKeys.Control) }));

        public static RoutedUICommand PropertiesView = new RoutedUICommand("终端属性管理器(_R)", "Properties",
            typeof(MenuCommandManager),
            new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.R, ModifierKeys.Control) }));

        public static RoutedUICommand Logger = new RoutedUICommand("日志管理器(_L)", "Logger",
            typeof(MenuCommandManager),
            new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F2, ModifierKeys.Alt) }));

        public static RoutedUICommand About = new RoutedUICommand("关于(_A)...", "About",
            typeof (MenuCommandManager),
            new InputGestureCollection(new InputGesture[] {}));
    }
}