using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InternAppApi.Helpers
{
    //source: https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/sort-filter-page?view=aspnetcore-6.0&fbclid=IwAR3jAwqtvatAGZ8ZNdjk0_09XISgIlmWG93fOdFDE09JESqDj-kvJzErFbQ

    //The CreateAsync method in this code takes page size and page number 
    //and applies the appropriate Skip and Take statements to the IQueryable. 
    //When ToListAsync is called on the IQueryable, it will return a List containing only the requested page. 
    //The properties HasPreviousPage and HasNextPage can be used to enable or disable Previous and Next paging buttons.

    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}