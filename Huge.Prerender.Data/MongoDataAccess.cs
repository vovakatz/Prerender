using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Vibrant.AIS.Framework.Data
{
    public class MongoDataAccess<T>
    {
        private MongoDatabase _database;

        public MongoDataAccess(string connectionString, string collectionName)
        {
            var url = new MongoUrl(connectionString);
            var client = new MongoClient(url);
            var server = client.GetServer();
            _database = server.GetDatabase(url.DatabaseName);
            Collection = _database.GetCollection<T>(collectionName);
        }

        public string ConnectionString { get; set; }

        public MongoCollection<T> Collection { get; set; }

        public IQueryable<T> Queryable
        {
            get
            {
                return this.Collection.AsQueryable<T>();
            }
        }

        public T Save(T entity)
        {
            this.Collection.Save(entity);

            return entity;
        }

        public IEnumerable<T> Save(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
                this.Collection.Save<T>(entity);

            return entities;
        }

        public List<T> List
        {
            get
            {
                return this.Collection.AsQueryable<T>().ToList();
            }
        }

        public long Count()
        {
            return this.Collection.Count();
        }

        public bool Exists(Expression<Func<T, bool>> criteria)
        {
            return this.Collection.AsQueryable<T>().Any(criteria);
        } 
    }
}
