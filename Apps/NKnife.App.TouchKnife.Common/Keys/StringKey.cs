namespace NKnife.App.TouchInputKnife.Commons.Keys
{
    public class StringKey : LogicalKeyBase
    {
        private string _StringToSimulate;

        public virtual string StringToSimulate
        {
            get { return _StringToSimulate; }
            set
            {
                if (value != _StringToSimulate)
                {
                    _StringToSimulate = value;
                    OnPropertyChanged("StringToSimulate");
                }
            }
        }

        public StringKey(string displayName, string stringToSimulate)
        {
            DisplayName = displayName;
            _StringToSimulate = stringToSimulate;
        }

        public StringKey()
        {
        }

        public override void Press()
        {
            _Simulator.Keyboard.TextEntry(_StringToSimulate);
            base.Press();
        }
    }
}
