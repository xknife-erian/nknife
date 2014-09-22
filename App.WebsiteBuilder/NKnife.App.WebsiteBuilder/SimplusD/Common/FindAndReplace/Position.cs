using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 查找到的位置信息
    /// </summary>
    public class Position
    {
        public Position(int index, int length, PropertyInfo property, object document)
        {
            this.Index = index;
            this.Length = length;
            this.Property = property;
            this._proxy = DocumentProxyFactory.CreateProxy(document);
        }

        /// <summary>
        /// 获取或设置结果字符起始位置
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// 获取或设置结果字符长度
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// 获取查找结果所在的属性
        /// </summary>
        public PropertyInfo Property { get; private set; }

        /// <summary>
        /// Document的代理对象
        /// </summary>
        private DocumentProxy _proxy;

        /// <summary>
        /// 获取Document对象
        /// </summary>
        /// <returns></returns>
        public object GetDocument()
        {
            return _proxy.GetDocument();
        }
    }
}
