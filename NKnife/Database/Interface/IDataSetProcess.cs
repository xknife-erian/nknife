using System.Data;

namespace NKnife.Database.Interface
{
    public interface IDataSetProcess<out T>
    {
        T Process(DataSet data);
    }
}
