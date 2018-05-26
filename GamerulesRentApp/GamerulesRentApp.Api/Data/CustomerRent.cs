using System;

namespace GamerulesRentApp.Api.Data
{
    public class CustomerRent : Entity
    {
        public string CustomerId { get; set; }
        public DateTime Starts { get; set; }
        public int Price { get; set; }
        public DateTime Ends { get; set; }
        public int OverDue { get; set; }
        public string BoardGame { get; set; }
    }
}
