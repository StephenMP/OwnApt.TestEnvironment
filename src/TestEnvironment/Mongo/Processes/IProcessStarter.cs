using System;
using System.Diagnostics;

namespace OwnApt.TestEnvironment.Mongo.Processes
{
    public interface IProcessStarter : IDisposable
    {
        IProcess Start(ProcessStartInfo processStartInfo);
    }
}