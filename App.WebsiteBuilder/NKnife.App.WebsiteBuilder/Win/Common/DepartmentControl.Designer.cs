using Jeelu.Win;
namespace Jeelu.Win
{
    partial class DepartmentControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDeptAdd = new System.Windows.Forms.Button();
            this.btnDeptDel = new System.Windows.Forms.Button();
            this.lblDeptTitle = new System.Windows.Forms.Label();
            this.tbxDeptTitle = new System.Windows.Forms.TextBox();
            this.gbxDeptLink = new System.Windows.Forms.GroupBox();
            this.tbxMSN = new System.Windows.Forms.TextBox();
            this.lblMSN = new System.Windows.Forms.Label();
            this.tbxMobelPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblLinkManSex = new System.Windows.Forms.Label();
            this.tbxLinkEmail = new System.Windows.Forms.TextBox();
            this.lblLinkEmail = new System.Windows.Forms.Label();
            this.tbxLinkPostCode = new System.Windows.Forms.TextBox();
            this.tbxLinkManSex = new System.Windows.Forms.TextBox();
            this.lblLinkPostCode = new System.Windows.Forms.Label();
            this.tbxLinkFax = new System.Windows.Forms.TextBox();
            this.lblLinkFax = new System.Windows.Forms.Label();
            this.tbxLinkAddress = new System.Windows.Forms.TextBox();
            this.lblLinkAddress = new System.Windows.Forms.Label();
            this.tbxLinkPhone = new System.Windows.Forms.TextBox();
            this.lblLinkPhone = new System.Windows.Forms.Label();
            this.tbxLinkMan = new System.Windows.Forms.TextBox();
            this.lblLinkMan = new System.Windows.Forms.Label();
            this.lblDeptList = new System.Windows.Forms.Label();
            this.tvwDeptList = new System.Windows.Forms.TreeView();
            this.tbxDeptDuty = new System.Windows.Forms.TextBox();
            this.lblDeptDuty = new System.Windows.Forms.Label();
            this.gbxDeptLink.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDeptAdd
            // 
            this.btnDeptAdd.Location = new System.Drawing.Point(9, 341);
            this.btnDeptAdd.Name = "btnDeptAdd";
            this.btnDeptAdd.Size = new System.Drawing.Size(59, 23);
            this.btnDeptAdd.TabIndex = 2;
            this.btnDeptAdd.Text = "%添加";
            this.btnDeptAdd.UseVisualStyleBackColor = true;
            this.btnDeptAdd.Click += new System.EventHandler(this.btnDeptAdd_Click);
            // 
            // btnDeptDel
            // 
            this.btnDeptDel.Location = new System.Drawing.Point(84, 341);
            this.btnDeptDel.Name = "btnDeptDel";
            this.btnDeptDel.Size = new System.Drawing.Size(59, 23);
            this.btnDeptDel.TabIndex = 3;
            this.btnDeptDel.Text = "%删除";
            this.btnDeptDel.UseVisualStyleBackColor = true;
            this.btnDeptDel.Click += new System.EventHandler(this.btnDeptDel_Click);
            // 
            // lblDeptTitle
            // 
            this.lblDeptTitle.AutoSize = true;
            this.lblDeptTitle.Location = new System.Drawing.Point(153, 10);
            this.lblDeptTitle.Name = "lblDeptTitle";
            this.lblDeptTitle.Size = new System.Drawing.Size(65, 12);
            this.lblDeptTitle.TabIndex = 4;
            this.lblDeptTitle.Text = "%部门名称:";
            // 
            // tbxDeptTitle
            // 
            this.tbxDeptTitle.Location = new System.Drawing.Point(155, 25);
            this.tbxDeptTitle.Name = "tbxDeptTitle";
            this.tbxDeptTitle.Size = new System.Drawing.Size(283, 21);
            this.tbxDeptTitle.TabIndex = 5;
            this.tbxDeptTitle.TextChanged += new System.EventHandler(this.tbxDeptTitle_TextChanged);
            // 
            // gbxDeptLink
            // 
            this.gbxDeptLink.Controls.Add(this.tbxMSN);
            this.gbxDeptLink.Controls.Add(this.lblMSN);
            this.gbxDeptLink.Controls.Add(this.tbxMobelPhone);
            this.gbxDeptLink.Controls.Add(this.lblPhone);
            this.gbxDeptLink.Controls.Add(this.lblLinkManSex);
            this.gbxDeptLink.Controls.Add(this.tbxLinkEmail);
            this.gbxDeptLink.Controls.Add(this.lblLinkEmail);
            this.gbxDeptLink.Controls.Add(this.tbxLinkPostCode);
            this.gbxDeptLink.Controls.Add(this.tbxLinkManSex);
            this.gbxDeptLink.Controls.Add(this.lblLinkPostCode);
            this.gbxDeptLink.Controls.Add(this.tbxLinkFax);
            this.gbxDeptLink.Controls.Add(this.lblLinkFax);
            this.gbxDeptLink.Controls.Add(this.tbxLinkAddress);
            this.gbxDeptLink.Controls.Add(this.lblLinkAddress);
            this.gbxDeptLink.Controls.Add(this.tbxLinkPhone);
            this.gbxDeptLink.Controls.Add(this.lblLinkPhone);
            this.gbxDeptLink.Controls.Add(this.tbxLinkMan);
            this.gbxDeptLink.Controls.Add(this.lblLinkMan);
            this.gbxDeptLink.Location = new System.Drawing.Point(155, 91);
            this.gbxDeptLink.Name = "gbxDeptLink";
            this.gbxDeptLink.Size = new System.Drawing.Size(294, 275);
            this.gbxDeptLink.TabIndex = 8;
            this.gbxDeptLink.TabStop = false;
            this.gbxDeptLink.Text = "%部门联系";
            // 
            // tbxMSN
            // 
            this.tbxMSN.Location = new System.Drawing.Point(88, 152);
            this.tbxMSN.Name = "tbxMSN";
            this.tbxMSN.Size = new System.Drawing.Size(174, 21);
            this.tbxMSN.TabIndex = 11;
            this.tbxMSN.TextChanged += new System.EventHandler(this.tbxDeptDuty_TextChanged);
            // 
            // lblMSN
            // 
            this.lblMSN.AutoSize = true;
            this.lblMSN.Location = new System.Drawing.Point(38, 156);
            this.lblMSN.Name = "lblMSN";
            this.lblMSN.Size = new System.Drawing.Size(35, 12);
            this.lblMSN.TabIndex = 10;
            this.lblMSN.Text = "%MSN:";
            // 
            // tbxMobelPhone
            // 
            this.tbxMobelPhone.Location = new System.Drawing.Point(88, 98);
            this.tbxMobelPhone.Name = "tbxMobelPhone";
            this.tbxMobelPhone.Size = new System.Drawing.Size(142, 21);
            this.tbxMobelPhone.TabIndex = 7;
            this.tbxMobelPhone.TextChanged += new System.EventHandler(this.tbxDeptDuty_TextChanged);
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(8, 105);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(65, 12);
            this.lblPhone.TabIndex = 6;
            this.lblPhone.Text = "%移动电话:";
            // 
            // lblLinkManSex
            // 
            this.lblLinkManSex.AutoSize = true;
            this.lblLinkManSex.Location = new System.Drawing.Point(32, 49);
            this.lblLinkManSex.Name = "lblLinkManSex";
            this.lblLinkManSex.Size = new System.Drawing.Size(53, 12);
            this.lblLinkManSex.TabIndex = 2;
            this.lblLinkManSex.Text = "性别(&X):";
            // 
            // tbxLinkEmail
            // 
            this.tbxLinkEmail.Location = new System.Drawing.Point(88, 179);
            this.tbxLinkEmail.Name = "tbxLinkEmail";
            this.tbxLinkEmail.Size = new System.Drawing.Size(174, 21);
            this.tbxLinkEmail.TabIndex = 13;
            this.tbxLinkEmail.TextChanged += new System.EventHandler(this.tbxDeptDuty_TextChanged);
            // 
            // lblLinkEmail
            // 
            this.lblLinkEmail.AutoSize = true;
            this.lblLinkEmail.Location = new System.Drawing.Point(26, 180);
            this.lblLinkEmail.Name = "lblLinkEmail";
            this.lblLinkEmail.Size = new System.Drawing.Size(47, 12);
            this.lblLinkEmail.TabIndex = 12;
            this.lblLinkEmail.Text = "%Email:";
            // 
            // tbxLinkPostCode
            // 
            this.tbxLinkPostCode.Location = new System.Drawing.Point(88, 231);
            this.tbxLinkPostCode.Name = "tbxLinkPostCode";
            this.tbxLinkPostCode.Size = new System.Drawing.Size(100, 21);
            this.tbxLinkPostCode.TabIndex = 17;
            this.tbxLinkPostCode.TextChanged += new System.EventHandler(this.tbxDeptDuty_TextChanged);
            // 
            // tbxLinkManSex
            // 
            this.tbxLinkManSex.Location = new System.Drawing.Point(88, 44);
            this.tbxLinkManSex.Name = "tbxLinkManSex";
            this.tbxLinkManSex.Size = new System.Drawing.Size(60, 21);
            this.tbxLinkManSex.TabIndex = 3;
            this.tbxLinkManSex.TextChanged += new System.EventHandler(this.tbxDeptDuty_TextChanged);
            // 
            // lblLinkPostCode
            // 
            this.lblLinkPostCode.AutoSize = true;
            this.lblLinkPostCode.Location = new System.Drawing.Point(8, 234);
            this.lblLinkPostCode.Name = "lblLinkPostCode";
            this.lblLinkPostCode.Size = new System.Drawing.Size(65, 12);
            this.lblLinkPostCode.TabIndex = 16;
            this.lblLinkPostCode.Text = "%邮编编码:";
            // 
            // tbxLinkFax
            // 
            this.tbxLinkFax.Location = new System.Drawing.Point(88, 125);
            this.tbxLinkFax.Name = "tbxLinkFax";
            this.tbxLinkFax.Size = new System.Drawing.Size(100, 21);
            this.tbxLinkFax.TabIndex = 9;
            this.tbxLinkFax.TextChanged += new System.EventHandler(this.tbxDeptDuty_TextChanged);
            // 
            // lblLinkFax
            // 
            this.lblLinkFax.AutoSize = true;
            this.lblLinkFax.Location = new System.Drawing.Point(32, 129);
            this.lblLinkFax.Name = "lblLinkFax";
            this.lblLinkFax.Size = new System.Drawing.Size(41, 12);
            this.lblLinkFax.TabIndex = 8;
            this.lblLinkFax.Text = "%传真:";
            // 
            // tbxLinkAddress
            // 
            this.tbxLinkAddress.Location = new System.Drawing.Point(88, 206);
            this.tbxLinkAddress.Name = "tbxLinkAddress";
            this.tbxLinkAddress.Size = new System.Drawing.Size(195, 21);
            this.tbxLinkAddress.TabIndex = 15;
            this.tbxLinkAddress.TextChanged += new System.EventHandler(this.tbxDeptDuty_TextChanged);
            // 
            // lblLinkAddress
            // 
            this.lblLinkAddress.AutoSize = true;
            this.lblLinkAddress.Location = new System.Drawing.Point(8, 210);
            this.lblLinkAddress.Name = "lblLinkAddress";
            this.lblLinkAddress.Size = new System.Drawing.Size(65, 12);
            this.lblLinkAddress.TabIndex = 14;
            this.lblLinkAddress.Text = "%通讯地址:";
            // 
            // tbxLinkPhone
            // 
            this.tbxLinkPhone.Location = new System.Drawing.Point(88, 71);
            this.tbxLinkPhone.Name = "tbxLinkPhone";
            this.tbxLinkPhone.Size = new System.Drawing.Size(142, 21);
            this.tbxLinkPhone.TabIndex = 5;
            this.tbxLinkPhone.TextChanged += new System.EventHandler(this.tbxDeptDuty_TextChanged);
            // 
            // lblLinkPhone
            // 
            this.lblLinkPhone.AutoSize = true;
            this.lblLinkPhone.Location = new System.Drawing.Point(8, 77);
            this.lblLinkPhone.Name = "lblLinkPhone";
            this.lblLinkPhone.Size = new System.Drawing.Size(77, 12);
            this.lblLinkPhone.TabIndex = 4;
            this.lblLinkPhone.Text = "办公电话(&X):";
            // 
            // tbxLinkMan
            // 
            this.tbxLinkMan.Location = new System.Drawing.Point(88, 18);
            this.tbxLinkMan.Name = "tbxLinkMan";
            this.tbxLinkMan.Size = new System.Drawing.Size(100, 21);
            this.tbxLinkMan.TabIndex = 1;
            this.tbxLinkMan.TextChanged += new System.EventHandler(this.tbxDeptDuty_TextChanged);
            // 
            // lblLinkMan
            // 
            this.lblLinkMan.AutoSize = true;
            this.lblLinkMan.Location = new System.Drawing.Point(20, 22);
            this.lblLinkMan.Name = "lblLinkMan";
            this.lblLinkMan.Size = new System.Drawing.Size(65, 12);
            this.lblLinkMan.TabIndex = 0;
            this.lblLinkMan.Text = "联系人(&W):";
            // 
            // lblDeptList
            // 
            this.lblDeptList.AutoSize = true;
            this.lblDeptList.Location = new System.Drawing.Point(7, 10);
            this.lblDeptList.Name = "lblDeptList";
            this.lblDeptList.Size = new System.Drawing.Size(65, 12);
            this.lblDeptList.TabIndex = 0;
            this.lblDeptList.Text = "%部门列表:";
            // 
            // tvwDeptList
            // 
            this.tvwDeptList.Location = new System.Drawing.Point(9, 25);
            this.tvwDeptList.Name = "tvwDeptList";
            this.tvwDeptList.Size = new System.Drawing.Size(134, 310);
            this.tvwDeptList.TabIndex = 1;
            this.tvwDeptList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwDeptList_AfterSelect);
            // 
            // tbxDeptDuty
            // 
            this.tbxDeptDuty.Location = new System.Drawing.Point(155, 64);
            this.tbxDeptDuty.Name = "tbxDeptDuty";
            this.tbxDeptDuty.Size = new System.Drawing.Size(283, 21);
            this.tbxDeptDuty.TabIndex = 7;
            this.tbxDeptDuty.TextChanged += new System.EventHandler(this.tbxDeptDuty_TextChanged);
            // 
            // lblDeptDuty
            // 
            this.lblDeptDuty.AutoSize = true;
            this.lblDeptDuty.Location = new System.Drawing.Point(153, 49);
            this.lblDeptDuty.Name = "lblDeptDuty";
            this.lblDeptDuty.Size = new System.Drawing.Size(41, 12);
            this.lblDeptDuty.TabIndex = 6;
            this.lblDeptDuty.Text = "%职务:";
            // 
            // DepartmentControl
            // 
            this.Controls.Add(this.tbxDeptDuty);
            this.Controls.Add(this.lblDeptDuty);
            this.Controls.Add(this.tvwDeptList);
            this.Controls.Add(this.lblDeptList);
            this.Controls.Add(this.gbxDeptLink);
            this.Controls.Add(this.tbxDeptTitle);
            this.Controls.Add(this.lblDeptTitle);
            this.Controls.Add(this.btnDeptDel);
            this.Controls.Add(this.btnDeptAdd);
            this.Name = "DepartmentControl";
            this.Size = new System.Drawing.Size(453, 370);
            this.gbxDeptLink.ResumeLayout(false);
            this.gbxDeptLink.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDeptTitle;
        private System.Windows.Forms.GroupBox gbxDeptLink;
        private System.Windows.Forms.Label lblLinkPostCode;
        private System.Windows.Forms.Label lblLinkFax;
        private System.Windows.Forms.Label lblLinkAddress;
        private System.Windows.Forms.Label lblLinkPhone;
        private System.Windows.Forms.Label lblLinkMan;
        private System.Windows.Forms.Label lblDeptList;
        private System.Windows.Forms.Label lblLinkEmail;
        private System.Windows.Forms.Button btnDeptAdd;
        private System.Windows.Forms.Button btnDeptDel;
        private System.Windows.Forms.TextBox tbxDeptTitle;
        private System.Windows.Forms.TextBox tbxLinkPostCode;
        private System.Windows.Forms.TextBox tbxLinkFax;
        private System.Windows.Forms.TextBox tbxLinkAddress;
        private System.Windows.Forms.TextBox tbxLinkPhone;
        private System.Windows.Forms.TextBox tbxLinkMan;
        private System.Windows.Forms.TreeView tvwDeptList;
        private System.Windows.Forms.TextBox tbxLinkEmail;
        private System.Windows.Forms.Label lblLinkManSex;
        private System.Windows.Forms.TextBox tbxLinkManSex;
        private System.Windows.Forms.TextBox tbxDeptDuty;
        private System.Windows.Forms.Label lblDeptDuty;
        private System.Windows.Forms.TextBox tbxMobelPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox tbxMSN;
        private System.Windows.Forms.Label lblMSN;
    }
}
