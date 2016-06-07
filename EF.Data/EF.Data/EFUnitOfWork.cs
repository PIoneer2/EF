using System;
using System.Collections.Generic;
using EF.Core;

namespace EF.Data
{
    

    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly EFDbContext context;
        private bool disposed;
        private Dictionary<string, object> repositories;

        public EFUnitOfWork(EFDbContext context)
        {
            this.context = context;
        }

        public EFDbContext ContexGetter()
        {
            return this.context;
        }
        /*
        public EFUnitOfWork()
        {

        }
        */
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public IRepository<T> Repository<T>() where T : class, IBaseEntity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(EFRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);
                repositories.Add(type, repositoryInstance);
            }
            return (EFRepository<T>)repositories[type];
        }
    }
}