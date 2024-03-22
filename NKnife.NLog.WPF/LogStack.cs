using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using NLog;

namespace NKnife.NLog.WPF;

public sealed class LogStack
{
    /// <summary>
    /// 最大显示数量
    /// </summary>
    public static int MaxViewCount { get; set; } = 2000;

    /// <summary>
    /// 被界面绑定的集合
    /// </summary>
    public ObservableCollection<LogEventInfo> Logs { get; } = new();

    /// <summary>
    ///     添加日志
    /// </summary>
    public void AddLog(LogEventInfo logEventInfo)
    {
        UiDispatcher?.Invoke(() =>
        {
            Logs.Insert(0, logEventInfo);
            SizeCollection(Logs, MaxViewCount);
        });
    }

    /// <summary>
    ///     将指定的集合缩减到指定大小
    /// </summary>
    /// <param name="collection">指定的集合</param>
    /// <param name="size">指定大小</param>
    private static void SizeCollection<T>(IList<T> collection, int size)
    {
        while (collection.Count >= size)
            for (var i = 0; i < collection.Count - size; i++)
                collection.RemoveAt(collection.Count - 1);
    }

    #region 独立于IoC框架之外的单件实例模式

    /// <summary>
    ///     获得一个本类型的单件实例.
    /// </summary>
    /// <value>The instance.</value>
    public static LogStack Instance => s_myInstance.Value;

    private static readonly Lazy<LogStack> s_myInstance = new(() => new LogStack());
    private static Dispatcher _dispatcher;
    private static Action<Action> UiDispatcher => CheckBeginInvokeOnUI;

    /// <summary>
    ///   对于UI线程上的其他操作
    /// </summary>
    private static void CheckBeginInvokeOnUI(Action action)
    {
        if (action == null)
            return;
        if (_dispatcher!.CheckAccess())
            action();
        else
            _dispatcher.BeginInvoke(action);
    }
    private LogStack()
    {
        _dispatcher = Dispatcher.CurrentDispatcher;
    }

    #endregion
}