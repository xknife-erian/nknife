using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Jeelu.WordSegmentor;

namespace Jeelu.WordSegmentor
{
    [Serializable]
    public class T_DictFile
    {
        public List<T_DictStruct> Dicts = new List<T_DictStruct>();
    }

    [Serializable]
    public class T_DictStruct
    {
        /// <summary>
        /// 单词
        /// </summary>
        public String Word;

        /// <summary>
        /// 词性
        /// </summary>
        public int Pos;

        /// <summary>
        /// 词频
        /// </summary>
        public double Frequency;

        public override string ToString()
        {
            return Word;
        }
    }

    public class Dict
    {
        /// <summary>
        /// 从文本文件读取字典
        /// </summary>
        /// <param name="fileName"></param>
        static public T_DictFile LoadFromTextDict(String fileName)
        {
            T_DictFile dictFile = new T_DictFile();

            String dictStr = CFile.ReadFileToString(fileName, "utf-8");

            String[] words = CRegex.Split(dictStr, "\r\n");

            foreach (String word in words)
            {
                String[] wp = CRegex.Split(word, @"\|");

                if (wp == null)
                {
                    continue;
                }

                if (wp.Length != 2)
                {
                    continue;
                }

                int pos = 0;

                try
                {
                    pos = int.Parse(wp[1]);
                }
                catch
                {
                    continue;
                }

                T_DictStruct dict = new T_DictStruct();
                dict.Word = wp[0];
                dict.Pos = pos;

                if (dict.Word.Contains("一") || dict.Word.Contains("二") ||
                    dict.Word.Contains("三") || dict.Word.Contains("四") ||
                    dict.Word.Contains("五") || dict.Word.Contains("六") ||
                    dict.Word.Contains("七") || dict.Word.Contains("八") ||
                    dict.Word.Contains("九") || dict.Word.Contains("十"))
                {
                    dict.Pos |= (int)T_POS.POS_A_M;
                }

                if (dict.Word == "字典")
                {
                    dict.Pos = (int)T_POS.POS_D_N;
                }
            
                dictFile.Dicts.Add(dict);
            }

            return dictFile;
        }

        static public T_DictFile LoadFromBinFile(String fileName)
        {
            MemoryStream s = CFile.ReadFileToStream(fileName);
            s.Position = 0;
            object obj;
            CSerialization.DeserializeBinary(s, out obj);
            return (T_DictFile)obj;
        }

        static public T_DictFile LoadFromBinFileEx(string fileName)
        {
            T_DictFile dictFile = new T_DictFile();
            dictFile.Dicts = new List<T_DictStruct>();

            File.SetAttributes(fileName, FileAttributes.Normal);
            FileStream fs = new FileStream(fileName, FileMode.Open);

            byte[] version = new byte[32];
            fs.Read(version, 0, version.Length);
            String ver = Encoding.UTF8.GetString(version, 0, version.Length);

            String verNumStr = CRegex.GetMatch(ver, "KTDictSeg Dict V(.+)", true);

            if (verNumStr == null || verNumStr == "")
            {
                //1.3以前版本

                fs.Close();
                return LoadFromBinFile(fileName);
            }

            while (fs.Position < fs.Length)
            {
                byte[] buf = new byte[sizeof(int)];
                fs.Read(buf, 0, buf.Length);
                int length = BitConverter.ToInt32(buf, 0);

                buf = new byte[length];

                T_DictStruct dict = new T_DictStruct();

                fs.Read(buf, 0, buf.Length);

                dict.Word = Encoding.UTF8.GetString(buf, 0, length - sizeof(int) - sizeof(double));
                dict.Pos = BitConverter.ToInt32(buf, length - sizeof(int) - sizeof(double));
                dict.Frequency = BitConverter.ToDouble(buf, length - sizeof(double));
                dictFile.Dicts.Add(dict);
            }

            fs.Close();

            return dictFile;
        }

        static public void SaveToTextFile(String fileNmae, T_DictFile dictFile)
        {
            if (dictFile.Dicts == null)
            {
                return;
            }

            StringBuilder dictStr = new StringBuilder();

            foreach (T_DictStruct dict in dictFile.Dicts)
            {
                dictStr.AppendFormat("{0}|{1}\r\n", dict.Word, dict.Pos);
            }

            CFile.WriteString(fileNmae, dictStr.ToString(), "utf-8");
        }

        static public void SaveToBinFile(String fileName, T_DictFile dictFile)
        {
            Stream s = CSerialization.SerializeBinary(dictFile);
            s.Position = 0;
            CFile.WriteStream(fileName, (MemoryStream)s);
        }

        static public void SaveToBinFileEx(String fileName, T_DictFile dictFile)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            byte[] version = new byte[32];

            int i = 0;
            foreach (byte v in System.Text.Encoding.UTF8.GetBytes("KTDictSeg Dict V1.3"))
            {
                version[i] = v;
                i++;
            }

            fs.Write(version, 0, version.Length);

            foreach (T_DictStruct dict in dictFile.Dicts)
            {
                byte[] word = System.Text.Encoding.UTF8.GetBytes(dict.Word);
                byte[] pos = System.BitConverter.GetBytes(dict.Pos);
                byte[] frequency = System.BitConverter.GetBytes(dict.Frequency);
                byte[] length = System.BitConverter.GetBytes(word.Length + frequency.Length + pos.Length);

                fs.Write(length, 0, length.Length);
                fs.Write(word, 0, word.Length);
                fs.Write(pos, 0, pos.Length);
                fs.Write(frequency, 0, frequency.Length);
            }

            fs.Close();
        }

    }
}
