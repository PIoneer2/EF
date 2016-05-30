using System.Data.Entity;
using EF.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EF.Core.Data;


namespace EF.Web.Models
{
    public class ASPNetUsersStarter : DropCreateDatabaseAlways<EFDbContext>
    {
        protected new void Seed(EFDbContext context)
        {
            var UserManager = new UserManager<User, long>(new CustomUserStore(context));
            var RoleManager = new RoleManager<Role, long>(new CustomRoleStore(context));
            string name = "Admin";
            string password = "123456";
            if (!RoleManager.RoleExists(name))
            {
                var roleresult = RoleManager.Create(new Role(name));
            }
            var user = new User();
            user.UserName = name;
            var adminresult = UserManager.Create(user, password);
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, name);
            }
            base.Seed(context);
        }
    }
}