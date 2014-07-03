﻿using System.Collections.Generic;
using WindowsInput.Native;

namespace NKnife.TouchInput.Controls.Keys
{
    public class CaseSensitiveKey : MultiCharacterKey
    {
        public CaseSensitiveKey(VirtualKeyCode keyCode, IList<string> keyDisplays)
            : base(keyCode, keyDisplays)
        {
        }
    }
}