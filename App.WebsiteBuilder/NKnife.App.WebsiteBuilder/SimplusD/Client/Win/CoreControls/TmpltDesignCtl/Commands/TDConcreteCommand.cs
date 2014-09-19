/*
 * TmpltDesignerControl�ľ��������ʵ��
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// ճ��ҳ��Ƭ����
    /// </summary>
    public class PasteSnipDataCommand : BaseCommand
    {
        #region ���Գ�Ա����

        List<Rect> PastedSnipRects;

        Dictionary<Rect,SnipData> OldSnipDatas = new Dictionary<Rect,SnipData>();

        SnipData NewSnipData;

        TmpltXmlDocument Doc;

        DesignPanel dPanel;

        #endregion

        #region ���캯��

        public PasteSnipDataCommand(DesignPanel tdPanel, List<Rect> pastedSnipRects, SnipData newData ,TmpltXmlDocument doc)
        {
            PastedSnipRects = pastedSnipRects;
            Doc = doc;
            NewSnipData = newData;
            dPanel = tdPanel;
            foreach (Rect rect in PastedSnipRects)
            {
                OldSnipDatas[rect] = rect.SnipData;
            }
            
            CommandInfo = "ճ��ҳ��Ƭ����";
        }

        #endregion

        #region ����������Ա�ӿ�

        /// <summary>
        /// ʵ��ִ�к���
        /// </summary>
        override public void Execute()
        {
            if (NewSnipData == null)
            {
                return;
            }
            TmpltDesignerPanel tDpanel = dPanel as TmpltDesignerPanel;
            tDpanel.SaveRects();
            foreach (Rect rect in PastedSnipRects)
            {
                rect.SnipData = new SnipData(NewSnipData);
                SnipXmlElement newEle = (SnipXmlElement)Doc.ParseElement(rect.SnipData.SnipElementText);
                if (Doc.HasContentSnip && newEle.SnipType == PageSnipType.Content)
                {
                    MessageService.Show("${res:tmpltDesign.tmpltDrawPanel.message.hasContentSnip}");
                    return;
                }
                SnipXmlElement ele = Doc.GetSnipElementById(rect.SnipID);
                ele.SelectSingleNode("parts").RemoveAll();
                ele.SetAttribute("hasSnip", newEle.GetAttribute("hasSnip"));
                ele.SetAttribute("type", newEle.GetAttribute("type"));

                XmlNodeList nodes = newEle.SelectSingleNode("parts").SelectNodes("//part");
                foreach (XmlNode partNode in nodes)
                {
                    (partNode as XmlElement).SetAttribute("partId", XmlUtilService.CreateIncreaseId());
                    ele.SelectSingleNode("parts").AppendChild(partNode);
                }
                rect.HasSnip = true;
            }
            dPanel.Modified = true;
        }

        override public void UnExecute()
        {
            foreach (Rect rect in PastedSnipRects)
            {
                rect.SnipData = OldSnipDatas[rect];
                SnipXmlElement ele = Doc.GetSnipElementById(rect.SnipID);
                ele = (SnipXmlElement)Doc.ParseElement(rect.SnipData.SnipElementText);
            }
        }

        #endregion
    }

    /// <summary>
    /// ����ҳ��Ƭ����
    /// </summary>
    public class ChangeSnipPerpertyCommand : BaseCommand
    {
        #region ���Գ�Ա����

        public SnipRect PastedSnipRect { get; set; }

        public SnipData OldSnipData { get; set; }

        public SnipData NewSnipData { get; set; }

        #endregion

        #region ���캯��

        public ChangeSnipPerpertyCommand(DesignPanel tdPanel, SnipRect pastedSnipRect, SnipData newData)
        {
            PastedSnipRect = pastedSnipRect;
            NewSnipData = newData;
            OldSnipData = new SnipData(pastedSnipRect.SnipData);

            TDPanel = tdPanel;
            TDPanel.Modified = true;
            CommandInfo = "����ҳ��Ƭ����";
        }

        #endregion

        #region ����������Ա�ӿ�

        /// <summary>
        /// ʵ��ִ�к���
        /// </summary>
        override public void Execute()
        {
            PastedSnipRect.SnipData = new SnipData(NewSnipData);
        }

        override public void UnExecute()
        {
            PastedSnipRect.SnipData = new SnipData(OldSnipData);
        }

        #endregion
    }
}