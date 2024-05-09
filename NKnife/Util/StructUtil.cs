using System;
using System.Runtime.InteropServices;

namespace NKnife.Util;

public static class StructUtil
{
    public static byte[] ToBytes<T>(this T obj) where T : struct
    {
        var size = Marshal.SizeOf(obj);
        var buffer = Marshal.AllocHGlobal(size);
        try
        {
            Marshal.StructureToPtr(obj, buffer, false);
            var bytes = new byte[size];
            Marshal.Copy(buffer, bytes, 0, size);
            return bytes;
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }

    public static object ToStruct(this byte[] bytes, Type structType, out int structSize)
    {
        var size = Marshal.SizeOf(structType);
        var buffer = Marshal.AllocHGlobal(size);
        try
        {
            Marshal.Copy(bytes, 0, buffer, size);
            structSize = size;
            return Marshal.PtrToStructure(buffer, structType)!;
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }
}