using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace Jeelu.SimplusD.Server
{ 
    /// <summary>
    /// 模板及页面的生成　;注意几点:1.先创建模板后创建页面 2.
    /// 如：新建模板，修改模板，新建网页，修改网页
    /// 此时，.sdsite文件的特征tmplt节点在前,所有page节点在后
    /// </summary>
    public class BuildFile
    {
        #region 字段定义
        //const string CssDAT = ".css";

        /// <summary>
        /// 返回项目在web服务器上的路径; 
        /// 如: Y:\zha\zhangling\projectname
        /// </summary>
        string sdWebAbsPath;

        /// <summary>
        /// 存储模板与页面的对应关系;1对多
        /// </summary>
        Dictionary<string, List<PageSimpleExXmlElement>> _FileDic;

        #endregion

        #region 属性定义
        /// <summary>
        /// 传入用户的部分路径(如:zha\zhangling\projectname)，得到此网站在路径
        /// </summary>
        public string BuildFilePath
        {
            set
            {
                sdWebAbsPath = AnyFilePath.GetWebAbsolutePath(value);
            }
        }

        public ToHtmlHelper ToHtmlHelperObj { get; set; }

        public SdsiteXmlDocument SdsiteDocument { get; set; }

        #endregion

        #region 构造函数
        public BuildFile()
        {
            _FileDic = new Dictionary<string, List<PageSimpleExXmlElement>>();
        }

        #endregion


        /// <summary>
        /// 生成页面文件
        /// </summary>
        /// <param name="simpleEle"></param>
        public void BuildPageFile(SimpleExIndexXmlElement simpleEle)
        {
            //添加模板到Dictionary字典中
            AddElementToDictionary(simpleEle);

            PageSimpleExXmlElement pageEle = (PageSimpleExXmlElement)simpleEle;
            string pageId = pageEle.Id;
            string channelId = pageEle.OwnerChannelElement.Id; //页面直属频道的Id
            
            PageXmlDocument pageDoc = SdsiteDocument.GetPageDocumentById(pageId);

            if (pageEle.IsDeletedRecursive)
            {
                //如果 是索引页面，删除本身的文件 filename.sdpage index.sdpage，其它页面则还要删除
                //及一系列的关于页面的文件filename_head.inc filename_content.inc filename_list.inc
                pageDoc.DeleteXhtml(ToHtmlHelperObj);

                //此时就要看与此文件关联的其它文件
                if (AsTmpltList(channelId))
                {

                    NewMethod(channelId);
                }
                //文件删除，则其它链接到此文件文件不做处理
            }
            else if (!pageEle.IsAlreadyPublished) //新建
            {
                //如果 是索引页面,新建本身的文件 filename.sdpage 及index.shtml
                //,其它类型页面则要新建一系列的关于页面的文件filename_head.inc filename_content.inc 


                //todo:...


                //此时，就要查看与此关联的文件，是如何
                if (AsTmpltList(channelId))
                {
                    //生成filename_list.inc文件

                    NewMethod(channelId);

                    //此处不用考虑其路径问题

                }

            }
            else
            {
                //文件路径是否改变（与重命名有密切关系）
                if (pageEle.IsChangedPosition)
                {
                    //先将文件移动到新的位置

                }

                if (pageEle.IsModified)
                {
                    //页面本身的一些文件重新生成

                    //关联页面
                    NewMethod(channelId);

                    //路径关联的一些页面的重新生成
                    //如果发生CustomID的变化，找到链接到此文件的所有页面，然后重新生成

                }
                else
                { 
                   //如果是模板的内容布局有所在改变，则content还是要重新生成


                }

            }

        }

        private void NewMethod(string channelId)
        {
            //查找拥有此页面直属的频道节点的所有的模板节点,返回模板ID集合
            string[] tmpltIdArray = SdsiteDocument.GetTmpltIdArray(channelId);

            foreach (var tmpltId in tmpltIdArray)
            {
                TmpltXmlDocument tmpltDoc = SdsiteDocument.GetTmpltDocumentById(tmpltId);
                
                //得到当前模板下拥有此页面所属频道的类型为list的所有part的父父级snip的集合
                string[] snipIdArray = SdsiteDocument.GetSnipIdArray(tmpltId, channelId);

                //循环snip集合，获得每个snip节点下的分别有拥有此页面所属的频道的类型为list的part节点的集合
                foreach (var snipId in snipIdArray )
                {
                    //重新生成 snip 页面
                    SnipXmlElement snipEle = tmpltDoc.GetSnipElementById(snipId);
                    if (!snipEle.IsModified)
                    {
                        snipEle.SaveXhtml(ToHtmlHelperObj);
                    }

                    //对其part 的处理
                    snipEle.GetPartsElement();
                }

                //不论按哪种排序，则先将其所属的snip节点重新生成一遍


                //如果其排序方式为自动提取关键字，则取出此part下的所有频道id,及页面类型集合


                //循环这些频道，找出页面类型为以上类型集合的所有页面


                //此处应有一些分支操作:=====isModified = false的一定要重新生成(并记录下isModified=false,并记录重新生成过的页面)，而ismodified为真或是isAlreadyPublish =false,或是isdeleted=true自然会有一些相应的操作


                //最后，将重新生成这些页面

            }
        }


        /// <summary>
        /// 生成模板文件
        /// </summary>
        /// <param name="simpleEle"></param>
        public void BuildTmpltFile(SimpleExIndexXmlElement simpleEle)
        {
            //添加模板到Dictionary字典中
            AddElementToDictionary(simpleEle);

            /// tmpltFolderAbsPath ="Y:\zha\zhangling\projectname\TmpltRootFolder";
            ///string tmpltFolderAbsPath = Path.Combine(sdsiteAbsPath, SdsiteDocument.TmpltFolder.FileName);

            TmpltSimpleExXmlElement tmpltEle = (TmpltSimpleExXmlElement)simpleEle;
            string tmpltId = tmpltEle.Id;
            TmpltXmlDocument tmpltDoc = ToHtmlHelperObj.SdsiteXmlDocument.GetTmpltDocumentById(tmpltId);

            if (tmpltEle.IsDeletedRecursive)
            {
                //删除模板本身的文件
                //删除此模板文件对应的模板文件夹（内有多个snip文件）
                //删除相应的css文件
                //string filePath = tmpltFolderAbsPath + @"\" + tmpltId + CommonService.Inc;
                //FileService.FileDelete(filePath);
                //string folderPath = tmpltFolderAbsPath + @"\" + tmpltId;
                //FolderService.FolderDelete(folderPath);
                //string cssPath = tmpltFolderAbsPath + @"\" + CommonService.Css + @"\" + tmpltId + CssDAT;
                //FileService.FileDelete(cssPath);
                tmpltDoc.DeleteXhtml(ToHtmlHelperObj);    
            }
            else if (!tmpltEle.IsAlreadyPublished) //新建
            {
                //打开相应的模板
                //生成本身的文件.css
                //遍历生成相应的snip文件
                //string tmpltPath = tmpltFolderAbsPath + @"\" + tmpltId + CommonService.Inc;
                //TmpltXmlDocument tmpltDoc = SdsiteDocument.GetTmpltDocumentById(tmpltId);
                //tmpltDoc.SaveXhtml();
                //tmpltDoc.SaveXhtml(tmpltPath);
                //string tmpltCssPath = tmpltFolderAbsPath + @"\" + tmpltId + @"\" + CssDAT;
                //tmpltDoc.SaveCss();
                tmpltDoc.SaveXhtml(ToHtmlHelperObj);

                //string folderPath = tmpltFolderAbsPath + @"\" + tmpltId;
                XmlNodeList snipList = tmpltDoc.GetSnipElementList();
                foreach (var item in snipList)
                {
                    SnipXmlElement snipEle = item as SnipXmlElement;
                    //string snipfile = snipEle.Id + CommonService.Inc;
                    //string snipFilePath = Path.Combine(folderPath, snipfile);
                    //snipEle.SaveXhtml();
                    //snipEle.SaveXhtml(snipFilePath);
                    snipEle.SaveXhtml(ToHtmlHelperObj);
                }

            }
            else
            {
                //此处没有路径改变的问题
                if (tmpltEle.IsModified)
                {
                    //打开相应的模板文件
                    //则需要重新生成本身的文件，及有改变过的snip的对应的文件
                    //tmpltDoc.SaveXhtml(); //方法内部在保存此文件时，还需要检查此文件是否存在，存在，则删除掉，然后将其在保存
                    //tmpltDoc.SaveCss();
                    tmpltDoc.SaveXhtml(ToHtmlHelperObj);

                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    XmlNodeList snipList = tmpltDoc.GetSnipElementList();
                    foreach (var item in snipList)
                    {
                        SnipXmlElement snipEle = item as SnipXmlElement;

                        if (snipEle.IsModified)
                        {
                            //则需要重新生成
                           // snipEle.SaveXhtml();
                            snipEle.SaveXhtml(ToHtmlHelperObj);
                        }

                        string snipFileName = snipEle.Id + CommonService.Inc;
                        dic.Add(snipFileName, "");
                    }

                    //看是否存在多余的snip文件，并将其删除
                    string snipFolderPath = sdWebAbsPath + @"\" + SdsiteDocument.TmpltFolder.Name + @"\" + tmpltId;
                    string[] allSnipFile = GetAllSnipFile(snipFolderPath);
                    foreach (var file in allSnipFile)
                    {
                        if (!dic.ContainsKey(file))
                        {
                            //删除此文件
                            string filePath = snipFolderPath + @"\" + file;
                            FileService.FileDelete(filePath);
                        }
                    }
                }
            }
        }

        #region 方法
        /// <summary>
        /// 添加模板或页面元素到Dictionary字典中
        /// </summary>
        /// <param name="simpleEle"></param>
        private void AddElementToDictionary(SimpleExIndexXmlElement simpleEle)
        {
            //循环遍历.sdsite文件时，将其模板(tmplt)元素添加到此字典中

            if (simpleEle.DataType == DataType.Tmplt)
            {
                string _tmpltId = ((TmpltSimpleExXmlElement)simpleEle).Id;
                _FileDic.Add(_tmpltId, new List<PageSimpleExXmlElement>());
            }
            else if (simpleEle.DataType == DataType.Page)
            {
                string _getTmpltId = ((PageSimpleExXmlElement)simpleEle).TmpltId;

                //找到字典中此模板id对应的list列表，直接添加进去//也会只会放一些更有用的页面//todo。。。
                _FileDic[_getTmpltId].Add((PageSimpleExXmlElement)simpleEle);
            }
        }

        /// <summary>
        /// 得到当前目录下的所有文件，并返回一组文件名
        /// </summary>
        /// <returns></returns>
        private string[] GetAllSnipFile(string path)
        {
            List<string> list = new List<string>();
            string[] pp = Directory.GetFiles(path);
            DirectoryInfo dic = new DirectoryInfo(path);
            FileInfo[] files = dic.GetFiles();
            foreach (var file in files)
            {
               // string ext = item.Extension;
                //string name = item.Name.Replace(ext, "");
                list.Add(file.Name);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 查看当前页面的所属频道是否作为模板中页面片类型为列表的编辑频道组(指所有模板)
        /// </summary>
        /// <returns></returns>
        private bool AsTmpltList(string channelId)
        {
            //string channelId = pageEle.OwnerChannelElement.Id;
            bool isAsChannelofsnip = SdsiteDocument.isExistChannelofsnip(channelId);
            if (isAsChannelofsnip)
            {
                return true;
            }
            return false;
        }

        #endregion

    }
}
