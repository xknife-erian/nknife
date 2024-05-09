﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// ReSharper disable once CheckNamespace
namespace NKnife.Win.Forms
{
    public sealed partial class Api
    {
        /// <summary>
        /// API输入法相关
        /// </summary>
        public class Imm32
        {
            [DllImport("imm32.dll")]
            internal static extern IntPtr ImmGetContext(IntPtr hwnd);
            [DllImport("imm32.dll")]
            internal static extern bool ImmGetOpenStatus(IntPtr himc);
            [DllImport("imm32.dll")]
            internal static extern bool ImmSetOpenStatus(IntPtr himc, bool b);
            [DllImport("imm32.dll")]
            internal static extern bool ImmGetConversionStatus(IntPtr himc, ref int lpdw, ref int lpdw2);
            [DllImport("imm32.dll")]
            internal static extern int ImmSimulateHotKey(IntPtr hwnd, int lngHotkey);
            private const int IME_CMODE_FULL_SHAPE = 0x8;
            private const int IME_CHOTKEY_SHAPE_TOGGLE = 0x11;

            /// <summary>
            /// 解决窗口切换输入法变全角的问题
            /// </summary>
            /// <param name="form">The form.</param>
            public static void SetImeMode(Form form)
            {
                IntPtr hIme = ImmGetContext(form.Handle);
                if (ImmGetOpenStatus(hIme))  //如果输入法处于打开状态
                {
                    int iMode = 0;
                    int iSentence = 0;
                    bool bSuccess = ImmGetConversionStatus(hIme, ref iMode, ref iSentence);  //检索输入法信息
                    if (bSuccess)
                    {
                        if ((iMode & IME_CMODE_FULL_SHAPE) > 0)   //如果是全角
                            ImmSimulateHotKey(form.Handle, IME_CHOTKEY_SHAPE_TOGGLE);  //转换成半角
                    }
                }
            }
        }
    }
}
