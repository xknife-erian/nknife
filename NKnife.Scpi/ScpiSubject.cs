namespace ScpiKnife
{
    /// <summary>
    /// 面向一个工作主题的指令集合
    /// </summary>
    public class ScpiSubject
    {
        /// <summary>
        /// 工作主题的描述
        /// </summary>
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