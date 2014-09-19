using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Jeelu.Billboard
{
    /// <summary>
    /// 停用词字典
    /// 格式：文本文件格式，一个词占一行
    /// design by lukan, 2008-7-11 01:18:25
    /// </summary>
    public class JDictionaryStop
    {
        public JDictionaryStop(string fileName)
        {
            this.Load(fileName, new Dictionary<string, int>());
        }
        public JDictionaryStop(string fileName, Dictionary<string, int> stopwordDic)
        {
            this.Load(fileName, stopwordDic);
        }
        public FileInfo FileInfo { get; set; }
        public Dictionary<string, int> StopWordDic { get; set; }
        /// <summary>
        /// 词库文件的版本号
        /// </summary>
        public uint Version { get; set; }
        /// <summary>
        /// 词库所在频道是否是根频道（类别）
        /// </summary>
        public bool IsRootChannel { get; set; }
        /// <summary>
        /// 词库是否有子频道（类别）
        /// </summary>
        public bool HasSubChannel { get; set; }
        /// <summary>
        /// 词典所在频道（类别）
        /// </summary>
        public string ChannelName { get; set; }
        /// <summary>
        /// 从停用词字典中加载停用词
        /// </summary>
        /// <param name="chsFileName">中文停用词</param>
        /// <remarks>对文件存取的异常不做异常处理，由调用者进行异常处理</remarks>
        public virtual void Load(string fileName, Dictionary<string, int> stopwordDic)
        {
            if (!File.Exists(fileName))/// 文件不存在，创建一个新文件
            {
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                fs.Flush();
                fs.Close();
                fs.Dispose();
                return;
            }
            this.FileInfo = new FileInfo(fileName);
            this.StopWordDic = stopwordDic;

            int i = 0;
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(fileName, Encoding.UTF8);

                //加载停用词文件
                while (!reader.EndOfStream)
                {
                    //按行读取中文停用词
                    string word = reader.ReadLine();

                    //如果哈希表中不包括该停用词则添加到哈希表中
                    if (!stopwordDic.ContainsKey(word))
                    {
                        stopwordDic.Add(word, i);
                        i++;
                    }
                }
                reader.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                reader.Dispose();
            }
        }

        /// <summary>
        /// 将停用词保存到文件中 
        /// </summary>
        /// <param name="fileName">要保存文件名</param>
        /// <remarks>对文件存取的异常不做异常处理，由调用者进行异常处理</remarks>
        public virtual void Save()
        {
            try
            {
                /// 创建一个新停用词的文本文件，若该文件存在则覆盖
                FileStream fs = new FileStream(this.FileInfo.FullName, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));

                /// 遍历停用词表，写入文件
                foreach (KeyValuePair<string, int> item in this.StopWordDic)
                {
                    sw.WriteLine(item.Key);
                }
                sw.Close();
                fs.Flush();
                fs.Close();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 增加一个停用词
        /// </summary>
        /// <param name="word"></param>
        public virtual void AddWord(string word)
        {
            //如果原来词库中已存在，则不做任何操作
            if (this.StopWordDic.ContainsKey(word))
            {
                return;
            }
            this.StopWordDic.Add(word, this.StopWordDic.Count);
        }

        /// <summary>
        /// 删除一个停用词
        /// </summary>
        /// <param name="word"></param>
        public virtual void DeleteWord(string word)
        {
            //如果原来词库中不存在，则不做任何操作
            this.StopWordDic.Remove(word);
        }


    }
}
