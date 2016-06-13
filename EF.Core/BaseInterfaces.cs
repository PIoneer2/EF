using EF.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core
{
    public interface IDbContext
    {

    }

    public interface ICustomUserStore
    {

    }

    public interface ICustomRoleStore
    {

    }

    public interface IUserManager
    {
        Task<User> FindByIdAsync(long userId);
        
    }

    public interface IRepository<T> where T : class, IBaseEntity
    {
        IQueryable<T> Table { get; }

        T GetById(long id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }

    public interface IUnitOfWork : IDisposable
    {
        new void Dispose();
        void Dispose(bool disposing);
        void Save();
        IRepository<T> Repository<T>() where T : class, IBaseEntity;
    }

    public interface IBusinessLogic
    {
        IEnumerable<T> Index<T>(IRepository<T> parametricRepository, long id) where T : class, IBaseEntity;
        T Details<T>(IRepository<T> parametricRepository, long id) where T : class, IBaseEntity;
        T CreateBlankModel<T>(IRepository<T> parametricRepository, long id) where T : class, IBaseEntity;
        T CreateEditInPost<T>(T mdl, IRepository<T> parametricRepository, long id) where T : class, IBaseEntity;
        void ConfirmDelete<T>(IRepository<T> parametricRepository, long id) where T : class, IBaseEntity;
        void Transform(IBaseEntity toObject, BaseEntity fromObject, bool copyId = false);
    }
}
