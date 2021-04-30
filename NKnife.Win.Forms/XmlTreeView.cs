using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace NKnife.Win.Forms
{
    /// <summary>一个显示XML的树
    /// </summary>
    public class XmlTreeView : TreeView
    {
        public TreeNode BindXml(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var treeNode = new TreeNode();
            if (doc.DocumentElement != null)
            {
                treeNode.Text = GetNodeText(doc.DocumentElement);
                BindXmlDocument(doc.DocumentElement, treeNode);
                Nodes.Clear();
                Nodes.Add(treeNode);
                return treeNode;
            }
            return null;
        }

        public void BindXmlDocument(XmlNode xmlNode, TreeNode treeNode)
        {
            if (xmlNode== null)
            {
                return;
            }
            foreach (XmlNode subNode in xmlNode.ChildNodes)
            {
                switch (subNode.NodeType)
                {
                    case XmlNodeType.Element:
                    {
                        var nodeText = GetNodeText(subNode);
                        var newtreeNode = new TreeNode(nodeText);
                        treeNode.Nodes.Add(newtreeNode);
                        if (subNode.HasChildNodes)
                        {
                            BindXmlDocument(subNode, newtreeNode);
                        }
                        break;
                    }
                    case XmlNodeType.Text:
                    {
                        if (!string.IsNullOrWhiteSpace(subNode.Value))
                        {
                            var valueNode = new TreeNode(subNode.Value);
                            treeNode.Nodes.Add(valueNode);
                        }
                        break;
                    }
                }

            }
        }

        private string GetNodeText(XmlNode node)
        {
            var sb = new StringBuilder();
            sb.Append(node.LocalName).Append(" ");
            if (node.Attributes != null && node.Attributes.Count >= 0)
            {
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    sb.Append($"{attribute.LocalName}=\"{attribute.Value}\"").Append(' ');
                }
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Insert(0, '<');
            sb.Append('>');
            return sb.ToString();
        }
    }
}