using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Xml;
using Gean;
using Gean.Configuring.CoderSetting;
using Gean.Configuring.Interfaces;
using NKnife.Extensions;
using NKnife.Utility.File;
using NLog;

namespace NKnife.Configuring.CoderSetting
{
    /// <summary>CoderSetting（程序员配置）服务管理器
    /// </summary>
    public sealed class CoderSettingService : IService<CoderSettingXmlFile>
    {
        /// <summary>CoderSetting（程序员配置）定义文件的后缀名
        /// </summary>
        private const string CODER_SETTING_SUFFIX_NAME = ".codersetting";

        /// <summary>单建实例静态属性的属性名
        /// </summary>
        private const string CTOR = "ME";

        /// <summary>核心载入程序员配置内容的方法名
        /// </summary>
        private const string INITIALIZES = "Initializes";

        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        private static CoderSettingXmlFile[] _SettingDocuments;

        private static readonly CoderSettingMap _OptionMap = new CoderSettingMap();

        /// <summary>应用程序的启动路径
        /// </summary>
        /// <value>应用程序的启动路径</value>
        public static string ApplicationStartPath
        {
            get { return AppDomain.CurrentDomain.SetupInformation.ApplicationBase; }
        }

        /// <summary>XML file map.它的Key是Option类的完全名，Value是该程序员配置数据所在的XML文件。
        /// </summary>
        /// <value>The XML file map.</value>
        public Dictionary<string, CoderSettingXmlFile> XmlFileMap { get; private set; }

        #region 单件实例

        private CoderSettingService()
        {
        }

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static CoderSettingService ME
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            internal static readonly CoderSettingService Instance;

            static Singleton()
            {
                Instance = new CoderSettingService();
                Instance.XmlFileMap = new Dictionary<string, CoderSettingXmlFile>();
            }
        }

        #endregion 单件实例

        #region IService<CoderSettingXmlFile> Members

        /// <summary> 初始化
        /// </summary>
        /// <param name="args">传入参数是所有程序员配置的Xml文件的封装类型(OptionXmlFile).</param>
        /// <returns></returns>
        public bool Initializes(params CoderSettingXmlFile[] args)
        {
            _Logger.Info(string.Format("开始初始化CoderSettingService"));
            if (args == null || args.Length <= 0)
            {
                _Logger.Error("当Option服务管理器初始化时,传入参数有误。传入的Option的Xml文件数量为零或为空。");
                return false;
            }
            _SettingDocuments = new CoderSettingXmlFile[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == null)
                {
                    _Logger.Warn("当Option服务管理器初始化时,传入的对象不是Option的XML，无法解析。" + args[i]);
                }
                _SettingDocuments[i] = args[i];
            }
            // 程序员配置的配置信息节点的集合
            var parmsInfoMap = new Dictionary<string, XmlElement>();
            foreach (CoderSettingXmlFile document in _SettingDocuments)
            {
                IEnumerable<XmlElement> eles = GetElementByClassMap(document);
                foreach (XmlElement ele in eles)
                {
                    string klass = ele.GetAttribute("class");
                    if (!parmsInfoMap.ContainsKey(klass))
                    {
                        parmsInfoMap.Add(klass, ele);
                        XmlFileMap.Add(klass, document);
                    }
                    else
                    {
                        _Logger.Warn(string.Format("配置信息节点的集合已包括了 {0} 个Option配置节点", klass));
                    }
                }
            }
            _Logger.Info(string.Format("从Option的配置Xml文件中找到 {0} 个Option配置节点", parmsInfoMap.Count));

            IEnumerable<Type> optionTypeArray = null;
            try
            {
                optionTypeArray = UtilityType.FindTypesByDirectory(ApplicationStartPath, typeof (ICoderSetting));
            }
            catch (Exception e)
            {
                _Logger.Error(string.Format("从目录中搜索程序员配置类时异常。{0}", e.Message), e);
            }
            if (optionTypeArray == null)
            {
                return false;
            }
            _Logger.Info(string.Format("找到 {0} 个ICoderSetting类型。", optionTypeArray.Count()));

            var optionList = new List<ICoderSetting>();
            // 遍历搜索出来的所有的Option进行实例
            foreach (Type klass in optionTypeArray)
            {
                try
                {
                    if (klass.IsAbstract)
                        continue;
                    if (klass.FullName != null)
                    {
                        if (!parmsInfoMap.ContainsKey(klass.FullName))
                        {
                            _Logger.Trace(string.Format("{0} 未找到程序员配置的配置信息，该程序员配置将不被启用。", klass.FullName));
                            continue;
                        }
                    }
                    //通过单建实例静态属性的属性名创建该程序员配置
                    var property = klass.GetProperty(CTOR) ?? klass.GetProperty("Instance");
                    if (property == null)
                    {
                        _Logger.Warn("未找到可用的单建实例的属性：" + klass.FullName);
                    }
                    else
                    {
                        var option = (ICoderSetting) (property.GetValue(null, null));
                        if (null != option)
                        {
                            // 填充Option类型的Order(排序)属性
                            if (klass.FullName != null)
                            {
                                if (parmsInfoMap.ContainsKey(klass.FullName))
                                {
                                    XmlElement orderEle = parmsInfoMap[klass.FullName];
                                    if (orderEle.HasAttribute("order"))
                                    {
                                        int order;
                                        if (int.TryParse(orderEle.GetAttribute("order"), out order))
                                            _Logger.Trace(klass.Name + "的排序定义为：" + order);
                                        option.Order = order;
                                    }
                                }
                            }
                            // 将有效的Option放入集合中
                            if (!optionList.Contains(option))
                                optionList.Add(option);
                            else
                                _Logger.Warn(string.Format("有重复的程序员配置。{0}", option.GetType().Name));
                        }
                    }
                }
                catch (Exception e)
                {
                    _Logger.ErrorE(string.Format("{0} 程序员配置初始化异常。", klass.FullName), e);
                }
            }

            // 按照配置文件中每个程序员配置节点定义的排序值进行排序
            optionList.Sort();

            // 遍历所有的Option的实例，调用这些Option的初始化方法进行初始化
            foreach (ICoderSetting option in optionList)
            {
                try
                {
                    Type type = option.GetType();
                    MethodInfo method = type.GetMethod(INITIALIZES, (BindingFlags.NonPublic | (BindingFlags.Public | BindingFlags.Instance)));
                    // 调用 Initializes(source) 方法，
                    if (type.FullName != null)
                    {
                        if (parmsInfoMap.ContainsKey(type.FullName))
                            method.Invoke(option, new object[] {parmsInfoMap[type.FullName]});

                        _Logger.Info(string.Format("Setting OK:{0} ", type.Name));
                        if (!_OptionMap.ContainsKey(type.FullName))
                            _OptionMap.Add(type.FullName, option);
                        else
                            _Logger.Warn(string.Format("程序员配置的Map中已有{0}的配置。", type.FullName));
                    }
                }
                catch (Exception e)
                {
                    _Logger.ErrorE(string.Format("{0} 程序员配置执行Load方法异常。", option.GetType().FullName), e);
                }
            }
            return _OptionMap.Count > 0;
        }

        /// <summary>
        /// 重新启动服务
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public bool ReStart(params CoderSettingXmlFile[] args)
        {
            _OptionMap.Clear();
            try
            {
                if (args == null || args.Length <= 0)
                    Initializes(_SettingDocuments);
                else
                    Initializes(args);
            }
            catch (Exception e)
            {
                _Logger.Error("Option服务管理器重新启动时发生异常。异常信息:" + e.Message, e);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            return true;
        }

        /// <summary>
        /// 终止服务
        /// </summary>
        /// <returns></returns>
        public bool Stop()
        {
            return true;
        }

        #endregion

        /// <summary>从指定的Document中的约定的节点遍历获得所有定义的“设置”的类的全名
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns></returns>
        private static IEnumerable<XmlElement> GetElementByClassMap(CoderSettingXmlFile document)
        {
            var elements = new List<XmlElement>();
            XmlNodeList nodelist = document.DocumentElement.SelectNodes("option[@class]");
            if (nodelist != null)
            {
                elements.AddRange(nodelist.OfType<XmlElement>().Where(element => element.Attributes["active"] == null || bool.Parse(element.GetAttribute("active"))));
            }
            return elements;
        }

        /// <summary>一个XML的节点，有name属性，其值为定义的接口名；有class属性，其值是实现了该接口的类的全名；
        /// 通过该方法快速创建该类型。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static KeyValuePair<string, T> InterfaceBuilder<T>(XmlNode node)
        {
            if (node == null || node.Attributes == null || node.Attributes.Count <= 0)
            {
                throw new ArgumentNullException("node", @"参数不能为空");
            }
            string name = node.Attributes["name"].Value;
            Type type = UtilityType.FindType(node.Attributes["class"].Value);
            object klass = UtilityType.CreateObject(type, typeof (T), true);
            return new KeyValuePair<string, T>(name, (T) klass);
        }

        /// <summary>默认获取应用程序所在的目录及子目录下的所有程序员配置数据文件的文件路径
        /// </summary>
        /// <returns></returns>
        public static CoderSettingXmlFile[] GetOptionFiles()
        {
            StringCollection sc = UtilityFile.SearchDirectory(ApplicationStartPath, "*" + CODER_SETTING_SUFFIX_NAME);
            _Logger.Info("程序员配置文件：" + sc.Count);
            return (from string filePath in sc select new CoderSettingXmlFile(filePath)).ToArray();
        }
    }
}