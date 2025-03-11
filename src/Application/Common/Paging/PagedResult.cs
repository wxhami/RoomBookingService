﻿namespace Application.Common.Paging;

public class PagedResult<T>
{
    public IList<T> Items { get; set; } = [];

    public int Count { get; set; }

    public int TotalCount { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalPages { get; set; }
}