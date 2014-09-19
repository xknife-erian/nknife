using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Jeelu.WordSegmentor;

namespace Jeelu.WordSegmentor
{
    public class SearchWordResult : IComparable
    {
        /// <summary>
        /// 单词
        /// </summary>
        public T_DictStruct Word;

        /// <summary>
        /// 相似度
        /// </summary>
        public float SimilarRatio;

        public override string ToString()
        {
            return Word.Word;
        }

        #region IComparable 成员

        public int CompareTo(object obj)
        {
            SearchWordResult dest = (SearchWordResult)obj;

            if (this.SimilarRatio == dest.SimilarRatio)
            {
                return 0;
            }
            else if (this.SimilarRatio > dest.SimilarRatio)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        #endregion
    }

    /// <summary>
    /// 字典管理
    /// 包括插入，修改，删除，搜索
    /// </summary>
    public class DictMgr
    {
        T_DictFile _Dict = null;
        bool _Approximate = false;
        Hashtable _DictTbl = new Hashtable();

        /// <summary>
        /// 字典
        /// </summary>
        public virtual T_DictFile Dict
        {
            get
            {
                return _Dict;
            }

            set
            {
                _Dict = value;

                foreach (T_DictStruct w in _Dict.Dicts)
                {
                    _DictTbl[w.Word] = w;
                }
            }
        }

        /// <summary>
        /// 是否允许模糊查询
        /// </summary>
        public virtual bool Approximate
        {
            get
            {
                return _Approximate;
            }
            set
            {
                _Approximate = value;
            }
        }

        /// <summary>
        /// 通过遍历方式搜索
        /// </summary>
        /// <returns></returns>
        private List<SearchWordResult> SearchByTraversal(String key)
        {
            Debug.Assert(_Dict != null);

            List<SearchWordResult> result = new List<SearchWordResult>();

            foreach (T_DictStruct word in _Dict.Dicts)
            {
                if (word.Word.Contains(key))
                {
                    SearchWordResult wordResult = new SearchWordResult();
                    wordResult.Word = word;
                    wordResult.SimilarRatio = (float)key.Length / (float)word.Word.Length;
                    result.Add(wordResult);
                }
            }

            return result;
        }

        private List<SearchWordResult> SearchByLucene(String key)
        {
            return null;
        }

        public virtual List<SearchWordResult> Search(String key)
        {
            if (Approximate)
            {
                return SearchByLucene(key);
            }
            else
            {
                return SearchByTraversal(key);
            }
        }

        public virtual T_DictStruct GetWord(String word)
        {
            return (T_DictStruct)_DictTbl[word];
        }

        public virtual void InsertWord(String word, double frequency, int pos)
        {
            word = word.Trim();

            if (GetWord(word) != null)
            {
                return;
            }

            T_DictStruct w = new T_DictStruct();
            w.Word = word;
            w.Frequency = frequency;
            w.Pos = pos;

            _Dict.Dicts.Add(w);
            _DictTbl[word] = w;
        }

        public virtual void UpdateWord(String word, double frequency, int pos)
        {
            word = word.Trim();

            T_DictStruct w = GetWord(word) ;

            if (w == null)
            {
                return;
            }
           
            w.Frequency = frequency;
            w.Pos = pos;
        }

        public virtual void DeleteWord(String word)
        {
            word = word.Trim();

            T_DictStruct w = GetWord(word);

            if (w == null)
            {
                return;
            }

            _DictTbl.Remove(w.Word);
            _Dict.Dicts.Remove(w);
        }
    }

}
