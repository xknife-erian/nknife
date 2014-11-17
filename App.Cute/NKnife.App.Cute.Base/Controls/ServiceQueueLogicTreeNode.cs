using System.Windows.Forms;
using Didaku.Wrapper;
using Didaku.Engine.Timeaxis.Base.Controls.Menus;
using Didaku.Engine.Timeaxis.Base.Implement;

namespace Didaku.Engine.Timeaxis.Base.Controls
{
    public sealed class ServiceQueueLogicTreeNode : TreeNode
    {
        private readonly ServiceQueueLogic _Logic;
        private readonly IdName _IdName;
        public ServiceQueueLogicTreeNode(ServiceQueueLogic logic)
        {
            _Logic = logic;
            _IdName = logic.Counter.GetIdName();
            Tag = _IdName.Id;
            Text = string.Format("{0} - {1}", _IdName.Name, 0).ToUpper();
            SetChildrenNode();
        }

        public void UpdateCount(int count)
        {
            Text = string.Format("{0} - {1}", _IdName.Name, count).ToUpper();
        }

        private void SetChildrenNode()
        {
            foreach (var group in _Logic)
            {
                var node = new ServiceQueueGroupTreeNode(group);
                this.Nodes.Add(node);
            }
        }
    }
}