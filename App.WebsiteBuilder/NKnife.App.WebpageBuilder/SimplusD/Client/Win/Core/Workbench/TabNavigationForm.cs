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

        #region _formList�ķ������Ա����

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

            ///���Pad������
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
        /// ��дSetVisibleCore�������Ըı�Show��Hide��Ĭ��ʵ��
        /// </summary>
        protected override void SetVisibleCore(bool value)
        {
            if (value)
            {
                LoadFileList();
                base.SetVisibleCore(value);
                this.Activate();

                ///����ļ�����������ݣ����ļ������Ϊ���㡣��������б���Ϊ����
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
                ///���ش���----Hide��API:ShowWindowĬ�϶��ý���仯�����Բ�ʹ��
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
                ///�Ȼ�ȡ������ĵ�ǰForm
                Form form = currentNode.Value;

                ///����ListViewItem
                ListViewItem item = new ListViewItem(form.Text);

                ///����ͼ��
                listViewFile.SmallImageList.Images.Add(form.Icon.Handle.ToString(),form.Icon);
                item.ImageKey = form.Icon.Handle.ToString();
                item.Tag = form;

                ///���õ�ǰ����
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

                ///ת����һ������ڵ�
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