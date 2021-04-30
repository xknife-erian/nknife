using System.Collections.Generic;
using System.Xml;
using NKnife.App.Sudoku.Common.Enum;
using NKnife.XML;

namespace NKnife.App.Sudoku.Common
{
    public class SuDoXml : AbstractXmlDocument
    {
        internal SuDoXml(string filepath)
            : base(filepath)
        {
        }

        public void CreatDoExerciseElement(SudoDifficulty difficulty, SudoExercise exercise)
        {
            XmlDocument rootDoc = (XmlDocument)(this.BaseXmlNode);
            XmlElement dataElement = rootDoc.CreateElement("data");

            XmlElement dataChildElement;

            dataChildElement = rootDoc.CreateElement("ID");
            dataChildElement.InnerText = exercise.ID;
            dataElement.AppendChild(dataChildElement);

            dataChildElement = rootDoc.CreateElement("Exercise");
            dataChildElement.InnerText = exercise.SingleExercise;
            dataElement.AppendChild(dataChildElement);

            dataChildElement = rootDoc.CreateElement("Solution");
            dataChildElement.InnerText = exercise.Solution;
            dataElement.AppendChild(dataChildElement);

            dataChildElement = rootDoc.CreateElement("CreaterEmail");
            dataChildElement.InnerText = exercise.CreaterEmail;
            dataElement.AppendChild(dataChildElement);

            dataChildElement = rootDoc.CreateElement("PlayerEmail");
            dataChildElement.InnerText = exercise.PlayerEmail;
            dataElement.AppendChild(dataChildElement);

            dataChildElement = rootDoc.CreateElement("SolveDuration");
            dataChildElement.InnerText = exercise.SolveDuration.ToShortTimeString();
            dataElement.AppendChild(dataChildElement);

            dataChildElement = rootDoc.CreateElement("SolveTime");
            dataChildElement.InnerText = exercise.SolveTime.ToString();
            dataElement.AppendChild(dataChildElement);

            this.GetDifficultyElement(difficulty).AppendChild(dataElement);
        }

        public SudoExercise[] SelectExercises(SudoDifficulty difficulty)
        {
            List<SudoExercise> exerciseList = new List<SudoExercise>();
            XmlElement dataelement = this.GetDifficultyElement(difficulty);
            foreach (XmlNode node in dataelement.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    break;
                }
                XmlElement ele = (XmlElement)node;
                exerciseList.Add(SudoExercise.Parse(ele));
            }
            return exerciseList.ToArray();
        }

        private XmlElement GetDifficultyElement(SudoDifficulty difficulty)
        {
            return XmlHelper.GetElementByName(this.DocumentElement.SelectSingleNode("Do"), difficulty.ToString());
        }

    }

}
