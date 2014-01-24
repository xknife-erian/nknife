using System.Collections.Generic;

namespace System
{
    public static class IntegerExtension
    {
        /// <summary>
        /// 将指定整数转换成指定长度字节数组，不足的前补0，超过的舍弃高位字节
        /// 例如：
        /// </summary>
        /// <param name="source"></param>
        /// <param name="byteCount">字节数组的长度，等于0时无长度约束，不补0也不舍弃 </param>
        /// <returns></returns>
        public static byte[] ToBytes(this int source,int byteCount = 0)
        {
            if(source ==0) return new byte[0];
            var list = new List<byte>();
            int seed = source;
            int remain;
            do
            {
                remain = seed % 256;
                list.Add((byte)remain);
                seed = (seed - remain) / 256;

            } while (seed > 0);
            if (byteCount == 0 || byteCount == list.Count)
            {
                list.Reverse(0, list.Count);
                return list.ToArray();
            }
            if(byteCount>list.Count)
            {
                var a = byteCount - list.Count;
                for(int i=0;i<a;i++)
                {
                    list.Add(0x00);
                }
                list.Reverse(0, list.Count);
                return list.ToArray();
            }
            if(byteCount<list.Count)
            {
                var a = list.Count - byteCount;
                for (int i = 0; i < a;i++ )
                {
                    var index = list.Count -1;
                    list.RemoveAt(index);
                }
                list.Reverse(0, list.Count);
                return list.ToArray();
            }
            return new byte[0];
        }
    }
}
