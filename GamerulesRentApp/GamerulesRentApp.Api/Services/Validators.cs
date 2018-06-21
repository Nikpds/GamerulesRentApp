using GamerulesRentApp.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamerulesRentApp.Api.Services
{
    public static class Validators
    {

        public static bool IsValid(this Customer customer)
        {
            return !string.IsNullOrEmpty(customer.Name) &&
                !string.IsNullOrEmpty(customer.Lastname) &&
                !string.IsNullOrEmpty(customer.Mobile) &&
                !string.IsNullOrEmpty(customer.IdentityNo);
        }
    }
}
