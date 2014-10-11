using System;
using System.Diagnostics;
using System.Reflection;

namespace NKnife.Utility
{
    public class UtilityReflection
    {
        /// <summary>
        ///     通过反射执行指定对象的指定方法
        /// </summary>
        /// <param name="target"></param>
        /// <param name="methodName"></param>
        /// <param name="paramList"></param>
        /// <param name="bindingFlags"></param>
        public static void InvokeMethod(object target, string methodName, object[] paramList, BindingFlags bindingFlags)
        {
            MethodInfo methodInfo = target.GetType().GetMethod(methodName, bindingFlags);
            if (methodInfo == null)
                throw new Exception(string.Format("找不到方法 '{0}'.", methodName));
            methodInfo.Invoke(target, paramList);
        }

        public static void InvokePrivateMethod(object target, string methodName, object[] paramList)
        {
            InvokeMethod(target, methodName, paramList, BindingFlags.NonPublic | BindingFlags.Instance);
        }

        /// <summary>
        ///     通过反射找到指定对象的相关事件，并处理
        /// </summary>
        public static void AttatchAllEvent(object target, object handlerHost, string handlerMethodName)
        {
            foreach (EventInfo eventInfo in target.GetType().GetEvents())
            {
                try
                {
                    Delegate d = GetEventHandler(eventInfo, handlerHost, handlerMethodName);
                    eventInfo.AddEventHandler(target, d);
                }
                catch(Exception e)
                {
                    Debug.Fail("反射找指定对象异常", e.Message);
                }
            }
        }

        private static Delegate GetEventHandler(EventInfo eventInfo, object handlerHost, string handlerMethodName)
        {
            try
            {
                if (eventInfo == null)
                    throw new Exception(string.Format("Unable to find an event named '{0}'.", eventInfo.Name));
                Type[] delegateParameters = GetDelegateParameterTypes(eventInfo.EventHandlerType);
                if (delegateParameters == null || delegateParameters.Length != 2)
                    throw new InvalidOperationException(string.Format("Event '{0}' is not valid.", eventInfo.Name));
                Type eventArgsType = delegateParameters[1];
                MethodInfo doEventMethod = handlerHost.GetType()
                    .GetMethod(handlerMethodName, BindingFlags.NonPublic | BindingFlags.Instance);
                if (doEventMethod == null)
                    throw new Exception(string.Format("{0} method doesn't exist.", handlerMethodName));
                if (!doEventMethod.IsGenericMethod)
                    throw new Exception(string.Format("{0} method is not a generic method.", handlerMethodName));
                MethodInfo concreteDoEventMethod = doEventMethod.MakeGenericMethod(eventArgsType);
                Delegate d = Delegate.CreateDelegate(eventInfo.EventHandlerType, handlerHost, concreteDoEventMethod);
                return d;
            }
            catch (Exception e)
            {
                Debug.Fail("GetEventHandler异常", e.Message);
                return null;
            }
        }

        /// <summary>
        ///     获得委托的参数类型
        /// </summary>
        private static Type[] GetDelegateParameterTypes(Type d)
        {
            if (d.BaseType != typeof (MulticastDelegate))
            {
                throw new InvalidOperationException("Not a delegate.");
            }
            MethodInfo invoke = d.GetMethod("Invoke");
            if (invoke == null)
            {
                throw new InvalidOperationException("Not a delegate.");
            }
            ParameterInfo[] parameters = invoke.GetParameters();
            var typeParameters = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                typeParameters[i] = parameters[i].ParameterType;
            }
            return typeParameters;
        }
    }
}