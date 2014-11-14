namespace Didaku.Engine.Timeaxis.Base.Enums
{
    /// <summary>卡片类型。对应的 short 值应与 NumberGenerationType 保持一致。
    /// </summary>
    public enum CardType : short
    {
        /// <summary>
        /// 银行磁条卡，银行磁条存折
        /// </summary>
        BankCard = 1,

        /// <summary>
        /// 银行IC卡
        /// </summary>
        BankICCard = 2,

        /// <summary>
        /// 身份证
        /// </summary>
        IdCard = 3,

        /// <summary>
        /// 手机号码
        /// </summary>
        MobilePhone = 4,

        /// <summary>
        /// 电话号码
        /// </summary>
        Phone = 5,

        /// <summary>
        /// 存折
        /// </summary>
        BankBook = 6,
    }
}
