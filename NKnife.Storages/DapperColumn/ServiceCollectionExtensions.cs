using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Dapper.DataAnnotations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDapperTypeMaps(this IServiceCollection services, params Assembly[] assemblies)
            => ConfigureDapper(services, assemblies);

        private static IServiceCollection ConfigureDapper(IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type =>
                {
                    PropertyInfo[] props = type.GetProperties();
                    return props.Any(info => info.GetCustomAttribute<ColumnAttribute>() != null);
                });

            foreach (Type type in types)
            {
                SqlMapper.SetTypeMap(type, new DapperColumnAttributeMapper(type));
            }

            return services;
        }
    }
}
