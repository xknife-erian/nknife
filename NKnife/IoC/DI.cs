using System;
using System.Collections.Generic;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using NKnife.Utility.File;

namespace NKnife.IoC
{
    public static class DI
    {
        private static bool _Initialized;
        private static CoreKernel _CoreKernel;

        private class CoreKernel : StandardKernel
        {
            public CoreKernel()
                : base(new NinjectSettings
                {
                    InjectNonPublic = true,
                })
            {
            }
        }

        public static void Initialize()
        {
            if (_Initialized) 
                return;

            var assems = UtilityFile.SearchAssemblyByDirectory(AppDomain.CurrentDomain.BaseDirectory);

            _CoreKernel = new CoreKernel();
            _CoreKernel.Load(assems);

            _Initialized = true;
        }

        public static void AddModule(NinjectModule module)
        {
            Initialize();
            _CoreKernel.Load(module);
        }

        /// <summary>
        /// Gets an instance of the specified service.
        /// </summary>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>An instance of the service.</returns>
        public static T Get<T>(params IParameter[] parameters)
        {
            Initialize(); 
            return _CoreKernel.Get<T>(parameters);
        }

        /// <summary>
        /// Gets an instance of the specified service by using the first binding with the specified name.
        /// </summary>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <param name="name">The name of the binding.</param>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>An instance of the service.</returns>
        public static T Get<T>(string name, params IParameter[] parameters)
        {
            Initialize();
            return _CoreKernel.Get<T>(name, parameters);
        }

        /// <summary>
        /// Gets an instance of the specified service by using the first binding that matches the specified constraint.
        /// </summary>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <param name="constraint">The constraint to apply to the binding.</param>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>An instance of the service.</returns>
        public static T Get<T>(Func<IBindingMetadata, bool> constraint, params IParameter[] parameters)
        {
            Initialize();
            return _CoreKernel.Get<T>(constraint, parameters);
        }

        /// <summary>
        /// Tries to get an instance of the specified service.
        /// </summary>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>An instance of the service, or <see langword="null"/> if no implementation was available.</returns>
        public static T TryGet<T>(params IParameter[] parameters)
        {
            Initialize();
            return _CoreKernel.Get<T>(parameters);
        }

        /// <summary>
        /// Tries to get an instance of the specified service by using the first binding with the specified name.
        /// </summary>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <param name="name">The name of the binding.</param>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>An instance of the service, or <see langword="null"/> if no implementation was available.</returns>
        public static T TryGet<T>(string name, params IParameter[] parameters)
        {
            Initialize();
            return _CoreKernel.Get<T>(name, parameters);
        }

        /// <summary>
        /// Tries to get an instance of the specified service by using the first binding that matches the specified constraint.
        /// </summary>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <param name="constraint">The constraint to apply to the binding.</param>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>An instance of the service, or <see langword="null"/> if no implementation was available.</returns>
        public static T TryGet<T>(Func<IBindingMetadata, bool> constraint, params IParameter[] parameters)
        {
            Initialize();
            return _CoreKernel.Get<T>(constraint, parameters);
        }

        /// <summary>
        /// Tries to get an instance of the specified service.
        /// </summary>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>An instance of the service, or <see langword="null"/> if no implementation was available.</returns>
        public static T TryGetAndThrowOnInvalidBinding<T>(params IParameter[] parameters)
        {
            Initialize();
            return _CoreKernel.Get<T>(parameters);
        }

        /// <summary>
        /// Tries to get an instance of the specified service by using the first binding with the specified name.
        /// </summary>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <param name="name">The name of the binding.</param>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>An instance of the service, or <see langword="null"/> if no implementation was available.</returns>
        public static T TryGetAndThrowOnInvalidBinding<T>(string name, params IParameter[] parameters)
        {
            Initialize();
            return _CoreKernel.Get<T>(name, parameters);
        }

        /// <summary>
        /// Tries to get an instance of the specified service by using the first binding that matches the specified constraint.
        /// </summary>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <param name="constraint">The constraint to apply to the binding.</param>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>An instance of the service, or <see langword="null"/> if no implementation was available.</returns>
        public static T TryGetAndThrowOnInvalidBinding<T>(Func<IBindingMetadata, bool> constraint, params IParameter[] parameters)
        {
            Initialize();
            return _CoreKernel.Get<T>(constraint, parameters);
        }

        /// <summary>
        /// Gets all available instances of the specified service.
        /// </summary>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>A series of instances of the service.</returns>
        public static IEnumerable<T> GetAll<T>(params IParameter[] parameters)
        {
            Initialize();
            return _CoreKernel.GetAll<T>(parameters);
        }

        /// <summary>
        /// Gets all instances of the specified service using bindings registered with the specified name.
        /// </summary>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <param name="name">The name of the binding.</param>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>A series of instances of the service.</returns>
        public static IEnumerable<T> GetAll<T>(string name, params IParameter[] parameters)
        {
            Initialize();
            return _CoreKernel.GetAll<T>(name, parameters);
        }

        /// <summary>
        /// Gets all instances of the specified service by using the bindings that match the specified constraint.
        /// </summary>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <param name="constraint">The constraint to apply to the bindings.</param>
        /// <param name="parameters">The parameters to pass to the request.</param>
        /// <returns>A series of instances of the service.</returns>
        public static IEnumerable<T> GetAll<T>(Func<IBindingMetadata, bool> constraint, params IParameter[] parameters)
        {
            Initialize();
            return _CoreKernel.GetAll<T>(constraint, parameters);
        }

    }
}