using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife
{
    public static class Global
    {
        private static string _Culture = "zh-CN";

        public static string Culture
        {
            get { return _Culture; }
            set { _Culture = value; }
        }
    }
}
