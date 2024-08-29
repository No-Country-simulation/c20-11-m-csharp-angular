using Microsoft.AspNetCore.Mvc.Filters;

namespace Tastys.API.Middlewares;

public class CheckToken:IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}