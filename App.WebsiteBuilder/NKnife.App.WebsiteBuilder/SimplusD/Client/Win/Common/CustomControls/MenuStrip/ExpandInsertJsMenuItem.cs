using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Data;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    public class ExpandInsertJsMenuItem : MyMenuItem
    {
        public ExpandInsertJsMenuItem(string text, string keyId)
            : base(text,keyId)
        {
            CreateChildItems(this, PathService.JsPlugIns_Folder);
        }

        /// <summary>
        /// 通过目录创建子项
        /// </summary>
        void CreateChildItems(ToolStripMenuItem parentItem,string path)
        {
            ///递归处理文件夹
            string[] dirs = Directory.GetDirectories(path,"*",SearchOption.TopDirectoryOnly);
            foreach (string dir in dirs)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(Path.GetFileName(dir));
                parentItem.DropDownItems.Add(item);
                CreateChildItems(item, dir);
            }

            ///处理文件
            EventHandler handler = delegate(object sender, EventArgs e)
            {
                ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
                string filePath = (string)menuItem.Tag;
                ShowPlugInForm(filePath);
            };

            string[] files = Directory.GetFiles(path, "*.config.xml", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(Path.GetFileName(file));
                item.Tag = file;
                item.Click += handler;
                parentItem.DropDownItems.Add(item);
            }
        }

        private void ShowPlugInForm(string filePath)
        {
            //MyPlugInForm form =  new MyPlugInForm(filePath);
            //if (form.ShowDialog() == DialogResult.OK)
            //{
            //    OnJsBuilded(new StringEventArgs(form.JsString));
            //}
        }

        public event EventHandler<StringEventArgs> JsBuilded;
        protected virtual void OnJsBuilded(StringEventArgs e)
        {
            if (JsBuilded != null)
            {
                JsBuilded(this, e);
            }
        }
    }
}
