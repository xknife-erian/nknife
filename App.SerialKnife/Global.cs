using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using NKnife.Wrapper;

namespace SocketKnife
{
    internal class Global
    {
        private static Font _monospacedFont;

        /// <summary>
        /// 等宽字体
        /// </summary>
        public static Font MonospacedFont
        {
            get
            {
                if (_monospacedFont == null)
                {
                    var fm = new FontFamily("Courier New");
                    var families = FontFamily.Families;
                    foreach (var f in families)
                    {
                        if (f.Name == "Consolas")
                            fm = new FontFamily("Consolas");
                    }
                    _monospacedFont = new Font(fm, 9);
                }
                return _monospacedFont;
            }
        }

        public class About : NKnife.Wrapper.About
        {
        }
    }
}
