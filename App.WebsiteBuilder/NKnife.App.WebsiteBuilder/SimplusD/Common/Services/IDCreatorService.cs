using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Xml;

namespace Jeelu.SimplusD
{
    public class IDCreatorService
    {
        /// <summary>
        /// 当前服务服务于此SdsiteXmlDocument
        /// </summary>
        public SdsiteXmlDocument SdsiteDocument { get; private set; }

        #region 与创建IDCreatorService相关的

        /// <summary>
        /// 缓存IDCreatorService对象。key是SdsiteXmlDocument的绝对路径。
        /// </summary>
        static Dictionary<string, IDCreatorService> _dicIdCreators = new Dictionary<string, IDCreatorService>();

        /// <summary>
        /// 隐藏构造函数以阻止外界直接创建其对象，应该使用GetService方法获得对象
        /// </summary>
        /// <param name="sdsiteDoc"></param>
        private IDCreatorService(SdsiteXmlDocument sdsiteDoc)
        {
            this.SdsiteDocument = sdsiteDoc;
        }

        /// <summary>
        /// 根据SdsiteXmlDocument获得对应的IDCreatorService。
        /// </summary>
        /// <param name="sdsiteDoc"></param>
        /// <returns></returns>
        public static IDCreatorService GetService(SdsiteXmlDocument sdsiteDoc)
        {
            Debug.Assert(sdsiteDoc != null);

            IDCreatorService creator;
            if (!_dicIdCreators.TryGetValue(sdsiteDoc.AbsoluteFilePath, out creator))
            {
                creator = new IDCreatorService(sdsiteDoc);
                _dicIdCreators.Add(sdsiteDoc.AbsoluteFilePath, creator);
                sdsiteDoc.Closed += new EventHandler(sdsiteDoc_Closed);
            }

            return creator;
        }

        static void sdsiteDoc_Closed(object sender, EventArgs e)
        {
            ///将缓存的IDCreatorService从容器中删除
            SdsiteXmlDocument sdsiteDoc = sender as SdsiteXmlDocument;
            Debug.Assert(sdsiteDoc != null);
            _dicIdCreators.Remove(sdsiteDoc.AbsoluteFilePath);
        }

        #endregion

        //public string CreateCustomId(string prefix)
        //{
        //    //SdsiteDocument.GetElementById(

        //    return null;
        //}

        //public string CreateCustomId(string prefix,TmpltXmlDocument doc)
        //{
        //    return null;
        //}

        //private bool HasExits(string customId)
        //{
        //}

        //private bool HasExits(string customId, TmpltXmlDocument doc)
        //{
        //}
    }
}
