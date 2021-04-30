using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NKnife.Base;

namespace NKnife.Chesses.Common.Base
{
    internal static class Servicer
    {
        static Servicer()
        {
            var regexs = new List<Tuple<AsStep, Regex>>
            {
                new Tuple<AsStep, Regex>(AsStep.As_e4, new Regex(@"^[a-h][1-8]$", RegexOptions.Compiled)),
                new Tuple<AsStep, Regex>(AsStep.As_Rd7, new Regex(@"^[RNBQK][a-h][1-8]$", RegexOptions.Compiled)),
                new Tuple<AsStep, Regex>(AsStep.As_Rxa2, new Regex(@"^[RNBQK]x[a-h][1-8]$", RegexOptions.Compiled)),
                new Tuple<AsStep, Regex>(AsStep.As_Rbe1, new Regex(@"^[RNBQK][a-h][a-h][1-8]$", RegexOptions.Compiled)),
                new Tuple<AsStep, Regex>(AsStep.As_N1c3, new Regex(@"^[RNBQK][1-8][a-h][1-8]$", RegexOptions.Compiled)),
                new Tuple<AsStep, Regex>(AsStep.As_hxg6, new Regex(@"^[a-h]x[a-h][1-8]$", RegexOptions.Compiled)),
                new Tuple<AsStep, Regex>(AsStep.As_O_O, new Regex(@"^[O]-[O]$", RegexOptions.Compiled)),
                new Tuple<AsStep, Regex>(AsStep.As_O_O_O, new Regex(@"^[O]-[O]-[O]$", RegexOptions.Compiled)),
                new Tuple<AsStep, Regex>(AsStep.As_Ngxf6, new Regex(@"^[RNBQK][a-h]x[a-h][1-8]$", RegexOptions.Compiled)),
                new Tuple<AsStep, Regex>(AsStep.As_R8xf5, new Regex(@"^[RNBQK][1-8]x[a-h][1-8]$", RegexOptions.Compiled)),
                new Tuple<AsStep, Regex>(AsStep.As_e8_Q, new Regex(@"^[a-h][1-8]=[RNBQ]$", RegexOptions.Compiled)),
                new Tuple<AsStep, Regex>(AsStep.As_exf8_Q, new Regex(@"^[a-h]x[a-h][1-8]=[RNBQ]$", RegexOptions.Compiled))
            };
            StepRegex = regexs.ToArray();
            Flags = FlagCollection.Single();
        }

        public enum AsStep
        {
            As_e4,
            As_Rd7,
            As_Rxa2,
            As_Rbe1,
            As_N1c3,
            As_hxg6,
            As_Ngxf6,
            As_R8xf5,
            As_e8_Q,
            As_exf8_Q,
            As_O_O,
            As_O_O_O,
        }
        public static Tuple<AsStep, Regex>[] StepRegex;
        public static FlagCollection Flags { get; private set; }

        #region class FlagCollection

        public class FlagCollection : IEnumerable<string>
        {
            private static Dictionary<string, string> _flags = new Dictionary<string, string>();
            private static FlagCollection _flagCollection = null;
            internal static FlagCollection Single()
            {
                if (_flagCollection == null)
                {
                    _flagCollection = new FlagCollection();
                }
                return _flagCollection;
            }

            private FlagCollection()
            {
                if (_flags.Count <= 0)
                {
                    _flags.Add("Move", this.Move);
                    _flags.Add("Capture", this.Capture);
                    _flags.Add("Check", this.Check);
                    _flags.Add("DoubleCheck", this.DoubleCheck);
                    _flags.Add("Checkmate1", this.Checkmate1);
                    _flags.Add("Checkmate2", this.Checkmate2);
                    _flags.Add("EnPassant", this.EnPassant);
                    _flags.Add("Favorable", this.Favorable);
                    _flags.Add("FavorablePro", this.FavorablePro);
                    _flags.Add("Misestimate", this.Misestimate);
                    _flags.Add("MisestimatePro", this.MisestimatePro);
                    _flags.Add("UnknownConsequences", this.UnknownConsequences);
                }
            }

            /* 简易评论标记符
              在目标格坐标前注明：[-] 走子； [:] or [x] 吃子
              在目标格坐标后注明：[+] 将军； [++] 双将；[x]or[#] 将死；[e.p.] 吃过路兵(en passant)；[棋子名] 升变
            */

            /// <summary>
            /// 目标格坐标前：走子
            /// </summary>
            public string Move { get { return "-"; } }
            /// <summary>
            /// 目标格坐标前：吃子
            /// </summary>
            public string Capture { get { return "x"; } }
            /// <summary>
            /// 目标格坐标后：将军
            /// </summary>
            public string Check { get { return "+"; } }
            /// <summary>
            /// 目标格坐标后：双将
            /// </summary>
            public string DoubleCheck { get { return "++"; } }
            /// <summary>
            /// 目标格坐标后：将死
            /// </summary>
            public string Checkmate1 { get { return "x"; } }
            /// <summary>
            /// 目标格坐标后：将死
            /// </summary>
            public string Checkmate2 { get { return "#"; } }
            /// <summary>
            /// 目标格坐标后：吃过路兵
            /// </summary>
            public string EnPassant { get { return "e.p."; } }

            /// <summary>
            /// 好棋。有利的棋招
            /// </summary>
            public string Favorable { get { return "!"; } }
            /// <summary>
            /// 好棋。非常有利的棋招
            /// </summary>
            public string FavorablePro { get { return "!!"; } }
            /// <summary>
            /// 昏招
            /// </summary>
            public string Misestimate { get { return "?"; } }
            /// <summary>
            /// 严重的昏招
            /// </summary>
            public string MisestimatePro { get { return "??"; } }
            /// <summary>
            /// 后果不明的棋招
            /// </summary>
            public string UnknownConsequences { get { return "!?"; } }

            #region IEnumerable<string> 成员

            public IEnumerator<string> GetEnumerator()
            {
                return _flags.Values.GetEnumerator();
            }

            #endregion

            #region IEnumerable 成员

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return _flags.Values.GetEnumerator();
            }

            #endregion
        }

        #endregion

    }
}
