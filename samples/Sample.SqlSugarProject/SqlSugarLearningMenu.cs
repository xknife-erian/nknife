using ConsoleUi;
using Sample.SqlSugarProject.Items;

namespace Sample.SqlSugarProject
{
    public partial class SqlSugarLearningMenu : SimpleMenu
    {
        public void Basic(IMenuContext ctx)
        {
            var d01 = new D01简单的基础操作();
            d01.Run(ctx.UserInterface);
        }

        public void SqlSugarQuery(IMenuContext ctx) { }
        public void SqlSugarJoin(IMenuContext ctx) { }
        public void SqlSugarPage(IMenuContext ctx) { }
        public void SqlSugarFilter(IMenuContext ctx) { }
        public void SqlSugarTransaction(IMenuContext ctx) { }
        public void SqlSugarProcedure(IMenuContext ctx) { }
        public void SqlSugarAttribute(IMenuContext ctx) { }

    }
}