using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKnife.Tools.Robot.CubeOctopus
{
    internal struct Pair<T, S>
    {
        public T First { get; set; }
        public S Second { get; set; }

        public Pair(T first, S second)
        {
            First = first;
            Second = second;
        }

        public static Pair<T, T> Build<T>(T p0, T p1)
        {
            return new Pair<T, T>(p0, p1);
        }
    }
}
