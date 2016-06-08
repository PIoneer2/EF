using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF.Core;
using EF.Core.Data;
using EF.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using EF.Web.BusinessLogic;

namespace EF.Web.SLocator
{
    public static class EFServiceLocator
    {
        public static T GetService<T>() where T : class
        {
            if (typeof(T) == typeof(IDbContext))
            {
                return new EFDbContext() as T;
            }
            if (typeof(T) == typeof(EFDbContext))
            {
                return new EFDbContext() as T;
            }
            if (typeof(T) == typeof(IDisposable))
            {
                return new EFDbContext() as T;
            }
            if (typeof(T) == typeof(ICustomUserStore))
            {
                return new CustomUserStore(EFServiceLocator.GetService<IDbContext>() as EFDbContext) as T;
            }
            if (typeof(T) == typeof(ICustomRoleStore))
            {
                return new CustomRoleStore(EFServiceLocator.GetService<IDbContext>() as EFDbContext) as T;
            }
            if (typeof(T) == typeof(IUserManager))
            {
                return new CustomUserManager(EFServiceLocator.GetService<ICustomUserStore>() as CustomUserStore) as T;
            }
            if (typeof(T) == typeof(IRepository<IBaseEntity>))
            {
                return new EFRepository<IBaseEntity>(EFServiceLocator.GetService<IDbContext>() as EFDbContext) as T;
            }
            if (typeof(T) == typeof(IUnitOfWork))
            {
                return new EFUnitOfWork(EFServiceLocator.GetService<IDbContext>() as EFDbContext) as T;
            }
            if (typeof(T) == typeof(IBusinessLogic))
            {
                return new EFBusinessLogic() as T;
            }
            /*
            if (typeof(T) == typeof(ApplicationUserManager))
            {
                return ApplicationUserManager.Create;
            }*/

            else
            {
                throw new NotImplementedException();
            }
        }
    }
}