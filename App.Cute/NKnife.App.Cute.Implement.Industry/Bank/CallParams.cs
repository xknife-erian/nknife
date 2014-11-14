using System;
using Didaku.Engine.Timeaxis.Implement.Abstracts;

namespace Didaku.Engine.Timeaxis.Implement.Industry.Bank
{
    public class CallParams : BaseActiveParams
    {
        #region Overrides of BaseActiveParams

        /// <summary>解析传入的参数集合并填充本类型
        /// </summary>
        /// <param name="args">传入的参数集合</param>
        protected override void Fill(object[] args)
        {
            Asker = Convert.ToString(args[0]);
            UserId = Convert.ToString(args[1]);
            QueueId = Convert.ToString(args[2]);
        }

        #endregion
    }
}