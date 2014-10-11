using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using System.Text;
using NKnife.NIo;

namespace Gean.Module.Chess
{
    /// <summary>
    /// Provides the parsing of the PGN standard game notation files
    /// as defined by the standard.
    /// </summary>
    public class PGNReader : IPGNReader
    {

        #region  ===== delegate -> event =====

        public delegate void newGame(IPGNReader iParser);
        public event newGame EventNewGame;
        public delegate void exitHeader(IPGNReader iParser);
        public event exitHeader EventExitHeader;
        public delegate void enterVariation(IPGNReader iParser);
        public event enterVariation EventEnterVariation;
        public delegate void exitVariation(IPGNReader iParser);
        public event exitVariation EventExitVariation;
        public delegate void starting(IPGNReader iParser);
        public event starting EventStarting;
        public delegate void finished(IPGNReader iParser);
        public event finished EventFinished;
        public delegate void tagParsed(IPGNReader iParser);
        public event tagParsed EventTagParsed;
        public delegate void nagParsed(IPGNReader iParser);
        public event nagParsed EventNagParsed;
        public delegate void moveParsed(IPGNReader iParser);
        public event moveParsed EventMoveParsed;
        public delegate void commentParsed(IPGNReader iParser);
        public event commentParsed EventCommentParsed;
        public delegate void endMarkerParsed(IPGNReader iParser);
        public event endMarkerParsed EventendMarkerParsed;

        #endregion

        private Regex _Regex;
        private StringBuilder _Value;
        private string _Data;
        private bool _NextGame;
        private int _PeriodCount;

        /// <summary>
        /// Saves the state of the parser as it enter into a variation.
        /// </summary>
        private Stack _SaveState;

        /// <summary>
        /// Allows access to the current state of the parser
        /// when an event has been fired.
        /// </summary>
        public Enums.PGNReaderState State
        {
            get { return this._State; }
            set { this._State = value; }
        }
        private Enums.PGNReaderState _State;
        private Enums.PGNReaderState _PrevState;

        /// <summary>
        /// Contains the PGN tag information.
        /// </summary>
        public string Tag
        {
            get { return _Tag; }
        }
        private string _Tag;

        /// <summary>
        /// Contains the values currently parsed, normally this
        /// is accessed from the event listeners as the parses
        /// signals it has found something.
        /// </summary>
        public string Value
        {
            get { return _Value.ToString(); }
        }

        /// <summary>
        /// File name to open and parse.
        /// </summary>
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }
        private string _filename;

        /// <summary>
        /// Constructor the initializes our parser.
        /// </summary>
        public PGNReader()
        {
            _Regex = new Regex("^\\[([A-Za-z]*) \"(.*)\"", RegexOptions.Compiled);
            _Value = new StringBuilder();
            _State = Enums.PGNReaderState.Header;
            _SaveState = new Stack();
            _PeriodCount = 0;
        }

        /// <summary>
        /// Adds a specific listner to the events fired by the parsing
        /// of the PGN data.
        /// </summary>
        /// <param name="ievents"></param>
        public void AddEvents(IPGNReaderEvents ievents)
        {
            EventNewGame += new newGame(ievents.NewGame);
            EventExitHeader += new exitHeader(ievents.ExitHeader);
            EventEnterVariation += new enterVariation(ievents.EnterVariation);
            EventExitVariation += new exitVariation(ievents.ExitVariation);
            EventStarting += new starting(ievents.Starting);
            EventFinished += new finished(ievents.Finished);
            EventTagParsed += new tagParsed(ievents.TagParsed);
            EventNagParsed += new nagParsed(ievents.NagParsed);
            EventMoveParsed += new moveParsed(ievents.StepParsed);
            EventCommentParsed += new commentParsed(ievents.CommentParsed);
            EventendMarkerParsed += new endMarkerParsed(ievents.EndMarker);
        }

        /// <summary>
        /// Remove a specific listner from the events fired by the parsing
        /// of the PGN data.
        /// </summary>
        /// <param name="ievents"></param>
        public void RemoveEvents(IPGNReaderEvents ievents)
        {
            EventNewGame -= new newGame(ievents.NewGame);
            EventExitHeader += new exitHeader(ievents.ExitHeader);
            EventEnterVariation -= new enterVariation(ievents.EnterVariation);
            EventExitVariation -= new exitVariation(ievents.ExitVariation);
            EventStarting += new starting(ievents.Starting);
            EventFinished -= new finished(ievents.Finished);
            EventTagParsed -= new tagParsed(ievents.TagParsed);
            EventNagParsed -= new nagParsed(ievents.NagParsed);
            EventMoveParsed -= new moveParsed(ievents.StepParsed);
            EventCommentParsed -= new commentParsed(ievents.CommentParsed);
            EventendMarkerParsed -= new endMarkerParsed(ievents.EndMarker);
        }

        /// <summary>
        /// Responsible for the main driver loop of the parser.  Here we read
        /// in the PGN file and produce events for the listening parties.
        /// </summary>
        public void Parse()
        {
            StringBuilder builder = new StringBuilder(1024);
            StreamReader reader = null;

            if (EventStarting != null)
                EventStarting(this);
            try
            {
                Encoding fileEncoding = TextFileEncoding.GetEncoding(_filename);
                reader = new StreamReader(_filename, fileEncoding);

                while (!reader.EndOfStream)
                {
                    #region while
                    char aChar = (char)reader.Read();
                    switch (aChar)
                    {
                        #region case
                        case '\n':
                            _Data = builder.ToString();
                            builder.Length = 0;
                            if (_Data.Length > 0)
                            {
                                if (_State != Enums.PGNReaderState.Comment && Regex.IsMatch(_Data, "^\\["))
                                {
                                    if (_NextGame == false)
                                    {
                                        CallEvent(_State);

                                        _NextGame = true;
                                        if (EventNewGame != null)
                                        {
                                            EventNewGame(this);
                                        }
                                    }
                                    _State = Enums.PGNReaderState.Header;
                                    ParseTag(_Data);
                                    _Value.Length = 0;
                                }
                                else
                                {
                                    if (_NextGame)
                                    {
                                        _NextGame = false;
                                        if (EventExitHeader != null)
                                        {
                                            EventExitHeader(this);
                                        }
                                    }
                                    ParseDetail(_Data);
                                }
                            }
                            break;
                        case '\r':
                            break;
                        default:
                            builder.Append(aChar);
                            if (reader.EndOfStream)
                            {
                                goto case '\n';
                            }
                            break;
                        #endregion
                    }
                    #endregion

                }//while

                CallEvent(_State);
            }
            catch (ApplicationException)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
            }
            if (EventFinished != null)
                EventFinished(this);
        }

        /// <summary>
        /// Parses out the PGN tag and value from the game header.
        /// </summary>
        /// <param name="line"></param>
        public void ParseTag(string line)
        {
            int nleft = line.IndexOf('"');
            int nright = line.LastIndexOf('"');

            Match regMatch = _Regex.Match(line);
            if (regMatch.Groups.Count == 3)
            {
                // Call the events with the tag and tag value.
                if (EventTagParsed != null)
                {
                    _Tag = regMatch.Groups[1].Value;
                    _Value.Length = 0;
                    _Value.Append(regMatch.Groups[2].Value);
                    EventTagParsed(this);
                }
            }
        }

        /// <summary>
        /// Handles the parsing of the actual game notation of the PGN file.
        /// </summary>
        /// <param name="line"></param>
        public void ParseDetail(string line)
        {
            foreach (char aChar in line)
            {
                // Handle any special processing of our text.
                switch (_State)
                {
                    case Enums.PGNReaderState.Comment:
                        if (aChar == '}')
                        {
                            CallEvent(_State);
                        }
                        else
                            _Value.Append(aChar);
                        break;

                    case Enums.PGNReaderState.Nags:
                        if (aChar >= '0' && aChar <= '9')
                            _Value.Append(aChar);
                        else
                        {
                            CallEvent(_State);
                            HandleChar(aChar);
                        }
                        break;
                    case Enums.PGNReaderState.Color:
                        if (aChar == '.')
                        {
                            _PeriodCount++;
                        }
                        else
                        {
                            _Value.Length = 0;
                            if (_PeriodCount == 1)
                            {
                                _State = Enums.PGNReaderState.White;
                            }
                            else if (_PeriodCount > 1)
                            {
                                _State = Enums.PGNReaderState.Black;
                            }
                            HandleChar(aChar);
                            _PeriodCount = 0;
                        }
                        break;

                    default:
                        HandleChar(aChar);
                        break;
                }
            }
            // Ensure we add a space between comment lines that are broken appart.
            if (_State == Enums.PGNReaderState.Comment)
                _Value.Append(' ');
            else
                CallEvent(_State);
        }

        /// <summary>
        /// Handles individual charaters which may signal a change in the
        /// parser's state.
        /// </summary>
        /// <param name="aChar"></param>
        private void HandleChar(char aChar)
        {
            switch (aChar)
            {
                case '{':
                    CallEvent(_State);
                    _PrevState = _State;
                    _State = Enums.PGNReaderState.Comment;
                    break;
                case '(':
                    if (EventEnterVariation != null)
                        EventEnterVariation(this);
                    _Value.Length = 0;
                    _SaveState.Push(_State);
                    break;
                case ')':
                    if (EventExitVariation != null)
                        EventExitVariation(this);
                    _Value.Length = 0;
                    _State = (Enums.PGNReaderState)_SaveState.Pop();
                    break;
                case ' ':
                    // Only if we have some data do we want to fire an event.
                    CallEvent(_State);
                    break;
                case '.':
                    // We may have come across 6. e4 6... d5 as in our example data.
                    _State = Enums.PGNReaderState.Number;
                    CallEvent(_State);
                    _PeriodCount = 1;
                    break;
                case '$':
                    CallEvent(_State);
                    _PrevState = _State;
                    _State = Enums.PGNReaderState.Nags;
                    break;
                case '!':
                case '?':
                    if (_State != Enums.PGNReaderState.ConvertNag)
                    {
                        CallEvent(_State);
                        _PrevState = _State;
                        _State = Enums.PGNReaderState.ConvertNag;
                    }
                    _Value.Append(aChar);
                    break;
                case '-':
                    if (_State != Enums.PGNReaderState.EndMarker && _Value.Length >= 1)
                    {
                        if ("012".IndexOf(_Value[_Value.Length - 1]) >= 0)
                            _State = Enums.PGNReaderState.EndMarker;
                    }
                    _Value.Append(aChar);
                    break;
                case '*':
                    _State = Enums.PGNReaderState.EndMarker;
                    _Value.Append(aChar);
                    break;
                case '\t':
                    break;
                default:
                    _Value.Append(aChar);
                    break;
            }
        }

        /// <summary>
        /// Calls the correct event based on the parsers state.
        /// </summary>
        /// <param name="state"></param>
        private void CallEvent(Enums.PGNReaderState state)
        {
            if (_Value.Length > 0)
            {
                switch (state)
                {
                    case Enums.PGNReaderState.Comment:
                        if (EventCommentParsed != null)
                            EventCommentParsed(this);
                        _State = _PrevState;
                        break;
                    case Enums.PGNReaderState.Nags:
                        if (EventNagParsed != null)
                            EventNagParsed(this);
                        _State = _PrevState;
                        break;
                    case Enums.PGNReaderState.ConvertNag:
                        string nag = _Value.ToString();
                        _Value.Length = 0;
                        if (nag.Equals("!"))
                            _Value.Append('1');
                        else if (nag.Equals("?"))
                            _Value.Append('2');
                        else if (nag.Equals("!!"))
                            _Value.Append('3');
                        else if (nag.Equals("??"))
                            _Value.Append('4');
                        else if (nag.Equals("!?"))
                            _Value.Append('5');
                        else if (nag.Equals("?!"))
                            _Value.Append('6');
                        else
                            _Value.Append('0');
                        if (EventNagParsed != null)
                            EventNagParsed(this);
                        _State = _PrevState;
                        break;
                    case Enums.PGNReaderState.Number:
                        if (EventMoveParsed != null)
                            EventMoveParsed(this);
                        _State = Enums.PGNReaderState.Color;
                        break;
                    case Enums.PGNReaderState.White:
                        if (EventMoveParsed != null)
                            EventMoveParsed(this);
                        _State = Enums.PGNReaderState.Black;
                        break;
                    case Enums.PGNReaderState.Black:
                        if (EventMoveParsed != null)
                            EventMoveParsed(this);
                        _State = Enums.PGNReaderState.Number;
                        break;
                    case Enums.PGNReaderState.EndMarker:
                        if (EventendMarkerParsed != null)
                            EventendMarkerParsed(this);
                        _State = Enums.PGNReaderState.Header;
                        break;
                }
            }
            // Always clear out our data as the handler should have used this value during the event.
            _Value.Length = 0;
        }

    }
}
