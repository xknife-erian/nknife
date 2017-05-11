using System;
using System.Net.Sockets;

// ReSharper disable once CheckNamespace
namespace System.Net.Sockets
{
    internal static class SocketExtension
	{
		public static void SafeShutdownClose(this Socket socket)
		{
			try
			{
				try
				{
					socket.Shutdown(SocketShutdown.Both);
				}
				catch { }

				socket.Close();
			}
			catch (ObjectDisposedException)
			{
			}
		}
	}
}
