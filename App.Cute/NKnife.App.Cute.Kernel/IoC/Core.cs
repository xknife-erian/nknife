﻿using System;
using System.Linq;
using System.Reflection;
using Common.Logging;
using Ninject;
using Ninject.Modules;
using NKnife.App.Cute.Implement.Environment;
using NKnife.App.Cute.Implement.Industry.Bank;

namespace NKnife.App.Cute.Kernel.IoC
{
    /// <summary>本框架的核心管理器
    /// </summary>
    public static class Core
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private static readonly StandardKernel _kernel = new StandardKernel();

        static Core()
        {
            var assembly = Assembly.GetExecutingAssembly();
            _kernel.Load(assembly);

            //将自动载入的Module打印到日志，以便调试框架。
            var modules = _kernel.GetModules();
            var ms = modules as INinjectModule[] ?? modules.ToArray();
            _logger.Info(string.Format("Ninject共载入{0}个Module。", ms.Count()));
            foreach (var module in ms)
                _logger.Info(string.Format("Ninject已载入:{0}", module.GetType().Name));

            UserPoolDemo();
        }

        /// <summary>获取应用内部的单建实例。在本应用中，需要单建管理的实例全部从此处进行获取。
        /// </summary>
        /// <typeparam name="T">单建实例的类型</typeparam>
        /// <returns>单建实例</returns>
        public static T Singleton<T>()
        {
            return _kernel.Get<T>();
        }

        private static void UserPoolDemo()
        {
            for (int i = 0; i < 5; i++)
            {
                var user = new UserAsBank();
                user.Id = Guid.NewGuid().ToString("N").ToUpper();
                user.LoginName = user.Id.Substring(2, 10);
                user.Name = user.Id.Substring(5, 10);
                user.Number = user.Id.Substring(10, 18);
                user.Email = user.Id.Substring(7, 15) + "@icbc.com.cn";
                user.MobilePhone = user.Id.Substring(4, 15);
                Core.Singleton<UserPool>().Add(user.Id, user);
            }
        }

    }
}
