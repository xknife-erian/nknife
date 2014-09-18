using System.Collections.Generic;
using WindowsInput.Native;

namespace NKnife.App.TouchIme.TouchInput.Controls.Keys
{
    public class ShiftSensitiveKey : MultiCharacterKey
    {
        public ShiftSensitiveKey(VirtualKeyCode keyCode, IList<string> keyDisplays)
            : base(keyCode, keyDisplays)
        {
        }
    }
}