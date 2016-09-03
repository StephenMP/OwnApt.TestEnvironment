using System.Diagnostics;

namespace OwnApt.TestEnvironment.Mongo.Processes
{
    internal static class ProcessExtensions
    {
        public static IProcess AsIProcess(this Process process)
        {
            return new ProcessFacade(process);
        }

        private class ProcessFacade : IProcess
        {
            private readonly Process process;

            public ProcessFacade(Process process)
            {
                this.process = process;
            }

            public int Id
            {
                get { return this.process.Id; }
            }

            public ProcessStartInfo StartInfo
            {
                get { return this.process.StartInfo; }
            }

            public void Kill()
            {
                this.process.Kill();
            }

            public void WaitForExit()
            {
                this.process.WaitForExit();
            }
        }
    }
}