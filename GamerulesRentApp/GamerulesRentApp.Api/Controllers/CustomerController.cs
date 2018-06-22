using GamerulesRentApp.Api.Data;
using GamerulesRentApp.Api.DataContext;
using GamerulesRentApp.Api.Services;
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
                if (customer.IsValid())
                {
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

        [Route("")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Customer customer)
        {
            try
            {
                if (customer.IsValid())
                {
                    var result = await _db.Customers.Update(customer);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Παρακαλώ συμπληρώστε Όνομα,Επίθετο,Τηλέφωνο και αριθμό Πελάτη");
                }
            }
            catch (Exception exc)
            {
                return BadRequest("Σφαλμα επεξεργασίας πελάτη");
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Δεν επιλέξατε έγκυρο πελάτη");
                }

                var result = await _db.Customers.Delete(id);

                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest("Σφαλμα διαγραφής πελάτη");
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
                return BadRequest("Σφαλμα ανάκτησης πελατών");
            }
        }
    }
}
