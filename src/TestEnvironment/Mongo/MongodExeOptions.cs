namespace OwnApt.TestEnvironment.Mongo
{
    public class MongodOptions
    {
        #region Private Fields

        private readonly string dbPath;
        private readonly int port;

        #endregion Private Fields

        #region Public Constructors

        public MongodOptions(int port, string dbPath)
        {
            this.port = port;
            this.dbPath = dbPath;
        }

        #endregion Public Constructors

        #region Public Methods

        public override string ToString()
        {
            return $"--port {this.port} --dbpath {this.dbPath}";
        }

        #endregion Public Methods
    }
}
