using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using NKnife.AutoUpdater.Commands;
using NKnife.AutoUpdater.Common;
using NKnife.AutoUpdater.Interfaces;
using NKnife.Utility;

namespace NKnife.AutoUpdater
{
    /// <summary>自动更新的背景线程
    /// </summary>
    internal class AppThread
    {
        private static readonly Type[] _workItemTypes;

        static AppThread()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            _workItemTypes = types.Where(type => type.ContainsInterface(typeof (IUpdaterCommand))).ToArray();
            Logger.WriteLine(string.Format("内部有{0}个工作项处理方法.", _workItemTypes.Length));
        }

        /// <summary>自动更新
        /// </summary>
        public void UpdaterThreadProcess()
        {
            //处理更新器启动时输入的命令行参数
            List<IUpdaterCommand> commandList = Process(Currents.Me.Args);
            //启动更新过程
            CommandRoute.Route(commandList);

            //更新工作全部完成。
            Thread.Sleep(500);
            OnUpdaterThreadProcessed();
        }

        /// <summary>
        /// 当自动更新的执行全部完成时发生的事件
        /// </summary>
        public event EventHandler UpdaterThreadProcessedEvent;

        protected virtual void OnUpdaterThreadProcessed()
        {
            if (UpdaterThreadProcessedEvent != null)
                UpdaterThreadProcessedEvent(this, EventArgs.Empty);
            Logger.IsRun = false;
        }

        /// <summary>处理更新器启动时输入的命令行参数
        /// </summary>
        private List<IUpdaterCommand> Process(string[] args)
        {
            var items = new List<IUpdaterCommand>();
            if (args != null && args.Length > 0)
            {
                foreach (string arg in args)
                {
                    string argName;
                    try
                    {
                        argName = arg.Contains(":") ? arg.Substring(1, arg.IndexOf(':') - 1) : arg.Substring(1);
                    }
                    catch (Exception e)
                    {
                        Logger.WriteLine("解析命令字异常.{0}", e);
                        continue;
                    }
                    foreach (Type itemType in _workItemTypes) //根据传入的参数判断有哪些命令需要加入路由表
                    {
                        try
                        {
                            if (itemType.Name.ToLower().Equals(argName.ToLower()))
                            {
                                var item = (IUpdaterCommand) Activator.CreateInstance(itemType);
                                item.Name = argName;
                                if (arg.Contains(":")) //判断是否包含参数
                                {
                                    string param = arg.Substring(arg.IndexOf(':') + 1);
                                    item.Param = param.Split(new[] {'^'}, StringSplitOptions.RemoveEmptyEntries);
                                }
                                items.Add(item);
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.WriteLine(string.Format("匹配命令字与命令字处理类时异常。{0},{1},{2}", argName, itemType.FullName, e));
                        } //trycache
                    } //foreach type
                } //foreach args
            } // if (!UtilityCollection.IsNullOrEmpty(Args))
            items.Sort((a, b) => a.Order - b.Order);
            return items;
        }
    }
}