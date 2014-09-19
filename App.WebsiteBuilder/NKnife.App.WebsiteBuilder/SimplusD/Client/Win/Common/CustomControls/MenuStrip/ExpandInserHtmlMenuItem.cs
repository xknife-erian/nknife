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
    public class ExpandInserHtmlMenuItem : MyMenuItem
    {
        public ExpandInserHtmlMenuItem(string text, string keyId)
            : base(text, keyId)
        {
            CreateChildItems(this, PathService.HtmlPlugIns_Folder);
        }
        void CreateChildItems(ToolStripMenuItem parentItem, string path)
        {
            ///递归处理文件夹

            string[] dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
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
            try
            {
                //MyHtmlPlugInForm form = new MyHtmlPlugInForm(filePath);
                //if (form.Init())
                //{
                //    if (form.ShowDialog() == DialogResult.OK)
                //    {
                //        OnHtmlBuilded(new StringEventArgs(form.HtmlString));
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("你所生要生成的Html扩展插入的原文件格式不正确!");
                //}
            }
            catch
            {
                MessageBox.Show("出现未知错误,此程序被终止!");
            }
        }

        public event EventHandler<StringEventArgs> HtmlBuilded;
        protected virtual void OnHtmlBuilded(StringEventArgs e)
        {
            if (HtmlBuilded != null)
            {
                HtmlBuilded(this, e);
            }
        }
    }

    public class StringEventArgs : EventArgs
    {
        private string _value;
        public string Value
        {
            get { return _value; }
        }
        public StringEventArgs(string value)
        {
            _value = value;
        }
    }
}
