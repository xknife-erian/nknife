using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MoreLinq;
using NKnife.Reflection;

// ReSharper disable once CheckNamespace
namespace System.Reflection
{
    public static class TypesExtensions
    {
        #region 接口实现相关

        /// <summary>
        /// 指定的目标类型是否实现了指定的接口类型
        /// </summary>
        /// <param name="targetType">指定的目标类型.</param>
        /// <param name="implType">指定的接口类型.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target type contains interface; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsInterface(this Type targetType, Type implType)
        {
            var interfaces = targetType.GetInterfaces();
            return interfaces.Contains(implType);
        }

        /// <summary>
        /// 指定的目标类型是否实现了指定的【泛型接口】类型
        /// </summary>
        /// <param name="targetType">指定的目标类型.</param>
        /// <param name="implType">指定的接口类型.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target type contains interface; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsGenericInterface(this Type targetType, Type implType)
        {
            return targetType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == implType);
        }

        #endregion

        #region 定制特性相关

        /// <summary>
        /// 指定的目标类型是否实现了指定的定制特性
        /// </summary>
        /// <param name="targetType">指定的目标类型.</param>
        /// <param name="attribute">指定的定制特性.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target type contains CustomAttribute; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsCustomAttribute(this Type targetType, Type attribute)
        {
            object[] attrs = targetType.GetCustomAttributes(true);
            return attrs.Any(attr => attr.GetType() == attribute);
        }

        /// <summary>尝试获取定制特性，如该类型没有指定的定制特性，将为空值
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <returns></returns>
        public static Attribute GetFirstCustomAttribute(this Type targetType)
        {
            object[] attrs = targetType.GetCustomAttributes(true);
            if (attrs.Length > 0)
                return (Attribute) attrs[0];
            return null;
        }

        /// <summary>尝试获取指定类型的定制特性，如该类型没有指定的定制特性，将为空值
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <returns></returns>
        public static T GetFirstCustomAttribute<T>(this Type targetType)
        {
            object[] attrs = targetType.GetCustomAttributes(typeof(T), true);
            if (attrs.Length > 0)
                return (T) attrs[0];
            return default(T);
        }

        #endregion

        #region 创建实例

        /// <summary>从类型中创建此类型的实例（本方法不支持参数可为Null的构造函数）
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="parameters">创建实例所需的参数值列表</param>
        /// <returns>类型实例</returns>
        public static object CreatingObject(this Type type, params object[] parameters)
        {
            int paramNum = 0;
            if (parameters != null)
                paramNum = parameters.Length;

            var paramTypes = new Type[paramNum];
            var paramValues = new object[paramNum];
            for (int i = 0; i < paramNum; i++)
            {
                if (parameters == null || parameters.Length <= 0)
                    continue;
                paramTypes[i] = parameters[i].GetType();
                paramValues[i] = parameters[i];
            }

            return CreatingObject(type, paramTypes, paramValues);
        }

        /// <summary>
        /// 从类型中创建此类型的实例(一些单例的规范命名的类型也可创建)
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="paramTypes">创建实例所需参数的类型列表</param>
        /// <param name="paramValues">创建实例所需的参数值列表</param>
        /// <returns>类型实例</returns>
        public static object CreatingObject(this Type type, Type[] paramTypes, object[] paramValues)
        {
            if (paramTypes != null && paramValues != null && paramTypes.Length != paramValues.Length)
                throw new ArgumentException("反射创建类型实例时，访问该类型的构造函数时，给定的参数类型数量和参数数量不一致。");

            object createdObject = null;
            if (paramTypes == null)
                paramTypes = new Type[] { };

            ConstructorInfo constructor = type.GetConstructor(paramTypes);
            if (constructor != null)
            {
                createdObject = constructor.Invoke(paramValues);
            }
            return createdObject;
        }

        /// <summary>从静态方法或属性创建实例
        /// </summary>
        /// <param name="type"></param>
        /// <param name="staticName">方法名或属性名(通常是静态、公共的)</param>
        /// <param name="paramValues">当有参数被传递进来时的参数数组。</param>
        /// <returns></returns>
        public static object CreatingObjectByStatic(this Type type, string staticName, object[] paramValues = null)
        {
            if (paramValues == null || paramValues.Length <= 0)
            {
                var p = type.GetProperty(staticName);
                if (p != null)
                {
                    //当没有参数传递进来时，先找一下属性名
                    var obj = p.GetValue(null, null);
                    return obj;
                }
            }

            //考虑到属性调用时无法传递参数
            //当有参数传递进来时，可以理解为一定是以方法做创建入口
            MethodInfo m = type.GetMethod(staticName);
            if (m == null)
                return null;
            try
            {
                var mObj = m.Invoke(null, paramValues);
                return mObj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region 获取成员属性相关

        public static List<string> GetBaseClassesNames(this Type type)
        {
            var stringList = new List<string>();
            for (var baseType = GetBaseType(type); (object) baseType != null && baseType.ToString() != "System.Object"; baseType = GetBaseType(baseType))
                stringList.Add(baseType.Name);
            return stringList;
        }

        public static List<Type> GetBaseClassesTypes(this Type type)
        {
            var typeList = new List<Type>();
            for (var baseType = GetBaseType(type); (object) baseType != null && baseType.ToString() != "System.Object"; baseType = GetBaseType(baseType))
                typeList.Add(baseType);
            return typeList;
        }

        public static List<string> GetMemberNames(this Type type, BindingFlags bindingFlags)
        {
            var memberNames = new List<string>();
            TypeExtensions.GetMembers(type, bindingFlags)
                .Where(s => s.MemberType == MemberTypes.Property || s.MemberType == MemberTypes.Field).ToList().ForEach(
                    s =>
                    {
                        if (IsCompilerGeneratedItem(s.GetCustomAttributes().ToList()))
                            return;
                        memberNames.Add(s.Name);
                    });
            return memberNames;
        }

        public static List<string> GetAllMemberNames(this Type type)
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var memberNames1 = type.GetMemberNames(bindingFlags);
            foreach (var baseClassesType in type.GetBaseClassesTypes())
            {
                var memberNames2 = baseClassesType.GetMemberNames(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
                memberNames1.AddRange(memberNames2);
            }

            return memberNames1;
        }

        public static List<MethodInfo> GetAllMethods(this Type type, bool includeCompilerGeneratedAttribute = false)
        {
            var methodInfo = new List<MethodInfo>();
            var bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var baseClassesTypes = type.GetBaseClassesTypes();
            baseClassesTypes.Add(type);
            baseClassesTypes.ForEach(t => methodInfo.AddRange(t.GetMembers(bindingFlags)
                .Where(s => s.MemberType == MemberTypes.Method)
                .Where(s => s.CustomAttributes.Any(a => (object) a.AttributeType == (object) typeof(CompilerGeneratedAttribute)) == includeCompilerGeneratedAttribute)
                .Where(s => (object) s.DeclaringType != (object) typeof(object))
                .Select(s => (MethodInfo) s).ToList().Where(s => s != null)
                .ToList()));
            methodInfo = methodInfo.DistinctBy(m => new
            {
                m.DeclaringType?.Namespace,
                m.DeclaringType?.Name,
                Signature = m.ToString()
            }).ToList();
            return methodInfo.ToList();
        }

        public static List<MethodMeta> GetAllMethodsMetaData(this Type type)
        {
            return type.GetAllMethods().Select(m =>
            {
                var methodMeta = new MethodMeta();
                var declaringType1 = m.DeclaringType;
                methodMeta.NameSpace = (object) declaringType1 != null ? declaringType1.Namespace : null;
                var declaringType2 = m.DeclaringType;
                methodMeta.DeclaringType = (object) declaringType2 != null ? declaringType2.Name : null;
                methodMeta.Name = m.Name;
                methodMeta.Signature = m.ToString();
                return methodMeta;
            }).ToList();
        }

        public static bool IsCompilerGeneratedItem(List<Attribute> customAttributes)
        {
            if (customAttributes == null || !customAttributes.Any())
                return false;
            foreach (object customAttribute in customAttributes)
                customAttribute.GetType();
            return customAttributes.Any(s => (object) s.GetType() == (object) typeof(CompilerGeneratedAttribute));
        }

        public static Type GetBaseType(Type type)
        {
            return type.GetTypeInfo().BaseType;
        }
        
        #endregion

    }
}