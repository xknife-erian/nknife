using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    abstract public class SiteType
    {
        private string _name;
        public string Name
        {
            get { return _name; }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
        }

        private string _imageKey;
        public string ImageKey
        {
            get { return _imageKey; }
        }

        public SiteType(string name,string text,string imageKey)
        {
            this._name = name;
            this._text = text;
            this._imageKey = imageKey;
        }

        public abstract SiteCreator GetSiteCreator();
    }
}
