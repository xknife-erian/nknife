using WindowsInput.Native;

namespace NKnife.App.TouchInputKnife.Commons.Keys
{
    public class VirtualKey : LogicalKeyBase
    {
        private VirtualKeyCode _KeyCode;

        public virtual VirtualKeyCode KeyCode
        {
            get { return _KeyCode; }
            set
            {
                if (value != _KeyCode)
                {
                    _KeyCode = value;
                    OnPropertyChanged("KeyCode");
                }
            }
        }

        public VirtualKey(VirtualKeyCode keyCode, string displayName)
        {
            DisplayName = displayName;
            KeyCode = keyCode;
        }

        public VirtualKey(VirtualKeyCode keyCode)
        {
            KeyCode = keyCode;
        }

        public VirtualKey()
        {
        }

        public override void Press()
        {
            _Simulator.Keyboard.KeyPress(_KeyCode);
            base.Press();
        }
    }
}