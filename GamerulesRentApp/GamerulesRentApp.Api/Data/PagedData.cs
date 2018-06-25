using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamerulesRentApp.Api.Data
{
    public class PagedData<T>
    {
        public int TotalRows { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string Search { get; set; }
        public string Order { get; set; }
        public bool IsAscending { get; set; }
        public List<T> Rows { get; set; }

        public PagedData()
        {
            Rows = new List<T>();
        }
    }
}
