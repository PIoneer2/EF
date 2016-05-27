using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EF.Web.Models;
using Castle.Windsor;
using System.Data.Entity;
using EF.Data;

namespace EF.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new WindsorContainer();
            // reg Ctrlrs & components
            container.Install(new ApplicationCastleInstaller());
            // my CtrlFactory
            var castleControllerFactory = new CastleControllerFactory(container);
            // Добавляем фабрику контроллеров для обработки запросов
            ControllerBuilder.Current.SetControllerFactory(castleControllerFactory);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //инициализатор БД для ASPNetUser
            //Database.SetInitializer<EFDbContext>(new ASPNetUsersStarter());

            //инициализатор БД для MembershipReboot
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DefaultMembershipRebootDatabase, BrockAllen.MembershipReboot.Ef.Migrations.Configuration>());
        }
    }
}
