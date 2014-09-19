using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Jeelu.WordSeg
{
    /// <summary>
    /// 字典类
    /// </summary>
    internal class Dict
    {
        public WordPosCollection LoadFromString()
        {
            WordPosCollection wpList = new WordPosCollection();
            string[] words = new string[3]; //= Initialize.SegWords;
            foreach (string word in words)
            {
                try
                {
                    string[] wordSplit = word.Split(new string[] { "\t\t" }, StringSplitOptions.RemoveEmptyEntries);
                    WordPos dictStruct = new WordPos(wordSplit[0], int.Parse(wordSplit[1]));
                    wpList.WordPosList.Add(dictStruct);
                }
                catch { }
            }
            return wpList;
        }

        /// <summary>
        /// 从文本文件读取字典
        /// </summary>
        /// <param name="fileName"></param>
        public WordPosCollection LoadFromTextDict(String fileName)
        {
            WordPosCollection dictFile = new WordPosCollection();

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

                WordPos dict = new WordPos(wp[0], pos);

                if (dict.Word.Contains("一") || dict.Word.Contains("二") ||
                    dict.Word.Contains("三") || dict.Word.Contains("四") ||
                    dict.Word.Contains("五") || dict.Word.Contains("六") ||
                    dict.Word.Contains("七") || dict.Word.Contains("八") ||
                    dict.Word.Contains("九") || dict.Word.Contains("十"))
                {
                    dict.Pos |= (int)PosEnum.POS_A_M;
                }

                if (dict.Word == "字典")
                {
                    dict.Pos = (int)PosEnum.POS_D_N;
                }

                dictFile.WordPosList.Add(dict);
            }

            return dictFile;
        }

        public void SaveToBinFile(String fileName, WordPosCollection dictFile)
        {
            Stream s = CSerialization.SerializeBinary(dictFile);
            s.Position = 0;
            CFile.WriteStream(fileName, (MemoryStream)s);
        }

        public void SaveDictToTextFile(string dictpath, WordPosCollection dictFile)
        {
            StreamWriter sw = new StreamWriter(dictpath, false, Encoding.UTF8);
            foreach (WordPos dict in dictFile.WordPosList)
            {
                sw.WriteLine("{0}\t\t{1}", dict.Word, dict.Pos);
            }
            sw.Close();
        }

        public WordPosCollection LoadFromTextFile(string dictpath)
        {
            WordPosCollection file = new WordPosCollection();
            StreamReader sr = new StreamReader(dictpath, Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                try
                {
                    string[] word = sr.ReadLine().Split(new string[] { "\t\t" }, StringSplitOptions.RemoveEmptyEntries);
                    WordPos dict = new WordPos(word[0], int.Parse(word[1]));
                    file.WordPosList.Add(dict);
                }
                catch { }
            }
            sr.Close();
            return file;
        }

        public WordPosCollection LoadFromBinFile(String fileName)
        {
            return LoadFromTextFile(fileName);
        }
    }
}
