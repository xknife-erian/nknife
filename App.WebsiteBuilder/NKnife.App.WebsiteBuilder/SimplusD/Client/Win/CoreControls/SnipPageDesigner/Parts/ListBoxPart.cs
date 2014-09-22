using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 列表型Part的子Part
    /// </summary>

    [Serializable]
    public class ListBoxPart : SnipPagePart
    {
        #region 构造函数

        protected ListBoxPart(SnipPageDesigner designer)
            : base(designer)
        {
            StyleType = StyleType.GeneralPageListPart;

            Init();

        }

        
        #endregion

        #region 内部变量

        #endregion

        #region 公共属性

        /// <summary>
        /// edir by zhenghao at 2008-06-17 11:45
        /// 获取或设置页面的类型
        /// </summary>
        public StyleType StyleType { get; set; }

        private StyleXmlElement _styleElement;
        /// <summary>
        /// 获取或设置样式元素
        /// </summary>
        public StyleXmlElement StyleElement
        {
            get
            {
                return _styleElement;
            }
            set
            {
                _styleElement = value;
                GetParts(this,value.GetPartsElement());
            }
        }

        #endregion

        #region 内部方法


        /// <summary>
        /// edit by zhenghao at 2008-06-18 13:50
        /// 初始化
        /// </summary>
        private void Init()
        {
           
        }

        /// <summary>
        /// 将element里保存的数据读取到parts(递归)
        /// </summary>
        private void GetParts(SnipPagePart part, XmlElement element)
        {
            XmlNodeList _nodes = element.SelectNodes("part");
            //part.ChildParts.Clear();
            foreach (XmlNode node in _nodes)
            {
                SnipPartXmlElement partEle = (SnipPartXmlElement)node;
                SnipPagePart _part = SnipPagePart.Parse(partEle, this.Designer);

                part.ChildParts.Add(_part);
                GetParts(_part, partEle);
            }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 创建ListBoxPart
        /// </summary>
        /// <param name="designer"></param>
        /// <returns></returns>
        static internal ListBoxPart Create(SnipPageDesigner designer)
        {
            return new ListBoxPart(designer);
        }

        public override bool CanInto(SnipPagePart targetPart)
        {
            if (targetPart.PartType == SnipPartType.List)
            {
                return true;
            }

            return base.CanInto(targetPart);
        }

        #endregion
        
    }
}
