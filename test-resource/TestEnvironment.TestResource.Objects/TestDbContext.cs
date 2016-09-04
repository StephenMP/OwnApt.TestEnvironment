using Microsoft.EntityFrameworkCore;

namespace TestEnvironment.TestResource.Objects
{
    public class TestDbContext : DbContext
    {
        #region Public Constructors

        public TestDbContext(DbContextOptions<TestDbContext> contextOptions) : base(contextOptions)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public DbSet<TestEntity> Test { get; set; }

        #endregion Public Properties
    }
}
