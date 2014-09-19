using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Jeelu.Billboard
{
    public class ParamState
    {
        #region  字段或属性

        public long ID { get; set; }

        /// <summary>
        /// key的值
        /// </summary>
        public string ParamKey { get; set; }

        /// <summary>
        /// 参数的状态
        /// </summary>
        public WebKeyState KeyState { get; set; }

        /// <summary>
        /// 标示参数的状态是否改变过
        /// </summary>
        public bool IsStateChange { get; set; }

        #endregion

        #region  构造函数

        public ParamState(string param)
        {
            Debug.Assert(param.Contains("="),"para is Invalidation");

            string[] keyvalue = param.Split('=');
            this.ParamKey = keyvalue[0];
            //this.Value = keyvalue[1];
            this.KeyState = WebKeyState.Availability; //默认都是有效的
        }

        public ParamState(string paramKey, WebKeyState keyState)
        {
            this.ParamKey = paramKey;
            this.KeyState = keyState;
        }

        #endregion

        #region  内部方法

        #endregion

        #region  外部方法
        
        public override string ToString()
        {
            return "";
        }

        #endregion



    }
}
