namespace Jeelu.SimplusD
{
    public enum DragDropResultType
    {
        None = 0,
        /// <summary>
        /// 前面
        /// </summary>
        Before = 1,
        /// <summary>
        /// 后面
        /// </summary>
        After = 2,
        /// <summary>
        /// 在里面
        /// </summary>
        Into = 3,
        /// <summary>
        /// 在拖拽的节点自己上
        /// </summary>
        Self = 4,
    }
}