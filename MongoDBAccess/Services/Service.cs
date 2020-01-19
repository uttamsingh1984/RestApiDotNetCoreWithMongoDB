using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBAccess.Services
{
    public interface IService<T>
    {
        List<T> GetAll();
        T Get(string Id);

        T Add(T blog);
        void Delete(string Id);
        void Update(string Id, T blog);

    }
    public class ServiceBase  
    {
        protected IMongoDatabase Database {
            get 
            {
                var client = new MongoClient("mongodb://localhost:27017");
                return client.GetDatabase("BlogDb");
            }
        }        
    }
}
