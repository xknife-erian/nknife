using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public class BaseSiteType : SiteType
    {
        public BaseSiteType(string name,string text,string imageKey)
            :base(name,text,imageKey)
        {
        }

        public override SiteCreator GetSiteCreator()
        {
            return new BaseSiteCreator();
        }
    }
}
