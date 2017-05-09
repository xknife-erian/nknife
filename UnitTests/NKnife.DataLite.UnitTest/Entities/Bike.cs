using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.DataLite.UnitTest.Entities
{
    public class Bike
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ProductionDate { get; set; }
    }
}
