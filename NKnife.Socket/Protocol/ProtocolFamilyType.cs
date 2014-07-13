namespace SocketKnife.Protocol
{
    /// <summary>
    ///     协议家族枚举
    /// </summary>
    public enum ProtocolFamilyType : ushort
    {
        /// <summary>
        ///     终端初始化信息
        /// </summary>
        BaseInfoGetter = 10010,

        /// <summary>
        ///     平安虚拟呼叫器
        /// </summary>
        PanOperator = 10100,

        /// <summary>
        ///     平安《智能营业厅》
        /// </summary>
        PanSOMS = 10101,

        /// <summary>
        ///     中国工商银行总行 - 虚拟呼叫器
        /// </summary>
        IcbcOperator = 10102,

        /// <summary>
        ///     中国工商银行总行 - 客户增值服务信息平台取号查询接口
        /// </summary>
        IcbcQueueQueryOp = 10103,

        /// <summary>
        ///     平安虚拟综合屏
        /// </summary>
        PanBranchLcd = 10104,

        /// <summary>
        ///     多机联网同步工具协议
        /// </summary>
        PanCoreSynchronizer = 10105,

        /// <summary>
        ///     CEN/XFS 3.0标准
        /// </summary>
        PanXFS = 10106,

        /// <summary>
        ///     填单机
        /// </summary>
        PanSDF = 10107,

        /// <summary>
        ///     平安单机报表
        /// </summary>
        PanReporter = 10108,

        /// <summary>
        ///     平安单机报表
        /// </summary>
        AgreeTechNATP = 10109,

        /// <summary>
        ///     平安虚拟评价器
        /// </summary>
        PanEvaluator = 10110,
    }
}