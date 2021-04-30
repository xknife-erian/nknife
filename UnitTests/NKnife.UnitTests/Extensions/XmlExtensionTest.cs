using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using FluentAssertions;
using Xunit;

namespace NKnife.UnitTests.Extensions
{
    public class XmlExtensionTest
    {
        [Fact]
        public void RemoveAllElementsTest1()
        {
            var src = "<root name=\"t\">abc<a/><b/><![CDATA[CCDDAATTAA]]><c/>xyz</root>";
            var target = "<root name=\"t\">abc<![CDATA[CCDDAATTAA]]>xyz</root>";
            var xml = new XmlDocument();
            xml.LoadXml(src);
            xml.DocumentElement.RemoveAllElements();
            xml.OuterXml.Should().Be(target);
        }
    }
}
