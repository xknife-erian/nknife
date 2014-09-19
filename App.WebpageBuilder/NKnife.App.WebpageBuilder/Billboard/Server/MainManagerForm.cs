/*
 * 1. 获取新URL列表   获取旧URL列表
 * 2. 获取URL对应的页面的源代码（抓取）
 * 3. 将源代码进行抽取正文
 * 4. 衡量正文关键词
 * 5. 用正文关键词与广告关键词进行匹配
 * 6. 结果写入文件
 * 
 * A. 广告库载入，转置为以关键词为Key的字典Hash（考虑每小时替换）
 * B. 关键词库载入，注意：a.量大；b.实时
 * C. 获取旧URL列表要考虑哪些旧URL需要更新
*/
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Collections;
using System.Data;

namespace Jeelu.Billboard.Server
{
    /// <summary>
    /// 主窗体，服务由此启动
    /// 初步构建 by lukan, 2008-7-8 02:34:12
    /// </summary>
    public partial class MainManagerForm : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MainManagerForm()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                ResourcesReader.SetControlPropertyHelper(this);
            }
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version: {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
        }

        /// <summary>
        /// 状态描述字符串
        /// </summary>
        public string StatusString
        {
            get { return this._StatusLabel.Text; }
            set
            {
                this._StatusLabel.Text = value;
                this.Update();
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// 以广告关键词为Key，广告ID数组为Value的字典
        /// </summary>
        public Dictionary<string, string[]> AdDatabaseDic { get; private set; }
        private AdHelper _AdHelper = new AdHelper();
        private Thread thread;
        /// <summary>
        /// 当窗体完全显示出来后，开始执行Server程序。
        /// </summary>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.InitOption();///初始化选项
            
            //启动一个线程，监听关键词客户端请求
            ServerCore serverCore = new ServerCore();
            thread = new Thread(new ThreadStart(serverCore.Listen));
            thread.IsBackground = true;
            thread.Start();

            this.Text = AssemblyProduct + ", Application Start time: " + DateTime.Now.ToShortDateString() + " | " + DateTime.Now.ToLongTimeString();
            this.StatusString = "初始化关键词词库...";
            InitializeApplication.InitializeWords();
            this.StatusString = "初始化广告数据...";
            _AdHelper.Initialize();
            this.StatusString = "Server程序启动就绪";

            //this.Start();/// Server程序启动
        }

        /// <summary>
        /// Server程序启动
        /// </summary>
        private void Start()
        {
            this.StatusString = "开始抓取";
            ///读入一批URL列表
            WebPage[] _CrawlUrls = Service.DbHelper.CrawlUrls;

            foreach (WebPage url in _CrawlUrls)
            {
                ///从URL链接获取该URL的HtmlCode
                Dictionary<string, ulong> keywords = HtmlHelper.SetupSingleUrl(
                                                                    url.Url, 
                                                                    true, 
                                                                    Service.JWordSegmentor,
                                                                    Service.DbHelper.WebRuleCollections);

                ///根据关键词匹配广告
                long[] adIds = KwHelper.Match(keywords, _AdHelper.AdInvertedIndex);

                ///建立广告位(页面)与广告的相关联的关系文件
                Service.FileService.CreateBillboardRelation(adIds, url.Url);
            }
        }

        #region Get Option

        private int timeoutMaxCount = 5;///最大尝试连接数
        private int connectSleepTime = 1000;///重试连接间隔时间

        /// <summary>
        /// 初始化选项
        /// </summary>
        private void InitOption()
        {
            if (string.IsNullOrEmpty(Service.Option.GetValue("TimeoutMaxCount")))
            {
                Service.Option.SetValue("TimeoutMaxCount", timeoutMaxCount.ToString());
            }
            else
            {
                timeoutMaxCount = int.Parse(Service.Option.GetValue("TimeoutMaxCount"));
            }
            if (string.IsNullOrEmpty(Service.Option.GetValue("ConnectSleepTime")))
            {
                Service.Option.SetValue("ConnectSleepTime", connectSleepTime.ToString());
            }
            else
            {
                connectSleepTime = int.Parse(Service.Option.GetValue("ConnectSleepTime"));
            }
        }

        #endregion

        #region ResourceGet

        public string GetText(string key)
        {
            return ResourcesReader.GetText(key, this);
        }

        public Icon GetIcon(string key)
        {
            return ResourcesReader.GetIcon(key, this);
        }

        public Image GetImage(string key)
        {
            return ResourcesReader.GetImage(key, this);
        }

        public Cursor GetCursor(string key)
        {
            return ResourcesReader.GetCursor(key, this);
        }

        public byte[] GetBytes(string key)
        {
            return ResourcesReader.GetBytes(key, this);
        }

        #endregion

        #region 程序集属性访问器

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void buttonFetch_Click(object sender, EventArgs e)
        {
            this.Start();
        }

        private void buttonIncAd_Click(object sender, EventArgs e)
        {
            this.StatusString = "开始更新广告库";
            _AdHelper.Increase();
            this.StatusString = "广告库更新完毕";
        }

        private void buttonInitAd_Click(object sender, EventArgs e)
        {
            this.StatusString = "开始加载广告库";
            _AdHelper.Initialize();
            this.StatusString = "广告库加载完毕";
        }
    }
}
