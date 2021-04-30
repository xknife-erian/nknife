using System.Collections.Generic;
using WindowsInput.Native;

namespace NKnife.App.TouchInputKnife.Commons.Keys
{
    public class NumLockSensitiveKey : MultiCharacterKey
    {
        public NumLockSensitiveKey(VirtualKeyCode keyCode, IList<string> keyDisplays)
            : base(keyCode, keyDisplays)
        {
        }
    }
}