using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public class BaseSiteCreator : SiteCreator
    {
        public BaseSiteCreator()
        {
        }

        public override void CreateSite(string sitePath,string siteName)
        {
            CreateProjectFiles(sitePath, siteName);
        }

        static string CreateProjectFiles(string path, string projectName)
        {
            ///�����ļ����ļ���
            string projectPath = Path.Combine(path, projectName);
            string projectSite = Path.Combine(projectPath, projectName + ".sdsite");
            Directory.CreateDirectory(projectPath);

            ///��������Ŀ����
            string projectFolder = Path.Combine(PathService.CL_BlankProject_Folder, "[projectfolder]");
            string projectFile = Path.Combine(projectFolder, "[project].sdsite");

            Utility.File.DirectoryCopy(projectFolder, projectPath);
            string projectFileOld = Path.Combine(projectPath, "[project].sdsite");
            File.Move(projectFileOld, projectSite);

            XmlDocument docSdsite = new XmlDocument();
            ///��.sdsite���һ��guid
            docSdsite.Load(projectSite);
            docSdsite.DocumentElement.SetAttribute("id", Guid.NewGuid().ToString("N"));
            docSdsite.Save(projectSite);

            File.SetAttributes(projectSite, FileAttributes.Normal);
            return projectSite;

            ///�����ļ����ļ���
            //string projectSiteFolder = Path.Combine(path, projectName);
            //string projectPath = Path.Combine(projectSiteFolder, projectName);
            //string projectSite = Path.Combine(projectSiteFolder, projectName + ".sdsite");
            //Directory.CreateDirectory(projectPath);

            /////��������Ŀ����
            //Utility.File.DirectoryCopy(Path.Combine(PathService.CL_BlankProject_Folder, "[projectfolder]"),
            //    projectPath);
            //File.Copy(Path.Combine(PathService.CL_BlankProject_Folder, "[project].sdsite"),
            //    projectSite);
            //File.SetAttributes(projectSite, FileAttributes.Normal);
            //return projectSite;
        }
    }
}
