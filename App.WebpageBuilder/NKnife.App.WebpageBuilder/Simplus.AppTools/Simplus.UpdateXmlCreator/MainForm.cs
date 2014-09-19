using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;

namespace Simplus.UpdateXmlCreator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ShowFolderSchema(string path)
        {
            if (path != string.Empty && Directory.Exists(path))
            {
                this.binFileTreeView.Nodes.Clear();
                this.binFileTreeView.Nodes.AddRange(this.LoadFolder(path));
                this.binFileTreeView.Nodes.AddRange(this.LoadFile(path));
            }
        }

        private TreeNode[] LoadFolder(string folderPath)
        {
            DirectoryInfo folder = new DirectoryInfo(folderPath);
            DirectoryInfo[] subFolders = folder.GetDirectories();
            List<TreeNode> nodeList = new List<TreeNode>();
            foreach (DirectoryInfo subFolder in subFolders)
            {
                if (subFolder.Attributes != FileAttributes.System)
                {
                    TreeNode node = new TreeNode(subFolder.Name);
                    node.Checked = true;
                    node.Tag = subFolder;
                    node.ToolTipText = "共 " + subFolder.GetDirectories().Length.ToString() + " 个目录\r\n共 "
                        + subFolder.GetFiles().Length.ToString() + " 个文件";
                    nodeList.Add(node);
                    node.Nodes.AddRange(this.LoadFolder(subFolder.FullName));
                    node.Nodes.AddRange(this.LoadFile(subFolder.FullName));
                }
            }
            return nodeList.ToArray();
        }

        private TreeNode[] LoadFile(string path)
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            List<TreeNode> nodeList = new List<TreeNode>();
            foreach (FileInfo file in folder.GetFiles())
            {
                TreeNode node = new TreeNode(file.Name);
                node.Checked = true;
                //node.ImageIndex
                switch (file.Extension.ToLower())
                {
                    case ".dll":
                    case ".exe":
                        {
                            KeyValuePair<string, object> kv = this.SetNodeTipAndTagFromDll(file);
                            node.ToolTipText = kv.Key;
                            node.Tag = kv.Value;
                            break;
                        }
                    default:
                        {
                            node.ToolTipText = "文件大小: " + file.Length.ToString() + "字节";
                            node.Tag = file;
                            break;
                        }
                }
                nodeList.Add(node);
            }
            return nodeList.ToArray();
        }

        private KeyValuePair<string, object> SetNodeTipAndTagFromDll(FileInfo file)
        {
            string str;
            object obj;
            try
            {
                Assembly ass = Assembly.LoadFile(file.FullName);
                object[] vers = ass.GetCustomAttributes(typeof(AssemblyVersionAttribute), false);
                str = ((AssemblyVersionAttribute)vers[0]).Version;
                obj = (AssemblyVersionAttribute)vers[0];
            }
            catch (Exception)
            {
                str = "文件大小: " + file.Length.ToString() + "字节";
                obj = file;
            }
            return new KeyValuePair<string, object>(str, obj);
        }

        private void 新建NToolStripButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            //dialog.RootFolder = Environment.SpecialFolder.MyDocuments;// = @"D:\SimplusD";
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.ShowFolderSchema(dialog.SelectedPath);
            }
        }

        private void binFileTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.fileInfoPropertyGrid.SelectedObject = e.Node.Tag;
        }

    }

}
