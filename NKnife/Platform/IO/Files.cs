using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace NKnife.Platform.IO
{
    public class Files
    {
        //遍历文件夹下面的文件
        public static ArrayList GetFileNameList(string DirInfo)
        {
            if (!Directory.Exists(DirInfo))//如果不存在这个文件夹
            {
                return null;
            }

            ArrayList LocalArrayList = new ArrayList();
            string[] TempFileString;
            foreach (string FileName in Directory.GetFiles(DirInfo))
            {
                if (File.Exists(FileName))
                {
                    TempFileString = FileName.Split('\\');
                    LocalArrayList.Add(TempFileString[TempFileString.Length - 1]);
                }
            }

            return LocalArrayList;
        }

        //读取文件内容并插入数据库
        public static ArrayList ReadFileToDatabase(string fileName)
        {
            try
            {
                StreamReader objReader = new StreamReader(fileName);
                string sLine = "";
                ArrayList arrText = new ArrayList();

                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        arrText.Add(sLine);
                }
                objReader.Close();
                return arrText;
            }
            catch
            {
                return null;
            }
        }

        //生成文件并将数据写入文件
        public static bool WriteFileFromReader(string filename,string areaCode, IDataReader myReader)
        {
            try
            {
                StreamWriter objWriter = new StreamWriter(File.Open(filename, FileMode.OpenOrCreate));
                StringBuilder lineString;
                while (myReader.Read())
                {
                    
                    lineString = new StringBuilder();
                    lineString.Append(areaCode + myReader[0].ToString());  //BranchID
                    lineString.Append("|");
                    lineString.Append(DateTime.Now.ToString("yyyyMMdd"));
                    lineString.Append("|");
                    lineString.Append(myReader[1].ToString());
                    lineString.Append("|");
                    lineString.Append(myReader[2].ToString());
                    lineString.Append("|");
                    lineString.Append(myReader[3].ToString());
                    lineString.Append("|");
                    lineString.Append(myReader[4].ToString());
                    lineString.Append("|");
                    lineString.Append(myReader[5].ToString());
                    lineString.Append("|");
                    lineString.Append(myReader[6].ToString());
                    lineString.Append("|");
                    lineString.Append(myReader[7].ToString());
                    lineString.Append("|");
                    lineString.Append(myReader[8].ToString());
                    lineString.Append("|");
                    if (Convert.ToString(myReader[9]).Trim().Length == 0)
                    {
                        lineString.Append("00");
                    }
                    else
                    {
                        lineString.Append(myReader[9].ToString());
                    }
                    lineString.Append("|");
                    if (Convert.ToString(myReader[10]).Trim().Length == 0)
                    {
                        lineString.Append("00");
                    }
                    else
                    {
                        lineString.Append(myReader[10].ToString());
                    }

                    objWriter.Write(lineString.ToString());
                    objWriter.WriteLine();
                }
                objWriter.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //根据网点名称生成文件名，时间是当天日期
        public static string GenerateFileName(string branchID,string areaCode,string hisDownFailDate)
        {
            string FileNameString = string.Empty;
            if (hisDownFailDate.Length > 0)
            {
                FileNameString = "BJRZ_" + areaCode + branchID + "_" + DateTime.Today.ToString("yyyyMMdd") + "_" + hisDownFailDate + ".txt";
            }
            else
            {
                FileNameString = "BJRZ_" + areaCode + branchID + "_" + DateTime.Today.ToString("yyyyMMdd") + "_" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            }
            return FileNameString;
        }

        //判断文件名称是否复合规则
        public static bool CheckFileName(string fileName)
        {
            Regex FileNameRegex = new Regex("^(phjsj_)[0-9]{4,9}[.][0-9]{8}[.](txt)$");
            if (FileNameRegex.IsMatch(fileName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
