using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Gean.Module.Chess;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Interface;
using NKnife.Files;

namespace NKnife.Chesses.Common.Record.PGN
{
    /// <summary>
    ///     Provides the parsing of the PGN standard game notation files
    ///     as defined by the standard.
    /// </summary>
    public class PgnReader : IPgnReader
    {
        #region  ===== delegate -> event =====

        public delegate void CommentParsed(IPgnReader iParser);

        public delegate void EndMarkerParsed(IPgnReader iParser);

        public delegate void EnterVariation(IPgnReader iParser);

        public delegate void ExitHeader(IPgnReader iParser);

        public delegate void ExitVariation(IPgnReader iParser);

        public delegate void Finished(IPgnReader iParser);

        public delegate void MoveParsed(IPgnReader iParser);

        public delegate void NagParsed(IPgnReader iParser);

        public delegate void NewGame(IPgnReader iParser);

        public delegate void Starting(IPgnReader iParser);

        public delegate void TagParsed(IPgnReader iParser);

        public event NewGame EventNewGame;

        public event ExitHeader EventExitHeader;

        public event EnterVariation EventEnterVariation;

        public event ExitVariation EventExitVariation;

        public event Starting EventStarting;

        public event Finished EventFinished;

        public event TagParsed EventTagParsed;

        public event NagParsed EventNagParsed;

        public event MoveParsed EventMoveParsed;

        public event CommentParsed EventCommentParsed;

        public event EndMarkerParsed EventendMarkerParsed;

        #endregion

        private readonly Regex _Regex;

        /// <summary>
        ///     Saves the state of the parser as it enter into a variation.
        /// </summary>
        private readonly Stack _SaveState;

        private readonly StringBuilder _Value;
        private string _Data;
        private bool _NextGame;
        private int _PeriodCount;

        private Enums.PGNReaderState _PrevState;

        /// <summary>
        ///     Constructor the initializes our parser.
        /// </summary>
        public PgnReader()
        {
            _Regex = new Regex("^\\[([A-Za-z]*) \"(.*)\"", RegexOptions.Compiled);
            _Value = new StringBuilder();
            State = Enums.PGNReaderState.Header;
            _SaveState = new Stack();
            _PeriodCount = 0;
        }

        /// <summary>
        ///     Allows access to the current state of the parser
        ///     when an event has been fired.
        /// </summary>
        public Enums.PGNReaderState State { get; set; }

        /// <summary>
        ///     Contains the PGN tag information.
        /// </summary>
        public string Tag { get; private set; }

        /// <summary>
        ///     Contains the values currently parsed, normally this
        ///     is accessed from the event listeners as the parses
        ///     signals it has found something.
        /// </summary>
        public string Value
        {
            get { return _Value.ToString(); }
        }

        /// <summary>
        ///     File name to open and parse.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        ///     Adds a specific listner to the events fired by the parsing
        ///     of the PGN data.
        /// </summary>
        /// <param name="ievents"></param>
        public void AddEvents(IPgnReaderEvents ievents)
        {
            EventNewGame += ievents.NewGame;
            EventExitHeader += ievents.ExitHeader;
            EventEnterVariation += ievents.EnterVariation;
            EventExitVariation += ievents.ExitVariation;
            EventStarting += ievents.Starting;
            EventFinished += ievents.Finished;
            EventTagParsed += ievents.TagParsed;
            EventNagParsed += ievents.NagParsed;
            EventMoveParsed += ievents.StepParsed;
            EventCommentParsed += ievents.CommentParsed;
            EventendMarkerParsed += ievents.EndMarker;
        }

        /// <summary>
        ///     Remove a specific listner from the events fired by the parsing
        ///     of the PGN data.
        /// </summary>
        /// <param name="ievents"></param>
        public void RemoveEvents(IPgnReaderEvents ievents)
        {
            EventNewGame -= ievents.NewGame;
            EventExitHeader += ievents.ExitHeader;
            EventEnterVariation -= ievents.EnterVariation;
            EventExitVariation -= ievents.ExitVariation;
            EventStarting += ievents.Starting;
            EventFinished -= ievents.Finished;
            EventTagParsed -= ievents.TagParsed;
            EventNagParsed -= ievents.NagParsed;
            EventMoveParsed -= ievents.StepParsed;
            EventCommentParsed -= ievents.CommentParsed;
            EventendMarkerParsed -= ievents.EndMarker;
        }

        /// <summary>
        ///     Responsible for the main driver loop of the parser.  Here we read
        ///     in the PGN file and produce events for the listening parties.
        /// </summary>
        public void Parse()
        {
            var builder = new StringBuilder(1024);
            StreamReader reader = null;

            if (EventStarting != null)
                EventStarting(this);
            try
            {
                Encoding fileEncoding = TextFileEncoding.GetEncoding(Filename);
                reader = new StreamReader(Filename, fileEncoding);

                while (!reader.EndOfStream)
                {
                    #region while

                    var aChar = (char) reader.Read();
                    switch (aChar)
                    {
                            #region case

                        case '\n':
                            _Data = builder.ToString();
                            builder.Length = 0;
                            if (_Data.Length > 0)
                            {
                                if (State != Enums.PGNReaderState.Comment && Regex.IsMatch(_Data, "^\\["))
                                {
                                    if (_NextGame == false)
                                    {
                                        CallEvent(State);

                                        _NextGame = true;
                                        if (EventNewGame != null)
                                        {
                                            EventNewGame(this);
                                        }
                                    }
                                    State = Enums.PGNReaderState.Header;
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
                } //while

                CallEvent(State);
            }
            catch (ApplicationException e)
            {
                Debug.Fail(string.Format("µ÷ÊÔÆÚÒì³£"), e.Message);
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
        ///     Parses out the PGN tag and value from the game header.
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
                    Tag = regMatch.Groups[1].Value;
                    _Value.Length = 0;
                    _Value.Append(regMatch.Groups[2].Value);
                    EventTagParsed(this);
                }
            }
        }

        /// <summary>
        ///     Handles the parsing of the actual game notation of the PGN file.
        /// </summary>
        /// <param name="line"></param>
        public void ParseDetail(string line)
        {
            foreach (char aChar in line)
            {
                // Handle any special processing of our text.
                switch (State)
                {
                    case Enums.PGNReaderState.Comment:
                        if (aChar == '}')
                        {
                            CallEvent(State);
                        }
                        else
                            _Value.Append(aChar);
                        break;

                    case Enums.PGNReaderState.Nags:
                        if (aChar >= '0' && aChar <= '9')
                            _Value.Append(aChar);
                        else
                        {
                            CallEvent(State);
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
                                State = Enums.PGNReaderState.White;
                            }
                            else if (_PeriodCount > 1)
                            {
                                State = Enums.PGNReaderState.Black;
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
            if (State == Enums.PGNReaderState.Comment)
                _Value.Append(' ');
            else
                CallEvent(State);
        }

        /// <summary>
        ///     Handles individual charaters which may signal a change in the
        ///     parser's state.
        /// </summary>
        /// <param name="aChar"></param>
        private void HandleChar(char aChar)
        {
            switch (aChar)
            {
                case '{':
                    CallEvent(State);
                    _PrevState = State;
                    State = Enums.PGNReaderState.Comment;
                    break;
                case '(':
                    if (EventEnterVariation != null)
                        EventEnterVariation(this);
                    _Value.Length = 0;
                    _SaveState.Push(State);
                    break;
                case ')':
                    if (EventExitVariation != null)
                        EventExitVariation(this);
                    _Value.Length = 0;
                    State = (Enums.PGNReaderState) _SaveState.Pop();
                    break;
                case ' ':
                    // Only if we have some data do we want to fire an event.
                    CallEvent(State);
                    break;
                case '.':
                    // We may have come across 6. e4 6... d5 as in our example data.
                    State = Enums.PGNReaderState.Number;
                    CallEvent(State);
                    _PeriodCount = 1;
                    break;
                case '$':
                    CallEvent(State);
                    _PrevState = State;
                    State = Enums.PGNReaderState.Nags;
                    break;
                case '!':
                case '?':
                    if (State != Enums.PGNReaderState.ConvertNag)
                    {
                        CallEvent(State);
                        _PrevState = State;
                        State = Enums.PGNReaderState.ConvertNag;
                    }
                    _Value.Append(aChar);
                    break;
                case '-':
                    if (State != Enums.PGNReaderState.EndMarker && _Value.Length >= 1)
                    {
                        if ("012".IndexOf(_Value[_Value.Length - 1]) >= 0)
                            State = Enums.PGNReaderState.EndMarker;
                    }
                    _Value.Append(aChar);
                    break;
                case '*':
                    State = Enums.PGNReaderState.EndMarker;
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
        ///     Calls the correct event based on the parsers state.
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
                        State = _PrevState;
                        break;
                    case Enums.PGNReaderState.Nags:
                        if (EventNagParsed != null)
                            EventNagParsed(this);
                        State = _PrevState;
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
                        State = _PrevState;
                        break;
                    case Enums.PGNReaderState.Number:
                        if (EventMoveParsed != null)
                            EventMoveParsed(this);
                        State = Enums.PGNReaderState.Color;
                        break;
                    case Enums.PGNReaderState.White:
                        if (EventMoveParsed != null)
                            EventMoveParsed(this);
                        State = Enums.PGNReaderState.Black;
                        break;
                    case Enums.PGNReaderState.Black:
                        if (EventMoveParsed != null)
                            EventMoveParsed(this);
                        State = Enums.PGNReaderState.Number;
                        break;
                    case Enums.PGNReaderState.EndMarker:
                        if (EventendMarkerParsed != null)
                            EventendMarkerParsed(this);
                        State = Enums.PGNReaderState.Header;
                        break;
                }
            }
            // Always clear out our data as the handler should have used this value during the event.
            _Value.Length = 0;
        }
    }
}