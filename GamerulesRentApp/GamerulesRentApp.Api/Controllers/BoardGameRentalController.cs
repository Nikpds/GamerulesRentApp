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
                    result.Customer = await _db.Customers.GetById(result.CustomerId);
                    return Ok(result);
                }

            }
            catch (Exception exc)
            {
                return BadRequest("Σφαλμα εισαγωγής");
            }
        }

        [Route("return/{id}")]
        [HttpGet]
        public async Task<IActionResult> ReturnBoardGame(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Δεν επιλέξατε ενοικίαση");
                }
                else
                {
                    var original = await _db.BoardGameRent.GetById(id);
                    original.ReturnedDate = DateTime.Now;
                    var result = await _db.BoardGameRent.Update(original);
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
                    var original = await _db.BoardGameRent.GetById(rental.Id);
                    original.BoardGames = rental.BoardGames;
                    original.ReturnDate = rental.RentDate.AddDays(rental.Days);
                    original.Days = rental.Days;
                    original.Price = rental.Price;
                    original.RentDate = rental.RentDate;
                    var result = await _db.BoardGameRent.Update(original);
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

        [Route("rents/{page}/{pageSize}/{status?}/{order?}/{search?}")]
        [HttpGet]
        public IActionResult GetAll(int page, int pageSize, string status = "", string order = "", string search = "")
        {
            try
            {
                IQueryable<BoardGameRent> query;
                if (status == "complete")
                {
                    query = _db.BoardGameRent.GetFilterQuery(x => x.ReturnedDate != null);
                }
                else if (status == "pending")
                {
                    query = _db.BoardGameRent.GetFilterQuery(x => x.ReturnedDate == null && x.ReturnDate > DateTime.Now);
                }
                else if (status == "delayed")
                {

                    query = _db.BoardGameRent.GetFilterQuery(x => x.ReturnDate < DateTime.Now);
                }
                else
                {
                    query = _db.BoardGameRent.GetQueryAll();
                }

               

                query = (from b in query
                         join cus in _db.Customers.GetQueryAll() on b.CustomerId equals cus.Id into customer
                         where (true)
                         select new BoardGameRent
                         {
                             BoardGames = b.BoardGames,
                             Days = b.Days,
                             RentDate = b.RentDate,
                             Price = b.Price,
                             Created = b.Created,
                             Id = b.Id,
                             ReturnDate = b.ReturnDate,
                             ReturnedDate = b.ReturnedDate,
                             Customer = customer.First()
                         });

                switch (order)
                {
                    case "lastname_asc":
                        query = query.OrderBy(s => s.Customer.Lastname);
                        break;
                    case "lastname_desc":
                        query = query.OrderByDescending(x => x.Customer.Lastname);
                        break;
                    case "return_asc":
                        query = query.OrderBy(s => s.ReturnDate);
                        break;
                    case "return_desc":
                        query = query.OrderByDescending(s => s.ReturnDate);
                        break;
                    case "rent_asc":
                        query = query.OrderBy(s => s.RentDate);
                        break;
                    case "rent_desc":
                        query = query.OrderByDescending(s => s.RentDate);
                        break;
                    default:
                        query = query.OrderByDescending(s => s.Created);
                        break;
                }

                if (search != null && search.Trim() != "" && search != "undefined")
                {
                    query = query.Where(x => x.Customer.Lastname.ToLowerInvariant().StartsWith(search.ToLowerInvariant()));
                }

                var rows = query.Count();
                var rents = query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);




                return Ok(new DataResponse<BoardGameRent> { TotalRows = rows, Rows = rents });
            }
            catch (Exception exc)
            {
                return BadRequest("Σφαλμα ανάκτησης πελατών");
            }
        }
    }
}
