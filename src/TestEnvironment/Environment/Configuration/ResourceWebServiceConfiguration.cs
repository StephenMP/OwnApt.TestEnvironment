using OwnApt.TestEnvironment.WebService;
using System;
using System.Collections.Generic;

namespace OwnApt.TestEnvironment.Environment.Configuration
{
    public class ResourceWebServiceConfiguration : IDisposable
    {
        #region Private Fields

        private readonly Dictionary<Type, ResourceWebService> webServices;

        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public ResourceWebServiceConfiguration()
        {
            this.webServices = new Dictionary<Type, ResourceWebService>();
        }

        #endregion Public Constructors

        #region Public Methods

        public void AddWebService<TStartup>() where TStartup : class
        {
            var baseUri = new Uri($"http://localhost:{TcpPortUtility.GetFreeTcpPort()}");
            this.webServices.Add(typeof(TStartup), ResourceWebService.Create<TStartup>(baseUri));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ResourceWebService WebService<TStartup>() where TStartup : class
        {
            return this.webServices[typeof(TStartup)];
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (var webHost in this.webServices.Values)
                    {
                        webHost?.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
