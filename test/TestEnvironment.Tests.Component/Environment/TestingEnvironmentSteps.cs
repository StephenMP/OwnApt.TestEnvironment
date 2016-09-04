using MongoDB.Driver;
using OwnApt.TestEnvironment.Environment;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TestEnvironment.TestResource.Api;
using TestEnvironment.TestResource.Objects;
using Xunit;

namespace TestEnvironment.Tests.Component.Environment
{
    public class TestingEnvironmentSteps : IDisposable
    {
        #region Private Fields

        private TestDbContext context;
        private bool disposedValue;
        private IMongoClient mongoClient;
        private TestEntity[] testEntities;
        private TestEntity testEntity;
        private TestingEnvironment testingEnvironment;
        private HttpResponseMessage webServiceResponse;

        #endregion Private Fields

        #region Private Properties

        private IMongoCollection<TestEntity> TestCollection => this.mongoClient.GetDatabase("Core").GetCollection<TestEntity>("Test");

        #endregion Private Properties

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

        internal void ThenICanVerifyICanAddMongo()
        {
            Assert.NotNull(this.mongoClient);
        }

        internal void ThenICanVerifyICanAddSqlContext()
        {
            Assert.NotNull(this.testingEnvironment.SqlDbContextOptions<TestDbContext>());
            Assert.NotNull(this.context);
        }

        internal void ThenICanVerifyICanAddWebService()
        {
            Assert.NotNull(this.testingEnvironment.WebService<TestStartup>());
        }

        internal void ThenICanVerifyICanCreateAndReadMongoData()
        {
            var mongoReadEntity = this.TestCollection.Find(p => p.Id == this.testEntity.Id).FirstOrDefault();
            Assert.NotNull(mongoReadEntity);
            Assert.Equal(testEntity.Id, mongoReadEntity.Id);
            Assert.Equal(testEntity.Value, mongoReadEntity.Value);
        }

        internal void ThenICanVerifyICanCreateAndReadMultipleMongoData()
        {
            foreach (var mongoTestEntity in testEntities)
            {
                var mongoReadEntity = this.TestCollection.Find(p => p.Id == mongoTestEntity.Id).FirstOrDefault();
                Assert.NotNull(mongoReadEntity);
                Assert.Equal(mongoTestEntity.Id, mongoReadEntity.Id);
                Assert.Equal(mongoTestEntity.Value, mongoReadEntity.Value);
            }
        }

        internal void ThenICanVerifyICanCreateAndReadMultipleSqlData()
        {
            this.ThenICanVerifyICanAddSqlContext();

            Assert.True(this.context.Test.Any());

            foreach (var testEntityObj in this.testEntities)
            {
                var entity = this.context.Test.FirstOrDefault(e => e.Value == testEntityObj.Value);
                Assert.NotNull(entity);
                Assert.Equal(testEntityObj.Value, entity.Value);
            }
        }

        internal void ThenICanVerifyICanCreateAndReadSqlData()
        {
            this.ThenICanVerifyICanAddSqlContext();
            Assert.True(this.context.Test.Any());

            var entity = this.context.Test.FirstOrDefault(e => e.Value == this.testEntity.Value);
            Assert.NotNull(entity);
            Assert.Equal(this.testEntity.Value, entity.Value);
        }

        internal async Task ThenICanVerifyIReceivedAResultAsync()
        {
            Assert.NotNull(this.webServiceResponse);
            Assert.True(this.webServiceResponse.IsSuccessStatusCode);

            var content = await this.webServiceResponse.Content.ReadAsStringAsync();
            Assert.NotNull(content);
            Assert.Contains("true", content);
        }

        internal void WhenIAddASqlContext()
        {
            this.testingEnvironment.AddSqlContext<TestDbContext>();
            this.context = new TestDbContext(this.testingEnvironment.SqlDbContextOptions<TestDbContext>());
        }

        internal void WhenIAddAWebService()
        {
            this.testingEnvironment.AddWebService<TestStartup>();
        }

        internal void WhenIAddMongo()
        {
            this.testingEnvironment.AddMongo();
            this.mongoClient = this.testingEnvironment.MongoClient();
        }

        internal void WhenICreateMongoData()
        {
            testEntity.Id = new Random().Next();
            this.TestCollection.InsertOne(this.testEntity);
        }

        internal void WhenICreateMultipleMongoData()
        {
            var random = new Random();
            foreach (var entity in testEntities)
            {
                entity.Id = random.Next();
            }

            this.TestCollection.InsertMany(this.testEntities);
        }

        internal void WhenICreateMultipleSqlData()
        {
            foreach (var entity in this.testEntities)
            {
                this.context.Add(entity);
            }

            this.context.SaveChanges();
        }

        internal void WhenICreateSqlData()
        {
            this.context.Test.Add(this.testEntity);
            this.context.SaveChanges();
        }

        internal async Task WhenIUseWebServiceAsync()
        {
            using (var client = new HttpClient())
            {
                var requestUri = $"{testingEnvironment.WebService<TestStartup>().BaseUri.AbsoluteUri.TrimEnd('/')}/api/test";
                this.webServiceResponse = await client.GetAsync(requestUri);
            }
        }

        #endregion Internal Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.context?.Dispose();
                    this.testingEnvironment?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
