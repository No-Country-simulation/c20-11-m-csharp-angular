using Microsoft.AspNetCore.Mvc.Filters;

namespace Tastys.API.Token;

internal class ManageToken(IConfiguration configuration)
{
    public async Task<RefreshTokenDTO> GetTokenWCode(string code)
    {
        try
        {
            Console.WriteLine(code);
            var CLIEN_ID = configuration.GetSection("AUTH")["CLIEN_ID"];
            var CLIENT_SECRET = configuration.GetSection("AUTH")["CLIENT_SECRET"];
            var CLIENT_HOST = configuration.GetSection("AUTH")["CLIEN_HOST"];
            
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,"https://dev-v2roygalmy6qyix2.us.auth0.com/oauth/token");

            var formData = new Dictionary<string,string>{
                {"grant_type", "authorization_code"},
                {"client_id", $"{CLIEN_ID}"},
                {"client_secret", $"{CLIENT_SECRET}"},
                {"code", $"{code}"},
                {"redirect_uri", $"{CLIENT_HOST}/pages/redirect"}
            };
            
            request.Content = new FormUrlEncodedContent(formData);
            HttpResponseMessage response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            RefreshTokenDTO token = await DeserializeRToken(response);

            Console.WriteLine($"Token: {token.AccessToken}");

            return token;

        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
    public async Task<TokenDTO> GetTokenFromRT(string refreshToken)
    {
        try
        {
                var CLIEN_ID = configuration.GetSection("AUTH")["CLIEN_ID"];
                var CLIENT_SECRET = configuration.GetSection("AUTH")["CLIENT_SECRET"];
                var CLIENT_HOST = configuration.GetSection("AUTH")["CLIEN_HOST"];

                var httpClient = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post,"https://dev-v2roygalmy6qyix2.us.auth0.com/oauth/token");

                // Add form data
                var formData = new Dictionary<string,string>
                {
                    {"grant_type", "refresh_token"},
                    {"client_id", $"{CLIEN_ID}"},
                    {"client_secret", $"{CLIENT_SECRET}"},
                    {"refresh_token", $"{refreshToken}"},
                    {"redirect_uri", $"{CLIENT_HOST}/pages/redirect"}
                };

                request.Content = new FormUrlEncodedContent(formData);

                HttpResponseMessage response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                TokenDTO token = await DeserializeToken(response);
                Console.WriteLine($"TOKEN DE RT {token.AccessToken}" );

                return token;

        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
    private static async Task<RefreshTokenDTO> DeserializeRToken(HttpResponseMessage dataResponse)
    {
        var responseContent = await dataResponse.Content.ReadAsStringAsync();

        var RtokenResponse = System.Text.Json.JsonSerializer.Deserialize<RefreshTokenDTO>(responseContent);

        return RtokenResponse;
    }
    private static async Task<TokenDTO> DeserializeToken(HttpResponseMessage dataResponse)
    {
        var responseContent = await dataResponse.Content.ReadAsStringAsync();

        var RtokenResponse = System.Text.Json.JsonSerializer.Deserialize<TokenDTO>(responseContent);

        return RtokenResponse;
    }
}