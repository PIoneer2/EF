using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using EF.Core;
using EF.Core.Data;
using System.Web;
using EF.Web.SLocator;

namespace EF.WebApi
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : EF.Web.ApplicationUserManager
    {
        public ApplicationUserManager(CustomUserStore store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            //var manager = HttpContext.Current.GetOwinContext().GetUserManager<CustomUserManager>();
            var manager = new ApplicationUserManager(EFServiceLocator.GetService<CustomUserStore>());//new CustomUserStore(context.Get<EFDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<EF.Core.Data.User, long>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<EF.Core.Data.User, long>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
