using System;
using System.Diagnostics;

namespace OwnApt.TestEnvironment.Mongo.Processes
{
    internal class ProcessStarter : IProcessStarter
    {
        #region Private Fields

        private bool disposedValue;
        private Process process;

        #endregion Private Fields

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IProcess Start(ProcessStartInfo processStartInfo)
        {
            this.process = new Process
            {
                StartInfo = processStartInfo
            };

            process.ErrorDataReceived += (s, e) => { if (e.Data != null) Console.WriteLine(e.Data); };
            process.OutputDataReceived += (s, e) => { if (e.Data != null) Console.WriteLine(e.Data); };
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return process.AsIProcess();
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.process?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
