using System.IO;
using System.Text;
using System.Xml;

namespace NKnife.XML
{
    public class XmlUtility
    {
        public static string XmlDocumentToString(ref XmlDocument doc)
        {
            var stream = new MemoryStream();
            var writer = new XmlTextWriter(stream, null) {Formatting = Formatting.Indented};
            doc.Save(writer); //转换
            
            var sr = new StreamReader(stream, Encoding.UTF8);
            stream.Position = 0;
            var xmlString = sr.ReadToEnd();
            sr.Close();
            stream.Close();

            return xmlString;
        }
    }
}
