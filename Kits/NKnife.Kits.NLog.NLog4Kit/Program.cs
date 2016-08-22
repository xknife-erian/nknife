﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NKnife.IoC;

namespace NKnife.Kits.NLog.NLog4Kit
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DI.Initialize();
            Application.Run(new Form1());
        }
    }
}
