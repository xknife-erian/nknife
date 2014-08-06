﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using NKnife.Configuring;
using NKnife.Configuring.Common;
using NKnife.Configuring.Interfaces;
using NLog;

namespace Gean.Configuring.Option
{
    [Serializable]
    public class OptionDataTable : DataTable
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        public OptionDataTable()
        {
            IsModified = false;
        }

        /// <summary>反映表的数据是否已经发生了改变
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is modified; otherwise, <c>false</c>.
        /// </value>
        public bool IsModified { get; set; }

        public StringBuilder AsXml
        {
            get
            {
                var xml = new StringBuilder();
                var tw = new StringWriter(xml);
                this.WriteXml(tw, XmlWriteMode.WriteSchema);
                tw.Flush();
                tw.Close();
                return xml;
            }
        }

        public DataRow this[string solution, string clientId]
        {
            get
            {
                string exp = string.Format("solution='{0}' and clientId='{1}'", solution, clientId);
                DataRow[] rows = this.Select(exp);
                if (rows.Length > 0)
                    return rows[0];
                else
                    return null;
            }
        }

        /// <summary>根据方案名返回有效数据行
        /// </summary>
        /// <value></value>
        public DataRow[] this[string solution]
        {
            get
            {
                string exp = string.Format("solution='{0}'", solution);
                DataRow[] rows = this.Select(exp);
                if (rows.Length > 0)
                    return rows;
                return new DataRow[] {};
            }
        }

        public IOptionDataStore ParentStore { get; set; }

        public string[] Solutions
        {
            get
            {
                const string exp = "";
                return Select(exp).Select(row => row["solution"]).Cast<string>().ToArray();
            }
        }

        public string[] Clients
        {
            get
            {
                const string exp = "";
                return this.Select(exp).Select(row => row["clientId"]).Cast<string>().ToArray();
            }
        }

        /// <summary>
        /// 提交自上次调用 System.Data.DataTable.AcceptChanges() 以来对该表进行的所有更改。
        /// 并触发 OptionTableChangedEvent 事件，以确保主从的选项信息表的同步动作。
        /// </summary>
        public new void AcceptChanges()
        {
            base.AcceptChanges();
            if (IsModified)
                OnOptionTableChanged(new OptionTableChangedEventArgs(this.TableName));
        }

        /// <summary>
        /// 内部数据发生改变时的事件
        /// </summary>
        public event OptionTableChangedEventHandler OptionTableChangedEvent;
        protected virtual void OnOptionTableChanged(OptionTableChangedEventArgs e)
        {
            if (OptionTableChangedEvent != null)
                OptionTableChangedEvent(this, e);
        }

        public override string ToString()
        {
            return new StringBuilder(this.TableName)
                .Append(":[")
                .Append(Columns.Count)
                .Append(',')
                .Append(Rows.Count)
                .Append(']')
                .ToString();
        }

        /// <summary>
        /// 通过DataTable的ReadXml方法来获取OptionDataTable实例
        /// </summary>
        /// <param name="childNode">指定的包含DataTable的序列化字符串的XML节点</param>
        /// <param name="store">The store.</param>
        /// <returns></returns>
        internal static OptionDataTable ParseTableNode(IOptionDataStore store, XmlNode childNode)
        {
            var table = new OptionDataTable();
            StringReader sr = null;
            try
            {
                var cdata = childNode.GetCDataElement();
                string xmltext = cdata.InnerText;
                sr = new StringReader(xmltext);
            }
            catch (Exception e)
            {
                _Logger.Warn("解析DataTable时，获取源字符串异常", e);
            }
            if (sr != null)
            {
                table.ReadXml(sr);
                string tablename = string.Empty;
                if (childNode.Attributes != null && childNode.Attributes["name"]!= null)
                    tablename = childNode.Attributes["name"].InnerText;
                SetOptionTableProperties(store, tablename, table);
            }
            return table;
        }

        /// <summary>
        /// 通过DataTable的ReadXml方法来获取OptionDataTable实例
        /// </summary>
        /// <param name="filefullname">指定的包含DataTable的序列化的计算机文件</param>
        /// <param name="store">The store.</param>
        /// <returns></returns>
        internal static OptionDataTable ParseTableNode(IOptionDataStore store, string filefullname)
        {
            var table = new OptionDataTable();
            if (!string.IsNullOrWhiteSpace(filefullname))
            {
                try
                {
                    table.ReadXml(filefullname);
                }
                catch (Exception e)
                {
                    _Logger.Error(string.Format("table.ReadXml异常。{0}", filefullname), e);
                }
                string tablename = Path.GetFileNameWithoutExtension(filefullname);
                SetOptionTableProperties(store, tablename, table);
            }
            return table;
        }

        private static void SetOptionTableProperties(IOptionDataStore store, string tablename, OptionDataTable table)
        {
            table.ParentStore = store;
            table.TableName = tablename;
            if (!table.Columns.Contains("solution"))
                table.Columns.Add("solution");
            if (!table.Columns.Contains("clientId"))
                table.Columns.Add("clientId");
        }
    }
}
