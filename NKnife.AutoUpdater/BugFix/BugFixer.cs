using System;
using System.Collections.Generic;

namespace NKnife.AutoUpdater.BugFix
{
    class BugFixer
    {
        /// <summary>老版本的自动更新器，会有参数中的路径中的空格解析错误的Bug，故在此做修理
        /// </summary>
        /// <param name="args"></param>
        /// <param name="list"></param>
        public static void Reset(string[] args, List<string> list)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                if (String.IsNullOrWhiteSpace(arg))
                {
                    continue;
                }
                if (arg[0].Equals('-'))
                {
                    list.Add(arg);
                    list.TrimExcess();
                }
                else
                {
                    if (i > 0)
                    {
                        string last = list[list.Count - 1];
                        list[list.Count - 1] = String.Format("{0} {1}", last, arg);
                    }
                }
            }
        }
    }
}
