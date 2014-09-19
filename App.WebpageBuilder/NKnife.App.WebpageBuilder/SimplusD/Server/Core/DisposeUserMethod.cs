using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Xml;
using System.IO;

namespace Jeelu.SimplusD.Server
{
    public class DisposeUserMethod
    {
        public static TcpClient Client { get;  set; }
        
        /// <summary>
        /// 将此用户删除,同时清楚对应的file字典
        /// </summary>
        public static void DeleteUser()
        {
            ServerCore.DicFileClient.Remove(Client);
            ServerCore.DicUserClient.Remove(Client);
        }

        /// <summary>
        /// 增加用户
        /// </summary>     
        public static void AddUser(string userName)
        {
            ServerCore.DicUserClient[Client]=userName;
        }

        /// <summary>
        /// 获取当前用户Ip
        /// </summary>
        public static string GetClientIp(TcpClient tcpClient)
        {
            try
            {
                return ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
            }
            catch
            {
                return null;
            }
        }

        public static void SetUserCurrentFile(string fileUrl)
        {
            ServerCore.DicFileClient[Client] = fileUrl;
        }

        //modified by zhangling on June 3,2008
        /// <summary>
        /// 用户名验证，且收集一些上传信息
        /// </summary>
        /// <param name="sdUserName">用户名</param>
        /// <param name="sdPassword">密码</param>
        /// <param name="sdTotle">文件总数</param>
        /// <param name="sdProject">项目</param>
        /// <param name="sdProjectGuid">项目id</param>
        /// <param name="totle">返回总数</param>
        /// <param name="userFilePath">返回用户部分路径</param>
        /// <param name="projectName">返回项目名</param>
        /// <param name="changedProjName">返回新名称(有同名的项目)</param>
        /// <param name="action">操作(新建,修改,删除)</param>
        /// <returns></returns>
        public static bool RegistUser(
            string sdUserName,
            string sdPassport,
            int sdTotal,
            string sdProject,
            string sdProjectGuid,
            out int total, out string userFilePath, out string projectName,out string changedProjName,out int action)
        {
            //modified by zhangling on June 3,2008
            total = sdTotal;
            userFilePath = "";
            projectName = sdProject;
            changedProjName = "";
            string userName = sdUserName;
            string passport = sdPassport;
            string projectId = sdProjectGuid;
            
            if (CheckUser.IsUser(userName, passport))
            {
                userFilePath = CommonService.GetUserRelativePath(userName, projectName); // zha\zhangling\projectname
                changedProjName = GetSavePath(userName, projectName, projectId, out action);
                //BulidSite.StrSdsiteFilePath = userFilePath;
                AddUser(userName);
                return true;
            }
            else
            {
                action = 0;
                return false;
            }
        }

        /// <summary>
        /// 返回一项目名
        /// </summary>
        /// <returns></returns>
        public static string GetSavePath(string userName, string projectName, string projectGuid,out int action)
        {
            string userPath = CommonService.GetUserPath(userName);
            string userXmlPath = Path.Combine(AnyFilePath.SdSiteFilePath, userPath) + @"\user.xml";
            //判断USER文件是否存在，在则进行相应判断，不在则创建
            if (File.Exists(userXmlPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(userXmlPath);
                XmlElement xmlEle = xmlDoc.SelectSingleNode(string.Format("//project [@ guid='{0}']", projectGuid)) as XmlElement;
               
                //如果有guid文件则说明是同一个文件，则还用原来的文件
                if (xmlEle != null)
                {
                    action = 2; //修改状态
                    return xmlEle.GetAttribute("fileName");
                }
                //若guid不存在则判断文件是否存在，不存在新建，存在则找到该文件的的名字后包含的最大数+1作为其路径
                else
                {
                    //add a definition by zhangling on May 30,2008
                    //在user.xml文件中,针对同网站名称的同名时，则在其网站名后加数字，以示区分
                    XmlElement fileXmlEle = xmlDoc.SelectSingleNode(string.Format("//project [@ fileName='{0}']", projectName)) as XmlElement;
                    if (fileXmlEle != null)
                    {
                        int oldFileCount = 1;
                        while (fileXmlEle != null)
                        {
                            projectName = projectName + "_" + oldFileCount;
                            fileXmlEle = xmlDoc.SelectSingleNode(string.Format("//project [@fileName='{0}'] ", projectName)) as XmlElement;
                            oldFileCount += 1;
                        }
                      
                    }
                      XmlElement newFileXmlEle= xmlDoc.CreateElement("project");
                       newFileXmlEle.SetAttribute("guid",projectGuid);
                       newFileXmlEle.SetAttribute("fileName", projectName);
                       xmlDoc.DocumentElement.AppendChild(newFileXmlEle);
                       xmlDoc.Save(userXmlPath);
                       action = 1;//新建状态
                       return projectName;
                }

            }
            else
            {
                XmlDocument userXmlDoc = new XmlDocument();
                userXmlDoc.LoadXml("<USER/>");
                XmlElement xmlEle = userXmlDoc.CreateElement("project");
                xmlEle.SetAttribute("guid", projectGuid);
                xmlEle.SetAttribute("fileName", projectName);
                userXmlDoc.DocumentElement.AppendChild(xmlEle);

                string dirPath =Path.GetDirectoryName(userXmlPath);
                Directory.CreateDirectory(dirPath);
                userXmlDoc.Save(userXmlPath);

                action = 1; //新建状态
                return projectName;
            }
        }
    }
}
