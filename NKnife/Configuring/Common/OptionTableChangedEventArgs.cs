﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gean.Configuring.Common
{
    /// <summary>
    /// 当事件发生时包含事件数据的类
    /// </summary>
    public class OptionTableChangedEventArgs : EventArgs
    {
        public string TableName { get; private set; }
        public OptionTableChangedEventArgs(string tableName)
        {
            this.TableName = tableName;
        }
    }
}