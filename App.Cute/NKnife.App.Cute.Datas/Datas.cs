using System;
using NKnife.App.Cute.Base.Interfaces;
using NKnife.App.Cute.Datas.Base;
using NKnife.App.Cute.Datas.Stores;
using NKnife.Interface;

namespace NKnife.App.Cute.Datas
{
    public class Datas : IInitializer
    {
        /// <summary>Transaction集合的存储
        /// </summary>
        public MongoStore<ITransaction, string> Transactions { get; private set; }

        public MongoStore<LogInfo, string> Logs { get; private set; }

         /// <summary>连接字符串
        /// </summary>
        public string DbConnection { get; set; }

        #region Implementation of IInitializer

        /// <summary>是否已经初始化
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>执行初始化动作
        /// </summary>
        /// <param name="args">初始化的动作参数</param>
        public bool Initialize(params object[] args)
        {
            if (!IsInitialized)
            {
                Logs = new LogEventInfoStore(DbConnection);
                Transactions = new TransactionStore(DbConnection);
                IsInitialized = true;
                OnInitialized(EventArgs.Empty);
            }
            return true;
        }

        /// <summary>初始化完成时发生的事件
        /// </summary>
        public event EventHandler InitializedEvent;

        protected virtual void OnInitialized(EventArgs e)
        {
            if (InitializedEvent != null)
                InitializedEvent(this, e);
        }

        #endregion
    }
}