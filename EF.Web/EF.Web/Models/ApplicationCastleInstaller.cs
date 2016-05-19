using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using EF.Data;
using System;
using EF.Web.Controllers;

namespace EF.Web.Models
{
    public class ApplicationCastleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //установка параметра для потроения UnitOfWork
            container.Register(Component.For<IDisposable>().ImplementedBy<UnitOfWork>()
                .DynamicParameters((r, k) => { k["context"] = new EFDbContext(); }).LifestylePerWebRequest());
            //установка параметра для потроения Repository<T>
            container.Register(Component.For(typeof(Repository<>))
                .DynamicParameters((r, k) => { k["context"] = new EFDbContext(); }).LifestylePerWebRequest());

            //установка свойства
            //container.Register(Component.For<Controller>()
            //    .DependsOn(Property.ForKey("unitOfWork").Eq(new UnitOfWork(new EFDbContext())))
            //);

            //при создании
            //container.Register(
            //Component.For(typeof(Controller))
            //.OnCreate((kernel, instance) => instance.unitOfWork = new UnitOfWork(new EFDbContext()))
            //);

            /*
            //не работает 
            container.Register(
            Component.For(typeof(Controller))
           .DependsOn(Property.ForKey("unitOfWork").Eq(new UnitOfWork()))
           );

            container.Register(
            Component.For(typeof(UnitOfWork))
            .DependsOn(Property.ForKey("context").Eq(new EFDbContext())));
            
            container.Register(
            Component.For(typeof(Repository<>))
            .DependsOn(Property.ForKey("context").Eq(new EFDbContext())));
            
            container.Register(
            Component.For(typeof(UnitOfWork))
            .DependsOn(Property.ForKey("context").Eq(new EFDbContext())));
            
            container.Register(
            Component.For(typeof(Controller))
            .OnCreate((kernel, instance) => instance.unitOfWork = new EFDbContext())
            );
            container.Register(
           Component.For(typeof(Controller))
           .DependsOn(Property.ForKey("unitOfWork").Eq(new UnitOfWork()))
           );
             */


            // reg Ctrlrs
            var controllers = Assembly.GetExecutingAssembly()
                .GetTypes().Where(x => x.BaseType == typeof(Controller)).ToList();

            foreach (var controller in controllers)
            {

                /*
                container.Register(
                Component.For(controller)
                .DependsOn(Property.ForKey("unitOfWork").Eq(new UnitOfWork(new EFDbContext()))).LifestylePerWebRequest());
                */

                if ("AccountController" != controller.Name && "HomeController" != controller.Name && "ManageController" != controller.Name) {
                    //container.Register(Component.For(controller).DependsOn(Property.ForKey("unitOfWork").Eq(new UnitOfWork(new EFDbContext()))).LifestylePerWebRequest());

                    container.Register(Component.For(controller)
                    .DynamicParameters((r, k) => { k["tmpUnit"] = new UnitOfWork(new EFDbContext()); }).LifestylePerWebRequest());

                }
                else {
                    container.Register(Component.For(controller).LifestylePerWebRequest());
                }
                /*
                */
            }

            //instancing
        }
    }
}