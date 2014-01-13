using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Gean.MvvmBase
{
    ///<summary>
    /// Provides support for extracting property information based on a property expression.
    ///</summary>
    public static class PropertyInfoHelper
    {
        private const string NOT_MEMBER_ACCESS_EXPRESSION = "The expression is not a member access expression.";
        private const string EXPRESSION_NOT_PROPERTY = "The member access expression does not access a property.";
        private const string STATIC_EXPRESSION = "The referenced property is a static property.";

        /// <summary>ͨ��һ�����Ե�lanada���ʽ���ٻ�ȡ������Ե������ַ���
        /// </summary>
        /// <param name="propertyExpression">���Ե�lanada���ʽ(e.g. () => PropertyName)</param>
        /// <returns>���Ե������ַ���.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException(NOT_MEMBER_ACCESS_EXPRESSION, "propertyExpression");
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException(EXPRESSION_NOT_PROPERTY, "propertyExpression");
            }

            var getMethod = property.GetGetMethod(true);
            if (getMethod.IsStatic)
            {
                throw new ArgumentException(STATIC_EXPRESSION, "propertyExpression");
            }

            return memberExpression.Member.Name;
        }

        public static Type ExtractMethodType<T>(Expression<Func<T>> expression)
        {
            var methodExpression = expression.Body as MethodCallExpression;
            return methodExpression == null ? null : methodExpression.Method.ReturnType;
        }

    }
}
