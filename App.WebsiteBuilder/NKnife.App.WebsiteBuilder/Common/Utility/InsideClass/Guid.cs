namespace Jeelu
{
    static public partial class Utility
    {
        static public class Guid
        {
            static public string NewGuid()
            {
                return System.Guid.NewGuid().ToString("N");
            }
        }
    }
}