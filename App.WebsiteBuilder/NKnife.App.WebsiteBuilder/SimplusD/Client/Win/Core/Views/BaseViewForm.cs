using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 所有视图窗口的基类
    /// </summary>
    abstract public class BaseViewForm : DockContent
    {
        protected ContextMenuStrip _contextMenuStrip = new ContextMenuStrip();

        ToolStripMenuItem saveMenuItem;
        ToolStripMenuItem closeMenuItem;
        ToolStripMenuItem closeAllWithoutThisMenu;
        ToolStripMenuItem gotoTreeMenuItem;

        protected BaseViewForm()
        {
            //顶部的上下文菜单
            saveMenuItem = new ToolStripMenuItem("保存");
            closeMenuItem = new ToolStripMenuItem("关闭");
            closeAllWithoutThisMenu = new ToolStripMenuItem("除此之外全部关闭");
            gotoTreeMenuItem = new ToolStripMenuItem("定位到树中");

            _contextMenuStrip.Items.Add(saveMenuItem);
            _contextMenuStrip.Items.Add(closeMenuItem);
            _contextMenuStrip.Items.Add(closeAllWithoutThisMenu);
            _contextMenuStrip.Items.Add(gotoTreeMenuItem);

            this.TabPageContextMenuStrip = _contextMenuStrip;

            saveMenuItem.Click += delegate
            {
                if (this is BaseEditViewForm)
                {
                    BaseEditViewForm editViewThis = (BaseEditViewForm)this;
                    if (editViewThis.IsModified)
                    {
                        editViewThis.Save();
                    }
                }
            };
            closeMenuItem.Click += delegate
            {
                this.Close();
            };
            closeAllWithoutThisMenu.Click += delegate
            {
                Service.Workbench.CloseAllForm(this);
            };
            gotoTreeMenuItem.Click += delegate
            {
                Service.Workbench.GotoTree(this);
            };

            this.DockAreas = DockAreas.Document;
            _contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(_contextMenuStrip_Opening);
        }

        void _contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this is BaseEditViewForm)
            {
                BaseEditViewForm editViewThis = (BaseEditViewForm)this;
                saveMenuItem.Enabled = editViewThis.IsModified;
            }
            else
            {
                saveMenuItem.Enabled = false;
            }

            switch (this.WorkDocumentType)
            {
                case WorkDocumentType.TmpltDesigner:
                case WorkDocumentType.HtmlDesigner:
                //case WorkDocumentType.Manager:
                case WorkDocumentType.SnipDesigner:
                case WorkDocumentType.Edit:
                    gotoTreeMenuItem.Enabled = true;
                    break;
                default:
                    gotoTreeMenuItem.Enabled = false;
                    break;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Service.Workbench.AddForm(this);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            Service.Workbench.ToFrontForm(this);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Service.Workbench.RemoveForm(this);

            base.OnFormClosed(e);
        }

        virtual public string Id { get { return string.Empty; } }

        virtual public WorkDocumentType WorkDocumentType { get { return WorkDocumentType.None; } }

        virtual public bool CanUndo() { return false; }
        virtual public bool CanRedo() { return false; }
        virtual public bool CanCut() { return false; }
        virtual public bool CanCopy() { return false; }
        virtual public bool CanPaste() { return false; }
        virtual public bool CanDelete() { return false; }
        virtual public bool CanSelectAll() { return false; }

        virtual public void Undo(){}
        virtual public void Redo(){}
        virtual public void Cut(){}
        virtual public void Copy(){}
        virtual public void Paste(){}
        virtual public void Delete(){}
        virtual public void SelectAll(){}

        protected override void OnTextChanged(EventArgs e)
        {
            this.TabText = this.Text;

            base.OnTextChanged(e);
        }
    }
}
