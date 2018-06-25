using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamerulesRentApp.Api.Data
{
    public class CustomerView
    {
        public string Fullname { get; set; }
        public string Id { get; set; }

        public static List<CustomerView> ConverCustomers(List<Customer> customers)
        {
            var result = new List<CustomerView>();
            customers.ForEach(el =>
            {
                result.Add(new CustomerView() { Fullname = string.Join(" ", el.Lastname, el.Name), Id = el.Id });
            });
            return result;
        }
    }
}
