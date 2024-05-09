namespace NKnife.Chesses.Controls.Interface
{
    public interface IRecordPlay
    {
        IActiveRecord ActiveRecord { get; }
        IChessBoard ChessBoard { get; }
        void Forward();
        void Back();
        void FastForward();
        void FastBack();
        void ForwardEnd();
        void BackStart();
        void Skip(int number);
        void AutoForward();
    }
}
