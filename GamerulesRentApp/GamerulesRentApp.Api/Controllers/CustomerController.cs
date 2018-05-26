using GamerulesRentApp.Api.Data;
using GamerulesRentApp.Api.DataContext;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamerulesRentApp.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private Context _db;

        public CustomerController(Context db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var customer = new Customer();
                customer.Name = "Nikos";
                customer.Lastname = "Perperidis";
                customer.Address = "Maxis pogradets";
                await _db.Customers.Insert(customer);


                return Ok(customer);
            }
            catch (Exception exc)
            {
                return BadRequest("ασδασδασδ");
            }
        }
    }
}
