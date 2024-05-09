using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NKnife.Reflection;

// ReSharper disable once CheckNamespace
namespace System.Reflection
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// 从程序集中获取程序集实例中具有指定名称的 System.Type 对象。
        /// 当输入assignableFromType时判断该Type是否是从assignableFromType继承。
        /// assignableFromType可以为Null，为Null时不作判断。
        /// </summary>
        /// <param name="assembly">指定的程序集</param>
        /// <param name="typeFullName">指定的类型全名</param>
        /// <param name="assignableFromType">被继承的类型</param>
        /// <returns>如找到返回该类型，未找到返Null</returns>
        public static Type FindType(this Assembly assembly, string typeFullName, Type assignableFromType = null)
        {
            Type type = assembly.GetType(typeFullName, true, false);
            if (assignableFromType == null)
                return type;
            return assignableFromType.IsAssignableFrom(type) ? type : null;
        }

        public static List<Type> GetAllTypes(this Assembly assembly)
        {
            try
            {
                return assembly.DefinedTypes
                    .ToList()
                    .Select(s => s.AsType())
                    .Where(s => s != null).ToList()
                    .OrderBy(s => s.Name).ToList();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(t => t != null).ToList();
            }
            catch (Exception)
            {
                return new List<Type>(0);
            }
        }

        public static Dictionary<string, List<string>> GetAllMemberNames(this Assembly assembly)
        {
            var allTypes = assembly.GetAllTypes();
            var dictionary = new Dictionary<string, List<string>>();
            foreach (var type in allTypes)
            {
                var index = $"{type.Namespace}-{type.Name}";
                var allMemberNames = type.GetAllMemberNames();
                dictionary[index] = allMemberNames;
            }

            return dictionary;
        }

        public static List<string> GetAllNameSpacesNames(this Assembly assembly)
        {
            var list = assembly.GetAllTypes().Select(type => type.Namespace).ToList();
            return list.Any() ? list.Distinct().OrderBy(s => s).ToList() : list;
        }

        public static List<string> GetAllTypesNames(this Assembly assembly)
        {
            return assembly.GetAllTypes().Select(s => s.Name).ToList();
        }

        public static List<MethodInfo> GetAllMethods(this Assembly assembly)
        {
            return assembly.GetAllTypes().SelectMany(s => s.GetAllMethods()).ToList();
        }

        public static List<MethodMeta> GetAllMethodsMetaData(this Assembly assembly)
        {
            return assembly.GetAllTypes().SelectMany(s => s.GetAllMethodsMetaData()).ToList();
        }
    }
}