using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public class RecentRecordMenuItem : MyMenuItem
    {
        private string _recentType;
        public RecentRecordMenuItem(string text,string keyId,string recentType)
            :base(text,keyId)
        {
            _recentType = recentType;
            ReLoad();
            Service.RecentFiles.FileChanged += delegate
            {
                ReLoad();
            };
            SoftwareOption.Saved += delegate
            {
                ReLoad();
            };
        }

        public virtual void ReLoad()
        {
            this.DropDownItems.Clear();
            switch (_recentType)
            {
                case "file":
                    {
                        RecentFileInfo[] recentFiles = Service.RecentFiles.GetFiles(Service.RecentFiles.RecentOpenFiles);

                        if (recentFiles.Length == 0)
                        {
                            ToolStripMenuItem item = new ToolStripMenuItem(
                                StringParserService.Parse("${res:SimplusD.mainMenu.hasNotRecentFile}"));
                            item.Enabled = false;
                            this.DropDownItems.Add(item);
                        }
                        else
                        {
                            foreach (RecentFileInfo recent in recentFiles)
                            {
                                ToolStripMenuItem item = new ToolStripMenuItem(recent.FilePath);
                                this.DropDownItems.Add(item);
                            }
                        }
                        break;
                    }
                case "project":
                    {
                        RecentFileInfo[] recentFiles = Service.RecentFiles.GetFiles(Service.RecentFiles.RecentOpenProjects);
                        EventHandler clickHandler = delegate(object sender, EventArgs e)
                        {
                            ///打开项目
                            string filePath = (string)((ToolStripMenuItem)sender).Tag;

                            if (File.Exists(filePath))
                            {
                                Service.Project.OpenProject(filePath);
                            }
                            else
                            {
                                if (MessageService.Show("${res:SimplusD.mainMenu.deleteNotExistsFileTip}", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                                    == DialogResult.OK)
                                {
                                    Service.RecentFiles.DeleteFilePath(Service.RecentFiles.RecentOpenProjects, filePath);
                                }
                            }
                        };

                        if (recentFiles.Length == 0)
                        {
                            ///没有最近的记录，则显示为：空
                            ToolStripMenuItem item = new ToolStripMenuItem(
                                StringParserService.Parse("${res:SimplusD.mainMenu.hasNotRecentProject}"));
                            item.Enabled = false;
                            this.DropDownItems.Add(item);
                        }
                        else
                        {
                            foreach (RecentFileInfo recent in recentFiles)
                            {
                                ToolStripMenuItem item = new ToolStripMenuItem(recent.FilePath);
                                item.Tag = recent.FilePath;
                                item.Click += clickHandler;
                                this.DropDownItems.Add(item);
                            }

                            ///添加间隔符
                            ToolStripSeparator separator = new ToolStripSeparator();
                            this.DropDownItems.Add(separator);

                            ///清空记录的选项
                            ToolStripMenuItem itemClear = new ToolStripMenuItem(
                                StringParserService.Parse("${res:SimplusD.mainMenu.clearRecentProject}"));
                            itemClear.Click += delegate
                            {
                                if (MessageService.Show("${res:SimplusD.mainMenu.clearTip}", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                                    == DialogResult.OK)
                                {
                                    Service.RecentFiles.ClearFilePath(Service.RecentFiles.RecentOpenProjects);
                                }
                            };
                            this.DropDownItems.Add(itemClear);
                        }
                        break;
                    }
                default:
                    throw new Exception();
            }
        }
    }
}
