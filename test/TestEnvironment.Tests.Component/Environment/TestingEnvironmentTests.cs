using Microsoft.EntityFrameworkCore;
using OwnApt.TestEnvironment.Environment;
using System;
using TestEnvironment.TestResource.Api;
using TestEnvironment.TestResource.Objects;
using Xunit;

namespace TestEnvironment.Tests.Component.Environment
{
    public class TestingEnvironmentTests : IDisposable
    {
        #region Private Fields

        private bool disposedValue;
        private OwnAptTestEnvironment testEnvironment;

        #endregion Private Fields

        #region Public Methods

        [Fact]
        public void CanNewUpTestingEnvironment()
        {
            this.testEnvironment = OwnAptTestEnvironment.CreateEnvironment();
            Assert.NotNull(testEnvironment);
        }

        [Fact]
        public void CanNewUpTestingEnvironmentUsingChaining()
        {
            this.testEnvironment = OwnAptTestEnvironment
                                        .CreateEnvironment()
                                        .UseMongo()
                                        .UseSql<TestDbContext>()
                                        .UseResourceWebService<TestStartup>();

            Assert.NotNull(this.testEnvironment);

            Assert.NotNull(this.testEnvironment.GetMongoClient());

            Assert.NotNull(this.testEnvironment.GetSqlDbContextOptions<TestDbContext>());
            Assert.IsType<DbContextOptions<TestDbContext>>(this.testEnvironment.GetSqlDbContextOptions<TestDbContext>());

            Assert.NotNull(this.testEnvironment.GetResourceWebService<TestStartup>());
            Assert.NotNull(this.testEnvironment.GetResourceWebService<TestStartup>().BaseUri);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.testEnvironment?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
