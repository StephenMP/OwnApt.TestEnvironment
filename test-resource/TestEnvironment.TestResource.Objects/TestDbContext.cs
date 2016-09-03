using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestEnvironment.TestResource.Objects
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> contextOptions) : base(contextOptions) { }

        public DbSet<TestEntity> Test { get; set; }
    }
}
