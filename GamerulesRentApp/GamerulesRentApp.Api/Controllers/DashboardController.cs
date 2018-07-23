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
    public class DashboardController : Controller
    {
        private Context _db;

        public DashboardController(Context db)
        {
            _db = db;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetDashboardInfo()
        {
            try
            {
                var info = new DashboardView();
                info.Customers = await _db.Customers.Count(x => true);
                info.ActiveOrders = _db.BoardGameRent.PendingRents(x => x.ReturnDate, y => y.ReturnedDate == null);
                info.CompletedOrders = await _db.BoardGameRent.Count(x => x.ReturnedDate != null);
                info.DelayedOrders = _db.BoardGameRent.DelayedRents(x => x.ReturnDate, y => y.ReturnedDate == null);
                info.RentsForToday = (from b in _db.BoardGameRent.GetTodayQuery(x => x.ReturnDate).ToList()
                                      join cus in _db.Customers.GetQueryAll() on b.CustomerId equals cus.Id into customer
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
                                          Customer = customer.FirstOrDefault()
                                      }).ToList();
                return Ok(info);
            }
            catch (Exception exc)
            {
                return BadRequest("Σφαλμα κατα την συλλογή στατιστικών");
            }
        }
        [Route("migration")]
        [HttpGet]
        public async Task<IActionResult> Migration()
        {
            try
            {
                var run = new MigrationFromOldDB(_db);
                var resulut = await run.ReadMDF();
                return Ok("ΟΚ");
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }



    }
}
