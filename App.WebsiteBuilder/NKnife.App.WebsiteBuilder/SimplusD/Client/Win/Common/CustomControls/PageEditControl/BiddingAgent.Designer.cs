namespace Jeelu.SimplusD.Client.Win
{
    partial class BiddingAgent
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
            this.lblAgent = new System.Windows.Forms.Label();
            this.tbxAgent = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.tbxPhone = new System.Windows.Forms.TextBox();
            this.lblAgentUnit = new System.Windows.Forms.Label();
            this.tbxAgentUnit = new System.Windows.Forms.TextBox();
            this.lblIsAgent = new System.Windows.Forms.Label();
            this.AgentSelectGroup = new Jeelu.Win.SelectGroup();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblAgent
            // 
            this.lblAgent.AutoSize = true;
            this.lblAgent.Location = new System.Drawing.Point(31, 36);
            this.lblAgent.Name = "lblAgent";
            this.lblAgent.Size = new System.Drawing.Size(71, 12);
            this.lblAgent.TabIndex = 4;
            this.lblAgent.Text = "代理负责人:";
            // 
            // tbxAgent
            // 
            this.tbxAgent.Location = new System.Drawing.Point(103, 33);
            this.tbxAgent.Name = "tbxAgent";
            this.tbxAgent.Size = new System.Drawing.Size(111, 21);
            this.tbxAgent.TabIndex = 1;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(243, 36);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(59, 12);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "联系电话:";
            // 
            // tbxPhone
            // 
            this.tbxPhone.Location = new System.Drawing.Point(308, 33);
            this.tbxPhone.Name = "tbxPhone";
            this.tbxPhone.Size = new System.Drawing.Size(111, 21);
            this.tbxPhone.TabIndex = 2;
            // 
            // lblAgentUnit
            // 
            this.lblAgentUnit.AutoSize = true;
            this.lblAgentUnit.Location = new System.Drawing.Point(19, 63);
            this.lblAgentUnit.Name = "lblAgentUnit";
            this.lblAgentUnit.Size = new System.Drawing.Size(83, 12);
            this.lblAgentUnit.TabIndex = 6;
            this.lblAgentUnit.Text = "招商代理单位:";
            // 
            // tbxAgentUnit
            // 
            this.tbxAgentUnit.Location = new System.Drawing.Point(103, 60);
            this.tbxAgentUnit.MaxLength = 50;
            this.tbxAgentUnit.Name = "tbxAgentUnit";
            this.tbxAgentUnit.Size = new System.Drawing.Size(317, 21);
            this.tbxAgentUnit.TabIndex = 3;
            // 
            // lblIsAgent
            // 
            this.lblIsAgent.AutoSize = true;
            this.lblIsAgent.Location = new System.Drawing.Point(7, 12);
            this.lblIsAgent.Name = "lblIsAgent";
            this.lblIsAgent.Size = new System.Drawing.Size(95, 12);
            this.lblIsAgent.TabIndex = 7;
            this.lblIsAgent.Text = "是否有招商代理:";
            // 
            // AgentSelectGroup
            // 
            this.AgentSelectGroup.DataSource = null;
            this.AgentSelectGroup.DisplayMember = null;
            this.AgentSelectGroup.HIndent = 5;
            this.AgentSelectGroup.HorizontalCount = 1;
            this.AgentSelectGroup.LineHeight = 20;
            this.AgentSelectGroup.Location = new System.Drawing.Point(104, 4);
            this.AgentSelectGroup.MultiSelect = false;
            this.AgentSelectGroup.Name = "AgentSelectGroup";
            this.AgentSelectGroup.SelectedStringValues = null;
            this.AgentSelectGroup.SelectedValue = null;
            this.AgentSelectGroup.SelectedValues = null;
            this.AgentSelectGroup.Size = new System.Drawing.Size(178, 23);
            this.AgentSelectGroup.TabIndex = 0;
            this.AgentSelectGroup.Text = null;
            this.AgentSelectGroup.ValueMember = null;
            this.AgentSelectGroup.VIndent = 5;
            this.AgentSelectGroup.SelectedValueChanged += new System.EventHandler(this.selectGroup1_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(421, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "限50字符";
            // 
            // BiddingAgent
            // 
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxAgentUnit);
            this.Controls.Add(this.lblAgentUnit);
            this.Controls.Add(this.tbxPhone);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.tbxAgent);
            this.Controls.Add(this.lblAgent);
            this.Controls.Add(this.AgentSelectGroup);
            this.Controls.Add(this.lblIsAgent);
            this.Name = "BiddingAgent";
            this.Size = new System.Drawing.Size(487, 93);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAgent;
        private System.Windows.Forms.TextBox tbxAgent;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox tbxPhone;
        private System.Windows.Forms.Label lblAgentUnit;
        private System.Windows.Forms.TextBox tbxAgentUnit;
        private System.Windows.Forms.Label lblIsAgent;
        private Jeelu.Win.SelectGroup AgentSelectGroup;
        private System.Windows.Forms.Label label1;

    }
}
