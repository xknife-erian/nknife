using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using AutoMapper;
using Example.Logic.MapProfiles;

namespace Example.CoreConsole.IoC
{
    public class MapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //https://kevsoft.net/2016/02/24/automapper-and-autofac-revisited.html
            builder.RegisterType<GeneralProfiles>().As<Profile>().SingleInstance();
            builder.Register(context =>
                {
                    var profiles = context.Resolve<IEnumerable<Profile>>();
                    var config = new MapperConfiguration(x =>
                    {
                        // Load in all our AutoMapper profiles that have been registered
                        foreach (var profile in profiles)
                        {
                            x.AddProfile(profile);
                        }
                    });
                    return config;
                }).SingleInstance() // We only need one instance
                .AutoActivate() // Create it on ContainerBuilder.Build()
                .AsSelf(); // Bind it to its own type

            // HACK: IComponentContext needs to be resolved again as 'tempContext' is only temporary.
            // See http://stackoverflow.com/a/5386634/718053 
            builder.Register(tempContext =>
            {
                var ctx = tempContext.Resolve<IComponentContext>();
                var config = ctx.Resolve<MapperConfiguration>();

                // Create our mapper using our configuration above
                return config.CreateMapper();
            }).As<IMapper>(); // Bind it to the IMapper interface
        }
    }
}
