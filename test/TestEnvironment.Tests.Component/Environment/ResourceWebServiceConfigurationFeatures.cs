using System;
using System.Threading.Tasks;
using Xunit;

namespace TestEnvironment.Tests.Component.Environment
{
    public class ResourceWebServiceConfigurationFeatures
    {
        #region Private Fields

        private readonly ResourceWebServiceConfigurationSteps steps;

        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public ResourceWebServiceConfigurationFeatures()
        {
            this.steps = new ResourceWebServiceConfigurationSteps();
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public void CanAddAResourceWebService()
        {
            this.steps.GivenIHaveAResourceWebServiceConfiguration();
            this.steps.WhenIAddAResourceWebService();
            this.steps.ThenICanVerifyICanAddAResourceWebService();
        }

        [Fact]
        public void CanNewUpAResourceWebServiceConfiguration()
        {
            this.steps.GivenIHaveAResourceWebServiceConfiguration();
            this.steps.ThenICanVerifyICanNewUpAResourceWebServiceConfiguration();
        }

        [Fact]
        public async Task CanUseWebServiceAsync()
        {
            this.steps.GivenIHaveAResourceWebServiceConfiguration();
            this.steps.GivenIHaveAWebService();
            await this.steps.WhenIUseWebServiceAsync();
            await this.steps.ThenICanVerifyICanUseWebServiceAsync();
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
