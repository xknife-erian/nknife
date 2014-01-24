using System.Data;

namespace NKnife.Data.Interface
{
    public interface IDataSetProcess<out T>
    {
        T Process(DataSet data);
    }
}
