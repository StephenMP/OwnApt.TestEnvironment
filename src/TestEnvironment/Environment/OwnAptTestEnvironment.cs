using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using OwnApt.TestEnvironment.WebService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwnApt.TestEnvironment.Environment
{
    public class OwnAptTestEnvironment : IDisposable
    {
        #region Private Fields

        private readonly OwnAptTestEnvironmentBuilder options;
        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public OwnAptTestEnvironment(OwnAptTestEnvironmentBuilder options)
        {
            this.options = options;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IMongoClient GetMongoClient()
        {
            return this.options.MongoConfiguration?.MongoClient;
        }

        public ResourceWebService GetResourceWebService<TStartup>() where TStartup : class
        {
            return this.options.ResourceWebServiceConfiguration?.WebService<TStartup>();
        }

        public DbContextOptions<TDbContext> GetSqlDbContextOptions<TDbContext>() where TDbContext : DbContext
        {
            return this.options.SqlConfiguration?.SqlDbContextOptions<TDbContext>();
        }

        public async Task ImportMongoDataAsync<TEntity>(string dbName, string collectionName, IEnumerable<TEntity> data)
        {
            await this.options?.MongoConfiguration?.ImportDataAsync(dbName, collectionName, data);
        }

        public async Task ImportSqlDataAsync<TDbContext, TEntity>(IEnumerable<TEntity> entities) where TDbContext : DbContext where TEntity : class
        {
            await this.options?.SqlConfiguration?.ImportDataAsync<TDbContext, TEntity>(entities);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.options?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
