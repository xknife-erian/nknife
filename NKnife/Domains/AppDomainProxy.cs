using System;
using System.Reflection;
using Common.Logging;
using NKnife.Interface;

namespace NKnife.Domains
{
    public class AppDomainProxy : MarshalByRefObject
    {
        private Assembly _assembly;

        public void InvokeMethod(string assemblyFullName, string className, string methodName, string[] args)
        {
            _assembly = null;
            try
            {
                _assembly = Assembly.LoadFrom(assemblyFullName);
                Type type = GetTypeByClassName(className);

                object mainPoint = Activator.CreateInstance(type);
                MethodInfo m = type.GetMethod(methodName);
                m.Invoke(mainPoint, new object[] { args });
            }
            catch (Exception e)
            {
                var logger = LogManager.GetCurrentClassLogger();
                logger.Fatal("主服务域的启动方法异常", e);
            }
        }

        private Type GetTypeByClassName(string className)
        {
            return _assembly != null ? _assembly.GetType(className, true, true) : Type.GetType(className, true, true);
        }
    }
}