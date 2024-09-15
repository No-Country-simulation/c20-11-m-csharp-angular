using System.Text;
using System.Text.Json;

internal class Auth0Utilities
{
        public static async Task<string> GetToken(IConfiguration configuration)
        {
            try
            {
                string clientSecret = configuration.GetSection("AUTH")["CLIENT_SECRET"];
                string clientId = configuration.GetSection("AUTH")["CLIENT_ID"];
                
                if(clientSecret == null || clientId == null)
                {
                    throw new Exception("No se aportaron datos env");
                }

                var httpClient = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://dev-v2roygalmy6qyix2.us.auth0.com/oauth/token");

                var jsonPayload = new
                {
                    client_id = $"{clientId}",
                    client_secret = $"{clientSecret}",
                    audience = "https://dev-v2roygalmy6qyix2.us.auth0.com/api/v2/",
                    grant_type = "client_credentials"
                };

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(jsonPayload);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();

                var tokenResponse = System.Text.Json.JsonSerializer.Deserialize<TokenDTO>(responseContent);

                string accessToken = tokenResponse.AccessToken;
                
                return accessToken;

            }
            catch (System.Exception)
            {
                
                throw;
            }

        }
}