using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel;
using NKnife.Tunnel.Generic;

namespace NKnife.Kits.SerialKnife.Filters
{
    public class QueryBusFilter : TunnelFilterBase<byte[],int>
    {
        public override void PrcoessReceiveData(ITunnelSession<byte[], int> session)
        {
            
        }

        public override void ProcessSessionBroken(int id)
        {

        }

        public override void ProcessSessionBuilt(int id)
        {
            //连接建立后就开始轮询
        }
    }
}
