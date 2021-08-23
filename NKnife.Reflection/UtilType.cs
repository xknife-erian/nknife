using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace NKnife.Reflection
{
    /// <summary>
    ///     面向C#定义的Type的工具方法集合
    /// </summary>
    public static class UtilType
    {
        /// <summary>
        /// 每次搜索Type是比较耗时的，在这里采用一个字典进行缓存
        /// </summary>
        private static readonly Dictionary<string, Dictionary<string, Type>> _AppTypes = new Dictionary<string, Dictionary<string, Type>>();

        /// <summary>在指定的目录中查找指定的类型
        /// </summary>
        /// <param name="typeName">类型全名（包括命名空间）</param>
        /// <param name="path"></param>
        /// <returns>找到则返回指定的类型，否则返回空</returns>
        public static Type FindType(string typeName, string path)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                throw new ArgumentNullException();

            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException(path + "不存在");

            Dictionary<string, Type> typeMap;
            if (!_AppTypes.ContainsKey(path))
            {
                typeMap = FindTypeMap(path);
                _AppTypes.Add(path, typeMap);
            }
            else
            {
                typeMap = _AppTypes[path];
            }

            if (typeMap != null && typeMap.ContainsKey(typeName))
            {
                return typeMap[typeName];
            }

            return null;
        }

        /// <summary>在当前应用程序域中查找指定的类型
        /// </summary>
        /// <param name="typeName">类型全名（包括命名空间）</param>
        /// <returns>找到则返回指定的类型，否则返回空</returns>
        public static Type FindType(string typeName)
        {
            return FindType(typeName, AppDomain.CurrentDomain.BaseDirectory);
        }

        /// <summary>从指定的目录找到所有Type，并返回Type全名为Key，Type为Value的Map
        /// </summary>
        /// <param name="path">指定的目录.</param>
        /// <returns></returns>
        public static Dictionary<string, Type> FindTypeMap(string path)
        {
            if (_AppTypes.ContainsKey(path))
                return _AppTypes[path];

            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException(path + "目录不存在。");

            var typeMap = new Dictionary<string, Type>();
            var assemblies = UtilAssembly.SearchForAssembliesInDirectory(path);
            foreach (var assembly in assemblies)
            {
                Type[] types;
                try
                {
                    types = assembly.GetTypes();
                }
                catch (Exception)
                {
                    continue;
                }

                foreach (var type in types)
                {
                    if (type.FullName == null)
                        continue;
                    if (!typeMap.ContainsKey(type.FullName))
                        typeMap.Add(type.FullName, type);
                }
            }

            return typeMap;
        }

        /// <summary>从指定的目录中找到所有的.Net程序集，并遍历所有程序集找到所有实现了指定接口或基类的类型
        /// </summary>
        /// <param name="path">指定的目录</param>
        /// <param name="targetType">指定接口的类型</param>
        /// <param name="isGenericTypeInterface">是否是泛型接口</param>
        /// <param name="containAbstract">是否包含虚类型</param>
        /// <returns></returns>
        public static IEnumerable<Type> FindTypesByDirectory(string path, Type targetType, bool isGenericTypeInterface = false, bool containAbstract = false)
        {
            if (!_AppTypes.ContainsKey(path))
            {
                _AppTypes.Add(path, FindTypeMap(path));
            }

            var typeMap = _AppTypes[path];
            var list = new List<Type>();
            foreach (var type in typeMap.Values)
            {
                if (!containAbstract && type.IsAbstract)
                {
                    continue;
                }

                if (!isGenericTypeInterface)
                {
                    if (type.ContainsInterface(targetType))
                        list.Add(type);
                    else if (type.IsSubclassOf(targetType))
                        list.Add(type);
                }
                else
                {
                    if (type.ContainsGenericInterface(targetType))
                        list.Add(type);
                }
            }

            return list;
        }

        /// <summary>
        /// 从目录中找到所有的.Net程序集，并遍历所有程序集找到所有指定的定制特性
        /// </summary>
        /// <param name="appStartPath">The app start path.</param>
        /// <returns></returns>
        public static T[] FindAttributes<T>(string appStartPath) where T : Attribute
        {
            var typeList = new List<T>();
            Assembly[] assArray = UtilAssembly.SearchForAssembliesInDirectory(appStartPath);
            if (assArray == null || assArray.Length <= 0)
                return typeList.ToArray();

            Parallel.ForEach(assArray, ass =>
            {
                Type[] types = null;
                try
                {
                    types = ass.GetTypes();
                }
                catch (Exception e)
                {
                    Debug.Fail($"Assembly.GetTypes()异常:{e.Message}");
                }

                if (!(types == null || types.Length <= 0))
                {
                    Parallel.ForEach(types, type =>
                    {
                        object[] attrs = type.GetCustomAttributes(true);
                        typeList.AddRange(attrs.Where(attr => attr.GetType() == typeof(T)).Cast<T>());
                    });
                }
            });
            return typeList.ToArray();
        }

        /// <summary>
        /// 从目录中找到所有的.Net程序集，并遍历所有程序集找到所有指定的定制特性
        /// </summary>
        /// <param name="appStartPath">The app start path.</param>
        /// <returns></returns>
        public static List<Tuple<T, Type>> FindAttributeMap<T>(string appStartPath) where T : Attribute
        {
            var list = new List<Tuple<T, Type>>();
            Assembly[] assArray = UtilAssembly.SearchForAssembliesInDirectory(appStartPath);
            if (assArray == null || assArray.Length <= 0)
                return list;

            Parallel.ForEach(assArray, ass =>
            {
                Type[] types = null;
                try
                {
                    types = ass.GetTypes();
                }
                catch (Exception e)
                {
                    Debug.Fail($"程序集获取Type失败。{e.Message}");
                }

                if (!(types == null || types.Length <= 0))
                {
                    Parallel.ForEach(types, type =>
                    {
                        object[] attrs = type.GetCustomAttributes(true);
                        if (!(attrs.Length <=0))
                        {
                            list.AddRange(from attr in attrs
                                where attr.GetType() == typeof(T)
                                select new Tuple<T, Type>((T) attr, type));
                        }
                    });
                }
            });
            return list;
        }

        /// <summary>
        /// 从目录中找到所有的.Net程序集，并遍历所有程序集找到所有拥有指定的定制特性的类型
        /// </summary>
        /// <param name="appStartPath">The app start path.</param>
        /// <param name="targetAttribute">指定接口的类型</param>
        /// <returns></returns>
        public static Type[] FindAttributesByDirectory(string appStartPath, Type targetAttribute)
        {
            var typeList = new List<Type>();
            Assembly[] assArray = UtilAssembly.SearchForAssembliesInDirectory(appStartPath);
            if (assArray == null || assArray.Length <= 0)
                return typeList.ToArray();

            Parallel.ForEach(assArray, ass =>
            {
                Type[] types = null;
                try
                {
                    types = ass.GetTypes();
                }
                catch (Exception e)
                {
                    Debug.Fail($"程序集获取Type失败。{e.Message}");
                }

                if (!(types == null || types.Length <= 0))
                {
                    typeList.AddRange(from type in types
                        let attrs = type.GetCustomAttributes(true)
                        where attrs.Any(attr => attr.GetType() == targetAttribute)
                        select type);
                }
            });
            return typeList.ToArray();
        }

        /// <summary>
        /// 从程序集中获得元属性
        /// </summary>
        /// <param name="assemblies">程序集，如果为null，则从当前应用程序域中获取所载入的所有程序集</param>
        /// <returns>找到的元属性的数组</returns>
        public static T[] GetAttributeFromAssembly<T>(Assembly[] assemblies) where T : Attribute
        {
            var list = new List<T>();
            if (assemblies == null)
            {
                assemblies = UtilAssembly.SearchForAssembliesInDirectory(AppDomain.CurrentDomain.BaseDirectory);
            }

            Parallel.ForEach(assemblies, assembly =>
            {
                var attributes = (T[]) assembly.GetCustomAttributes(typeof(T), false);
                if (attributes.Length > 0)
                {
                    list.AddRange(attributes);
                }
            });
            return list.ToArray();
        }

        /// <summary>
        /// 从运行时的堆栈中获取元属性
        /// </summary>
        /// <param name="includeAll">是否包含堆栈上所有的元属性</param>
        /// <typeparam name="T">元属性类型</typeparam>
        /// <returns>找到的元属性的数组</returns>
        public static T[] GetAttributeFromRuntimeStack<T>(bool includeAll) where T : Attribute
        {
            var list = new List<T>();
            var t = new StackTrace();
            for (int i = 0; i < t.FrameCount; i++)
            {
                StackFrame f = t.GetFrame(i);
                var m = (MethodInfo) f.GetMethod();
                var a = Attribute.GetCustomAttributes(m, typeof(T)) as T[];
                if (a != null && a.Length > 0)
                {
                    list.AddRange(a);
                    if (!includeAll)
                    {
                        break;
                    }
                }
            }

            return list.ToArray();
        }
        
        /// <summary>
        /// 根据类型的名称创建一个对象（无参的构造函数）, 考虑了从程序目录中所有的程序集进行创建
        /// </summary>
        /// <param name="klass">The Class.</param>
        /// <returns></returns>
        public static object CreateSimpleObject(string klass)
        {
            Type type = FindType(klass);
            if (null != type)
            {
                try
                {
                    return Activator.CreateInstance(type);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// 一个XML的节点，有name属性，其值为定义的接口名；有class属性，其值是实现了该接口的类的全名；
        /// 通过该方法快速创建该类型。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static Tuple<string, T> InterfaceBuilder<T>(XmlNode node)
        {
            if (node?.Attributes == null || node.Attributes.Count <= 0)
            {
                throw new ArgumentNullException(nameof(node), @"参数不能为空");
            }

            string name = node.Attributes["name"].Value;
            string classname = node.Attributes["class"].Value;
            if (!string.IsNullOrWhiteSpace(classname))
            {
                Type type = FindType(classname);
                object lass = type.CreatingObject(typeof(T), true);
                var pair = new Tuple<string, T>(name, (T) lass);
                return pair;
            }

            return new Tuple<string, T>(name, default(T));
        }

        /// <summary>
        /// 一个XML的节点，有name属性，其值为定义的接口名；有class属性，其值是实现了该接口的类的全名；
        /// 通过该方法快速创建该类型。接收返回值时，必须校验是否为空。
        /// 当有多个实现时，通过过滤器委托判断是否是需要的接口实现。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="typeFilter"></param>
        /// <returns></returns>
        public static Tuple<string, T> CoderSettingClassBuilder<T>(XmlNode node, Func<Type, bool> typeFilter)
        {
            if (node == null || node.ChildNodes.Count <= 0)
            {
                throw new ArgumentNullException(nameof(node), @"参数不能为空");
            }

            var typeList = new List<Type>();
            XmlNode selectSingleNode = node.SelectSingleNode("Interface");
            if (selectSingleNode == null)
            {
                throw new ArgumentNullException(nameof(node), @"Node中的关键参数不能解析到");
            }

            string interfaceName = selectSingleNode.InnerText;
            var classNameList = node.SelectNodes("ClassName");
            if (classNameList != null)
            {
                for (int i = 0; i < classNameList.Count; i++)
                {
                    XmlNode nd = classNameList[i];
                    string className = nd.InnerText;
                    if (!String.IsNullOrWhiteSpace(className))
                    {
                        Type type = FindType(className);
                        typeList.Add(type);
                    }
                }
            }

            Type finalType = null;
            foreach (Type type in typeList)
            {
                if (typeFilter.Invoke(type))
                    finalType = type;
            }

            object lass = finalType.CreatingObject(typeof(T), true);
            var pair = new Tuple<string, T>(interfaceName, (T) lass);
            return pair;
        }

        /// <summary>
        /// 一个XML的节点，有name属性，其值为定义的接口名；有class属性，其值是实现了该接口的类的全名；
        /// 通过该方法快速创建该类型。接收返回值时，必须校验是否为空。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static Tuple<string, T> CoderSettingClassBuilder<T>(XmlNode node)
        {
            if (node == null || node.ChildNodes.Count <= 0)
            {
                throw new ArgumentNullException(nameof(node), "参数不能为空");
            }

            var interfaceNode = node.SelectSingleNode("Interface");
            var classnameNode = node.SelectSingleNode("ClassName");

            if (interfaceNode != null && classnameNode != null)
            {
                string interfaceName = interfaceNode.InnerText;
                string className = classnameNode.InnerText;
                if (!String.IsNullOrWhiteSpace(className))
                {
                    var t = FindType(className);
                    object klass = t.CreatingObject(typeof(T), true);
                    var pair = new Tuple<string, T>(interfaceName, (T) klass);
                    return pair;
                }

                return new Tuple<string, T>(interfaceName, default(T));
            }

            return new Tuple<string, T>("", default(T));
        }
    }
}