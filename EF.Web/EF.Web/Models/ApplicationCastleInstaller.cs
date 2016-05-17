using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using EF.Data;

namespace EF.Web.Models
{
    public class ApplicationCastleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // reg components: all types Repository
            container.Register(Component.For(typeof(Repository<>)));

            //container.Register(Component.For(typeof(Repository<>))
            //.Forward(typeof(Repository<>))
            //.ImplementedBy(typeof(Repository<>))
            //.LifestylePerWebRequest());

            // setting prop
            var repo = container.Resolve(typeof(Repository<>));
            //repo.context = new EFDbContext();
            container.Register(
            Component.For(typeof(Repository<>))
            .DependsOn(Property.ForKey("context").Eq(new EFDbContext())));

            // reg Ctrlrs
            var controllers = Assembly.GetExecutingAssembly()
                .GetTypes().Where(x => x.BaseType == typeof(Controller)).ToList();
            foreach (var controller in controllers)
            {
                container.Register(Component.For(controller).LifestylePerWebRequest());
            }
        }
    }
}