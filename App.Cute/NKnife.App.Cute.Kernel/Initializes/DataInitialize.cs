using System;
using System.Xml;
using Common.Logging;
using NKnife.App.Cute.Kernel.IoC;
using NKnife.Attributes;
using NKnife.Interface;

namespace NKnife.App.Cute.Kernel.Initializes
{
    /// <summary>启动数据持久化层服务
    /// </summary>
    [EnvironmentItem(770, "启动数据持久化层服务。")]
    class DataInitialize : IInitializer
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        #region Implementation of IInitializer

        /// <summary>是否已经初始化
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>执行初始化动作
        /// </summary>
        /// <param name="args">初始化的动作参数</param>
        public bool Initialize(params object[] args)
        {
            try
            {
                if(!IsInitialized)
                {
                    var doc = new XmlDocument();
                    doc.Load(OptionInitialize.ConfigFilePath);
                    XmlElement source = doc.DocumentElement;
                    if (source == null)
                        return false;

                    var dbConnection = source.SelectSingleNode("DbConnection");
                    Core.Singleton<Datas.Datas>().DbConnection = (dbConnection != null) ? dbConnection.InnerText : "mongodb://localhost/?safe=true";

                    Core.Singleton<Datas.Datas>().Initialize();
                    IsInitialized = true;
                    OnInitialized(EventArgs.Empty);
                }
            }
            catch (Exception e)
            {
                _logger.Error("启动数据持久化层服务异常", e);
            }
            return true;
        }

        /// <summary>初始化完成时发生的事件
        /// </summary>
        public event EventHandler InitializedEvent;

        protected virtual void OnInitialized(EventArgs e)
        {
            if (InitializedEvent != null)
                InitializedEvent(this, e);
        }

        #endregion
    }
}
