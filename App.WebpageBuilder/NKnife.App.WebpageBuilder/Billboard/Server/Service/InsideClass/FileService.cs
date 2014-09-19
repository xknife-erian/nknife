using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace Jeelu.Billboard.Server
{
    internal static partial class Service
    {
        /// <summary>
        /// 应用程序的File静态类（一般是一些针对性的文件操作）
        /// </summary>
        internal static partial class FileService
        {
            /// <summary>
            /// 建立广告位(页面)与广告的相关联的关系文件
            /// </summary>
            internal static void CreateBillboardRelation(long[] adIds, string url)
            {
                if (string.IsNullOrEmpty(url))
                {
                    return;
                }
                Uri uri = new Uri(url);

                string absPath = uri.AbsolutePath.Replace('/', '\\').TrimStart('\\');
                string filefull = System.IO.Path.Combine(Path.Directory.RelationFileBasePath, absPath);
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filefull));

                StreamWriter sw = File.CreateText(filefull);
                StringBuilder sb = new StringBuilder();
                foreach (var item in adIds)
                {
                    sb.Append(item).Append(',');
                }
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Dispose();
            }
        }
    }
}




/*

private class UrlReg
{
    public long RegId { get; set; }
    public string[] Codes { get; set; }
}


/// <summary>
/// 动态url正则式的缓存
/// </summary>
//TODO: 最好是改为LRU算法的缓存，或者是memcached
private static Dictionary<string, UrlReg> _UrlRegCache = new Dictionary<string, UrlReg>();

if (url != null)
{
    //TODO: 去除魔法字符串
    //TODO: 正则式缓存
    Uri Uri = new Uri(url);
    //基本路径 + 域名 + 网页路径 + (index.html 静态url，且以/结尾) / ( 正则式id + 处理后的参数 )
    //以 http://dev2dev.bea.com/pub/a/2006/01/ejb-3.html?page=last&id=1 为例子
    //先取得 D:\jihui\ + dev2dev.bea.com + \pub\a\2006\01\ejb-3.html
    String path = System.IO.Path.Combine(Service.Path.Directory.RelationFileBasePath, Uri.Host);
    path = System.IO.Path.Combine(path, Uri.AbsolutePath);
    //如果是动态页，而且不是 http://www.google.cn/? 这种单留一个？的这种url
    if (Uri.Query.Contains("?") && !Uri.Query.Equals("?"))
    {
        //取出?page=last&id=1 中的 page=last 和 id=1
        string[] querys = Uri.Query.Substring(1).Split('&');
        //拆为 page为last ， id为1 的map，并依据两个key的字符排序
        SortedDictionary<string, string> qDic = new SortedDictionary<string, string>();
        foreach (var query in querys)
        {
            string[] x = query.Split('=');
            if (x != null && x.Length != 0)
            {
                if (x.Length > 1)
                {
                    qDic[x[0]] = x[1];
                }
                else if (x.Length == 1)
                {
                    qDic[x[0]] = "";
                }
            }
        }
        //拼成 id,page 这样的字符串做为动态url的 排序后参数列表
        StringBuilder sb = new StringBuilder();
        foreach (var item in qDic.Keys)
        {
            sb.Append(item).Append(",");
        }
        string OrderedParam = sb.ToString(0, sb.Length - 1);
        //依据 排序后参数列表 从缓存中找相应的参数表(只包含有效参数)
        UrlReg UrlReg;
        if (!_UrlRegCache.TryGetValue(OrderedParam, out UrlReg))
        {
            UrlReg = new UrlReg();
            //缓存中没有就去数据库中查找
            DataTable RegIdDataTable = null;//DbHelper.GetFetchurlReg(Uri.Host, OrderedParam);
            if (RegIdDataTable.Rows.Count == 0)
            {
                //数据库中没有就新建一个reg和若干个param
                //TODO:
            }
            else
            {
                long Id = (long)RegIdDataTable.Rows[0][0];
                UrlReg.RegId = Id;
                DataTable ParamDataTable = null;// DbHelper.GetFetchurlParam(Id);
                string[] TempCodes = new string[ParamDataTable.Rows.Count];
                for (int i = 0; i < TempCodes.Length; i++)
                {
                    if (ParamDataTable.Rows[i][0] != DBNull.Value)
                    {
                        TempCodes[i] = (string)ParamDataTable.Rows[i][0];
                    }
                    else
                    {
                        TempCodes[i] = "";
                    }
                }
                UrlReg.Codes = TempCodes;
                _UrlRegCache[OrderedParam] = UrlReg;
            }
        }
        //取出排序后的结果集
        string[] values = new string[UrlReg.Codes.Length];
        for (int i = 0; i < UrlReg.Codes.Length; i++)
        {
            values[i] = qDic[UrlReg.Codes[i]];
        }
        sb = new StringBuilder();
        foreach (var item in values)
        {
            sb.Append(item).Append(",");
        }
        path = System.IO.Path.Combine(path, Convert.ToString(UrlReg.RegId));
        path = System.IO.Path.Combine(path, sb.ToString(0, sb.Length - 1));
    }
    else
    {
        if (Uri.AbsolutePath.EndsWith(@"/"))
        {
            path = System.IO.Path.Combine(path, "index.html");
        }
    }
    // 保存到文件
    //TODO: 
}
 */
