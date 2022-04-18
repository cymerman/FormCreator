using Autofac;
using Microsoft.Extensions.Configuration;

namespace FormsCreator.Domain.Db
{
    public class DbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x =>
                {
                    var configuration = x.Resolve<IConfiguration>();

                    return configuration.GetSection(DbConfig.Path).Get<DbConfig>();
                })
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}