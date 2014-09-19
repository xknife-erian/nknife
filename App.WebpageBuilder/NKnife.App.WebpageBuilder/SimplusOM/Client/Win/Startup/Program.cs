using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;

namespace Jeelu.SimplusOM.Client
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
            
              LoginForm login = new LoginForm();
              if (login.ShowDialog() == DialogResult.OK)
              {
                  OMWorkBench.BaseInfoDS = login.BaseInfoDataSet;
                  OMWorkBench.CurrentInfo = login.SelfInfoDataSet;
                  DataRow loginRow=login.SelfInfoDataSet.Tables["login"].Rows[0];
                  OMWorkBench.AgentId = Convert.ToInt32(loginRow["id"]);
                  OMWorkBench.AgentName = loginRow["name"].ToString();
                  OMWorkBench.Balance =Convert.ToDecimal(loginRow["balance"]);
                  OMWorkBench.MangerId = Convert.ToInt32(loginRow["id1"]);
                  OMWorkBench.MangerName = loginRow["name1"].ToString();
                  OMWorkBench.AreaId = Convert.ToInt32(loginRow["area_id"]);
                  OMWorkBench.Grade = Convert.ToInt32(loginRow["grade"]);
                  OMWorkBench.Rights = OMWorkBench.StrToBitStr(Convert.ToString(loginRow["rights"]));
              }
              else
              {
                  return;
              }
            Application.Run(OMWorkBench.MainForm);
        }
    }
}
