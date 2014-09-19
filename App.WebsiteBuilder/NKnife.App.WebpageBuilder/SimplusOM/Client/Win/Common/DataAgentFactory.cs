using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;

namespace Jeelu.SimplusOM.Client.Win
{
    static public class DataAgentFactory
    {
        static bool _isInited;
        static string _url = "http://192.168.1.141:5060/DBService.SOAP";

        static public DataAgent GetDataAgent()
        {
            if (!_isInited)
            {
                RemotingConfiguration.RegisterActivatedClientType(typeof(DataAgent), _url);
                _isInited = true;
            }

            return (DataAgent)Activator.GetObject(typeof(DataAgent), _url);
        }
    }
}
