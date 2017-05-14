using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using NKnife.DataLite.UnitTest.Entities;

namespace NKnife.DataLite.UnitTest.SingleDatabase
{
    public class BikeRepository : GlobalDataBaseRepositoryBase<Bike, int>
    {
    }
}
