using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    /// <summary>
    /// 允许NavigationStrip对其进行导航。
    /// </summary>
    public interface INavigatable
    {
        /// <summary>
        /// 选择一个节点。通常是用户单击导航栏的一项引发。
        /// </summary>
        /// <param name="node"></param>
        void Select(INavigateNode node);

        /// <summary>
        /// 获取当前需要导航的层次节点集。
        /// </summary>
        IEnumerable<INavigateNode> GetLevelNodes();

        /// <summary>
        /// 需要导航的层次节点集已改变。
        /// </summary>
        event EventHandler LevelNodesChanged;
    }

    /// <summary>
    /// NavigationStrip导航的每个节点。
    /// </summary>
    public interface INavigateNode
    {
        /// <summary>
        /// 获取显示在导航栏的文本。
        /// </summary>
        string Title { get; }

        /// <summary>
        /// 获取详细信息(显示为ToolTips)。
        /// </summary>
        string Message { get; }

        /// <summary>
        /// 获取是否选中。
        /// </summary>
        bool IsSelected { get; }
    }

    /// <summary>
    /// 导航栏。
    /// </summary>
    public partial class NavigationStrip : StatusStrip
    {
        private INavigatable _navigateTarget;
        /// <summary>
        /// 获取或设置导航的目标。
        /// </summary>
        public INavigatable NavigateTarget
        {
            get { return _navigateTarget; }
            set
            {
                _navigateTarget = value;

                if (!this.DesignMode)
                {
                    if (_navigateTarget != null)
                    {
                        ///初始化NavigateTarget
                        this.NavigateTarget.LevelNodesChanged += new EventHandler(NavigateTarget_LevelNodesChanged);
                    }
                }
            }
        }

        public NavigationStrip()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 节点改变时，刷新导航栏
        /// </summary>
        void NavigateTarget_LevelNodesChanged(object sender, EventArgs e)
        {
            this.SuspendLayout();

            ///先清空之前的导航节点
            this.Items.Clear();

            ///获取新的层级节点
            IEnumerable<INavigateNode> nodes = this.NavigateTarget.GetLevelNodes();

            ///通过获取的层级节点，生成导航栏的导航节点
            foreach (INavigateNode node in nodes)
            {
                ///两个导航按钮间添加间隔符。
                if (this.Items.Count > 0)
                {
                    ///添加一个间隔符
                    ToolStripSeparator separator = new ToolStripSeparator();
                    //ToolStripStatusLabel separator = new ToolStripStatusLabel("|");
                    this.Items.Add(separator);
                }

                ///构造导航按钮。
                ToolStripButton button = new ToolStripButton(node.Title);
                button.ToolTipText = node.Message;
                button.Tag = node;
                button.Click += new EventHandler(button_Click);
                if (node.IsSelected)
                {
                    button.Checked = true;
                }

                ///添加到集合
                this.Items.Add(button);
            }

            this.ResumeLayout();
        }

        void button_Click(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;
            INavigateNode node = item.Tag as INavigateNode;

            ///控件的点击触发导航目标的Select方法
            this.NavigateTarget.Select(node);
        }
    }
}
