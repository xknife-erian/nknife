using System.Data;
using NKnife.Database.Interface;

namespace NKnife.Database.Process
{
    public class ArrayProcess : IDataReaderProcess<object[]>
    {
        public object[] Process(IDataReader data)
        {
            while (data.Read())
            {
                var objs = new object[data.FieldCount];
                data.GetValues(objs);
                return objs;
            }
            return null;
        }
    }
}
