using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;
using System.Reflection;
namespace MongoCsharpGenericDriver
{
    public class DataAccess<T>
    {
        //"mongodb://localhost:27017"
        //"QuickPayBT"
        private MongoDatabase _db;
        public DataAccess(string MongoClientAddress,string DatabaseName)
        {  //MongoDB server address
            MongoClient _client = new MongoClient(MongoClientAddress);
#pragma warning disable CS0618 // Type or member is obsolete 
            //Name of the Database
            _db = _client.GetServer().GetDatabase(DatabaseName);
#pragma warning restore CS0618 // Type or member is obsolete
        }
        public IEnumerable<T> Get() => _db.GetCollection<T>(typeof(T).Name).FindAll();
        public T Get(ObjectId id) => _db.GetCollection<T>(typeof(T).Name).FindOneById(id);
        public WriteConcernResult Create(T t) => _db.GetCollection<T>(typeof(T).Name).Save(t);
        private void TrySetProperty(object obj, string property, object value)
        {
            PropertyInfo prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(obj, value, null);
            }
        }
        public void Update(ObjectId id, T t)
        {
            TrySetProperty(t, "Id", id);
            _db.GetCollection<T>(typeof(T).Name).Save(t);
        }
        public void Remove(ObjectId id)
        {
            IMongoQuery mongoQuery = Query.EQ("_id", id);
            _db.GetCollection<T>(typeof(T).Name).Remove(mongoQuery);
        }
    }
}
