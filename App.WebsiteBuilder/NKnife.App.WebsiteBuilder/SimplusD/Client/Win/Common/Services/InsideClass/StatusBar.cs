using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        static public class StatusBar
        {
            static StatusStrip _statusStrip;
            static public void Initialize(StatusStrip statusStrip)
            {
                _statusStrip = statusStrip;
            }

            static public void ShowMessage(string text)
            {
                if (_statusStrip.Items.Count > 0)
                {
                    _statusStrip.Items[0].Text = text;
                }
                else
                {
                    MessageBox.Show(text);
                }
            }
        }
    }
}