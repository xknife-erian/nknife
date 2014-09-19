using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using Jeelu.WordSegmentor;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading;
using Jeelu.Billboard;

namespace Jeelu.KeywordResonator.Client
{
    /// <summary>
    /// 关键词管理类
    /// </summary>
    class WordsManager
    {
        #region 属性及字段
        private WorkbenchForm _mainForm;
        private int _threadNum = 4;
        private int _getedPage = 0;
        private DateTime _startCrawlerTime;
        private int _totalPages = 0;
        ProgressBarForm progressBar = null;

        /// <summary>
        /// 线程数
        /// </summary>
        public int ThreadNum
        {
            get { return _threadNum; }
            set { _threadNum = value; }
        }

        public DateTime StartCrawlerTime
        {
            get { return _startCrawlerTime; }
            set { _startCrawlerTime = value; }
        }

        public int GetedPage
        {
            
            get { return _getedPage; }
            set { _getedPage = value; }
        }


        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }


        internal WorkbenchForm MainForm
        {
            get { return _mainForm; }
            set { _mainForm = value; }
        }

        /// <summary>
        /// 词库已有关键词
        /// </summary>
        public Dictionary<string, ulong> ExistWords { get; set; }

        /// <summary>
        /// 新出现关键词
        /// </summary>
        public Dictionary<string, ulong> NewWords { get; set; }

        #endregion

        #region 构造函数
        public WordsManager(WorkbenchForm mainForm)
        {
            NewWords = new Dictionary<string, ulong>();
            ExistWords = new Dictionary<string, ulong>();
            _mainForm = mainForm;
        }
        #endregion

        #region 公共方法

        /// <summary>
        /// 管理关键词
        /// </summary>
        public void ManageWords()
        {
            FetchNewWords();          
        }

        /// <summary>
        /// 获取新的词列表
        /// </summary>
        public void FetchNewWords()
        {
            // 获取URL
            MainForm.UrlManager.GetUrls();
            _totalPages = MainForm.UrlManager.Urls.Count;
            _getedPage = 0;
            StartCrawlerTime = DateTime.Now;
            //准备显示进度条
            if (progressBar == null)
            {
                progressBar = new ProgressBarForm();
                progressBar.SetMaxProgress(_totalPages);
                progressBar.LableTotalPage.Text = TotalPages.ToString();
            }
            else
            {
                progressBar.SetMaxProgress(_totalPages);
                progressBar.LableTotalPage.Text = TotalPages.ToString();
                progressBar.ResetProgress();
            }
            //多线程抓取并分词
            for (int i = 0; i < _threadNum; i++)
            {
                ThreadStart startCrawlePage = new ThreadStart(StartCrawlePage);
                Thread crawlePage = new Thread(startCrawlePage);
                crawlePage.Start();
            }        
            progressBar.ShowDialog(MainForm);//线程创建结束后才能将进度条显示，否则进程被阻在此处       
        }

        /// <summary>
        /// 线程执行方法
        /// </summary>
        void StartCrawlePage()
        {
            // 循环抓取URL
            while (MainForm.UrlManager.Urls.Count>0)
            {
                UrlItem url;
                lock (MainForm.UrlManager.Urls)
                {
                    if (MainForm.UrlManager.Urls != null && MainForm.UrlManager.Urls.Count > 0)
                    {
                        url = MainForm.UrlManager.Urls[0];
                        MainForm.UrlManager.Urls.Remove(url);
                    }
                    else
                    {
                        return;
                    }
                }
                WordSeg seg = Service.WordSegment;
                // 抓取单个网页并分词
                Dictionary<string, ulong> words = HtmlContent.SetupSingleUrl(url.Url, false, seg);
                // 合并所有线程抓回的单词
                if (words != null && words.Count > 0)
                {
                    lock (NewWords)
                    {
                        foreach (KeyValuePair<string, ulong> word in words)
                        {
                            if (NewWords.ContainsKey(word.Key))
                            {
                                NewWords[word.Key] = NewWords[word.Key] + word.Value;
                            }
                            else
                            {
                                NewWords.Add(word.Key, word.Value);
                            }
                        }
                    }
                }
                // 抓取一个网页
                Interlocked.Increment(ref _getedPage);
                _mainForm.BeginInvoke(new Action(UpdateProgress));
                // 如果已经抓取完毕
                if (_getedPage == _totalPages)
                {
                    _mainForm.BeginInvoke(new Action(PostCrawlerProcess));
                }
            }//while
        }

        private void PostCrawlerProcess()
        {
            progressBar.Close();
            MergeExistWords();
            ShowNewWordsForm();
        }
        
        /// <summary>
        /// 显示管理界面
        /// </summary>
        public void ShowNewWordsForm()
        {
            if (NewWords == null||NewWords.Count == 0)
            {
                MessageService.Show("新关键词为空");
                return;
            }
            if (MainForm.NewWordsView!=null)
            {
                MainForm.NewWordsView.Close();
            }
            MainForm.NewWordsView = new KeywordListView(this, KeywordFormType.NewWord);
            MainForm.NewWordsView.ContextMenuStrip = MyContextMenuStrip.CreateForListCheckBox(MainForm.NewWordsView);
            MainForm.NewWordsView.Text = "KeywordListView";
            MainForm.NewWordsView.Show(_mainForm.MainDockPanel, DockState.Document);
        }

        /// <summary>
        /// 显示已经关键词界面
        /// </summary>
        public void ShowExistWordsForm()
        {
            if (ExistWords == null || ExistWords.Count == 0)
            {
                //MessageService.Show("关键词为空");
                //return;
            }
            if (MainForm.ExistWordsView != null)
            {
                MainForm.ExistWordsView.Close();
            }
            MainForm.ExistWordsView = new KeywordListView(this,KeywordFormType.ExistWord);
            MainForm.ExistWordsView.ContextMenuStrip = MyContextMenuStrip.CreateForListCheckBox(MainForm.ExistWordsView);
            MainForm.ExistWordsView.Text = "KeywordListView";
            MainForm.ExistWordsView.Show(_mainForm.MainDockPanel, DockState.Document);

        }

        /// <summary>
        /// 更新进度条
        /// </summary>
        private void UpdateProgress()
        {
            progressBar.IncreaseValue();
        }

        /// <summary>
        /// 合并词库中已有词汇
        /// </summary>
        public void MergeExistWords()
        {
            foreach (KeyValuePair<string, ulong> word in NewWords)
            {
                //if (Service.DictService.IsExist(word.Key, ""))
                //{
                //    ExistWords.Add(word.Key, Service.DictService.GetFrequency(word.Key, "") + word.Value);
                //}
            }
            foreach (KeyValuePair<string, ulong> word in ExistWords)
            {
                if (NewWords.ContainsKey(word.Key))
                {
                    NewWords.Remove(word.Key);
                }
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取URL列表
        /// </summary>
        /// <param name="urlsFile"></param>
        /// <returns></returns>
        private string[] GetUrls(string urlsFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(urlsFile);
            XmlNodeList nodes = doc.SelectNodes(@"//url");
            string[] urls = new string[nodes.Count];
            int i = 0;
            foreach (XmlNode node in nodes)
            {
                urls[i] = node.Value;
                i++;
            }
            return urls;
        }
        #endregion

        /// <summary>
        /// 返回更新字符串
        /// </summary>
        public string GetUpdateCmd()
        {
            WordSqlXml sql = Service.WordSqlXml;
            foreach (var item in this.NewWords)
            {
                WordSqlXmlAction action = sql.CreatAction(DictAction.Add, item.Key, item.Value, "");
                sql.Add(action);
            }
            foreach (var item in this.ExistWords)
            {
                WordSqlXmlAction action = sql.CreatAction(DictAction.UpdataFrequency, item.Key, item.Value, "");
                sql.Add(action);
            }
            return sql.ToString();
        }

        public bool IsWordExist(string word)
        {
            return NewWords.ContainsKey(word) || ExistWords.ContainsKey(word) || JWordSegmentorService.IsExist(word, "");
        }     
    }
}
