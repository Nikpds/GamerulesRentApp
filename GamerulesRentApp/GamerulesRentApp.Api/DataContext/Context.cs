using GamerulesRentApp.Api.Data;
using MongoDB.Driver;
using System;

namespace GamerulesRentApp.Api.DataContext
{

    public class Context
    {
        public IMongoDatabase Database { get; private set; }

        public MongoDbRepository<Customer> Customers { get; set; }
        public MongoDbRepository<BoardGameRent> CustomerRent { get; set; }

        public Context(string connectionString)
        {
            var url = new MongoUrl(connectionString);

            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            var client = new MongoClient(settings);
            if (url.DatabaseName == null)
            {
                throw new ArgumentException("Your connection string must contain a database name", connectionString);
            }
            this.Database = client.GetDatabase(url.DatabaseName);

            Customers = new MongoDbRepository<Customer>(this.Database, "Customers");
            CustomerRent = new MongoDbRepository<BoardGameRent>(this.Database, "CustomerRent");
        }

    }

}
