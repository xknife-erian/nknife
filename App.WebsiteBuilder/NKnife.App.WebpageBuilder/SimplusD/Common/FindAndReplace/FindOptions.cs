using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 枚举：查找策略类型，（正常、正则表达式、通配符）
    /// </summary>
    public enum FindType
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        RegEx = 0,
        /// <summary>
        /// 通配符
        /// </summary>
        Wildcard = 1
    }

    /// <summary>
    /// 枚举：查找范围类型
    /// </summary>
    public enum FindScope
    {
        /// <summary>
        /// 当前窗口
        /// </summary>
        CurrentForm = 0,
        /// <summary>
        /// 所有打开的窗口
        /// </summary>
        AllOpenForm = 1,
        /// <summary>
        /// 所有频道
        /// </summary>
        WholeChannels = 2
    }

    /// <summary>
    /// edited by zhenghao at 2008-05-28 : 查找目标的类型
    /// </summary>
    public enum FindTargetType
    {
        /// <summary>
        /// 默认
        /// </summary>
        Normal = 0,
        ///// <summary>
        ///// 模板类型
        ///// </summary>
        //Tmplt = 1,
        ///// <summary>
        ///// 页面片类型
        ///// </summary>
        //Snip = 2,
        ///// <summary>
        ///// 页面类型
        ///// </summary>
        Page = 1
    }
    /// <summary>
    /// 操作的类型
    /// </summary>
    public enum WantToDoType
    {
        /// <summary>
        /// 查找
        /// </summary>
        SearchAll = 0,
        /// <summary>
        /// 替换
        /// </summary>
        ReplaceAll = 1,
        /// <summary>
        /// 查找下一个
        /// </summary>
        SearchNext = 2,
        /// <summary>
        /// 替换下一个
        /// </summary>
        ReplaceNext = 3,
    }
    public class FindOptions
    {
        private FindOptions()
        {
            Positions = new List<Position>();
            this.index = -1;
            this.ruleIndex = -1;
        }

        readonly static public FindOptions Singler = new FindOptions();

        string findContent = "";
        /// <summary>
        /// 获取或设置需要查找的内容
        /// </summary>
        public string FindContent
        {
            get
            {
                return findContent;
            }
            set
            {
                if (value != FindContent)
                {
                    findContent = value;
                }
            }
        }

        string replaceContent = "";
        /// <summary>
        /// 获取或设置需要替换成的内容
        /// </summary>
        public string ReplaceContent
        {
            get
            {
                return replaceContent;
            }
            set
            {
                if (value != ReplaceContent)
                {
                    replaceContent = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置是否全字匹配
        /// </summary>
        public bool MatchWholeWord { get; set; }

        /// <summary>
        /// 获取或设置是否大小写匹配
        /// </summary>
        public bool MatchCase { get; set; }

        /// <summary>
        /// 获取或设置是否向上搜索
        /// </summary>
        public bool UpWard { get; set; }

        /// <summary>
        /// 获取或设置是否要查找子目录
        /// </summary>
        public bool IncludeSubdirectories { get; set; }

        /// <summary>
        /// 获取或设置查找范围
        /// </summary>
        public FindScope FindScope { get; set; }

        /// <summary>
        /// 获取或设置查找方法
        /// </summary>
        public FindType FindType { get; set; }

        /// <summary>
        /// 获取或设置是否使用查找方式
        /// </summary>
        public bool IsUsingFindType { get; set; }

        /// <summary>
        /// 获取或设置查找目标的类型
        /// </summary>
        public FindTargetType FindTargetType { get; set; }

        /// <summary>
        /// 获取或设置查找起始位置
        /// </summary>
        public Position StartPosition { get; set; }

        /// <summary>
        /// 获取或设置当前指定的位置
        /// </summary>
        public Position CurrentPosition { get; set; }


        /// <summary>
        /// 获取或设置最后的位置
        /// </summary>
        public Position LastPosition { get; set; }
        /// <summary>
        /// 获取或设置所有的查找结果
        /// </summary>
        public List<Position> Positions { get; private set; }
        /// <summary>
        /// 正则。通配符的截取标记
        /// </summary>
        public int ruleIndex { get; set; }

        /// <summary>
        /// 标识是否找到
        /// </summary>
        public bool IsFindResult = false;

        public int index { get; set; }
        /// <summary>
        /// 刷新
        /// </summary>
        public void Reset()
        {
            Positions.Clear();
            CurrentPosition = null;
        }
    }
}
