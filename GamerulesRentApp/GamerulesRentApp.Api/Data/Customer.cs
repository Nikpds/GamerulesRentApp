using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamerulesRentApp.Api.Data
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string CardNumber { get; set; }
        public string PostalCode { get; set; }
        public string IdentityNo { get; set; }
        public bool IsVerified { get; set; }


    }
}
