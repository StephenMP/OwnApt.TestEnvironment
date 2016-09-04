using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using MongoDB.Driver;
using OwnApt.TestEnvironment.Mongo;
using OwnApt.TestEnvironment.WebService;
using System;
using System.Collections.Generic;
using System.IO;

namespace OwnApt.TestEnvironment.Environment
{
    public class TestingEnvironment : IDisposable
    {
        #region Private Fields

        private readonly IServiceProvider serviceProvider;
        private readonly Dictionary<Type, IDbContextOptions> sqlDbContextOptions;
        private readonly Dictionary<Type, TestWebService> webServices;
        private bool disposedValue;
        private IMongoClient mongoClient;
        private MongoTestServer mongoServer;

        #endregion Private Fields

        #region Public Constructors

        public TestingEnvironment()
        {
            this.sqlDbContextOptions = new Dictionary<Type, IDbContextOptions>();
            this.webServices = new Dictionary<Type, TestWebService>();
            this.serviceProvider = new ServiceCollection()
                 .AddEntityFrameworkInMemoryDatabase()
                 .BuildServiceProvider();
        }

        #endregion Public Constructors

        #region Public Methods

        public void AddMongo()
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

            this.mongoServer = new MongoTestServer(TcpPortUtil.GetFreeTcpPort(), Path.Combine(mongoPath, "DevOps\\Mongo\\bin\\mongod.exe"));
            this.mongoClient = mongoServer.Database.Client;
        }

        public void AddSqlContext<TDbContext>() where TDbContext : DbContext
        {
            var builder = new DbContextOptionsBuilder<TDbContext>()
                                .UseInMemoryDatabase()
                                .UseInternalServiceProvider(serviceProvider);

            this.sqlDbContextOptions.Add(typeof(TDbContext), builder.Options);
        }

        public void AddWebService<TStartup>() where TStartup : class
        {
            var baseUri = new Uri($"http://localhost:{TcpPortUtil.GetFreeTcpPort()}");
            this.webServices.Add(typeof(TStartup), TestWebService.Create<TStartup>(baseUri));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IMongoClient MongoClient()
        {
            return this.mongoClient;
        }

        public DbContextOptions<TDbContext> SqlDbContextOptions<TDbContext>() where TDbContext : DbContext
        {
            return this.sqlDbContextOptions[typeof(TDbContext)] as DbContextOptions<TDbContext>;
        }

        public TestWebService WebService<TStartup>() where TStartup : class
        {
            return this.webServices[typeof(TStartup)];
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
                    foreach (var webHost in this.webServices.Values)
                    {
                        webHost?.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
