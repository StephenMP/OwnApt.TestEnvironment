using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace OwnApt.TestEnvironment.Environment
{
    public class TestingEnvironment
    {
        #region Private Fields

        private IServiceProvider serviceProvider;

        #endregion Private Fields

        #region Public Constructors

        public TestingEnvironment()
        {
            this.sqlDbContextOptions = new Dictionary<Type, DbContextOptions>();
            this.serviceProvider = new ServiceCollection()
                 .AddEntityFrameworkInMemoryDatabase()
                 .BuildServiceProvider();
        }

        #endregion Public Constructors

        #region Public Properties

        private Dictionary<Type, DbContextOptions> sqlDbContextOptions;

        #endregion Public Properties

        #region Public Methods

        public void AddSqlContext<TDbContext>() where TDbContext : DbContext
        {
            var builder = new DbContextOptionsBuilder<TDbContext>()
                                .UseInMemoryDatabase()
                                .UseInternalServiceProvider(serviceProvider);

            this.sqlDbContextOptions.Add(typeof(TDbContext), builder.Options);
        }

        public DbContextOptions<TDbContext> SqlDbContextOptions<TDbContext>() where TDbContext : DbContext
        {
            return this.sqlDbContextOptions[typeof(TDbContext)] as DbContextOptions<TDbContext>;
        }

        #endregion Public Methods
    }
}
