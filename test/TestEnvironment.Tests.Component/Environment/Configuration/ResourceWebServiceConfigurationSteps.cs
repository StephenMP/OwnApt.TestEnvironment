using OwnApt.TestEnvironment.Environment.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TestEnvironment.TestResource.Api;
using Xunit;

namespace TestEnvironment.Tests.Component.Environment.Configuration
{
    public class ResourceWebServiceConfigurationSteps : IDisposable
    {
        #region Private Fields

        private bool disposedValue;
        private ResourceWebServiceConfiguration resourceWebServiceConfiguration;

        private HttpResponseMessage webServiceResponse;

        #endregion Private Fields

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Internal Methods

        internal void GivenIHaveAResourceWebServiceConfiguration()
        {
            this.resourceWebServiceConfiguration = new ResourceWebServiceConfiguration();
        }

        internal void GivenIHaveAWebService()
        {
            this.resourceWebServiceConfiguration.AddWebService<TestStartup>();
        }

        internal void ThenICanVerifyICanAddAResourceWebService()
        {
            var webService = this.resourceWebServiceConfiguration.WebService<TestStartup>();
            Assert.NotNull(webService);
            Assert.NotNull(webService.BaseUri);
        }

        internal void ThenICanVerifyICanNewUpAResourceWebServiceConfiguration()
        {
            Assert.NotNull(this.resourceWebServiceConfiguration);
        }

        internal async Task ThenICanVerifyICanUseWebServiceAsync()
        {
            Assert.NotNull(this.webServiceResponse);
            Assert.True(this.webServiceResponse.IsSuccessStatusCode);

            var content = await this.webServiceResponse.Content.ReadAsStringAsync();
            Assert.NotNull(content);
            Assert.Contains("true", content);
        }

        internal void WhenIAddAResourceWebService()
        {
            this.resourceWebServiceConfiguration.AddWebService<TestStartup>();
        }

        internal async Task WhenIUseWebServiceAsync()
        {
            using (var client = new HttpClient())
            {
                var requestUri = $"{resourceWebServiceConfiguration.WebService<TestStartup>().BaseUri.AbsoluteUri.TrimEnd('/')}/api/test";
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
                    this.resourceWebServiceConfiguration?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
