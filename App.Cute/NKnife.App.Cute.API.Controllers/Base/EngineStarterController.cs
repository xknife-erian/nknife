using System.Web.Http;
using AttributeRouting.Web.Http;
using NKnife.App.Cute.Kernel;

namespace NKnife.App.Cute.API.Controllers.Base
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