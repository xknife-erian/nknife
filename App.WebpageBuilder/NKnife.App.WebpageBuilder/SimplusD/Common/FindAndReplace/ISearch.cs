using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD
{
    public interface ISearch
    {
        /// <summary>
        /// 查找下一个
        /// </summary>
        /// <returns>查找到的位置信息类型</returns>
        Position SearchNext(WantToDoType type);

        /// <summary>
        /// 替换函数
        /// </summary>
        /// <param name="position">在指定的位置替换</param>
        void Replace(Position position);
    }
}
