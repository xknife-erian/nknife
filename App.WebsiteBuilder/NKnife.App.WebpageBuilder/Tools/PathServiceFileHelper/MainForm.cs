using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace Jeelu.PathServiceFileHelper
{
    public partial class MainForm : Form
    {
        #region ��ʼ��

        XmlDocument _doc = new XmlDocument();
        string _strSoftRootPath = string.Empty;
        string _strSitePath = string.Empty;
        string _binPath = string.Empty;
        
        public MainForm()
        {
            InitializeComponent();
            this.textBox1.Text = Path.GetFullPath(@"..\..\BuildData\Debug\");
            this.textBox2.Text = Path.GetFullPath(@"..\..\BuildData\shitang_bak\");
            this.textBox3.Text = Path.GetFullPath(@"..\..\BuildData\Putout\");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _strSoftRootPath = this.textBox1.Text;
            _strSitePath = this.textBox2.Text;
            _binPath = this.textBox3.Text;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            #region ����Path.xml�ļ�

            _doc.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?><root><!--���ļ������������Զ����ɣ������ֶ����������--><softwarePath></softwarePath><sitePath></sitePath><userPath></userPath></root>");

            XmlElement eleSoftwarePath = (XmlElement)_doc.DocumentElement.SelectSingleNode("softwarePath");
            XmlElement eleSitePath = (XmlElement)_doc.DocumentElement.SelectSingleNode("sitePath");
            XmlElement eleUserPath = (XmlElement)_doc.DocumentElement.SelectSingleNode("userPath");

            string[] dirs = Directory.GetDirectories(_strSoftRootPath, "*", SearchOption.TopDirectoryOnly);
            foreach (string strDir in dirs)
            {
                /////��ʾ�Ƿ����������Ŀ¼���ļ���0��ʾ��������1��ʾ��Ҫ��2��ʾ�����������⴦��
                //int isContinue = 0;

                switch (Path.GetFileName(strDir))
                {
                    case "USER":
                        {
                            //CreateXmlElementFiles(_strSoftRootPath + @"\USER\zhangling", eleUserPath, _strSoftRootPath + @"\USER\zhangling", "USER_");
                            break;
                        }
                    case "Xslt":
                        {
                            CreateXmlElementFiles(_strSoftRootPath, eleSoftwarePath, strDir, "Xslt_");
                            break;
                        }
                    case "CHS":
                        {
                            string[] chsDirs = Directory.GetDirectories(strDir, "*", SearchOption.TopDirectoryOnly);
                            foreach (string chsSubDir in chsDirs)
                            {
                                CreateXmlElementDir(_strSoftRootPath, eleSoftwarePath, chsSubDir, "CL_");

                                if (Path.GetFileName(chsSubDir) == "DataSources")
                                {
                                    CreateXmlElementFiles(_strSoftRootPath, eleSoftwarePath, chsSubDir, "CL_DS_");
                                }
                                //else if (Path.GetFileName(chsSubDir) == "BlankProject")
                                //{
                                //    string[] blankDirs = Directory.GetDirectories(chsSubDir, "*", SearchOption.TopDirectoryOnly);
                                //    foreach (string blankSubDir in blankDirs)
                                //    {
                                //        CreateXmlElementDir(_strSoftRootPath, eleSoftwarePath, blankSubDir, "CL_BP_");
                                //    }
                                //}
                            }

                            CreateXmlElementFiles(_strSoftRootPath, eleSoftwarePath, strDir, "CL_");

                            break;
                        }
                    case "Config":
                        {
                            CreateXmlElementFiles(_strSoftRootPath, eleSoftwarePath, strDir, "Config_");
                            break;
                        }
                    default:
                        break;
                }

                XmlElement eleDir = _doc.CreateElement("directory");
                eleDir.SetAttribute("name", Path.GetFileName(strDir) + "_Folder");
                string path = strDir.Substring(_strSoftRootPath.Length);
                if (path[path.Length - 1] != '\\')
                {
                    path += @"\";
                }
                eleDir.SetAttribute("path", path);

                eleSoftwarePath.AppendChild(eleDir);
            }

            ///site
            CreateXmlElementFilesAll(_strSitePath, eleSitePath, _strSitePath, "Site_");

            string[] arrProjectDirs = Directory.GetDirectories(_strSitePath, "*", SearchOption.TopDirectoryOnly);
            foreach (string projectDir in arrProjectDirs)
            {
                CreateXmlElementDir(_strSitePath, eleSitePath, projectDir, "Site_");

                if (Path.GetFileName(projectDir) == "Root")
                {
                    string[] arrResourcesDir = Directory.GetDirectories(projectDir, "*", SearchOption.TopDirectoryOnly);
                    foreach (string rootDir in arrResourcesDir)
                    {
                        CreateXmlElementDir(_strSitePath, eleSitePath, rootDir, "Site_Root_");
                    }
                }
            }

            //string[] arrProjectFiles = Directory.GetFiles(_strSitePath, "*", SearchOption.AllDirectories);

            string putoutFilePath = Path.Combine(this.textBox3.Text, "Path.xml");
            FileStream stm = new FileStream(putoutFilePath,FileMode.Create,FileAccess.Write,FileShare.None);
            _doc.Save(new StreamWriter(stm,Encoding.UTF8));
            stm.Flush();
            stm.Close();

            this.Text = "Successfull!!!!!!!!!!!!";

            if (MessageBox.Show("���ɳɹ����Ƿ�λ���ļ���", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                System.Diagnostics.Process.Start("explorer", "/select," + putoutFilePath);
            }

            #endregion
        }

        #region һЩ����

        /// <summary>
        /// ����һЩFile
        /// </summary>
        /// <param name="strRootPath">��ȡ·���ã���Ϊһ�����Ȳ���</param>
        /// <param name="eleRootPath">eleSoftwarePath,eleSitePath,eleUserPath����һ��</param>
        /// <param name="strDir">��Ҫ���ɵ��ļ���</param>
        /// <param name="dirName">��ֱ�Ӹ�Ŀ¼�µ��ļ��ǣ���Ҫ�ӵ�ǰ׺</param>
        private void CreateXmlElementFiles(string strRootPath, XmlElement eleRootPath, string strDir,string dirName)
        {

            string[] arrSubFiles = Directory.GetFiles(strDir, "*", SearchOption.TopDirectoryOnly);
            foreach (string subFile in arrSubFiles)
            {
                ///ȥ�������ļ�
                if ((File.GetAttributes(subFile) & FileAttributes.Hidden) == FileAttributes.Hidden)
                {
                    continue;
                }
                XmlElement eleChsSubFile = _doc.CreateElement("file");
                eleChsSubFile.SetAttribute("name", dirName + Path.GetFileNameWithoutExtension(subFile));
                eleChsSubFile.SetAttribute("path", subFile.Substring(strRootPath.Length));

                eleRootPath.AppendChild(eleChsSubFile);
            }
        }

        /// <summary>
        /// ����һЩFile
        /// </summary>
        /// <param name="strRootPath">��ȡ·���ã���Ϊһ�����Ȳ���</param>
        /// <param name="eleRootPath">eleSoftwarePath,eleSitePath,eleUserPath����һ��</param>
        /// <param name="strDir">��Ҫ���ɵ��ļ���</param>
        /// <param name="dirName">��ֱ�Ӹ�Ŀ¼�µ��ļ��ǣ���Ҫ�ӵ�ǰ׺</param>
        private void CreateXmlElementFilesAll(string strRootPath, XmlElement eleRootPath, string strDir, string dirName)
        {
            string[] arrSubFiles = Directory.GetFiles(strDir, "*", SearchOption.AllDirectories);
            foreach (string subFile in arrSubFiles)
            {
                ///ȥ�������ļ�
                if ((File.GetAttributes(subFile) & FileAttributes.Hidden) == FileAttributes.Hidden)
                {
                    continue;
                }
                XmlElement eleChsSubFile = _doc.CreateElement("file");
                eleChsSubFile.SetAttribute("name", dirName + Path.GetFileNameWithoutExtension(subFile));
                eleChsSubFile.SetAttribute("path", subFile.Substring(strRootPath.Length));

                eleRootPath.AppendChild(eleChsSubFile);
            }
        }

        /// <summary>
        /// ����һ��Dir
        /// </summary>
        /// <param name="strRootPath">��ȡ·���ã���Ϊһ�����Ȳ���</param>
        /// <param name="eleRootPath">eleSoftwarePath,eleSitePath,eleUserPath����һ��</param>
        /// <param name="strDir">��Ҫ���ɵ��ļ���</param>
        /// <param name="dirName">��ֱ�Ӹ�Ŀ¼�µ��ļ��ǣ���Ҫ�ӵ�ǰ׺</param>
        private void CreateXmlElementDir(string strRootPath, XmlElement eleRootPath, string chsSubDir, string dirName)
        {
            XmlElement eleSubDir = _doc.CreateElement("directory");
            eleSubDir.SetAttribute("name", dirName + Path.GetFileNameWithoutExtension(chsSubDir) + "_Folder");
            string path = chsSubDir.Substring(strRootPath.Length);
            if (path[path.Length - 1] != '\\')
            {
                path += @"\";
            }
            eleSubDir.SetAttribute("path", path);

            eleRootPath.AppendChild(eleSubDir);
        }

        private string BuildPartCode(string prefix, string name)
        {
            string strFormat = @"
        static private string {2};
        static public string {1} {{ get {{ return Path.Combine({0},{2}); }} }}
";
            return string.Format(strFormat, prefix, name, ToVar(name));
        }

        string ToVar(string str)
        {
            return "_" + char.ToLower(str[0]).ToString() + str.Substring(1);
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            #region ����PathService.cs�ļ�

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Path.Combine(this.textBox3.Text, "Path.xml"));
            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes(@"*/*");
            StringBuilder sb = new StringBuilder();
            sb.Append(StaticText.BeginCode);

            string strCaseFormat = @"
                                    case ""{0}"":
                                        {1} = reader.GetAttribute(""path"");
                                        break;";
            foreach (XmlNode node in nodeList)
            {
                string strName = node.Attributes["name"].Value;
                sb.AppendFormat(strCaseFormat, strName, ToVar(strName));
            }

            sb.Append(StaticText.BeginCode2);

            foreach (XmlNode node in nodeList)
            {
                string prefix = "_softwarePath";
                if (node.ParentNode.Name == "sitePath")
                {
                    prefix = "ProjectPath";
                }
                else if (node.ParentNode.Name == "userPath")
                {
                    prefix = @"PathService.USER_Folder + @""\"" + UserService.UserID";
                }
                sb.Append(BuildPartCode(prefix, node.Attributes["name"].Value));
            }

            sb.Append(StaticText.EndCode);

            string putoutFilePath = Path.Combine(this.textBox3.Text, "PathService.cs");
            File.WriteAllText(putoutFilePath, sb.ToString(),Encoding.UTF8);

            this.Text = "OK!!!!!!!!!!!!";

            if (MessageBox.Show("���ɳɹ����Ƿ�λ���ļ���", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                System.Diagnostics.Process.Start("explorer", "/select," + putoutFilePath);
            }
            #endregion
        }

        #region ��ť�¼�

        //static private string _softwarePath;
        //static public string CL_DataSources_Folder
        //{
        //    get
        //    {
        //        return _softwarePath + @"\CHS\DataSources";
        //    }
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.SelectedPath = this.textBox1.Text;
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = f.SelectedPath;
                this._strSoftRootPath = this.textBox1.Text;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.SelectedPath = this.textBox2.Text;
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.textBox2.Text = f.SelectedPath;
                this._strSitePath = this.textBox2.Text;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.SelectedPath = this.textBox3.Text;
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.textBox3.Text = f.SelectedPath;
                this._binPath = this.textBox3.Text;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Process.Start(textBox1.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Process.Start(textBox2.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Process.Start(textBox3.Text);
        }
        #endregion

    }
}