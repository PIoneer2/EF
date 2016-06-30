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
            if (typeof(T) == typeof(DbContext))
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
            if (typeof(T) == typeof(CustomUserStore))
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
            if (typeof(T) == typeof(CustomUserManager))
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
            if (typeof(T) == typeof(Transactions))
            {
                return new Transactions() as T;
            }
            if (typeof(T) == typeof(Goods))
            {
                return new Goods() as T;
            }
            if (typeof(T) == typeof(GoodsInTransaction))
            {
                return new GoodsInTransaction() as T;
            }
            if (typeof(T) == typeof(GoodsInWarehauses))
            {
                return new GoodsInWarehauses() as T;
            }
            if (typeof(T) == typeof(Restrictions))
            {
                return new Restrictions() as T;
            }
            if (typeof(T) == typeof(RestrictionsSet))
            {
                return new RestrictionsSet() as T;
            }
            if (typeof(T) == typeof(Role))
            {
                return new Role() as T;
            }
            if (typeof(T) == typeof(Sizes))
            {
                return new Sizes() as T;
            }
            if (typeof(T) == typeof(TranactionType))
            {
                return new TranactionType() as T;
            }
            if (typeof(T) == typeof(TypeOfStorage))
            {
                return new TypeOfStorage() as T;
            }
            if (typeof(T) == typeof(User))
            {
                return new User() as T;
            }
            if (typeof(T) == typeof(UserRole))
            {
                return new Sizes() as T;
            }
            if (typeof(T) == typeof(WarehousesPlaces))
            {
                return new WarehousesPlaces() as T;
            }
            if (typeof(T) == typeof(TransactionDTO))
            {
                return new TransactionDTO() as T;
            }
            if (typeof(T) == typeof(List<TransactionDTO>))
            {
                return new List<TransactionDTO>() as T;
            }
            /*
             if (typeof(T) == typeof())
            {
                return new () as T;
            }
             */
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}