using System;
using System.Collections.Generic;

namespace NKnife.Serials.ParseTools
{
    public abstract class Voucher : IVoucher
    {
        protected FieldConfig _fieldConfig;
        protected byte[] _head;
        protected byte[] _tail;
        protected List<byte> _data;
        protected short _headWritePosition;
        protected short _tailWritePosition;

        #region Implementation of IVoucher

        /// <summary>
        ///     起始标记
        /// </summary>
        public virtual ArraySegment<byte> Begin => new ArraySegment<byte>(_head, _fieldConfig.Begin.Item1, _fieldConfig.Begin.Item2);

        /// <summary>
        ///     凭据属性
        /// </summary>
        public virtual ArraySegment<byte> Attribute => new ArraySegment<byte>(_head, _fieldConfig.Attribute.Item1, _fieldConfig.Attribute.Item2);

        /// <summary>
        ///     地址
        /// </summary>
        public virtual ArraySegment<byte> Address => new ArraySegment<byte>(_head, _fieldConfig.Address.Item1, _fieldConfig.Address.Item2);

        /// <summary>
        ///     已解析出的协议的命令字
        /// </summary>
        public virtual ArraySegment<byte> Command => new ArraySegment<byte>(_head, _fieldConfig.Command.Item1, _fieldConfig.Command.Item2);

        /// <summary>
        ///     数据域长度
        /// </summary>
        public virtual ArraySegment<byte> DataFieldLength => new ArraySegment<byte>(_head, _fieldConfig.DataFieldLength.Item1, _fieldConfig.DataFieldLength.Item2);

        /// <summary>
        ///     数据域
        /// </summary>
        public virtual ArraySegment<byte> DataField => new ArraySegment<byte>(_data.ToArray());

        /// <summary>
        ///     校验
        /// </summary>
        public virtual ArraySegment<byte> CRC => new ArraySegment<byte>(_tail, 0, _fieldConfig.CRC);

        /// <summary>
        ///     结尾标记
        /// </summary>
        public virtual ArraySegment<byte> End => new ArraySegment<byte>(_tail, _fieldConfig.CRC, _fieldConfig.End);


        public IVoucher ToVoucher()
        {
            return this;
        }

        #endregion

        /// <summary>
        /// 追加头部的字节
        /// </summary>
        public virtual void AppendHead(ArraySegment<byte> bytes)
        {
            var i = 0;
            foreach (var b in bytes)
            {
                _head[_headWritePosition + i] = b;
                i++;
            }

            _headWritePosition += (short) i;
            if (_headWritePosition == _fieldConfig.GetHeadLength())
            {
                if (TryGetDataFieldLength(out var length))
                    _data = new List<byte>(length);
            }
        }

        /// <summary>
        /// 追加数据域的字节
        /// </summary>
        public virtual void AppendData(ArraySegment<byte> bytes)
        {
            if (TryGetDataFieldLength(out var length))
            {
                if (_data.Count + bytes.Count > length)
                    _data.AddRange(bytes.Slice(0, length - _data.Count));
                _data.AddRange(bytes.ToArray());
            }
        }

        /// <summary>
        /// 追加尾部字节
        /// </summary>
        /// <param name="bytes"></param>
        public virtual void AppendTail(ArraySegment<byte> bytes)
        {
            var i = 0;
            foreach (var b in bytes)
            {
                if (_tailWritePosition + i > _tail.Length - 1)
                    break;
                _tail[_tailWritePosition + i] = b;
                i++;
            }

            _tailWritePosition += (short) i;
        }

        private int _dataFieldLength = -1;

        /// <summary>
        ///     获取数据域长度数值。
        /// </summary>
        /// <returns>
        /// 当解析正确可以获取时，返回true，out参数返回数据域的长度。
        /// 当解析正确可以获取时，返回false，out参数:
        /// 当-1，没有可能解析的长度数据；
        /// 当-2，解析出的数据大于设定的最大值；
        /// 当-3，解析出的数据小于0；
        /// </returns>
        public virtual bool TryGetDataFieldLength(out int dataFieldLength)
        {
            dataFieldLength = _dataFieldLength;
            switch (_dataFieldLength)
            {
                case -1:
                {
                    if (_headWritePosition < _fieldConfig.GetHeadLength())
                        return false;
                    var length = 0;
                    switch (_fieldConfig.DataFieldLength.Item2)
                    {
                        case 1:
                            length = _head[_fieldConfig.DataFieldLength.Item1];
                            break;
                        case 4:
                            length = BitConverter.ToInt32(_head, _fieldConfig.DataFieldLength.Item1);
                            break;
                        case 2:
                        default:
                            length = BitConverter.ToInt16(_head, _fieldConfig.DataFieldLength.Item1);
                            break;
                    }

                    if (length > _fieldConfig.DataFieldMaxLength)
                    {
                        _dataFieldLength = -2;
                        dataFieldLength = _dataFieldLength;
                        return false;
                    }

                    if (length < 0)
                    {
                        _dataFieldLength = -3;
                        dataFieldLength = _dataFieldLength;
                        return false;
                    }

                    _dataFieldLength = length;
                    dataFieldLength = _dataFieldLength;
                    return true;
                }
                case -2:
                case -3:
                    return false;
                default:
                    if (_dataFieldLength < 0)
                        return false;
                    return true;
            }
        }

    }
}