using System.Data;

namespace NKnife.Data.Interface
{
    public interface IDataReaderProcess<T>
    {
        T Process(IDataReader data);
    }
}
