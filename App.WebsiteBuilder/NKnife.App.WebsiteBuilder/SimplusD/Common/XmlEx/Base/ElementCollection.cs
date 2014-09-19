using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD
{
    public class ElementCollection<T, M> : RichList<T>
        where M : BaseDocument
        where T : BaseElement<M>

    {
        public M OwnerDocument { get; private set; }
        internal ElementCollection(M ownerDocument)
            : base()
        {
            this.OwnerDocument = ownerDocument;
        }

        public T this[string id]
        {
            get
            {
                foreach (T ele in this)
                {
                    if (ele._innerXmlEle.GetAttribute("id") == id)
                    {
                        return ele;
                    }
                }

                return null;
            }
        }
    }
}