using NKnife.Chesses.Common.Base;
using NKnife.Interface;

namespace NKnife.Chesses.Common.Interface
{
    /// <summary>
    /// 描述一局棋的当前局面
    /// </summary>
    public interface ISituation : IFenNotation, IParse, IGenerator
    {
        /// <summary>
        /// 返回指定位置的棋格是否含有棋子
        /// </summary>
        /// <param name="dot">指定位置</param>
        bool ContainsPiece(int dot);

        /// <summary>
        /// 尝试返回指定位置的棋格是否含有棋子，如含有棋子则返回该棋子
        /// </summary>
        /// <param name="dot">指定位置的棋格</param>
        /// <param name="pieceType">该棋格中的棋子，如有则返回，如无则返回"Enums.PieceType.None"</param>
        /// <returns></returns>
        bool TryGetPiece(int dot, out Enums.PieceType pieceType);
        bool TryGetPiece(int dot, out char c);
    }
}