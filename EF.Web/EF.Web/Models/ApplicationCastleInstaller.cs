using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using EF.Data;
using System;
using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using BrockAllen.MembershipReboot.WebHost;
using EF.Web.Controllers;
using System.Data.Entity;
using EF.Core;

namespace EF.Web.Models
{
    public class ApplicationCastleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //установка параметра для потроения UnitOfWork
            //container.Register(Component.For<IDisposable>().ImplementedBy<UnitOfWork>().DynamicParameters((r, k) => { k["context"] = new EFDbContext(); }).LifestylePerWebRequest());
            
            //установка параметра для потроения Repository<T>
            //container.Register(Component.For(typeof(Repository<>)).DynamicParameters((r, k) => { k["context"] = new EFDbContext(); }).LifestylePerWebRequest());

            //установка параметра для коннекшена МембершипРебут
            /*
            container.Register(Component.For<IUserAccountRepository>().ImplementedBy<DefaultUserAccountRepository>()
                .DynamicParameters((r, k) => { k["name"] = "DefaultConnection"; }).LifestylePerWebRequest());
                */

            //UserAccount   ***
            //IUserAccountRepository
            //DefaultUserAccountRepository 
            //IGroupQuery    aka Roles

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

            //(3-4)компоненты для MembershipReboot
            /*
            container.Register(Component.For<UserAccountService>().LifestylePerWebRequest());
            container.Register(Component.For<AuthenticationService>().ImplementedBy<SamAuthenticationService>().LifestylePerWebRequest());
            container.Register(Component.For<IUserAccountQuery, IUserAccountRepository>().ImplementedBy<DefaultUserAccountRepository>().LifestylePerWebRequest());
            */
            //container.Register(Component.For<IUserAccountRepository>().ImplementedBy<DefaultUserAccountRepository>().LifestylePerWebRequest());


            //регистрация всех порожденных классов Controller из сборки, в которой есть класс AccountController
            container.Register(Classes.FromAssemblyContaining<AccountController>().BasedOn<Controller>().LifestylePerWebRequest());//регистрирует HomeController
            container.Register(Classes.FromAssemblyContaining<EFUnitOfWork>().BasedOn<EFUnitOfWork>().LifestylePerWebRequest());
            container.Register(Classes.FromAssemblyContaining<EFRepository<IBaseEntity>>().BasedOn<EFRepository<IBaseEntity>>().LifestylePerWebRequest());
            container.Register(Classes.FromAssemblyContaining<EFDbContext>().BasedOn<EFDbContext>().LifestylePerWebRequest());
            //container.Register(Classes.FromAssemblyContaining<EFUnitOfWork>().BasedOn<IUnitOfWork>().LifestylePerWebRequest());
            //container.Register(Classes            .FromAssemblyInThisApplication()            .BasedOn<IController>()            .WithServiceAllInterfaces()            .LifestylePerWebRequest());

            //или
            //регистрация всех классов из этой сборки
            /*
            container.Register(Classes.FromThisAssembly().Pick().WithServiceAllInterfaces().LifestylePerWebRequest());
            */

            //или 
            //более старым методом через рефлексию
            /*
            var controllers = Assembly.GetExecutingAssembly()
                .GetTypes().Where(x => x.BaseType == typeof(Controller)).ToList();

            foreach (var controller in controllers)
            {
                if ("AccountController" != controller.Name && "HomeController" != controller.Name && "ManageController" != controller.Name && "UserAccountController" != controller.Name) {
                    //container.Register(Component.For(controller).DependsOn(Property.ForKey("unitOfWork").Eq(new UnitOfWork(new EFDbContext()))).LifestylePerWebRequest());

                    container.Register(Component.For(controller)
                    .DynamicParameters((r, k) => { k["tmpUnit"] = new UnitOfWork(new EFDbContext()); }).LifestylePerWebRequest());

                }
                else {
                    container.Register(Component.For(controller).LifestylePerWebRequest());
                }
            }*/
        }
    }
}