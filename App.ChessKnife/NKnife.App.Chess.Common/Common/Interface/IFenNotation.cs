using Gean.Module.Chess;
using NKnife.Chesses.Common.Base;

namespace NKnife.Chesses.Common.Interface
{
    public interface IFenNotation
    {
        #region From FEN

        /// <summary>
        /// 1) 棋子位置数值区域(Piece placement data) 
        /// </summary>
        string PiecePlacementData { get; }
        /// <summary>
        /// 2) 轮走棋方(Active color)
        /// </summary>
        Enums.GameSide GameSide { get; }
        /// <summary>
        /// 3) 易位可行性(Castling availability)(1.白方王侧)
        /// </summary>
        bool WhiteKingCastlingAvailability { get; }
        /// <summary>
        /// 3) 易位可行性(Castling availability)(2.白方后侧)
        /// </summary>
        bool WhiteQueenCastlingAvailability { get; }
        /// <summary>
        /// 3) 易位可行性(Castling availability)(3.黑方王侧)
        /// </summary>
        bool BlackKingCastlingAvailability { get; }
        /// <summary>
        /// 3) 易位可行性(Castling availability)(4.黑方后侧)
        /// </summary>
        bool BlackQueenCastlingAvailability { get; }
        /// <summary>
        /// 4) 吃过路兵目标格(En passant target square)
        /// </summary>
        Position EnPassantTargetPosition { get; }
        /// <summary>
        /// 5) 半回合计数(Halfmove clock)。用一个非负数表示自从上一次动兵或吃子之后目前走了的半回合数。这个是为了适应50步和棋规则而定。
        /// </summary>
        int HalfMoveClock { get; }
        /// <summary>
        /// 6) 回合数(Fullmove number)。当前要进行到的回合数。不管白还是黑，第一步时总是以1表示，以后黑方每走一步数字就加1。
        /// </summary>
        int FullMoveNumber { get; }

        #endregion
    }
}
