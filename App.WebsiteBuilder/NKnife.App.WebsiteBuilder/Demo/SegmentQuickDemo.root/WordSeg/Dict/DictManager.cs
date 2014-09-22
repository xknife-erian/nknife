using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Jeelu.WordSeg
{
    internal class DictManager
    {
        public WordPosCollection dictFile;
        WordPosBuilder _POS = new WordPosBuilder();
        bool isReg;

        public void LoadDict(string dictpath)
        {
            Dict dict = new Dict();
            dictFile = dict.LoadFromBinFile(dictpath + "Dict.dct");

            foreach (WordPos word in dictFile.WordPosList)
            {
                _POS.AddWordPos(word.Word, word.Pos);
            }
        }

        public void LoadDictFromText(string dictpath)
        {
            Dict dict = new Dict();
            dictFile = dict.LoadFromTextFile(dictpath + "Dict.dct");

            foreach (WordPos word in dictFile.WordPosList)
            {
                _POS.AddWordPos(word.Word, word.Pos);
            }
        }

        /// <summary>
        /// ���ӵ�����
        /// </summary>
        /// <param name="newWord"></param>
        /// <param name="pos"></param>
        public void AddDict(string newWord, int pos)
        {
            if (string.IsNullOrEmpty(newWord)) return;
            newWord = newWord.Trim();
            if (_POS.GetPos(newWord, out isReg).Length > 0) return;

            WordPos word = new WordPos(newWord, pos);

            dictFile.WordPosList.Add(word);
            _POS.AddWordPos(word.Word, word.Pos);
        }


        /// <summary>
        /// ���ӵ�����
        /// </summary>
        /// <param name="newWord"></param>
        /// <param name="pos"></param>
        public void AddDict(string newWord)
        {
            if (string.IsNullOrEmpty(newWord)) return;
            newWord = newWord.Trim();
            if (_POS.GetPos(newWord, out isReg).Length > 0) return;
            string[] new1 = newWord.Split(new char[] { '\t' });
            string newword = new1[0];
            int newpos = 0;
            if (new1.Length > 1)
            {
                newpos = int.Parse(new1[1]);
            }
            WordPos word = new WordPos(newword, newpos);

            dictFile.WordPosList.Add(word);
            _POS.AddWordPos(word.Word, word.Pos);
        }

        /// <summary>
        /// ���Ӷ����,ÿһ��һ��
        /// </summary>
        /// <param name="newWords"></param>
        /// <param name="defaultPost">ȱʡ�Ĵ���</param>
        public void AddMulitDict(string newWords, int defaultPos)
        {
            StringReader sr = new StringReader(newWords);
            string word;
            while (!string.IsNullOrEmpty(word = sr.ReadLine()))
                AddDict(word, defaultPos);
            sr.Close();
        }

        /// <summary>
        /// ���ı��ļ����ȡ��һ��һ���ʣ���ָ��ȱʡ����
        /// </summary>
        /// <param name="dictpath"></param>
        /// <param name="defaultPos"></param>
        public void AddDictFromFile(string dictpath, int defaultPos)
        {
            if (!File.Exists(dictpath)) return;
            try
            {
                StreamReader sr = new StreamReader(dictpath);
                string word;
                while (!string.IsNullOrEmpty(word = sr.ReadLine()))
                    AddDict(word, defaultPos);
                sr.Close();

            }
            catch { throw; }
        }

        /// <summary>
        /// ���ı����ȡ��һ��һ���ʺʹ���ֵ
        /// </summary>
        /// <param name="dictPath"></param>
        public void AddDictFromFile(string dictpath)
        {
            if (!File.Exists(dictpath)) return;
            try
            {
                StreamReader sr = new StreamReader(dictpath);
                string word;
                while (!string.IsNullOrEmpty(word = sr.ReadLine()))
                    AddDict(word);
                sr.Close();

            }
            catch { throw; }
        }

        public void SaveDictFromFile(string dictpath)
        {
            Dict dict = new Dict();
            dict.SaveToBinFile(dictpath, dictFile);
        }


        public void SaveDictToTextFile(string dictpath)
        {
            Dict dict = new Dict();
            dict.SaveDictToTextFile(dictpath, dictFile);
        }
    }
}
