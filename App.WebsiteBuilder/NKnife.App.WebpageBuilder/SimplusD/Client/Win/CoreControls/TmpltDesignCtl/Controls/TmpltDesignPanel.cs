using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class TmpltDesignerPanel : DesignPanel
    {
        #region 属性成员定义

        /// <summary>
        /// 获取模板的ID
        /// </summary>
        public string TmpltID { get;private set; }

        //modified by zhucai in 2008年3月31日//不知对不对，看有警告就改了
        new TmpltXmlDocument TmpltDoc { get { return (TmpltXmlDocument)base.TmpltDoc; } set { base.TmpltDoc = value; } }

        Image _backImg;

        /// <summary>
        /// 修改过的SNIP 如增加的/修改的
        /// add by fenggy 2008-06-23
        /// </summary>
        //public Dictionary<string, SnipXmlElement> ModifiySnip;

        /// <summary>
        /// 模板文件一起始的DOC 不保存时恢复用  add by fenggy 2008-06-23
        /// </summary>
        public string strRectsData = String.Empty;

        #endregion

        #region 构造函数
        public TmpltDesignerPanel(int width, int height, Image backImage,TmpltXmlDocument doc)
            : base(width, height, backImage)
        {
            _backImg = backImage;
            TmpltID = doc.Id;
            InitializeComponent();
            InitEvents();
            TmpltDoc = doc;
            LoadTmplt(doc);

            strRectsData = TmpltDoc.GetRectsElement().InnerXml; 
        }

        #endregion

        #region 公共函数成员接口
        
        /// <summary>
        /// 载入模板数据
        /// </summary>
        /// <param name="tmpltID"></param>
        public void LoadTmplt(TmpltXmlDocument doc)
        {
            try
            {
                HasContentSnip = bool.Parse(doc.DocumentElement.GetAttribute("hasContent"));
            }
            catch (Exception)
            {
                HasContentSnip = false;
            }

            LoadLines(doc);
            LoadRects(doc);
            //DrawPanel.TmpltDoc = doc;
            ((TmpltDrawPanel)DrawPanel).TmpltDoc = doc;
            TmpltDoc = doc;
            DrawPanel.Invalidate();
        }

        /// <summary>
        /// 保存模板
        /// </summary>
        public void SaveTmplt(TmpltXmlDocument doc)
        {
            if (doc.HasContentSnip != HasContentSnip)
            {
                TmpltDoc.HasContentSnip = HasContentSnip;
                Internal.InternalService.OnTmpltDocumentHealthChanged(new EventArgs<string>(TmpltID));
            }            
            float x = DrawPanel.CurZoom;
            DrawPanel.CurZoom = 1;

            SaveLines();
            SaveRects();

            DrawPanel.CurZoom = x;

            doc.Save();

            //保存XML文档内容,如果恢复的话这里就是起点
            strRectsData = doc.GetRectsElement().InnerXml;

        }
        
        #endregion

        #region 私有函数        

        /// <summary>
        /// 保存线数据
        /// </summary>
        private void SaveLines()
        {
            //写入直线结点
            List<PartitionLine> allLines = new List<PartitionLine>(DrawPanel.ListLine.HPartionLines);
            allLines.AddRange(DrawPanel.ListLine.VPartionLines);

            for (int i = 0; i < 4; i++)
            {
                allLines.Remove(DrawPanel.ListLine.BorderLines[i]);
            }

            XmlElement docEle = (XmlElement)TmpltDoc.DocumentElement.SelectSingleNode("lines");
            XmlElement borderLineEle;
            XmlElement childLineEle;
            List<XmlElement> borderLineEles = new List<XmlElement>();
            List<XmlElement> lineEles = new List<XmlElement>();

            docEle.RemoveAll();
            foreach (PartitionLine bline in DrawPanel.ListLine.BorderLines)
            {
                borderLineEle =  TmpltDoc.CreateElement("borderLine");
                borderLineEle.SetAttribute("start",bline.Start.ToString());
                borderLineEle.SetAttribute("end", bline.End.ToString());
                borderLineEle.SetAttribute("position", bline.Position.ToString());
                borderLineEle.SetAttribute("isRow", bline.IsRow.ToString());

                if (bline.ChildLines != null)
                {
                    foreach (PartitionLine childLine in bline.ChildLines)
                    {
                        childLineEle = TmpltDoc.CreateElement("line");
                        childLineEle.SetAttribute("start",childLine.Start.ToString());
                        childLineEle.SetAttribute("end", childLine.End.ToString());
                        childLineEle.SetAttribute("position", childLine.Position.ToString());
                        childLineEle.SetAttribute("isRow", childLine.IsRow.ToString());

                        borderLineEle.AppendChild(childLineEle);
                    }
                }
                borderLineEles.Add(borderLineEle);
            }
            XmlElement lineEle;
            foreach (PartitionLine line in allLines)
            {
                lineEle = TmpltDoc.CreateElement("line");
                lineEle.SetAttribute("start", line.Start.ToString());
                lineEle.SetAttribute("end", line.End.ToString());
                lineEle.SetAttribute("position", line.Position.ToString());
                lineEle.SetAttribute("isRow", line.IsRow.ToString());
                if (line.ChildLines != null)
                {
                    foreach (PartitionLine childLine in line.ChildLines)
                    {
                        childLineEle = TmpltDoc.CreateElement("line");
                        childLineEle.SetAttribute("start", childLine.Start.ToString());
                        childLineEle.SetAttribute("end", childLine.End.ToString());
                        childLineEle.SetAttribute("position", childLine.Position.ToString());
                        childLineEle.SetAttribute("isRow", childLine.IsRow.ToString());

                        lineEle.AppendChild(childLineEle);
                    }
                }
                lineEles.Add(lineEle);
            }

            ///在文档中插入边界线节点
            if (borderLineEles == null || borderLineEles.Count <= 0)
                return;
            XmlElement linesElement = (XmlElement)TmpltDoc.DocumentElement.SelectSingleNode("lines");
            foreach (XmlElement ele in borderLineEles)
            {
                linesElement.AppendChild(ele);
            }
            ///

            ///在文档中插入线节点
            if (lineEles == null || lineEles.Count <= 0)
                return;
            foreach (XmlElement ele in lineEles)
            {
                linesElement.AppendChild(ele);
            }
            ///
        }

        /// <summary>
        /// 保存矩形
        /// </summary>
        public void SaveRects()
        {
            float x = DrawPanel.CurZoom;
            DrawPanel.CurZoom = 1;
            Dictionary<string, XmlElement> _tempSnips = TmpltDoc.GetAllSnipElementClone();
            XmlElement docEle = (XmlElement)TmpltDoc.DocumentElement.SelectSingleNode("rects");
            docEle.RemoveAll();

            RectLayer boundaryRect = new RectLayer(0, 0, DrawPanel.Width, DrawPanel.Height,TmpltDoc.TmpltCss);
            LineToRect(boundaryRect);
            

            AddRectToXml(docEle, new RectTreeNode(boundaryRect, null));
            
            XmlNodeList snipNodes = docEle.SelectNodes(@"//snip");
            foreach (XmlNode snipNode in snipNodes)
            {
                if (snipNode.NodeType == XmlNodeType.Element)
                {
                    SnipXmlElement snipEle = (SnipXmlElement)snipNode;
                    string id = snipEle.Id;
                    if (_tempSnips.ContainsKey(id))
                    {
                        CssSection tempCss = CssSection.Parse(snipEle.GetAttribute("css"));
                        XmlUtilService.CopyXmlElement(_tempSnips[id], snipEle);
                        snipEle.Width = tempCss.Properties["width"];
                        snipEle.Height = tempCss.Properties["height"];
                        snipEle.Css = tempCss.ToString();
                    }
                }
            }
            DrawPanel.CurZoom = x;

            TmpltDoc.Reseted = true;
        }

        /// <summary>
        /// 递归实现 矩形向Xml 节点的转换
        /// </summary>
        /// <param name="parentEle"></param>
        /// <param name="rect"></param>
        void AddRectToXml(XmlElement parentEle, RectTreeNode rect)
        {
            Rect resultRect = DrawPanel.ListRect.SnipRectList.Find(
                new FindRectByLayerRect(rect.RectLayer).PredicateEqualRect);

            if (resultRect != null)
            {
                //由一个矩形（SnipRect）生成一个XmlElement节点
                SnipRect srect = (SnipRect)resultRect;
                SnipXmlElement snipXmlEle = (SnipXmlElement)TmpltDoc.CreateElement("snip");//CreateSnip(srect.SnipID);
                SnipPartsXmlElement partsEle = (SnipPartsXmlElement)TmpltDoc.CreateElement("parts");
                snipXmlEle.AppendChild(partsEle);
                snipXmlEle.Id = srect.SnipID;
                snipXmlEle.SnipName = srect.SnipName;
                snipXmlEle.Title = srect.Title;
                snipXmlEle.Css = srect.ToCss;
                snipXmlEle.HasSnip = srect.HasSnip;
                snipXmlEle.SnipType = srect.SnipType;
                if (srect.SnipType == PageSnipType.Content)
                {
                    TmpltDoc.HasContentSnip  = true;
                }
                else
                {
                    TmpltDoc.HasContentSnip = false;
                }
                snipXmlEle.X = srect.X;
                snipXmlEle.Y = srect.Y;
                snipXmlEle.Width = srect.Width.ToString() + "px";
                snipXmlEle.Height = srect.Height.ToString() + "px";
                snipXmlEle.IsLocked = srect.IsLocked;
                rect.Element = snipXmlEle;
            }
            else
            {
                SnipRectXmlElement rectEle = (SnipRectXmlElement)TmpltDoc.CreateElement("rect");
                rectEle.Css = rect.RectLayer.Css;
                rectEle.IsRow = !rect.RectLayer.IsRow;
                rect.Element = rectEle;
                //如果有孩子，则继续遍历
                if (rect.RectLayer.ChildRects != null)
                {
                    foreach (RectLayer childRect in rect.RectLayer.ChildRects)
                    {
                        AddRectToXml(rect.Element, new RectTreeNode(childRect, null));
                    }
                }
            }
            parentEle.AppendChild(rect.Element);
        }

        /// <summary>
        /// 载入线数据
        /// </summary>
        /// <param name="tmpltID"></param>
        private void LoadLines(XmlDocument doc)
        {
            DrawPanel.ListLine.BorderLines.Clear();
            DrawPanel.ListLine.HPartionLines.Clear();
            DrawPanel.ListLine.VPartionLines.Clear();
            XmlNode docEle = doc.DocumentElement.SelectSingleNode("lines");
            XmlNodeList borderLineNodes = docEle.SelectNodes("borderLine");
            XmlNodeList childLineNodes;
            XmlNodeList lineNodes = docEle.SelectNodes("line");
            PartitionLine line;
            int start, end, pos;
            bool isRow;

            //载入四条边界线
            foreach (XmlNode borderLineNode in borderLineNodes)
            {
                XmlElement borderLineEle = (XmlElement)borderLineNode;
                start = Convert.ToInt32(borderLineEle.GetAttribute("start"));
                end = Convert.ToInt32(borderLineEle.GetAttribute("end"));
                pos = Convert.ToInt32(borderLineEle.GetAttribute("position"));
                isRow = bool.Parse(borderLineEle.GetAttribute("isRow"));
                line = new PartitionLine(start, end, pos, isRow);

                childLineNodes = borderLineNode.SelectNodes("line");
                if (childLineNodes != null && childLineNodes.Count > 1)
                {
                    line.ChildLines = new SDLinkedList<PartitionLine>();
                    foreach (XmlNode borderChild in childLineNodes)
                    {
                        XmlElement borderChildEle = (XmlElement)borderChild;
                        start = Convert.ToInt32(borderChildEle.GetAttribute("start"));
                        end = Convert.ToInt32(borderChildEle.GetAttribute("end"));

                        line.ChildLines.AddLast(new PartitionLine(start, end, line));
                    }
                }

                DrawPanel.ListLine.BorderLines.Add(line);
                DrawPanel.ListLine.AddLine(line);
            }
            //载入所有直线

            foreach (XmlNode lineNode in lineNodes)
            {
                XmlElement linEle = (XmlElement)lineNode;
                start = Convert.ToInt32(linEle.GetAttribute("start"));
                end = Convert.ToInt32(linEle.GetAttribute("end"));
                pos = Convert.ToInt32(linEle.GetAttribute("position"));
                isRow = bool.Parse(linEle.GetAttribute("isRow"));
                line = new PartitionLine(start, end, pos, isRow);

                childLineNodes = lineNode.SelectNodes("line");
                //如果有孩子线段,则读入孩子线段
                if (childLineNodes != null && childLineNodes.Count > 1)
                {
                    line.ChildLines = new SDLinkedList<PartitionLine>();
                    foreach (XmlNode childNode in childLineNodes)
                    {
                        XmlElement childLineEle = (XmlElement)childNode;
                        start = Convert.ToInt32(childLineEle.GetAttribute("start"));
                        end = Convert.ToInt32(childLineEle.GetAttribute("end"));

                        line.ChildLines.AddLast(new PartitionLine(start, end, line));
                    }
                }
                DrawPanel.ListLine.AddLine(line);
            }
        }

        /// <summary>
        /// 载入矩形数据
        /// </summary>
        /// <param name="tmpltID"></param>
        private void LoadRects(XmlDocument doc)
        {
            DrawPanel.ListRect.SnipRectList.Clear();

            XmlNodeList rectNodes = doc.DocumentElement.SelectNodes(@"//snip");
            SnipRect rect;
            int x, y, width, height;
            string id;

            //载入所有矩形
            foreach (XmlNode node in rectNodes)
            {
                if (node is XmlElement)
                {
                    XmlElement ele = (XmlElement)node;
                    id = ele.GetAttribute("id");
                    x = Utility.Convert.PxStringToInt(ele.GetAttribute("x"));
                    y = Utility.Convert.PxStringToInt(ele.GetAttribute("y"));
                    width = Utility.Convert.PxStringToInt(ele.GetAttribute("width"));
                    height = Utility.Convert.PxStringToInt(ele.GetAttribute("height"));
                    rect = new SnipRect(x, y, width, height, id);
                    //rect.IsSelected = Utility.Convert.StringToBool(ele.GetAttribute("isSelected"));
                    rect.IsLocked = Utility.Convert.StringToBool(ele.GetAttribute("isLocked"));
                    rect.HasSnip = Utility.Convert.StringToBool(ele.GetAttribute("hasSnip"));
                    rect.SnipType = (PageSnipType)Enum.Parse(typeof(PageSnipType), ele.GetAttribute("type"));
                    rect.SnipName = ele.GetAttribute("snipName");
                    rect.ToCss = ele.GetAttribute("css");
                    DrawPanel.ListRect.SnipRectList.Add(rect);
                }
            }
        }

        ///// <summary>
        ///// 生成div层次关系
        ///// </summary>
        //private void MakeDivLayer()
        //{
        //    RectLayer boundaryRect = new RectLayer(0, 0, DrawPanel.Width, DrawPanel.Height,TmpltDoc.TmpltCss);
        //    LineToRect(boundaryRect);
        //    WriteRectLayer(boundaryRect);
        //}

        ///// <summary>
        ///// 将层次化的矩形写入XML,使用树的宽度遍历算法
        ///// </summary>
        ///// <param name="rect"></param>
        //private void WriteRectLayer(RectLayer rect)
        //{
        //    XmlDocument xmlDoc = new XmlDocument();
        //    string str = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<body></body>";

        //    xmlDoc.LoadXml(str);
        //    //xmlDoc.Load(@"D:\shiyitang\div.xml");

        //    XmlElement xmlElement = xmlDoc.DocumentElement;
        //    XmlElement xmlEleTmp = null;

        //    Queue<RectTreeNode> que = new Queue<RectTreeNode>();
        //    que.Enqueue(new RectTreeNode(rect, xmlElement));

        //    RectTreeNode curNode = null;
        //    Rect resultRect = null;

        //    while (que.Count > 0)
        //    {
        //        //访问本结点
        //        curNode = que.Dequeue();
        //        resultRect = DrawPanel.ListRect.SnipRectList.Find(
        //            new FindRectByLayerRect(curNode.RectLayer).PredicateEqualRect);
        //        if (resultRect != null)
        //        {
        //            curNode.Element.SetAttribute("snippage_id", ((SnipRect)resultRect).SnipID);
        //        }

        //        //如果有孩子，则孩子结点入队
        //        if (curNode.RectLayer.ChildRects != null)
        //        {
        //            foreach (RectLayer childRect in curNode.RectLayer.ChildRects)
        //            {
        //                xmlEleTmp = xmlDoc.CreateElement("div");
        //                curNode.Element.AppendChild(xmlEleTmp);//置其为孩子

        //                que.Enqueue(new RectTreeNode(childRect, xmlEleTmp));
        //            }
        //        }
        //    }
        //    xmlDoc.Save(@"d:\" + TmpltID + @".xml");
        //}

        /// <summary>
        /// 递归实现将分割线转化为矩形
        /// </summary>
        /// <param name="snipRect"></param>
        private void LineToRect(RectLayer snipRect)
        {
            if (snipRect == null)
            {
                return;
            }

            FindLindByRectAndRow findRectPartion;    ///寻找能以给定方向(isRow)切割给定矩形之分割线
            FindLineByLine findLinePartion;    ///寻找能与给定直线正交之分割线
            FindLineByLine findLineTo;         /// 寻找终点位于给定直线之分割线
            List<PartitionLine> resultRectPartion;  ///找到与给定方向(isRow)切割给定矩形之分割线的结果集
            List<PartitionLine> resultLinePartion;  ///找到与给定直线正交之分割线的结果集
            RectLayer Rect;

            ///行优先寻找能横向切割矩形的分割线
            findRectPartion = new FindLindByRectAndRow(snipRect, true);
            resultRectPartion = DrawPanel.HPartionLines.FindAll(new Predicate<PartitionLine>(findRectPartion.Predicate));
            if (resultRectPartion.Count > 2)//找到横向切割线,2为两个边界线
            {
                snipRect.IsRow = true;
                resultRectPartion.Sort(new CompareLinePosition());
                if (snipRect.ChildRects == null)//如果还没有new出childRects,则创建
                {
                    snipRect.ChildRects = new List<RectLayer>();
                }

                ///循环生成页面矩形
                int i;
                for (i = 1; i < resultRectPartion.Count; i++)
                {
                    Rect = new RectLayer(snipRect.X,
                        resultRectPartion[i - 1].Position, 
                        snipRect.Width, 
                        resultRectPartion[i].Position - resultRectPartion[i - 1].Position,
                        "");
                    snipRect.ChildRects.Add(Rect);
                    LineToRect(Rect);
                }

            }
            else///没有横向切割线,则纵向寻找
            {
                snipRect.IsRow = false;
                findRectPartion = new FindLindByRectAndRow(snipRect, false);
                resultRectPartion = DrawPanel.VPartionLines.FindAll(new Predicate<PartitionLine>(findRectPartion.Predicate));

                if (resultRectPartion.Count > 2)///找到纵向切割线
                {
                    resultRectPartion.Sort(new CompareLinePosition());

                    if (snipRect.ChildRects == null)///如果还没有new出childRects,则创建
                    {
                        snipRect.ChildRects = new List<RectLayer>();
                    }
                    int i = 0, j = 0;
                    while (i < resultRectPartion.Count - 1)
                    {
                        PartitionLine conditionLine = new PartitionLine(
                            snipRect.Y, snipRect.Y + snipRect.Height, resultRectPartion[i].Position, false
                            );///此分割线实质上是此矩的左边界的向右偏移
                        findLinePartion = new FindLineByLine(conditionLine);
                        resultLinePartion = DrawPanel.HPartionLines.FindAll(new Predicate<PartitionLine>(findLinePartion.PredicatePartedLineRight));
                        if (resultLinePartion.Count > 0)///找到所有与给定线相交的分割线  
                        {
                            resultLinePartion.Sort(new CompareLineLen(resultRectPartion[i]));
                            findLineTo = new FindLineByLine(resultLinePartion[0]);
                            j = i;
                            i = resultRectPartion.FindLastIndex(
                                new Predicate<PartitionLine>(findLineTo.PredicateLineTo)
                                );
                            if (i == -1)///如果没有找到,说明此正交分割线没有终止于结果集的任何一条分割线
                            ///此时,结果应该是最后一条
                            {
                                i = resultRectPartion.Count - 1;
                            }
                            Rect = new RectLayer(
                                resultRectPartion[j].Position,
                                snipRect.Y,
                                resultRectPartion[i].Position - resultRectPartion[j].Position,
                                snipRect.Height,"");
                            snipRect.ChildRects.Add(Rect);
                            LineToRect(Rect);

                        }
                        else//没有与之下次之直线，则形成一个新矩形
                        {
                            j = i;
                            ++i;
                            Rect = new RectLayer(
                                resultRectPartion[j].Position,
                                snipRect.Y,
                                resultRectPartion[i].Position - resultRectPartion[j].Position,
                                snipRect.Height,"");
                            snipRect.ChildRects.Add(Rect);
                            LineToRect(Rect);
                        }
                    }
                }
            }
        }
       
        /// <summary>
        /// 初始化一些事件
        /// </summary>
        private void InitEvents()
        {
            DeleteLineEvent += new DeleleLineEventHandler(TmpltDesignerControlPanel_DeleteLineEvent);
            MergeRectEvent += new MergeRectEventHandler(TmpltDesignerControlPanel_MergeRectEvent);
            PartRectEvent += new PartRectEventHandler(TmpltDesignerControlPanel_PartRectEvent);
            DrawPanel.RectDeleted += new EventHandler<EventArgs<Rect>>(DrawPanel_RectDeleted);
        }

        /// <summary>
        /// 删除页面片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DrawPanel_RectDeleted(object sender, EventArgs<Rect> e)
        {
            e.Item.HasSnip = false;
            SnipXmlElement snipEle = ((TmpltXmlDocument)TmpltDoc).GetSnipElementById(e.Item.SnipID);
            if (snipEle.SnipType == PageSnipType.Content)
            {
                ((TmpltXmlDocument)TmpltDoc).HasContentSnip = false;
            }
            snipEle.HasSnip = false;
            snipEle.GetPartsElement().RemoveAll();
        }

        private void TmpltDesignerControlPanel_DeleteLineEvent(object sender, DeleteLineEventArgs e)
        {
            foreach (NeighbourRect rect in e.NeighbourRectList)
            {
                
            }
            ///提示用户保留每对矩形的哪个数据
            MergeRectForm mergeRectForm = new MergeRectForm(e.NeighbourRectList,_backImg);
            if (mergeRectForm.ShowDialog() == DialogResult.OK)
            {
                e.Cancel = false;
                ///执行删除操作,本应该在command.Execute里执行,但因为需要和mergeRectForm交互,便提前在此操作
                for (int i = 0; i < e.NeighbourRectList.Count; i++)
                {
                    if (((RadioButton)mergeRectForm.GroupBoxList[i].Controls[0]).Checked)
                    {
                        e.HoldedRectList.Add(e.NeighbourRectList[i].FirstRect);
                        e.RemovedRectList.Add(e.NeighbourRectList[i].SecondRect);
                    }
                    else
                    {
                        e.HoldedRectList.Add(e.NeighbourRectList[i].SecondRect);
                        e.RemovedRectList.Add(e.NeighbourRectList[i].FirstRect);
                    }
                }
                this.Invalidate();
            }
            else
            {
                e.Cancel = true;
            }
        }

        void TmpltDesignerControlPanel_MergeRectEvent(object sender, MergeRectEventArgs e)
        {
            MergeRectForm mergeRectForm = new MergeRectForm(e.SelectedRects,_backImg);

            if (mergeRectForm.ShowDialog() == DialogResult.OK)
            {
                e.Cancel = false;
                ///执行删除操作,本应该在command.Execute里执行,但因为需要和mergeRectForm交互,便提前在此操作
                e.HoldRect = mergeRectForm.HoldRect;// CommonFuns.GetChedIndex(mergeRectForm.GroupBoxList[0]);
                this.Invalidate();
            }
            else
            {
                e.Cancel = true;
            }
        }

        void TmpltDesignerControlPanel_PartRectEvent(object sender, PartRectEventArgs e)
        {
            PartRectForm partRectForm = new PartRectForm(e.SelectedRect);
            partRectForm.ShowDialog();
            if (partRectForm.DialogResult == DialogResult.OK)
            {
                e.Cancel = false;
                ///读入单选框和文本框内容
                e.IsRow = ((RadioButton)partRectForm.Controls["isRowRadioBtn"]).Checked;
                e.PartNum = partRectForm.PartNum;
            }
            else
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region 工厂模式的动态生成函数

        /// <summary>
        /// 重写基类之创建矩形函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        override public Rect CreateRect(int x, int y, int width, int height)
        {
            return new SnipRect(x, y, width, height, Guid.NewGuid().ToString("N"));
        }

        /// <summary>
        /// 工厂模式动态创建Rect
        /// </summary>
        override public Rect CreateRect(int x, int y, int width, int height, Rect rect)
        {
            SnipRect newSnipRect = new SnipRect(x, y, width, height, Guid.NewGuid().ToString("N"));
            ///复制页面片数据
            if (((SnipRect)rect).SnipData != null)
            {
                newSnipRect.SnipData = new SnipData(((SnipRect)rect).SnipData);
            }
            
            return newSnipRect;
        }

        /// <summary>
        /// 工厂模式动态创建DrawFrame
        /// </summary>
        protected override DrawFrame CreateDrawFrame(DesignPanel tD, int width, int height, Image backImage)
        {
            TmpltDrawFrame df = new TmpltDrawFrame(tD, width, height, backImage);
            df.TmpltDoc = TmpltDoc;
            return df;
        }
        
        /// <summary>
        /// 工厂模式动态创建HRuler
        /// </summary>
        protected override HRuler CreateHRuler(DesignPanel tD)
        {
            return new TmpltHRuler(tD);
        }

        /// <summary>
        /// 动态创建VRuler
        /// </summary>
        protected override VRuler CreateVRuler(DesignPanel tD)
        {
            return new TmpltVRuler(tD);
        }


        #endregion
    }

    /// <summary>
    /// 层次形矩形的树结点类
    /// </summary>
    public class RectTreeNode
    {
        public RectTreeNode(RectLayer rect, XmlElement xmlEle)
        {
            this.Element = xmlEle;
            this.RectLayer = rect;
        }
        public XmlElement Element;
        /// <summary>
        /// edit by zhenghao at 2008-06-24 9:50 : 加了注释
        /// 层级矩形
        /// </summary>
        public RectLayer RectLayer;
    };
}
