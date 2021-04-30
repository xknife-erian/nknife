using System;
using System.Collections.Generic;
using NKnife.Serials.ParseTools;

namespace NKnife.Serials.UnitTests.Stub
{
    public class UsualVoucherToolStub : UsualVoucherTool
    {
        public VoucherSegment SegmentSource => _segment;
   }
}