using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

internal class RequestUtilities
{
    public static bool FirstRequestTime(AuthorizationFilterContext context, float time = 0)
    {
        try
        {
            string cacheKey = "FirstRequestTime";
            var cache = context.HttpContext.RequestServices.GetService(typeof(IMemoryCache)) as IMemoryCache;
            var FrExist = cache.TryGetValue(cacheKey, out DateTime firstRequest);
            if(time == 0)
            {
                cache.Remove(cacheKey);
                return false;
            }
            if (cache == null)
            {
                throw new Exception("IMemoryCache no está disponible. Asegúrate de que está registrado en el contenedor de servicios.");
            }
            


            if (!FrExist)
            {
                Console.WriteLine("fr no existe");
                firstRequest = DateTime.Now;
                cache.Set(cacheKey, firstRequest, TimeSpan.FromMinutes(10));
                return false;
            }

            Console.WriteLine($"DIFERENCIA DE TIEMPO ENTRE REQUEST: {DateTime.Now - firstRequest} : {TimeSpan.FromMinutes(time)}");

            if ((DateTime.Now - firstRequest) > TimeSpan.FromMinutes(time))
            {
                Console.WriteLine("SE RESETEO EL CHECK TOKEN");
                cache.Set(cacheKey, DateTime.Now, TimeSpan.FromMinutes(time)); // Reiniciar el tiempo
                return false; // Reinicia el chequeo si ha pasado el tiempo
            }

            return true; // Peticiones dentro del tiempo permitido
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en FirstRequestTime: {ex.Message}");
            throw;
        }
    }

}