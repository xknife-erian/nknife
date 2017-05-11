using System.Data;

namespace NKnife.Datas.Interface
{
    public interface IDataSetProcess<out T>
    {
        T Process(DataSet data);
    }
}
