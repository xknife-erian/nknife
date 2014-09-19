namespace Jeelu
{
    static public partial class Utility
    {
        /// <summary>
        /// 比较两个数组
        /// </summary>
        /// <returns>真或假,bool值</returns>
        static public bool IsAllEquals<T>(T[] arr1, T[] arr2)
        {
            if (object.ReferenceEquals(arr1, arr2))
            {
                return true;
            }

            if (arr1 == null || arr2 == null)
            {
                return false;
            }

            if (arr1.Length != arr2.Length)
            {
                return false;
            }

            for (int i = 0; i < arr1.Length; i++)
            {
                if (!arr1[i].Equals(arr2[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
