using System.Collections.Generic;
using WindowsInput.Native;

namespace NKnife.App.TouchKnife.Common.Keys
{
    public class CaseSensitiveKey : MultiCharacterKey
    {
        public CaseSensitiveKey(VirtualKeyCode keyCode, IList<string> keyDisplays)
            : base(keyCode, keyDisplays)
        {
        }
    }
}