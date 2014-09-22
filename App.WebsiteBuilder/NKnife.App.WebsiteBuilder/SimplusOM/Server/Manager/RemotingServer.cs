using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;

namespace Jeelu.SimplusOM.Server
{
    static public class RemotingServer
    {
        static public void Start()
        {
            //if (!System.Diagnostics.EventLog.SourceExists("Remoting Service"))
            //{
            //    //System.Diagnostics.EventLog.CreateEventSource("Remoting Service", "Remoting Service");
            //}
            //System.Diagnostics.EventLog.WriteEntry("Remoting Service", "RemotingService 在" + DateTime.Now.ToString() + " 时启动");
            try
            {
                //string path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\DBServer.exe.config";
                //RemotingConfiguration.Configure(path, false);
                HttpChannel httpChannel = new HttpChannel(5060);
                //服务器激活
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(DataAgentImplement), "DBService.SOAP", WellKnownObjectMode.SingleCall);
                
                ////实例化类
                //RemotingConfiguration.RegisterActivatedServiceType(typeof(DataAgentImplement));
                    //, "DBService.SOAP", WellKnownObjectMode.SingleCall);

                //注册信息
                //  RemotingServices.Marshal(entry_, "DBService.SOAP");
                ChannelServices.RegisterChannel(httpChannel, false);
            }
            catch (Exception e)
            {
                //System.Diagnostics.EventLog.WriteEntry("Remoting Service", "RemotingService 在" + DateTime.Now.ToString() + " 时启动失败,详细信息:" +
                //e.Message, EventLogEntryType.Error);
            }
        }
    }
}
