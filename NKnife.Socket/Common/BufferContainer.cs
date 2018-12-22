using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SocketKnife.Common
{
    /// <summary>
    ///     一个数据包容器, 将SocketAsyncEventArgs的缓存预先打开备用。
    /// </summary>
    public sealed class BufferContainer
    {
        private readonly int _bufferSize;
        private readonly int _numSize;
        private byte[] _buffer;
        private int _currentIndex;
        private Stack<int> _freeIndexPool;

        public BufferContainer(int numsize, int buffersize)
        {
            _numSize = numsize;
            _bufferSize = buffersize;
        }

        public void Initialize()
        {
            _buffer = new byte[_numSize];
            _freeIndexPool = new Stack<int>(_numSize/_bufferSize);
        }

        internal void FreeBuffer(SocketAsyncEventArgs args)
        {
            _freeIndexPool.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }

        internal Boolean SetBuffer(SocketAsyncEventArgs args)
        {
            if (_freeIndexPool.Count > 0)
            {
                args.SetBuffer(_buffer, _freeIndexPool.Pop(), _bufferSize);
            }
            else
            {
                if ((_numSize - _bufferSize) < _currentIndex)
                    return false;
                args.SetBuffer(_buffer, _currentIndex, _bufferSize);
                _currentIndex += _bufferSize;
            }
            return true;
        }
    }
}