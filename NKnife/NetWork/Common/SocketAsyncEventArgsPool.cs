using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace NKnife.NetWork.Common
{
    /// <summary>
    /// Based on example from:
    /// http://msdn2.microsoft.com/en-us/library/system.net.sockets.socketasynceventargs.socketasynceventargs.aspx
    /// Represents a collection of reusable SocketAsyncEventArgs objects.  
    /// </summary>
    internal sealed class SocketAsyncEventArgsPool : IEnumerable<SocketAsyncEventArgs>
    {
        /// <summary>
        /// SocketAsyncEventArgs栈
        /// </summary>
        readonly Stack<SocketAsyncEventArgs> _Pool;

        /// <summary>
        /// 初始化SocketAsyncEventArgs池
        /// </summary>
        /// <param name="capacity">最大可能使用的SocketAsyncEventArgs对象.</param>
        internal SocketAsyncEventArgsPool(Int32 capacity)
        {
            this._Pool = new Stack<SocketAsyncEventArgs>(capacity);
        }

        /// <summary>
        /// 返回SocketAsyncEventArgs池中的 数量
        /// </summary>
        internal int Count
        {
            get { return this._Pool.Count; }
        }

        internal void Clear()
        {
            lock (this._Pool)
            {
                this._Pool.Clear();
            }
        }

        /// <summary>
        /// 弹出一个SocketAsyncEventArgs
        /// </summary>
        /// <returns>SocketAsyncEventArgs removed from the pool.</returns>
        internal SocketAsyncEventArgs Pop()
        {
            lock (this._Pool)
            {
                return this._Pool.Pop();
            }
        }

        /// <summary>
        /// 添加一个 SocketAsyncEventArgs
        /// </summary>
        /// <param name="item">SocketAsyncEventArgs instance to add to the pool.</param>
        internal void Push(SocketAsyncEventArgs item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(@"Items added to a SocketAsyncEventArgsPool cannot be null");
            }
            lock (this._Pool)
            {
                this._Pool.Push(item);
            }
        }

        public IEnumerator<SocketAsyncEventArgs> GetEnumerator()
        {
            return _Pool.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Pool.GetEnumerator();
        }
    }
}
