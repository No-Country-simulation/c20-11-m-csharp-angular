using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Tastys.API.Token;

public class SetToken : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        string tokenCookieName = "token";
        string refreshTokenCookieName = "refresh-token";

        try
        {
            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            if (configuration == null)
            {
                Console.WriteLine("Configuration service is null");
                throw new NullReferenceException("Configuration service is null");
            }

            string tokenCookie = context.HttpContext.Request.Cookies[tokenCookieName];
            Console.WriteLine($"Token Cookie Read: {tokenCookie}");

            if (!string.IsNullOrEmpty(tokenCookie) && tokenCookie.StartsWith("Bearer "))
            {
                string token = tokenCookie.Substring("Bearer ".Length).Trim();
                ValidateToken validate = new ValidateToken(configuration);

                JwtSecurityToken validatedToken = await validate.ValidateAsync(token);
                if (validatedToken != null)
                {
                    context.HttpContext.Items["token"] = $"Bearer {token}";
                    Console.WriteLine("Token validated successfully");
                    return;
                }
            }

            ManageToken manageToken = new ManageToken(configuration);
            string code = context.HttpContext.Request.Query["code"];

            if (!string.IsNullOrEmpty(code))
            {
                RefreshTokenDTO tokenWRT = await manageToken.GetTokenWCode(code);

                Console.WriteLine($"Setting token cookie: Bearer {tokenWRT.AccessToken}");
                context.HttpContext.Response.Cookies.Append(tokenCookieName, $"Bearer {tokenWRT.AccessToken}", SetCookie.Config());

                Console.WriteLine($"Setting refresh-token cookie: {tokenWRT.RefreshToken}");
                context.HttpContext.Response.Cookies.Append(refreshTokenCookieName, tokenWRT.RefreshToken,SetCookie.Config());

                context.HttpContext.Items["token"] = $"Bearer {tokenWRT.AccessToken}";
                context.HttpContext.Items["refresh-token"] = tokenWRT.RefreshToken;
                return;
            }

            context.Result = new UnauthorizedResult();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
