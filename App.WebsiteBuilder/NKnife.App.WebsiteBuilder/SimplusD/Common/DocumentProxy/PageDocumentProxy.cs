using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD
{
    public class PageDocumentProxy : DocumentProxy
    { 
        public PageDocumentProxy(PageXmlDocument document)
        {
            this._id = document.Id;
            this._sdsiteDocument = document.OwnerSdsiteDocument;
        }

        public override object GetDocument()
        {
            return _sdsiteDocument.GetPageDocumentById(_id);
        }
    }
}
