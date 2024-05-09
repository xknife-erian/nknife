using WindowsInput.Native;

namespace NKnife.App.TouchInputKnife.Commons.Keys
{
    public class InstantaneousModifierKey : ModifierKeyBase
    {
        public InstantaneousModifierKey(string displayName, VirtualKeyCode keyCode) :
            base(keyCode)
        {
            DisplayName = displayName;
        }

        public override void Press()
        {
            if (IsInEffect)
                _Simulator.Keyboard.KeyUp(KeyCode);
            else
                _Simulator.Keyboard.KeyDown(KeyCode);

            // We need to use IsKeyDownAsync here so we will know exactly what state the key will be in
            // once the active windows read the input from the MessagePump.  IsKeyDown will only report
            // the correct value after the input has been read from the MessagePump and will not be correct
            // by the time we set IsInEffect.
            IsInEffect = _Simulator.InputDeviceState.IsKeyDown(KeyCode);
            OnKeyPressed();
        }

        public override void SynchroniseKeyState()
        {
            IsInEffect = _Simulator.InputDeviceState.IsKeyDown(KeyCode);
        }
    }
}