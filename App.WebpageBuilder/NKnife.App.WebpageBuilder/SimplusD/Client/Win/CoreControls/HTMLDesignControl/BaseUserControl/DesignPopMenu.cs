using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using mshtml;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class DesignPopMenu : ContextMenuStrip
    {
        /// <summary>
        /// 正文设计器设计窗口弹出菜单
        /// </summary>
        HTMLEditHelper _htmledit = new HTMLEditHelper();
        IHTMLDocument2 idoc2 = null;
        IHTMLElement currentEle = null;
        HTMLDesignControl HTMLDesign = null;


        ToolStripMenuItem copyToolStripMenuItem = null;//复制
        ToolStripMenuItem cutToolStripMenuItem = null;//剪切
        ToolStripMenuItem pasteToolStripMenuItem = null;//粘贴
        ToolStripMenuItem findToolStripMenuItem = null;//粘贴

        ToolStripMenuItem tableDropToolStripMenuItem = null;//表格
        ToolStripMenuItem delcolToolStripMenuItem = null;//删除列
        ToolStripMenuItem inscolToolStripMenuItem = null;//新增列
        ToolStripMenuItem delrowToolStripMenuItem = null;//删除行
        ToolStripMenuItem insrowToolStripMenuItem = null;//新增行
        ToolStripMenuItem splitToolStripMenuItem = null;//拆分
        ToolStripMenuItem mergerToolStripMenuItem = null;//合并
        ToolStripMenuItem otherToolStripMenuItem = null;//其他

        ToolStripMenuItem insimgToolStripMenuItem = null;//插入图片
        ToolStripMenuItem inslinkToolStripMenuItem = null;//插入链接

        ToolStripMenuItem imgproToolStripMenuItem = null;//图片属性
        ToolStripMenuItem activeproToolStripMenuItem = null;//FLASH,媒体属性
        ToolStripMenuItem lineproToolStripMenuItem = null;//只想属性
        ToolStripMenuItem hyperlinkToolStripMenuItem = null;//超链接属性

        ToolStripSeparator ts1 = new ToolStripSeparator();
        ToolStripSeparator ts2 = new ToolStripSeparator();
        ToolStripSeparator ts3 = new ToolStripSeparator();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="contendID">正文ID</param>
        /// <param name="type">处理的HTML元素;类型</param>
        /// <param name="htmlDesign"></param>
        public DesignPopMenu( string type, HTMLDesignControl htmlDesign)
        {
            InitializeComponent();
            HTMLDesign = htmlDesign;
            idoc2 = htmlDesign.Idoc2;
            currentEle = htmlDesign.CurrentElement;
            InitMy(type);
        }


        /// <summary>
        /// 初始化设计窗口菜单
        /// </summary>
        /// <param name="type"></param>
        /// <param name="htmlDesign"></param>
        /// <returns></returns>
        public void InitMy(string type)
        {

            #region item初始化
            copyToolStripMenuItem = new ToolStripMenuItem();
            cutToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            findToolStripMenuItem = new ToolStripMenuItem();

            tableDropToolStripMenuItem = new ToolStripMenuItem();
            delcolToolStripMenuItem = new ToolStripMenuItem();
            inscolToolStripMenuItem = new ToolStripMenuItem();
            delrowToolStripMenuItem = new ToolStripMenuItem();
            insrowToolStripMenuItem = new ToolStripMenuItem();
            splitToolStripMenuItem = new ToolStripMenuItem();
            mergerToolStripMenuItem = new ToolStripMenuItem();
            otherToolStripMenuItem = new ToolStripMenuItem();

            insimgToolStripMenuItem = new ToolStripMenuItem();
            inslinkToolStripMenuItem = new ToolStripMenuItem();

            imgproToolStripMenuItem = new ToolStripMenuItem();
            activeproToolStripMenuItem = new ToolStripMenuItem();
            lineproToolStripMenuItem = new ToolStripMenuItem();
            hyperlinkToolStripMenuItem = new ToolStripMenuItem();

            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Text = ResourceService.GetResourceText("DesigncontextMenuStrip.base.copy");
            copyToolStripMenuItem.Image = ResourceService.GetResourceImage("MainMenu.edit.copy");

            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.Text = ResourceService.GetResourceText("DesigncontextMenuStrip.base.cut");
            cutToolStripMenuItem.Image = ResourceService.GetResourceImage("MainMenu.edit.cut");

            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Text = ResourceService.GetResourceText("DesigncontextMenuStrip.base.paste");
            pasteToolStripMenuItem.Image = ResourceService.GetResourceImage("MainMenu.edit.paste");

            findToolStripMenuItem.Name = "findToolStripMenuItem";
            findToolStripMenuItem.Text = "查找";// ResourceService.GetResourceText("DesigncontextMenuStrip.base.paste");

            tableDropToolStripMenuItem.Name = "tableToolStripMenuItem";
            tableDropToolStripMenuItem.Text =ResourceService.GetResourceText("DesigncontextMenuStrip.table.tb");
            tableDropToolStripMenuItem.Image = ResourceService.GetResourceImage("table");

            delcolToolStripMenuItem.Name = "delcolToolStripMenuItem";
            delcolToolStripMenuItem.Text =ResourceService.GetResourceText("DesigncontextMenuStrip.table.delcol");

            delrowToolStripMenuItem.Name = "delrowToolStripMenuItem";
            delrowToolStripMenuItem.Text =ResourceService.GetResourceText("DesigncontextMenuStrip.table.delrow");

            insrowToolStripMenuItem.Name = "insrowToolStripMenuItem";
            insrowToolStripMenuItem.Text =ResourceService.GetResourceText("DesigncontextMenuStrip.table.insrow");

            inscolToolStripMenuItem.Name = "inscolToolStripMenuItem";
            inscolToolStripMenuItem.Text =ResourceService.GetResourceText("DesigncontextMenuStrip.table.inscol");

            splitToolStripMenuItem.Name = "splitToolStripMenuItem";
            splitToolStripMenuItem.Text =ResourceService.GetResourceText("DesigncontextMenuStrip.table.tds");

            mergerToolStripMenuItem.Name = "mergerToolStripMenuItem";
            mergerToolStripMenuItem.Text =ResourceService.GetResourceText("DesigncontextMenuStrip.table.tdm");

            otherToolStripMenuItem.Name = "otherToolStripMenuItem";
            otherToolStripMenuItem.Text = ResourceService.GetResourceText("DesigncontextMenuStrip.table.tbo");

            insimgToolStripMenuItem.Name = "insimgToolStripMenuItem";
            insimgToolStripMenuItem.Text = ResourceService.GetResourceText("DesigncontextMenuStrip.insert.insimg");
            insimgToolStripMenuItem.Image = ResourceService.GetResourceImage("pictrue");

            inslinkToolStripMenuItem.Name = "inslinkToolStripMenuItem";
            inslinkToolStripMenuItem.Text = ResourceService.GetResourceText("DesigncontextMenuStrip.insert.inslink");
            inslinkToolStripMenuItem.Image = ResourceService.GetResourceImage("link");

            imgproToolStripMenuItem.Name = "imgproToolStripMenuItem";
            imgproToolStripMenuItem.Text = ResourceService.GetResourceText("DesigncontextMenuStrip.image.imgpro");

            activeproToolStripMenuItem.Name = "activeproToolStripMenuItem";
            activeproToolStripMenuItem.Text = ResourceService.GetResourceText("DesigncontextMenuStrip.flash.activepro");

            lineproToolStripMenuItem.Name = "lineproToolStripMenuItem";
            lineproToolStripMenuItem.Text = ResourceService.GetResourceText("DesigncontextMenuStrip.line.linepro");

            hyperlinkToolStripMenuItem.Name = "hyperlinkToolStripMenuItem";
            hyperlinkToolStripMenuItem.Text = ResourceService.GetResourceText("DesigncontextMenuStrip.hyperlink.hyperlinkpro");

            #endregion

            tableDropToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { delcolToolStripMenuItem, delrowToolStripMenuItem, inscolToolStripMenuItem, insrowToolStripMenuItem,ts2,
                splitToolStripMenuItem,mergerToolStripMenuItem,ts3,otherToolStripMenuItem });

            this.Items.AddRange(new ToolStripItem[] {
                copyToolStripMenuItem,cutToolStripMenuItem,pasteToolStripMenuItem,
                findToolStripMenuItem,
                ts1,
                inslinkToolStripMenuItem,insimgToolStripMenuItem,tableDropToolStripMenuItem
             });
            tableDropToolStripMenuItem.DropDownItemClicked += new ToolStripItemClickedEventHandler(tableToolStripMenuItem_DropDownItemClicked);

            this.Name = "designContextMenuStrip";
            this.Size = new Size(153, 70);
            this.SuspendLayout();
            this.ItemClicked += new ToolStripItemClickedEventHandler(designContextMenuStrip_ItemClicked);

            
            InitItemsVisual();//先将所有项都设置为不可见
            SetDesignContextMenuEnable();
            MenuSwitch(type);//根据类型设置可见的项目
        }

        /// <summary>
        /// 表格弹出菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tableToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "delcolToolStripMenuItem":
                    {
                        GeneralMethodsForDesign.deleteColumn(currentEle);
                    } break;
                case "inscolToolStripMenuItem":
                    {
                        GeneralMethodsForDesign.insertColumn(currentEle);
                    } break;
                case "delrowToolStripMenuItem":
                    {
                        GeneralMethodsForDesign.deleteRow(currentEle);
                        break;
                    }
                case "insrowToolStripMenuItem":
                    {
                        GeneralMethodsForDesign.insertRow(currentEle);
                        break;
                    }
                case "splitToolStripMenuItem":
                    {
                        #region
                        HTMLEditHelper hp = new HTMLEditHelper();
                        IHTMLTable tb =hp.GetParentTable(currentEle);
                        IHTMLTableRow tr =hp.GetParentRow(currentEle);
                        IHTMLTableCell td = currentEle as IHTMLTableCell;
                        int rowindex = tr.rowIndex;
                        int rowcount = hp.GetRowCount(tb);
                        int colindex = td.cellIndex;
                        hp.Row_InsertCell(tr, colindex - 1);
                        int currentSpan = 0;
                        for (int k = 0; k < colindex; k++)
                        {
                            currentSpan += (hp.Row_GetCell(tr, colindex) as IHTMLTableCell).colSpan;
                        }
                        for (int i = 0; i < rowcount; i++)
                        {
                            if (i != rowindex)
                            {
                                IHTMLTableRow tr0 = hp.GetRow(tb, i) as IHTMLTableRow;
                                IHTMLTableCell td0 = hp.Row_GetCell(tr0, colindex) as IHTMLTableCell;
                                int rowSpan = 0;
                                int cellnum = 0;
                                do
                                {
                                    rowSpan += (hp.Row_GetCell(tr0, cellnum) as IHTMLTableCell).colSpan;
                                    td0 = hp.Row_GetCell(tr0, cellnum) as IHTMLTableCell;
                                    cellnum++;
                                }
                                while (rowSpan <= currentSpan);

                                if (td0.colSpan > 0)
                                    td0.colSpan += 1;
                            }
                            else
                                continue;
                        }
                        break;
                        #endregion
                    }
                case "mergerToolStripMenuItem":
                    {
                        #region
                        HTMLEditHelper hp = new HTMLEditHelper();

                       /* IHTMLTableRow tr = hp.GetParentRow(currentEle);
                        int colNum = hp.Row_GetCellCount(tr);
                        MergeCellForm mergeCel = new MergeCellForm(colNum);
                        if (mergeCel.ShowDialog() == DialogResult.OK)
                        {

                        }*/

                        IHTMLTableCell c1 = currentEle as IHTMLTableCell;
                        mshtml.IHTMLDOMNode n1 = c1 as mshtml.IHTMLDOMNode;
                        IHTMLTableCell c2 = hp.PreviousSibling(n1) as IHTMLTableCell;
                        IHTMLTableCell c3 = hp.NextSibiling(n1) as IHTMLTableCell;
                        IHTMLElement e1 = c1 as IHTMLElement;
                        IHTMLElement e2 = c2 as IHTMLElement;
                        IHTMLElement e3 = c3 as IHTMLElement;

                        int span = c1.colSpan;
                        string tdtext = e1.innerHTML;
                        if (c2 != null)
                        {
                            span += c2.colSpan;
                            tdtext = e2.innerText + tdtext;
                            hp.RemoveNode(e2, false);
                        }
                        if (c3 != null)
                        {
                            span += c3.colSpan;
                            tdtext += e3.innerText;
                            hp.RemoveNode(e3, false);
                        }
                        c1.colSpan = span;
                        e1.innerHTML = tdtext;
                        break;
                        #endregion
                    }
                case "otherToolStripMenuItem":
                    {
                        break;
                    }
            }
        }



        /// <summary>
        /// 设计窗口弹出菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void designContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            InsertElementHelper insert = new InsertElementHelper();
            switch (e.ClickedItem.Name)
            {
                case "copyToolStripMenuItem": idoc2.execCommand("Copy", true, true); break;
                case "cutToolStripMenuItem": idoc2.execCommand("Cut", true, true); break;
                case "pasteToolStripMenuItem": idoc2.execCommand("Paste", true, true); break;
                case "tableToolStripMenuItem":
                    {
                        foreach (ToolStripItem ts in ((ToolStripMenuItem)e.ClickedItem).DropDownItems)
                        {
                            if (ts.Visible == true)
                            {
                                return;
                            }
                        }
                        InsertElementHelper.Inserttable(idoc2);
                        HTMLDesign.DesignToCode();
                        break;
                    }
                case "insimgToolStripMenuItem":
                    {
                        InsertElementHelper.InsertImage(idoc2 );
                        break;
                    }
                case "inslinkToolStripMenuItem":
                    {
                        InsertElementHelper.Insertlink(idoc2,HTMLDesign);
                        break;
                    }
                case "imgproToolStripMenuItem":
                    {

                        break;
                    }
                case "activeproToolStripMenuItem":
                    {
                        break;
                    }
                case "findToolStripMenuItem":
                    {
                        insert.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_FIND, idoc2);
                        break;
                    }
            }
            if (((ToolStripMenuItem)e.ClickedItem).DropDownItems.Count == 0)
            ((ContextMenuStrip)sender).Close();
           /* if (HTMLDesign.DesignWebBrowser.Focused)
            {
                HTMLDesign.DesignToCode();
                HTMLDesign.CodeToDesign();
            }

            else
            {
                HTMLDesign.CodeToDesign();
            }*/
        }




        void MenuSwitch(string type)
        {
            //然后根据不同的元素显示不同的菜单项
            switch (type)
            {
                case "P":
                case "BODY":
                case "SPAN":
                    {
                        copyToolStripMenuItem.Visible = cutToolStripMenuItem.Visible =
                            pasteToolStripMenuItem.Visible = inslinkToolStripMenuItem.Visible =
                         insimgToolStripMenuItem.Visible = true;
                        break;
                    }
                case "TD":
                    {
                        copyToolStripMenuItem.Visible = cutToolStripMenuItem.Visible =
                        pasteToolStripMenuItem.Visible = delcolToolStripMenuItem.Visible =
                        delrowToolStripMenuItem.Visible = inscolToolStripMenuItem.Visible =
                        insrowToolStripMenuItem.Visible = insimgToolStripMenuItem.Visible =
                        inslinkToolStripMenuItem.Visible = splitToolStripMenuItem.Visible =
                        mergerToolStripMenuItem.Visible= true;
                        break;
                    }

                case "TABLE":
                    {
                        copyToolStripMenuItem.Visible = cutToolStripMenuItem.Visible =
                        pasteToolStripMenuItem.Visible = delcolToolStripMenuItem.Visible =
                        delrowToolStripMenuItem.Visible = inscolToolStripMenuItem.Visible =
                        insrowToolStripMenuItem.Visible = insimgToolStripMenuItem.Visible =
                        inslinkToolStripMenuItem.Visible = true;
                        break;
                    }
                case "IMG":
                    {
                        copyToolStripMenuItem.Visible = cutToolStripMenuItem.Visible =
                        pasteToolStripMenuItem.Visible = inslinkToolStripMenuItem.Visible =
                         imgproToolStripMenuItem.Visible = true;
                        break;
                    }
                case "OBJECT":
                    {
                        copyToolStripMenuItem.Visible=cutToolStripMenuItem.Visible=
                        pasteToolStripMenuItem.Visible= inslinkToolStripMenuItem.Visible=
                        insimgToolStripMenuItem.Visible=true;
                        break;
                    }
                case "A":
                    {
                        copyToolStripMenuItem.Visible = cutToolStripMenuItem.Visible =
                        pasteToolStripMenuItem.Visible = hyperlinkToolStripMenuItem.Visible = true;
                        break;
                    }
                case "HR":
                    {
                        copyToolStripMenuItem.Visible= cutToolStripMenuItem.Visible=
                        pasteToolStripMenuItem.Visible=true;
                        break;
                    }
            }
        }

        /// <summary>
        /// 设置复制,剪切,粘贴的可用性
        /// </summary>
        /// <param name="idoc"></param>
        /// <param name="designPopMenu"></param>
        public void SetDesignContextMenuEnable()
        {
            copyToolStripMenuItem.Enabled = idoc2.queryCommandEnabled("Copy");
            cutToolStripMenuItem.Enabled = idoc2.queryCommandEnabled("Cut");
            pasteToolStripMenuItem.Enabled = idoc2.queryCommandEnabled("Paste");
            //inslinkToolStripMenuItem.Enabled = idoc2.queryCommandEnabled("Copy");
        }

        /// <summary>
        /// 将指定的item置为可见
        /// </summary>
        /// <param name="visible"></param>
        /// <param name="items"></param>
        void SetdesignContextMenuVisible(params ToolStripMenuItem[] items)
        {
            if (items == null)
            {
                return;
            }
            foreach (ToolStripMenuItem item in items)
            {
                item.Visible = true;
            }
        }

        void InitItemsVisual()
        {
             copyToolStripMenuItem.Visible=false;
             cutToolStripMenuItem.Visible = false;
             pasteToolStripMenuItem.Visible = false;

             tableDropToolStripMenuItem.Visible = false;
             delcolToolStripMenuItem.Visible = false;
             inscolToolStripMenuItem.Visible = false;
             delrowToolStripMenuItem.Visible = false;
             insrowToolStripMenuItem.Visible = false;
             splitToolStripMenuItem.Visible = false;
             mergerToolStripMenuItem.Visible = false;
             otherToolStripMenuItem.Visible = false;

             insimgToolStripMenuItem.Visible = false;
             inslinkToolStripMenuItem.Visible = false;

             imgproToolStripMenuItem.Visible = false;
             activeproToolStripMenuItem.Visible = false;
             lineproToolStripMenuItem.Visible = false;
             hyperlinkToolStripMenuItem.Visible = false;

             ts1.Visible = ts2.Visible = ts3.Visible = false;
        }
    }
}
