using System;
using System.Collections.Generic;
using NKnife.Serials.ParseTools;
using Xunit.Sdk;

namespace NKnife.Serials.UnitTests.Examples
{
    public class UsePackageToolExample
    {
        private readonly UsualVoucherTool _tool = new UsualVoucherTool();

        public List<(byte, ArraySegment<byte>)> Results { get; } = new List<(byte, ArraySegment<byte>)>();
        public List<IVoucher> Vouchers { get; set; } = new List<IVoucher>();

        public void SetSkipCRC(bool skip)
        {
            _tool.SkipCRC = skip;
        }

        /// <summary>
        ///     最后的执行成果
        /// </summary>
        public bool TrueAtLastast { get; set; }

        /// <summary>
        ///     方法正确执行完成的次数
        /// </summary>
        public short TrueCount { get; private set; }

        public FieldConfig FieldConfig
        {
            get => _tool.Field;
            set => _tool.Field = value;
        }

        public bool Run(byte[] message)
        {
            return Run(new ArraySegment<byte>(message));
        }

        private bool Run(ArraySegment<byte> message)
        {
            var vouchers = new List<IVoucher>(1);
            TrueAtLastast = _tool.TryParse(message, ref vouchers);
            foreach (var voucher in vouchers) Add(voucher);
            if (TrueAtLastast)
                TrueCount++;
            return TrueAtLastast;
        }

        private void Add(IVoucher v)
        {
            Vouchers.Add(v);
            Results.Add((v.Command.At(0), v.DataField));
        }
    }
}