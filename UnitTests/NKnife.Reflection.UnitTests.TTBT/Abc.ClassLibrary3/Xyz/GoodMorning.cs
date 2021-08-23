using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abc.ClassLibrary3.Xyz
{
    public class GoodMorning
    {
        public Hello Build(string a, string b, string c)
        {
            return new Hello(a, b, c);
        }
    }
}
