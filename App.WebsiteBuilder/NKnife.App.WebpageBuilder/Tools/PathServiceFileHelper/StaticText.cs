using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.PathServiceFileHelper
{
    static public class StaticText
    {

        static public readonly string BeginCode = @"using System.Xml;
using System.IO;
using System.Windows.Forms;

///���ļ������������Զ����ɣ������ֶ����������
namespace Jeelu.SimplusD
{
    static public class PathService
    {
        static string _softwarePath;
        static public string SoftwarePath{ get { return _softwarePath; } }
        static public string ProjectPath { get; set; }

        static public void Initialize(string softwarePath)
        {
            _softwarePath = softwarePath;
            using (XmlReader reader = XmlReader.Create(Path.Combine(softwarePath, ""Path.xml"")))
            {
                while (reader.Read())
                {
                    if (reader.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    switch (reader.Name)
                    {
                        case ""directory"":
                        case ""file"":
                            {
                                switch (reader.GetAttribute(""name""))
                                {";

        static public readonly string BeginCode2 = @"default:
                                        break;
                                }
                                break;
                            }
                        default:
                            break;
                    }
                }
            }

        }
        
        ";
        static public readonly string EndCode = @"
    }
}";
    }
}
