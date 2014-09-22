using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.WordSegmentor
{
    /// <summary>
    /// 规则接口
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// 处理规则
        /// </summary>
        /// <param name="preWords">预处理后的单词列表</param>
        /// <param name="index">当前要处理的词位于预处理后的单词列表中的位置</param>
        /// <param name="retWords">输出的单词列表</param>
        /// <returns>规则生效返回下一个要处理的Index位置，否则返回-1</returns>
        int ProcRule(List<String> preWords, int index, List<String> retWords);

    }
}
