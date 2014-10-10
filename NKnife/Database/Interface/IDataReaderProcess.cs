using System.Data;

namespace NKnife.Database.Interface
{
    public interface IDataReaderProcess<T>
    {
        T Process(IDataReader data);
    }
}
