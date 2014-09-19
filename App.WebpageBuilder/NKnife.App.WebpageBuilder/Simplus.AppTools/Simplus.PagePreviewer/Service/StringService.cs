using System;
using System.IO;
using System.Web;

namespace Jeelu.SimplusPagePreviewer
{
    class StringService
    {
        /// <summary>
        /// 获取请求中的Get部分
        /// </summary>
        public static string GetRequest(string GetString)
        {
            string str = GetString.Substring(5, GetString.Length - 14);
            //System.Windows.Forms.MessageBox.Show(GetString.Substring(5, GetString.Length - 14));
            return str;
        }

        /// <summary>
        ///  获得请求类型
        /// </summary>
        public static string GetRequestType(string GetString)
        {
            //通过寻找最后一个"."后的内容取得类型
            string type = Path.GetExtension(GetString);
            if (type != "")
            {
                string tempType = type.Substring(1).ToLower();
                int tempQuoPosition = tempType.IndexOf("?");
                if (tempQuoPosition > 0)
                {
                    return tempType.Substring(0, tempQuoPosition);
                }
                else
                {
                    return tempType;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 取出“”中的内容
        /// </summary>
        public static string GetQuotationMarkComment(string StringName)
        {
            try
            {
                string comment = null;
                int frist_Number = 0;
                int second_Number = 0;

                char[] charStringName = StringName.ToCharArray();
                int i = 0;
                //frist_Number = i;
                //second_Number = 0;
                while (charStringName[i] != '"')
                {
                    frist_Number += 1;
                    i = i + 1;
                }
                i = i + 1;
                frist_Number += 1;
                while (charStringName[i] != '"')
                {
                    second_Number += 1;
                    i = i + 1;
                }
                comment = StringName.Substring(frist_Number, second_Number);

                return comment;
            }
            catch (Exception e)
            {
                ExceptionService.CreateException(e);
                return null;
            }
        }

        /// <summary>
        ///  删除?后面内容
        /// </summary>
        public static string DeleteQuest(string incloudString)
        {
            int i = 0;
            char[] inclodeChars = incloudString.ToCharArray();
            foreach (char incloudchar in inclodeChars)
            {
                if (incloudchar == '?')
                {
                    break;
                }
                i = i + 1;
            }
            return incloudString.Substring(1, i - 1);
        }
    }
}