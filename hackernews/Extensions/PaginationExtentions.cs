using hackernews.Models;
using hackernews.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hackernews.Extensions
{
    public static class PaginationExtentions
    {
        public static Pageable<Story> Page(this IEnumerable<Story> source, IPageRequest request)
        {
            Pageable<Story> pageable = new Pageable<Story>
            {
                totalCount = source.Count(),
                pageSize = request.pageSize
            };
            if (!string.IsNullOrWhiteSpace(request.search))
            {
                pageable.content = source.Where(x => x?.title?.IndexOf(request.search.Trim(), StringComparison.OrdinalIgnoreCase) > -1);
            }
            else
            {
                pageable.content = source;
            }
            pageable.filtered   = (pageable.content?.Count()).GetValueOrDefault();
            pageable.pageCount  = pageable.filtered / request.pageSize;

            if(pageable.filtered % request.pageSize > 0)
            {
                pageable.pageCount += 1;
            }
            if(request.page > pageable.pageCount - 1)
            {
                request.page = 0;
            }
            pageable.content    = pageable.content?.Skip(request.page * request.pageSize).Take(request.pageSize);
            return pageable;
        }
        public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int page, int pageSize)
        {
            return source?.Skip(page * pageSize).Take(pageSize);
        }
    }
}
