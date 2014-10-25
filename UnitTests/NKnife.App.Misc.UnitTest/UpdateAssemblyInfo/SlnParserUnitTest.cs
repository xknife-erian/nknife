using Microsoft.VisualStudio.TestTools.UnitTesting;
using NKnife.App.UpdateAssemblyInfo.Common;

namespace NKnife.App.Misc.UnitTest.UpdateAssemblyInfo
{
    [TestClass]
    public class SlnParserUnitTest
    {
        [TestMethod]
        public void GetMatchValueTest1()
        {
            const string LINE = "Project(\"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}\") = \"NKnife\", \"NKnife\\NKnife.csproj\", \"{266FCF12-D8F4-402C-BD7E-FC1DF5F6A0BF}\"";
            var parser = new TestSlnParser();
            var actual = parser.GetMatchValueTest(LINE);
            Assert.AreEqual("NKnife\\NKnife.csproj", actual);
        }

        [TestMethod]
        public void GetMatchValueTest2()
        {
            const string LINE = "Project(\"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}\") = \"NKnife.App.MathsExercise\", \"App.Misc\\NKnife.App.MathsExercise\\NKnife.App.MathsExercise.csproj\", \"{3833F0B2-D74D-44D7-9358-F7EC6145DFC6}\"";
            var parser = new TestSlnParser();
            var actual = parser.GetMatchValueTest(LINE);
            Assert.AreEqual("App.Misc\\NKnife.App.MathsExercise\\NKnife.App.MathsExercise.csproj", actual);
        }

        [TestMethod]
        public void GetMatchValueTest3()
        {
            const string LINE = "Project(\"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}\") = \"NKnife.App.Ele.ResistanceValueCombiner\", \"App.Misc\\NKnife.App.Ele.ResistanceValueCombiner\\NKnife.App.Ele.ResistanceValueCombiner.csproj\", \"{260137D2-3427-49AC-B4C7-1F040937572A}\"";
            var parser = new TestSlnParser();
            var actual = parser.GetMatchValueTest(LINE);
            Assert.AreEqual("App.Misc\\NKnife.App.Ele.ResistanceValueCombiner\\NKnife.App.Ele.ResistanceValueCombiner.csproj", actual);
        }

        [TestMethod]
        public void GetMatchValueTest4()
        {
            const string LINE = "Project(\"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}\")  =  \"NKnife\",  \"NKnife\\NKnife.csproj\",  \"{266FCF12-D8F4-402C-BD7E-FC1DF5F6A0BF}\"";
            var parser = new TestSlnParser();
            var actual = parser.GetMatchValueTest(LINE);
            Assert.AreEqual("NKnife\\NKnife.csproj", actual);
        }

        [TestMethod]
        public void GetMatchValueTest5()
        {
            const string LINE = "Project(\"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}\")        =  \"NKnife\",  \"NKnife\\NKnife.csproj\",  \"{266FCF12-D8F4-402C-BD7E-FC1DF5F6A0BF}\"";
            var parser = new TestSlnParser();
            var actual = parser.GetMatchValueTest(LINE);
            Assert.AreEqual("NKnife\\NKnife.csproj", actual);
        }

        [TestMethod]
        public void GetMatchValueTest6()
        {
            const string LINE = " Project(\"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}\") = \"NKnife\", \"NKnife\\NKnife.csproj\", \"{266FCF12-D8F4-402C-BD7E-FC1DF5F6A0BF}\" ";
            var parser = new TestSlnParser();
            var actual = parser.GetMatchValueTest(LINE);
            Assert.AreEqual("NKnife\\NKnife.csproj", actual);
        }

        class TestSlnParser : SlnParser
        {
            public string GetMatchValueTest(string line)
            {
                return GetMatchValue(line);
            }
        }
    }
}
