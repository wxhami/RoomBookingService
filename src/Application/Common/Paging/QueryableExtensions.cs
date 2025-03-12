using Application.Common.Constants;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Paging;

public static class QueryableExtensions
{
    internal static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IPagedQuery pagedQuery) =>
        query.Skip((pagedQuery.PageSize ?? RequestConstants.PageSize) *
                   ((pagedQuery.PageNumber ?? RequestConstants.PageNumber) - 1))
            .Take(pagedQuery.PageSize ?? RequestConstants.PageSize);

    internal static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        IPagedQuery request,
        CancellationToken cancellationToken)
    {
        var pageSize = request.PageSize ?? RequestConstants.PageSize;

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query.ApplyPaging(request).ToListAsync(cancellationToken);

        return new PagedResult<T>
        {
            Items = items,
            PageNumber = request.PageNumber ?? RequestConstants.PageNumber,
            PageSize = pageSize,
            Count = items.Count,
            TotalCount = totalCount,
            TotalPages = totalCount / pageSize + (totalCount % pageSize > 0 ? 1 : 0),
        };
    }
}