using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunGroops.Application.Models
{
    public class PagedList<T>
    {
        private PagedList(List<T> list, int page, int pageSize, int totalCount)
        {
            List = list;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
        public List<T> List { get; set; }
        public int TotalCount { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
        public bool HasNextPage => (Page + 1) * PageSize < TotalCount;
        public bool HasPreviousPage => Page > 0;
        public static async Task<PagedList<T>> CreateAsync (IQueryable<T> query, int page, int pageSize)
        {
            int totalCount = await query.CountAsync();
            var items = await query.Skip(page * pageSize).Take(pageSize).ToListAsync();

            return new(items, page, pageSize, totalCount);
        }
    }
}
