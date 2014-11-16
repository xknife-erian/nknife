namespace NKnife.App.Cute.Base.Interfaces
{
    /// <summary>一个描述指定类型可做一个排队队列的先决要素。如业务类型、客户类别均可以做为队列的先决要素。
    /// </summary>
    public interface IServiceQueueElement
    {
        /// <summary>获得本元素的IdName
        /// </summary>
        /// <returns></returns>
        string Id { get; set; }

        /// <summary>队列的元素是否是可用状态。如，业务类型可能不在工作时间范围之内。
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is actived; otherwise, <c>false</c>.
        /// </returns>
        bool IsActived();
    }
}