using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamerulesRentApp.Api.Services
{
    public class DataResponse<T>
    {
        public int TotalRows { get; set; }
        public IEnumerable<T> Rows { get; set; }
    }
}
