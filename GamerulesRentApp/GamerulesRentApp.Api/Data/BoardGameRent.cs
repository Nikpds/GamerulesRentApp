using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace GamerulesRentApp.Api.Data
{
    public class BoardGameRent : Entity
    {
        public string CustomerId { get; set; }
        [BsonIgnore]
        public Customer Customer { get; set; }

        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime Created { get; set; }
        [BsonIgnore]
        public bool Overdue { get; set; }
        public int Price { get; set; }        
        public int Days { get; set; }

        public List<string> BoardGames { get; set; }

        public BoardGameRent()
        {
            BoardGames = new List<string>();
        }
    }
}
