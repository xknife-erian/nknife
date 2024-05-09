using ConsoleUi;

namespace Sample.SqlSugarProject
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, SqlSugar! 2024-5-9");

            var menuRun = new ConsoleUi.Console.ConsoleMenuRunner();
            var menu    = new SqlSugarLearningMenu();
            await menuRun.Run(menu);
        }
    }

    public class SqlSugarLearningMenu : SimpleMenu
    {
        
    }
}
