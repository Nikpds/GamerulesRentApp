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
        [HttpPost]
        public IActionResult GetAll([FromBody] PagedData<Customer> options)
        {
            try
            {
                var query = _db.Customers.GetQueryAll();

                if (options.Search != null && options.Search.Trim() != "")
                {
                    query.Where(x => x.Lastname.ToLowerInvariant().StartsWith(options.Search.ToLowerInvariant()));
                }

                switch (options.Order)
                {
                    case "lastname":
                        query = options.IsAscending ? query.OrderBy(s => s.Lastname) : query.OrderByDescending(s => s.Lastname);
                        break;
                    case "created":
                        query = options.IsAscending ? query.OrderBy(s => s.Created) : query.OrderByDescending(s => s.Created);
                        break;
                    default:
                        query = query.OrderBy(s => s.Lastname);
                        break;
                }

                options.TotalRows = query.Count();
                options.Rows = query
                    .Skip((options.Page - 1) * options.PageSize)
                    .Take(options.PageSize)
                    .ToList();

                return Ok(options);
            }
            catch (Exception exc)
            {
                return BadRequest("Σφαλμα ανάκτησης πελατών");
            }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Δεν Βρέθηκε ο πελάτης");
                }
                var result = await _db.Customers.GetById(id);

                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest("Σφαλμα ανάκτησης πελατών");
            }
        }

        [Route("rent/{searchtext}")]
        [HttpGet]
        public IActionResult SearchCustomer(string searchtext)
        {
            try
            {
                if (string.IsNullOrEmpty(searchtext))
                {
                    return BadRequest("Δεν Βρέθηκε ο πελάτης");
                }

                var result = _db.Customers.GetCustomQuery(x => x.Lastname.ToLowerInvariant().Contains(searchtext), t => t.IdentityNo == searchtext).ToList();
                var view = CustomerView.ConverCustomers(result);

                return Ok(view == null ? new List<CustomerView>() : view);
            }
            catch (Exception ex)
            {
                return BadRequest("Σφαλμα ανάκτησης πελατών");
            }
        }


    }
}
