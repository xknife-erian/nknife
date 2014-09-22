using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// ��snipID,snipName���������ҳ��Ƭ����
    /// </summary>
    [Serializable]
    public class SnipData
    {
        public string SnipElementText { get; set; }
        #region ���캯��

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
        ///  ������������
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
        /// ճ���Լ�����
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
