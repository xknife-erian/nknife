using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKnife.Chesses.Common.Base
{
    public class Definition
    {
        protected readonly Dictionary<string, string> _Definitions = new Dictionary<string, string>();

        public void Add(string key, string value)
        {
            _Definitions.Add(key, value);
        }

        public string Get(string key)
        {
            return _Definitions[key];
        }

        public string Update(string key, string value)
        {
            if (!_Definitions.ContainsKey(key))
            {
                _Definitions.Add(key, value);
            }

            return $"{key}:{value}";
        }
    }
}
