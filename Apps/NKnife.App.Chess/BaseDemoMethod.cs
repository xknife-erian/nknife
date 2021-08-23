using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using Gean.Gui.ChessControl.Demo;
using NKnife.Chesses.Common.Record;
using NKnife.Chesses.Common.Record.PGN;

namespace Gean.Module.Chess.Demo
{
    internal class BaseDemoMethod
    {
        /// <summary>
        /// 整个Demo程序的一些静态初始化内容
        /// </summary>
        public static void Initialize()
        {
            //TODO: 在这里构建整个Demo程序的一些静态初始化内容
        }

        /// <summary>
        /// Demo程序的主窗体的OnLoad事件
        /// </summary>
        /// <param name="form"></param>
        internal void OnLoadMethod()
        {
            MyDemoDialog.StatusText1 = "Success of OnLoad method.";
        }

        /// <summary>
        /// Demo程序的主窗体中的主Button的Click事件
        /// </summary>
        /// <param name="form"></param>
        internal void MainClick()
        {
            MarkRecords(records);
        }

        /// <summary>
        /// Demo程序的主窗体中的TreeView的节点TreeNode的Click事件
        /// </summary>
        internal void NodeClick(TreeNode treeNode)
        {
            MyDemoDialog.StatusText2 = "Node Click...";
        }

        /***********************/

        private Records records = new Records();
        private static void MarkRecords(Records records)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            records = new Records();
            MyDemoDialog.Clear();
            PgnReader reader = new PgnReader();
            //reader.Filename = Program.PGNFile_CHS_Game;
            reader.Filename = Program.PGNFile_Test_2_Game;
            //reader.Filename = @"e:\ChessBase\Bases\08a.pgn";
            reader.AddEvents(records);
            reader.Parse();
            watch.Stop();
            double rTime = watch.ElapsedMilliseconds;//总时间
            double fTIme = rTime / records.Count;
            MyDemoDialog.StatusText1 = string.Format("记录个数：{2}, 总时间：{0}，每个记录的解析时间为：{1}", rTime, fTIme, records.Count);

            MyDemoDialog.RecordTree.BeginUpdate();
            foreach (Record record in records)
            {
                TreeNode node = new TreeNode();
                node.Text = (string)record.Tags.Get("Event");
                node.Tag = record;
                MyDemoDialog.RecordTree.Nodes.Add(node);
            }
            MyDemoDialog.RecordTree.EndUpdate();
        }

        #region 生成一些测试用的Step用例

        /// <summary>
        /// 生成一些测试用的Step用例
        /// </summary>
        private static void MarkSteps()
        {
            MyDemoDialog.FormCursor = Cursors.WaitCursor;

            List<string> step2 = new List<string>();
            List<string> step3 = new List<string>();
            List<string> step4 = new List<string>();
            List<string> step5 = new List<string>();
            List<string> step6 = new List<string>();
            List<string> step7 = new List<string>();
            List<string> step8 = new List<string>();
            List<string> stepPr = new List<string>();

            string regex = @"\d+\.";
            string file = @"DemoFile\largeness,game.pgn";
            string[] lines = File.ReadAllLines(file);
            MyDemoDialog.ProgressBar.Maximum = lines.Length + 20;
            MyDemoDialog.ProgressBar.Value = 0;

            MyDemoDialog.StatusText1 = "File read complate. Line count: " + lines.Length.ToString() + ".";
            Application.DoEvents();

            foreach (string line in lines)
            {
                #region line
                MyDemoDialog.ProgressBar.PerformStep();
                MyDemoDialog.ProgressBar.Invalidate();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                string v = line.Trim();
                if (v.StartsWith("["))
                {
                    continue;
                }
                string[] step = (new Regex(regex)).Split(v);
                foreach (string str in step)
                {
                    string s = str.Trim();
                    if (string.IsNullOrEmpty(s))
                    {
                        continue;
                    }
                    string[] lei = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in lei)
                    {
                        if (item.IndexOf('-') > 0) continue;
                        if (item.IndexOf('=') > 0)
                        {
                            stepPr.Add(item);
                            continue;
                        }
                        switch (item.Length)
                        {
                            #region case
                            case 2:
                                step2.Add(item);
                                break;
                            case 3:
                                step3.Add(item);
                                break;
                            case 4:
                                step4.Add(item);
                                break;
                            case 5:
                                step5.Add(item);
                                break;
                            case 6:
                                step6.Add(item);
                                break;
                            case 7:
                                step7.Add(item);
                                break;
                            case 8:
                                step8.Add(item);
                                break;
                            default:
                                break;
                            #endregion
                        }
                    }
                }
                #endregion
            }
            stepPr.Sort();
            StringBuilder sbAll = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            MarkStringBuilder(sbAll, sb1, sb2, stepPr, step6, step5, step4, step3, step2);

            File.Delete("stepsAll.txt");
            File.Delete("steps1.txt");
            File.Delete("steps2.txt");
            File.AppendAllText("stepsAll.txt", sbAll.ToString());
            File.AppendAllText("steps1.txt", sb1.ToString());
            File.AppendAllText("steps2.txt", sb2.ToString());

            MyDemoDialog.Clear();
            MyDemoDialog.TextBox3 = "Complated!";

            MyDemoDialog.FormCursor = Cursors.Default;
        }

        private static void MarkStringBuilder(StringBuilder sbAll, StringBuilder sb1, StringBuilder sb2, params List<string>[] steps)
        {
            int j = 1;
            foreach (List<string> item in steps)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    string str = item[i];
                    sbAll.AppendLine(str);
                    if (i % (10 * j) == 0)
                        sb1.AppendLine(str);
                    if (i % (20 * j) == 0)
                        sb2.AppendLine(str);
                }
                j = j * 2;
            }
        }

        #endregion
    }
}
