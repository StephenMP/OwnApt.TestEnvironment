using OwnApt.TestEnvironment.Environment;
using System;
using System.Linq;
using TestEnvironment.TestResource.Objects;
using Xunit;

namespace TestEnvironment.Tests.Component.Environment
{
    public class TestingEnvironmentSteps : IDisposable
    {
        #region Private Fields

        private TestDbContext context;
        private bool disposedValue = false;
        private TestEntity testEntity;
        private TestingEnvironment testingEnvironment;
        private TestEntity[] testEntities;

        #endregion Private Fields

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Internal Methods

        internal void GivenIHaveATestingEnvironment()
        {
            this.testingEnvironment = new TestingEnvironment();
        }

        internal void GivenIHaveDataToCreate()
        {
            this.testEntity = new TestEntity { Value = Guid.NewGuid().ToString() };
        }

        internal void ThenICanVerifyICanAddSqlContext()
        {
            Assert.NotNull(this.testingEnvironment.SqlDbContextOptions<TestDbContext>());
            Assert.NotNull(this.context);
        }

        internal void ThenICanVerifyICanCreateAndReadMultipleSqlData()
        {
            this.ThenICanVerifyICanAddSqlContext();

            Assert.True(this.context.Test.Any());

            foreach(var testEntityObj in this.testEntities)
            {
                var entity = this.context.Test.FirstOrDefault(e => e.Value == testEntityObj.Value);
                Assert.NotNull(entity);
                Assert.Equal(testEntityObj.Value, entity.Value);
            }
        }

        internal void WhenICreateMultipleSqlData()
        {
            foreach(var entity in this.testEntities)
            {
                this.context.Add(entity);
            }

            this.context.SaveChanges();
        }

        internal void GivenIHaveMultipleDataToCreate()
        {
            this.testEntities = new TestEntity[]
            {
                new TestEntity { Value = Guid.NewGuid().ToString() },
                new TestEntity { Value = Guid.NewGuid().ToString() },
                new TestEntity { Value = Guid.NewGuid().ToString() },
                new TestEntity { Value = Guid.NewGuid().ToString() },
                new TestEntity { Value = Guid.NewGuid().ToString() }
            };
        }

        internal void ThenICanVerifyICanCreateAndReadSqlData()
        {
            this.ThenICanVerifyICanAddSqlContext();
            Assert.True(this.context.Test.Any());

            var entity = this.context.Test.FirstOrDefault(e => e.Value == this.testEntity.Value);
            Assert.NotNull(entity);
            Assert.Equal(this.testEntity.Value, entity.Value);
        }

        internal void WhenIAddASqlContext()
        {
            this.testingEnvironment.AddSqlContext<TestDbContext>();
            this.context = new TestDbContext(this.testingEnvironment.SqlDbContextOptions<TestDbContext>());
        }

        internal void WhenICreateSqlData()
        {
            this.context.Test.Add(this.testEntity);
            this.context.SaveChanges();
        }

        #endregion Internal Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    this.context?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
