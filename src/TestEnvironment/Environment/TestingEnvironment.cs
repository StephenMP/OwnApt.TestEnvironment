using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OwnApt.TestEnvironment.Mongo;
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using System.Linq;

namespace OwnApt.TestEnvironment.Environment
{
    public class TestingEnvironment : IDisposable
    {
        #region Private Fields

        private IServiceProvider serviceProvider;

        #endregion Private Fields

        #region Public Constructors

        public TestingEnvironment()
        {
            this.sqlDbContextOptions = new Dictionary<Type, DbContextOptions>();
            this.serviceProvider = new ServiceCollection()
                 .AddEntityFrameworkInMemoryDatabase()
                 .BuildServiceProvider();
        }

        #endregion Public Constructors

        #region Public Properties

        private Dictionary<Type, DbContextOptions> sqlDbContextOptions;
        private MongoTestServer mongoServer;
        private IMongoClient mongoClient;

        #endregion Public Properties

        #region Public Methods

        public void AddSqlContext<TDbContext>() where TDbContext : DbContext
        {
            var builder = new DbContextOptionsBuilder<TDbContext>()
                                .UseInMemoryDatabase()
                                .UseInternalServiceProvider(serviceProvider);

            this.sqlDbContextOptions.Add(typeof(TDbContext), builder.Options);
        }

        public void AddMongo()
        {
            var mongoPath = PlatformServices.Default.Application.ApplicationBasePath;
            var hasDevops = false;
            while (!hasDevops)
            {
                mongoPath = Path.GetFullPath(Path.Combine(mongoPath, @"..\"));
                var dirs = Directory.GetDirectories(mongoPath);

                foreach(var dir in dirs)
                {
                    if (dir.Contains("DevOps"))
                    {
                        hasDevops = true;
                        break;
                    }
                }
            }

            this.mongoServer = new MongoTestServer(TcpPort.GetFreeTcpPort(), Path.Combine(mongoPath, "DevOps\\Mongo\\bin\\mongod.exe"));
            this.mongoClient = mongoServer.Database.Client;
        }

        public DbContextOptions<TDbContext> SqlDbContextOptions<TDbContext>() where TDbContext : DbContext
        {
            return this.sqlDbContextOptions[typeof(TDbContext)] as DbContextOptions<TDbContext>;
        }

        public IMongoClient MongoClient()
        {
            return this.mongoClient;
        }

        #region IDisposable Support
        private bool disposedValue;

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #endregion Public Methods
    }
}
