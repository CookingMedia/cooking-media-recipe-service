using System.Linq.Dynamic.Core;
using CookingMedia.Recipe.EntityModels.Dto.Requests;
using CookingMedia.Recipe.EntityModels.Dto.Responses;
using Microsoft.IdentityModel.Tokens;

namespace CookingMedia.Recipe.Repositories;

internal static class QueryExtension
{
    public static PagedResult<T> AddPaging<T>(this IQueryable<T> query, PageRequest pageRequest)
    {
        var (page, pageSize, sort, direction) = pageRequest;
        if (direction == Direction.Desc)
        {
            sort += " desc";
        }

        if (!pageRequest.OrderBy.IsNullOrEmpty())
        {
            query = query.OrderBy(sort);
        }
        return query.PageResult(page, pageSize);
    }

    public static PageResult<T> ToPageResult<T>(this PagedResult<T> query)
    {
        return new PageResult<T>
        {
            Result = query.Queryable.ToList(),
            Total = query.RowCount,
            PageCount = query.PageCount,
            Page = query.CurrentPage,
            Size = query.PageSize
        };
    }
}
