using Autofac;
using Autofac.Integration.Mvc;
using TeamManager.DataLayer.Abstract;
using TeamManager.DataLayer.Concrete;
using TeamManager.Logic;
using System.Reflection;
using System.Web.Mvc;

namespace TeamManager.CompositionRoot
{
    public static class DependencyInjector
    {
        public static IDependencyResolver GetAutofacResolver(Assembly executingAssembly)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(executingAssembly);
            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<PlayersLogic>();
            builder.RegisterType<TeamsLogic>();

            var container = builder.Build();

            return new AutofacDependencyResolver(container);
        }
    }
}
