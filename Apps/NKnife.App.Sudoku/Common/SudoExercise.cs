using System;
using System.Text;
using System.Xml;

namespace NKnife.App.Sudoku.Common
{
    public class SudoExercise
    {
        public SudoExercise(string createrEmail, string playerEmail, string doExercise)
        {
            this.ID = Guid.NewGuid().ToString("N");
            this.CreaterEmail = createrEmail;
            this.PlayerEmail = playerEmail;
            this.SingleExercise = doExercise;
            int[] solution = SuDoTable.Solve(doExercise);
            StringBuilder sb = new StringBuilder();
            foreach (var item in solution)
            {
                sb.Append(item);
            }
            this.Solution = sb.ToString();
            this.SolveDuration = DateTime.Parse("00:00:00");
            this.SolveTime = 0;
        }

        private SudoExercise() { }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public string ID { get; set; }

        public string CreaterEmail { get; set; }
        public string PlayerEmail { get; set; }

        /// <summary>
        /// 获取或设置一道数独题目
        /// </summary>
        /// <value>The single exercise.</value>
        public string SingleExercise { get; set; }

        /// <summary>
        /// 获取或设置该数独的答案
        /// </summary>
        /// <value>The solution.</value>
        public string Solution { get; set; }
        /// <summary>
        /// 获取或设置最终的解题时间
        /// </summary>
        /// <value>The run time.</value>
        public DateTime SolveDuration { get; set; }
        /// <summary>
        /// 获取或设置已解题次数
        /// </summary>
        /// <value>The solve time.</value>
        public int SolveTime { get; set; }

        internal static SudoExercise Parse(XmlElement element)
        {
            SudoExercise exercise = new SudoExercise();
            exercise.ID             = XmlHelper.GetElementByName(element, "ID").InnerText;
            exercise.CreaterEmail   = XmlHelper.GetElementByName(element, "CreaterEmail").InnerText;
            exercise.PlayerEmail    = XmlHelper.GetElementByName(element, "PlayerEmail").InnerText;
            exercise.SingleExercise = XmlHelper.GetElementByName(element, "Exercise").InnerText;
            exercise.Solution       = XmlHelper.GetElementByName(element, "Solution").InnerText;
            string durationString   = XmlHelper.GetElementByName(element, "SolveDuration").InnerText;
            if (string.IsNullOrEmpty(durationString) || durationString == "0")
            {
                exercise.SolveDuration = DateTime.Parse(durationString);
            }
            exercise.SolveTime      = int.Parse(XmlHelper.GetElementByName(element, "SolveTime").InnerText);
            return exercise;
        }

        public override bool Equals(object obj)
        {
            if (string.Equals(ID,((SudoExercise)obj).ID))
            {
                return string.Equals(this.SingleExercise, ((SudoExercise)obj).SingleExercise);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return unchecked(27 * SingleExercise.GetHashCode() + Solution.GetHashCode());
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            return sb.Append("Exercise: ").Append(SingleExercise).Append("; \r\nSolution: ").Append(Solution).ToString();
        }
    }
}
