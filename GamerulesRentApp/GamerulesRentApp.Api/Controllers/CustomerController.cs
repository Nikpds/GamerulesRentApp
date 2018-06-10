using GamerulesRentApp.Api.Data;
using GamerulesRentApp.Api.DataContext;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamerulesRentApp.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private Context _db;

        public CustomerController(Context db)
        {
            _db = db;
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Customer customer)
        {
            try
            {
                var result = await _db.Customers.Insert(customer);

                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest("Σφαλμα εισαγωγής πελάτη");
            }
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _db.Customers.GetAll();

                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest("Σφαλμα εισαγωγής πελάτη");
            }
        }
    }
}
