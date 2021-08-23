namespace Abc.ClassLibrary3.Xyz
{
    public abstract class BaseHello : IHelloPro
    {
        public abstract void SetAbc(string a, string b, string c);

        #region Implementation of IHello

        public string Aaa { get; protected set; }
        public string Bbb { get; protected set; }
        public string Ccc { get; protected set; }

        #endregion

        #region Implementation of IHelloPro

        public string Pro { get; }

        #endregion
    }
}