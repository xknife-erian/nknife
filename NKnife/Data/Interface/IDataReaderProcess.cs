using System.Data;

namespace Gean.Data.Interface
{
    public interface IDataReaderProcess<T>
    {
        T Process(IDataReader data);
    }
}
