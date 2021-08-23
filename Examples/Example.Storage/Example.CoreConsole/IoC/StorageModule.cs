using Autofac;
using Dapper;
using Example.StoragesLevel;
using NKnife.Storages;

namespace Example.CoreConsole.IoC
{
    public class StorageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<SimpleCRUD.TableNameResolver>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<ExampleDbService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ConnectionManager>().As<IConnectionManager>().SingleInstance();

            builder.RegisterGeneric(typeof(GeneralStorageRead<>))
                .As(typeof(BaseStorageRead<>)).SingleInstance();
            builder.RegisterGeneric(typeof(GeneralStorageWrite<>))
                .As(typeof(BaseStorageWrite<>)).SingleInstance();

            var assembly = typeof(StorageModule).Assembly;
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => !t.IsAbstract && (t.Name.EndsWith("StorageRead") || t.Name.EndsWith("StorageWrite")))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}