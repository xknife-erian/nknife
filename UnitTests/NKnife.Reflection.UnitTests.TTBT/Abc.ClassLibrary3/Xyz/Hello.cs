namespace Abc.ClassLibrary3.Xyz
{
    public class Hello : BaseHello
    {
        public Hello()
        {
            
        }

        public Hello(string a, string b, string c)
        {
            Aaa = a;
            Bbb = b;
            Ccc = c;
        }

        #region Overrides of BaseHello

        public override void SetAbc(string a, string b, string c)
        {
            Aaa = a;
            Bbb = b;
            Ccc = c;
        }

        #endregion
    }
}