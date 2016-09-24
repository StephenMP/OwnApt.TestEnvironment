using MongoDB.Driver;
using OwnApt.TestEnvironment.Environment.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEnvironment.TestResource.Objects;
using Xunit;

namespace TestEnvironment.Tests.Component.Environment
{
    public class MongoConfigurationSteps : IDisposable
    {
        #region Private Fields

        private const string CollectionName = "Test";
        private const string DbName = "Core";
        private bool disposedValue;
        private MongoConfiguration mongoConfiguration;
        private TestEntity[] testEntities;

        #endregion Private Fields

        #region Private Properties

        private IMongoCollection<TestEntity> TestCollection => this.mongoConfiguration.MongoClient.GetDatabase(DbName).GetCollection<TestEntity>(CollectionName);

        #endregion Private Properties

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Internal Methods

        internal void GivenIHaveAMongoConfiguration()
        {
            this.mongoConfiguration = new MongoConfiguration();
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

        internal void ThenICanVerifyICanImportMongoDataAsync()
        {
            var testEntity = this.testEntities.FirstOrDefault();
            var entity = this.ReadEntity(testEntity.Id);
            Assert.NotNull(entity);
            Assert.Equal(testEntity.Id, entity.Id);
            Assert.Equal(testEntity.Value, entity.Value);
        }

        internal void ThenICanVerifyICanImportMultipleMongoDataAsync()
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

        internal void ThenICanVerifyICanNewUpMongoConfiguration()
        {
            Assert.NotNull(this.mongoConfiguration);
        }

        internal async Task WhenIImportMongoDataAsync()
        {
            await this.mongoConfiguration.ImportMongoDataAsync(DbName, CollectionName, this.testEntities);
        }

        #endregion Internal Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.mongoConfiguration?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private IEnumerable<TestEntity> ReadEntities()
        {
            return this.TestCollection.Find(e => e.Id > 0).ToList();
        }

        private TestEntity ReadEntity(int id)
        {
            return this.TestCollection.Find(e => e.Id == id).FirstOrDefault();
        }

        #endregion Private Methods
    }
}
