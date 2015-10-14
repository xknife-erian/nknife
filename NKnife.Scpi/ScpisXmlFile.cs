using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NKnife.XML;

namespace ScpiKnife
{
    public class ScpisXmlFile : AbstractXmlDocument
    {
        public ScpisXmlFile(string fullName) 
            : base(fullName)
        {
        }

        public override string RootNodeLocalName
        {
            get { return "instrument"; }
        }
    }
}

