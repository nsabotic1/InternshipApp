using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternAppApi.Dtos.Paging
{
    //model napravljen na osnovu Microsoft dokumentacije
    public class PagedListDto<T>
    {
        public List<T> Data {get;set;}
        public int TotalPages {get;set;}
        public int PageIndex {get;set;}
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}