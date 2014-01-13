using System.Data;

namespace Gean.Data.Interface
{
    public interface IDataSetProcess<out T>
    {
        T Process(DataSet data);
    }
}
