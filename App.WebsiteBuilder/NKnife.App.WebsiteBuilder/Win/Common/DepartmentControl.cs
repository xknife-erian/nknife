using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using System.IO;

namespace Jeelu.Win
{
    public partial class DepartmentControl : UserControl  
    {
        /// <summary>
        /// 类内部可用来操作的部门集合
        /// </summary>
        List<DepartmentData> _deptlist = new List<DepartmentData>();

        /// <summary>
        /// 获取或设置部门的集合
        /// </summary>
        public DepartmentData[] Value
        {
            get
            {
                ReadValues();
                return _deptlist.ToArray();
            }
            set
            {
                _deptlist.Clear();

                _deptlist.AddRange(value);
            }
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public DepartmentControl()
        {
            InitializeComponent();
            this.tvwDeptList.HideSelection = false;
        }

        #region 控件的事件
   
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            loading();
            ShowControlText();
        }

        private void btnDeptAdd_Click(object sender, EventArgs e)
        {
            AddDept();
            OnValueChanged(EventArgs.Empty);
        }

        private void btnDeptDel_Click(object sender, EventArgs e)
        {
            DelDept();
            OnValueChanged(EventArgs.Empty);
        }

        private void tvwDeptList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            NodeSelected(e.Node);
        }

        private void tbxDeptTitle_TextChanged(object sender, EventArgs e)
        {
            TreeNode checkedNode = this.tvwDeptList.SelectedNode;
            if (checkedNode != null)
            {
                string name = ((DepartmentData)checkedNode.Tag).Name;
                if (this.tbxDeptTitle.Text != "")
                {
                    checkedNode.Text = this.tbxDeptTitle.Text;
                    ((DepartmentData)checkedNode.Tag).Name = this.tbxDeptTitle.Text;
                }
                else
                {
                    checkedNode.Text = name;
                }
            }
            ///以上有一点需注意，树节点最起码保留一个字,不为空
            OnValueChanged(EventArgs.Empty);
        }

        private void tbxDeptDuty_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged(EventArgs.Empty);
        }

        //protected override void OnLeave(EventArgs e)
        //{
        //    //在关闭时,或在窗体其它的位置激活时调用,而右击选项头时并不调用
        //    base.OnLeave(e);
        //    ReadValues();
        //    OnValueChanged(EventArgs.Empty);
        //}


        private void ReadValues()
        {
            ///保存最后一个部门节点的值
            TreeNode node = tvwDeptList.SelectedNode;
            if (node != null)
            {
                WriteNodeProperty(node);
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        ///加载时,控件初始化
        /// </summary>
        private void loading()
        {

            if (_deptlist.Count != 0)
            {
                ///显示在TreeView   
                ShowDeptToTreeView();
                this.tvwDeptList.Nodes[0].Expand();
                this.tvwDeptList.SelectedNode = this.tvwDeptList.Nodes[0];
            }
            else
            {
                ///设置界面状态    
                SetEnabled(false);
            }
        }
        /// <summary>
        /// 显示部门到树视图
        /// </summary>
        private void ShowDeptToTreeView()
        {
            foreach (var dept in _deptlist)
            {
                TreeNode node = new TreeNode();
                this.tvwDeptList.Nodes.Add(node);
                node.Text = dept.Name;
                node.Tag = dept;
            }
        }

        /// <summary>
        /// 控件显示文本
        /// </summary>
        private void ShowControlText()
        {
            lblDeptDuty.Text = StringParserService.Parse("${res:department.deptDuty}");
            lblDeptList.Text = StringParserService.Parse("${res:department.deptList}");
            lblDeptTitle.Text = StringParserService.Parse("${res:department.deptName}");
            lblLinkAddress.Text = StringParserService.Parse("${res:department.address}");
            lblLinkEmail.Text = StringParserService.Parse("${res:department.email}");
            lblLinkFax.Text = StringParserService.Parse("${res:department.fax}");
            lblLinkMan.Text = StringParserService.Parse("${res:department.linkMan}");
            lblLinkManSex.Text = StringParserService.Parse("${res:department.sex}");
            lblLinkPhone.Text = StringParserService.Parse("${res:department.officePhone}");
            lblLinkPostCode.Text = StringParserService.Parse("${res:department.post}");
            lblMSN.Text = StringParserService.Parse("${res:department.msn}");
            lblPhone.Text = StringParserService.Parse("${res:department.mobelPhone}");
            btnDeptAdd.Text = StringParserService.Parse("${res:department.btnAdd}");
            btnDeptDel.Text = StringParserService.Parse("${res:department.btnDel}");
            gbxDeptLink.Text = StringParserService.Parse("${res:department.deptLink}");
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        private void AddDept()
        {
            int count = this.tvwDeptList.Nodes.Count;
            if (count == 0)
                SetEnabled(true);
            else
            {
                /* 验证 */
                TreeNode node = this.tvwDeptList.SelectedNode;
                WriteNodeProperty(node);
            }

            ///创建部门
            DepartmentData newDept = CreateDept();
            _deptlist.Add(newDept);

            ///添加树节点
            TreeNode treeNode = new TreeNode();
            treeNode.Text = newDept.Name;
            treeNode.Tag = newDept;
            this.tvwDeptList.Nodes.Add(treeNode);


            this.tvwDeptList.Select();
            this.tvwDeptList.SelectedNode = treeNode;
        }
        /// <summary>
        /// 创建新部门
        /// </summary>
        /// <returns></returns>
        private DepartmentData CreateDept()
        {
            DepartmentData newDept = new DepartmentData();
            newDept.Name = CreateDefaultName("部门");
            return newDept;
        }
        /// <summary>
        /// 删除部门
        /// </summary>
        private void DelDept()
        {
            TreeNode checkedNode = this.tvwDeptList.SelectedNode;
            if (checkedNode != null)
            {
                int index = checkedNode.Index;
                if (MessageBox.Show("删除当前部门,则部门的相关信息也随之删除!", "警告", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DepartmentData dept = (DepartmentData)checkedNode.Tag;
                    _deptlist.Remove(dept);

                    this.tvwDeptList.Nodes[index].Remove();
                    ///删除节点后，树控件的节点个数
                    int n = this.tvwDeptList.Nodes.Count;
                    if (n == 0)
                    {
                        ClearValues();
                        SetEnabled(false);
                    }
                    else
                    {

                        if (index != 0)
                        {
                            this.tvwDeptList.Select();
                            this.tvwDeptList.SelectedNode = this.tvwDeptList.Nodes[index - 1];
                        }
                        else
                        {
                            this.tvwDeptList.Select();
                            this.tvwDeptList.SelectedNode = this.tvwDeptList.Nodes[0];
                        }


                    }

                }
            }

        }
        TreeNode preNode;
        /// <summary>
        /// 切换树节点时 (在更改选定内容后发生)
        /// </summary>
        private void NodeSelected(TreeNode treeNode)
        {
            Debug.Assert(treeNode != null);

            if (preNode != null)
            {
                /* 验证 */
                WriteNodeProperty(preNode);
            }
            preNode = treeNode;

            ///显示节点信息
            ReadNodeProperty(treeNode);

        }
        /// <summary>
        /// 设置部门数据编辑区是否可用 
        /// </summary>
        private void SetEnabled(bool isEnabled)
        {
            SetSubEnabled(this, isEnabled);
        }
        private void SetSubEnabled(Control control, bool isEnabled)
        {
            foreach (Control ctl in control.Controls)
            {
                Type type = ctl.GetType();
                if (type == typeof(Panel) || type == typeof(GroupBox))
                {
                    SetSubEnabled(ctl, isEnabled);
                }
                if (type == typeof(TextBox) || type == typeof(Button) || type == typeof(SelectGroup))
                {
                    if (ctl.Name != "btnDeptAdd")
                    {
                        ctl.Enabled = isEnabled;
                    }

                }

            }
        }
        /// <summary>
        /// 清空部门数据编辑区的数据
        /// </summary>
        private void ClearValues()
        {
            foreach (Control control in this.Controls)
            {
                Type type = control.GetType();
                if (type == typeof(TextBox))
                {
                    control.Text = string.Empty;
                }
                if (type == typeof(SelectGroup))
                {
                    ((SelectGroup)control).SelectedValues = new object[0];
                }
            }

        }

        /// <summary>
        ///读取部门相关信息 (Form _deptList)
        /// </summary>
        private void ReadNodeProperty(TreeNode treeNode)
        {
            DepartmentData deptData = (DepartmentData)treeNode.Tag;

            this.tbxDeptTitle.Text = deptData.Name;
            this.tbxDeptDuty.Text = deptData.Duty;

            this.tbxLinkMan.Text = deptData.LinkMan;
            this.tbxLinkManSex.Text = deptData.LinkManSex;
            this.tbxLinkPhone.Text = deptData.Phone;
            this.tbxMobelPhone.Text = deptData.MobilePhone;
            this.tbxLinkFax.Text = deptData.Fax;
            this.tbxMSN.Text = deptData.Msn;
            this.tbxLinkEmail.Text = deptData.Email;
            this.tbxLinkAddress.Text = deptData.Address;
            this.tbxLinkPostCode.Text = deptData.PostCode;
        }
        /// <summary>
        /// 写入部门相关信息 (To _deptList)
        /// </summary>
        private void WriteNodeProperty(TreeNode treeNode)
        {
            DepartmentData deptData = (DepartmentData)treeNode.Tag;

            deptData.Name = this.tbxDeptTitle.Text;
            deptData.Duty = this.tbxDeptDuty.Text;

            deptData.LinkMan = this.tbxLinkMan.Text;
            deptData.LinkManSex = this.tbxLinkManSex.Text;
            deptData.Phone = this.tbxLinkPhone.Text;
            deptData.MobilePhone = this.tbxMobelPhone.Text;
            deptData.Fax = this.tbxLinkFax.Text;
            deptData.Msn = this.tbxMSN.Text;
            deptData.Email = this.tbxLinkEmail.Text;
            deptData.Address = this.tbxLinkAddress.Text;
            deptData.PostCode = this.tbxLinkPostCode.Text;
        }
        /// <summary>
        /// 创建默认的部门名称 如:"部门1、部门2..."
        /// </summary>
        /// <returns></returns>
        private string CreateDefaultName(string defaultName)
        {
            string newName = "";
            int index = 1;
            do
            {
                newName = string.Concat(defaultName, index);
                index++;
            }
            while (IsExists(newName));
            return newName;
        }
        /// <summary>
        /// 判断当前的名称是否存在
        /// </summary>
        /// <returns></returns>
        private bool IsExists(string name)
        {
            ///存在为真，否则为假
            foreach (var item in _deptlist)
            {
                if (string.Compare(item.Name, name) == 0)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        public event EventHandler ValueChanged;
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }

    }
}
