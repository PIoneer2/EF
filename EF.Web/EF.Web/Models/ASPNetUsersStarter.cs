using System.Data.Entity;
using EF.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EF.Core.Data;


namespace EF.Web.Models
{
    public class ASPNetUsersStarter : DropCreateDatabaseAlways<EFDbContext>
    {
        protected void Seed(ApplicationDbContext context)
        {
            var UserManager = new UserManager<ApplicationUser, long>(new CustomUserStore(context));
            var RoleManager = new RoleManager<CustomRole, long>(new CustomRoleStore(context));
            string name = "Admin";
            string password = "123456";
            if (!RoleManager.RoleExists(name))
            {
                var roleresult = RoleManager.Create(new CustomRole(name));
            }
            var user = new ApplicationUser();
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