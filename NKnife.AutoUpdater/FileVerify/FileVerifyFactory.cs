using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NKnife.AutoUpdater.Common;
using NKnife.AutoUpdater.Interfaces;

namespace NKnife.AutoUpdater.FileVerify
{
    internal class FileVerifyFactory
    {
        #region 单件实例

        private FileVerifyFactory()
        {
        }

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static FileVerifyFactory Instance
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            internal static readonly FileVerifyFactory Instance;

            static Singleton()
            {
                Instance = new FileVerifyFactory();
            }
        }

        #endregion

        /// <summary>所有实现了 IUpdaterFileVerify 接口的类型
        /// </summary>
        private static readonly Type[] _Types;

        private readonly Dictionary<string, IUpdaterFileVerify> _Verifies = new Dictionary<string, IUpdaterFileVerify>(3);

        static FileVerifyFactory()
        {
            //不同版本的UpdateIndex.xml实现不同的IUpdaterFileVerify
            Assembly asm = Assembly.GetExecutingAssembly();
            _Types = asm.GetTypes().Where(t => typeof (IUpdaterFileVerify).IsAssignableFrom(t)).ToArray();
        }

        /// <summary>根据版本获取指定版本的 IUpdaterFileVerify 工具类型。
        /// </summary>
        public IUpdaterFileVerify GetFileVerify(string version = "4")
        {
            if (_Verifies.ContainsKey(version))
                return _Verifies[version];
            foreach (Type t in _Types)
            {
                object attribute = t.GetCustomAttributes(typeof (UpdateIndexFileAttribute), false).FirstOrDefault();
                if (attribute != null)
                {
                    var attr = attribute as UpdateIndexFileAttribute;
                    if (attr != null && attr.Version == version)
                    {
                        var verify = (IUpdaterFileVerify) Activator.CreateInstance(t);
                        _Verifies.Add(version, verify);
                        return verify;
                    }
                }
            }
            return null;
        }
    }
}