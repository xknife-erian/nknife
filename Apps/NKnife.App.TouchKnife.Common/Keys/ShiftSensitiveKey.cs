using System.Collections.Generic;
using WindowsInput.Native;

namespace NKnife.App.TouchInputKnife.Commons.Keys
{
    public class ShiftSensitiveKey : MultiCharacterKey
    {
        public ShiftSensitiveKey(VirtualKeyCode keyCode, IList<string> keyDisplays)
            : base(keyCode, keyDisplays)
        {
        }
    }
}