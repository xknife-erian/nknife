using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NKnife.Kits.SocketKnife.StressTest.Base
{
    public class AppUtility
    {
        public static void LimitTextBoxTextLengh(TextBox textbox, int limit = 3000)
        {
            if (textbox.Text.Length > 1000)
            {
                textbox.Text = textbox.Text.Substring(0,
                    textbox.Text.LastIndexOf("\r\n", StringComparison.Ordinal));
            }
        }
    }
}
