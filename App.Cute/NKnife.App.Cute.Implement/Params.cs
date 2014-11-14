using System;
using System.Collections.Generic;
using System.Xml;
using Didaku.CoderSetting;
using Didaku.Engine.Timeaxis.Base.Interfaces;
using Didaku.Engine.Timeaxis.Implement.Abstracts.Params;

namespace Didaku.Engine.Timeaxis.Implement
{
    public class Params : XmlCoderSetting
    {
        private readonly Dictionary<int, IActiveParams> _ActiveParamses = new Dictionary<int, IActiveParams>();

        public IActiveParams Build(int kind, string asker, string userId, string queueId)
        {
            var param = _ActiveParamses[kind];
            param.Asker = asker;
            param.QueueId = queueId;
            param.UserId = userId;
            return param;
        }

        public IActiveParams Build(int kind, string xyzAsker, string abcUser, string queueId, ITransaction transaction)
        {
            throw new NotImplementedException();
        }

        #region Overrides of CoderSetting<XmlElement>

        /// <summary>从指定的源(一般是一个XmlElement，也可以是任何类型)载入本类型的各个属性值
        /// </summary>
        /// <param name="source">指定的源(一般是一个XmlElement)</param>
        protected override void Load(XmlElement source)
        {
        }

        #endregion

        #region 单件实例

        private static readonly Lazy<Params> _Instance = new Lazy<Params>(() => new Params());

        /// <summary> 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static Params Instance
        {
            get { return _Instance.Value; }
        }

        private Params()
        {
        }

        #endregion

    }
}