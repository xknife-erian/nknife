using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using ConsoleUi.Console;
using Dapper;
using Example.CoreConsole.IoC;
using Example.StoragesLevel;
using NKnife.Storages;
using NLog;

namespace Example.CoreConsole
{
    public static class Program
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        public static readonly string StoragesOptionFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"config{Path.DirectorySeparatorChar}", "DomainSqlConfig.yaml");

        public static async Task Main(params string[] args)
        {
            _Logger.Info("Start Application...");
            _Logger.Info(StoragesOptionFilePath);

            SimpleCRUD.SetTableNameResolver(new TableName());

            var builder = new ContainerBuilder();

            if (!File.Exists(StoragesOptionFilePath))
            {
                Console.WriteLine($"配置文件不存在: {StoragesOptionFilePath}");
                Console.ReadKey();
            }

            //重点：从外围文件配置NKnife.Storages的使用信息
            builder.RegisterOptionYamlFiles(StoragesOptionFilePath)
                .RegisterOption<DomainSqlConfig>();

            //重点：各层的IoC配置信息
            builder.RegisterModule<StorageModule>();
            builder.RegisterModule<MapperModule>();
            builder.RegisterModule<LogicModule>();

            //Demo程序的一些内容
            builder.RegisterType<SimpleData>().AsSelf().SingleInstance();
            builder.RegisterType<SimpleRead>().AsSelf().SingleInstance();
            builder.RegisterType<ConsoleMenuRunner>().AsSelf().SingleInstance();
            builder.RegisterType<ProgramMainMenu>().AsSelf().SingleInstance();

            await using (var container = builder.Build())
            {
                var dbService = container.Resolve<IDbService>();
                await dbService.StartAsync(new CancellationToken());

                var runner = container.Resolve<ConsoleMenuRunner>();
                var mainMenu = container.Resolve<ProgramMainMenu>();

                await runner.Run(mainMenu);
            }
        }


        class TableName : SimpleCRUD.ITableNameResolver
        {
            #region Implementation of ITableNameResolver

            public string ResolveTableName(Type type)
            {
                return type.Name;
            }

            #endregion
        }
    }
}