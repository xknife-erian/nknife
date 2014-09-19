using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WeifenLuo.WinFormsUI.Docking;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class TabNavigationForm : Form
    {
        WorkbenchForm _mainForm;

        #region _formList的方法与成员声明

        LinkedList<Form> _formList
        {
            get { return Service.Workbench.FormList; }
        }

        #endregion

        public TabNavigationForm(WorkbenchForm mainForm)
        {
            this._mainForm = mainForm;
            this.Owner = mainForm;
            this.KeyPreview = true;
            InitializeComponent();

            listViewPad.SmallImageList = new ImageList();
            listViewFile.SmallImageList = new ImageList();

            ///添加Pad工具栏
            AddPadForm(_mainForm.MainPropertyPad);
            AddPadForm(_mainForm.MainTreePad);
            AddPadForm(_mainForm.MainWizardPad);
            AddPadForm(_mainForm.MainPreviewPad);
            AddPadForm(_mainForm.MainResultPad);
        }
        private void AddPadForm(Form pad)
        {
            ListViewItem item = new ListViewItem(pad.Text);
            item.Tag = pad;
            listViewPad.SmallImageList.Images.Add(pad.Icon.Handle.ToString(),
                pad.Icon);
            item.ImageKey = pad.Icon.Handle.ToString();
            this.listViewPad.Items.Add(item);
        }

        /// <summary>
        /// 重写SetVisibleCore函数，以改变Show和Hide的默认实现
        /// </summary>
        protected override void SetVisibleCore(bool value)
        {
            if (value)
            {
                LoadFileList();
                base.SetVisibleCore(value);
                this.Activate();

                ///如果文件面板中有数据，则文件面板设为焦点。否则将面板列表设为焦点
                if (listViewFile.Items.Count > 0)
                {
                    this.listViewFile.Focus();
                }
                else
                {
                    listViewPad.Focus();
                }
            }
            else
            {
                ///隐藏窗口----Hide和API:ShowWindow默认都让焦点变化，所以不使用
                Utility.DllImport.SetWindowHide(this);
                this.Owner.Activate();
            }
        }

        void LoadFileList()
        {
            listViewFile.BeginUpdate();
            listViewFile.Items.Clear();
            listViewFile.SmallImageList.Images.Clear();
            bool prevCurrent = false;
            LinkedListNode<Form> currentNode = _formList.First;
            while (currentNode != null)
            {
                ///先获取链表里的当前Form
                Form form = currentNode.Value;

                ///创建ListViewItem
                ListViewItem item = new ListViewItem(form.Text);

                ///设置图标
                listViewFile.SmallImageList.Images.Add(form.Icon.Handle.ToString(),form.Icon);
                item.ImageKey = form.Icon.Handle.ToString();
                item.Tag = form;

                ///设置当前窗口
                if (_mainForm.ActiveMdiChild == form)
                {
                    prevCurrent = true;
                    if (currentNode.Next == null)
                    {
                        if (listViewFile.Items.Count <= 0)
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            listViewFile.Items[0].Selected = true;
                        }
                    }
                }
                else
                {
                    if (prevCurrent)
                    {
                        item.Selected = true;
                        prevCurrent = false;
                    }
                }
                listViewFile.Items.Add(item);

                ///转向下一个链表节点
                currentNode = currentNode.Next;
            }

            listViewFile.EndUpdate();
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);

            Hide();
        }

        private void listViewPad_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left
                && listViewPad.SelectedItems.Count == 1
                && listViewPad.GetItemAt(e.X, e.Y) == listViewPad.SelectedItems[0])
            {
                HideAndOpenSelect();
            }
        }

        private void listViewFile_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left
                && listViewFile.SelectedItems.Count == 1
                && listViewFile.GetItemAt(e.X, e.Y) == listViewFile.SelectedItems[0])
            {
                HideAndOpenSelect();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Control)
            {
                TabNextInView(listViewFile.Focused ? listViewFile : listViewPad);
            }
            base.OnKeyDown(e);
        }

        void TabNextInView(ListView listView)
        {
            if (listView.Items.Count < 1)
            {
                return;
            }
            int current = 0;
            if (listView.SelectedItems == null || listView.SelectedItems.Count < 1)
            {
                current = -1;
            }
            else
            {
                current = listView.SelectedItems[0].Index;
            }
            int next = current == (listView.Items.Count - 1) ? 0 : current + 1;
            listView.Items[next].Selected = true;
            listView.Items[next].EnsureVisible();
        }

        void HideAndOpenSelect()
        {
            if (listViewPad.Focused)
            {
                this.Hide();
                if (listViewPad.SelectedItems.Count > 0)
                {
                    PadContent pad = (PadContent)listViewPad.SelectedItems[0].Tag;
                    pad.Show();
                }
            }
            else
            {
                this.Hide();
                if (listViewFile.SelectedItems.Count > 0)
                {
                    IDockContent form = (Form)listViewFile.SelectedItems[0].Tag as IDockContent;
                    form.DockHandler.Activate();
                }
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.KeyCode == Keys.ControlKey)
            {
                HideAndOpenSelect();
            }
        }
    }
}