using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScpiKnife.Parser
{
    public class ScpiTokenTypeaa
    {
        private string _COLON = (":");
        private string _SEMICOLON = (";");
        private string _QUOTEDstring = "\"[^\"]*?\"";
        private string _COMMAND = ("[a-zA-z*_?]+");
        private string _ARGUMENT = ("[a-zA-z0-9\\.]+");
        private string _WHITESPACE = ("[ \t]+");
        private string _NEWLINE = ("[\r\n]+");

        public string COLON
        {
            get { return _COLON; }
        }

        public string SEMICOLON
        {
            get { return _SEMICOLON; }
        }

        public string QUOTEDstring
        {
            get { return _QUOTEDstring; }
        }

        public string COMMAND
        {
            get { return _COMMAND; }
        }

        public string ARGUMENT
        {
            get { return _ARGUMENT; }
        }

        public string WHITESPACE
        {
            get { return _WHITESPACE; }
        }

        public string NEWLINE
        {
            get { return _NEWLINE; }
        }
    }
}
