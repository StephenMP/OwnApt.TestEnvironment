using System;

namespace OwnApt.TestEnvironment.Mongo
{
    public class MongodOptions
    {
        private readonly int port;
        private readonly string dbPath;

        public MongodOptions(int port, string dbPath)
        {
            this.port = port;
            this.dbPath = dbPath;
        }

        public override string ToString()
        {
            return $"--port {this.port} --dbpath {this.dbPath}";
        }
    }
}