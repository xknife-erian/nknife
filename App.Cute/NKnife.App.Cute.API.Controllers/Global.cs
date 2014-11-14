using System.IO;
using Didaku.Engine.Timeaxis.Kernel;
using NLog;

namespace Didaku.Engine.Timeaxis.API.Controllers
{
    internal class Global
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        internal static IQueueEngine Engine { get; private set; }
        internal static IDepartment Department { get; private set; }

        internal static bool EngineStarter()
        {
            Department = GetDemoDepartments();
            Engine = new SpeedEngine();
            return Engine.Initialize(Department, GetDemoEngineOption());
        }

        internal static bool EngineReStarter()
        {
            Department = GetDemoDepartments();
            Engine = new SpeedEngine();
            return Engine.Initialize(Department, GetDemoEngineOption());
        }

        internal static bool EngineStop()
        {
            Department = GetDemoDepartments();
            Engine = new SpeedEngine();
            return Engine.Initialize(Department, GetDemoEngineOption());
        }

        private static IDepartment GetDemoDepartments()
        {
            return null;
        }

        private static EngineOption GetDemoEngineOption()
        {
            var option = new EngineOption();
            string file = Path.Combine("", "Data\\{0}.queuedb");
            option.DatabaseFile = file;
            return option;
        }
    }
}