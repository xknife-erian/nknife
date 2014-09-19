using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Jeelu.KeywordResonator.Client
{
    class UrlManager
    {
        #region 字段与属性
        private WorkbenchForm _mainForm = null;
        private XmlDocument _urlDoc = null;

        /// <summary>
        /// Url的Xml文档
        /// </summary>
        public XmlDocument UrlDoc
        {
            get { return _urlDoc; }
            set { _urlDoc = value; }
        }
        private System.Collections.Generic.List<UrlItem> _urls;

        /// <summary>
        /// 所有待抓取之Url
        /// </summary>
        public List<UrlItem> Urls
        {
            get { return _urls; }
            set { _urls = value; }
        }
        #endregion

        #region 公共函数

        /// <summary>
        /// 保存Url的Xml文档
        /// </summary>
        public void SaveUrlDoc()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 获取URL列表
        /// </summary>
        /// <param name="urlsFile"></param>
        /// <returns></returns>
        public void GetUrls()
        {
            if (UrlDoc == null)
            {
                UrlDoc = new XmlDocument();
                try
                {
                    UrlDoc.Load(Service.PathService.File.Url);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            XmlNodeList nodes = UrlDoc.SelectNodes(@"//record");
            Urls.Clear();
            foreach (XmlNode node in nodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                XmlElement ele = (XmlElement)node;
                Urls.Add(new UrlItem(ele.GetAttribute("url")));
            }
        }

        /// <summary>
        ///  构造函数
        /// </summary>
        public UrlManager(WorkbenchForm mainForm)
        {
            _mainForm = mainForm;
            Urls = new List<UrlItem>();
        }


        #endregion



    }
}
