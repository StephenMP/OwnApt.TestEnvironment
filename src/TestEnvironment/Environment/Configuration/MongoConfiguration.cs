using Microsoft.Extensions.PlatformAbstractions;
using MongoDB.Driver;
using OwnApt.TestEnvironment.Mongo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OwnApt.TestEnvironment.Environment.Configuration
{
    public class MongoConfiguration : IDisposable
    {
        #region Private Fields

        private readonly MongoTestServer mongoServer;
        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public MongoConfiguration()
        {
            var mongoPath = PlatformServices.Default.Application.ApplicationBasePath;
            var hasDevops = false;
            while (!hasDevops)
            {
                mongoPath = Path.GetFullPath(Path.Combine(mongoPath, @"..\"));
                var dirs = Directory.GetDirectories(mongoPath);

                foreach (var dir in dirs)
                {
                    if (dir.Contains("DevOps"))
                    {
                        hasDevops = true;
                        break;
                    }
                }
            }

            this.mongoServer = new MongoTestServer(TcpPortUtility.GetFreeTcpPort(), Path.Combine(mongoPath, "DevOps\\BuildSystem\\Mongo\\bin\\mongod.exe"));
            this.MongoClient = mongoServer.Database.Client;
        }

        #endregion Public Constructors

        #region Public Properties

        public IMongoClient MongoClient { get; }

        #endregion Public Properties

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task ImportDataAsync<TEntity>(string dbName, string collectionName, IEnumerable<TEntity> data)
        {
            await this.MongoClient.GetDatabase(dbName).GetCollection<TEntity>(collectionName).InsertManyAsync(data);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.mongoServer?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
