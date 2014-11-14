using System;
using System.Collections.Generic;
using System.Reflection;
using Common.Logging;
using NKnife.Attributes;
using NKnife.Interface;
using NKnife.Utility;

namespace Didaku.Engine.Timeaxis.Kernel
{
    /// <summary>描述SDF应用程序的环境，以及环境的相关启动函数
    /// </summary>
    public class EnvironmentInitializer : IInitializer
    {
        private static readonly ILog _Logger = LogManager.GetCurrentClassLogger();

        private readonly List<EnvironmentItemAttribute> _ItemList = new List<EnvironmentItemAttribute>();

        /// <summary>所有启动项的集合，将在 Initialize() 函数中被初始化
        /// </summary>
        private readonly Dictionary<EnvironmentItemAttribute, IInitializer> _ItemMap = new Dictionary<EnvironmentItemAttribute, IInitializer>();

        #region Implementation of IInitializer

        /// <summary>是否已经初始化
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>应用程序环境的初始化, 执行初始化动作
        /// </summary>
        /// <param name="args">初始化的动作参数</param>
        public bool Initialize(params object[] args)
        {
            if (IsInitialized) 
                return IsInitialized;
            try
            {
                Assembly asse = Assembly.GetExecutingAssembly();
                Type[] types = asse.GetTypes();
                foreach (Type type in types)
                {
                    if ((type.ContainsInterface(typeof (IInitializer)) && !type.IsAbstract))
                    {
                        try
                        {
                            if (type.ContainsCustomAttribute(typeof (EnvironmentItemAttribute)))
                            {
                                var attr = (EnvironmentItemAttribute) Attribute.GetCustomAttribute(type, typeof (EnvironmentItemAttribute));
                                var initializer = (IInitializer) Activator.CreateInstance(type);
                                _ItemMap.Add(attr, initializer);
                                _ItemList.Add(attr);
                            }
                        }
                        catch (Exception e)
                        {
                            _Logger.Warn(string.Format("寻找启动时的应用程序服务项异常."), e);
                        }
                    }
                }
                string info = string.Format("找到应用程序环境服务项{0}个。", _ItemMap.Count);
                _Logger.Info(info);
                if (_ItemMap.Count > 0)
                {
                    //按定义的顺序进行排序
                    _ItemList.TrimExcess();
                    _ItemList.Sort();
                    //按顺序进行启动(初始化)
                    for (int i = 0; i < _ItemMap.Count; i++)
                    {
                        try
                        {
                            EnvironmentItemAttribute attr = _ItemList[i];
                            _ItemMap[attr].Initialize();
                            info = string.Format("启动\"{0}\"服务完成。-- {1}", attr.Description, i);
                            _Logger.Info(info);
                        }
                        catch (Exception e)
                        {
                            _Logger.Warn(string.Format("应用程序服务项初始化异常."), e);
                        }
                    }
                }
                IsInitialized = true;
                _Logger.Info("应用程序环境的初始化完成。");
                OnInitialized(EventArgs.Empty);
                return true;
            }
            catch (Exception e)
            {
                _Logger.Fatal("应用程序环境的初始化异常", e);
                return false;
            }
        }

        /// <summary>应用程序环境的初始化完成时发生的事件
        /// </summary>
        public event EventHandler InitializedEvent;

        protected virtual void OnInitialized(EventArgs e)
        {
            if (InitializedEvent != null)
                InitializedEvent(this, e);
        }

        /// <summary>当应用程序退出时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnApplicationExit(object sender, EventArgs e)
        {
        }

        #endregion

    }
}