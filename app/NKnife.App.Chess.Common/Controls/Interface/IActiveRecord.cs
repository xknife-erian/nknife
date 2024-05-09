using NKnife.Chesses.Common;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Record;

namespace NKnife.Chesses.Controls.Interface
{
    public interface IActiveRecord
    {
        Record ActiveRecord { get; }
        int CurrChessStepPair { get; }
        Enums.GameSide CurrGameSide { get; }
        Step GetStep();
    }
}
