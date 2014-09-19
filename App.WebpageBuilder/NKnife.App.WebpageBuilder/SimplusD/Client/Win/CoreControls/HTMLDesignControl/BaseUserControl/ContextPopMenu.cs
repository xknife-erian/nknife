using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class ContextPopMenu : ContextMenuStrip
    {
        /// <summary>
        /// 正文设计器代码窗口弹出菜单
        /// </summary>
        TextEditorControl codeTextEditorControl = null;

        ToolStripMenuItem copyToolStripMenuItem = null;//复制
        ToolStripMenuItem cutToolStripMenuItem = null;//剪切
        ToolStripMenuItem pasteToolStripMenuItem = null;//粘贴
        ToolStripMenuItem undoToolStripMenuItem = null;//取消上一步
        ToolStripMenuItem redoToolStripMenuItem = null;//从做上一步
        //ToolStripMenuItem searchToolStripMenuItem = null;//查找
        //ToolStripMenuItem sear_replaceToolStripMenuItem = null;//查找替换
        ToolStripMenuItem selectallToolStripMenuItem = null;//全选
        
        public ContextPopMenu( TextEditorControl codeTextEditor)
        {
            InitializeComponent();
            codeTextEditorControl = codeTextEditor;
            InitMy();
        }

        public void InitMy()
        {
            #region item初始化
            copyToolStripMenuItem = new ToolStripMenuItem();
            cutToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            //searchToolStripMenuItem = new ToolStripMenuItem();
            //sear_replaceToolStripMenuItem = new ToolStripMenuItem();
            selectallToolStripMenuItem = new ToolStripMenuItem();

            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Text = ResourceService.GetResourceText("CodecontextMenuStrip.ccp.copy");
            copyToolStripMenuItem.Image = ResourceService.GetResourceImage("MainMenu.edit.copy");

            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.Text = ResourceService.GetResourceText("CodecontextMenuStrip.ccp.cut");
            cutToolStripMenuItem.Image = ResourceService.GetResourceImage("MainMenu.edit.cut");

            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Text = ResourceService.GetResourceText("CodecontextMenuStrip.ccp.paste");
            pasteToolStripMenuItem.Image = ResourceService.GetResourceImage("MainMenu.edit.paste");

            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Text = ResourceService.GetResourceText("CodecontextMenuStrip.unredo.undo");
            undoToolStripMenuItem.Image = ResourceService.GetResourceImage("MainMenu.edit.undo");

            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Text = ResourceService.GetResourceText("CodecontextMenuStrip.unredo.redo");
            redoToolStripMenuItem.Image = ResourceService.GetResourceImage("MainMenu.edit.redo");

            //searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            //searchToolStripMenuItem.Text = ResourceService.GetResourceText("CodecontextMenuStrip.findreplace.find");
            //searchToolStripMenuItem.Image = ResourceService.GetResourceImage("MainMenu.edit.find");

            //sear_replaceToolStripMenuItem.Name = "sear_replaceToolStripMenuItem";
            //sear_replaceToolStripMenuItem.Text = ResourceService.GetResourceText("CodecontextMenuStrip.findreplace.replace");
            //sear_replaceToolStripMenuItem.Image = ResourceService.GetResourceImage("MainMenu.edit.replace");

            selectallToolStripMenuItem.Name = "selectallToolStripMenuItem";
            selectallToolStripMenuItem.Text = ResourceService.GetResourceText("CodecontextMenuStrip.select.text");

            ToolStripSeparator tscSeparator1 = new ToolStripSeparator();
            ToolStripSeparator tscSeparator2 = new ToolStripSeparator();
            ToolStripSeparator tscSeparator3 = new ToolStripSeparator();
            ToolStripSeparator tscSeparator4 = new ToolStripSeparator();

            #endregion
            this.Items.AddRange(new ToolStripItem[] {
                copyToolStripMenuItem,cutToolStripMenuItem,pasteToolStripMenuItem, tscSeparator1,
                undoToolStripMenuItem,redoToolStripMenuItem, tscSeparator2,    
               // searchToolStripMenuItem,sear_replaceToolStripMenuItem,tscSeparator3,
                 selectallToolStripMenuItem
                });
            this.Name = "codeContextMenuStrip";
            this.Size = new Size(153, 170);
            this.SuspendLayout();
            this.ItemClicked += new ToolStripItemClickedEventHandler(codeContextMenuStrip_ItemClicked);
        }


        /// <summary>
        /// 代码窗口弹出菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void codeContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "copyToolStripMenuItem":
                    {
                        codeTextEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(null, null);
                        break;
                    }
                case "cutToolStripMenuItem":
                    {
                        codeTextEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(null, null);
                        break;
                    }
                case "pasteToolStripMenuItem":
                    {
                        codeTextEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(null, null);
                    };
                    break;
                case "undoToolStripMenuItem":
                    {
                        if (codeTextEditorControl.EnableUndo)
                            codeTextEditorControl.Undo();
                    } break;
                case "redoToolStripMenuItem":
                    {
                        if (codeTextEditorControl.EnableRedo)
                            codeTextEditorControl.Redo();
                    }
                    break;
                case "selectallToolStripMenuItem":
                    {
                        TextArea textArea = codeTextEditorControl.ActiveTextAreaControl.TextArea;
                        Point startPoint = new Point(0, 0);
                        Point endPoint = codeTextEditorControl.Document.OffsetToPosition(codeTextEditorControl.Document.TextLength);
                        textArea.SelectionManager.SetSelection(new DefaultSelection(codeTextEditorControl.Document, startPoint, endPoint));
                    }
                    break;
            }
        }
    }
}
