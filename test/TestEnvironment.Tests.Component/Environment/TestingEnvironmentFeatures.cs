using System;
using Xunit;

namespace TestEnvironment.Tests.Component.Environment
{
    public class TestingEnvironmentFeatures : IDisposable
    {
        #region Private Fields

        private bool disposedValue = false;
        private TestingEnvironmentSteps steps = new TestingEnvironmentSteps();

        #endregion Private Fields

        #region Public Methods

        [Fact]
        public void CanAddSqlContext()
        {
            this.steps.GivenIHaveATestingEnvironment();
            this.steps.WhenIAddASqlContext();
            this.steps.ThenICanVerifyICanAddSqlContext();
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

        [Fact]
        public void CanCreateAndReadMultipleSqlData()
        {
            this.steps.GivenIHaveATestingEnvironment();
            this.steps.GivenIHaveMultipleDataToCreate();
            this.steps.WhenIAddASqlContext();
            this.steps.WhenICreateMultipleSqlData();
            this.steps.ThenICanVerifyICanCreateAndReadMultipleSqlData();
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
            if(!disposedValue)
            {
                if(disposing)
                {
                    this.steps?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
