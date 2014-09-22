using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    abstract public class SiteCreator
    {
        public SiteCreator()
        {

        }

        public abstract void CreateSite(string sitePath,string siteName);
    }
}
