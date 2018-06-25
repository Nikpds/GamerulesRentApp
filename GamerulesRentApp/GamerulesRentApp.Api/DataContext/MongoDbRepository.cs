using GamerulesRentApp.Api.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GamerulesRentApp.Api.DataContext
{
    public class MongoDbRepository<T> where T : Entity
    {
        protected internal IMongoCollection<T> collection;

        public IMongoCollection<T> Collection { get { return collection; } }

        public MongoDbRepository(IMongoDatabase database, string collectionName)
        {
            this.collection = database.GetCollection<T>(collectionName);
        }

        /// Gets

        public virtual async Task<T> GetById(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var query = await collection.FindAsync(filter);
            var result = await query.ToListAsync();
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var documents = await collection.FindAsync(x => true);
            var result = await documents.ToListAsync();

            return result;
        }



        public IQueryable<T> GetQueryAll()
        {
            var documents = collection.AsQueryable();

            return documents;
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate)
        {

            var documents = collection.AsQueryable().Where(predicate);

            return documents;
        }

        public IList<T> GetCustomQuery(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> predicate2)
        {
            var builder = Builders<T>.Filter;
            var filter = builder.Where(predicate) | builder.Where(predicate2);

            var group = collection.Aggregate()
                                  .Match(filter)
                                  .ToListAsync().Result;

            return group;
        }

        public IList<T> CustomQuery(Expression<Func<T, DateTime>> predicate)
        {

            var d = DateTime.Now;
            var endDate = new DateTime(d.Year, d.Month, d.Day, 23, 59, 59, 999);
            var builder = Builders<T>.Filter;
            var filter = builder.Gte(predicate, endDate);

            var group = collection.Aggregate()
                                  .Match(filter)
                                  .ToListAsync().Result;

            return group;
        }
        public IList<T> GetAbsencesQuery(Expression<Func<T, DateTime>> predicate, Expression<Func<T, string>> predicate2, IEnumerable<string> ids, int days)
        {
            var d = DateTime.Now;
            var startDate = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0, 0).AddDays(-days);
            var builder = Builders<T>.Filter;
            var filter = builder.In(predicate2, ids) & builder.Gte(predicate, startDate);

            var group = collection.Aggregate()
                                  .Match(filter)
                                  .ToListAsync().Result;

            return group;
        }

        public IQueryable<T> GetFilterQuery(Expression<Func<T, bool>> predicate)
        {
            return collection.AsQueryable().Where(predicate);
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate)
        {
            var documents = await collection.FindAsync(predicate);
            var result = await documents.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<T>> Get(FilterDefinition<T> filter)
        {
            var documents = await this.collection.FindAsync(filter);
            var result = await documents.ToListAsync();

            return result;
        }

        ///Insert and Count

        public virtual async Task<T> Insert(T entity)
        {

            await collection.InsertOneAsync(entity);

            return entity;
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            var count = await this.collection.CountAsync(predicate);
            return (int)count;
        }

        ///Update
        ///
        public virtual async Task<T> Update(T entity)
        {
            var _id = entity.Id;
            var filter = Builders<T>.Filter.Eq("Id", entity.Id);
            var result = await collection.ReplaceOneAsync(filter, entity);

            return entity;
        }

        public virtual async Task<bool> UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            var result = await collection.UpdateManyAsync(filter, update);

            return result.MatchedCount == result.ModifiedCount;
        }

        public virtual async Task<bool> UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            var result = await collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }

        public virtual async Task<IEnumerable<T>> BulkInsert(IEnumerable<T> entities)
        {
            if (entities == null || entities.Count() == 0)
                return new List<T>().AsEnumerable();
            else
            {
                await collection.InsertManyAsync(entities);
                return entities;
            }
        }

        ///Delete
        public virtual async Task<bool> Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = await collection.DeleteOneAsync(filter);

            return result.IsAcknowledged && result.DeletedCount == 1;
        }

        public virtual async Task<bool> DeleteAll()
        {
            var result = await collection.DeleteManyAsync(x => true);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public virtual async Task<bool> Delete(T entity)
        {
            return await this.Delete(entity.Id);
        }

        public virtual async Task<bool> Delete(Expression<Func<T, bool>> predicate)
        {
            var result = await collection.DeleteManyAsync(predicate);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}

