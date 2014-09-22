using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    class OMMainToolStrip : ToolStrip
    {
        private System.ComponentModel.IContainer components;

        public OMMainToolStrip()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            ToolStripButton _newToolStripButton = new ToolStripButton("新增");
            ToolStripButton _editToolStripButton = new ToolStripButton("修改");
            ToolStripButton _forzedToolStripButton = new ToolStripButton("冻结");
            ToolStripButton _deleteToolStripButton = new ToolStripButton("删除");
            ToolStripButton _cancelToolStripButton = new ToolStripButton("取消");
            ToolStripButton _saveToolStripButton  = new ToolStripButton("保存");

            this.Items.Add(_newToolStripButton);
        }
    }
}
