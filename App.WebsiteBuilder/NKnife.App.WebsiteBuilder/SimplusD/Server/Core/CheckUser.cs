using System;
using System.Collections.Generic;
using System.Text;
namespace Jeelu.SimplusD.Server
{
    /// <summary>
    /// 
    /// </summary>
    class CheckUser
    {
        ///用户名,与自创的新GUID来判定是否此用户为真正的项目的用户在操作
        public static bool IsUser(string userName, string passport)
        {
            return true;
            //以下是可用的
            //if (CommonService.RecordUserDic.ContainsKey(userName))
            //{
            //    string _value = "";
            //    if (CommonService.RecordUserDic.TryGetValue(userName, out _value))
            //    {
            //        if (string.Compare(_value, passport) == 0)
            //        {
            //            return true;
            //        }
            //    }
            //}
            //return false;

        }
    }
}
