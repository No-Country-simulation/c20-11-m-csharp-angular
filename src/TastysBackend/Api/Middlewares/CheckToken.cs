using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tastys.BLL.Utils;

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
                    Dictionary<string,string> claims = JwtValidate.ValidateClaimsToken(token,["custom_email_claim","custom_name_claim","sub"]);

                    UserDataToken userData = new UserDataToken{
                        authId = claims["sub"],
                        authName = claims["custom_name_claim"],
                        email = claims["custom_email_claim"],
                        token = token,
                    };

                    //AÑADO EL ITEM userdata para poder acceder desde el controlador a los datos del token mandado por el cliente.
                    context.HttpContext.Items.Add("userdata", userData);

                    Console.WriteLine("Token Validado");
                    return;
                }

            }
            throw new UnauthorizedAccessException("Error en la validacion del token");
        }
        catch (System.Exception)
        {
            context.Result = new UnauthorizedResult();
            throw;
        }
    }
}