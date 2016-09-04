using System.Diagnostics;

namespace OwnApt.TestEnvironment.Mongo.Processes
{
    internal static class ProcessExtensions
    {
        #region Public Methods

        public static IProcess AsIProcess(this Process process)
        {
            return new ProcessFacade(process);
        }

        #endregion Public Methods

        #region Private Classes

        private class ProcessFacade : IProcess
        {
            #region Private Fields

            private readonly Process process;

            #endregion Private Fields

            #region Public Constructors

            public ProcessFacade(Process process)
            {
                this.process = process;
            }

            #endregion Public Constructors

            #region Public Properties

            public int Id
            {
                get { return this.process.Id; }
            }

            public ProcessStartInfo StartInfo
            {
                get { return this.process.StartInfo; }
            }

            #endregion Public Properties

            #region Public Methods

            public void Kill()
            {
                this.process.Kill();
            }

            public void WaitForExit()
            {
                this.process.WaitForExit();
            }

            #endregion Public Methods
        }

        #endregion Private Classes
    }
}
