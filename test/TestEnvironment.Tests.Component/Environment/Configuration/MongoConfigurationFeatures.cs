using System;
using System.Threading.Tasks;
using Xunit;

namespace TestEnvironment.Tests.Component.Environment.Configuration
{
    public class MongoConfigurationFeatures : IDisposable
    {
        #region Private Fields

        private readonly MongoConfigurationSteps steps;

        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public MongoConfigurationFeatures()
        {
            this.steps = new MongoConfigurationSteps();
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public async Task CanImportMultipleSqlDataAsync()
        {
            this.steps.GivenIHaveAMongoConfiguration();
            this.steps.GivenIHaveMultipleDataToImport();
            await this.steps.WhenIImportMongoDataAsync();
            this.steps.ThenICanVerifyICanImportMultipleMongoDataAsync();
        }

        [Fact]
        public async Task CanImportSqlDataAsync()
        {
            this.steps.GivenIHaveAMongoConfiguration();
            this.steps.GivenIHaveDataToImport();
            await this.steps.WhenIImportMongoDataAsync();
            this.steps.ThenICanVerifyICanImportMongoDataAsync();
        }

        [Fact]
        public void CanNewUpMongoConfiguration()
        {
            this.steps.GivenIHaveAMongoConfiguration();
            this.steps.ThenICanVerifyICanNewUpMongoConfiguration();
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
                    this.steps?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
