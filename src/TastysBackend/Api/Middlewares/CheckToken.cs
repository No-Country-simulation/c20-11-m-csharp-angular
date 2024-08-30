using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tastys.API.Middlewares;

public class CheckToken:Attribute,IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            
            string authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            
            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length).Trim();

                ValidateToken validate = new ValidateToken(configuration);

                JwtSecurityToken validatedToken = await validate.ValidateAsync(token);
                if (validatedToken == null)
                {
                    context.Result = new UnauthorizedResult();
                    throw new UnauthorizedAccessException("Token invalido");
                }
                else
                {
                    Console.WriteLine("Token Validado");
                }

                

                return;

            }

        }
        catch (System.Exception)
        {
            context.Result = new UnauthorizedResult();
            throw;
        }
    }
}