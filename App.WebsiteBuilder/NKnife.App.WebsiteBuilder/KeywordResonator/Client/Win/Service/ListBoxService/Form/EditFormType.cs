using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.KeywordResonator.Client
{
    public enum EditFormType
    {
        CombinItem = 0, //合并项
        SplitItem = 1, //拆分项
        EditItem = 2, //编辑项
        EditFrequency = 3, //修改老词权重
        AddItem = 4, //增加项

    }
}
