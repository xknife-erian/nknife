using System.Collections.Generic;

namespace ScpiKnife
{
    /// <summary>
    /// 一组指令，将按顺序执行
    /// </summary>
    public class ScpiGroup : LinkedList<ScpiCommand>
    {
    }
}