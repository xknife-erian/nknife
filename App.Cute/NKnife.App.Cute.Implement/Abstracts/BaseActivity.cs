using NKnife.App.Cute.Base.Interfaces;

namespace NKnife.App.Cute.Implement.Abstracts
{
    /// <summary>描述一个活动，这是做为一个预约及预约产生的工作的各个节点的触发动作。它是一个描述动作的对象。
    /// 在排队系统中，它可以是一个“呼叫”，“转移”等动作。
    /// </summary>
    public abstract class BaseActivity : IActivity
    {
        /// <summary>向本活动发出请求。
        /// </summary>
        /// <param name="param">活动的参数</param>
        /// <param name="transaction">活动关联的交易信息</param>
        /// <returns>请求是否成功</returns>
        public abstract bool Ask<T>(T param, out ITransaction transaction) where T : IActiveParams;

        /// <summary>找到与本Activty匹配的参数类型
        /// </summary>
        /// <returns>一个空的参数类型实体</returns>
        public abstract IActiveParams Find();
    }
}
