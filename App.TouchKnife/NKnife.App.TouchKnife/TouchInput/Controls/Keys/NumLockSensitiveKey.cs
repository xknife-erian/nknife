using System.Collections.Generic;
using WindowsInput.Native;

namespace NKnife.App.TouchIme.TouchInput.Controls.Keys
{
    public class NumLockSensitiveKey : MultiCharacterKey
    {
        public NumLockSensitiveKey(VirtualKeyCode keyCode, IList<string> keyDisplays)
            : base(keyCode, keyDisplays)
        {
        }
    }
}