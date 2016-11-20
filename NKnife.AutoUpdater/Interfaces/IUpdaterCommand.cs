namespace NKnife.AutoUpdater.Interfaces
{
    /// <summary>(命令模式)根据升级程序的参数进行分解成独立的命令。“将行为抽象为对象”
    /// </summary>
    internal interface IUpdaterCommand
    {
        /// <summary>命令行参数名
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>命令行参数的参数
        /// </summary>
        /// <value>
        /// The param.
        /// </value>
        string[] Param { get; set; }

        /// <summary>执行顺序
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        short Order { get; }

        /// <summary>执行操作
        /// </summary>
        /// <returns></returns>
        bool Run();
    }
}