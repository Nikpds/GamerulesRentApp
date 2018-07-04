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
    public class BoardGameRentalController : Controller
    {
        private Context _db;

        public BoardGameRentalController(Context db)
        {
            _db = db;
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] BoardGameRent rental)
        {
            try
            {
                if (rental.IsValid())
                {
                    rental.Created = DateTime.Now;
                    rental.ReturnDate = rental.RentDate.AddDays(rental.Days);
                    var result = await _db.BoardGameRent.Insert(rental);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Παρακαλώ συμπληρώστε τα υποχρεωτικά πεδία");
                }

            }
            catch (Exception exc)
            {
                return BadRequest("Σφαλμα εισαγωγής");
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
                    return BadRequest("Δεν επιλέξατε ενοικίαση");
                }
                else
                {
                    var result = await _db.BoardGameRent.GetById(id);
                    return Ok(result);
                }

            }
            catch (Exception exc)
            {
                return BadRequest("Σφαλμα εισαγωγής");
            }
        }

        [Route("")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BoardGameRent rental)
        {
            try
            {
                if (rental.IsValid())
                {
                    rental.ReturnDate = rental.RentDate.AddDays(rental.Days);
                    var result = await _db.BoardGameRent.Insert(rental);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Παρακαλώ συμπληρώστε τα υποχρεωτικά πεδία");
                }

            }
            catch (Exception exc)
            {
                return BadRequest("Σφαλμα επεξεργασίας");
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
                    return BadRequest("Δεν επιλέξατε ενοικίαση");
                }
                else
                {
                    var result = await _db.BoardGameRent.Delete(id);
                    return Ok(result);

                }

            }
            catch (Exception exc)
            {
                return BadRequest("Σφαλμα επεξεργασίας");
            }
        }
    }
}
