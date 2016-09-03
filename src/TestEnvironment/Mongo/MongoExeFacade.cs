using OwnApt.TestEnvironment.Mongo.Processes;
using System;
using System.Diagnostics;

namespace OwnApt.TestEnvironment.Mongo
{
    public class MongodExeFacade : IDisposable
    {
        #region Private Fields

        private readonly string mongoExeLocation;
        private readonly IProcessStarter processStarter;

        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public MongodExeFacade(string mongoExeLocation)
        {
            this.processStarter = new ProcessStarter();
            this.mongoExeLocation = mongoExeLocation;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IProcess Start(MongodOptions options)
        {
            var exeLocation = this.mongoExeLocation;
            var exeArgs = options.ToString();
            var processStartInfo = new ProcessStartInfo(exeLocation, exeArgs)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            return this.processStarter.Start(processStartInfo);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.processStarter?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
