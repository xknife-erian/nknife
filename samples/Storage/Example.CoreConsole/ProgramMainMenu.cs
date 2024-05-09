using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ConsoleUi;
using Example.Common;
using NKnife.Storages;
using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Realisations.Queries;
using NKnife.Storages.SQL.Realisations.Sql;
using YamlDotNet.Serialization;

namespace Example.CoreConsole
{
    public class ProgramMainMenu : SimpleMenu
    {
        private readonly SimpleData _demoData;
        private readonly DomainSqlConfig _config;
        private readonly SimpleRead _simpleRead;
        
        public ProgramMainMenu(DomainSqlConfig config, SimpleData demoData, SimpleRead simpleRead)
        {
            _config = config;
            _demoData = demoData;
            _simpleRead = simpleRead;
        }

        /// <summary>
        /// 测试ConsoleUi的菜单功能
        /// </summary>
        public void ConsoleUiIsFreeSoftware(IMenuContext c) { }

        /// <summary>
        /// 测试ConsoleUi的菜单功能
        /// </summary>
        public void ConsoleUiIsDistributedInTheHopeThatItWillBeUseful(IMenuContext c){}

        /// <summary>
        /// 测试ConsoleUi的菜单功能
        /// </summary>
        public void ConsoleMenuRunner(IMenuContext c) { }

        public async Task DoMoreStuff(IMenuContext context)
        {
            await context.RunUntilCancelled(async ct =>
            {
                using var progress = context.UserInterface.StartProgress("Doing stuff...");

                for (int i = 0; i <= 100 && !ct.IsCancellationRequested; i++)
                {
                    await Task.Delay(30, ct);
                    progress.SetProgress(i);
                }
            });
        }

        public async Task DoEvenMoreStuff(IMenuContext context)
        {
            await context.RunUntilCancelled(async ct =>
            {
                using var progress = context.UserInterface.StartProgress("Doing stuff...");
                progress.SetIndeterminate();
                await Task.Delay(5000, ct);
            });
        }

        public IMenu ManyOptions()
        {
            ActionMenuItem BuildMenuItem(int i)
            {
                return new ActionMenuItem(i.ToString(), ctx => { });
            }
            var es = Enumerable.Range(0, 15).Select(BuildMenuItem);
            return new Menu("Many options", es);
        }

        public IMenu Counter() => new CountingMenu();

        public void I______(IMenuContext c){}

        public void ViewStorageOption(IMenuContext c)
        {
            var ot = c.UserInterface;
            foreach (var sql in _config.SqlMap.Values)
            {
                ot.Error("---------");
                ot.Error("---------");
                ot.Info($"Name:\t{sql.TypeName}");
                ot.Info($"{nameof(sql.Type)}:\t{sql.Type}");
                ot.Info($"DbType:\t{sql.CurrentDbType}");
                ot.Info($"{nameof(sql.Read)}:\t{sql.Read}");
                ot.Info($"{nameof(sql.Write)}:\t{sql.Write}");
                ot.Warning("------");
                foreach (var pair in sql.CreateTable)
                    ot.Info($"{pair.Key}:\t{pair.Value}");
                ot.Warning("------");
                foreach (var pair in sql.Insert)
                    ot.Info($"{pair.Key}:\t{pair.Value}");
                ot.Warning("------");
                foreach (var pair in sql.Update)
                    ot.Info($"{pair.Key}:\t{pair.Value}");
            }
        }

        public void RunDomainSqlBuilder(IMenuContext c)
        {
            var builder = new SimpleDomainSqlBuilder();
            builder.Run(c.UserInterface);
        }

        public async void BuildDemoData(IMenuContext c)
        {
            await _demoData.AddBooks();
            c?.UserInterface.Info("AddBooks创建完成。");
            await _demoData.AddPeople();
            c?.UserInterface.Info("AddPeople创建完成。");
        }

        public async void BuyAnyBooks(IMenuContext c)
        {
            const int COUNT = 10;
            for (int i = 0; i < COUNT; i++)
            {
                //发生一次随机记录
                //1.随机让某读者购买一本书
                await _demoData.OneRandomTransaction();
            }
            c.UserInterface.Info($"购买{COUNT}本书完成。");
        }

        public async void ViewPersonBuyingRecord(IMenuContext c)
        {
            await _demoData.ViewPersonBuyingRecord();
        }

        public async void GetAllPublishers(IMenuContext c)
        {
            var result = await _simpleRead.GetAllPublishers();
            foreach (var publisher in result)
            {
                c.UserInterface.Info($"{publisher.Year}\t{publisher.Scale}\t{publisher.Name}\t{publisher.Id}");
            }
        }

        public async void GetAllBooks(IMenuContext c)
        {
            var result = await _simpleRead.GetAllBooks();
            foreach (var book in result)
            {
                c.UserInterface.Info($"{book.Name}\t{book.Price}\t{book.Publisher}\t{book.Id}");
            }
        }

        public async void GetAllPeople(IMenuContext c)
        {
            var result = await _simpleRead.GetAllPeople();
            foreach (var person in result)
            {
                c.UserInterface.Info($"{person.Name}\t{person.Birthday:yyMMdd}\t{person.Email}\t{person.Address}\t{person.HasCollection}");
            }
        }

        public async void GetAllBuyingRecords(IMenuContext c)
        {
            var result = await _simpleRead.GetAllBuyingRecords();
            foreach (var record in result)
            {
                c.UserInterface.Info($"{record.TradingTime}\t{record.TradingPrice}\t{record.CurrentOwner}\t{record.OriginalOwner}");
            }
        }

        protected override Task<bool> CanExit(IMenuContext context)
        {
            return context.UserInterface.Confirm(true, "Exit ?");
        }
    }

    public class CountingMenu : DynamicMenu
    {
        private int counter;

        public CountingMenu()
            : base("Counter")
        {
        }

        protected override Task<IEnumerable<IMenuItem>> LoadItems(CancellationToken cancellationToken)
        {
            return Task.FromResult(new IMenuItem[]
            {
                new ActionMenuItem($"Count: {counter}", ctx =>
                {
                    ++counter;
                    ctx.SuppressPause();
                    ctx.InvalidateMenu();
                })
            }.AsEnumerable());
        }
    }

}