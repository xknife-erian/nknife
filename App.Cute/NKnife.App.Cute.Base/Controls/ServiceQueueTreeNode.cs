using System.Windows.Forms;
using Didaku.Engine.Timeaxis.Base.Implement;

namespace Didaku.Engine.Timeaxis.Base.Controls
{
    public class ServiceQueueTreeNode : TreeNode
    {
        private readonly ServiceQueue _Queue;
        public ServiceQueueTreeNode(ServiceQueue queue)
        {
            _Queue = queue;
            Tag = _Queue;
            Text = string.Format("{0}:{1}-{2}", _Queue.Number, _Queue.Name, _Queue.CallHead);
            SetChildrenNode();
        }

        private void SetChildrenNode()
        {
            foreach (var element in _Queue.Elements)
            {
                var node = new ServiceQueueElementTreeNode(element);
                this.Nodes.Add(node);
            }
        }

    }
}