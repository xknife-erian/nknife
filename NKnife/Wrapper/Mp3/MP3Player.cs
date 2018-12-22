using System;
using System.Runtime.InteropServices;

namespace NKnife.Wrapper.Mp3
{
    /// <summary>
    ///     一个基本的MP3的播放类
    /// </summary>
    public class Mp3Player
    {
        #region 定义API函数使用的字符串变量 

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] 
        private string _name = "";
        [MarshalAs(UnmanagedType.LPTStr, SizeConst = 128)] 
        private string _temStr = "";
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] 
        private string _durLength = "";

        #endregion

        /// <summary>
        ///     播放状态枚举变量
        /// </summary>
        public enum State
        {
            Playing = 1,
            Puase = 2,
            Stop = 3
        };

        protected StructMci _Mci = new StructMci();

        /// <summary>
        ///     MP3文件
        /// </summary>
        public string FileName
        {
            get { return _Mci._IName; }
            set
            {
                try
                {
                    _temStr = "";
                    _temStr = _temStr.PadLeft(127, Convert.ToChar(" "));
                    _name = _name.PadLeft(260, Convert.ToChar(" "));
                    _Mci._IName = value;
                    ApiClass.GetShortPathName(_Mci._IName, _name, _name.Length);
                    _name = GetCurrPath(_name);
                    _name = "open " + Convert.ToChar(34) + _name + Convert.ToChar(34) + " alias media";
                    ApiClass.mciSendString("close all", _temStr, _temStr.Length, 0);
                    ApiClass.mciSendString(_name, _temStr, _temStr.Length, 0);
                    ApiClass.mciSendString("set media time format milliseconds", _temStr, _temStr.Length, 0);
                    _Mci._State = State.Stop;
                }
                catch
                {
                }
            }
        }

        /// <summary>
        ///     总时间
        /// </summary>
        public int Duration
        {
            get
            {
                _durLength = "";
                _durLength = _durLength.PadLeft(128, Convert.ToChar(" "));
                ApiClass.mciSendString("status media length", _durLength, _durLength.Length, 0);
                _durLength = _durLength.Trim();
                if (_durLength == "") return 0;
                return (int) (Convert.ToDouble(_durLength)/1000f);
            }
        }

        /// <summary>
        ///     当前时间
        /// </summary>
        public int CurrentPosition
        {
            get
            {
                _durLength = "";
                _durLength = _durLength.PadLeft(128, Convert.ToChar(" "));
                ApiClass.mciSendString("status media position", _durLength, _durLength.Length, 0);
                _Mci._IPos = (int) (Convert.ToDouble(_durLength)/1000f);
                return _Mci._IPos;
            }
        }

        /// <summary>
        ///     播放
        /// </summary>
        public void Play()
        {
            _temStr = "";
            _temStr = _temStr.PadLeft(127, Convert.ToChar(" "));
            ApiClass.mciSendString("play media", _temStr, _temStr.Length, 0);
            _Mci._State = State.Playing;
        }

        /// <summary>
        ///     停止
        /// </summary>
        public void Stop()
        {
            _temStr = "";
            _temStr = _temStr.PadLeft(128, Convert.ToChar(" "));
            ApiClass.mciSendString("close media", _temStr, 128, 0);
            ApiClass.mciSendString("close all", _temStr, 128, 0);
            _Mci._State = State.Stop;
        }

        public void Puase()
        {
            _temStr = "";
            _temStr = _temStr.PadLeft(128, Convert.ToChar(" "));
            ApiClass.mciSendString("pause media", _temStr, _temStr.Length, 0);
            _Mci._State = State.Puase;
        }

        private string GetCurrPath(string name)
        {
            if (name.Length < 1) return "";
            name = name.Trim();
            name = name.Substring(0, name.Length - 1);
            return name;
        }

        private class ApiClass
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern int GetShortPathName(
                string lpszLongPath,
                string shortFile,
                int cchBuffer
                );

            [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
            public static extern int mciSendString(
                string lpstrCommand,
                string lpstrReturnString,
                int uReturnLength,
                int hwndCallback
                );
        }

        /// <summary>
        ///     结构变量
        /// </summary>
        public struct StructMci
        {
            public bool _BMut;
            public int _IBal;
            public int _IDur;
            public string _IName;
            public int _IPos;
            public int _IVol;
            public State _State;
        };
    }
}