namespace NKnife.Tools.Robot.CubeOctopus.Base
{
    /// <summary>描述指定的舵机将要到达的位置
    /// </summary>
    struct Steering
    {
        /// <summary>
        /// 舵机编号
        /// </summary>
        public ushort Index { get; set; }
        /// <summary>
        /// 将要到达的位置
        /// </summary>
        public int Position { get; set; }

        public override string ToString()
        {
            return string.Format("#{0}P{1}", Index, Position);
        }
    }
}