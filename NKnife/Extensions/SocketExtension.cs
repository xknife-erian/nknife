﻿using System;
using System.Net;
using System.Net.Sockets;

namespace System.Net.Sockets
{
	public static class SocketEx
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