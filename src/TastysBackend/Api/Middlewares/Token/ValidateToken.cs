using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
internal class ValidateToken(IConfiguration configuration)
{
    public async Task<JwtSecurityToken> ValidateAsync(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            Console.WriteLine($"SOY EL TOKEN VALIDATETOKENASYNC {token}");
            var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
            $"https://{configuration.GetSection("AUTH")["DOMAIN"]}/.well-known/openid-configuration",
            new OpenIdConnectConfigurationRetriever(),
            new HttpDocumentRetriever());

            var discoveryDocument = await configurationManager.GetConfigurationAsync();
            var signingKeys = discoveryDocument.SigningKeys;
            Console.WriteLine($"https://{configuration.GetSection("AUTH")["DOMAIN"]}/");

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidIssuer = $"{configuration.GetSection("AUTH")["DOMAIN"]}",
                ValidateAudience = false,
                // ValidAudience = $"{configuration.GetSection("AUTH")["DOMAIN"]}",
                ValidAudience = $"https://{configuration.GetSection("AUTH")["DOMAIN"]}",
                ValidateIssuerSigningKey = true,
                IssuerSigningKeys = signingKeys,
                ValidateLifetime = false
            };

            var handler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwtToken = handler.ReadJwtToken(token);
            
            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            
            jwtToken = (JwtSecurityToken)validatedToken;
                        
            return jwtToken;
            
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}