using System;
using System.Collections.Generic;
using System.Text;
using Jeelu.Data;

namespace Jeelu.SimplusOM.Server
{
    public class Manager
    {
        static public void Initialize()
        {
            DataAccess.Initialize(DbProvider.MySql, "218.246.34.205", "jeelu_major", "qinhd", "jihui#)@");
            //218.246.34.205--qinhd:jihui#)@
            //192.168.1.190--qinhd:qinhd
        }
    }
}
