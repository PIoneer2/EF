namespace EF.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using EF.Data;
    using EF.Core;
    using EF.Core.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<EF.Data.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EF.Data.EFDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        //private UnitOfWork unitOfWork = new UnitOfWork();
        //private Repository<Transactions> transactionsRepository;
        //transactionsRepository = unitOfWork.Repository<Transactions>();

        //context.TranactionType.AddOrUpdate(
        //          p => p.Name,
        //          new TranactionType { Name = "Supply" },
        //          new TranactionType { Name = "Order" }
        //        );

        //    context.PermissionsSet.AddOrUpdate(
        //          p => p.Name,
        //          new Permissions { Name = "Write" },
        //          new Permissions { Name = "Read" }
        //        );

        //    context.RolesSet.AddOrUpdate(
        //          p => p.Name,
        //          new Roles { Name = "Admin" },
        //          new Roles { Name = "Director" },
        //          new Roles { Name = "Manager" }
        //        );

        //    context.SaveChanges();

        //    context.PermissionSets.AddOrUpdate(
        //          p => p.RolesId,
        //          new PermissionSet { RolesId = 1, PermissionsId = 1 },
        //          new PermissionSet { RolesId = 2, PermissionsId = 2 },
        //          new PermissionSet { RolesId = 3, PermissionsId = 1 }
        //        );
        //    context.SaveChanges();
        //    context.UsersSet.AddOrUpdate(
        //          p => p.Login,
        //          new Users { Login = "1@2.com", Password = "123456", RolesId = 1 },
        //          new Users { Login = "2@2.com", Password = "1256", RolesId = 2 },
        //          new Users { Login = "3@2.com", Password = "12", RolesId = 3 }
        //        );
        //    context.SaveChanges();

        //    context.TransactionsSet.AddOrUpdate(
        //          p => p.Date,
        //          new Transactions { Description = "", Date = System.DateTime.Parse("2016-05-10"), TranactionTypeId = 1, UsersId = 1 },
        //          new Transactions { Description = "1", Date = System.DateTime.Parse("2016-05-12"), TranactionTypeId = 1, UsersId = 3 },
        //          new Transactions { Description = "2", Date = System.DateTime.Parse("2016-05-13"), TranactionTypeId = 2, UsersId = 3 }
        //        );

        //    context.SaveChanges();
        }
    }
}
