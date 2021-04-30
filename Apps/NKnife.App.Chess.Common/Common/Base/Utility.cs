using System;

namespace NKnife.Chesses.Common.Base
{
    static class Utility
    {
        //Regex moveSplit = new Regex(@"[1-9]+[0-9]?\.(?'move'\s*?(O-O|O-O-O|\w+)(=[QNRB])?[+#]?(\s+(O-O|O-O-O|\w+)(=[QNRB])?[+#]?\s+)?)", RegexOptions.Singleline | RegexOptions.Compiled);
        //Regex singleMoveSplit = new Regex("(?'piece'[NBRKQ])?((?'drank'[12345678])|(?'dfile'[abcdefgh]))?x?(?'dest'[abcdefgh][12345678])(=(?'promote'[QNRB]))?");

        public const int TOP = 8;
        public const int FOOTER = 1;
        public const int LEFT = 1;
        public const int RIGHT = 8;

        /// <summary>
        /// 将指定的棋盘横坐标字符转换成整型数字
        /// </summary>
        /// <param name="c">指定的棋盘横坐标字符</param>
        /// <returns>
        /// 如果返回值为-1，则输入值是非棋盘横坐标字符。
        /// 坐标值遵循象棋规则将从1开始。
        /// </returns>
        static public int CharToInt(char c)
        {
            if (char.IsUpper(c))
            {
                string str = Convert.ToString(c);
                str = str.ToLowerInvariant();
                c = Convert.ToChar(str);
            }
            switch (c)
            {
                #region case
                case 'a':
                    return 1;
                case 'b':
                    return 2;
                case 'c':
                    return 3;
                case 'd':
                    return 4;
                case 'e':
                    return 5;
                case 'f':
                    return 6;
                case 'g':
                    return 7;
                case 'h':
                    return 8;
                default:
                    return -1;
                #endregion
            }
        }

        /// <summary>
        /// 将指定的棋盘横坐标字符转换成整型数字
        /// </summary>
        /// <param name="str">指定的棋盘横坐标字符</param>
        /// <returns>
        /// 如果返回值为-1，则输入值是非棋盘横坐标字符。
        /// 坐标值遵循象棋规则将从1开始。
        /// </returns>
        static public int StringToInt(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return -1;
            }
            if (str.Length > 1)
            {
                return -1;
            }
            return Utility.CharToInt(Convert.ToChar(str.ToLowerInvariant()));
        }

        /// <summary>
        /// 将指定的坐标值转换成字符
        /// </summary>
        static public char IntToChar(int i)
        {
            if (i >= 1 && i <= 8)
            {
                #region switch
                switch (i)
                {
                    case 1:
                        return 'a';
                    case 2:
                        return 'b';
                    case 3:
                        return 'c';
                    case 4:
                        return 'd';
                    case 5:
                        return 'e';
                    case 6:
                        return 'f';
                    case 7:
                        return 'g';
                    case 8:
                        return 'h';
                    default:
                        return '*';
                }
                #endregion
            }
            return '*';
        }

        /// <summary>
        /// 将指定的坐标值转换成字符
        /// </summary>
        static public string IntToString(int i)
        {
            return Utility.IntToChar(i).ToString();
        }

        static Random _rand;
        static public Int64 Rand64()
        {
            if (_rand == null)
            {
                _rand = new Random(unchecked((int)DateTime.Now.Ticks));
            }
            return _rand.Next() ^ ((Int64)_rand.Next() << 15) ^ ((Int64)_rand.Next() << 30) ^ ((Int64)_rand.Next() << 45) ^ ((Int64)_rand.Next() << 60);
        }

    }
}
