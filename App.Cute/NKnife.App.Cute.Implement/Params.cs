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

        /// <summary>��ָ����Դ(һ����һ��XmlElement��Ҳ�������κ�����)���뱾���͵ĸ�������ֵ
        /// </summary>
        /// <param name="source">ָ����Դ(һ����һ��XmlElement)</param>
        protected override void Load(XmlElement source)
        {
        }

        #endregion

        #region ����ʵ��

        private static readonly Lazy<Params> _Instance = new Lazy<Params>(() => new Params());

        /// <summary> ���һ�������͵ĵ���ʵ��.
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