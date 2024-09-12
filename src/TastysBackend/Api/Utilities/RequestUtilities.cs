using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

internal class RequestUtilities
{
    public static bool FirstRequestTime(AuthorizationFilterContext context,int time = 10)
    {
        try
        {   
            string cacheKey = "FirstRequestTime";
            var cache = context.HttpContext.RequestServices.GetService(typeof(IMemoryCache)) as IMemoryCache;
            var FrExist = cache.TryGetValue(cacheKey, out DateTime firstRequest);
            if(!FrExist)
            {
                Console.WriteLine("fr no existe");
                firstRequest = DateTime.Now;
                cache.Set(cacheKey, firstRequest,TimeSpan.FromMinutes(time));
                return false;
            }
            Console.WriteLine($"DIFERENCIA DE TIEMPO ENTRE REQUEST : {DateTime.Now - firstRequest} : {TimeSpan.FromMinutes(time)}");
            if((DateTime.Now - firstRequest) > TimeSpan.FromMinutes(time))
            {
                Console.WriteLine("SE RESETEO EL CHECK TOKEN");

                return false;
            }else{
                return true;
            }
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}