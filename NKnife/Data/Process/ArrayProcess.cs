using System.Data;
using Gean.Data.Interface;

namespace Gean.Data.Process
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
