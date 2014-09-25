using System;

namespace SocketKnife.Interfaces.Sockets
{
    public interface ISocketHandler
    {
        /// <summary>
        /// Invoked from an I/O processor thread when a new connection has been created.
        /// Because this method is supposed to be called from the same thread that
        /// handles I/O of multiple sessions, please implement this method to perform
        /// tasks that consumes minimal amount of time such as socket parameter
        /// and user-defined session attribute initialization.
        /// </summary>
        /// <param name="session"></param>
        void SessionCreated(ISocketSession session);

        /// <summary>
        /// Invoked when a connection has been opened.  This method is invoked after
        /// {@link #sessionCreated(IoSession)}.  The biggest difference from
        /// {@link #sessionCreated(IoSession)} is that it's invoked from other thread
        /// than an I/O processor thread once thread model is configured properly.
        /// </summary>
        /// <param name="session"></param>
        void SessionOpened(ISocketSession session);

        /// <summary>
        /// Invoked when a connection is closed.
        /// </summary>
        /// <param name="session"></param>
        void SessionClosed(ISocketSession session);

        /// <summary>
        /// Invoked when any exception is thrown by user {@link IoHandler}
        /// implementation or by MINA.  If <code>cause</code> is an instance of
        /// {@link IOException}, MINA will close the connection automatically.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="cause"></param>
        void ExceptionCaught(ISocketSession session, Exception cause);

       /// <summary>
        /// Invoked when a message is received.
       /// </summary>
       /// <param name="session"></param>
       /// <param name="message"></param>
        void MessageReceived(ISocketSession session, object message);

        /// <summary>
        /// Invoked when a message written by {@link IoSession#write(Object)} is sent out.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="message"></param>
        void MessageSent(ISocketSession session, object message);
    }
}