using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using EF.Core;
using EF.Core.Data;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;

namespace EF.Data
{
    public class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("name=DefaultConnection")
        {

        }

        //only for I-variant
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class, IBaseEntity
        {
            return base.Set<TEntity>();
        }

        public class EFDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EFDbContext>
        //public class EFDbInitializer : DropCreateDatabaseAlways<EFDbContext>
        {
            protected override void Seed(EFDbContext context)
            {

            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
           .Where(type => !String.IsNullOrEmpty(type.Namespace))
           .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);

        }

        //public System.Data.Entity.DbSet<EF.Core.Data.Goods> Goods { get; set; }
        //public DbSet <Order> Orders { get; set; }

    }
}