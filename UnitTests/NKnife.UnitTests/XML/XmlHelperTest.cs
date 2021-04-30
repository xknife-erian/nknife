using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using FluentAssertions;
using NKnife.XML;
using Xunit;

namespace NKnife.UnitTests.XML
{
    public class XmlHelperTest
    {
        [Fact]
        public void PathSeparatorCharTest()
        {
            var a = Path.AltDirectorySeparatorChar;
            var b = Path.DirectorySeparatorChar;
            var c = Path.PathSeparator;
            var d = Path.VolumeSeparatorChar;

            a.Should().Be('/');
            b.Should().Be('\\');
            c.Should().Be(';');
            d.Should().Be(':');
        }

        [Fact]
        public void CreateNewDocumentTest()
        {
            var path = $"d:\\---{Guid.NewGuid().ToString()}\\---abc\\---xyz\\---mn\\";
            var xmlFile = $"new---{Guid.NewGuid().ToString()}.xml";
            var xmlFileFullPath = Path.Combine(path, xmlFile);
            XmlHelper.CreateNewDocument(xmlFileFullPath);
            File.Exists(xmlFileFullPath).Should().BeTrue();
            var xml = new XmlDocument();
            xml.Load(xmlFileFullPath);
            xml.DocumentElement.Should().NotBeNull();
            xml.DocumentElement.LocalName.Should().Be("root");
            Directory.Delete(path, true);
        }
    }
}
