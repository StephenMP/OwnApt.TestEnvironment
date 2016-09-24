using Microsoft.EntityFrameworkCore;
using OwnApt.TestEnvironment.Environment.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwnApt.TestEnvironment.Environment
{
    public class OwnAptTestEnvironmentBuilder : IDisposable
    {
        #region Private Fields

        private bool disposedValue;

        #endregion Private Fields

        #region Public Properties

        public MongoConfiguration MongoConfiguration { get; private set; }
        public ResourceWebServiceConfiguration ResourceWebServiceConfiguration { get; private set; }
        public SqlConfiguration SqlConfiguration { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public OwnAptTestEnvironmentBuilder AddMongo()
        {
            this.MongoConfiguration = this.MongoConfiguration ?? new MongoConfiguration();
            return this;
        }

        public OwnAptTestEnvironmentBuilder AddResourceWebService<TStartup>() where TStartup : class
        {
            this.ResourceWebServiceConfiguration = this.ResourceWebServiceConfiguration ?? new ResourceWebServiceConfiguration();
            this.ResourceWebServiceConfiguration?.AddWebService<TStartup>();
            return this;
        }

        public OwnAptTestEnvironmentBuilder AddSqlContext<TDbContext>() where TDbContext : DbContext
        {
            this.SqlConfiguration = this.SqlConfiguration ?? new SqlConfiguration();
            this.SqlConfiguration?.AddSqlContext<TDbContext>();
            return this;
        }

        public OwnAptTestEnvironment BuildEnvironment()
        {
            return new OwnAptTestEnvironment(this);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task ImportMongoDataAsync<TEntity>(string dbName, string collectionName, IEnumerable<TEntity> data)
        {
            await this.MongoConfiguration?.ImportDataAsync(dbName, collectionName, data);
        }

        public async Task ImportSqlDataAsync<TDbContext, TEntity>(IEnumerable<TEntity> entities) where TDbContext : DbContext where TEntity : class
        {
            await this.SqlConfiguration?.ImportDataAsync<TDbContext, TEntity>(entities);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.MongoConfiguration?.Dispose();
                    this.ResourceWebServiceConfiguration?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
