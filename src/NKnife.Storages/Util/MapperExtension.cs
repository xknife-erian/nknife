using System;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace AutoMapper
{
    public static class MapperExtension
    {
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> map, params Expression<Func<TDestination, object>>[] selectors)
        {
            foreach (var selector in selectors)
            {
                map.ForMember(selector, config => config.Ignore());
            }

            return map;
        }
    }
}