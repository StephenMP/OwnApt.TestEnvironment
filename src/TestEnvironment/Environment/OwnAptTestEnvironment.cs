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

        private readonly OwnAptTestEnvironmentBuilder ownaptTestEnvironmentBuilder;
        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public OwnAptTestEnvironment(OwnAptTestEnvironmentBuilder ownaptTestEnvironmentBuilder)
        {
            this.ownaptTestEnvironmentBuilder = ownaptTestEnvironmentBuilder;
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
            return this.ownaptTestEnvironmentBuilder?.GetMongoClient();
        }

        public IResourceWebService GetResourceWebService<TStartup>() where TStartup : class
        {
            return this.ownaptTestEnvironmentBuilder?.GetResourceWebService<TStartup>();
        }

        public DbContextOptions<TDbContext> GetSqlDbContextOptions<TDbContext>() where TDbContext : DbContext
        {
            return this.ownaptTestEnvironmentBuilder?.GetSqlDbContextOptions<TDbContext>();
        }

        public async Task ImportMongoDataAsync<TEntity>(string dbName, string collectionName, IEnumerable<TEntity> data)
        {
            await this.ownaptTestEnvironmentBuilder?.ImportMongoDataAsync(dbName, collectionName, data);
        }

        public async Task ImportSqlDataAsync<TDbContext, TEntity>(IEnumerable<TEntity> entities) where TDbContext : DbContext where TEntity : class
        {
            await this.ownaptTestEnvironmentBuilder?.ImportSqlDataAsync<TDbContext, TEntity>(entities);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.ownaptTestEnvironmentBuilder?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
