using ConsoleECommerce.Objects;
using Newtonsoft.Json;

namespace ConsoleECommerce.Operations
{
    internal class OrderOperations
    {
        public async Task<GetOrdersResponse> GetOrdersAsync(string dt)
        {
            try
            {
                string p = null;
                BasicResponse res = await ApiOperations.SendRequestWithBearerTokenAsync("fulfillment/v1/order?filter=creationdate:%5B" + dt + "Z..%5D", p);

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
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetOrdersAsync: {ex}");
                return new GetOrdersResponse();
            }
        }
    }
}
