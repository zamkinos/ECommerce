using ConsoleECommerce.Objects;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ConsoleECommerce
{
    internal class AllInOne
    {
        public async void Test()
        {

            Constants.OAuthUserToken = await GetTokenAsync();

            GetOrdersResponse objGetOrdersResponse = await GetOrdersAsync("2023-08-01T08:30:00.000");

            foreach (Order o in objGetOrdersResponse.orders)
            {
                Console.WriteLine("Order Id : " + o.orderId + " - Creation Date : " + o.creationDate.ToString("dd.MM.yyyy HH:mm:ss"));
            }
        }
        private static async Task<string> GetTokenAsync()
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

        private async Task<GetOrdersResponse> GetOrdersAsync(string dt)
        {
            string p = null;
            BasicResponse res = await SendRequestWithBearerTokenAsync("fulfillment/v1/order?filter=creationdate:%5B" + dt + "Z..%5D", p);
            if (res.HasError)
            {
                // Log the error later
                return new GetOrdersResponse();
            }
            else
            {
                return JsonConvert.DeserializeObject<GetOrdersResponse>(res.JsonResult);
            }
        }

        private static async Task<BasicResponse> SendRequestWithBearerTokenAsync<T>(string method, T pObject)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(360);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Constants.OAuthUserToken);

                    HttpResponseMessage message;

                    if (pObject == null)
                    {
                        message = await client.GetAsync(Constants.API_MAIN_URL + method);
                    }
                    else
                    {
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(pObject), Encoding.UTF8, "application/json");
                        message = await client.PostAsync(Constants.API_MAIN_URL + method, content);
                    }

                    if (message.IsSuccessStatusCode)
                    {
                        return new BasicResponse() { HasError = false, JsonResult = await message.Content.ReadAsStringAsync() };
                    }
                    else
                    {
                        Console.WriteLine($"API request failed with status code: {message.StatusCode}");
                        var errorContent = await message.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error content: {errorContent}");

                        return new BasicResponse() { HasError = true, JsonResult = errorContent };
                    }
                }
            }
            catch (Exception ex)
            {
                return new BasicResponse() { HasError = true, JsonResult = ex.Message };
            }
        }

    }
}
