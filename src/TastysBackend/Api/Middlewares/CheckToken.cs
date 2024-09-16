using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tastys.API.Token;
using Tastys.BLL;
using Tastys.BLL.Utils;

namespace Tastys.API.Middlewares;

public class CheckToken : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            if (context.HttpContext.Request.Cookies.TryGetValue("token", out string tokenCookie))
            {
                Console.WriteLine($"Cookie token exist");
                context.HttpContext.Items["token"] = $"{tokenCookie}";
            }
            else
            {
                Console.WriteLine("Cookie token not found.");
            }

            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            if (configuration == null)
            {
                throw new NullReferenceException("Configuration service is null");
            }

            string itemToken = (string)context.HttpContext.Items["token"];
            Console.WriteLine($"TOKEN DE Items: {itemToken}");

            if (!string.IsNullOrEmpty(itemToken) && itemToken.StartsWith("Bearer "))
            {
                string token = itemToken.Substring("Bearer ".Length).Trim();
                Console.WriteLine($"Token Extraído");

                ValidateToken validate = new ValidateToken(configuration);
                JwtSecurityToken validatedToken = await validate.ValidateAsync(token);

                if (validatedToken != null)
                {
                    Dictionary<string, string> claims = JwtValidate.ValidateClaimsToken(token, ["custom_email_claim", "custom_name_claim", "sub"]);

                    if (claims != null && claims.ContainsKey("sub"))
                    {
                        UserDataToken userData = new UserDataToken
                        {
                            authId = claims["sub"],
                            authName = claims.ContainsKey("custom_name_claim") ? claims["custom_name_claim"] : null,
                            email = claims.ContainsKey("custom_email_claim") ? claims["custom_email_claim"] : null,
                            token = token
                        };

                        context.HttpContext.Items["userdata"] = userData;
                        Console.WriteLine("Token Validado y Datos de Usuario Agregados al Contexto");
                        return;
                    }
                }

                // Si el token no es válido, trata de generar uno nuevo usando el refresh token
                if (context.HttpContext.Request.Cookies.TryGetValue("refresh-token", out string cookieRTValue))
                {
                    Console.WriteLine($"Cookie exist");
                    context.HttpContext.Items["refresh-token"] = $"Bearer {tokenCookie}";
                }
                else
                {
                    Console.WriteLine("Cookie not found.");
                    throw new Exception("No Refresh Token");
                }
                Console.WriteLine($"Refresh-Token Cookie exist");

                if (!string.IsNullOrEmpty(cookieRTValue))
                {
                    ManageToken manageToken = new ManageToken(configuration);
                    TokenDTO newToken = await manageToken.GetTokenFromRT(cookieRTValue);
                    Console.WriteLine($"Nuevo Token Generado, Expira en: {newToken.ExpiresIn}");

                    context.HttpContext.Response.Cookies.Append("Token", $"Bearer {newToken.AccessToken}", new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(7),
                        HttpOnly = true,
                        IsEssential = true,
                        Secure = true,
                        SameSite = SameSiteMode.None
                    });
                    context.HttpContext.Request.Headers["Authorization"] = $"Bearer {newToken.AccessToken}";
                    return;
                }
            }
            else
            {
                throw new UnauthorizedException("Falta el refresh token.");
            }
        }
        catch (UnauthorizedException)
        {
            throw;
        }
        catch (Exception ex) 
        {
            throw new UnauthorizedException($"Error en la validación del token: {ex.Message}");
        }
    }
}
