using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Xml;
using System.Reflection;
using System.IO;
using System.Windows.Forms;

namespace Jeelu.SimplusD
{
    public partial class TmpltXmlDocument : IndexXmlDocument
    {
        /// 2008-7-1 10:04:34 再次小规模整理，近于重构

        #region IToHtml 成员

        /// <summary>
        /// 是否已经生成过XHtml文件
        /// </summary>
        private bool _isAlreadyToHtml = false;
        /// <summary>
        /// 通过SaveXhtml方法传递进来的ToHtmlHelper对象
        /// </summary>
        private ToHtmlHelper ToHtmlHelper { get; set; }

        /// <summary>
        /// 模板的子文件的路径，路径名为ID，供Snip生成时使用, 绝对路径
        /// </summary>
        internal string TmpltToHtmlPath
        {
            get { return Path.Combine(this.ToHtmlHelper.TmpltPath, this.Id + @"\"); }
        }
        /// <summary>
        /// 模板的子文件的URL，路径名为ID，供Snip生成时使用, 相对路径
        /// </summary>
        internal string TmpltToHtmlUrl
        {
            get { return this.ToHtmlHelper.TmpltRelativeUrl + this.Id + @"/"; }
        }

        /// <summary>
        /// 获取当前模板生成的Xhtml代码字符串
        /// </summary>
        public virtual string ToHtml()
        {
            if (!this._isAlreadyToHtml)
            {
                this.MarkXhtmlElement();
            }
            return this._ParentXhtmlElement.ToString();
        }

        /// <summary>
        /// 获取当前模板生成的供预览使用的Xhtml代码字符串
        /// </summary>
        public virtual string ToHtmlPreview()
        {
            return this._XhtmlElement.ToString();
        }

        /// <summary>
        /// 保存当前模板生成的CSS代码为一个文件
        /// </summary>
        public virtual bool SaveCss(ToHtmlHelper htmlHelper)
        {
            this.ToHtmlHelper = htmlHelper;

            #region 临时效果

            string str = this.Id + "\r\n" + this.Title + "\r\nTmpltXmlDocument.SaveCss() Test!\r\n" + DateTime.Now.ToString(Utility.Const.TimeFormat);

            Directory.CreateDirectory(Path.GetDirectoryName(this.ToHtmlHelper.CSSPath));///创建目录

            string file = Path.Combine(this.ToHtmlHelper.CSSPath, this.Id + Utility.Const.CssFileExt);
            FileStream fs = File.Open(file, FileMode.Create, FileAccess.Write);
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            fs.Write(buffer, 0, buffer.Length);
            fs.Close();
            fs.Dispose();

            string fileUser = Path.Combine(this.ToHtmlHelper.CSSPath, this.Id + "_custom" + Utility.Const.CssFileExt);
            FileStream fsUser = File.Open(fileUser, FileMode.Create, FileAccess.Write);
            byte[] bufferUser = Encoding.UTF8.GetBytes(str + "\r\n\r\nThis is User custom css!");
            fsUser.Write(bufferUser, 0, bufferUser.Length);
            fsUser.Close();
            fsUser.Dispose();

            return true;

            #endregion
        }

        /// <summary>
        /// 保存当前模板生成的Xhtml代码为一个文件
        /// </summary>
        public virtual bool SaveXhtml(ToHtmlHelper htmlHelper)
        {
            this.ToHtmlHelper = htmlHelper;
            this.SaveCss(htmlHelper);
            return ToHtmlHelper.FileSave(Path.Combine(this.ToHtmlHelper.TmpltPath, this.Id + Utility.Const.ShtmlFileExt), this.ToHtml());
        }

        public virtual bool DeleteXhtml(ToHtmlHelper htmlhelper)
        {
            this.ToHtmlHelper = htmlhelper;
            File.Delete(this.HtmlFile);
            Directory.Delete(this.TmpltToHtmlPath, true);
            return true;
        }

        /// <summary>
        /// 清理可能已不存在的Snip对应的生成文件,一般在模板的isModify为真时调用
        /// </summary>
        public virtual bool ClearSnipXhtml(ToHtmlHelper htmlhelper)
        {
            return true;
        }

        /// <summary>
        /// 该模板的Xhtml代码的父模板。
        /// 对于模板来父级就页面本身，故初始时即为实例一个XhtmlPage。
        /// </summary>
        public XhtmlElement ParentXhtmlElement
        {
            get { return this._ParentXhtmlElement; }
            set { this._ParentXhtmlElement = value; }
        }
        private XhtmlElement _ParentXhtmlElement;

        /// <summary>
        /// 该模板的Xhtml代码。
        /// 对于模板来讲他的XhtmlElement在用的时候一般就是Body节点。
        /// </summary>
        public XhtmlElement XhtmlElement
        {
            get { return this._XhtmlElement; }
            set { this._XhtmlElement = value; }
        }
        private XhtmlElement _XhtmlElement;

        /// <summary>
        /// 模板生成的文件，包含绝对路径，和文件名。
        /// 文件名的组成：主体是模板的ID，后缀名“.shtml”。
        /// 目录间隔采用的是“\”
        /// </summary>
        public string HtmlFile
        {
            get
            {
                if (string.IsNullOrEmpty(this._HtmlFile))
                {
                    _HtmlFile = Path.Combine(this.ToHtmlHelper.TmpltPath, this.Id + Utility.Const.ShtmlFileExt);
                }
                return _HtmlFile;
            }
        }
        private string _HtmlFile;


        /// <summary>
        /// 根据当前模板的特性来设置该节点
        /// </summary>
        protected virtual void MarkXhtmlElement()
        {
            XhtmlPage ownerPage = (XhtmlPage)this._ParentXhtmlElement.OwnerPage;

            //<meta http-equiv="Expires" content="Fri, 26 Mar 1999 23:59:59 GMT">
            //<meta http-equiv="pragma" content="no-cache">
            //<meta name="Author" content="Apple Inc.">
            //<meta name="Keywords" content="Apple">

            //<link rel="home" href="http://www.apple.com/">
            //<link rel="alternate" type="application/rss+xml" title="RSS" href="http://images.apple.com/main/rss/hotnews/hotnews.rss">
            //<link rel="index" href="http://www.apple.com/find/sitemap.html">


            #region Meta: Content-Type

            XhtmlAtts.Http_equiv httpEquiv = new XhtmlAtts.Http_equiv(Xhtml.Http_equiv.content_type);
            XhtmlAtts.Content content = new XhtmlAtts.Content("text/html; charset=utf-8");
            XhtmlTagElement contentType = ownerPage.CreateXhtmlMeta(httpEquiv, content);
            ownerPage.Head.AppendChild(contentType);

            #endregion

            //<!--#include virtual="http://ssi.jeelu.com/GetHtml.cgi?domain=$SERVER_ADDR&dir=$DOCUMENT_URI&filename=asdfads"-->
            SdCgiObject cgi = new SdCgiObject(CgiPlace.Head);
            ownerPage.Head.AppendChild(ownerPage.CreateXhtmlCommentShtml(cgi));

            #region Meta: Power
#if !DEBUG
            string version = Application.ProductVersion;
            XhtmlAtts.Name name = new XhtmlAtts.Name("Generator");
            content = new XhtmlAtts.Content("网站360°: v" + version + ",http://www.SimplusD.com");
            XhtmlTagElement power = ownerPage.CreateXhtmlMeta(name, content);
            ownerPage.Head.AppendChild(power);
#endif
            #endregion

            XhtmlElement mainElement = ownerPage.CreateXhtmlDiv();
            this._XhtmlElement.AppendChild(mainElement);

            #region 辩别生成效果
#if DEBUG
            mainElement.SetAttribute("name", "tmplt");
#endif
            #endregion

            /// this.GetRectsElement().ChildNodes[0]是指的rects节点下的第一个节点,一般是rect
            /// 按照SimplusD的规则，这个节点有仅有一个rect节点
            XmlNode firstNode = null;
            if (this.GetRectsElement().ChildNodes[0] is SnipXmlElement)
            {
                firstNode = this.GetRectsElement();///当一个模板里只有一个页面片时的特殊情况处理
            }
            else
            {
                firstNode = this.GetRectsElement().ChildNodes[0];
            }

            foreach (XmlNode node in firstNode.ChildNodes)///遍历子节点，通过Element的继承关系递归
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                if (node is ToHtmlXmlElement)///当节点是继承自ToHtmlXmlElement，即可以生成页面
                {
                    ToHtmlXmlElement htmlNode = (ToHtmlXmlElement)node;
                    htmlNode.ParentXhtmlElement = mainElement;///下一级节点的父节点即为本节点
                    htmlNode.ToHtml();///调用下一级节点的ToHtml
                }
            }
            this._isAlreadyToHtml = true;///已经ToHtml的标记
        }

        #endregion

    }
}



/*
 
HTML页面HEAD信息介绍

　　META标签是HTML语言HEAD区的一个辅助性标签，它位于HTML文档头部的<HEAD>标记和<TITLE> 标记之间，它提供用户不可见的信息。meta标签通常用来为搜索引擎robots定义页面主题，或者是定义用户浏览器上的cookie；它可以用于鉴别作者，设定页面格式，标注内容提要和关键字；还可以设置页面使其可以根据你定义的时间间隔刷新自己,以及设置RASC内容等级，等等。

    下面介绍一些有关 标记的例子及解释。

　　META标签分两大部分：HTTP标题信息(HTTP-EQUIV)和页面描述信息(NAME)。

       ※ HTTP-EQUIV

　　HTTP-EQUIV类似于HTTP的头部协议，它回应给浏览器一些有用的信息，以帮助正确和精确地显示网页内容。常用的HTTP-EQUIV类型有：


　　1、Content-Type和Content-Language (显示字符集的设定)

　　说明：设定页面使用的字符集，用以说明主页制作所使用的文字已经语言，浏览器会根据此来调用相应的字符集显示page内容。

　　用法：<Meta http-equiv="Content-Type" Content="text/html; Charset=gb2312">
　　　　　　<Meta http-equiv="Content-Language" Content="zh-CN">

　　注意：　该META标签定义了HTML页面所使用的字符集为GB2132，就是国标汉字码。如果将其中的“charset=GB2312”替换成“BIG5”，则该页面所用的字符集就是繁体中文Big5码。当你浏览一些国外的站点时，IE浏览器会提示你要正确显示该页面需要下载xx语支持。这个功能就是通过读取HTML页面META标签的Content-Type属性而得知需要使用哪种字符集显示该页面的。如果系统里没有装相应的字符集，则IE 就提示下载。其他的语言也对应不同的charset，比如日文的字符集是“iso-2022-jp ”，韩文的是“ks_c_5601”。
　　　　　　
　　Content-Type的Content还可以是：text/xml等文档类型；
　　Charset选项：ISO-8859-1(英文)、BIG5、UTF-8、SHIFT-Jis、Euc、Koi8-2、us-ascii, x-mac-roman, iso-8859-2, x-mac-ce, iso-2022-jp, x-sjis, x-euc-jp,euc-kr, iso-2022-kr, gb2312, gb_2312-80, x-euc-tw, x-cns11643-1,x-cns11643-2等字符集；Content-Language的Content还可以是：EN、FR等语言代码。

　　2、Refresh (刷新)

　　　说明：让网页多长时间（秒）刷新自己，或在多长时间后让网页自动链接到其它网页。
　　　用法：<Meta http-equiv="Refresh" Content="30">
　　　　　　<Meta http-equiv="Refresh" Content="5; Url=http://www.xia8.net">
　　　注意：其中的5是指停留5秒钟后自动刷新到URL网址。

　　3、Expires (期限)

　　　说明：指定网页在缓存中的过期时间，一旦网页过期，必须到服务器上重新调阅。
　　　用法：<Meta http-equiv="Expires" Content="0">
　　　　　　<Meta http-equiv="Expires" Content="Wed, 26 Feb 1997 08:21:57 GMT">
　　　注意：必须使用GMT的时间格式，或直接设为0(数字表示多少时间后过期)。

　　4、Pragma (cach模式)

　　　说明：禁止浏览器从本地机的缓存中调阅页面内容。
　　　用法：<Meta http-equiv="Pragma" Content="No-cach">
　　　注意：网页不保存在缓存中，每次访问都刷新页面。这样设定，访问者将无法脱机浏览。

　　5、Set-Cookie (cookie设定)

　　说明：浏览器访问某个页面时会将它存在缓存中，下次再次访问时就可从缓存中读取，以提高速度。当你希望访问者每次都刷新你广告的图标，或每次都刷新你的计数器，就要禁用缓存了。通常HTML文件没有必要禁用缓存，对于ASP等页面，就可以使用禁用缓存，因为每次看到的页面都是在服务器动态生成的，缓存就失去意义。如果网页过期，那么存盘的cookie将被删除。
　　　用法：<Meta http-equiv="Set-Cookie" Content="cookievalue=xxx; expires=Wednesday,
　　　　　　 21-Oct-98 16:14:21 GMT; path=/">
　　　注意：必须使用GMT的时间格式。

　　6、Window-target (显示窗口的设定)

　　　说明：强制页面在当前窗口以独立页面显示。
　　　用法：<Meta http-equiv="Widow-target" Content="_top">
　　　注意：这个属性是用来防止别人在框架里调用你的页面。Content选项：_blank、_top、_self、_parent。

　　7、Pics-label (网页RSAC等级评定)
　　　说明：在IE的Internet选项中有一项内容设置，可以防止浏览一些受限制的网站，而网站的限制级
　　　　　　别就是通过该参数来设置的。
　　　用法：<META http-equiv="Pics-label" Contect=
　　　　　　　　　　　　　　　"(PICS－1.1'http://www.rsac.org/ratingsv01.html'
　　　　　　 I gen comment 'RSACi North America Sever' by 'inet@microsoft.com'
　　　　　　　for 'http://www.microsoft.com' on '1997.06.30T14:21－0500' r(n0 s0 v0 l0))">

　　　注意：不要将级别设置的太高。RSAC的评估系统提供了一种用来评价Web站点内容的标准。用户可以设置Microsoft Internet Explorer（IE3.0以上）来排除包含有色情和暴力内容的站点。上面这个例子中的HTML取自Microsoft的主页。代码中的（n 0 s 0 v 0 l 0）表示该站点不包含不健康内容。级别的评定是由RSAC，即美国娱乐委员会的评级机构评定的，如果你想进一步了解RSAC评估系统的等级内容，或者你需要评价自己的网站，可以访问RSAC的站点：http://www.rsac.org/。

　　8、Page-Enter、Page-Exit (进入与退出)

　　　说明：这个是页面被载入和调出时的一些特效。
　　　用法：<Meta http-equiv="Page-Enter" Content="blendTrans(Duration=0.5)">
　　　　　　<Meta http-equiv="Page-Exit" Content="blendTrans(Duration=0.5)">
　　　注意：blendTrans是动态滤镜的一种，产生渐隐效果。另一种动态滤镜RevealTrans也可以用于页面进入与退出效果:

　　　　　　<Meta http-equiv="Page-Enter" Content="revealTrans(duration=x, transition=y)">
　　　　　　<Meta http-equiv="Page-Exit" Content="revealTrans(duration=x, transition=y)">

　　　　　　　Duration　　表示滤镜特效的持续时间(单位：秒)
　　　　　　　Transition　滤镜类型。表示使用哪种特效，取值为0-23。

　　　　　　　0 矩形缩小
　　　　　　　1 矩形扩大
　　　　　　　2 圆形缩小
　　　　　　　3 圆形扩大
　　　　　　　4 下到上刷新
　　　　　　　5 上到下刷新
　　　　　　　6 左到右刷新
　　　　　　　7 右到左刷新
　　　　　　　8 竖百叶窗
　　　　　　　9 横百叶窗
　　　　　　 10 错位横百叶窗
　　　　　　 11 错位竖百叶窗
　　　　　　 12 点扩散
　　　　　　 13 左右到中间刷新
　　　　　　 14 中间到左右刷新
　　　　　　 15 中间到上下
　　　　　　 16 上下到中间
　　　　　　 17 右下到左上
　　　　　　 18 右上到左下
　　　　　　 19 左上到右下
　　　　　　 20 左下到右上
　　　　　　 21 横条
　　　　　　 22 竖条
　　　　　　 23 以上22种随机选择一种

　　9、MSThemeCompatible (XP主题)
　　　说明：是否在IE中关闭 xp 的主题
　　　用法：<Meta http-equiv="MSThemeCompatible" Content="Yes">
　　　注意：关闭 xp 的蓝色立体按钮系统显示样式，从而和win2k 很象。

　　10、IE6 (页面生成器)
　　　说明：页面生成器generator，是ie6
　　　用法：<Meta http-equiv="IE6" Content="Generator">
　　　注意：用什么东西做的，类似商品出厂厂商。

　　11、Content-Script-Type (脚本相关)
　　　说明：这是近来W3C的规范，指明页面中脚本的类型。
　　　用法：<Meta http-equiv="Content-Script-Type" Content="text/javascript">
　　　注意：

       ※NAME变量

　　name是描述网页的，对应于Content（网页内容），以便于搜索引擎机器人查找、分类（目前几乎所有的搜索引擎都使用网上机器人自动查找meta值来给网页分类）。
　　name的value值（name=""）指定所提供信息的类型。有些值是已经定义好的。例如description(说明)、keyword(关键字)、refresh(刷新)等。还可以指定其他任意值，如：creationdate(创建日期) 、
document ID(文档编号)和level(等级)等。
　　name的content指定实际内容。如：如果指定level(等级)为value(值)，则Content可能是beginner(初级)、intermediate(中级)、advanced(高级)。


　　1、Keywords (关键字)
　　　说明：为搜索引擎提供的关键字列表
　　　用法：<Meta name="Keywords" Content="关键词1,关键词2，关键词3,关键词4,……">
　　　注意：各关键词间用英文逗号“,”隔开。META的通常用处是指定搜索引擎用来提高搜索质量的关键词。当数个META元素提供文档语言从属信息时，搜索引擎会使用lang特性来过滤并通过用户的语言优先参照来显示搜索结果。例如：
　　　　　　<Meta name="Kyewords" Lang="EN" Content="vacation,greece,sunshine">
　　　　　　<Meta name="Kyewords" Lang="FR" Content="vacances,grè:ce,soleil">

　　2、Description (简介)
　　　说明：Description用来告诉搜索引擎你的网站主要内容。
　　　用法：<Meta name="Description" Content="你网页的简述">
　　　注意：

　　3、Robots (机器人向导)
　　　说明：Robots用来告诉搜索机器人哪些页面需要索引，哪些页面不需要索引。Content的参数有all、none、index、noindex、follow、nofollow。默认是all。
　　　用法：<Meta name="Robots" Content="All|None|Index|Noindex|Follow|Nofollow">
　　　注意：许多搜索引擎都通过放出robot/spider搜索来登录网站，这些robot/spider就要用到meta元素的一些特性来决定怎样登录。

　　　 all：文件将被检索，且页面上的链接可以被查询；
　　　 none：文件将不被检索，且页面上的链接不可以被查询；(和 "noindex, no follow" 起相同作用)
　　　 index：文件将被检索；（让robot/spider登录）
　　　 follow：页面上的链接可以被查询；
　　　 noindex：文件将不被检索，但页面上的链接可以被查询；(不让robot/spider登录)
　　　nofollow：文件将不被检索，页面上的链接可以被查询。(不让robot/spider顺着此页的连接往下探找)

　　4、Author (作者)
　　　说明：标注网页的作者或制作组
　　　用法：<Meta name="Author" Content="张三，abc@sohu.com">
　　　注意：Content可以是：你或你的制作组的名字,或Email

　　5、Copyright (版权)
　　　说明：标注版权
　　　用法：<Meta name="Copyright" Content="本页版权归Zerospace所有。All Rights Reserved">
　　　注意：

　　6、Generator (编辑器)
　　　说明：编辑器的说明
　　　用法：<Meta name="Generator" Content="PCDATA|FrontPage|">
　　　注意：Content="你所用编辑器"

　　7、revisit-after (重访)
　　　说明：
　　　用法：<META name="revisit-after" CONTENT="7 days" >
　　　注意：

       ※Head中的其它一些用法

　　1、scheme (方案)
　　　说明：scheme can be used when name is used to specify how the value of content should
　　　　　　be interpreted.
　　　用法：<meta scheme="ISBN" name="identifier" content="0-14-043205-1" />
　　　注意：

　　2、Link (链接)
　　　说明：链接到文件
　　　用法：<Link href="soim.ico" rel="Shortcut Icon">
　　　注意：很多网站如果你把她保存在收件夹中后，会发现它连带着一个小图标，如果再次点击进入之后还会发现地址栏中也有个小图标。现在只要在你的页头加上这段话，就能轻松实现这一功能。<LINK> 用来将目前文件与其它 URL 作连结，但不会有连结按钮，用於 <HEAD> 标记间， 格式如下：
　　　　　　　<link href="URL" rel="relationship">
　　　　　　　<link href="URL" rev="relationship">

　　3、Base (基链接)
　　　说明：插入网页基链接属性
　　　用法：<Base href="http://www.csdn.net/" target="_blank">
　　　注意：你网页上的所有相对路径在链接时都将在前面加上“http://www.cn8cn.com/”。其中target="_blank"是链接文件在新的窗口中打开，你可以做其他设置。将“_blank”改为“_parent”是链接文件将在当前窗口的父级窗口中打开；改为“_self”链接文件在当前窗口（帧）中打开；改为“_top”链接文件全屏显示。



    <head>　　
        <title>文件头，显示在浏览器标题区</title>
        <meta http-equiv="Content-Language" content="zh-cn">
    　<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    　<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
    　<meta name="ProgId" content="FrontPage.Editor.Document">
    　<meta name="制作人" content="Simonzy">
    　<meta name="主题词" content="HTML 网页制作 C# .NET JavaScript JS">
    </head>



        以上是META标签的一些基本用法，其中最重要的就是：Keywords和Description的设定。为什么呢？道理很简单，这两个语句可以让搜索引擎能准确的发现你，吸引更多的人访问你的站点!根据现在流行搜索引擎(Google，Lycos，AltaVista等)的工作原理，搜索引擎先派机器人自动在WWW上搜索，当发现新的网站时，便于检索页面中的Keywords和Description，并将其加入到自己的数据库，然后再根据关键词的密度将网站排序。
*/