using System;
using System.Collections.Generic;
using System.Text;
using NKnife.Interface;

namespace Gean.Module.Chess
{
    public class Nag : IItem
    {
        public Nag()
        {

        }
        public Nag(string value)
        {
            this.Value = value;
        }

        public string ItemType { get { return "Nag"; } }
        public string Value { get; set; }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
