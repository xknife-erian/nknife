using System;
using System.Collections.Generic;
using System.Text;
using NKnife.Serials.ParseTools;

namespace NKnife.Serials.UnitTests.Stub
{
    public class VoucherStub : Voucher
    {
        public VoucherStub()
        {
            _fieldConfig = new FieldConfig();
            var n = _fieldConfig.Begin.Item2
                    + _fieldConfig.Attribute.Item2
                    + _fieldConfig.Address.Item2
                    + _fieldConfig.Command.Item2
                    + _fieldConfig.DataFieldLength.Item2;
            _head = new byte[n];
            _tail = new byte[_fieldConfig.CRC + _fieldConfig.End];
        }
    }
}
