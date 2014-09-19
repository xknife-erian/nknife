using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Jeelu
{
    static public partial class Utility
    {
        public static class DllImport
        {
            [DllImport("user32.dll")]
            private static extern IntPtr LoadCursorFromFile(string fileName);
            public static Cursor LoadCursor(string fileName)
            {
                return new Cursor(LoadCursorFromFile(fileName));
            }

            [DllImport("user32.dll")]
            private static extern void SetWindowPos(IntPtr formHandle, IntPtr hWndInsertAfter, int x, int y, int width, int height, int args);
            const int SWP_NOSIZE = 0x0001;
            const int SWP_NOMOVE = 0x0002;
            const int SWP_NOZORDER = 0x0004;
            const int SWP_NOREDRAW = 0x0008;
            const int SWP_NOACTIVATE = 0x0010;
            const int SWP_FRAMECHANGED = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
            const int SWP_SHOWWINDOW = 0x0040;
            const int SWP_HIDEWINDOW = 0x0080;
            const int SWP_NOCOPYBITS = 0x0100;
            const int SWP_NOOWNERZORDER = 0x0200;  /* Don't do owner Z ordering */
            const int SWP_NOSENDCHANGING = 0x0400;  /* Don't send WM_WINDOWPOSCHANGING */

            public static void SetWindowHide(System.Windows.Forms.Form form)
            {
                SetWindowPos(form.Handle, (IntPtr)0, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_HIDEWINDOW | SWP_NOSENDCHANGING);
            }
            public static void SetWindowShow(System.Windows.Forms.Form form, System.Windows.Forms.Form formAfter, int x, int y, int width, int height)
            {
                SetWindowPos(form.Handle, (formAfter == null ? (IntPtr)0 : formAfter.Handle), x, y, width, height, SWP_SHOWWINDOW | SWP_NOACTIVATE);
            }

            public const int WM_USER = 0x0400;
            public const int WM_SENDTHISHWND = WM_USER + 143;
            public const int WM_WEBVIEWISSTART = WM_USER + 144;
            public const int WM_SENDTOCLOSEFORM = WM_USER + 145;
            [DllImport("user32.dll", EntryPoint = "SendMessage")]
            public static extern int SendMessage(
                IntPtr hwnd,
                int wMsg,
                IntPtr wParam,
                IntPtr lParam
            );


            const int WM_VSCROLL = 0x0115;

            const int SB_LINEUP = 0;
            const int SB_LINELEFT = 0;
            const int SB_LINEDOWN = 1;
            const int SB_LINERIGHT = 1;
            const int SB_PAGEUP = 2;
            const int SB_PAGELEFT = 2;
            const int SB_PAGEDOWN = 3;
            const int SB_PAGERIGHT = 3;
            const int SB_THUMBPOSITION = 4;
            const int SB_THUMBTRACK = 5;
            const int SB_TOP = 6;
            const int SB_LEFT = 6;
            const int SB_BOTTOM = 7;
            const int SB_RIGHT = 7;
            const int SB_ENDSCROLL = 8;

            /// <summary>
            /// 让树控件向上滚动
            /// </summary>
            static public void ScrollTreeViewLineUp(TreeView treeView)
            {
                SendMessage(treeView.Handle, WM_VSCROLL, (IntPtr)SB_LINEUP, (IntPtr)0);
            }

            /// <summary>
            /// 让树控件向下滚动
            /// </summary>
            static public void ScrollTreeViewLineDown(TreeView treeView)
            {
                SendMessage(treeView.Handle, WM_VSCROLL, (IntPtr)SB_LINEDOWN, (IntPtr)0);
            }
        }
    }
}