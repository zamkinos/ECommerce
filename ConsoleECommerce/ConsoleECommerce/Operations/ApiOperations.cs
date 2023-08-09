using ConsoleECommerce.Objects;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ConsoleECommerce.Operations
{
    internal static class ApiOperations
    {
        public static BasicResponse SendRequestWithBearerToken<T>(string method, T pObject)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 360);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Constants.OAuthUserToken);
                    HttpResponseMessage message;
                    if (pObject == null)
                    {
                        message = client.GetAsync(Constants.API_MAIN_URL + method).Result;
                    }
                    else
                    {
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(pObject), Encoding.UTF8, "application/json");
                        message = client.PostAsync(Constants.API_MAIN_URL + method, content).Result;
                    }
                    if (message.IsSuccessStatusCode)
                    {
                        return new BasicResponse() { HasError = false, JsonResult = message.Content.ReadAsStringAsync().Result };
                    }
                    else
                    {
                        return new BasicResponse() { HasError = true, JsonResult = message.Content.ToString() };
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
