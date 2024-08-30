using System.IdentityModel.Tokens.Jwt;

namespace Tastys.BLL.Utils;

public class JwtValidate
{
    //este metodo convierte el string token un jwt para despues obtener sus claims que estaran especificados en una lista de strings.
    public static Dictionary<string,string> ValidateClaimsToken(string token,List<string> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwt = tokenHandler.ReadJwtToken(token);

        Dictionary<string,string> result = new Dictionary<string, string>();

        foreach (var type in claims)
        {
            string value = jwt.Claims.First(claim => claim.Type == type).Value;
            if (value != null)
            {
                result.Add(type, value);
            }
        }

        return result;
    }
}