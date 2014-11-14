using System;
using System.Web.Http;
using Didaku.Engine.Timeaxis.Implement.Environment;
using Didaku.Engine.Timeaxis.Kernel;

namespace Didaku.Engine.Timeaxis.API.Controllers.Base
{
    public class EngineStarterController : ApiController
    {
        [POST("EngineStarter/Start")]
        public bool Start()
        {
            var initializer = new EnvironmentInitializer();
            bool isComplate = initializer.Initialize();
            return isComplate;
        }

        [POST("EngineStarter/ReStart")]
        public string ReStart()
        {
            bool isComplate = true;//Global.EngineReStarter();
            return isComplate ? "EngineReStart-OK" : "EngineReStart-No";
        }

        [POST("EngineStarter/Stop")]
        public string Stop()
        {
            bool isComplate = true;// Global.EngineStop();
            return isComplate ? "EngineStop-OK" : "EngineStop-No";
        }
    }
}