using System;
using MongoDB.Driver;

namespace OwnApt.TestEnvironment.Mongo
{
    public interface IMongoTestDatabase : IDisposable
    {
        IMongoDatabase Database { get; }
    }
}