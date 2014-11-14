namespace Didaku.Engine.Timeaxis.Base.Interfaces.Funcs
{
    public interface IDeleteFunc<T>
    {
        /// <summary>删除交易的筛选条件
        /// </summary>
        /// <value>The params.</value>
        /// <remarks></remarks>
        T Condition { get; set; }

        /// <summary>执行删除
        /// </summary>
        /// <returns></returns>
        bool Execute();
    }
}