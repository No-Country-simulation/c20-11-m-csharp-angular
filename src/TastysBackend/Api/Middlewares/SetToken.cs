using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tastys.API.Token;

namespace Tastys.API.Middlewares;

public class SetToken:Attribute,IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {

            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            
            string authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                return;
            }

            ManageToken manageToken = new ManageToken(configuration);

            string code = context.HttpContext.Request.Query["code"];

            if (string.IsNullOrEmpty(code) == false)
            {
                RefreshTokenDTO tokenWRT = await manageToken.GetTokenWCode(code);
                context.HttpContext.Request.Headers.Add("Refresh-Token",$"{tokenWRT.RefreshToken}");
                context.HttpContext.Request.Headers.Add("Authorization", $"Bearer {tokenWRT.AccessToken}");
                //añadiendo la cookie del token y refresh_token
                context.HttpContext.Response.Cookies.Append("Token",tokenWRT.AccessToken,SetCookie.Config(10));
                context.HttpContext.Response.Cookies.Append("Refresh-Token",tokenWRT.RefreshToken,SetCookie.Config(10));
                return;
            }
                
            string refresh_token = context.HttpContext.Request.Headers["Refresh-Token"].FirstOrDefault();

            
            context.Result = new UnauthorizedResult();
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}