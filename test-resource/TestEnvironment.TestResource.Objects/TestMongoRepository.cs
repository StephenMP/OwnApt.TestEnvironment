using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestEnvironment.TestResource.Objects
{
    public class TestMongoRepository
    {
        private IMongoDatabase coreDatabase;

        public TestMongoRepository(IMongoClient mongoClient)
        {
            this.coreDatabase = mongoClient.GetDatabase("Core");
        }

        private IMongoCollection<TestEntity> PropertiesCollection => this.coreDatabase.GetCollection<TestEntity>("Test");

        public async Task<TestEntity> CreateAsync(int key, string value)
        {
            var entity = new TestEntity { Id = key, Value = value };
            await this.PropertiesCollection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<TestEntity> ReadAsync(int key)
        {
            var asyncCursor = await this.PropertiesCollection.FindAsync(p => p.Id == key);
            return await asyncCursor.FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(int key, string value)
        {
            var entity = new TestEntity { Id = key, Value = value };
            await this.PropertiesCollection.ReplaceOneAsync(p => p.Id == key, entity);
        }

        public async Task DeleteAsync(int key)
        {
            await this.PropertiesCollection.DeleteOneAsync(p => p.Id == key);
        }
    }
}
