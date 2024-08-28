using Microsoft.AspNetCore.Mvc.Filters;

namespace Tastys.API.Middlewares;

public class Authenticate:IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            

        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}