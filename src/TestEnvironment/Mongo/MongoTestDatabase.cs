using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace OwnApt.TestEnvironment.Mongo
{
    internal class MongoTestDatabase : IMongoTestDatabase
    {
        #region Private Fields

        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public MongoTestDatabase(int port)
        {
            if (port == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(port), port, "The port number cannot be '0'.");
            }

            var databaseName = Guid.NewGuid().ToString("N");
            this.Client = new MongoClient($"mongodb://localhost:{port}");
            this.Database = this.Client.GetDatabase(databaseName);
        }

        #endregion Public Constructors

        #region Public Properties

        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }

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
                    this.Client?.DropDatabase(this.Database.DatabaseNamespace.DatabaseName);
                    this.Client?.Cluster.Dispose();

                    // Giant hack to simulate old MongoServer.Disconnect() API behaviour.
                    var clusterKeyType = typeof(MongoClient).GetTypeInfo().Assembly.GetType("MongoDB.Driver.ClusterKey");
                    var clusterRegistryType = typeof(MongoClient).GetTypeInfo().Assembly.GetType("MongoDB.Driver.ClusterRegistry");
                    var staticClusterRegistryField = clusterRegistryType.GetField("__instance", BindingFlags.NonPublic | BindingFlags.Static);
                    var registryDictionaryField = clusterRegistryType.GetField("_registry", BindingFlags.NonPublic | BindingFlags.Instance);
                    var clusterRegistry = staticClusterRegistryField.GetValue(null);
                    var registryDictionary = registryDictionaryField.GetValue(clusterRegistry);
                    var registeryDictionaryType = typeof(Dictionary<,>).MakeGenericType(clusterKeyType, typeof(ICluster));
                    var clearMethodInfo = registeryDictionaryType.GetMethod("Clear", BindingFlags.Public | BindingFlags.Instance);
                    clearMethodInfo.Invoke(registryDictionary, null);
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
