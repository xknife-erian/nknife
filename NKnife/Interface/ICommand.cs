namespace NKnife.Interface
{
    /// <summary>
    /// 面向设计模式中的命令模式的命令接口。
    /// 命令模式：将一个请求封装为一个对象，从而可用不同的的请求对客户进行参数化，队请求排队或者记录请求日志，以及支持可撤销的操作。
    /// 1.它能很容易的维护所有命令的集合。
    /// 2.它可以很方便的实现撤销和恢复命令。
    /// 3.可以很方便的将每个执行记录日志。
    /// 4.最重要的就是将发起者与实现者分离。
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 执行操作
        /// </summary>
        void Execute();

        /// <summary>
        /// 取消操作
        /// </summary>
        void Cancel();

        /// <summary>
        /// 是否允许执行操作
        /// </summary>
        /// <returns></returns>
        bool CanExecute();

        /// <summary>
        /// 是否允许取消操作
        /// </summary>
        /// <returns></returns>
        bool CanCancel();
    }
}
