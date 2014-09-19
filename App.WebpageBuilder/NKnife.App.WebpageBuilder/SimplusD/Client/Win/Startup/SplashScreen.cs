using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using Jeelu.SimplusD.Client.Win.Startup.Properties;
using System.Reflection;

namespace Jeelu.SimplusD.Client.Win.Startup
{
    #region Win32Class
    // a static class to expose needed win32 gdi functions.
    class Win32
    {
        public const Int32 ULW_COLORKEY = 0x00000001;
        public const Int32 ULW_ALPHA = 0x00000002;
        public const Int32 ULW_OPAQUE = 0x00000004;

        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;


        public enum Bool
        {
            False = 0,
            True
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public Int32 x;
            public Int32 y;

            public Point(Int32 x, Int32 y) { this.x = x; this.y = y; }
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct Size
        {
            public Int32 cx;
            public Int32 cy;

            public Size(Int32 cx, Int32 cy) { this.cx = cx; this.cy = cy; }
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct ARGB
        {
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }


        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        #region DC About

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool DeleteObject(IntPtr hObject);

        #endregion
    }

    #endregion

    public class SplashScreenForm : Form
    {
        public const string VersionText = "SimplusD!";// + Assembly..Version;

        static SplashScreenForm splashScreen;
        static List<string> requestedFileList = new List<string>();
        static List<string> parameterList = new List<string>();
        Bitmap bitmap;

        public static SplashScreenForm SplashScreen
        {
            get
            {
                return splashScreen;
            }
            set
            {
                splashScreen = value;
            }
        }
        /**/
        /// <para>Changes the current bitmap with a custom opacity level. Here is where all happens!</para>
        public void SetBitmap(Bitmap bitmap, byte opacity)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");

            // The ideia of this is very simple,
            // 1. Create a compatible DC with screen;
            // 2. Select the bitmap with 32bpp with alpha-channel in the compatible DC;
            // 3. Call the UpdateLayeredWindow.

            IntPtr screenDc = Win32.GetDC(IntPtr.Zero);
            IntPtr memDc = Win32.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0)); // grab a GDI handle from this GDI+ bitmap
                oldBitmap = Win32.SelectObject(memDc, hBitmap);//  select the newbitmap,return the oldbitmap

                Win32.Size size = new Win32.Size(bitmap.Width, bitmap.Height);//create the size of LayeredWindow
                this.Size = new Size(bitmap.Width, bitmap.Height);
                Win32.Point pointSource = new Win32.Point(0, 0);

                int x = (Screen.PrimaryScreen.Bounds.Width - bitmap.Width) / 2;
                int y = (Screen.PrimaryScreen.Bounds.Height - bitmap.Height) / 2;

                Win32.Point topPos = new Win32.Point(x, y);//LayeredWindow start position
                Win32.BLENDFUNCTION blend = new Win32.BLENDFUNCTION();

                /**/
                /////????
                blend.BlendOp = Win32.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = opacity;
                blend.AlphaFormat = Win32.AC_SRC_ALPHA;

                //the most important function
                Win32.UpdateLayeredWindow(this.Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, Win32.ULW_ALPHA);
            }
            finally
            {
                Win32.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBitmap);
                    //Windows.DeleteObject(hBitmap); // The documentation says that we have to use the Windows.DeleteObject but since there is no such method I use the normal DeleteObject from Win32 GDI and it's working fine without any resource leak.
                    Win32.DeleteObject(hBitmap);
                }
                Win32.DeleteDC(memDc);
            }

        }

        protected override CreateParams CreateParams
        {
            //set form style
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00080000; // This form has to have the WS_EX_LAYERED extended style
                return cp;
            }
        }
        public SplashScreenForm()
        {
#if !DEBUG
			TopMost         = true;
#endif
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;

            SetBitmap(Resources.Welcome, 255);
        }

        public static void ShowSplashScreen()
        {
            splashScreen = new SplashScreenForm();
            splashScreen.Show();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (bitmap != null)
                {
                    bitmap.Dispose();
                    bitmap = null;
                }
            }
            base.Dispose(disposing);
        }

        public static string[] GetParameterList()
        {
            return parameterList.ToArray();
        }

        public static string[] GetRequestedFileList()
        {
            return requestedFileList.ToArray();
        }

        public static void SetCommandLineArgs(string[] args)
        {
            requestedFileList.Clear();
            parameterList.Clear();

            foreach (string arg in args)
            {
                if (arg.Length == 0) continue;
                if (arg[0] == '-' || arg[0] == '/')
                {
                    int markerLength = 1;

                    if (arg.Length >= 2 && arg[0] == '-' && arg[1] == '-')
                    {
                        markerLength = 2;
                    }

                    parameterList.Add(arg.Substring(markerLength));
                }
                else
                {
                    requestedFileList.Add(arg);
                }
            }
        }
    }
}
