using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 除snipID,snipName以外的所有页面片数据
    /// </summary>
    [Serializable]
    public class SnipData
    {
        public string SnipElementText { get; set; }
        #region 构造函数

        public SnipData(SnipData snipData)
        {
            SnipElementText = snipData.SnipElementText;
        }

        public SnipData(XmlElement snipElement)
        {
            SnipElementText = snipElement.OuterXml;
        }

        #endregion

        /// <summary>
        ///  拷贝到剪贴板
        /// </summary>
        public void CopyToClipboard()
        {
            DataFormats.Format format =
                 DataFormats.GetFormat(typeof(SnipData).FullName);

            //now copy to clipboard
            IDataObject dataObj = new DataObject();
            dataObj.SetData(format.Name,false, this);            
            Clipboard.SetDataObject(dataObj, true);
        }

        /// <summary>
        /// 粘贴自剪贴板
        /// </summary>
        /// <returns></returns>
        public static SnipData GetFromClipboard()
        {
            SnipData snipData = null;
            IDataObject dataObj = Clipboard.GetDataObject();
            string format = typeof(SnipData).FullName;
            
            if (dataObj.GetDataPresent(format))
            {
                snipData = dataObj.GetData(format) as SnipData;
            }
            return snipData;
        }
    }
}
