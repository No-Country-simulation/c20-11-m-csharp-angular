
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;

//este middleware debe ser usado despues de un checkToken para que pueda acceder al userData
public class CheckPermissions(string customPermission = null, int tokenTimeCheck = 10) : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            UserDataToken userData = (UserDataToken)context.HttpContext.Items["userdata"];

            string authId = userData.authId.Replace("|", "%7C");

            if (authId == null)
            {
                throw new Exception("No se proporciono los datos de token para chequear la permission");
            }

            if (RequestUtilities.FirstRequestTime(context, tokenTimeCheck) == false)
            {
                Console.WriteLine("SE ESTA COMPROBANDO POR API");
                string tokenApi = await Auth0Utilities.GetToken(configuration);
                //peticion a la api auth0 para verificar permisiones
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://dev-v2roygalmy6qyix2.us.auth0.com/api/v2/users/{authId}/permissions");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", $"Bearer {tokenApi}");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());

                var jsonResponse = await response.Content.ReadAsStringAsync();

                var permissions = JArray.Parse(jsonResponse);

                bool permissionExists = false;

                foreach (var permission in permissions)
                {
                    if (permission["permission_name"].ToString() == customPermission)
                    {
                        permissionExists = true;
                        break;
                    }
                }

                if (!permissionExists)
                {
                    var reset = RequestUtilities.FirstRequestTime(context);
                    throw new Exception("No esta autorizado");
                }

                Console.WriteLine($"La permisión '{customPermission}' está presente en la respuesta.");
                return;
            }
            else
            {
                Console.WriteLine("El Token fue comprobado recientemente");
                return;
            }
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}