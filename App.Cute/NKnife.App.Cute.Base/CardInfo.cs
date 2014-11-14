using System;
using Didaku.Collections;
using Didaku.Wrapper;
using Didaku.Engine.Timeaxis.Base.Enums;

namespace Didaku.Engine.Timeaxis.Base.Entities
{
    /// <summary>客户实体类
    /// 客户检测到后，经识别后产生，客户办完业务离开后，该信息应从字典中删除
    /// </summary>
    [Serializable]
    public abstract class CardInfo
    {
        #region ICardInfo Members

        /// <summary>
        /// 客户来时输入的身份识别信息，如卡号，折号，身份证号，手机号，
        /// CardNumber是CardInfo的主键
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 客户所持卡片类型
        /// </summary>
        public abstract CardType CardType { get; set; }

        /// <summary>卡片分类(客户分类)
        /// </summary>
        public IdName Category { get; set; }

        /// <summary>
        /// 除卡号的额外信息，以键值对的形式存储，不同的卡对应的
        /// 身份证:包含姓名，身份证，地址等等
        /// 磁卡：包含二磁道，三磁道，一磁道
        /// </summary>
        public SerializableMap<string, string> Details { get; set; }

        #endregion
    }
}