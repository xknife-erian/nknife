using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 定位接口
    /// </summary>
    public interface IMarkPosition
    {
        /// <summary>
        /// 定位函数（定位到位置参数的位置）
        /// </summary>
        /// <param name="position">需要定位到的位置</param>
        void MarkPosition(Position position);

        /// <summary>
        /// 
        /// </summary>
        ISearch Search { get; }

        /// <summary>
        /// 选出来的位置集合
        /// </summary>
        List<Position> SelectedPositions { get; }

        /// <summary>
        /// 当前所在的位置
        /// </summary>
        Position CurrentPosition { get; }
    }
}
