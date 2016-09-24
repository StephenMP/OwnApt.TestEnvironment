using Microsoft.EntityFrameworkCore;
using OwnApt.TestEnvironment.Environment.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEnvironment.TestResource.Objects;
using Xunit;

namespace TestEnvironment.Tests.Component.Environment
{
    public class SqlConfigurationSteps
    {
        #region Private Fields

        private DbContextOptions<TestDbContext> dbContextOptions;
        private SqlConfiguration sqlConfiguration;
        private TestEntity[] testEntities;

        #endregion Private Fields

        #region Internal Methods

        internal void GivenIAddedADbContext()
        {
            this.sqlConfiguration.AddSqlContext<TestDbContext>();
        }

        internal void GivenIHaveADbContext()
        {
            this.sqlConfiguration.AddSqlContext<TestDbContext>();
        }

        internal void GivenIHaveASqlConfiguration()
        {
            this.sqlConfiguration = new SqlConfiguration();
        }

        internal void GivenIHaveDataToImport()
        {
            this.testEntities = new TestEntity[]
            {
                new TestEntity { Id = new Random().Next(), Value = Guid.NewGuid().ToString() }
            };
        }

        internal void GivenIHaveMultipleDataToImport()
        {
            this.testEntities = new TestEntity[]
            {
                new TestEntity { Id = 1, Value = Guid.NewGuid().ToString() },
                new TestEntity { Id = 2, Value = Guid.NewGuid().ToString() },
                new TestEntity { Id = 3, Value = Guid.NewGuid().ToString() },
                new TestEntity { Id = 4, Value = Guid.NewGuid().ToString() },
                new TestEntity { Id = 5, Value = Guid.NewGuid().ToString() }
            };
        }

        internal void ThenICanVerifyICanAddDbContext()
        {
            Assert.NotNull(this.sqlConfiguration.SqlDbContextOptions<TestDbContext>());
        }

        internal void ThenICanVerifyICanImportMultipleSqlDataAsync()
        {
            var entities = this.ReadEntities();

            foreach (var entity in entities)
            {
                var testEntity = this.testEntities.FirstOrDefault(e => e.Id == entity.Id);
                Assert.NotNull(testEntity);
                Assert.Equal(entity.Id, testEntity.Id);
                Assert.Equal(entity.Value, testEntity.Value);
            }
        }

        internal void ThenICanVerifyICanImportSqlDataAsync()
        {
            var testEntity = this.testEntities.FirstOrDefault();
            var entity = this.ReadEntity(testEntity.Id);
            Assert.NotNull(entity);
            Assert.Equal(testEntity.Id, entity.Id);
            Assert.Equal(testEntity.Value, entity.Value);
        }

        internal void ThenICanVerifyICanNewUpSqlConfiguration()
        {
            Assert.NotNull(this.sqlConfiguration);
        }

        internal void ThenICanVerifyICCanRetrieveDbContextOptions()
        {
            Assert.NotNull(this.dbContextOptions);
            Assert.IsType<DbContextOptions<TestDbContext>>(this.dbContextOptions);
        }

        internal void WhenIAddADbContext()
        {
            this.sqlConfiguration.AddSqlContext<TestDbContext>();
        }

        internal async Task WhenIImportSqlDataAsync()
        {
            await this.sqlConfiguration.ImportSqlDataAsync<TestDbContext, TestEntity>(this.testEntities);
        }

        internal void WhenIRetrieveADbContext()
        {
            this.dbContextOptions = this.sqlConfiguration.SqlDbContextOptions<TestDbContext>();
        }

        #endregion Internal Methods

        #region Private Methods

        private IEnumerable<TestEntity> ReadEntities()
        {
            using (var context = new TestDbContext(this.sqlConfiguration.SqlDbContextOptions<TestDbContext>()))
            {
                return context.Test.ToList();
            }
        }

        private TestEntity ReadEntity(int id)
        {
            using (var context = new TestDbContext(this.sqlConfiguration.SqlDbContextOptions<TestDbContext>()))
            {
                return context.Test.FirstOrDefault(e => e.Id == id);
            }
        }

        #endregion Private Methods
    }
}
