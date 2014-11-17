using System;
using System.IO;
using Common.Logging;
using NKnife.App.Cute.Kernel.Configurations;
using NKnife.Attributes;
using NKnife.Interface;
using NKnife.Utility;

namespace NKnife.App.Cute.Kernel.Initializes
{
    /// <summary>项目选项初始化
    /// </summary>
    [EnvironmentItem(880, "项目选项初始化。")]
    class OptionInitialize : IInitializer
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        internal static string ConfigFilePath { get; set; }

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
                if (!IsInitialized)
                {
                    var root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                    const string name = "ApplicationOption.codersetting";
                    _logger.Debug(string.Format("检查配置:{0}", name));
                    ConfigFilePath = Path.Combine(root, @"configs\", name);
                    if (!File.Exists(ConfigFilePath))
                    {
                        UtilityFile.CreateDirectory(Path.GetDirectoryName(ConfigFilePath));
                        var bt = CoderSettingFileResource.ApplicationOption;
                        using (FileStream fs = File.Create(ConfigFilePath))
                        {
                            fs.Write(bt, 0, bt.Length);
                            fs.Flush();
                            fs.Close();
                            fs.Dispose();
                        }
                        _logger.Debug(string.Format("配置文件创建:{0}", name));
                    }
                    else
                    {
                        _logger.Debug(string.Format("配置文件正常:{0}", name));
                    }
                    IsInitialized = true;
                    OnInitialized(EventArgs.Empty);
                }
                return true;
            }
            catch (Exception e)
            {
                _logger.Error("项目选项初始化异常", e);
                return false;
            }
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