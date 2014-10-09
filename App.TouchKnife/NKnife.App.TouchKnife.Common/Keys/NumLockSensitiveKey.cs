using System.Collections.Generic;
using WindowsInput.Native;

namespace NKnife.App.TouchKnife.Common.Keys
{
    public class NumLockSensitiveKey : MultiCharacterKey
    {
        public NumLockSensitiveKey(VirtualKeyCode keyCode, IList<string> keyDisplays)
            : base(keyCode, keyDisplays)
        {
        }
    }
}