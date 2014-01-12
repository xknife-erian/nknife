using System.Text;

namespace NKnife.Net.EMTASS
{
    /// <summary>
    /// 测试用会话Session类
    /// </summary>
    public class SocketSession : TSessionBase
    {

        /// <summary>
        /// 重写错误处理方法, 返回消息给客户端
        /// </summary>
        protected override void OnDatagramDelimiterError()
        {
            base.OnDatagramDelimiterError();

            base.SendDatagram("datagram delimiter error");
        }

        /// <summary>
        /// 重写错误处理方法, 返回消息给客户端
        /// </summary>
        protected override void OnDatagramOversizeError()
        {
            base.OnDatagramOversizeError();

            base.SendDatagram("datagram over size");
        }
        /// <summary>
        /// 重写 AnalyzeDatagram 方法, 调用数据存储方法
        /// </summary>
        protected override void AnalyzeDatagram(byte[] datagramBytes)
        {

            string datagramText = Encoding.Default.GetString(datagramBytes);

            string clientName = string.Empty;

            int n = datagramText.IndexOf('|');  // 格式为 <C12345,0000000000,****>
            if (n >= 1)
            {
                clientName = datagramText.Substring(0, n);
            }

            base.OnDatagramAccepted();  // 模拟接收到一个完整的数据包

            if (!string.IsNullOrEmpty(clientName))
            {
                //base.SendDatagram("<OK: " + clientName + ", datagram length = " + datagramTextLength.ToString() + ",content = " + datagramText + ">");

                //this.Store(datagramBytes);
                base.State = TSessionState.Active;

                //this.QCore.MsgProcessing(this.ID,clientName, datagramText);

                base.OnMsgProcessing(this.ID, clientName, datagramText);

                base.OnDatagramHandled();  // 模拟已经处理（存储）了数据包
            }
            else
            {
                base.SendDatagram("指令格式不正确,无命令字");
                base.OnDatagramError();
            }
        }

        /// <summary>
        /// 自定义的数据存储方法
        /// </summary>
        private void Store(byte[] datagramBytes)
        {
            if (this.DatabaseObj == null)
            {
                return;
            }

            DbEvent db = this.DatabaseObj as DbEvent;
            db.Store(datagramBytes, this);
        }


    }
}
