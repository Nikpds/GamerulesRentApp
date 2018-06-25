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
    public class BoardGameRental : Controller
    {
        private Context _db;

        public BoardGameRental(Context db)
        {
            _db = db;
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] BoardGameRental rental)
        {
            try
            {
                if (customer.IsValid())
                {
                    customer.Created = DateTime.Now;
                    var result = await _db.Customers.Insert(customer);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Παρακαλώ συμπληρώστε Όνομα, Επίθετο, Τηλέφωνο και αριθμό Πελάτη");
                }

            }
            catch (Exception exc)
            {
                return BadRequest("Σφαλμα εισαγωγής πελάτη");
            }
        }
    }
}
