using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace NKnife.Serials.ParseTools
{
    public class UsualVoucherTool : IVoucherTool
    {
        private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     临时保存的断包数据
        /// </summary>
        protected VoucherSegment _Segment;

        /// <summary>
        ///     凭据中各字段的属性
        /// </summary>
        public FieldConfig Field { get; set; } = new FieldConfig();

        /// <summary>
        ///     是否跳过CRC校验。默认False,不跳过。
        /// </summary>
        public bool SkipCRC { get; set; } = false;

        /// <summary>
        ///     解析收到的数据
        /// </summary>
        /// <param name="message">指定的字节数据</param>
        /// <param name="vouchers">已解析完成的凭据集合</param>
        public virtual bool TryParse(ArraySegment<byte> message, ref List<IVoucher> vouchers)
        {
            if (message.Array == null || message.Count <= 0)
                return false;
            //从上包是否为空进行判断，判断上次的凭据解析是否完成。
            if (_Segment == null)
                return TryFirstParse(message, ref vouchers);
            return TryNextParse(message, ref vouchers);
        }

        /// <summary>
        ///     整个通讯过程中，上次解析正确完成，那么本次解析重新开始
        /// </summary>
        /// <param name="message">指定的字节数据</param>
        /// <param name="vouchers">已解析完成的凭据集合</param>
        protected virtual bool TryFirstParse(ArraySegment<byte> message, ref List<IVoucher> vouchers)
        {
            var _head_ = Field.GetHeadLength();
            var _tail_ = Field.GetTailLength();

            //_lastSegment为Null时，也就是当前数据是新包时
            int i = -1;
            if (Field.BeginChars.Length == 1)
                i = message.IndexOf(Field.BeginChars[0]);
            else
                message.IndexOf(Field.BeginChars); //找到起始字节，丢弃起始字节之前的数据

            if (i < 0)
            {
                //当新包时，找不到起始字符，鬼知道是啥子数据
                if (s_logger.IsInfoEnabled)
                    s_logger.Info($"丢弃整包数据：{message.ToArray().ToHexString(" ")}");
                return false;
            }

            if (i > 0)
            {
                if (s_logger.IsInfoEnabled)
                    s_logger.Info($"丢弃前导无效数据：{message.Slice(0, i).ToArray().ToHexString(" ")}");
                message = message.Slice(i); //丢弃数据后的数据
            }

            //数据包有几种很不爽的情况，虽然机率很小，但需要处理：头部不全。
            if (message.Count < Field.GetHeadLength())
            {
                //进入半包模式，等待新的数据到来后再处理
                _Segment = new VoucherSegment(Field);
                _Segment.AppendHead(message);
                return false;
            }

            _Segment = new VoucherSegment(Field);
            _Segment.AppendHead(message.Slice(0, _head_));

            _Segment.TryGetDataFieldLength(out var dataLength);
            var expect = _head_ + dataLength + _tail_; //期望的数据包长度
            var tailIndex = _head_ + dataLength;

            if (message.Count < expect) //是个半包，等待下个包到达后拼包完成解析
            {
                if (message.Count < _head_ + dataLength)
                {
                    _Segment.AppendData(message.Slice(_head_)); //数据域数量不够
                    return false;
                }

                _Segment.AppendData(message.Slice(_head_, dataLength));
                _Segment.AppendTail(message.Slice(tailIndex, message.Count - tailIndex));
                //尾部不全
                return false;
            }

            _Segment.AppendData(message.Slice(_head_, dataLength));
            _Segment.AppendTail(message.Slice(tailIndex, _tail_));

            if (message.Count == expect) //哈哈，整整齐齐的一个数据包
            {
                if (!_Segment.Verify(SkipCRC))
                {
                    _Segment = null;
                    return false;
                }

                vouchers.Add(_Segment.ToVoucher());
                _Segment = null;
                return true;
            }

            if (message.Count > expect) //有粘包，但同时有一个正确的数据包
            {
                if (!_Segment.Verify(SkipCRC))
                {
                    _Segment = null;
                    var success = TryParse(message.Slice(expect), ref vouchers); //将多出来的数据递归处理
                    return false | success;
                }

                vouchers.Add(_Segment.ToVoucher());
                _Segment = null;
                TryParse(message.Slice(expect), ref vouchers); //将多出来的数据递归处理
                return true;
            }

            return false;
        }

        /// <summary>
        ///     整个通讯过程中，上次解析的凭据不完整，本次解析接续
        /// </summary>
        /// <param name="message">指定的字节数据</param>
        /// <param name="vouchers">已解析完成的凭据集合</param>
        protected virtual bool TryNextParse(ArraySegment<byte> message, ref List<IVoucher> vouchers)
        {
            var _head_ = Field.GetHeadLength();
            var _tail_ = Field.GetTailLength();

            var currentReadCount = _Segment.CountCurrentlyWritten;
            //当未收到完整的头部数据时
            if (currentReadCount < _head_)
            {
                if (message.Count + currentReadCount >= _head_)
                {
                    //当后续的数据补足了头部时
                    //将头部数据补齐，立即处理
                    _Segment.AppendHead(message.Slice(0, _head_ - currentReadCount));
                    var again = message.Slice(_head_ - currentReadCount);
                    TryParse(again, ref vouchers);
                }
                else
                {
                    //当后续的包仍然无法补足头部时
                    //将收到的数据置入上包中，等待接收数据
                    _Segment.AppendHead(message);
                }

                return false;
            }

            //当头部正确(已知数据域长度)时。剩余数据域长度。
            _Segment.TryGetDataFieldLength(out var dataLength);
            var restDataCount = dataLength - _Segment.CountOfDataFieldCurrentlyWritten;
            //当头部正确(已知数据域长度)时。剩余(含尾部)数据长度。上次未收到（未解析）的完整数据包长度。
            var restTotalCount = _head_ + dataLength + _tail_ - _Segment.CountCurrentlyWritten;
            //如果本次的数据仍然不满足总长度，将当前的数据填入LastSegment，等待新的数据到来后再处理
            if (message.Count < restTotalCount)
            {
                if (message.Count > restDataCount)
                {
                    //可能包含一部份尾部字符
                    _Segment.AppendData(message.Slice(0, restDataCount));
                    _Segment.AppendTail(message.Slice(restDataCount));
                }
                else
                {
                    //尾部字符有缺失
                    _Segment.AppendData(message);
                }

                return false;
            }

            //无论是否粘包，本包的主体数据是确认已收到。完成本包的解析过程。
            //粘包与否，后续再进行判断。
            if (restDataCount == 0)
            {
                //上次已将数据域收到完整的数据，只是尾部数据不完整
                var t = _head_ + dataLength + _tail_; //期望的数据包长度
                _Segment.AppendTail(message.Slice(0, t - currentReadCount));
            }
            else
            {
                //上次已将数据域收到完整的数据，只是尾部数据不完整
                var p = _head_ + dataLength - currentReadCount;
                _Segment.AppendData(message.Slice(0, p));
                _Segment.AppendTail(message.Slice(p, _tail_));
            }

            //本包的主体数据解析基本正确，最后一步，CRC校验
            if (_Segment.Verify(SkipCRC))
                //当CRC校验通过后，取出相应的数据进行相应的处理
                vouchers.Add(_Segment.ToVoucher());

            //如果本次数据大于总长度，即又发生粘包的情况
            if (message.Count > restTotalCount)
            {
                _Segment = null;
                //将未解析数据分割出来，做为一个新的事件数据
                TryParse(message.Slice(restTotalCount, message.Count - restTotalCount), ref vouchers);
            }

            _Segment = null;
            return true;
        }

    }
}