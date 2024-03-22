using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NLog;

namespace NKnife.NLog.WPF;

public sealed class LogStack
{
    private const int MAX_VIEW_COUNT = 5000;
    public static Action<Action>? UIDispatcher { get; set; }

    /// <summary>
    /// 被界面绑定的集合
    /// </summary>
    public ObservableCollection<LogEventInfo> Logs { get; } = new();

    /// <summary>
    ///     添加日志
    /// </summary>
    public void AddLog(LogEventInfo logEventInfo)
    {
        UIDispatcher?.Invoke(() =>
        {
            Logs.Insert(0, logEventInfo);
            SizeCollection(Logs, MAX_VIEW_COUNT);
        });
    }

    /// <summary>
    ///     将指定的集合缩减到指定大小
    /// </summary>
    /// <param name="collection">指定的集合</param>
    /// <param name="size">指定大小</param>
    private static void SizeCollection<T>(IList<T> collection, int size)
    {
        if (collection.Count >= size)
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

    private LogStack()
    {
    }

    #endregion
}