using System.Diagnostics;

namespace OwnApt.TestEnvironment.Mongo.Processes
{
    public interface IProcess
    {
        int Id { get; }
        ProcessStartInfo StartInfo { get; }
        void Kill();
        void WaitForExit();
    }
}