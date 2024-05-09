namespace Abc.ClassLibrary3.Xyz
{
    public class HelloWorld : Hello, IWorld
    {
        #region Implementation of IWorld

        public string Qwe { get; }

        #endregion

        public HelloWorld(string a, string b, string c) : base(a, b, c)
        {
        }

        private HelloWorld()
        {
        }

        public static HelloWorld Me => new HelloWorld();

        public static HelloWorld Build(string a, string b, string c)
        {
            return new HelloWorld(a, b,c);
        }
    }
}