using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamerulesRentApp.Api.Data
{
    public class DashboardView
    {
        public int Customers { get; set; }
        public int ActiveOrders { get; set; }
        public int CompletedOrders { get; set; }
        public int DelayedOrders { get; set; }
        public List<BoardGameRent> RentsForToday { get; set; }

        public DashboardView()
        {
            RentsForToday = new List<BoardGameRent>();
        }
    }
}
