using WindowsInput.Native;

namespace NKnife.TouchInput.Controls.Keys
{
    public abstract class ModifierKeyBase : VirtualKey
    {
        private bool _IsInEffect;

        public bool IsInEffect
        {
            get { return _IsInEffect; }
            set
            {
                if (value != _IsInEffect)
                {
                    _IsInEffect = value;
                    OnPropertyChanged("IsInEffect");
                }
            }
        }

        protected ModifierKeyBase(VirtualKeyCode keyCode) :
            base(keyCode)
        {
        }

        public abstract void SynchroniseKeyState();
    }
}