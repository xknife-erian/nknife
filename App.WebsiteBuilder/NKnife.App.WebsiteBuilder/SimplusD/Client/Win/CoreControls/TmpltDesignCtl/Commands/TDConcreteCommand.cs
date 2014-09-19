/*
 * TmpltDesignerControl的具体命令的实现
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 粘贴页面片数据
    /// </summary>
    public class PasteSnipDataCommand : BaseCommand
    {
        #region 属性成员定义

        List<Rect> PastedSnipRects;

        Dictionary<Rect,SnipData> OldSnipDatas = new Dictionary<Rect,SnipData>();

        SnipData NewSnipData;

        TmpltXmlDocument Doc;

        DesignPanel dPanel;

        #endregion

        #region 构造函数

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
            
            CommandInfo = "粘贴页面片数据";
        }

        #endregion

        #region 公共函数成员接口

        /// <summary>
        /// 实现执行函数
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
    /// 更改页面片属性
    /// </summary>
    public class ChangeSnipPerpertyCommand : BaseCommand
    {
        #region 属性成员定义

        public SnipRect PastedSnipRect { get; set; }

        public SnipData OldSnipData { get; set; }

        public SnipData NewSnipData { get; set; }

        #endregion

        #region 构造函数

        public ChangeSnipPerpertyCommand(DesignPanel tdPanel, SnipRect pastedSnipRect, SnipData newData)
        {
            PastedSnipRect = pastedSnipRect;
            NewSnipData = newData;
            OldSnipData = new SnipData(pastedSnipRect.SnipData);

            TDPanel = tdPanel;
            TDPanel.Modified = true;
            CommandInfo = "更改页面片属性";
        }

        #endregion

        #region 公共函数成员接口

        /// <summary>
        /// 实现执行函数
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