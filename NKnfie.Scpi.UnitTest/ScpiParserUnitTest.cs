using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScpiKnife;

namespace NKnfie.Scpi.UnitTest
{
    /// <summary>
    /// ScpiParserUnitTest 的摘要说明
    /// </summary>
    [TestClass]
    public class ScpiParserUnitTest
    {
        #region base
        
        public ScpiParserUnitTest()
        {
        }

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext { get; set; }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        #endregion

        [TestMethod]
        public void ParseTestMethod00()//当无意义节点，或者节点中无指令列表时，抛出导常
        {
            var doc = new XmlDocument();
            doc.LoadXml("<a><b></b></a>");
            var parser = new ScpiParser();
            try
            {
                ScpiCommandList result;
                parser.TryParse(doc.DocumentElement, out result);
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(ScpiParseException), e.GetType());
            }
        }

        [TestMethod]
        public void ParseTestMethod01()//正常解析，能够返回列表,不考虑解析的内容是否正确
        {
            var xmlelement = ScpiXml.GetCommandListElement(1);
            var parser = new ScpiParser();
            ScpiCommandList commandlist;
            parser.TryParse(xmlelement, out commandlist);
            Assert.IsNotNull(commandlist);
        }

        [TestMethod]
        public void ParseTestMethod02() //解析，检验命令的数量
        {
            var xmlelement = ScpiXml.GetCommandListElement(1);
            var parser = new ScpiParser();
            ScpiCommandList commandlist;
            parser.TryParse(xmlelement, out commandlist);
            Assert.AreEqual(3, commandlist.Count);
        }

        [TestMethod]
        public void ParseTestMethod03() //解析，检验命令的数量
        {
            var xmlelement = ScpiXml.GetCommandListElement(1);
            var parser = new ScpiParser();
            ScpiCommandList commandlist;
            parser.TryParse(xmlelement, out commandlist);
            var command = commandlist.ElementAt(0);
            Assert.AreEqual("重置", command.Description);
            Assert.AreEqual("*RST", command.Command);
            Assert.AreEqual(500, command.Interval);
            Assert.AreEqual(true, command.IsStandard);
            Assert.AreEqual(false, command.IsReturn);
            Assert.AreEqual(null, command.Next);
        }                
    }
}
