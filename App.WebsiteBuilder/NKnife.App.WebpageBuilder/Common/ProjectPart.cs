using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu
{
    public class ProjectPart
    {
        /// <summary>
        /// 是否为当前进行中
        /// </summary>
        public bool IsDoing { get; set; }
        /// <summary>
        /// 项目阶段名称
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 阶段开始时间
        /// </summary>
        public DateTime PartStartTime { get; set; }

        /// <summary>
        /// 阶段完成时间
        /// </summary>
        public DateTime PartEndTime { get; set; }

        /// <summary>
        /// 阶段成本
        /// </summary>
        public double partCost { get; set; }
    }
}
