using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    [ComVisible(true)]
    public class DataForScriptInStartup
    {
        public DataForScriptInStartup()
        {
        }
        /// <summary>
        /// 返回最近打开的项目,供欢迎界面列表显示
        /// </summary>
        /// <returns></returns>
        public string LatestProject()
        {
            RecentFileInfo[] recentFiles = Service.RecentFiles.GetFiles(Service.RecentFiles.RecentOpenProjects);
            string retstr = "";
            for (int i = 0; i < recentFiles.Length; i++)
            {
                RecentFileInfo project = recentFiles[i];
                if (i != recentFiles.Length - 1)
                    retstr += project.FilePath + ",";
                else
                    retstr += project.FilePath + "";
            }
            return retstr;
        }

        /// <summary>
        /// 打开项目
        /// </summary>
        /// <param name="projectPath">如果为空字符串,则弹出打开对话框,否则打开对应的文件</param>
        public void OpenProject(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
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
            }
            else
            {
                Service.Workbench.ShowDialogForOpenProject();
            }
        }

        /// <summary>
        /// 鼠标划过时状态栏的显示
        /// </summary>
        /// <param name="projectPath"></param>
        public void MouseOverProject(string projectPath)
        {
            Service.StatusBar.ShowMessage(projectPath);
        }

        /// <summary>
        /// 新建项目
        /// </summary>
        public void CreateProject()
        {
            Service.Workbench.ShowDialogForCreateProject();
        }
    }
}
