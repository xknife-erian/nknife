namespace NKnife.Kits.SocketKnife.Common
{
    public class SocketMessage
    {
        public SocketDirection SocketDirection { get; set; }
        public string Time { get; set; }
        public string Message { get; set; }
        public string Command { get; set; }
    }

    public enum SocketDirection
    {
        Send,Receive
    }
}
