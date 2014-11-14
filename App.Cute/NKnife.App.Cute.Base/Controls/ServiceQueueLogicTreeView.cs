using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Didaku.Wrapper;
using Didaku.Engine.Timeaxis.Base.Controls.Menus;
using Didaku.Engine.Timeaxis.Base.Implement;

namespace Didaku.Engine.Timeaxis.Base.Controls
{
    public class ServiceQueueLogicTreeView : TreeView
    {
        public void Fill(ICollection<ServiceQueueLogic> logics)
        {
            foreach (var logic in logics)
            {
                var node = new ServiceQueueLogicTreeNode(logic);
                this.Nodes.Add(node);
            }
        }

        private MenuMethods _MenuMethods;

        public void FillContextMenu(MenuMethods menuMethods)
        {
            _MenuMethods = menuMethods;
            foreach (TreeNode node in Nodes)
                LoopNode(node);
        }

        private void LoopNode(TreeNode treeNode)
        {
            if(treeNode is ServiceQueueLogicTreeNode)
            {
                var id = (string)treeNode.Tag;
                var menu = new CounterContextMenuStrip(id);
                menu.SetCallMethod(_MenuMethods.CallMethod);
                treeNode.ContextMenuStrip = menu;
            }
            if (treeNode is ServiceQueueTreeNode)
            {
                var queue = (ServiceQueue)treeNode.Tag;
                var menu = new QueueContextMenuStrip(queue.Id);
                menu.SetGetTicketMethod(_MenuMethods.GetTicketMethod);
                treeNode.ContextMenuStrip = menu;
            }
            if (treeNode.Nodes.Count > 0)
            {
                foreach (TreeNode node in treeNode.Nodes)
                    LoopNode(node);
            }
        }
    }
}
