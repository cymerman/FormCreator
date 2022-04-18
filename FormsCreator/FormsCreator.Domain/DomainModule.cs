using System.Reflection;
using Autofac;
using FormsCreator.Domain.ContextInfo;
using FormsCreator.Domain.Core.Base.Queries;
using FormsCreator.Domain.Core.ContextInfo;
using FormsCreator.Domain.Db;
using FormsCreator.Domain.Security;
using Module = Autofac.Module;

namespace FormsCreator.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DbModule>();
           
            RegisterQueryAndCommandHandlers(builder);

            builder.Register(x => new QueryProcessor(x.Resolve<IComponentContext>()))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.Register(x => new CommandExecutor(x.Resolve<IComponentContext>(), x.Resolve<ICurrentUserProvider>()))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<CurrentUserProvider>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<AuthService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
        
        /// <summary>
        ///  Metoda rejestruje serwisy zapytań i poleceń
        /// </summary>
        /// <param name="builder">Budowniczy kontenera IOC</param>
        private static void RegisterQueryAndCommandHandlers(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => typeof(IHandler).IsAssignableFrom(t))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
