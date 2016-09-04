using MongoDB.Driver;
using System;

namespace OwnApt.TestEnvironment.Mongo
{
    public interface IMongoTestDatabase : IDisposable
    {
        #region Public Properties

        IMongoDatabase Database { get; }

        #endregion Public Properties
    }
}
