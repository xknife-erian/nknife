using System.Collections.Generic;
using WindowsInput.Native;

namespace NKnife.Chinese.TouchInput.Controls.Keys
{
    public class ShiftSensitiveKey : MultiCharacterKey
    {
        public ShiftSensitiveKey(VirtualKeyCode keyCode, IList<string> keyDisplays)
            : base(keyCode, keyDisplays)
        {
        }
    }
}