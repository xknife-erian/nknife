using System.Data;

namespace NKnife.Datas.Interface
{
    public interface IDataReaderProcess<out T>
    {
        T Process(IDataReader data);
    }
}
