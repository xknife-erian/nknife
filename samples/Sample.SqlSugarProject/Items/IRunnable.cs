using ConsoleUi;

namespace Sample.SqlSugarProject.Items
{
    internal interface IRunnable
    {
        bool Run(IMenuUserInterface ui, object? obj = null);
    }
}