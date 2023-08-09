using ConsoleECommerce.Objects;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ConsoleECommerce.Operations
{
    internal static class LoginOperations
    {
        internal static string GetToken()
        {
            string token = "";
            using (var httpClient = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("scope", "https://api.ebay.com/oauth/api_scope/sell.inventory " +
                                                              "https://api.ebay.com/oauth/api_scope/sell.fulfillment " +
                                                              "https://api.ebay.com/oauth/api_scope/sell.account " +
                                                              "https://api.ebay.com/oauth/api_scope/sell.fulfillment.readonly " +
                                                              "https://api.ebay.com/oauth/api_scope/sell.account.readonly " +
                                                              "https://api.ebay.com/oauth/api_scope/sell.item")
                });
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                var authHeaderValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Constants.APP_ID}:{Constants.CERT_ID}"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
                HttpResponseMessage response = httpClient.PostAsync(Constants.API_TOKEN_URL, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    token = JsonConvert.DeserializeObject<TokenResponse>(responseBody).access_token;
                }
                else
                {
                    Console.WriteLine("Token request failed");
                }
            }
            return token;
        }


        internal static async Task<string> GetTokenAsync()
        {
            string token = "";

            using (var httpClient = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("scope", "https://api.ebay.com/oauth/api_scope")
                });

                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                var authHeaderValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Constants.APP_ID}:{Constants.CERT_ID}"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

                HttpResponseMessage response = await httpClient.PostAsync(Constants.API_TOKEN_URL, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    token = JsonConvert.DeserializeObject<TokenResponse>(responseBody).access_token;
                }
                else
                {
                    Console.WriteLine($"Token request failed with status code: {response.StatusCode}");
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error content: {errorContent}");
                }
            }

            return token;
        }
    }
}
