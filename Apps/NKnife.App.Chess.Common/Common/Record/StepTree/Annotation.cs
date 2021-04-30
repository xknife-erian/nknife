using System.Text;
using System.Text.RegularExpressions;
using NKnife.ShareResources;
using NKnife.Util;
using IItem = NKnife.Chesses.Common.Interface.IChessItem;

namespace NKnife.Chesses.Common.Record.StepTree
{
    /// <summary>
    /// 描述棋评的类。他被包括在一对大括号中。一般都将跟随在一个ChessStep的后面。
    /// </summary>
    public class Annotation : IItem
    {
        /// <summary>
        /// 棋评
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 棋评的作者，一般都将是一个Email地址。
        /// </summary>
        public string UserID
        {
            get { return this._userId; }
            set
            {
                if (CheckEmail(value))
                    _userId = value;
            }
        }
        private string _userId;

        #region IItem 成员

        public string ItemType { get { return "Annotation"; } }
        public string Value
        {
            get { return this.ToString(); }
        }

        #endregion

        /// <summary>
        /// 已重写。按照规定的格式输出到一对大括号中。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" { ");
            if (!string.IsNullOrEmpty(_userId))
            {
                sb.AppendFormat("<{0}> ", _userId);
            }
            sb.Append(this.Comment).Append(" } ");
            return sb.ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            Annotation comment = (Annotation)obj;
            if (!(UtilEquals.StringEquals(this.Comment, comment.Comment))) return false;
            if (!(UtilEquals.StringEquals(this._userId, comment._userId))) return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked(3 * (Comment.GetHashCode() + _userId.GetHashCode()));
        }

        /// <summary>
        /// 对给定的定符串进行解析，返回一个棋局评论类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Annotation Parse(string value)
        {
            Annotation comment = new Annotation();
            value = value.Trim().Trim(new char[] { '{', '}' }).Trim();
            if (value[0] == '<')
            {
                if (value.IndexOf('>') > 1)
                {
                    int index = value.IndexOf('>') - 1;
                    string user = value.Substring(1, index).Trim();
                    if (CheckEmail(user))
                    {
                        comment._userId = user;
                        value = value.Substring(index+2).Trim();
                    }
                }
            }
            comment.Comment = value;
            return comment;
        }
        /// <summary>
        /// 用一个不太复杂的正则校验一个指定的字符串是否是邮件地址
        /// </summary>
        /// <param name="var">指定的字符串</param>
        /// <returns></returns>
        private static bool CheckEmail(string var)
        {
            Regex r = new Regex(RegexString.RegexStr_SimpleEmail);
            return r.Match(var).Success;
        }

    }
}
