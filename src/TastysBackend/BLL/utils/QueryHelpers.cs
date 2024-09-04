namespace Tastys.BLL;

public static class QueryHelpers
{
    /// <summary>
    /// Pagina los resultados de una query que es subclase de PaginatedQuery.
    /// </summary>
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginatedQuery queryParameters)
    {
        if (queryParameters.Offset.HasValue)
            query = query.Skip(queryParameters.Offset.Value);

        if (queryParameters.Length.HasValue)
            query = query.Take(queryParameters.Length.Value);

        return query;
    }
}
