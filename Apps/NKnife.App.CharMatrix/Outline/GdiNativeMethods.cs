using System;
using System.Runtime.InteropServices;

namespace NKnife.App.CharMatrix.Outline
{
    /// <summary>
    /// GDI·½·¨
    /// </summary>
    public class GdiNativeMethods
    {
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);

        [DllImport("gdi32.dll")]
        public static extern int GetGlyphOutline(IntPtr hdc, uint uChar, uint fuFormat, out GLYPHMETRICS lpgm, uint cbBuffer, IntPtr lpBuffer, ref MAT2 lpmat2);

        [DllImport("Kernel32.dll")]
        public static extern IntPtr CloseHandle(IntPtr hObject);
    }
}
