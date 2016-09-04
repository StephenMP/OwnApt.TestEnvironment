﻿using Microsoft.AspNetCore.Hosting;
using System;

namespace OwnApt.TestEnvironment.WebService
{
    public class TestWebService : IDisposable
    {
        #region Private Fields

        private readonly IWebHost webHost;
        private bool disposedValue;

        #endregion Private Fields

        #region Internal Constructors

        internal TestWebService(Uri baseUri, IWebHost webHost)
        {
            this.BaseUri = baseUri;
            this.webHost = webHost;
        }

        #endregion Internal Constructors

        #region Public Properties

        public Uri BaseUri { get; }

        #endregion Public Properties

        #region Public Methods

        public static TestWebService Create<TStartup>(Uri baseUri) where TStartup : class
        {
            var webHost = new WebHostBuilder().UseKestrel().UseStartup<TStartup>().UseUrls().Start(baseUri.AbsoluteUri);
            return new TestWebService(baseUri, webHost);
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
                    this.webHost?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
