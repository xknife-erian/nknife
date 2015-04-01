﻿using System.Windows.Input;
using GalaSoft.MvvmLight;

namespace NKnife.Kits.SocketKnife.Mvvm
{
    public class WorkbenchViewModel : ViewModelBase
    {
        #region 菜单命令
        
        public static readonly RoutedUICommand ServerCreator = new RoutedUICommand("新建服务器(_S)...", "ServerCreator",
            typeof (WorkbenchViewModel), new InputGestureCollection(new InputGesture[] {new KeyGesture(Key.F3)}));

        public static readonly RoutedUICommand ClientCreator = new RoutedUICommand("新建客户端(_S)...", "ClientCreator",
            typeof (WorkbenchViewModel),
            new InputGestureCollection(new InputGesture[] {new KeyGesture(Key.F4)}));

        public static readonly RoutedUICommand Option = new RoutedUICommand("选项(_O)...", "Option",
            typeof (WorkbenchViewModel),
            new InputGestureCollection(new InputGesture[] {new KeyGesture(Key.O, ModifierKeys.Control)}));

        public static readonly RoutedUICommand Exit = new RoutedUICommand("退出(_X)", "Exit",
            typeof (WorkbenchViewModel),
            new InputGestureCollection(new InputGesture[] {new KeyGesture(Key.F4, ModifierKeys.Alt)}));

        public static readonly RoutedUICommand ParamsView = new RoutedUICommand("终端参数管理器(_M)", "Params",
            typeof (WorkbenchViewModel),
            new InputGestureCollection(new InputGesture[] {new KeyGesture(Key.M, ModifierKeys.Control)}));

        public static readonly RoutedUICommand PropertiesView = new RoutedUICommand("终端属性管理器(_R)", "Properties",
            typeof (WorkbenchViewModel),
            new InputGestureCollection(new InputGesture[] {new KeyGesture(Key.R, ModifierKeys.Control)}));

        public static readonly RoutedUICommand About = new RoutedUICommand("关于(_A)...", "About",
            typeof (WorkbenchViewModel),
            new InputGestureCollection(new InputGesture[] {}));

        #endregion

    }
}