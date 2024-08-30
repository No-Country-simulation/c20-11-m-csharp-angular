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

            if (string.IsNullOrEmpty(code))
            {
                RefreshTokenDTO tokenWRT = await manageToken.GetTokenWCode(code);
                context.HttpContext.Request.Headers.Add("Refresh-Token",$"{tokenWRT.RefreshToken}");
                context.HttpContext.Request.Headers.Add("Authorization", $"Bearer {tokenWRT.AccessToken}");

                return;
            }
                
            string refresh_token = context.HttpContext.Request.Headers["Refresh-Token"].FirstOrDefault();

            if (string.IsNullOrEmpty(refresh_token))
            {
                TokenDTO token = await manageToken.GetTokenFromRT(refresh_token);
                Console.WriteLine(token.ExpiresIn);
                context.HttpContext.Request.Headers.Add("Authorization", $"Bearer {token.AccessToken}");
                return;
            }

            
            context.Result = new UnauthorizedResult();
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}