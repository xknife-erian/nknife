using System;
using System.Reflection;
using NKnife.Adapters;
using NKnife.Interface;

namespace NKnife.Domains
{
    public class AppDomainProxy : MarshalByRefObject
    {
        private Assembly _Assembly;

        public void InvokeMethod(string assemblyFullName, string className, string methodName, string[] args)
        {
            _Assembly = null;
            try
            {
                _Assembly = Assembly.LoadFrom(assemblyFullName);
                Type type = GetTypeByClassName(className);

                object mainPoint = Activator.CreateInstance(type);
                MethodInfo m = type.GetMethod(methodName);
                m.Invoke(mainPoint, new object[] { args });
            }
            catch (Exception e)
            {
                var logger = LogFactory.GetCurrentClassLogger();
                logger.Fatal("������������������쳣", e);
            }
        }

        private Type GetTypeByClassName(string className)
        {
            return _Assembly != null ? _Assembly.GetType(className, true, true) : Type.GetType(className, true, true);
        }
    }
}