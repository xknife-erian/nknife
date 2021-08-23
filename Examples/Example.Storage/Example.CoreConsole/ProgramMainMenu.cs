using System;
using System.IO;
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
        
        public void ViewStorageOption(IMenuContext c)
        {
            foreach (var sql in _config.SqlMap.Values)
            {
                c.UserInterface.Error("---------");
                c.UserInterface.Error("---------");
                c.UserInterface.Info($"Name:\t{sql.TypeName}");
                c.UserInterface.Info($"{nameof(sql.Type)}:\t{sql.Type}");
                c.UserInterface.Info($"DbType:\t{sql.CurrentDbType}");
                c.UserInterface.Info($"{nameof(sql.Read)}:\t{sql.Read}");
                c.UserInterface.Info($"{nameof(sql.Write)}:\t{sql.Write}");
                c.UserInterface.Warning("------");
                foreach (var pair in sql.CreateTable)
                    c.UserInterface.Info($"{pair.Key}:\t{pair.Value}");
                c.UserInterface.Warning("------");
                foreach (var pair in sql.Insert)
                    c.UserInterface.Info($"{pair.Key}:\t{pair.Value}");
                c.UserInterface.Warning("------");
                foreach (var pair in sql.Update)
                    c.UserInterface.Info($"{pair.Key}:\t{pair.Value}");
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
}