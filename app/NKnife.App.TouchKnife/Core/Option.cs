using System;
using NKnife.Configuring.Interfaces;
using NKnife.Configuring.OptionCase;

namespace NKnife.App.TouchInputKnife.Core
{
    public class Option : IOptionManager
    {
        public string CurrentClientId { get; private set; }
        public string GetOptionValue(string category, string key)
        {
            switch (key)
            {
                case "OffsetX":
                    return 0.ToString();
                case "OffsetY":
                    return 0.ToString();
            }
            return string.Empty;
        }

        public T GetOptionValue<T>(string category, string key) where T : struct
        {
            throw new NotImplementedException();
        }

        public T GetOptionValue<T>(string category, string key, Func<object, T> parser)
        {
            throw new NotImplementedException();
        }

        public bool SetOptionValue(string category, string key, object value)
        {
            throw new NotImplementedException();
        }

        public bool ReLoad()
        {
            throw new NotImplementedException();
        }

        public bool Clean()
        {
            throw new NotImplementedException();
        }

        public object Backup()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public event EventHandler LoadedEvent;
        public OptionCaseItem CurrentCase { get; set; }
        public void AddOrUpdateCaseStore(OptionCaseItem optionCase)
        {
            throw new NotImplementedException();
        }

        public void RemoveCaseStore(OptionCaseItem optionCase)
        {
            throw new NotImplementedException();
        }

        public OptionCaseItem CopyCaseFrom(OptionCaseItem srcCase)
        {
            throw new NotImplementedException();
        }

        public void AddCase(OptionCaseItem optionCase, bool isStore)
        {
            throw new NotImplementedException();
        }

        public void RemoveCase(OptionCaseItem optionCase, bool isStore)
        {
            throw new NotImplementedException();
        }
    }
}
