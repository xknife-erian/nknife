using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;

namespace NKnife.Kits.DataLiteKit.Entities
{
    public class Company
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Address { get; set; }
        public int WorkerCount { get; set; }
    }
}
