using System.Windows.Forms;
using Didaku.Engine.Timeaxis.Base.Implement;

namespace Didaku.Engine.Timeaxis.Base.Controls
{
    public class ServiceQueueGroupTreeNode : TreeNode
    {
        private readonly ServiceQueueGroup _Group;
        public ServiceQueueGroupTreeNode(ServiceQueueGroup group)
        {
            _Group = group;
            Text = string.Format("¶ÓÁÐ×é: {0}", group.GetType().Name.Replace("QueueGroup", ""));
            SetChildrenNode();
        }

        private void SetChildrenNode()
        {
            foreach (var queue in _Group)
            {
                var node = new ServiceQueueTreeNode(queue);
                this.Nodes.Add(node);
            }
        }
    }
}