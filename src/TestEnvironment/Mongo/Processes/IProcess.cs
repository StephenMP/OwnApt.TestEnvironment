using System.Diagnostics;

namespace OwnApt.TestEnvironment.Mongo.Processes
{
    public interface IProcess
    {
        #region Public Properties

        int Id { get; }
        ProcessStartInfo StartInfo { get; }

        #endregion Public Properties

        #region Public Methods

        void Kill();

        void WaitForExit();

        #endregion Public Methods
    }
}
