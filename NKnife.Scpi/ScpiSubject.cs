namespace ScpiKnife
{
    /// <summary>
    /// 描述一项采集工作的指令集合
    /// </summary>
    public class ScpiSubject
    {
        public string Description { get; set; }
        /// <summary>
        /// 前导指令集合
        /// </summary>
        public ScpiGroup Preload { get; set; }
        /// <summary>
        /// 采集指令集合
        /// </summary>
        public ScpiGroup Collect { get; set; }
    }
}