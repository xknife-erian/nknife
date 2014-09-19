using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 一个缓存了所有类型的页面的定制特性相关的PropertyInfo的静态服务类。
    /// 应在应用程序启动时初始化。
    /// design by lukan, 2008-6-30 00:57:13
    /// </summary>
    public static class PageAttributeService
    {
        public static Dictionary<Type, Dictionary<string, PropertyInfo>> AllPageAttributes
        {
            get { return _AllPageAttributes; }
            set { _AllPageAttributes = value; }
        }
        private static Dictionary<Type, Dictionary<string, PropertyInfo>> _AllPageAttributes = new Dictionary<Type, Dictionary<string, PropertyInfo>>();

        /// <summary>
        /// 在应用程序启动时初始化，将所有的Page相关的定制特性保存一个静态字典中待用，尤其在生成页面时使用是比较频繁的
        /// </summary>
        public static void Initialize()
        {
            Type[] types = typeof(PageXmlDocument).Assembly.GetTypes();
            foreach (Type type in types)
            {
                object[] objs = type.GetCustomAttributes(typeof(PageCustomAttribute), false);
                if (objs.Length > 0)
                {
                    Dictionary<string, PropertyInfo> classCustomAtt = new Dictionary<string, PropertyInfo>();
                    PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    foreach (PropertyInfo pi in properties)
                    {
                        object[] subobjs = pi.GetCustomAttributes(typeof(SnipPartAttribute), false);
                        if (subobjs.Length > 0)
                        {
                            SnipPartAttribute att = (SnipPartAttribute)subobjs[0];
                            classCustomAtt.Add(att.Name, pi);
                        }
                    }
                    _AllPageAttributes.Add(type, classCustomAtt);
                }
            }
        }

        /// <summary>
        /// 获取一个PropertyInfo
        /// </summary>
        /// <param name="attName">定制特性的名字</param>
        /// <param name="type">页面的类型</param>
        public static PropertyInfo GetPropertyInfo(string attName, Type type)
        {
            Dictionary<string, PropertyInfo> classAtt = new Dictionary<string, PropertyInfo>();
            if (!_AllPageAttributes.TryGetValue(type, out classAtt))
            {
                Debug.Fail(type + " is Error type! or PageAttributeService Don't Initalize!");
            }
            PropertyInfo pi;
            if (!classAtt.TryGetValue(attName, out pi))
            {
                return null;
            }
            return pi;
        }

    }
}
