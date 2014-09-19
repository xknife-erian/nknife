using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WeifenLuo.WinFormsUI.Docking;

namespace Jeelu.KeywordResonator.Client
{
    public partial class TabNavigationForm : Form
    {
        WorkbenchForm _mainForm;

        LinkedList<Form> _formList
        {
            get { return Service.Workbench.FormList; }
        }

        #region ��ʼ���ؼ�

        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewPad = new System.Windows.Forms.ListView();
            this.listViewFile = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "����ߴ���";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(201, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "��ļ�";
            // 
            // listViewPad
            // 
            this.listViewPad.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewPad.BackColor = System.Drawing.SystemColors.Control;
            this.listViewPad.Location = new System.Drawing.Point(11, 26);
            this.listViewPad.MultiSelect = false;
            this.listViewPad.Name = "listViewPad";
            this.listViewPad.Size = new System.Drawing.Size(181, 325);
            this.listViewPad.TabIndex = 1;
            this.listViewPad.UseCompatibleStateImageBehavior = false;
            this.listViewPad.View = System.Windows.Forms.View.List;
            this.listViewPad.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewPad_MouseClick);
            // 
            // listViewFile
            // 
            this.listViewFile.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewFile.BackColor = System.Drawing.SystemColors.Control;
            this.listViewFile.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewFile.Location = new System.Drawing.Point(202, 26);
            this.listViewFile.MultiSelect = false;
            this.listViewFile.Name = "listViewFile";
            this.listViewFile.Size = new System.Drawing.Size(361, 325);
            this.listViewFile.TabIndex = 0;
            this.listViewFile.UseCompatibleStateImageBehavior = false;
            this.listViewFile.View = System.Windows.Forms.View.List;
            this.listViewFile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewFile_MouseClick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.listViewPad);
            this.panel1.Controls.Add(this.listViewFile);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(576, 364);
            this.panel1.TabIndex = 2;
            // 
            // TabNavigationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 364);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TabNavigationForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TabNavigationForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listViewPad;
        private System.Windows.Forms.ListView listViewFile;

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
            ListViewItem itemProperty = new ListViewItem(_mainForm.MainPropertyPad.Text);
            itemProperty.Tag = _mainForm.MainPropertyPad;
            listViewPad.SmallImageList.Images.Add(_mainForm.MainPropertyPad.Icon.Handle.ToString(),
                _mainForm.MainPropertyPad.Icon);
            itemProperty.ImageKey = _mainForm.MainPropertyPad.Icon.Handle.ToString();

            ListViewItem itemTree = new ListViewItem(_mainForm.MainStonePad.Text);
            itemTree.Tag = _mainForm.MainStonePad;
            listViewPad.SmallImageList.Images.Add(_mainForm.MainStonePad.Icon.Handle.ToString(),
                _mainForm.MainStonePad.Icon);
            itemTree.ImageKey = _mainForm.MainStonePad.Icon.Handle.ToString();

            this.listViewPad.Items.Add(itemProperty);
            this.listViewPad.Items.Add(itemTree);
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
                    AbstractPad pad = (AbstractPad)listViewPad.SelectedItems[0].Tag;
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