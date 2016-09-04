using MongoDB.Driver;
using OwnApt.TestEnvironment.Mongo.File;
using OwnApt.TestEnvironment.Mongo.Processes;
using System;

namespace OwnApt.TestEnvironment.Mongo
{
    public class MongoTestServer : IDisposable
    {
        #region Private Fields

        private readonly IFileSystem fileSystem;
        private readonly string mongoDatabasePath;
        private readonly MongodExeFacade mongoFacade;
        private readonly IProcess process;
        private readonly IMongoTestDatabase testDatabase;
        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public MongoTestServer(int port, string mongoExeLocation)
        {
            this.fileSystem = new FileSystem();
            this.mongoFacade = new MongodExeFacade(mongoExeLocation);
            this.testDatabase = new MongoTestDatabase(port);
            this.mongoDatabasePath = this.fileSystem.CreateTempFolder();
            this.process = mongoFacade.Start(new MongodOptions(port, mongoDatabasePath));
        }

        #endregion Public Constructors

        #region Public Properties

        public IMongoDatabase Database => this.testDatabase.Database;

        #endregion Public Properties

        #region Public Methods

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
                    this.testDatabase?.Dispose();
                    this.process?.Kill();
                    this.process?.WaitForExit();
                    this.mongoFacade?.Dispose();

                    if (this.fileSystem != null)
                    {
                        if (this.fileSystem.DirectoryExists(this.mongoDatabasePath))
                        {
                            this.fileSystem.DeleteDirectory(this.mongoDatabasePath, true);
                        }
                    }
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
