using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwnApt.TestEnvironment.Environment.Configuration
{
    public class SqlConfiguration
    {
        #region Private Fields

        private readonly IServiceProvider serviceProvider;
        private readonly Dictionary<Type, IDbContextOptions> sqlDbContextOptions;

        #endregion Private Fields

        #region Public Constructors

        public SqlConfiguration()
        {
            this.sqlDbContextOptions = new Dictionary<Type, IDbContextOptions>();
            this.serviceProvider = new ServiceCollection()
                 .AddEntityFrameworkInMemoryDatabase()
                 .BuildServiceProvider();
        }

        #endregion Public Constructors

        #region Public Methods

        public void AddSqlContext<TDbContext>() where TDbContext : DbContext
        {
            var builder = new DbContextOptionsBuilder<TDbContext>()
                                .UseInMemoryDatabase()
                                .UseInternalServiceProvider(serviceProvider);

            this.sqlDbContextOptions.Add(typeof(TDbContext), builder.Options);
        }

        public async Task ImportDataAsync<TDbContext, TEntity>(IEnumerable<TEntity> entity) where TDbContext : DbContext where TEntity : class
        {
            using (var context = (TDbContext)Activator.CreateInstance(typeof(TDbContext), this.SqlDbContextOptions<TDbContext>()))
            {
                context.Set<TEntity>().AddRange(entity);
                await context.SaveChangesAsync();
            }
        }

        public DbContextOptions<TDbContext> SqlDbContextOptions<TDbContext>() where TDbContext : DbContext
        {
            return this.sqlDbContextOptions[typeof(TDbContext)] as DbContextOptions<TDbContext>;
        }

        #endregion Public Methods
    }
}
