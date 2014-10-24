using Gean.Module.Chess;
using NKnife.Chesses.Common.Base;

namespace NKnife.Chesses.Common.Interface
{
    /// <summary>
    /// This interface defines the minimum functionality that a game 
    /// parser must implement.  It does not have to have all the functionality
    /// present, but the interface must be delcared.
    /// <seealso>
    ///     <cref>IGameParserEvents</cref>
    /// </seealso>
    /// </summary>
    public interface IPgnReader
    {
        /// <summary>
        /// Name of the fileto parse.
        /// </summary>
        string Filename { get; set; }
        /// <summary>
        /// Show what STATE with in the parser 
        /// that is currently active.
        /// </summary>
        Enums.PGNReaderState State { get; set; }
        /// <summary>
        /// Holds any header/game information that describes attributes
        /// of the game such as: Date, Players, ELO, ECO, Ratings, and others.
        /// Will contain valid data during the IGameParserEvents.tagParsed event.
        /// </summary>
        string Tag { get; }
        /// <summary>
        /// Will contain valid data during the IGameParserEvents.tagParsed event which
        /// represents the data associated with the game tag.  Also during the following
        /// events:
        ///   IGameParserEvents.nagParsed
        ///   IGameParserEvents.moveParsed
        ///   IGameParserEvents.commentParsed
        /// </summary>
        string Value { get; }
        /// <summary>
        /// Executes the main loop for parsing out the games.
        /// </summary>
        void Parse();
        /// <summary>
        /// Used to register event handlers.
        /// </summary>
        /// <param name="ievents"></param>
        void AddEvents(IPgnReaderEvents ievents);
        /// <summary>
        /// Used to remove event handlers.
        /// </summary>
        /// <param name="ievents"></param>
        void RemoveEvents(IPgnReaderEvents ievents);
    }
}
