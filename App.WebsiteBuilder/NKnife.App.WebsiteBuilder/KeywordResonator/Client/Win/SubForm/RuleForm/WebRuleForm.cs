using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;
using Jeelu.Billboard;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Jeelu.KeywordResonator.Client
{
    public partial class WebRuleForm : Form
    {
        /// <summary>
        /// 备份WebRuleCollectionList
        /// </summary>
        List<WebRuleCollection> WebRuleColl = new List<WebRuleCollection>();
        Dictionary<string, List<TreeNode>> domainDic = new Dictionary<string, List<TreeNode>>();
        WebRuleCollection activeRules; //当前激活的域名规则集合对象
        WebRule activeRule; //当前激活的此域名下的某个规则对象
        WebRuleState ruleState = WebRuleState.None;

        public WebRuleForm()
        {
            InitializeComponent();
            Init();
            this.BuildRuleBtn.Enabled = false;
            this.SaveRuleBtn.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAllDomain();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchOperState();
            string text = this.toolStripComboBox1.Text;
            ShowPartDomain(text);
        }

        private void DomainTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            //检查webRuleState的状态是否处于添加，修改，删除操作而没有保存
            SwitchOperState();
        }

        private void DomainTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            WebRuleCollection rules = (WebRuleCollection)e.Node.Tag;
            activeRules = rules;
            ShowRuleList(rules, "");
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            SwitchOperState();
            DbHelper.UpdateRuleCollection(null);
            this.Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            //Service.WebRuleCollectionList = WebRuleColl;
            this.Close();
        }

        private void RuleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchOperState();

            this._ParamPanel.Controls.Clear();
            //清空正文规则列表 on2008年7月18日
            this.TextRuleNameComboBox.Items.Clear();

            //显示参数列表
            string ruleName = this.RuleComboBox.Text;
            activeRule = activeRules[ruleName];

            Dictionary<string, ParamState> paramDic = activeRule.ParamStates;
            Debug.Assert(paramDic != null, "paramDic为空");
            if (paramDic == null )
            {
                return;
            }
            foreach (string key in paramDic.Keys)
            {
                ParamState state = paramDic[key];
                ParamCotrol ctr = new ParamCotrol();
                ctr.ParaName = key;
                ctr.KeyValid = state.KeyState;
                ctr.Size = new Size(140, 28);
                this._ParamPanel.Controls.Add(ctr);
            }

            this._ParamPanel.Enabled = false;

            //add by zl on2008年7月18日
            ShowTextRuleName(activeRules[ruleName]);
        }

        private void DeleteRuleBtn_Click(object sender, EventArgs e)
        {
            this.urlTextBox.Enabled = false;

            this.DeleteRuleBtn.Enabled = false;
            this.ModifiedRuleBtn.Enabled = false;
            this.SaveRuleBtn.Enabled = true;

            //add on 2008年7月18日
            TextRuleControlEnable(false);
            ruleState = WebRuleState.Delete;
        }

        private void ModifiedRuleBtn_Click(object sender, EventArgs e)
        {
            this.urlTextBox.Enabled = false;

            this.ModifiedRuleBtn.Enabled = false;
            this.DeleteRuleBtn.Enabled = false;
            this.SaveRuleBtn.Enabled = true;

            //add on 2008年7月18日
            TextRuleControlEnable(true);
            ruleState = WebRuleState.Modify;

            this._ParamPanel.Enabled = true;
        }

        private void SaveRuleBtn_Click(object sender, EventArgs e)
        {
            SaveRule();
        }

        private void urlTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.urlTextBox.Text))
            {
                this.BuildRuleBtn.Enabled = true;
                this.ModifiedRuleBtn.Enabled = false;
                this.DeleteRuleBtn.Enabled = false;
            }
            else
            {
                if (this.RuleComboBox.Items.Count != 0)
                    BtnDefaultState();

                this.BuildRuleBtn.Enabled = false;
            }
        }

        private void BuildRuleBtn_Click(object sender, EventArgs e)
        {
            string url = this.urlTextBox.Text.Trim();

            #region  判断url的合理性
            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                url = "http://" + url;
            }

            //检查url是否是在此域名下的 及url的格式是否正确
            bool flag = CheckURl(url);
            if (!flag)
            {
                MessageBox.Show("此url不是当前域名下的网址，请重新填写");
                this.urlTextBox.Focus();
                this.urlTextBox.SelectAll();
                return;
            }

            flag = Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
            if (!flag)
            {
                MessageBox.Show("Url的网址格式不正确，请重新填写");
                this.urlTextBox.Focus();
                this.urlTextBox.SelectAll();
                return;
            }

            if (url.IndexOf("?") == -1)
            {
                MessageBox.Show("请输入动态的网址，当前网址是不能生成规则的！");
                this.urlTextBox.Focus();
                this.urlTextBox.SelectAll();
                return;
            }
            #endregion

            this._ParamPanel.Controls.Clear();
            this._ParamPanel.Enabled = true;
            this.BuildRuleBtn.Enabled = false;
            this.SaveRuleBtn.Enabled = true;


            Uri uri = new Uri(url);
            activeRule = new WebRule(uri);
            bool isExist = activeRules.IsExistRule(activeRule.RuleName);
            if (isExist)
            {
                MessageBox.Show("此规则已存在，请重新输入Url!");
                this.urlTextBox.Focus();
                this.urlTextBox.SelectAll();
                return;
            }
            this.ruleNameTextBox.Text = activeRule.RuleName;
            this.RuleComboBox.Text = activeRule.RuleName;

            //如果是静态网址，则不需要显示参数列表

            Dictionary<string, ParamState> paramDic = activeRule.ParamStates;
            foreach (string key in paramDic.Keys)
            {
                ParamState state = paramDic[key];
                ParamCotrol ctr = new ParamCotrol();
                ctr.ParaName = key;
                ctr.KeyValid = state.KeyState;
                ctr.Size = new Size(140, 28);
                this._ParamPanel.Controls.Add(ctr);
            }

            //add on 2008年7月18日
            TextRuleControlEnable(true);

            ruleState = WebRuleState.New;

            this.urlTextBox.ReadOnly = true;
        }

        #region 实现方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            foreach (WebRuleCollection coll in Service.DbHelper.WebRuleCollections.Values)
            {
                WebRuleColl.Add(coll);
            }
            for (int i = 0; i < this.toolStripComboBox1.Items.Count; i++)
            {
                string item = this.toolStripComboBox1.Items[i].ToString();
                domainDic.Add(item, new List<TreeNode>());
            }
        }

        /// <summary>
        /// 加载树
        /// </summary>
        private void LoadAllDomain()
        {
            TreeNode parentNode = new TreeNode();
            foreach (WebRuleCollection webRule in Service.DbHelper.WebRuleCollections.Values)
            {
                string domain = webRule.SiteName;
                Debug.Assert(domain != "");
                TreeNode treeNode = new TreeNode(domain);
                treeNode.Tag = webRule;
                parentNode.Nodes.Add(treeNode);

                string initial = domain.Trim().Substring(0, 1);
                if (Char.IsNumber(initial, 0))
                {
                    domainDic["0-9"].Add(treeNode);
                }
                else
                {
                    string initialUp = initial.ToUpper();
                    domainDic[initialUp].Add(treeNode);
                }
            }

            TreeNode[] treeNodeColl = new TreeNode[parentNode.Nodes.Count];
            parentNode.Nodes.CopyTo(treeNodeColl, 0);
            this.DomainTreeView.Nodes.AddRange(treeNodeColl);

            TreeViewDefaultShow();
        }

        private void TreeViewDefaultShow()
        {
            //默认
            if (this.DomainTreeView.Nodes.Count == 0)
            {
                //没有此字母开头的域名，则右侧按键处于不可用状态

                this.DeleteRuleBtn.Enabled = false;
                this.ModifiedRuleBtn.Enabled = false;
                this.urlTextBox.Enabled = false;

                TextRuleControlEnable(false);
            }
            else
            {
                //默认显示第一项
                this.DomainTreeView.SelectedNode = this.DomainTreeView.Nodes[0];
                WebRuleCollection rules = (WebRuleCollection)this.DomainTreeView.Nodes[0].Tag;
                activeRules = rules;
                ShowRuleList(rules, "");
            }
        }

        /// <summary>
        /// 依据条件不同，显示不同部分的域名
        /// </summary>
        private void ShowPartDomain(string word)
        {
            //找出以字母word开头的域名
            List<TreeNode> list = domainDic[word];

            //重新显示树
            this.DomainTreeView.Nodes.Clear();
            TreeNode[] treeNodeColl = list.ToArray();
            this.DomainTreeView.Nodes.AddRange(treeNodeColl);

            ClearControl();

            TreeViewDefaultShow();
        }

        private void SaveRule()
        {
            BtnDefaultState();

            //如果是删除保存，则显示第一条
            switch (ruleState)
            {
                case WebRuleState.None:
                    break;
                case WebRuleState.New:
                    //保存所作修改
                    GetModifiedRule(activeRule);

                    //添加webRule到集合里
                    activeRules.Add(activeRule);
                    ruleState = WebRuleState.None;

                    //规则列表重新显示
                    ShowRuleList(activeRules, activeRule.RuleName);

                    //清空
                    this.ruleNameTextBox.Text = "";
                    this.urlTextBox.Text = "";

                    break;
                case WebRuleState.Modify:
                    if (activeRule.RuleState != WebRuleState.New)
                    {
                        activeRule.RuleState = WebRuleState.Modify;    
                    }
                    
                    //保存所作修改
                    GetModifiedRule(activeRule);

                    ruleState = WebRuleState.None;

                    _ParamPanel.Enabled = false;
                    break;
                case WebRuleState.Delete:
                    if (activeRule.RuleState == WebRuleState.New)
                    {
                        //直接删除此条记录
                        activeRules.Remove(activeRule);
                    }
                    else
                    {
                        activeRule.RuleState = WebRuleState.Delete;
                    }

                    ruleState = WebRuleState.None;
                    //规则列表重新显示
                    ShowRuleList(activeRules, "");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 将修改后的参数属性保存到对象中
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        private void GetModifiedRule(WebRule rule)
        {
            Control.ControlCollection ctrls = this._ParamPanel.Controls;
            foreach (Control ctrl in ctrls)
            {
                if (ctrl.GetType() == typeof(ParamCotrol))
                {
                    ParamCotrol paramCtrl = (ParamCotrol)ctrl;
                    rule.SetParamSate(paramCtrl.ParaName, paramCtrl.KeyValid, paramCtrl.IsModified);
                }
            }
        }

        /// <summary>
        /// 显示规则列表
        /// </summary>
        /// <param name="rules"></param>
        private void ShowRuleList(WebRuleCollection rules, string ruleName)
        {
            ClearControl();

            foreach (WebRule rule in rules)
            {
                if (rule.RuleState != WebRuleState.Delete)
                {
                    this.RuleComboBox.Items.Add(rule.RuleName);
                }
            }

            if (this.RuleComboBox.Items.Count != 0)
            {
                this.DeleteRuleBtn.Enabled = true;
                this.ModifiedRuleBtn.Enabled = true;
                this.urlTextBox.Enabled = true;
                TextRuleControlEnable(false);

                if (string.IsNullOrEmpty(ruleName))
                {
                    this.RuleComboBox.SelectedIndex = 0;
                }
                else
                {
                    //根据当前项，取其索引
                    int i = this.RuleComboBox.FindStringExact(ruleName);
                    this.RuleComboBox.SelectedIndex = i;
                }
            }
            else
            {
                this.urlTextBox.Enabled = true;
                this.DeleteRuleBtn.Enabled = false;
                this.ModifiedRuleBtn.Enabled = false;
                TextRuleControlEnable(false);
            }

        }

        private void SwitchOperState()
        {
            if (ruleState != WebRuleState.None)
            {
                if (MessageBox.Show("是否要保存规则！", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    SaveRule();
                    activeRule = null;
                }
                else
                {
                    activeRule = null;
                    ruleState = WebRuleState.None;

                    BtnDefaultState();
                    ClearControl();
                }
            }
        }

        private void BtnDefaultState()
        {
            this.DeleteRuleBtn.Enabled = true;
            this.ModifiedRuleBtn.Enabled = true;
            this.urlTextBox.Enabled = true;
            this.urlTextBox.ReadOnly = false;
            this.BuildRuleBtn.Enabled = false;
            this.SaveRuleBtn.Enabled = false;

            //add on2008年7月18日
            TextRuleControlEnable (false);
        }

        private void ClearControl()
        {
            //清空右侧显示
            this.RuleComboBox.Items.Clear();
            this.ruleNameTextBox.Text = "";
            this.urlTextBox.Text = "";
            this._ParamPanel.Controls.Clear();
        }

        private bool CheckURl(string url)
        {
            string head = "http://" + activeRules.SiteName.Trim();

            return url.StartsWith(head, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        private void NewTextRuleBtn_Click(object sender, EventArgs e)
        {
            TextRuleEditForm form = new TextRuleEditForm(true);
            if (form.ShowDialog() == DialogResult.OK)
            {
                string dt = DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString();
                ExtractRule textRule = new ExtractRule(dt, form.StartLabel, form.EndLabel, "");
                textRule.ExtractRuleState = WebRuleState.New;
                textRule.IsStateChange = true;
                activeRule.ExtractRuleCollection.Add(textRule);

                this.TextRuleNameComboBox.Items.Add(dt);
                this.TextRuleNameComboBox.Text = dt;
            }
                
        }

        private void DelTextRuleBtn_Click(object sender, EventArgs e)
        {
            //得到当前操作的ExtractRule对象
            string name = this.TextRuleNameComboBox.Text ;
            ExtractRule delRule = activeRule.ExtractRuleCollection[name];
            if (MessageBox.Show("确定要删除", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (delRule.ExtractRuleState == WebRuleState.New)
                {
                    activeRule.ExtractRuleCollection.Remove(delRule);
                }
                else
                {
                    delRule.ExtractRuleState = WebRuleState.Delete;
                    delRule.IsStateChange = true;
                }
                this.TextRuleNameComboBox.Items.Remove(name);
            }
        }

        private void ModifyTextRuleBtn_Click(object sender, EventArgs e)
        {
            //得到当前操作的ExtractRule对象
            string name = this.TextRuleNameComboBox.Text;
            ExtractRule modRule = activeRule.ExtractRuleCollection[name];
            if (modRule.ExtractRuleState != WebRuleState.New)
            {
                modRule.ExtractRuleState = WebRuleState.Modify;
                modRule.IsStateChange = true;
            }

            TextRuleEditForm form = new TextRuleEditForm(false);
            form.StartLabel = modRule.Start;
            form.EndLabel = modRule.End;
            if (form.ShowDialog() == DialogResult.OK)
            {
                modRule.Start = form.StartLabel;
                modRule.End = form.EndLabel;
            }
        }

        /// <summary>
        /// 显示当前规则下的正文规则列表名
        /// </summary>
        private void ShowTextRuleName(WebRule webRule)
        {
            foreach (ExtractRule textRule in webRule.ExtractRuleCollection)
            {
                if (textRule.ExtractRuleState != WebRuleState.Delete)
                {
                    this.TextRuleNameComboBox.Items.Add(textRule.Name);
                }
            }    
        }

        private void TextRuleControlEnable(bool isEnable)
        {
            this.TextRuleNameComboBox.Enabled = isEnable;
            this.NewTextRuleBtn.Enabled = isEnable;
            this.DelTextRuleBtn.Enabled = isEnable;
            this.ModifyTextRuleBtn .Enabled = isEnable;
        }

    }

    /// <summary>
    /// 显示参数及属性的控件
    /// 控件的显示内容与WebKeyState枚举一样
    /// </summary>
    class ParamCotrol : Control
    {

        #region 属性
        private string _paraName;
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParaName
        {
            get
            {
                return _paraName;
            }
            set
            {
                _paraName = value;
                this.ParamLabel.Text = _paraName;
            }
        }

        private int _keyValid;
        /// <summary>
        /// 此参数是否有效
        /// </summary>
        public WebKeyState KeyValid
        {
            get
            {
                return (WebKeyState)this.PropComboBox.SelectedIndex;
            }
            set
            {
                _keyValid = (int)value;
                this.PropComboBox.SelectedIndex = _keyValid;
            }
        }

        /// <summary>
        /// 此参数是否修改
        /// </summary>
        public bool IsModified
        {
            get
            {
                int i = this.PropComboBox.SelectedIndex;
                if (_keyValid == i)
                {
                    return false;
                }
                return true;
            }

        }
        #endregion

        public ParamCotrol()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.PropComboBox = new System.Windows.Forms.ComboBox();
            this.ParamLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PropComboBox
            // 
            this.PropComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PropComboBox.FormattingEnabled = true;
            this.PropComboBox.Items.AddRange(new object[] {
            "Availability",
            "Invalidation"});
            this.PropComboBox.Location = new System.Drawing.Point(0, 1);
            this.PropComboBox.Name = "PropComboBox";
            this.PropComboBox.Size = new System.Drawing.Size(79, 20);
            this.PropComboBox.TabIndex = 0;
            // 
            // ParamLabel
            // 
            this.ParamLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // this.ParamLabel.AutoSize = true;
            //this.ParamLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ParamLabel.Location = new System.Drawing.Point(85, 3);
            this.ParamLabel.Name = "ParamLabel";
            this.ParamLabel.Size = new System.Drawing.Size(55, 20);
            this.ParamLabel.TabIndex = 1;
            this.ParamLabel.Text = "label1";
            // 
            // UserControl1
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            // this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ParamLabel);
            this.Controls.Add(this.PropComboBox);
            this.Name = "ParamCotrol";
            this.Size = new System.Drawing.Size(138, 21);
            this.ResumeLayout(false);
            // this.PerformLayout();

        }
        private System.Windows.Forms.ComboBox PropComboBox;
        private System.Windows.Forms.Label ParamLabel;
    }
}
