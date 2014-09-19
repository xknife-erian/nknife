using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Jeelu.KeywordResonator.Client
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ///初始化WorkbenchForm窗体,保存传入参数,这可能是双击某文件,
            ///产生的关联启动了本程序,那么参数就是文件路径
            WorkbenchForm.Initialize(args);

            InitializeApplication.Initialize();

            Application.Run(WorkbenchForm.MainForm);
        }
    }
}
