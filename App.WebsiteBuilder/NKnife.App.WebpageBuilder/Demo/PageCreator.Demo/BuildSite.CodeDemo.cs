using System;
using System.Collections.Generic;
using System.Text;
using Jeelu.SimplusD;

namespace PageCreator.Demo
{
    class BuildSite
    {
        public BuildSite()
        {
            ToHtmlHelper siteHelper = new ToHtmlHelper("", @"D:\_abc\myabc");

            TmpltXmlDocument tmpltDoc = null;
            tmpltDoc.SaveXhtml(siteHelper);
            tmpltDoc.DeleteXhtml(siteHelper);

            PageXmlDocument pageDoc = null;
            pageDoc.SaveXhtml(siteHelper);
            pageDoc.DeleteXhtml(siteHelper);

            foreach (var item in tmpltDoc.GetSnipElementList())
            {
                SnipXmlElement snip = (SnipXmlElement)item;
                snip.SaveXhtml(siteHelper);
                snip.DeleteXhtml(siteHelper);
            }
        }
    }
}
