using System;
using Xunit;

namespace TestEnvironment.Tests.Component.Environment
{
    public class TestingEnvironmentFeatures : IDisposable
    {
        #region Private Fields

        private readonly TestingEnvironmentSteps steps;
        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public TestingEnvironmentFeatures()
        {
            this.steps = new TestingEnvironmentSteps();
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public void CanAddMongo()
        {
            this.steps.GivenIHaveATestingEnvironment();
            this.steps.WhenIAddMongo();
            this.steps.ThenICanVerifyICanAddMongo();
        }

        [Fact]
        public void CanAddSqlContext()
        {
            this.steps.GivenIHaveATestingEnvironment();
            this.steps.WhenIAddASqlContext();
            this.steps.ThenICanVerifyICanAddSqlContext();
        }

        [Fact]
        public void CanCreateAndReadMongoData()
        {
            this.steps.GivenIHaveATestingEnvironment();
            this.steps.GivenIHaveDataToCreate();
            this.steps.WhenIAddMongo();
            this.steps.WhenICreateMongoData();
            this.steps.ThenICanVerifyICanCreateAndReadMongoData();
        }

        [Fact]
        public void CanCreateAndReadMultipleMongoData()
        {
            this.steps.GivenIHaveATestingEnvironment();
            this.steps.GivenIHaveMultipleDataToCreate();
            this.steps.WhenIAddMongo();
            this.steps.WhenICreateMultipleMongoData();
            this.steps.ThenICanVerifyICanCreateAndReadMultipleMongoData();
        }

        [Fact]
        public void CanCreateAndReadMultipleSqlData()
        {
            this.steps.GivenIHaveATestingEnvironment();
            this.steps.GivenIHaveMultipleDataToCreate();
            this.steps.WhenIAddASqlContext();
            this.steps.WhenICreateMultipleSqlData();
            this.steps.ThenICanVerifyICanCreateAndReadMultipleSqlData();
        }

        [Fact]
        public void CanCreateAndReadSqlData()
        {
            this.steps.GivenIHaveATestingEnvironment();
            this.steps.GivenIHaveDataToCreate();
            this.steps.WhenIAddASqlContext();
            this.steps.WhenICreateSqlData();
            this.steps.ThenICanVerifyICanCreateAndReadSqlData();
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
