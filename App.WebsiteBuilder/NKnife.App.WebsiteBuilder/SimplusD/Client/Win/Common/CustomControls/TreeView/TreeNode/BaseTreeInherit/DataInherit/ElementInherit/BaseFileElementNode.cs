using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public abstract class BaseFileElementNode : ElementNode
    {
        public BaseFileElementNode(SimpleExIndexXmlElement element)
            :base(element,false)
        {
        }
    }
}
