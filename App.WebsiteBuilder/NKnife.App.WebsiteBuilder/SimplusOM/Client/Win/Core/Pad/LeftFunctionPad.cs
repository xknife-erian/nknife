using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client.Win
{
    public class LeftFunctionPad : AbstractPad
    {
        private ListView _listViewEx;
        private LeftFunctionPad()
        {
            this._listViewEx = new ListView();
            this.Controls.Add(this._listViewEx);
            this._listViewEx.Dock = DockStyle.Fill;
        }

        static LeftFunctionPad _ownLeftFunctionPad = null;
        static public LeftFunctionPad LeftFunctionPadSingle()
        {
            if (_ownLeftFunctionPad == null)
            {
                _ownLeftFunctionPad = new LeftFunctionPad();
            }
            return _ownLeftFunctionPad;
        }
    }
}
