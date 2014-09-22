using System;
using System.Collections.Generic;
using System.Threading;
using Jeelu.Billboard;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace Jeelu.KeywordResonator.Client
{
    /// <summary>
    /// 关键词管理类
    /// </summary>
    public class WordsManager
    {
        #region 属性及字段
        // private WorkbenchForm _mainForm;

        public ProgressBarForm ProgressBar { get; set; }

        /// <summary>
        /// 线程数
        /// </summary>
        public int ThreadNum { get; set; }
        /// <summary>
        /// 开始抓取的时间记录
        /// </summary>
        //public DateTime StartCrawlerTime { get; set; }

        private int _getedPage = 0;
        /// <summary>
        /// 已经抓取的网页数
        /// </summary>
        public int GetedPage
        {
            get { return _getedPage; }
            set { _getedPage = value; }
        }
        private int _failedPage = 0;
        /// <summary>
        /// 失败之网页数
        /// </summary>
        public int FailedPage
        {
            get { return _failedPage; }
            set { _failedPage = value; }
        }
        /// <summary>
        /// 总共需要抓取的网页数
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// 此管理器隶属的主窗体
        /// </summary>
        public WorkbenchForm MainForm { get; set; }
        /// <summary>
        /// 词库已有关键词
        /// </summary>
        public System.Collections.Generic.Dictionary<string, ulong> ExistWords { get; set; }
        /// <summary>
        /// 新出现关键词
        /// </summary>
        public Dictionary<string, ulong> NewWords { get; set; }

        /// <summary>
        /// 抓取失败的网页
        /// </summary>
        public List<string> FailedUrls { get; set; }
        /// <summary>
        /// 已经抓取的网页
        /// </summary>
        public List<string> GetedUrls { get; set; }

        #endregion

        #region 构造函数
        public WordsManager(WorkbenchForm mainForm)
        {
            ThreadNum = 4;
            GetedPage = 0;
            TotalPages = 0;
            ProgressBar = null;

            FailedUrls = new List<string>();
            GetedUrls = new List<string>();
            NewWords = new Dictionary<string, ulong>();
            ExistWords = new Dictionary<string, ulong>();
            MainForm = mainForm;
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
            //准备初始数据
            TotalPages = MainForm.UrlManager.Urls.Count;
            GetedPage = 0;
            FailedPage = 0;
            GetedUrls = new List<string>();
            FailedUrls = new List<string>();

            //StartCrawlerTime = DateTime.Now;
            //准备显示进度条
            if (ProgressBar == null)
            {
                ProgressBar = new ProgressBarForm();
                ProgressBar.SetMaxProgress(TotalPages);
                ProgressBar.lableTotalPage.Text = TotalPages.ToString();
            }
            else
            {
                ProgressBar.SetMaxProgress(TotalPages);
                ProgressBar.lableTotalPage.Text = TotalPages.ToString();
                ProgressBar.ResetProgress();
            }
            //多线程抓取并分词
            for (int i = 0; i < ThreadNum; i++)
            {
                ThreadStart startCrawlePage = new ThreadStart(StartCrawlePage);
                Thread crawlePage = new Thread(startCrawlePage);
                crawlePage.Start();
            }
            ProgressBar.ShowDialog(MainForm);//线程创建结束后才能将进度条显示，否则进程被阻在此处       
        }

        /// <summary>
        /// 线程执行方法
        /// </summary>
        void StartCrawlePage()
        {
            // 循环抓取URL
            while (MainForm.UrlManager.Urls.Count > 0)
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
                // 抓取单个网页并分词
                Dictionary<string, ulong> words = null;
                try
                {
                    words = HtmlHelper.SetupSingleUrl(url.Url, false, Service.JWordSegmentor, Service.DbHelper.WebRuleCollections);
                }
                catch (Exception)
                {
                    // 抓取一个网页
                    Interlocked.Increment(ref _failedPage);
                    FailedUrls.Add(url.Url);
                    MainForm.BeginInvoke(new Action(FailCrawle));
                    // 如果已经抓取完毕
                    if ((GetedPage + FailedPage) == TotalPages)
                    {
                        MainForm.BeginInvoke(new Action(PostCrawlerProcess));
                    }
                    continue;
                }

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
                        //foreach (KeyValuePair<string, ulong> word in words)
                        //{
                        //    if (JWordSegmentorService.IsExist(word.Key, ""))
                        //    {
                        //        if (ExistWords.ContainsKey(word.Key))
                        //        {
                        //            ExistWords[word.Key] = ExistWords[word.Key] + word.Value;
                        //        }
                        //        else
                        //        {
                        //            ExistWords.Add(word.Key, word.Value);
                        //        }
                        //        continue;
                        //    }
                        //    if (NewWords.ContainsKey(word.Key))
                        //    {
                        //        NewWords[word.Key] = NewWords[word.Key] + word.Value;
                        //    }
                        //    else
                        //    {
                        //        NewWords.Add(word.Key, word.Value);
                        //    }
                        //}
                    }
                }
                // 抓取一个网页
                Interlocked.Increment(ref _getedPage);
                GetedUrls.Add(url.Url);
                MainForm.BeginInvoke(new Action(SucceedCrawle));
                // 如果已经抓取完毕
                if ((GetedPage + FailedPage) == TotalPages)
                {
                    MainForm.BeginInvoke(new Action(PostCrawlerProcess));
                }
            }//while
        }

        /// <summary>
        /// 更新进度条
        /// </summary>
        private void SucceedCrawle()
        {
            ProgressBar.SucceedCrawle();
        }

        /// <summary>
        /// 抓取失败时处理
        /// </summary>
        void FailCrawle()
        {
            ProgressBar.FailCrawle();
        }

        /// <summary>
        /// 抓取结果后调用,不管是否成功抓取
        /// </summary>
        private void PostCrawlerProcess()
        {
            ProgressBar.Close();
            OutputUrlResult();
            MergeExistWords();
            ShowNewWordsForm();
        }

        private void OutputUrlResult()
        {
            StreamWriter sw = new StreamWriter("urlResult.txt");
            foreach (var url in FailedUrls)
            {
                sw.WriteLine(url+":失败");
            }
            foreach (var url in GetedUrls)
            {
                sw.WriteLine(url + ":成功");
            }
            sw.Close();
        }

        /// <summary>
        /// 显示管理界面
        /// </summary>
        public void ShowNewWordsForm()
        {
            if (NewWords == null || NewWords.Count == 0)
            {
                MessageService.Show("新关键词为空");
                return;
            }
            if (MainForm.NewWordsView != null)
            {
                MainForm.NewWordsView.Close();
            }
            MainForm.NewWordsView = new KeywordListView(this, KeywordFormType.NewWord);
            MainForm.NewWordsView.ContextMenuStrip = MyContextMenuStrip.CreateForListCheckBox(MainForm.NewWordsView);
            MainForm.NewWordsView.Text = "KeywordListView";
            MainForm.NewWordsView.Show(MainForm.MainDockPanel, DockState.Document);
        }

        /// <summary>
        /// 显示已经关键词界面
        /// </summary>
        public void ShowExistWordsForm()
        {
            if (ExistWords == null || ExistWords.Count == 0)
            {
                MessageService.Show("关键词为空");
                return;
            }
            if (MainForm.ExistWordsView != null)
            {
                MainForm.ExistWordsView.Close();
            }
            MainForm.ExistWordsView = new KeywordListView(this, KeywordFormType.ExistWord);
            MainForm.ExistWordsView.ContextMenuStrip = MyContextMenuStrip.CreateForListCheckBox(MainForm.ExistWordsView);
            MainForm.ExistWordsView.Text = "KeywordListView";
            MainForm.ExistWordsView.Show(MainForm.MainDockPanel, DockState.Document);

        }

        /// <summary>
        /// 合并词库中已有词汇
        /// </summary>
        public void MergeExistWords()
        {
            Dictionary<string, ulong> existNewwords = new Dictionary<string, ulong>();
            // 将词库中已有词库记录并在新词中删除
            foreach (KeyValuePair<string, ulong> word in NewWords)
            {
                if (JWordSegmentorService.IsExist(word.Key))
                {
                    existNewwords.Add(word.Key, JWordSegmentorService.GetFrequency(word.Key) + word.Value);
                }
            }
            foreach (KeyValuePair<string, ulong> word in existNewwords)
            {
                if (NewWords.ContainsKey(word.Key))
                {
                    NewWords.Remove(word.Key);
                }
            }
            // 将已存词存入已在词库属性中
            foreach (var word in existNewwords)
            {
                if (ExistWords.ContainsKey(word.Key))
                {
                    ExistWords[word.Key] += word.Value;
                }
                else
                {
                    ExistWords.Add(word.Key,word.Value);
                }
            }
        }

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

        /// <summary>
        /// 判断关键词是否存在于新词和词库中
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool IsWordExist(string word)
        {
            return NewWords.ContainsKey(word) || ExistWords.ContainsKey(word) || JWordSegmentorService.IsExist(word, "");
        }
        #endregion

        /// <summary>
        /// 更新本地词库
        /// </summary>
        internal void UpdateLocalWord()
        {
            MergeExistWords();
            ShowNewWordsForm();
            ShowExistWordsForm();

        }
    }
}
