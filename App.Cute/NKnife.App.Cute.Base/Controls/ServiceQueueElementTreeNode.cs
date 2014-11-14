using System.Windows.Forms;
using Didaku.Engine.Timeaxis.Base.Interfaces;

namespace Didaku.Engine.Timeaxis.Base.Controls
{
    public class ServiceQueueElementTreeNode : TreeNode
    {
        private readonly IServiceQueueElement _Element;
        public ServiceQueueElementTreeNode(IServiceQueueElement element)
        {
            _Element = element;
            var idName = element.GetIdName();
            Text = string.Format("[{0}] - {1}", idName.Name, idName.Id).ToUpper();
            SetChildrenNode();
        }

        private void SetChildrenNode()
        {
        }

    }
}