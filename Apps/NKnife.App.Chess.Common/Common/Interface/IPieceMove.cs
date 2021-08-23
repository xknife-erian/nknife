using Gean.Module.Chess;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Pieces;

namespace NKnife.Chesses.Common.Interface
{
    /// <summary>
    /// 一个描述棋子移动时方法的接口
    /// </summary>
    public interface IPieceMove
    {
        void PieceMoveIn(PositionPair step);
        event PieceMoveIn PieceMoveInEvent;

        void PieceMoveOut(Position pos);
        event PieceMoveOut PieceMoveOutEvent;
    }
    /// <summary>
    /// 一个描述棋子移动入某棋格的委托类型
    /// </summary>
    /// <param name="piece">棋子</param>
    /// <param name="actions">移动所产生的动作类型</param>
    /// <param name="pair">移动时源、目标棋格对</param>
    public delegate void PieceMoveIn(Piece piece, Enums.ActionCollection actions, PositionPair pair);
    /// <summary>
    /// 一个描述棋子移出棋局（被杀死）的委托类型
    /// </summary>
    /// <param name="piece">棋子</param>
    /// <param name="pos">棋子被杀死时的所在棋格</param>
    public delegate void PieceMoveOut(Piece piece, Position pos);
}
