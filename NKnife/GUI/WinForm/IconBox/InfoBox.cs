﻿using System.Drawing;

namespace NKnife.GUI.WinForm.IconBox
{
    public class InfoBox : Gean.Gui.WinForm.IconBox.IconBox
    {
        protected override Icon CoreIcon
        {
            get { return Properties.Resources.Info; }
        }
    }
}
