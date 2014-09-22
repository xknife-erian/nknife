using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu
{
    /// <summary>
    /// 指标该类型是否需要实现资源文件的建立
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed public class ResReaderAttribute : Attribute
    {
        /// <summary>
        /// 构造函数：创建一个ResourcesReaderAttribute实例
        /// </summary>
        /// <param name="isGetResource">是否在调用该类型的父类型中创建该类型的内部资源文件，
        /// true创建，false不创建</param>
        public ResReaderAttribute(bool isGetResource)
        {
            this.IsGetResource = isGetResource;
        }

        public bool IsGetResource { get; set; }
    }
}
