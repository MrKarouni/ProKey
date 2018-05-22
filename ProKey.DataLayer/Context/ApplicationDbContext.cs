using ProKey.DomainClasses;
using ProKey.DomainClasses.EntityConfiguration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProKey.DataLayer.Context
{
    public class ApplicationDbContext :DbContext, IUnitOfWork
    {
        public ApplicationDbContext()
            :base("ProKeyConnectionString")
        {
        }

        public DbSet<User> Users { set; get; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Configurations.Add(new UserConfig());
            builder.Configurations.Add(new UserRoleConfig());
            builder.Configurations.Add(new UserTokenConfig());

            base.OnModelCreating(builder);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public int SaveAllChanges()
        {
            return base.SaveChanges();
        }

        public IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            return ((DbSet<TEntity>)this.Set<TEntity>()).AddRange(entities);
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public void ForceDatabaseInitialize()
        {
            this.Database.Initialize(force: true);
        }
    }
}
