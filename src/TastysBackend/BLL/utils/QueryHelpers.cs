namespace Tastys.BLL;

public static class QueryHelpers
{
    /// <summary>
    /// Pagina los resultados de una query que es subclase de PaginatedQuery.
    /// </summary>
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginatedQuery queryParameters)
    {
        if (queryParameters.Page.HasValue && queryParameters.PageSize.HasValue)
            query = query
                .Skip(queryParameters.Page.Value * queryParameters.PageSize.Value)
                .Take(queryParameters.PageSize.Value);

        return query;
    }
}
